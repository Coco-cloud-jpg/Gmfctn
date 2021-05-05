import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { EventIS } from 'src/app/shared/models/event';
import { SayThankModalComponent } from '../../../../shared/components/say-thank-modal/say-thank-modal.component';
import { EventService } from '../../services/event-service/event.service';

@Component({
  selector: 'app-exoft-achievements',
  templateUrl: './exoft-achievements.component.html',
  styleUrls: ['./exoft-achievements.component.scss'],
})
export class ExoftAchievementsComponent implements OnInit, OnDestroy {
  events!: EventIS[];
  subscription = new Subscription();

  constructor(public dialog: MatDialog, private eventService: EventService) {}

  ngOnInit(): void {
    this.subscription.add(this.eventService.getAllEvents().subscribe(res => {
      this.events = res;
      if (res) {
        this.fillData();
      }
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private fillData(): void {
    this.events.sort((a, b) => b.date.getTime() - a.date.getTime());
  }

  openModal(user: object): void {
    this.dialog.open(SayThankModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
      data: user,
    });
  }
}
