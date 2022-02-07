import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { MessageComponent } from '../message/message.component';

@Injectable({
    providedIn: 'root',
})
export class NotificationService {
    constructor(public snackBar: MatSnackBar) {}

    config: MatSnackBarConfig = {
        duration: 6000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'style-success',
    };

    success(msg: string) {
        this.config.panelClass = ['notification', 'success'];
        this.config.data = msg;
        this.snackBar.openFromComponent(MessageComponent, this.config);
    }

    warn(msg: string) {
        this.config.panelClass = ['notification', 'warn'];
        this.config.data = msg;
        this.snackBar.openFromComponent(MessageComponent, this.config);
    }
}
