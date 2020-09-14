import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { InvoiceService } from '../services/invoiceservice';

@Component({
  templateUrl: './addinvoice.component.html'
})

export class createinvoice implements OnInit {
  invoiceForm: FormGroup;
  title: string = "Create Invoice";
  invoiceId: number;
  invoiceNo: string;
  customerId: number;
  invoiceDate: Date;
  invoiceAmount: string;
  paymentDueDate: Date;
  errorMessage: any;
  customerList: Array<any> = [];
  submitted = false;

  //invoiceDate = new Date();
  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute, private invoiceservice: InvoiceService,
    private router: Router) {

    if (this._avRoute.snapshot.params["id"]) {
      this.invoiceId = this._avRoute.snapshot.params["id"];
    }

    this.invoiceForm = this._fb.group({
      invoiceId: 0,

      invoiceNo: ['', [Validators.required]],

      customerId: ['', [Validators.required]],

      invoiceDate: ['', [Validators.required]],

      invoiceAmount: ['', [Validators.required]],

      paymentDueDate: ['', [Validators.required]],

  
    })
  }

  ngOnInit() {

    this.invoiceservice.getCustomerList().subscribe(
      data => this.customerList = data
    )

    if (this.invoiceId > 0) {
      this.title = "Edit Invoice";
      this.invoiceservice.getInvoiceById(this.invoiceId)
        .subscribe(resp => this.invoiceForm.setValue(resp)
          , error => this.errorMessage = error);
    }
  }

  get f() { return this.invoiceForm.controls; }

  save() {

    this.submitted = true;

    if (!this.invoiceForm.valid) {
      return;
    }
    if (this.title == "Create Invoice") {
      this.invoiceservice.saveInvoice(this.invoiceForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-invoice']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit Invoice") {
      this.invoiceservice.updateInvoice(this.invoiceForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-invoice']);
        }, error => this.errorMessage = error)
    }

  }

  cancel() {
    this.router.navigate(['/fetch-invoice']);
  }


}
