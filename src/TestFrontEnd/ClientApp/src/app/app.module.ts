import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ClaimsComponent } from './claims/claims.component';
import { ClaimsRemoteComponent } from './claims-remote/claims-remote.component';
import { ConfigRemoteComponent } from './config-remote/config-remote.component';

import { PersonModule } from './person-module/person.module';
import { SharedModule } from './shared/shared.module';
import { HeaderComponent } from './header/header.component';

@NgModule({
    declarations: [AppComponent, HeaderComponent, ClaimsComponent, ClaimsRemoteComponent, ConfigRemoteComponent],
    imports: [PersonModule, BrowserModule, AppRoutingModule, SharedModule],

    bootstrap: [AppComponent],
})
export class AppModule {}
