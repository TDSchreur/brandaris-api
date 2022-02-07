import { NgModule } from '@angular/core';
import { PersonComponent } from './person.component';
import { PersonListComponent } from './person-list/person-list.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { PersonAddComponent } from './person-add/person-add.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [{ path: 'person', component: PersonComponent }];
@NgModule({
    declarations: [PersonComponent, PersonListComponent, PersonEditComponent, PersonAddComponent],
    imports: [SharedModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PersonModule {}
