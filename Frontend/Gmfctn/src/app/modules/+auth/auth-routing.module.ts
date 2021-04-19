import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthComponent } from './auth.component';
import { SignInComponent } from './components/sign-in/sign-in.component';


const routes: Routes = [
    {
        path: '',
        component: AuthComponent,
        children: [
        {
            path: 'sign-in',
            component: SignInComponent,
            data : {errorOccured : ''}
        },
        {
            path: '',
            redirectTo: 'sign-in',
            pathMatch: 'full'
        }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
