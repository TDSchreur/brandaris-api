import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from '../material.module';

import { ApiInterceptor } from './api.interceptor';

describe('AuthInterceptor', () => {
    beforeEach(() =>
        TestBed.configureTestingModule({
            providers: [ApiInterceptor],
            imports: [RouterTestingModule, MaterialModule],
        })
    );

    it('should be created', () => {
        const interceptor: ApiInterceptor = TestBed.inject(ApiInterceptor);
        expect(interceptor).toBeTruthy();
    });
});
