import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

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

        // this.dataService.getConfigRemote().subscribe((data: { key: string; value: string }[]) => {
        //     this.config = JSON.stringify(data, null, 2);
        // });
    }
}
