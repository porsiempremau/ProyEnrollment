class AuthDiscount {
    id;
    account;
    accountAdjusted;
    nameUserResponse;
    status;
    amountDiscount;
    amount;
    branchOffice;
    observation;
    fileName;
    fileNameDB;
    isApplied;
    keyFirebase;
    requestDate;
    type;
    userAuthorizationId;
    constructor(authdiscount) {
        this.id = authdiscount.id || 0;
        this.status = authdiscount.status || '';
        this.account = authdiscount.account || '';
        this.accountAdjusted = authdiscount.accountAdjusted || '';
        this.nameUserResponse = authdiscount.nameUserResponse || '';
        this.amount = authdiscount.amount || 0;
        this.branchOffice = authdiscount.branchOffice || '';
        this.observation = authdiscount.observation || '';
        this.fileName = authdiscount.fileName || '';
        this.fileNameDB = authdiscount.fileNameDB || '';
        this.amountDiscount = authdiscount.amountDiscount || 0;
        this.isApplied = authdiscount.isApplied;
        this.keyFirebase = authdiscount.keyFirebase || "";
        this.requestDate = authdiscount.requestDate || "";
        this.type = authdiscount.type || "";
        this.userAuthorizationId = authdiscount.userAuthorizationId || "";
    }
}