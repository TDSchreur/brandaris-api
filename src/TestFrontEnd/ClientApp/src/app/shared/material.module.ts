import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';

@NgModule({
    declarations: [],
    imports: [
        MatTabsModule,
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatListModule,
        MatMenuModule,
        MatInputModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatDividerModule,
        MatSnackBarModule,
        MatSlideToggleModule,
        MatCardModule,
    ],
    exports: [
        MatTabsModule,
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatListModule,
        MatMenuModule,
        MatInputModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatDividerModule,
        MatSnackBarModule,
        MatSlideToggleModule,
        MatCardModule,
    ],
})
export class MaterialModule {}
