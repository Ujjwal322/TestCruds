import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


@Injectable()

export class InvoiceService {
  myAppUrl: string = "";

  constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getCustomerList() {
    return this.http.get(this.myAppUrl + 'api/Customer/GetCustomerList')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getInvoiceList() {
    return this.http.get(this.myAppUrl + 'api/Invoice/GetInvoice')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getInvoiceById(id: number) {
    return this.http.get(this.myAppUrl + "api/Invoice/DetailsInvoice/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  saveInvoice(invoice) {
    return this.http.post(this.myAppUrl + 'api/Invoice/Create',invoice)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updateInvoice(invoice) {
    return this.http.put(this.myAppUrl + 'api/Invoice/Edit', invoice)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  deleteInvoice(id) {
    return this.http.delete(this.myAppUrl + "api/Invoice/DeleteInv/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }



  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }

}
