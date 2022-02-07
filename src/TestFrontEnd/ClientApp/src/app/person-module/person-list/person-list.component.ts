import { Component, OnInit } from '@angular/core';
import { IBaseResponse } from '../../shared/models/IBaseResponse';
import { IPerson } from '../../shared/models/iperson';
import { DataService } from '../../shared/services/data.service';

@Component({
    selector: 'app-person-list',
    templateUrl: './person-list.component.html',
    styleUrls: ['./person-list.component.scss'],
})
export class PersonListComponent implements OnInit {
    public persons: IPerson[] = [];

    approved = true;

    constructor(protected dataService: DataService) {}

    ngOnInit(): void {
        this.reloadPersons();
    }

    reloadPersons(): void {
        this.dataService.getPersons(this.approved).subscribe((data: IBaseResponse<IPerson[]>) => {
            this.persons = data.value;
        });
    }

    approvePerson(id: number): void {
        this.dataService.approvePerson(id).subscribe();
    }
}
