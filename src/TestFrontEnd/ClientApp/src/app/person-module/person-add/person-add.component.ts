import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IPerson } from '../../shared/models/iperson';
import { DataService } from '../../shared/services/data.service';

@Component({
    selector: 'app-person-add',
    templateUrl: './person-add.component.html',
    styleUrls: ['./person-add.component.scss'],
})
export class PersonAddComponent {
    public mainForm: FormGroup<PersonForm>;

    constructor(protected dataService: DataService) {
        this.mainForm = new FormGroup<PersonForm>({
            firstName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
            lastName: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        });
    }

    onSubmit() {
        const person = this.mainForm.getRawValue() as IPerson;

        this.dataService.addPerson(person).subscribe(() => {
            this.mainForm.reset();
        });
    }
}
interface PersonForm {
    firstName: FormControl<string>;
    lastName: FormControl<string>;
}
