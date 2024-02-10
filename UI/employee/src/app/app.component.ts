import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    console.log(this.router.url);
  }

  /**
   *
   */
  constructor(private router: Router) {
  console.log(this.router.url);
    
  }
  title = 'employee';
}
