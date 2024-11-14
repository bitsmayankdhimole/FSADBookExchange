import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CardModule } from 'primeng/card';

// Layout Components
import { PublicLayoutComponent } from './layouts/public-layout/public-layout.component';
import { PrivateLayoutComponent } from './layouts/private-layout/private-layout.component';

// Auth Components
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

// Book Components
import { BookListingComponent } from './books/book-listing/book-listing.component';
import { AddBookModalComponent } from './books/add-book-modal/add-book-modal.component';
import { BookSearchComponent } from './books/book-search/book-search.component';
import { BookExchangeComponent } from './books/book-exchange/book-exchange.component';

// Dashboard Component
import { DashboardComponent } from './dashboard/dashboard.component';

// Services
import { AuthInterceptorService } from './interceptors/auth-interceptor.service';
import { PasswordResetTokenComponent } from './auth/password-reset-token/password-reset-token.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';

@NgModule({
  declarations: [
    AppComponent,
    PublicLayoutComponent,
    PrivateLayoutComponent,
    LoginComponent,
    RegisterComponent,
    BookListingComponent,
    AddBookModalComponent,
    BookSearchComponent,
    BookExchangeComponent,
    DashboardComponent,
    PasswordResetTokenComponent,
    ResetPasswordComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    ButtonModule,
    InputTextModule,
    ToastModule,
    TableModule,
    PaginatorModule,
    DialogModule,
    DropdownModule,
    InputTextareaModule,
    CardModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }