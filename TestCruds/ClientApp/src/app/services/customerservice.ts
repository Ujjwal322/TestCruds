import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()

export class CustomerService {
  myAppUrl: string = "";

  constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getCustomerList() {
    return this.http.get(this.myAppUrl + 'api/Customer/Index')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getInvoiceList() {
    return this.http.get(this.myAppUrl + 'api/Customer/GetCustomers')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getCustomerReport() {
    return this.http.get(this.myAppUrl + 'api/Customer/GetInvoicesDetail')
      .map(res => res.json())
      .catch(this.errorHandler);
  }


  getCustomerById(id: number) {
    return this.http.get(this.myAppUrl + "api/Customer/Details/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }


  saveCustomer(customer) {
    return this.http.post(this.myAppUrl + 'api/Customer/Create', customer)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }


  updateCustomer(customer) {
    return this.http.put(this.myAppUrl + 'api/Customer/Edit', customer)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  deleteCustomer(id) {
    return this.http.delete(this.myAppUrl + "api/Customer/Delete/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }



  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
