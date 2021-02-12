class ProductsVM {
    id;
    name;
    order;
    parent;
    haveTariff;
    isActive;
    haveAccount;
    divisionId;
    tariffProducts;
    productParams;
    label;
    children;
    constructor(p) {
        this.id = p.id;
        this.name = p.name;
        this.order = p.order;
        this.parent = p.parent;
        this.haveTariff = p.haveTariff;
        this.isActive = p.isActive;
        this.haveAccount = p.haveAccount;
        this.divisionId = p.divisionId;
        this.tariffProducts = p.tariffProducts || [];
        this.productParams = p.productParams || [];
        this.label = p.label || "";
        this.children = p.children || [];
    }
}

class DebtDetailsVM {
    id;
    amount;
    OnAccount;
    OnPayment;
    haveTax;
    CodeConcept;
    NameConcept;
    Tax;
    DebtId;
    Debt;
    quantity;
    type;
    percentage;
    timesFactor;
    agreementId;
    subtotal;
    Unity;
    UnitPrice;
    Description;
    OrderSaleId;
    OrderSale;
    constructor(debt) {
        this.id = debt.id || 0;
        this.amount = debt.Amount || 0;
        this.OnAccount = debt.OnAccount || 0;
        this.OnPayment = debt.OnPayment || 0;
        this.haveTax = debt.HaveTax;
        this.CodeConcept = debt.CodeConcept || '';
        this.NameConcept = debt.NameConcept || '';
        this.Tax = debt.Tax || 0;
        this.DebtId = debt.DebtId || 0;
        this.Debt = debt.Debt || '';
        this.quantity = 1;
        this.type = debt.type || '';
        this.percentage = debt.percentage || 0;
        this.timesFactor = debt.timesFactor || 0;
        this.agreementId = debt.agreementId || 0;
        this.subtotal = debt.Amount || 0;
        this.Unity = debt.Unity || '';
        this.UnitPrice = debt.UnitPrice || 0;
        this.Description = debt.Description || '';
        this.OrderSaleId = debt.OrderSaleId || 0;
        this.OrderSale = debt.OrderSale || '';
    }
}

class TariffProductVM {
    idProduct;
    type;
    account;
    amount;
    tax;
    total;
    percentage;
    timesFactor;
    haveTax;
    product;
    constructor(tariff) {
        this.idProduct = tariff.idProduct || 0;
        this.type = tariff.type || 0;
        this.account = tariff.account || "";
        this.amount = tariff.amount || 0;
        this.tax = tariff.tax || 0;
        this.total = tariff.total || 0;
        this.percentage = tariff.percentage || 0;
        this.timesFactor = tariff.timesFactor || 0;
        this.haveTax = tariff.haveTax;
        this.product = tariff.product || [];
    }
}

class OrderSaleVM {
    id;
    folio;
    dateOrder;
    amount;
    onAccount;
    year;
    period;
    type;
    tax;
    descriptionType;
    status;
    descriptionStatus;
    observation;
    idOrigin;
    expirationDate;
    divisionId;
    division;
    taxUserId;
    taxUser;
    orderSaleDetails;
    orderSaleDiscounts;
    orderSaleStatuses;
    constructor(o) {
        this.id = o.id || 0;
        this.folio = o.folio || null;
        this.dateOrder = o.dateOrder;
        this.amount = o.amount;
        this.onAccount = o.onAccount || 0.0;
        this.year = o.year;
        this.period = o.period || 1;
        this.type = o.type || "OA001";
        this.tax = o.tax;
        this.descriptionType = o.descriptionType || null;
        this.status = o.status || "ED001";
        this.descriptionStatus = o.descriptionStatus || null;
        this.observation = o.observation;
        this.idOrigin = o.idOrigin || 0;
        this.expirationDate = o.expirationDate || "0001-01-01T00:00:00";
        this.divisionId = o.divisionId;
        this.division = o.division || null;
        this.taxUserId = o.taxUserId || 0;
        this.taxUser = o.taxUser;
        this.orderSaleDetails = o.orderSaleDetails;   
        this.orderSaleDiscounts = o.orderSaleDiscounts || [];
        this.orderSaleStatuses = o.orderSaleStatuses || [];
    }
}



