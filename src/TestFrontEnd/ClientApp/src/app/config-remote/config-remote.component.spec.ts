import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { DataService } from '../data.service';

import { ConfigRemoteComponent } from './config-remote.component';

describe('ConfigRemoteComponent', () => {
    let component: ConfigRemoteComponent;
    let fixture: ComponentFixture<ConfigRemoteComponent>;

    let dataServiceMock: DataService;

    beforeEach(async () => {
        dataServiceMock = <DataService>{};
        dataServiceMock.getConfigRemote = jest.fn().mockReturnValue(of(undefined));

        await TestBed.configureTestingModule({
            declarations: [ConfigRemoteComponent],
            providers: [{ provide: DataService, useValue: dataServiceMock }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ConfigRemoteComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
