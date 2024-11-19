import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ClothingItemComponent } from './clothing-item.component';
import { ClothingItem } from '../../models/wardrobe';

describe('ClothingItemComponent', () => {
  let component: ClothingItemComponent;
  let fixture: ComponentFixture<ClothingItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClothingItemComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ClothingItemComponent);
    component = fixture.componentInstance;
    component.item = {
      name: 'Jacket',
      description: 'A warm winter jacket',
      imageURL: 'assets/jacket.png',
      clothingItemId: 1,
      weatherConditionId: 1,
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
