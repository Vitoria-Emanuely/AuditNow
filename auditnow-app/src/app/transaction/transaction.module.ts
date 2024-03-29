import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { AlertService } from '../core/services/alert.service';
import { SharedModule } from '../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UIModule } from '../shared/ui/ui.module';
import { UiSwitchModule } from 'ngx-ui-switch';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TransactionComponent } from './transaction.component';
import { TransactionRoutes } from './transaction.routing';


@NgModule({
  declarations: [
    TransactionComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(TransactionRoutes),
    ReactiveFormsModule,//
    TranslateModule,
    AlertModule.forRoot(),
    TabsModule.forRoot(),
    AccordionModule.forRoot(),
    SharedModule,
    NgSelectModule,
    PaginationModule.forRoot(),
    FormsModule,
    UIModule,
    UiSwitchModule,
    NgbModule,
  ],
  providers:[
    AlertService
  ]
})
export class TransactionModule { }
