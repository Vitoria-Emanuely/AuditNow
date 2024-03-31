import { Component } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { PageChangedEvent } from "ngx-bootstrap/pagination";
import { NgxSpinnerService } from "ngx-spinner";
import { TransactionService } from "../core/services/transaction.service";
import { TransactionFilter } from "../core/models/transaction.models";
import { TranslateService } from "@ngx-translate/core";
import { BsModalService } from "ngx-bootstrap/modal";
import { ModalNewTransactionComponent } from "./modal-new-transaction/modal-new-transaction.component";

@Component({
  selector: "app-transaction",
  templateUrl: "./transaction.component.html",
  styleUrls: ["./transaction.component.scss"],
})
export class TransactionComponent {
  showFilter: boolean = true;
  form: FormGroup;
  alerts: any = [];
  transactions: any = [];
  transactionType: any = [];
  balance: any;

  prevText = "<";
  nextText = ">";
  page = 1;
  total = 0;
  itemsPerPage = 10;
  endPage = 0;
  endItem = 0;
  startItem = 0;

  returnedArray?: any;
  contentArray = new Array().fill("");

  constructor(
    private spinner: NgxSpinnerService,
    public translate: TranslateService,
    private formBuilder: FormBuilder,
    private transactionService: TransactionService,
    private modalService: BsModalService,
  ) {
    this.translate.addLangs(["pt", "es"]);
    this.translate.setDefaultLang("pt");
    const browserLang = this.translate.getBrowserLang();
    this.translate.use(browserLang.match(/pt|es/) ? browserLang : "pt");
  }

  ngOnInit() {
    this.createFilterForm(new TransactionFilter());
    this.getTransactionTypes();
    this.filterTransactions();
  }

  createFilterForm(transaction: TransactionFilter) {
    this.form = this.formBuilder.group({
      transactionId: new FormControl(transaction.transactionId, null),
      transactionType: new FormControl(transaction.transactionType, null),
      isActive: new FormControl(transaction.isActive, null),
    });
  }

  filterTransactions() {
    this.spinner.show();
    this.transactionService
      .getTransactionsFilter(this.form.value)
      .subscribe(
        (response: any) => {
          this.transactions = response.data == null ? [] : response.data;

          this.contentArray = this.transactions.map((v: string, i: number) => `Content line ${i + 1}`);
          this.returnedArray = this.transactions.slice(0, 10);

          this.pageChanged({itemsPerPage: this.itemsPerPage, page: this.page});
        
          this.transactions.map((transaction) => {
            const statusIndex = this.transactionType.findIndex(
              (s) => s.id === transaction.transactionType
            );
            if (statusIndex !== -1) {
              transaction.transactionType =
                this.transactionType[statusIndex].description;
            } else {  
              transaction.transactionType = null;
            }
          });     
        },
        (error: any) => {}
      )
      .add(() => {
        this.spinner.hide();
      });
  }

  getTransactionTypes() {
    this.transactionService.getTransactionType().subscribe(
      (response: any) => {
        this.transactionType = response.data == null ? [] : response.data;
        this.transactionType.unshift({
          description: "TRANSACTION",
          id: 0,
        });
      },
      (error: any) => {}
    );
  }

  pageChanged(event: PageChangedEvent): void {
    const { page, itemsPerPage } = event;

    const totalPages = Math.ceil(this.transactions.length / itemsPerPage);
    const currentPage = Math.max(1, Math.min(page, totalPages));

    const startItem = (currentPage - 1) * itemsPerPage + 1;
    let endItem = startItem + itemsPerPage - 1;

    if (endItem > this.transactions.length) {
      endItem = this.transactions.length;
    }

    this.returnedArray = this.transactions.slice(startItem - 1, endItem);

    this.page = currentPage;
    this.endPage = totalPages;

    this.startItem = startItem;
    this.endItem = endItem;
  }

  openModalNewTransaction() {
    let modalRef = this.modalService.show(ModalNewTransactionComponent, { class: 'modal-md modal-dialog-centered', ariaLabelledBy: "modal-basic-title" })

    modalRef.content.event.subscribe((result) => {
      if (result) {
        this.filterTransactions();
      }
    });
  }
}
