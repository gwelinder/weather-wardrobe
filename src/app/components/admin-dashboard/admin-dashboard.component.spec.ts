// admin-dashboard.component.spec.ts
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { ClothingService } from '../../services/clothing.service';
import { of } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';

class MockClothingService {
  getAllClothingItems() {
    return of([
      {
        id: 1,
        name: 'Jacket',
        description: 'A warm winter jacket',
        imageURL: 'assets/jacket.png',
      },
    ]);
  }

  deleteClothingItem(itemId: number) {
    return of(null);
  }
}

describe('AdminDashboardComponent', () => {
  let component: AdminDashboardComponent;
  let fixture: ComponentFixture<AdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminDashboardComponent, MatButtonModule],
      providers: [{ provide: ClothingService, useClass: MockClothingService }],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
