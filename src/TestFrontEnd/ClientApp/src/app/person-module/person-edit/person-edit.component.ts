/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { DataService } from '../../shared/services/data.service';
import { IPerson } from '../../shared/models/iperson';
import { IBaseResponse } from '../../shared/models/IBaseResponse';

@Component({
    selector: 'app-person-edit',
    templateUrl: './person-edit.component.html',
    styleUrls: ['./person-edit.component.scss'],
})
export class PersonEditComponent implements OnInit {
    public person = '';
    public personId = 1;
    public mainForm!: UntypedFormGroup;

    constructor(protected dataService: DataService, private fb: UntypedFormBuilder) {}

    ngOnInit(): void {
        this.mainForm = this.fb.group({
            id: [null],
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
        });
    }

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.mainForm.get('id')?.setValue(person.value.id);
            this.mainForm.get('firstName')?.setValue(person.value.firstName);
            this.mainForm.get('lastName')?.setValue(person.value.lastName);
        });
    }

    onSubmit() {
        const person = <IPerson>{
            id: this.mainForm.get('id')?.value,
            firstName: this.mainForm.get('firstName')?.value,
            lastName: this.mainForm.get('lastName')?.value,
        };
        this.dataService.savePerson(person).subscribe(() => {
            this.mainForm.reset();
        });
    }
}
