// clothing.service.spec.ts
import { TestBed } from '@angular/core/testing';
import { ClothingService } from './clothing.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { ClothingItem } from '../models/wardrobe';
import { environment } from '../environment/environment';

describe('ClothingService', () => {
  let service: ClothingService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ClothingService],
    });
    service = TestBed.inject(ClothingService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all clothing items', () => {
    const dummyItems: ClothingItem[] = [
      {
        clothingItemId: 1,
        name: 'Jacket',
        description: 'A warm winter jacket',
        imageURL: 'assets/jacket.png',
        weatherConditionId: 1,
      },
      {
        clothingItemId: 2,
        name: 'T-Shirt',
        description: 'A cool summer t-shirt',
        imageURL: 'assets/tshirt.png',
        weatherConditionId: 2,
      },
    ];

    service.getAllClothingItems().subscribe((items) => {
      expect(items.length).toBe(2);
      expect(items).toEqual(dummyItems);
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/api/clothingitems`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyItems);
  });

  it('should delete a clothing item', () => {
    service.deleteClothingItem(1).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/api/clothingitems/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
