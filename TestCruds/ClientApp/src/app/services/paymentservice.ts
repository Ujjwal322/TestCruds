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
  getInvoiceList() {
    return this.http.get(this.myAppUrl + 'api/Invoice/GetInvoice')
      .map(res => res.json())
      .catch(this.errorHandler);
  }
  getPaymentList() {
    return this.http.get(this.myAppUrl + 'api/Payment/GetPayment')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getPaymentById(id: number) {
    return this.http.get(this.myAppUrl + "api/Payment/DetailsPayment/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  savePayment(payment) {
    return this.http.post(this.myAppUrl + 'api/Payment/Create', payment)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updatePayment(payment) {
    return this.http.put(this.myAppUrl + 'api/Payment/Edit', payment)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  deletePayment(id) {
    return this.http.delete(this.myAppUrl + "api/Payment/DeletePay/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }


  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }

}
