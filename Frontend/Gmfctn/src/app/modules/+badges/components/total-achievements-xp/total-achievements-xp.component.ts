import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-total-achievements-xp',
  templateUrl: './total-achievements-xp.component.html',
  styleUrls: ['./total-achievements-xp.component.scss']
})
export class TotalAchievementsXpComponent {
  @Input() userXp = 0;
  @Input() userBadges = 0;
}
