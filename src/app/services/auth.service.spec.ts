import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { environment } from '../environment/environment';
describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService],
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login and set authentication token', () => {
    service.login('user', 'password').subscribe((success) => {
      expect(success).toBeTrue();
      expect(localStorage.getItem('authToken')).toBe(btoa('user:password'));
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/api/auth/validate`);
    expect(req.request.method).toBe('GET');
    req.flush({});
  });

  it('should logout and remove authentication token', () => {
    service.logout();
    expect(service.isLoggedIn()).toBeFalse();
    expect(localStorage.getItem('authToken')).toBeNull();
  });
});
