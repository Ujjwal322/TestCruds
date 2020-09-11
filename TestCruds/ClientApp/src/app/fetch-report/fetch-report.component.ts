import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import * as XLSX from 'xlsx';
import { CustomerService } from '../services/customerservice';
declare var $;
declare var jQuery;
@Component({
  selector: 'fetch-report',
  templateUrl: './fetch-report.component.html',
  styleUrls: ['./fetch-report.component.css']
})

export class FetchReportComponent implements OnInit {

  dataTable: any;
  tableData: any;

  @ViewChild('TABLE') TABLE: ElementRef;

  title = 'Excel';
  ExportTOExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'InvoiceDetails');
    XLSX.writeFile(wb, 'CustomersInvoicesPayment.xlsx');
  }

  constructor(public http: Http, private _router: Router, private customerservice: CustomerService) {

  }

  ngOnInit(): void {
    this.getCustomerData();
  }
  getCustomerData() {
    this.customerservice.getCustomerReport().subscribe(data => {
      this.tableData = data;

      var i = $('#table1').DataTable({
        data: this.tableData,
        "language": {
          "lengthMenu": "Display _MENU_ records per page  ",
          "zeroRecords": "No records found",
          "infoEmpty": "No records available",
          "infoFiltered": "(filtered from _MAX_ total records)",
        },
        lengthMenu: [[10, 15, 25, 50, -1], [10, 15, 25, 50, "All"]],
        columnDefs: [
          { type: 'stringMonthYear', targets: 0 }
        ],
        "deferRender": true,
        columns: [
          {
            data: 'dateOfMonth',
            render: function (data) {
              var date = new Date(data);
              var month = date.toLocaleString('en-us', { month: 'short' });
              var Data1 = (month.toString()) + "-" + date.getFullYear().toString().substr(-2);
              return Data1;
            }
          },
          { data: 'customerName' },
          { data: 'noOfInvoices', className: "dt-right" },
          { data: 'sales', render: $.fn.dataTable.render.number(',', '.', 2, '$'), className: "dt-right" },
          { data: 'paymentCollection', render: $.fn.dataTable.render.number(',', '.', 2, '$'), className: "dt-right" },
        ],

        initComplete: function () {
          this.api().columns([1]).every(function () {
            var column = this;
            var select = $('<select><option value="">Select Customername</option></select>')
              .appendTo('#ddlcustomername')
              .on('change', function () {
                var val = $(this).val();
                column.search(this.value).draw();
              });
            console.log(column.data().unique());

            column.data().unique().sort().each(function (d, j) {
              select.append('<option value="' + d + '">' + d + '</option>')
            });

          });
        },

      });

    });

    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
      "stringMonthYear-pre": function (s) {
        var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

        var dateComponents = s.split("-");
        dateComponents[0] = dateComponents[0].replace(",", "");
        dateComponents[1] = jQuery.trim(dateComponents[1]);

        var year = dateComponents[1];

        var month = 0;
        for (var i = 0; i < months.length; i++) {
          if (months[i].toLowerCase() == dateComponents[0].toLowerCase().substring(0, 3)) {
            month = i;
            break;
          }
        }

        return new Date(year, month, 1);
      },

      "stringMonthYear-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
      },

      "stringMonthYear-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
      }
    });

  }

}
