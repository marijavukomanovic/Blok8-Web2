import { Component, OnInit } from '@angular/core';
import {LinijaService} from 'src/app/servisi/linija.service';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css']
})
export class LinijeComponent implements OnInit {

  info : string;

  constructor(private lineSService : LinijaService) { }

  ngOnInit() {
  }

  ShowLines(routeType : number){
    this.lineSService.getLines(routeType).subscribe(data => {
      console.log(data);
      this.info = data;
    });
  }

}
