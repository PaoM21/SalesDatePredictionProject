import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.css'
})
export class CustomersComponent {
  public customersList: any[] | null = null;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.http.get<any[]>('https://localhost:44302/api/Customers').subscribe(result => {
      this.customersList = result;
    }, error => console.error(error));
  }
}
