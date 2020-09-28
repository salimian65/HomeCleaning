import {
  required,
  minLength,
  maxLength
} from "vuelidate/lib/validators";

import Contact from "../../../components/Contact";
// import NaturalPerson from "../components/NaturalPerson";
import LegalPerson from "../components/LegalPerson";
import OperationArea from "./components/OperationArea";
import ShareHolder from "../components/ShareHolder";
import Ceo from "../components/Ceo";
import distributorApi from "../../../api/distributor";

export default {
  props: {
    registrantId: {
      type: String,
      required
    },
  },
  data() {
    return {
      recipient: {
        operationArea: null,
        registrant: {
          id: this.registrantId
        },
        legalPerson: {
          nationalCode: "",
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
        },
      }
    };
  },
  methods: {
    register: async function () {
      this.$v.$touch();
      debugger;
      if (this.$v.$error) return false;
      await distributorApi.register(this.recipient);
    }
  },
  components: {
    // NaturalPerson,
    OperationArea,
    LegalPerson,
    Contact,
    ShareHolder,
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
  validations: {
    recipient: {
      operationArea: {
        required
      },
      legalPerson: {
        proprietorship: {
          required
        },
        nationalCode: {
          required,
          minLength: minLength(11),
          maxLength: maxLength(11)
        },
        officialName: {
          required,
          minLength: minLength(3),
          maxLength: maxLength(50)
        },
        ceo: {
          firstName: {
            required,
            minLength: minLength(3),
            maxLength: maxLength(20)
          },
          surname: {
            required,
            minLength: minLength(3),
            maxLength: maxLength(20)
          },
          nationalCode: {
            required,
            minLength: minLength(10),
            maxLength: maxLength(10)
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
            minLength: minLength(3),
            maxLength: maxLength(50)
          },
          postalCode: {
            required,
            minLength: minLength(10),
            maxLength: maxLength(10)
          },
          phone: {
            required,
            minLength: minLength(13),
            maxLength: maxLength(13)
          }
        }
      }
    }
  }
};
