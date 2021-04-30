import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from './modules/layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { AuthModule } from './modules/+auth/auth.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SignInGuard } from './core/guards/sign-in/sign-in.guard';
import { AuthHttpInterceptor } from './core/interceptors/http/auth-http.interseptor';
import { LoggedInGuard } from './core/guards/logged-in/logged-in.guard';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutModule,
    SharedModule,
    AuthModule,
    HttpClientModule
  ],
  providers: [
    LoggedInGuard,
    SignInGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
