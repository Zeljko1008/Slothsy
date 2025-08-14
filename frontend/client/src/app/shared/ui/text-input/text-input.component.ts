import { CommonModule } from '@angular/common';
import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent implements ControlValueAccessor{
  @Input() label: string = '';
  @Input() type: string = 'text';
   _value: any = '';

   onChange = (_: any) => {};
  onTouched = () => {};

  constructor(@Self() public controlDir: NgControl){
    this.controlDir.valueAccessor = this;
  }

  writeValue(value: any): void {
     this._value = value;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onBlur() {
    this.onTouched();
  }

   onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    this._value = input.value;
    this.onChange(this._value);
  }

  get control(): FormControl {
    return this.controlDir.control as FormControl
  }

}
