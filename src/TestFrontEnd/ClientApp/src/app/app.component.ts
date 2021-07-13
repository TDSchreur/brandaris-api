import { Component } from '@angular/core';
import { DataService } from './data.service';
import { IBaseResponse, IPerson } from './models/iperson';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    public person: string = '';
    public personId: number = 1;

    constructor(protected dataService: DataService) {}

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.person = JSON.stringify(person, null, 2);
        });
    }
}
