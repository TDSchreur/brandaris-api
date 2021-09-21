import { Component } from '@angular/core';
import { DataService } from './data.service';
import { IBaseResponse, IPerson } from './models/iperson';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    public claims = '';
    public person = '';
    public personId = 1;

    constructor(protected dataService: DataService) {}

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.person = JSON.stringify(person, null, 2);
        });
    }

    GetClaims() {
        this.dataService.getClaims().subscribe((data: any) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }

    GetClaimsRemote() {
        this.dataService.getClaimsRemote().subscribe((data: any) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }
}
