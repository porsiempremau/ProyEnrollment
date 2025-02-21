﻿class AgreementVM {
    id;
    account;
    route;
    derivatives;
    typeServiceId;
    typeUseId;
    typeConsumeId;
    typeRegimeId;
    typePeriodId;
    typeCommertialBusinessId;
    typeStateServiceId;
    typeIntakeId;
    typeClasificationId;
    diameterId;
    typeAgreement;
    agreementPrincipalId;
    userId;
    observations;
    servicesId;
    adresses;
    clients;
    agreementDetails;
    constructor(a) {
        this.id = a.id || 0;
        this.account = a.account || "";
        this.route = a.route || "1";
        this.derivatives = a.derivatives || 0;
        this.typeServiceId = a.typeServiceId || 0;
        this.typeUseId = a.typeUseId || 0;
        this.typeConsumeId = a.typeConsumeId || 0;
        this.typeRegimeId = a.typeRegimeId || 0;
        this.typePeriodId = a.typePeriodId || 0;
        this.typeCommertialBusinessId = a.typeCommertialBusinessId || 0;
        this.typeStateServiceId = a.typeStateServiceId || 0;
        this.typeIntakeId = a.typeIntakeId || 0;
        this.typeClasificationId = a.typeClasificationId || 0;
        this.diameterId = a.diameterId || 0;
        this.typeAgreement = a.typeAgreement || "";
        this.agreementPrincipalId = a.agreementPrincipalId || 0;
        this.userId = a.userId || "";
        this.observations = a.observations || "";
        this.servicesId = a.servicesId || [];
        this.adresses = a.adresses || [];
        this.clients = a.clients || [];
        this.agreementDetails = a.agreementDetails || [];
    }
}

class AgreementDetailVM {
    id
    folio;
    register;
    taxableBase;
    ground;
    built;
    agreementDetailDate;
    lastUpdate;
    sector;
    observation;
    manifest;
    catastralKey;
    agreementId;
    constructor(ad) {
        this.id = ad.id || 0;
        this.folio = ad.folio || "";
        this.register = ad.register || "";
        this.taxableBase = ad.taxableBase || 0.0;
        this.ground = ad.ground || 0.0;
        this.build = ad.build || 0.0;
        this.agreementDetailDate = ad.agreementDetailDate;
        this.lastUpdate = ad.lastUpdate;
        this.sector = ad.sector || 0;
        this.observation = ad.observation || "";
        this.manifest = ad.manifest || false;
        this.catastralKey = ad.catastralKey || "";
        this.agreementId = ad.agreementId || 0;
    }
}

class ClientVM {
    id;
    name;
    lastName;
    secondLastName;
    rfc;
    curp;
    ine;
    eMail;
    typeUser;
    taxRegime;
    isMale;
    isActive;
    contacts;
    agreementId;
    constructor(c) {
        this.id = c.id || 0;
        this.name = c.name || "";
        this.lastName = c.lastName || "-";
        this.secondLastName = c.secondLastName || "-";
        this.rfc = c.rfc || "XAXX010101000";
        this.curp = c.curp || "";
        this.ine = c.ine || "";
        this.eMail = c.eMail || "";
        this.typeUser = c.typeUser || "";
        this.taxRegime = c.taxRegime || false;
        this.isMale = c.isMale || true;
        this.isActive = c.isActive;
        this.contacts = c.contacts || [];
        this.agreementId = c.agreementId || 0;
    }
}

class ContactVM {
    id;
    phoneNumber;
    typeNumber;
    isActive;
    clientId;
    constructor(contact) {
        this.id = contact.id || 0;
        this.phoneNumber = contact.phoneNumber || "";
        this.typeNumber = contact.typeNumber || "";
        this.isActive = contact.isActive;
        this.clientId = contact.clientId;
    }
}

