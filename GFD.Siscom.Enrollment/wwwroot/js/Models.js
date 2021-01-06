class AgreementVM {
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
}

class AgreementDetailVM {
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
        this.lastName = c.lastName || "";
        this.secondLastName = c.secondLastName || "";
        this.rfc = c.rfc || "";
        this.curp = c.curp || "";
        this.ine = c.ine || "";
        this.eMail = c.eMail || "";
        this.typeUser = c.typeUser || "";
        this.taxRegime = c.taxRegime || true;
        this.isMale = c.isMale || true;
        this.isActive = c.isActive || true;
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
    isActive;
}