export const EmptyGuid = "00000000-0000-0000-0000-000000000000";

export const RegistrantStatus = Object.freeze({
    UNKNOWN: -1,
    NOT_REGISTERED: 0,
    CELLPHONE_NOT_VERIFIED: 1,
    EMAIL_NOT_VERIFIED: 2,
    FINISHED: 3,
});

export const ApiResultStatus = Object.freeze({
    NOT_FOUND: 1404,
    CONFLICT: 1409,
    BAD_REQUEST: 1400,
    PRECONDITION_FAILED: 1412,
    OK: 200,
});

export const RecipientType = Object.freeze({
    SUPPLIER: 1,
    DISTRIBUTOR: 2,
    RETAILER: 3
});

export const ProprietorshipType = Object.freeze({
    UNKNOWN: 0,
    PRIVATE_NATURAL_PERSON: 1,
    PRIVATE_LEGAL_PERSON: 2,
    PUBLIC: 3
});

export const OperationTime = Object.freeze({
    UNKNOWN: 0,
    DAILY: 1,
    TwentyFourSeven: 2,
});

export const OperationArea = Object.freeze({
    UNKNOWN: 0,
    PROVINCIAL: 1,
    COUNTRYWIDE: 2,
});

export const OperationField = Object.freeze({
    UNKNOWN: 0,
    MANUFACTURING: 1,
    IMPORT: 2,
    MANUFACTURING_IMPORT: 3
});

export const PaymentMethod = Object.freeze({
    UNKNOWN: 0,
    CHEQUE: 1,
    CASH: 2
});

export const ValidationLength = Object.freeze({
    FIRSTNAME: {
        MIN: 3,
        MAX: 15
    },
    lastName: {
        MIN: 3,
        MAX: 15
    },
    INSTITUE_NAME: {
        MIN: 3,
        MAX: 30
    },
    NATURAL_NATIONALCODE: {
        MIN: 10,
        MAX: 10
    },
    LEGAL_NATIONALCODE: {
        MIN: 11,
        MAX: 11
    },
    HIX: {
        MIN: 10,
        MAX: 10
    },
    GLN: {
        MIN: 13,
        MAX: 13
    },
    ADDRESS: {
        MIN: 25,
        MAX: 150
    },
    POSTALCODE: {
        MIN: 10,
        MAX: 10
    },
    PHONE: {
        MIN: 13,
        MAX: 13
    },
    CELLPHONE: {
        MIN: 13,
        MAX: 13
    },
});

// class EnumItem {
//     constructor(title) {
//       this.title = title;
//     }

//     toString() {
//       return this.title;
//     }
//   }

//   export const RegistrantStatus = Object.freeze({
//     NO_USER: new EnumItem("NO_USER"),
//     CELLPHONE_NOT_VERIFIED: new EnumItem("CELLPHONE_NOT_VERIFIED"),
//     EMAIL_NOT_VERIFIED: new EnumItem("EMAIL_NOT_VERIFIED"),
//     VERIFIED: new EnumItem("VERIFIED")
//   });