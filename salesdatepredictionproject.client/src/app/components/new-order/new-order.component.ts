import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Router } from '@angular/router';

@Component({
  selector: 'app-new-order',
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})

export class NewOrderComponent implements OnInit {
  private customerId: string;
  orderForm!: FormGroup;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private http: HttpClient, private router: Router) {
    this.customerId = this.route.snapshot.paramMap.get('id')!;
  }

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      empId: [0, Validators.required],
      shipperId: [0, Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      orderDate: ['', Validators.required],
      requiredDate: ['', Validators.required],
      shippedDate: ['', Validators.required],
      freight: [0, Validators.required],
      shipCountry: ['', Validators.required],
      productId: [0, Validators.required],
      qty: [0, [Validators.required, Validators.min(1)]],
      unitPrice: [0, [Validators.required, Validators.min(0.01)]],
      discount: [0, [Validators.required, Validators.min(0.01)]]
    });
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const headers = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('Accept', 'application/json');

      this.http.post(`https://localhost:44302/api/Orders`, this.orderForm.value, { headers }).subscribe(result => {
        console.log('Order created', result);

        this.router.navigate(['/'])
          .then(success => console.log('Navigation success:', success))
          .catch(error => console.error('Navigation error:', error));
      }, error => console.error(error));

      console.log(this.orderForm.value);
    }
  }
}
