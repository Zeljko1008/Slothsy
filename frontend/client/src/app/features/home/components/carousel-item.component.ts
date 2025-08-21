import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Categories } from '../../../shared/models/categories';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-carousel-item',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './carousel-item.component.html',
  styleUrl: './carousel-item.component.scss',
})
export class CarouselItemComponent {
  @Input() imageUrl!: string;
  @Input() shortDescription!: string;
  @Input() fullDescription!: string;
  @Input() link!: string;
  @Input() category!: Categories;

  baseImgUrl = environment.baseImgUrl;
}
