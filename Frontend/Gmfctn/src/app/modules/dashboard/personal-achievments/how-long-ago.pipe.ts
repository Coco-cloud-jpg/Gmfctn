import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'howLongAgo',
})
export class HowLongAgoPipe implements PipeTransform {
  transform(value: Date, ...args: unknown[]): string {
    const result = new Date();
    let temp: number = result.getFullYear() - value.getFullYear();
    if ( temp === 0) {
      temp = result.getMonth() - value.getMonth();
      if ( temp === 0) {
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
          return temp === 1 ? temp + ' day ago' : temp + ' days ago';
        }
      } else {
        return temp === 1 ? temp + ' month ago' : temp + ' months ago';
      }
    } else {
      return temp === 1 ? temp + ' year ago' : temp + ' years ago';
    }
  }
}
