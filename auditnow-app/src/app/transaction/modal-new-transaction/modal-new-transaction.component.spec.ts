import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalNewTransactionComponent } from './modal-new-transaction.component';

describe('ModalNewTransactionComponent', () => {
  let component: ModalNewTransactionComponent;
  let fixture: ComponentFixture<ModalNewTransactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalNewTransactionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalNewTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
