 import defaultTheme from 'tailwindcss/defaultTheme';

 /** @type {import('tailwindcss').Config} */
export default {
   content: ["./src/**/*.{html,js}"],
   theme: {
     extend: {
       animation: {
        'fade-in': 'fadeIn 1s ease-in-out',
        'slide-in-right': 'slideInRight 1s ease-out',
      },
       fontFamily: {
        raleway: ['Raleway', ...defaultTheme.fontFamily.sans]
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        },
        slideInRight: {
          '0%': { transform: 'translateX(100%)', opacity: '0' },
          '100%': { transform: 'translateX(0)', opacity: '1' },
        },
     },
   },
   plugins: [],
 }
}

