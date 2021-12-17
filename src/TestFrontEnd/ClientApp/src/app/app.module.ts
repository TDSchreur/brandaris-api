import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { MessageComponent } from './message/message.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { ApiInterceptor } from './interceptor/api.interceptor';
import { ToNumberPipe } from './pipes/to-number.pipe';

@NgModule({
    declarations: [AppComponent, MessageComponent, ForbiddenComponent, HomeComponent, ToNumberPipe],
    imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, HttpClientModule, FormsModule, ReactiveFormsModule, MaterialModule],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true }],
    bootstrap: [AppComponent],
})
export class AppModule {}
