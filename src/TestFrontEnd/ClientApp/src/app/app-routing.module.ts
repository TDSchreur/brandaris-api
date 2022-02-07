import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClaimsRemoteComponent } from './claims-remote/claims-remote.component';
import { ClaimsComponent } from './claims/claims.component';
import { ConfigRemoteComponent } from './config-remote/config-remote.component';

const routes: Routes = [
    { path: 'claims', component: ClaimsComponent },
    { path: 'claims-remote', component: ClaimsRemoteComponent },
    { path: 'config-remote', component: ConfigRemoteComponent },
    { path: '', redirectTo: 'person', pathMatch: 'full' },
    { path: '**', redirectTo: 'person', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
