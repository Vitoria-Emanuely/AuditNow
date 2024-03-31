import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
    })
};

@Injectable({ providedIn: 'root' })
export class TransactionService {
    constructor(private http: HttpClient) { }

    getTransactionsFilter(transaction) {
        return this.http.get(
            `${environment.apiBaseAddress}Transaction/find?` +
            `transactionId=${(transaction.transactionId == null || transaction.transactionId == "") ? 0 : parseInt(transaction.transactionId)}&` +
            `transactionType=${transaction.transactionType == null ? 0 : transaction.transactionType}&` +
            `isActive=${transaction.isActive == null ? "" : transaction.isActive}`,
            httpOptions
        );
    }

    createTransaction(transaction) {
        return this.http.post(
            `${environment.apiBaseAddress}Transaction`,
            transaction,
            httpOptions
        );
    }

    getTransactionType() {
        return this.http.get(
            `${environment.apiBaseAddress}Transaction/transactionTypes`,
            httpOptions
        );
    }

}
