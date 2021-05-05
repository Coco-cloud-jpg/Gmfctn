import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoggedInGuard } from './core/guards/logged-in/logged-in.guard';
import { SignInGuard } from './core/guards/sign-in/sign-in.guard';
import { LayoutComponent } from './modules/layout/layout.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'auth',

    loadChildren: () =>
      import('./modules/+auth/auth.module').then(module => module.AuthModule),
    canActivate: [LoggedInGuard]
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
    path: '404',
    component: NotFoundComponent
  },
  {
    path : '',
    redirectTo: 'home',
    pathMatch: 'full',
   },
   {
     path: '**',
     redirectTo: '/404'
   }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
