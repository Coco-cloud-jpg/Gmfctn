import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
        {
        path : 'dashboard',
        loadChildren: () =>
            import('../+dashboard/dashboard.module').then(module => module.DashboardModule),
        },
        {
            path : 'badges',
            loadChildren: () =>
            import('../+badges/badges.module').then(module => module.BadgesModule),
        },
        {
            path : '',
            redirectTo: 'dashboard',
            pathMatch: 'full'
        },
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
