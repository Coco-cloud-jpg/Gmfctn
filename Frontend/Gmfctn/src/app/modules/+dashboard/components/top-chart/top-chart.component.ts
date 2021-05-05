import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../../../../shared/components/say-thank-modal/say-thank-modal.component';
import { Graph } from 'src/app/shared/models/graph';
import { UserService } from 'src/app/core/services/users-service/user.service';
import { UserSI } from 'src/app/shared/models/user-short-info';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-top-chart',
  templateUrl: './top-chart.component.html',
  styleUrls: ['./top-chart.component.scss'],
})
export class TopChartComponent implements OnInit, OnDestroy {
  users: UserSI[] = [];
  bars: Graph[] = [
    {
      Value: 0,
      Color: 'rgb(42,122,163)',
    },
    {
      Value: 0,
      Color: 'rgb(117,66,147)',
    },
    {
      Value: 0,
      Color: 'rgb(212,142,39)',
    },
    {
      Value: 0,
      Color: 'rgb(34,237,233)',
    },
    {
      Value: 0,
      Color: 'rgb(240,214,96)',
    },
  ];

  subscription = new Subscription();

  constructor(public dialog: MatDialog, private userService: UserService) {}

  ngOnInit(): void {
    if (!this.userService.usersList$.value) {
      this.subscription.add(this.userService.getTopFiveUsers().subscribe(users => {
        this.users = users;
        this.calculateGraphsLength();
      }));
    }
    else {
      this.subscription.add(this.userService.usersList$.subscribe(users => {
        this.users = users;
        this.calculateGraphsLength();
      }));
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private calculateGraphsLength(): void {
    const maxLength = this.users[0].xp;

    for (let i = 0; i < this.bars.length; ++i) {
      this.bars[i].Value = (this.users[i].xp * 100) / maxLength;
    }
  }

  openModal(user: UserSI): void {
    this.dialog.open(SayThankModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
      data: user,
    });
  }
}
