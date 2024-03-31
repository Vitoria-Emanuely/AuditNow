import { Component, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CreateTransaction } from 'src/app/core/models/transaction.models';
import { AlertService } from 'src/app/core/services/alert.service';
import { TransactionService } from 'src/app/core/services/transaction.service';

@Component({
  selector: 'app-modal-new-transaction',
  templateUrl: './modal-new-transaction.component.html',
  styleUrls: ['./modal-new-transaction.component.scss']
})
export class ModalNewTransactionComponent {
  public event: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  alerts: any = [];
  transactionType: any = [];

  constructor(
    public toastr: ToastrService,
    private spinner: NgxSpinnerService,
    public translate: TranslateService,
    private formBuilder: FormBuilder,
    private transactionService: TransactionService,
    public modalService: BsModalService,
    private alertService: AlertService,
  ) {

  }

  ngOnInit(): void {
    this.createForm(new CreateTransaction());
    this.getTransactionTypes();
  }

  createForm(transaction: CreateTransaction) {
    this.form = this.formBuilder.group({
      transactionType: new FormControl(transaction.transactionType, Validators.required),
      value: new FormControl(transaction.value, Validators.required)
    });
  }

  private validateFields() {
    Object.keys(this.form.controls).forEach((key) => {
      if (
        this.getControl(key).value == "" ||
        this.getControl(key).value == null
      )
        this.getControl(key).markAsTouched();
    });
  }

  getControl(name: string): AbstractControl {
    return this.form.get(name);
  }

  getTransactionTypes() {
    this.transactionService.getTransactionType().subscribe(
      (response: any) => {
        this.transactionType = response.data == null ? [] : response.data;
      },
      (error: any) => {}
    );
  }

  createTransaction() {
    if (this.form.invalid) {
      this.validateFields();
    } else {
      this.spinner.show();

      this.transactionService.createTransaction(this.form.value).subscribe(
        (response: any) => {
          if (response.isSuccessful == true) {
            this.toastr.success(response.message);
            this.modalService.hide();
            this.event.emit(response.isSuccessful);
          }
          else {
            this.alerts.push(this.alertService.errorIcon(response.message));
          }
        },
        error => {
          this.alerts.push(this.alertService.errorIcon("Erro ao fazer transação"));
        }
      ).add(() => {
        this.spinner.hide();
      })
    }
  }

}
