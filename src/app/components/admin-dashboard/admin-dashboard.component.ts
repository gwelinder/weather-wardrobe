// admin-dashboard.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ClothingService } from '../../services/clothing.service';
import { ClothingItem } from '../../models/wardrobe';
import { WeatherCondition } from '../../models/weather';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule,
    MatIconModule,
    MatDialogModule,
    MatChipsModule,
    MatDividerModule,
    MatTooltipModule
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss'],
})
export class AdminDashboardComponent implements OnInit {
  clothingItems: ClothingItem[] = [];
  weatherConditions: WeatherCondition[] = [];
  itemForm: FormGroup;
  editingItem: ClothingItem | null = null;
  imagePreview: string | null = null;
  selectedFile: File | null = null;

  constructor(
    private clothingService: ClothingService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.itemForm = this.fb.group({
      Name: ['', Validators.required],
      Description: ['', Validators.required],
      ImageURL: [''],
      WeatherConditions: [[], Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadClothingItems();
    this.loadWeatherConditions();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  removeImage(): void {
    this.imagePreview = null;
    this.selectedFile = null;
    this.itemForm.patchValue({ ImageURL: '' });
  }

  async uploadImage(): Promise<string> {
    if (!this.selectedFile) {
      return this.itemForm.get('ImageURL')?.value || '';
    }

    // Create FormData
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    try {
      const response = await firstValueFrom(this.clothingService.uploadImage(formData));
      return response?.imageUrl || '';
    } catch (error) {
      console.error('Error uploading image:', error);
      this.showError('Failed to upload image');
      return '';
    }
  }

  async onSubmit(): Promise<void> {
    if (this.itemForm.valid) {
      try {
        // First upload the image
        const imageUrl = await this.uploadImage();
        
        // Get the raw form values - they will already be in PascalCase from the form control names
        const formValues = this.itemForm.value;
        console.log('Form values:', formValues);
        
        // Transform weather conditions to only include IDs
        const weatherConditionIds = formValues.WeatherConditions.map((condition: WeatherCondition) => condition.WeatherConditionId);
        
        const item: Partial<ClothingItem> = {
          Name: formValues.Name,
          Description: formValues.Description,
          ImageURL: imageUrl,
          WeatherConditionIds: weatherConditionIds
        };
        
        console.log('Request payload:', item);

        if (this.editingItem) {
          // Update existing item
          const updatedItem = { ...this.editingItem, ...item };
          this.clothingService.updateClothingItem(updatedItem).subscribe({
            next: () => {
              this.loadClothingItems();
              this.resetForm();
              this.showSuccess('Item updated successfully');
            },
            error: (error) => {
              this.showError('Failed to update item');
              console.error('Update error:', error);
            }
          });
        } else {
          // Create new item
          this.clothingService.createClothingItem(item).subscribe({
            next: () => {
              this.loadClothingItems();
              this.resetForm();
              this.showSuccess('Item added successfully');
            },
            error: (error) => {
              this.showError('Failed to add item');
              console.error('Create error:', error);
            }
          });
        }
      } catch (error) {
        this.showError('Failed to process image');
        console.error('Submit error:', error);
      }
    } else {
      console.log('Form validation errors:', this.itemForm.errors);
      Object.keys(this.itemForm.controls).forEach(key => {
        const control = this.itemForm.get(key);
        if (control?.errors) {
          console.log(`${key} errors:`, control.errors);
        }
      });
    }
  }

  resetForm(): void {
    this.itemForm.reset({
      Name: '',
      Description: '',
      ImageURL: '',
      WeatherConditions: []
    });
    this.editingItem = null;
    this.imagePreview = null;
    this.selectedFile = null;
  }

  editItem(item: ClothingItem): void {
    this.editingItem = item;
    this.itemForm.patchValue({
      Name: item.Name,
      Description: item.Description,
      ImageURL: item.ImageURL,
      WeatherConditions: item.WeatherConditions
    });
  }

  deleteItem(itemId: number | undefined): void {
    if (!itemId) return;
    
    if (confirm('Are you sure you want to delete this item?')) {
      this.clothingService.deleteClothingItem(itemId).subscribe({
        next: () => {
          this.loadClothingItems();
          this.showSuccess('Item deleted successfully');
        },
        error: (error) => {
          this.showError('Failed to delete item');
          console.error('Delete error:', error);
        }
      });
    }
  }

  loadClothingItems(): void {
    this.clothingService.getAllClothingItems().subscribe({
      next: (items: ClothingItem[]) => {
        this.clothingItems = items;
      },
      error: (error) => {
        this.showError('Failed to load clothing items');
      }
    });
  }

  loadWeatherConditions(): void {
    this.clothingService.getWeatherConditions().subscribe({
      next: (conditions: WeatherCondition[]) => {
        this.weatherConditions = conditions;
      },
      error: (error) => {
        this.showError('Failed to load weather conditions');
      }
    });
  }

  showSuccess(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['success-snackbar']
    });
  }

  showError(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['error-snackbar']
    });
  }
}
