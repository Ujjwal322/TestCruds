import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/customerservice';


@Component({
  templateUrl: './addcustomer.component.html'
})

export class createcustomer implements OnInit {
  customerForm: FormGroup;
  title: string = "Create Customer";
  customerId: number;
  errorMessage: any;


  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute, private customerservice: CustomerService,
    private router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.customerId = this._avRoute.snapshot.params["id"];
    }

    this.customerForm = this._fb.group({
      customerId: 0,

         customerNo: ['', [Validators.required]],

      customerName: ['', [Validators.required]],
    })
  }

  ngOnInit() {
    if (this.customerId > 0) {
      this.title = "Edit Customer";
      this.customerservice.getCustomerById(this.customerId)
        .subscribe(resp => this.customerForm.setValue(resp)
          , error => this.errorMessage = error);
    }
  }

  save() {
    if (!this.customerForm.valid) {
      return;
    }
    if (this.title == "Create Customer") {
      this.customerservice.saveCustomer(this.customerForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-customer']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit Customer") {
      this.customerservice.updateCustomer(this.customerForm.value)
        .subscribe((data) => {
          this.router.navigate(['/fetch-customer']);
        }, error => this.errorMessage = error)
    }

  }

  cancel() {
    this.router.navigate(['/fetch-customer']);
  }

}
