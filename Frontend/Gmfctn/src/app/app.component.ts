import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  private title = 'Gmfctn';
  public user = {
    name: 'Aezakmi',
    surname: 'Houston',
  };
}
