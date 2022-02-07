/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DataService } from '../data.service';
import { IBaseResponse, IPerson } from '../models/iperson';

@Component({
    selector: 'app-person',
    templateUrl: './person.component.html',
    styleUrls: ['./person.component.scss'],
})
export class PersonComponent implements OnInit {
    public person = '';
    public personId = 1;
    public mainForm!: FormGroup;

    constructor(protected dataService: DataService, private fb: FormBuilder) {}

    ngOnInit(): void {
        this.mainForm = this.fb.group({
            id: [null],
            firstName: [''],
            lastName: [''],
            date: [''],
        });
    }

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.mainForm.get('id')?.setValue(person.value.id);
            this.mainForm.get('firstName')?.setValue(person.value.firstName);
            this.mainForm.get('lastName')?.setValue(person.value.lastName);
            this.mainForm.get('date')?.setValue(person.value.date);
            this.mainForm.get('number')?.setValue(person.value.number);
        });
    }

    onSubmit() {
        const person = <IPerson>{
            id: this.mainForm.get('id')?.value,
            firstName: this.mainForm.get('firstName')?.value,
            lastName: this.mainForm.get('lastName')?.value,
            date: this.mainForm.get('date')?.value,
        };
        this.dataService.savePerson(person).subscribe();
    }
}
