class PartialPaymentVM {
    idPartialPayment;
    folio;
    partialPaymentDate;
    amount;
    numberPayments;
    description;
    expiration_date;
    agreementId;
    account;
    constructor(p) {
        this.idPartialPayment = p.idPartialPayment || 0;
        this.folio = p.folio || "";
        this.partialPaymentDate = p.partialPaymentDate || "";
        this.amount = p.amount || 0.0;
        this.numberPayments = p.numberPayments || 0;
        this.description = p.description || "";
        this.expiration_date = p.expiration_date || "";
        this.agreementId = p.agreementId || 0;
        this.account = p.account || "";
    }
}

class PartialPaymentDetailsVM {
    amount;
    description;
    onAccount;
    iva;
    paymentDay;
    paymentNumber;
    releaseDate;
    releasePeriod;
    constructor(p) {
        this.amount = p.amount || 0;
        this.description = p.description || "";
        this.onAccount = p.onAccount || 0;
        this.iva = p.iva || 0;
        this.paymentDay = p.paymentDay || "";
        this.paymentNumber = p.paymentNumber || 0;
        this.releaseDate = p.releaseDate || "";
        this.releasePeriod = p.releasePeriod || "";
    }
}