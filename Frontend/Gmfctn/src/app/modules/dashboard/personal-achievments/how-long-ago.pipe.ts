import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'howLongAgo'
})
export class HowLongAgoPipe implements PipeTransform {

  transform(value: Date, ...args: unknown[]): string {
    var result = new Date();
    let temp: number;
    if((temp = result.getFullYear() - value.getFullYear())===0){
      if((temp = result.getMonth() - value.getMonth())===0){
        if((temp = result.getDate() - value.getDate())===0){
          if((temp = result.getHours() - value.getHours())===0){
            if((temp = result.getMinutes() - value.getMinutes())===0){
              return temp +' mins ago';
            }
            else{
              return temp===1? temp + ' min ago': temp + ' mins ago';
            }
          }
          else{
            return temp===1? temp + ' hour ago': temp + ' hours ago';
          }
        }
        else{
          return temp===1? temp + ' day ago': temp + ' days ago';
        }
      }
      else{
        return temp===1? temp + ' month ago': temp + ' months ago';
      }
    }
    else{
    return temp===1? temp + ' year ago': temp + ' years ago';
    }
  }

}
