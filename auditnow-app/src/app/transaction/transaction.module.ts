import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertModule } from 'ngx-bootstrap/alert';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { AlertService } from '../core/services/alert.service';
import { SharedModule } from '../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UIModule } from '../shared/ui/ui.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TransactionComponent } from './transaction.component';
import { TransactionRoutes } from './transaction.routing';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { ModalNewTransactionComponent } from './modal-new-transaction/modal-new-transaction.component';
import { NgxMaskModule } from 'ngx-mask';


registerLocaleData(localePt);


@NgModule({
  declarations: [
    TransactionComponent,
    ModalNewTransactionComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(TransactionRoutes),
    ReactiveFormsModule,//
    TranslateModule,
    AlertModule.forRoot(),
    SharedModule,
    NgSelectModule,
    PaginationModule.forRoot(),
    FormsModule,
    UIModule,
    NgbModule,
    NgxMaskModule.forRoot()
  ],
  providers:[
    AlertService,
    CurrencyPipe
  ]
})
export class TransactionModule { }
