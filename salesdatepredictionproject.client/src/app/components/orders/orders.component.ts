import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  public orders: any[] | null = null;
  private customerId: string;

  constructor(private route: ActivatedRoute, private http: HttpClient) {
    this.customerId = this.route.snapshot.paramMap.get('id')!;
  }

  ngOnInit() {
    this.http.get<any[]>(`https://localhost:44302/api/orders/${this.customerId}`).subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }
}
