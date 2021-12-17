/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import { DecimalPipe } from '@angular/common';
import { isExpressionFactoryMetadata } from '@angular/compiler/src/render3/r3_factory';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../data.service';
import { IBaseResponse, IPerson } from '../models/iperson';
import { ToNumberPipe } from '../pipes/to-number.pipe';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
    providers: [DecimalPipe],
})
export class HomeComponent implements OnInit {
    public claims = '';
    public person = '';
    public personId = 1;
    public date: Date | undefined;
    public number: number | undefined;
    public mainForm!: FormGroup;

    constructor(protected dataService: DataService, private fb: FormBuilder, private decimalPipe: DecimalPipe) {}
    ngOnInit(): void {
        this.mainForm = this.fb.group({
            id: [null],
            firstName: [''],
            lastName: [''],
            date: [''],
            number: [this.decimalPipe.transform('1.01'), [Validators.pattern('^[-]?[0-9]{1,3}[,]?[0-9]{0,2}')]],
        });
    }

    hasError = (controlName: string, errorName: string) => {
        return this.mainForm.controls[controlName]?.hasError(errorName);
    };

    LogFormValue() {
        console.log(this.mainForm.getRawValue());
    }
    onSubmit() {
        const person = <IPerson>{
            id: this.mainForm.get('id')?.value,
            firstName: this.mainForm.get('firstName')?.value,
            lastName: this.mainForm.get('lastName')?.value,
            date: this.mainForm.get('date')?.value,
            number: this.ToNumber(this.mainForm.get('number')?.value as string),
        };
        this.dataService.savePerson(person).subscribe();
    }

    private ToNumber(value: string): number {
        const retNumber = Number(value.replace(',', '.'));
        return isNaN(retNumber) ? 0 : retNumber;
    }

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.person = JSON.stringify(person, null, 2);

            this.date = person.value.date;
            this.number = person.value.number;

            this.mainForm.get('id')?.setValue(person.value.id);
            this.mainForm.get('firstName')?.setValue(person.value.firstName);
            this.mainForm.get('lastName')?.setValue(person.value.lastName);
            this.mainForm.get('date')?.setValue(person.value.date);
            this.mainForm.get('number')?.setValue(this.decimalPipe.transform(person.value.number));
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
