import { Component, Output, EventEmitter } from '@angular/core';
import { DataService } from '../shared/services/data.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
    @Output() public sidenavToggle = new EventEmitter();

    constructor(protected dataService: DataService) {}
    public onToggleSidenav = () => {
        this.sidenavToggle.emit();
    };

    generateException() {
        // eslint-disable-next-line @typescript-eslint/no-unsafe-call
        this.dataService.generateException().subscribe((response: any) => {
            console.log(response);
        });
    }
}
