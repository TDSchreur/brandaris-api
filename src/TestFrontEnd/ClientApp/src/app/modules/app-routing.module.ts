import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClaimsRemoteComponent } from '../claims-remote/claims-remote.component';
import { ClaimsComponent } from '../claims/claims.component';
import { ConfigRemoteComponent } from '../config-remote/config-remote.component';
import { ForbiddenComponent } from '../forbidden/forbidden.component';
import { HomeComponent } from '../home/home.component';
import { PersonComponent } from '../person/person.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'person', component: PersonComponent },
    { path: 'claims', component: ClaimsComponent },
    { path: 'claims-remote', component: ClaimsRemoteComponent },
    { path: 'config-remote', component: ConfigRemoteComponent },
    { path: 'forbidden', component: ForbiddenComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
