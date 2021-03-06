import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from '../../shared/services/data.service';
import { MaterialModule } from '../../shared/material.module';
import { PersonComponent } from '../person.component';

describe('PersonComponent', () => {
    let component: PersonComponent;
    let fixture: ComponentFixture<PersonComponent>;

    let dataServiceMock: DataService;

    beforeEach(async () => {
        dataServiceMock = <DataService>{};

        await TestBed.configureTestingModule({
            imports: [BrowserAnimationsModule, FormsModule, ReactiveFormsModule, MaterialModule],
            declarations: [PersonComponent],
            providers: [{ provide: DataService, useValue: dataServiceMock }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(PersonComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