class AddressVM {
    id;
    street;
    outdoor;
    indoor;
    zip;
    reference;
    lat;
    lon;
    typeAddress;
    suburbsId;
    suburbs;
    isActive;
    stateId
    stateName;
    townId
    townName;
    suburbName;
    agreementId;
    constructor(address) {
        this.id = address.id || 0;
        this.street = address.street || "";
        this.outdoor = address.outdoor || "";
        this.indoor = address.indoor || "";
        this.zip = address.zip || "";
        this.reference = address.reference || "";
        this.lat = address.lat || "";
        this.lon = address.lon || "";
        this.typeAddress = address.typeAddress || "";
        this.suburbsId = address.suburbsId || 0;
        this.suburbs = address.suburbs;
        this.isActive = address.isActive;
        this.stateId = address.stateId || 0;
        this.stateName = address.stateName || "";
        this.townId = address.townId || 0;
        this.townName = address.townName || "";
        this.suburbName = address.suburbName || "";
        this.agreementId = address.agreementId || 0;
    }
}

class DebtVM {
    id;
    debitDate;
    fromDate;
    untilDate;
    derivatives;
    typeIntake;
    typeService;
    consumption;
    discount;
    amount;
    onAccount;
    year;
    type;
    descriptionType;
    status;
    descriptionStatus;
    newStatus;
    debtPeriodId;
    expirationDate;
    rechargesDate;
    observations;
    agreementId;
    agreement;
    debtDetails;
    constructor(d) {
        this.id = d.id || 0;
        this.debitDate = d.debitDate || "0001-01-01T00:00:00";
        this.fromDate = d.fromDate || "0001-01-01T00:00:00";
        this.untilDate = d.untilDate || "0001-01-01T00:00:00";
        this.derivatives = d.derivatives || 0;
        this.typeIntake = d.typeIntake || "0";
        this.typeService = d.typeService || "0";
        this.consumption = d.consumption || "0";
        this.discount = d.discount || "";
        this.amount = d.amount || 0.0;
        this.onAccount = d.onAccount || 0.0;
        this.year = d.year || 0;
        this.type = d.type || "TIP02";
        this.descriptionType = d.descriptionType || "";
        this.status = d.status || "ED001";
        this.descriptionStatus = d.descriptionStatus || "";
        this.newStatus = d.newStatus || "";
        this.debtPeriodId = d.debtPeriodId || 0;
        this.expirationDate = d.expirationDate || "0001-01-01T00:00:00";
        this.rechargesDate = d.rechargesDate || "0001-01-01T00:00:00";
        this.observations = d.observations || "N/A";
        this.agreementId = d.agreementId || 0;
        this.agreement = d.agreement || null;
        this.debtDetails = d.debtDetails || [];
    }
}

class AgreementListVM {
    account;
    address;
    agreementId;
    debit;
    endDate;
    idClient;
    idStatus;
    isActiveDiscount;
    nameDiscount;
    nombre;
    numDerivades;
    rfc;
    status;
    taxableBase;
    token;
    type;
    withDiscount;
    constructor(a) {
        this.account = a.account || "";
        this.address = a.address || "";
        this.agreementId = a.agreementId || 0;
        this.debit = a.debit || 0;
        this.endDate = a.endDate;
        this.idClient = a.idClient || 0;
        this.idStatus = a.idStatus || 0;
        this.isActiveDiscount = a.isActiveDiscount;
        this.nameDiscount = a.nameDiscount || "";
        this.nombre = a.nombre || "";
        this.numDerivades = a.numDerivades || 0;
        this.rfc = a.rfc || "";
        this.status = a.status || "";
        this.taxableBase = a.taxableBase;
        this.token = a.token || "";
        this.type = a.type || ""
        this.withDiscount = a.withDiscount;
    }
}


class ProofNoDebtVM {
    id;
    folio;
    expedition_date;
    expiration_date;
    userId;
    agreementId;
    agreement;
    constructor(p) {
        this.id = p.id || 0;
        this.folio = p.folio || 0;
        this.expedition_date = p.expedition_date;
        this.expiration_date = p.expirationDate;
        this.userId = p.userId || '';
        this.agreementId = p.agreementId || 0;
        this.agreement = p.agreement || null;
    }
}


