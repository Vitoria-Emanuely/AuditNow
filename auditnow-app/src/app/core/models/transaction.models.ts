export class TransactionFilter {
    public constructor(init?: Partial<TransactionFilter>){
        Object.assign(this, init)
    }

    transactionId: number;
    transactionType: number;
    isActive: boolean;
}

export class CreateTransaction {
    public constructor(init?: Partial<CreateTransaction>){
        Object.assign(this, init)
    }

    transactionType: number;
    value: number;
}

export class Transaction {
    public constructor(init?: Partial<CreateTransaction>){
        Object.assign(this, init)
    }

    transactionId: number;
    transactionType: number;
    value: number;
    balance: number;
    isActive: boolean;
}