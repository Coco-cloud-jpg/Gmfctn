import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  private title = 'Gmfctn';
  isSignedIn = false;
  public user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: '../assets/1.jpg', status: '',  email: 'asdqweqw@gmail.com'};
}
