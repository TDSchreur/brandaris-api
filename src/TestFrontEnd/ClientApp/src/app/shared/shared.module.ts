import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ApiInterceptor } from './interceptor/api.interceptor';
import { MessageComponent } from './message/message.component';
import { MaterialModule } from './material.module';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: 'forbidden', component: ForbiddenComponent }];
@NgModule({
    declarations: [MessageComponent, ForbiddenComponent],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true }],
    imports: [
        CommonModule,
        MaterialModule,
        HttpClientModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),
    ],
    exports: [MaterialModule, BrowserAnimationsModule, FormsModule, ReactiveFormsModule, RouterModule],
})
export class SharedModule {}
