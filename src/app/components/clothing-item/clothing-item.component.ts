// clothing-item.component.ts
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClothingItem } from '../../models/wardrobe';

@Component({
  selector: 'app-clothing-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './clothing-item.component.html',
  styleUrls: ['./clothing-item.component.scss'],
})
export class ClothingItemComponent {
  @Input() item!: ClothingItem;
}
