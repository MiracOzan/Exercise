import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Value } from './Models/value';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  constructor(private httpclient: HttpClient) { }

  Value:Value[] = []

  ngOnInit() {
    this.GetValues().subscribe(data => {
      this.Value = data
    });
  }

  GetValues() {
    return this.httpclient.get('/api/HomeController')
  }
  title = 'frontend';
}
