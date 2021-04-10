import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MaterialModule} from './shared/materials.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToolbarComponent } from './modules/toolbar/toolbar.component';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from './modules/sidebar/sidebar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SignInWindowComponent } from './modules/sign-in-window/sign-in-window.component';
@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    SidebarComponent,
    SignInWindowComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
