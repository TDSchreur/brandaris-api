import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBaseResponse, IPerson } from './models/iperson';

@Injectable({
    providedIn: 'root',
})
export class DataService {
    constructor(protected http: HttpClient) {}

    getPerson(personId: number): Observable<IBaseResponse<IPerson>> {
        return this.http.get<IBaseResponse<IPerson>>(`api/person/${personId}`);
    }

    getClaims(): Observable<any> {
        return this.http.get<any>('api/person/claims');
    }
}
