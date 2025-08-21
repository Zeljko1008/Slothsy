using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Slothsy.Application.Interfaces;
using Slothsy.Application.Mappings;
using Slothsy.Application.Services;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using Slothsy.Infrastructure.Data;
using Slothsy.Infrastructure.Identity.Config;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Interfaces;
using Slothsy.Infrastructure.Identity.Seed;
using Slothsy.Infrastructure.Identity.Services;
using Slothsy.Infrastructure.Persistance.Repositories;
using System.Text;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
{
    var secretFromEnv = Environment.GetEnvironmentVariable("JWT_SECRET");
    if (!string.IsNullOrEmpty(secretFromEnv))
    {
        jwtSettings.Secret = secretFromEnv;
    }
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    }; options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }
    };
});



// ------------------------------------------------------------
// Register application services and repositories
// ------------------------------------------------------------
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductReadService, ProductReadService>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
builder.Services.AddScoped<IProductColorVariantRepository, ProductColorVariantRepository>();
builder.Services.AddScoped<IProductColorVariantService, ProductColorVariantService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryReadService, CategoryReadService>();
builder.Services.AddScoped<IAdminProductService, AdminProductService>();
builder.Services.AddScoped<IAdminProductColorVariantService, AdminProductColorVariantService>();
builder.Services.AddScoped<IColorOptionRepository , ColorOptionRepository>();
builder.Services.AddScoped<IProductColorVariantService, ProductColorVariantService>();
builder.Services.AddScoped<IAdminProductVariantService, AdminProductVariantService>();
builder.Services.AddScoped<ISizeOptionRepository, SizeOptionRepository>();
builder.Services.AddScoped<IAdminSizeOptionService, AdminSizeOptionService>();
builder.Services.AddScoped<IAdminEnumService, AdminEnumService>();
builder.Services.AddScoped<IAdminCategoryService, AdminCategoryService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenCleanupService, RefreshTokenCleanupService>();
builder.Services.AddHostedService<RefreshTokenCleanupBackgroundService>();
builder.Services.AddScoped<IAuthService, AuthService>();






// ------------------------------------------------------------
// Configure database context with SQL Server connection string
// ------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//--------------------------------------------------------------
// Configure Identity services with custom user and role classes
//--------------------------------------------------------------


builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddSignInManager<SignInManager<ApplicationUser>>()
.AddDefaultTokenProviders();


// ------------------------------------------------------------
// Register AutoMapper profiles from the specified assembly
// ------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductCategoryProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductVariantProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductVariantImageProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProductColorVariantProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ColorOptionProfile).Assembly);
builder.Services.AddAutoMapper(typeof(SizeOptionProfile).Assembly);

// ------------------------------------------------------------
// Add controllers (API endpoints)
// ------------------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// ------------------------------------------------------------
// Add Swagger/OpenAPI support for API documentation
// ------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ------------------------------------------------------------
// Configure settings for refresh token cleanup
// ------------------------------------------------------------
builder.Services.Configure<RefreshTokenCleanupSettings>(
    builder.Configuration.GetSection("RefreshTokenCleanup"));


// ------------------------------------------------------------
// Configure CORS policy to allow requests from any origin
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
    builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
    });
});

builder.Services.AddTransient<IdentitySeeder>();
var app = builder.Build();

if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    app.Urls.Clear();
    app.Urls.Add("http://+:80"); // Docker
}



// ------------------------------------------------------------
// Use centralized error handling endpoint (only in production)
// ------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------------------------------------------------
// Initialize and seed the database on app startup
// ------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await DbInitializer.InitializeAsync(context);

        var identitySeeder = services.GetRequiredService<IdentitySeeder>();
        await identitySeeder.SeedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during DB initialization: {ex.Message}");
    }
}


app.UseStaticFiles();
// ------------------------------------------------------------
// Enforce HTTPS redirection
// ------------------------------------------------------------
if (app.Environment.IsDevelopment() && Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
{
    app.UseHttpsRedirection();
}

// ------------------------------------------------------------
// Use routing middleware to match incoming requests to endpoints
// ------------------------------------------------------------
app.UseRouting();

// ------------------------------------------------------------
// Use CORS middleware to apply the defined policy
// ------------------------------------------------------------
app.UseCors();

// ------------------------------------------------------------
// Use authentication middleware to validate user credentials
// ------------------------------------------------------------
app.UseAuthentication();
// ------------------------------------------------------------
// Use authorization middleware to enforce access control
// ------------------------------------------------------------
app.UseAuthorization();

// ------------------------------------------------------------
// Map controller routes for incoming requests
// ------------------------------------------------------------

app.MapControllers();

// ------------------------------------------------------------
// Run the application
// ------------------------------------------------------------
app.Run();
