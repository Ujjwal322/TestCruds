import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


@Injectable()

export class PaymentService {
  myAppUrl: string = "";

  constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getPaymentList() {
    return this.http.get(this.myAppUrl + 'api/Payment/GetPayment')
      .map(res => res.json())
      .catch(this.errorHandler);
  }


  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }

}
