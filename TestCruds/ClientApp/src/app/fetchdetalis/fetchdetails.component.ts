import { Component, ViewChild, ElementRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import * as XLSX from 'xlsx';
import { CustomerService } from '../services/customerservice';

@Component({
  selector: 'fetchdetails',
  templateUrl: './fetchdetails.component.html',
  styleUrls: ['./fetchdetails.component.html']
})

export class FetchDetailsComPonent {
   


  @ViewChild('TABLE') TABLE: ElementRef;

  title = 'Excel';
  ExportTOExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'InvoiceDetails');
    XLSX.writeFile(wb, 'CustomersInvoicesPayment.xlsx');
  }
  dateFormat: string = "M/D/YYYY HH:mm:ss";
  public invList: InvoicesData[];
  month: string;
  customerName: string;
  noOfInvoices: number;
  sales: number;
  paymentCollection: number;

  public selectedInvoiceData: InvoicesData[];

 public customers: CustomerData[];
 public selectedCustomer: CustomerData[];

  constructor(public http: Http, private _router: Router, private customerservice: CustomerService) {
    
    this.getCustomer();
    this.getInvoices();
  }

  //ngOnInit() {
    
  //}
    

  getInvoices() {
    this.customerservice.getInvoiceList().subscribe(
      (data: InvoicesData[]) => {
        var res = data;
        this.invList = data;
        this.selectedInvoiceData = this.invList;
        console.log(this.selectedInvoiceData);
      }
    );
  }
  getCustomer() {
    this.customerservice.getCustomerList().subscribe(
      (data: CustomerData[]) => {
        var res = data;
        this.customers = data;
        //this.selectedCustomer = this.customers;
        //console.log(this.selectedCustomer);
      });
  }

  //handleCustomerChange(name: string) {
  //  let obj = this.customers.filter(x => x.customerName == name);
  //  this.customers = obj;
  //  return this.customers;
  //}

  //filterForeCasts(filterVal:any) {
  //  if (filterVal == "0") 
  //    this.customers = this.selectedCustomer;
  //    else
  //    this.customers = this.selectedCustomer.filter((item) => item.customerName == filterVal);

  //  console.log(filterVal);
  //}

  filterForeCasts(filterVal:any) {
    if (filterVal == "0") 
      this.invList = this.selectedInvoiceData;
      else
      this.invList = this.selectedInvoiceData.filter((item) => item.customerName == filterVal);

    console.log(filterVal);
  }

}

interface InvoicesData {
  month:string;
  customerName: string;
  noOfInvoices: number;
  sales: number;
  paymentCollection: number;
}

interface CustomerData {
  customerId: number;
  customerNo: string;
  customerName: string;
}
