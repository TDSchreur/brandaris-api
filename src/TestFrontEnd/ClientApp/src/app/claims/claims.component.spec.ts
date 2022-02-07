import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { DataService } from '../data.service';

import { ClaimsComponent } from './claims.component';

describe('ClaimsComponent', () => {
    let component: ClaimsComponent;
    let fixture: ComponentFixture<ClaimsComponent>;

    let dataServiceMock: DataService;

    beforeEach(async () => {
        dataServiceMock = <DataService>{};
        dataServiceMock.getClaims = jest.fn().mockReturnValue(of(undefined));

        await TestBed.configureTestingModule({
            declarations: [ClaimsComponent],
            providers: [{ provide: DataService, useValue: dataServiceMock }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ClaimsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
