import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ClothingRecommendationComponent } from './clothing-recommendation.component';
import { ClothingService } from '../../services/clothing.service';
import { of } from 'rxjs';

class MockClothingService {
  getRecommendations() {
    return of([]);
  }
}

describe('ClothingRecommendationComponent', () => {
  let component: ClothingRecommendationComponent;
  let fixture: ComponentFixture<ClothingRecommendationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClothingRecommendationComponent],
      providers: [{ provide: ClothingService, useClass: MockClothingService }],
    }).compileComponents();

    fixture = TestBed.createComponent(ClothingRecommendationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
