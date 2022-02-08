import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/services/data.service';

@Component({
    selector: 'app-claims',
    templateUrl: './claims.component.html',
    styleUrls: ['./claims.component.scss'],
})
export class ClaimsComponent implements OnInit {
    public claims = '';

    constructor(protected dataService: DataService) {}

    ngOnInit(): void {
        this.dataService.getClaims().subscribe((data: { name: string; value: string }[]) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }
}
