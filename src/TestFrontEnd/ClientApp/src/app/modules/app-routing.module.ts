import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForbiddenComponent } from '../forbidden/forbidden.component';
import { HomeComponent } from '../home/home.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'forbidden', component: ForbiddenComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
