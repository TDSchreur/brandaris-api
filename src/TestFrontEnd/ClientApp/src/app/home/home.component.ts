import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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
            date: [''],
        });
    }

    GetPerson() {
        this.dataService.getPerson(this.personId).subscribe((person: IBaseResponse<IPerson>) => {
            this.person = JSON.stringify(person, null, 2);
            this.date = person.value.date;

            this.mainForm.get('date')?.setValue(person.value.date);
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
