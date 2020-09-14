import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Data } from '@angular/router';
import { PaymentService } from '../services/paymentservice';


@Component({
  templateUrl: './addpayment.component.html'
})

export class createpayment implements OnInit {
  paymentForm: FormGroup;
  title: string = "Create Payment";
  paymentId: number;
  paymentNo: string;
  invoiceId: number;
  paymentDate: Date;
  paymentAmount: string;
  errorMessage: any;
  invoiceList: Array<any> = [];
  submitted = false;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute, private paymentservice: PaymentService,
    private router: Router) {

    if (this._avRoute.snapshot.params["id"]) {
      this.paymentId = this._avRoute.snapshot.params["id"];
    }

    this.paymentForm = this._fb.group({
      paymentId: 0,

      paymentNo: ['', [Validators.required]],

      invoiceId: ['', [Validators.required]],

      paymentDate: ['', [Validators.required]],

      paymentAmount: ['', [Validators.required]],
    })
  }

  ngOnInit() {

    this.paymentservice.getInvoiceList().subscribe(
      data => this.invoiceList = data
    )

    if (this.paymentId > 0) {
      this.title = "Edit Payment";
      this.paymentservice.getPaymentById(this.paymentId)
        .subscribe(resp => this.paymentForm.setValue(resp)
          , error => this.errorMessage = error);
    }
  }

  get f() { return this.paymentForm.controls; }

  save() {
    this.submitted = true;
    if (!this.paymentForm.valid) {
      return;
    }
    if (this.title == "Create Payment") {
      this.paymentservice.savePayment(this.paymentForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-payment']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit Payment") {
      this.paymentservice.updatePayment(this.paymentForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-payment']);
        }, error => this.errorMessage = error)
    }

  }

  cancel() {
    this.router.navigate(['/fetch-payment']);
  }

}
