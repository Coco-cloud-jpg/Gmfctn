import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../say-thank-modal/say-thank-modal.component';
import { ThankService } from 'src/app/core/services/thank-service/thank.service';
import { Thank } from '../../models/thank';
import { UserService } from 'src/app/core/services/users-service/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.scss']
})
export class ThankYouComponent implements OnInit, OnDestroy {
  thank!: Thank;
  noContent = false;
  subscription = new Subscription();

  constructor(public dialog: MatDialog, private thankService: ThankService, private userService: UserService) {}

  ngOnInit(): void {
    this.subscription.add(this.thankService.getLastThank().subscribe(thank => {
      this.thank = thank;
      this.noContent = !thank;
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  openModal(): void {
    this.dialog.open(SayThankModalComponent, {
      width: '50%',
      panelClass: 'custom-modalbox',
      data: this.thank.fromUser
    });
  }
}
