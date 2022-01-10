import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IBaseResponse, IPerson } from './models/iperson';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
    providedIn: 'root',
})
export class DataService {
    constructor(protected http: HttpClient) {}

    getPerson(personId: number) {
        return this.http
            .get<IBaseResponse<IPerson>>(`api/person/${personId}`)
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    savePerson(person: IPerson) {
        return this.http.put<IPerson>(`api/person`, person, httpOptions);
    }

    getClaims() {
        return this.http
            .get<{ name: string; value: string }[]>('account/claims')
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    getClaimsRemote() {
        return this.http
            .get<{ name: string; value: string }[]>('account/claims_remote')
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    getConfigRemote() {
        return this.http
            .get<{ name: string; value: string }[]>('api/config')
            .pipe(catchError((error: HttpErrorResponse) => this.handleError(error)));
    }

    private handleError(err: HttpErrorResponse) {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            errorMessage = `An error occurred: ${err.error.message}`;
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.log(errorMessage);

        return throwError(errorMessage);
    }
}
