import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/services/data.service';

@Component({
    selector: 'app-config-remote',
    templateUrl: './config-remote.component.html',
    styleUrls: ['./config-remote.component.scss'],
})
export class ConfigRemoteComponent implements OnInit {
    public config = '';
    constructor(protected dataService: DataService) {}

    ngOnInit(): void {
        this.dataService.getConfigRemote().subscribe((data: { name: string; value: string }[]) => {
            this.config = JSON.stringify(data, null, 2);
        });
    }
}
