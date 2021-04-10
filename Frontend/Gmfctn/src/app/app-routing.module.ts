import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BadgesComponent } from './modules/badges/badges.component';

const routes: Routes = [
  {
      path: 'dashboard',
      loadChildren: () =>
          import('./modules/dashboard/dashboard.module').then(module => module.DashboardModule)
  },
  {
    path: 'badges',
    loadChildren: () =>
        import('./modules/badges/badges.module').then(module => module.BadgesModule)
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
