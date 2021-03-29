import { Component, OnInit, Input } from '@angular/core';
import '../../models/user';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: ''};
}
