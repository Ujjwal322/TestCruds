import { Component, ViewChild, ElementRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/customerservice';
import { InvoiceService } from '../services/invoiceservice';
import * as XLSX from 'xlsx';

@Component({
  selector: 'fetchinvoice',
  templateUrl: './fetchinvoice.component.html'
})

export class FetchInvoiceComponent {
  @ViewChild('TABLE') TABLE: ElementRef;

  title = 'Excel';
  ExportTOExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Invoice');
    XLSX.writeFile(wb, 'CustomersInvoicesPayment.xlsx');
  }  




  public invList: InvoiceData[];

  invoiceId: number;
  invoiceNo: string;
  customerId: number;
  invoiceDate: Date;
  invoiceAmount: string;
  paymentDueDate: Date;

  constructor(public http: Http, private _router: Router, private invoiceservice: InvoiceService) {
    this.getInvoice();

  }

  getInvoice() {
    this.invoiceservice.getInvoiceList().subscribe(
      (data: InvoiceData[]) => {
        var res = data;
        this.invList = data;
      }
    );
  }

  delete(invoiceId) {
    var ans = confirm("Do you want to delete customer with  invoiceId: " + invoiceId);
    if (ans) {
      this.invoiceservice.deleteInvoice(invoiceId).subscribe(() => {
        this.getInvoice();
      }, error => console.error(error))
    }
  }

}

interface InvoiceData {
  invoiceId: number;
  invoiceNo: string;
  customerId: number;
  invoiceDate: Date;
  invoiceAmount: string;
  paymentDueDate: Date;
}
