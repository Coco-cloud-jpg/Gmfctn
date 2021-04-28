import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInGuard } from './core/guards/sign-in.guard';
import { AuthComponent } from './modules/+auth/auth.component';
import { SignInComponent } from './modules/+auth/components/sign-in/sign-in.component';
import { LayoutComponent } from './modules/layout/layout.component';

const routes: Routes = [
  {
    path: 'auth',

    loadChildren: () =>
      import('./modules/+auth/auth.module').then(module => module.AuthModule),
  },
  {
      path: 'home',
      component: LayoutComponent,
      children: [
        {
        path : 'dashboard',
        loadChildren: () =>
            import('./modules/+dashboard/dashboard.module').then(module => module.DashboardModule),
        },
        {
            path : 'badges',
            loadChildren: () =>
            import('./modules/+badges/badges.module').then(module => module.BadgesModule),
        },
        {
            path : '',
            redirectTo: 'dashboard',
            pathMatch: 'full'
        },
      ],
      canActivate: [SignInGuard]  
  },
  {
    path : '',
    redirectTo: 'home',
    pathMatch: 'full',
   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
