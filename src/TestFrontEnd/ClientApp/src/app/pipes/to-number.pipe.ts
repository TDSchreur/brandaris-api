import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'toNumber',
})
export class ToNumberPipe implements PipeTransform {
    transform(value: string): number | string {
        const retNumber = Number(value);
        return isNaN(retNumber) ? value : retNumber;
    }
}
