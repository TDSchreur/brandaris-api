import { Component, Inject } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
    selector: 'app-message',
    templateUrl: './message.component.html',
    styleUrls: ['./message.component.scss'],
})
export class MessageComponent {
    constructor(public snackBarRef: MatSnackBarRef<MessageComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) {}
}
