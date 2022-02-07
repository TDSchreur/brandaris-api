import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { DataService } from '../data.service';

import { ClaimsRemoteComponent } from './claims-remote.component';

describe('ClaimsRemoteComponent', () => {
    let component: ClaimsRemoteComponent;
    let fixture: ComponentFixture<ClaimsRemoteComponent>;

    let dataServiceMock: DataService;

    beforeEach(async () => {
        dataServiceMock = <DataService>{};
        dataServiceMock.getClaimsRemote = jest.fn().mockReturnValue(of(undefined));

        await TestBed.configureTestingModule({
            declarations: [ClaimsRemoteComponent],
            providers: [{ provide: DataService, useValue: dataServiceMock }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ClaimsRemoteComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
