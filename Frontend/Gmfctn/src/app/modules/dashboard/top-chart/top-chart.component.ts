import { Component, OnInit } from '@angular/core';
import '../../../models/user';
import '../../../models/graph';
@Component({
  selector: 'app-top-chart',
  templateUrl: './top-chart.component.html',
  styleUrls: ['./top-chart.component.scss']
})
export class TopChartComponent implements OnInit {
  users: User[] = [{
                  name: 'Pepe',
                  surname: 'Jodgi',
                  total: 10,
                  icon: '../../../../assets/phoenix.png'
                }, {
                  name: 'Petro',
                  surname: 'Poroshenko',
                  total: 100,
                  icon: '../../../../assets/phoenix.png'
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
  bars: Graph[] = [{
                    Value: 0,
                    Color: 'rgb(42,122,163)',
                  }, {
                    Value: 0,
                    Color: 'rgb(117,66,147)',
                  }, {
                    Value: 0,
                    Color: 'rgb(212,142,39)',
                  }, {
                    Value: 0,
                    Color: 'rgb(34,237,233)',
                  }, {
                    Value: 0,
                    Color: 'rgb(240,214,96)',
                  } ];

  ngOnInit(): void {
    this.users.sort(( a, b) => b.total - a.total);
    const maxLength = this.users[0].total;
    for ( let i = 0; i < this.bars.length; ++i){
      this.bars[i].Value = this.users[i].total * 100 / maxLength;
    }
  }

}