class TaxUserVM {
    id;
    name;
    rfc;
    curp;
    phoneNumber;
    eMail;
    isActive;
    isProvider;
    taxAddresses;
    breaches;
    orderSales;
    constructor(t) {
        this.id = t.id || 0;
        this.name = t.name;
        this.rfc = t.rfc;
        this.curp = t.curp;
        this.phoneNumber = t.phoneNumber;
        this.eMail = t.eMail;
        this.isActive = t.isActive || true;
        this.isProvider = t.isProvider;
        this.taxAddresses = t.taxAddresses || [];
        this.breaches = t.breaches || [];
        this.orderSales = t.orderSales || [];
    }
}

class TaxAddressVM {
    id;
    street;
    outdoor;
    indoor;
    zip;
    suburb;
    town;
    state;
    taxUserId;
    taxUser;
    constructor(t) {
        this.id = t.id || 0;
        this.street = t.street;
        this.outdoor = t.outdoor;
        this.indoor = t.indoor;
        this.zip = t.zip;
        this.suburb = t.suburb;
        this.town = t.town;
        this.state = t.state;
        this.taxUserId = t.taxUserId || 0;
        this.taxUser = t.taxUser || null;
    }
}

//class OrderSaleVM {
//    Id;
//    Folio;
//    DateOrder;
//    Amount;
//    OnAccount;
//    Year;
//    Period;
//    Type;
//    Tax;
//    DescriptionType;
//    Status;
//    DescriptionStatus;
//    Observation;
//    IdOrigin;
//    ExpirationDate;
//    DivisionId;
//    Division;
//    TaxUserId;
//    TaxUser;
//    OrderSaleDetails;
//    OrderSaleDiscounts;
//    OrderSaleStatuses;
//    constructor(o) {
//        this.Id = o.Id || 0;
//        this.Folio = o.Folio || null;
//        this.DateOrder = o.DateOrder;
//        this.Amount = o.Amount;
//        this.OnAccount = o.OnAccount || 0.0;
//        this.Year = o.Year;
//        this.Period = o.Period || 1;
//        this.Type = o.Type || "OA001";
//        this.Tax = o.Tax;
//        this.DescriptionType = o.Description || null;
//        this.Status = o.Status || "ED001";
//        this.DescriptionStatus = o.DescriptionStatus || null;
//        this.Observation = o.Observation;
//        this.IdOrigin = o.IdOrigin || 0;
//        this.ExpirationDate = o.ExpirationDate || "0001-01-01T00:00:00";
//        this.DivisionId = o.DivisionId;
//        this.Division = o.Division || null;
//        this.TaxUserId = o.TaxUserId || 0;
//        this.TaxUser = o.TaxUser;
//        this.OrderSaleDetails = o.OrderSaleDetails;
//        this.OrderSaleDiscounts = o.OrderSaleDiscounts || [];
//        this.OrderSaleStatuses = o.OrderSaleStatuses || [];
//    }
//}



//class TaxUserVM {
//    Id;
//    Name;
//    RFC;
//    CURP;
//    PhoneNumber;
//    EMail;
//    IsActive;
//    IsProvider;
//    TaxAddresses;
//    Breaches;
//    OrderSales;
//    constructor(t) {
//        this.Id = t.Id || 0;
//        this.Name = t.Name;
//        this.RFC = t.RFC;
//        this.CURP = t.CURP;
//        this.PhoneNumber = t.PhoneNumber;
//        this.EMail = t.EMail;
//        this.IsActive = t.isActive || true;
//        this.IsProvider = t.IsProvider;
//        this.TaxAddresses = t.TaxAddresses || [];
//        this.Breaches = t.Breaches || [];
//        this.OrderSales = t.OrderSale || [];
//    }
//}

//class TaxAddressVM {
//    Id;
//    Street;
//    Outdoor;
//    Indoor;
//    Zip;
//    Suburb;
//    Town;
//    State;
//    TaxUserId;
//    TaxUser;
//    constructor(t) {
//        this.Id = t.Id || 0;
//        this.Street = t.Street;
//        this.Outdoor = t.Outdoor;
//        this.Indoor = t.Indoor;
//        this.Zip = t.Zip;
//        this.Suburb = t.Suburb;
//        this.Town = t.Town;
//        this.State = t.State;
//        this.TaxUserId = t.TaxUserId || 0;
//        this.TaxUser = t.TaxUser || null;
//    }
//}