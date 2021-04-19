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
      loadChildren: () =>
        import('./modules/layout/layout.module').then(module => module.LayoutModule),
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
