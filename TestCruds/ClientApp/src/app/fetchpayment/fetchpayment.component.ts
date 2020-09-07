import { Component, ViewChild, ElementRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/customerservice';
import { InvoiceService } from '../services/invoiceservice';
import * as XLSX from 'xlsx';
import { PaymentService } from '../services/paymentservice';

@Component({
  selector: 'fetchpayment',
  templateUrl: './fetchpayment.component.html'
})

export class FetchPaymentComponent {
   

  @ViewChild('TABLE') TABLE: ElementRef;

  title = 'Excel';
  ExportTOExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Payment');
    XLSX.writeFile(wb, 'CustomersInvoicesPayment.xlsx');
  }

  public payList: PaymentData[];

  paymentId: number;
  paymentNo: string;
  invoiceId: number;
  paymentDate: string;
  paymentAmount: string;

  constructor(public http: Http, private _router: Router, private paymentservice: PaymentService) {
    this.getPaymet();

  }

  getPaymet() {
    this.paymentservice.getPaymentList().subscribe(
      (data: PaymentData[]) => {
        var res = data;
        this.payList = data;
      }
    );
  }


}

interface PaymentData {
  paymentId: number;
  paymentNo: string;
  invoiceId: number;
  paymentDate: string;
  paymentAmount: string;
}
