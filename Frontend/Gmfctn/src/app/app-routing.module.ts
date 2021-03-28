import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
      path: 'dashboard',
      loadChildren: () =>
          import('./modules/dashboard/dashboard.module').then(module => module.DashboardModule)
  }
  /*{
      path: 'reactive-form',
      loadChildren: () =>
          import('./modules/+reactive-forms/reactive-form.module').then(module => module.ReactiveFormModule)
  }*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
