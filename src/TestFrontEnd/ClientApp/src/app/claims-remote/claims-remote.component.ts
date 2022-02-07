import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
    selector: 'app-claims-remote',
    templateUrl: './claims-remote.component.html',
    styleUrls: ['./claims-remote.component.scss'],
})
export class ClaimsRemoteComponent implements OnInit {
    public claims = '';

    constructor(protected dataService: DataService) {}

    ngOnInit(): void {
        this.dataService.getClaimsRemote().subscribe((data: { name: string; value: string }[]) => {
            this.claims = JSON.stringify(data, null, 2);
        });
    }
}
