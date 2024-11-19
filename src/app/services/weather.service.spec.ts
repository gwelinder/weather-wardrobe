import { TestBed } from '@angular/core/testing';
import { WeatherService } from './weather.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { WeatherCondition } from '../models/weather';
import { environment } from '../environment/environment';
describe('WeatherService', () => {
  let service: WeatherService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WeatherService],
    });
    service = TestBed.inject(WeatherService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve weather data for a location', () => {
    const dummyWeather: WeatherCondition = {
      weatherConditionId: 1,
      conditionName: 'Sunny',
      temperatureRange: '10-20',
      clothingItems: [],
      temperature: 10,
    };

    service.getWeatherData('New York').subscribe((weather) => {
      expect(weather).toEqual(dummyWeather);
    });

    const req = httpMock.expectOne(
      `${environment.apiUrl}/api/weather?location=New York`
    );
    expect(req.request.method).toBe('GET');
    req.flush(dummyWeather);
  });
});
