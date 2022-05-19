import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPerson } from '../models/iperson';
import { IBaseResponse } from '../models/IBaseResponse';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
    providedIn: 'root',
})
export class DataService {
    constructor(protected http: HttpClient) {}

    getPerson(personId: number) {
        return this.http.get<IBaseResponse<IPerson>>(`api/person/${personId}`);
    }

    getPersons(approved: boolean) {
        return this.http.get<IBaseResponse<IPerson[]>>(`api/person?approved=${approved.toString()}`);
    }

    savePerson(person: IPerson) {
        return this.http.put<IPerson>(`api/person`, person, httpOptions);
    }

    addPerson(person: IPerson) {
        return this.http.post<IPerson>(`api/person`, person, httpOptions);
    }

    approvePerson(personId: number) {
        return this.http.put(`api/person/${personId}`, null, httpOptions);
    }

    getClaims() {
        return this.http.get<{ name: string; value: string }[]>('account/claims');
    }

    getClaimsRemote() {
        return this.http.get<{ name: string; value: string }[]>('account/claims_remote');
    }

    getConfigRemote() {
        return this.http.get<{ name: string; value: string }[]>('api/config');
    }
}
