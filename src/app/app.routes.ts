// app.routes.ts
import { Routes } from '@angular/router';
import { LocationInputComponent } from './components/location-input/location-input.component';
import { ClothingRecommendationComponent } from './components/clothing-recommendation/clothing-recommendation.component';
import { LoginComponent } from './components/login/login.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { NotFoundComponent } from './components/not-found/not-found.component';

export const routes: Routes = [
  { path: '', component: LocationInputComponent },
  { path: 'recommendations', component: ClothingRecommendationComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', component: NotFoundComponent },
];
