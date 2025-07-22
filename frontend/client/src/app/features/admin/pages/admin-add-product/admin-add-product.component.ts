import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductAddForm } from '../../../../shared/models/product-add-form';
import { AdminProductService } from '../../services/admin-product.service';
import { FormErrorService } from '../../../../core/services/form-error.service';
import { combineLatest } from 'rxjs';
import { CategoryForForm } from '../../../../shared/models/categories-for-form';

@Component({
  selector: 'app-admin-add-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-add-product.component.html',
  styleUrl: './admin-add-product.component.scss',
})
export class AdminAddProductComponent implements OnInit {
  form!: FormGroup;
  enums!: ProductAddForm;
  categories: CategoryForForm[] = [];
  selectedCategoryIds: number[] = [];

  constructor(
    private fb: FormBuilder,
    private adminProductService: AdminProductService,
    private formErrorService: FormErrorService
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadEnumOptions();

    combineLatest([
      this.form.get('gender')!.valueChanges,
      this.form.get('ageGroup')!.valueChanges,
    ]).subscribe(([gender, ageGroup]) => {
      if (gender && ageGroup) {
        this.loadCategories();
      }
    });
  }
  loadCategories(): void {
    const gender = this.form.get('gender')!.value;
    const ageGroup = this.form.get('ageGroup')!.value;

    if (gender != null && ageGroup != null) {
      this.adminProductService
        .getCategoriesByGenderAndAgeGroup(gender, ageGroup)
        .subscribe({
          next: (data) => {
            this.categories = data;
          },
          error: (err) => {
            console.error('Error loading categories:', err);
          },
        });
    } else {
      const formArray = this.form.get('selectedCategoryIds') as FormArray;
      while (formArray.length) {
        formArray.removeAt(0);
      }
    }
  }
  onCategoryToggle(event: Event, categoryId: number): void {
  const input = event.target as HTMLInputElement;
  const checked = input.checked;

  const formArray = this.form.get('selectedCategoryIds') as FormArray;

  if (checked) {
    formArray.push(new FormControl(categoryId));
  } else {
    const index = formArray.controls.findIndex(ctrl => ctrl.value === categoryId);
    if (index !== -1) {
      formArray.removeAt(index);
    }
  }
}

  private buildForm(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      shortDescription: ['', [Validators.required, Validators.maxLength(500)]],
      description: ['', [Validators.required, Validators.maxLength(2000)]],
      brand: ['', [Validators.required, Validators.maxLength(100)]],
      purpose: [null, Validators.required],
      fit: [null, Validators.required],
      gender: [null, Validators.required],
      ageGroup: [null, Validators.required],
      material: [null, Validators.required],
      season: [null, Validators.required],
      seoTitle: ['', [Validators.maxLength(100)]],
      seoDescription: ['', [Validators.maxLength(150)]],
      isActive: [true],
      selectedCategoryIds: this.fb.array([], Validators.required),
    });
  }

  private loadEnumOptions(): void {
    this.adminProductService.getFormData().subscribe({
      next: (data) => {
        this.enums = data;
      },
      error: (err) => {
        console.error('Error loading enum options:', err);
      },
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const product = this.form.value;
      console.log('Saving product:', product);
      // this.productService.addProduct(product).subscribe(...)
    } else {
      this.form.markAllAsTouched();
    }
  }
  getErrorMessage(controlName: string): string | null {
    return this.formErrorService.getErrorMessage(this.form.get(controlName));
  }
  toggleCategory(categoryId: number): void {
  const formArray = this.form.get('selectedCategoryIds') as FormArray;
  const index = formArray.controls.findIndex(ctrl => ctrl.value === categoryId);

  if (index === -1) {
    formArray.push(new FormControl(categoryId));
  } else {
    formArray.removeAt(index);
  }
}
}
