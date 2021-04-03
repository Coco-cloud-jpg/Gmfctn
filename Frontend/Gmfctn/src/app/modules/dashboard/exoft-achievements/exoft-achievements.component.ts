import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import '../../../models/achievement';
import '../../../models/user';
import { SaythankModalComponent } from '../../modal-windows/saythank-modal/saythank-modal.component';
@Component({
  selector: 'app-exoft-achievements',
  templateUrl: './exoft-achievements.component.html',
  styleUrls: ['./exoft-achievements.component.scss', '../../../app.component.scss']
})
export class ExoftAchievementsComponent implements OnInit {
  users: User[] = [{
                    name: 'Pepe',
                    surname: 'Jodgi',
                    total: 10,
                    icon: '../../../../assets/phoenix.png'
                  }, {
                    name: 'Petro',
                    surname: 'Poroshenko',
                    total: 100,
                    icon: '../../../../assets/5.jpg'
                  }, {
                    name: 'Shrek',
                    surname: 'Bolothnyi',
                    total: 120,
                    icon: ''
                  }, {
                    name: 'Mr.',
                    surname: 'Heisenberg',
                    total: 15,
                    icon: ''
                  }, {
                    name: 'Ihor',
                    surname: 'Da',
                    total: 190,
                    icon: ''
                  }];
  achList: Achievement[] = [{
                            icon: '../../../../assets/phoenix.png',
                            name: 'Exoft turbo power',
                            description: '',
                            xp: 5,
                            time: new Date('December 17, 2005 03:24:00')
                          }, {
                            icon: '../../../../assets/phoenix.png',
                            name: 'Exoft skylark power',
                            description: '',
                            xp: 5,
                            time: new Date()
                          }, {
                            icon: '../../../../assets/phoenix.png',
                            name: 'Exoft corporate power',
                            description: '',
                            xp: 5,
                            time: new Date('March 27, 2021 22:24:00')
                          }];
  dictionary: { user: User, achievement: Achievement }[] = [];

  constructor(public dialog: MatDialog) {

  }
  ngOnInit(): void {
    this.fillData();
  }

  private fillData(): void {

    for ( let i = 0; i < 25; ++i ) {

      this.dictionary.push({ user: this.users[this.getRandomInt(this.users.length)],
        achievement: this.achList[this.getRandomInt(this.achList.length)]});

    }

    this.dictionary.sort(( a, b) => b.achievement.time.getTime() - a.achievement.time.getTime());
  }
  private getRandomInt( max: number): number {

    return Math.floor(Math.random() * max);

  }

  public openModal(user: User): void{

    const dialogConfig = this.dialog.open(SaythankModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
      data: user
    });

  }
}
