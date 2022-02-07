/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IPerson } from '../../shared/models/iperson';
import { DataService } from '../../shared/services/data.service';

@Component({
    selector: 'app-person-add',
    templateUrl: './person-add.component.html',
    styleUrls: ['./person-add.component.scss'],
})
export class PersonAddComponent implements OnInit {
    public mainForm!: FormGroup;

    constructor(protected dataService: DataService, private fb: FormBuilder) {}

    ngOnInit(): void {
        this.mainForm = this.fb.group({
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
        });
    }

    onSubmit() {
        const person = <IPerson>{
            firstName: this.mainForm.get('firstName')?.value,
            lastName: this.mainForm.get('lastName')?.value,
        };
        this.dataService.addPerson(person).subscribe();

        this.mainForm.reset();
    }
}
