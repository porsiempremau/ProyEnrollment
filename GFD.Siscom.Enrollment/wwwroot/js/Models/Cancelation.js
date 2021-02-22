class CancelationVM {
    id;
    status;
    dateRequest;
    reason;
    manager;
    dateAuthorization;
    managerObservation;
    keyFirebase;
    transactionId;
    transaction;
    constructor(x) {
        this.id = x.id || 0;
        this.status = x.status || "";
        this.dateRequest = x.dateRequest || "";
        this.reason = x.reason || ""
        this.manager = x.manager || null;
        this.dateAuthorization = x.dateAuthorization || "";
        this.managerObservation = x.managerObservation || null;
        this.keyFirebase = x.keyFirebase || "";
        this.transactionId = x.transactionId || 0;
        this.transaction = x.transaction || null;
    }
}


class CancelationTransactionVM {
    id;
    status;
    dateRequest;
    userName;
    reason;
    branchOffice;
    amount;
    printingFolio;
    manager;
    dateAuthorization;
    managerObservation;
    keyFirebase;
    transactionId;
    constructor(x) {
        this.id = x.id || 0;
        this.status = x.status || "";
        this.dateRequest = x.dateRequest || "";
        this.userName = x.userName || "";
        this.reason = x.reason || "";
        this.branchOffice = x.branchOffice || "";
        this.amount = x.amount || 0.0;
        this.printingFolio = x.printingFolio || "";
        this.manager = x.manager || null;
        this.dateAuthorization = x.dateAuthorization || "";
        this.managerObservation = x.managerObservation || null;
        this.keyFirebase = x.keyFirebase || "";
        this.transactionId = x.transactionId || 0;
    }
}

