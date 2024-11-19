// admin-dashboard.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClothingService } from '../../services/clothing.service';
import { ClothingItem } from '../../models/wardrobe';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss'],
})
export class AdminDashboardComponent implements OnInit {
  clothingItems: ClothingItem[] = [];

  constructor(private clothingService: ClothingService) {}

  ngOnInit(): void {
    this.clothingService
      .getAllClothingItems()
      .subscribe((items: ClothingItem[]) => {
        this.clothingItems = items;
      });
  }

  deleteItem(itemId: number): void {
    this.clothingService.deleteClothingItem(itemId).subscribe(() => {
      this.clothingItems = this.clothingItems.filter(
        (item) => item.clothingItemId !== itemId
      );
    });
  }
}
