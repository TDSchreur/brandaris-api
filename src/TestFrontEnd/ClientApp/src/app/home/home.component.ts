/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { DataService } from '../data.service';
import { IBaseResponse, IPerson } from '../models/iperson';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
    public claims = '';
    public person = '';
    public personId = 1;
    public date: Date | undefined;
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

    hasError = (controlName: string, errorName: string) => {
        return this.mainForm.controls[controlName]?.hasError(errorName);
    };

    public executeSelectedChange = (event: MatTabChangeEvent) => {
        console.log(event);
    };

    onSubmit() {
        const person = <IPerson>{
            id: this.mainForm.get('id')?.value,
            firstName: this.mainForm.get('firstName')?.value,
            lastName: this.mainForm.get('lastName')?.value,
            date: this.mainForm.get('date')?.value,
        };
        this.dataService.savePerson(person).subscribe();
    }

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.person = JSON.stringify(person, null, 2);
            this.date = person.value.date;

            this.mainForm.get('id')?.setValue(person.value.id);
            this.mainForm.get('firstName')?.setValue(person.value.firstName);
            this.mainForm.get('lastName')?.setValue(person.value.lastName);
            this.mainForm.get('date')?.setValue(person.value.date);
            this.mainForm.get('number')?.setValue(person.value.number);
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

    GetConfigRemote() {
        this.dataService.getConfigRemote().subscribe((data: { name: string; value: string }[]) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }
}
