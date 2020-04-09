import { Component, OnInit } from '@angular/core';
import Typed from 'typed.js';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    var typed = new Typed('#typed', {
      stringsElement: '#typed-strings',
      typeSpeed: 70,
      backSpeed: 50,
      backDelay: 1500,
      startDelay: 1500
    });
  }
  
  year = new Date().getFullYear();

}
