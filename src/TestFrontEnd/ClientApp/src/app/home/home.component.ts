import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { IBaseResponse, IPerson } from '../models/iperson';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
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
        this.dataService.getClaims().subscribe((data: { name: string; value: string }[]) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }

    GetClaimsRemote() {
        this.dataService.getClaimsRemote().subscribe((data: { name: string; value: string }[]) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }
}
