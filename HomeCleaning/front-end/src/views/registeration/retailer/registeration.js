import {
  required,
  minLength,
  maxLength,
  requiredIf
} from "vuelidate/lib/validators";

import Contact from "../../../components/Contact";
//import NaturalPerson from "../components/NaturalPerson";
import LegalPerson from "../components/LegalPerson";
import RecipientIdentity from "./components/RecipientIdentity";
import Shifts from "./components/Shifts";
import RetailerShareHolder from "./components/RetailerShareHolder";
import Ceo from "../components/Ceo";
import retailerApi from "../../../api/retailer";
import {
  ValidationLength,
  ProprietorshipType
} from "../../../constants";
import ScrollToInvalidInput from "../../../components/ScrollToInvalidInput";
import router from "../../../router";

export default {
  mixins: [ScrollToInvalidInput],
  props: {
    registrantId: {
      type: String,
      required
    },
  },
  data() {
    return {
      recipient: {
        hixCode: "",
        gln: "",
        operationTime: null,
        registrant: {
          id: this.registrantId
        },
        legalPerson: {
          nationalCode: "",
          officialName: "",
          proprietorship: null,
          ceo: {
            firstName: "",
            surname: "",
            nationalCode: ""
          },
          contact: {
            provinceId: null,
            countyId: null,
            cityId: null
          },
          legalShareHolder: {
            officialName: "",
            nationalCode: ""
          },
          naturalShareHolder: {
            firstName: "",
            surname: "",
            nationalCode: ""
          }
        }
      },
      ProprietorshipType: ProprietorshipType
    };
  },
  computed: {
    showCeo: function () {
      return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_LEGAL_PERSON ||
        this.recipient.legalPerson.proprietorship === ProprietorshipType.PUBLIC;
    }
  },
  methods: {
    register: async function () {
      this.$v.$touch();
      if (this.$v.$error) {
        this.goToInvalidInput();
        return false;
      }
      await retailerApi.register(this.recipient);
      router.push({
        name: "Registration‌‌Done"
      });
    }
  },
  components: {
    RecipientIdentity,
    Shifts,
    LegalPerson,
    Contact,
    RetailerShareHolder,
    Ceo
  },
  provide() {
    return {
      $vRecipient: this.$v.recipient,
      $vLegalPerson: this.$v.recipient.legalPerson,
      $vCeo: this.$v.recipient.legalPerson.ceo,
      $vContact: this.$v.recipient.legalPerson.contact
    };
  },
  validations: function () {
    return {
      recipient: {
        gln: {
          required,
          minLength: minLength(ValidationLength.GLN.MIN),
          maxLength: maxLength(ValidationLength.GLN.MAX),
          glnStructure: (value) => {
            return true;
            if (!value)
              return false;
            const checksum = (value.charCodeAt(12) - 48);
            const invertedChecksum = checksum === 0 ? 0 : 10 - checksum;
            console.log(checksum);
            let sum = 0;
            for (let i = 0; i < 12; i++) {
              sum += (value.charCodeAt(i) - 48) * (i % 2 === 0 ? 1 : 3);
            }
            return sum % 10 === invertedChecksum;
          }
        },
        hixCode: {
          required,
          minLength: minLength(ValidationLength.HIX.MIN),
          maxLength: maxLength(ValidationLength.HIX.MAX)
        },
        operationTime: {
          required
        },
        legalPerson: {
          proprietorship: {
            required
          },
          nationalCode: {
            required,
            minLength: minLength(ValidationLength.LEGAL_NATIONALCODE.MIN),
            maxLength: maxLength(ValidationLength.LEGAL_NATIONALCODE.MAX)
          },
          officialName: {
            required,
            minLength: minLength(ValidationLength.INSTITUE_NAME.MIN),
            maxLength: maxLength(ValidationLength.INSTITUE_NAME.MAX)
          },
          ceo: {
            firstName: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? minLength(ValidationLength.FIRSTNAME.MIN) : false,
              maxLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? maxLength(ValidationLength.FIRSTNAME.MAX) : false
            },
            surname: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? minLength(ValidationLength.SURNAME.MIN) : false,
              maxLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? maxLength(ValidationLength.SURNAME.MAX) : false
            },
            nationalCode: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? minLength(ValidationLength.NATURAL_NATIONALCODE.MIN) : false,
              maxLength: this.recipient.legalPerson.proprietorship !== ProprietorshipType.PRIVATE_NATURAL_PERSON ? maxLength(ValidationLength.NATURAL_NATIONALCODE.MAX) : false
            }
          },
          contact: {
            provinceId: {
              required
            },
            countyId: {
              required
            },
            cityId: {
              required
            },
            address: {
              required,
              minLength: minLength(ValidationLength.ADDRESS.MIN),
              maxLength: maxLength(ValidationLength.ADDRESS.MAX)
            },
            postalCode: {
              required,
              minLength: minLength(ValidationLength.POSTALCODE.MIN),
              maxLength: maxLength(ValidationLength.POSTALCODE.MAX)
            },
            phone: {
              required,
              minLength: minLength(ValidationLength.PHONE.MIN),
              maxLength: maxLength(ValidationLength.PHONE.MAX)
            },
          },
          naturalShareHolder: {
            firstName: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: minLength(ValidationLength.FIRSTNAME.MIN),
              maxLength: maxLength(ValidationLength.FIRSTNAME.MAX)
            },
            surname: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: minLength(ValidationLength.SURNAME.MIN),
              maxLength: maxLength(ValidationLength.SURNAME.MAX)
            },
            nationalCode: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_NATURAL_PERSON;
              }),
              minLength: minLength(ValidationLength.NATURAL_NATIONALCODE.MIN),
              maxLength: maxLength(ValidationLength.NATURAL_NATIONALCODE.MAX)
            }
          },
          legalShareHolder: {
            officialName: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_LEGAL_PERSON || this.recipient.legalPerson.proprietorship === ProprietorshipType.PUBLIC;
              }),
              minLength: minLength(ValidationLength.INSTITUE_NAME.MIN),
              maxLength: maxLength(ValidationLength.INSTITUE_NAME.MAX)
            },
            nationalCode: {
              required: requiredIf(function () {
                return this.recipient.legalPerson.proprietorship === ProprietorshipType.PRIVATE_LEGAL_PERSON || this.recipient.legalPerson.proprietorship === ProprietorshipType.PUBLIC;
              }),
              minLength: minLength(ValidationLength.LEGAL_NATIONALCODE.MIN),
              maxLength: maxLength(ValidationLength.LEGAL_NATIONALCODE.MAX)
            }
          }
        }
      }
    };
  }
};
