import { NgModule } from '@angular/core';

import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MATERIAL_SANITY_CHECKS } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';

@NgModule({
    imports: [
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatTableModule,
        MatInputModule,
        MatSelectModule,
        MatSnackBarModule,
        MatProgressSpinnerModule,
        MatGridListModule,
        MatChipsModule,
        MatDialogModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatNativeDateModule,
    ],
    exports: [
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatTableModule,
        MatInputModule,
        MatSelectModule,
        MatSnackBarModule,
        MatProgressSpinnerModule,
        MatGridListModule,
        MatChipsModule,
        MatDialogModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatNativeDateModule,
    ],
    providers: [
        {
            provide: MATERIAL_SANITY_CHECKS,
            useValue: false,
        },
        { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'always' } },
    ],
})
export class MaterialModule {}
