import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'passedTimePipe'
})
export class PassedTimePipePipe implements PipeTransform {
  monts: number[] = [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

  transform(value: Date): string {
    const result = new Date();
    let temp: number = result.getFullYear() - value.getFullYear();
    let temp2: number;

    if ( temp === 0) {
      temp = result.getMonth() - value.getMonth();
      temp2 = result.getDate() + this.monts[value.getMonth()] - value.getDate();

      if ( temp === 0 || ( temp === 1 && temp2 < this.monts[result.getMonth()] )) {
        temp = result.getDate() - value.getDate();
        if ( temp === 0) {
          temp = result.getHours() - value.getHours();
          if ( temp === 0) {
            temp = result.getMinutes() - value.getMinutes();
            if ( temp === 0) {
              return temp + ' mins ago';
            } else {
              return temp === 1 ? temp + ' min ago' : temp + ' mins ago';
            }
          } else {
            return temp === 1 ? temp + ' hour ago' : temp + ' hours ago';
          }
        } else {
          return temp === 1 ? 'Yesterday' : temp < 0 ? temp2 + ' days ago' : temp + ' days ago';
        }
      } else {
        return temp === 1 ? temp + ' month ago' : temp + ' months ago';
      }
    } else {
      return temp === 1 ? temp + ' year ago' : temp + ' years ago';
    }
  }
}
