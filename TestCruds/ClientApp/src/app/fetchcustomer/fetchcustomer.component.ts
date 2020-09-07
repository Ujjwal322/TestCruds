import { Component, ViewChild, ElementRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/customerservice';
import * as XLSX from 'xlsx';  


@Component({
  selector: 'fetchcustomer',
  templateUrl: './fetchcustomer.component.html'
})

export class FetchCustomerComponent {
  @ViewChild('TABLE') TABLE: ElementRef;  

  title = 'Excel';
  ExportTOExcel() {  
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);  
    const wb: XLSX.WorkBook = XLSX.utils.book_new();  
    XLSX.utils.book_append_sheet(wb, ws, 'Customers');  
    XLSX.writeFile(wb, 'CustomersInvoicesPayment.xlsx');  
  }  



  public custList: CustomerData[];
  customerId: number;
  customerNo: string;
  customerName: string;

  constructor(public http: Http, private _router: Router, private customerservice: CustomerService) {
    this.getCustomer();

  }

  getCustomer() {
    this.customerservice.getCustomerList().subscribe(
      (data: CustomerData[]) => {
        var res = data;
        this.custList = data;
      }
    );
  }

  delete(customerId) {
    var ans = confirm("Do you want to delete customer with customerId: " + customerId);
    if (ans) {
      this.customerservice.deleteCustomer(customerId).subscribe(() => {
        this.getCustomer();
      }, error => console.error(error))
    }
  }

}


interface CustomerData {
  customerId: number;
  customerNo: string;
  customerName: string;
}
