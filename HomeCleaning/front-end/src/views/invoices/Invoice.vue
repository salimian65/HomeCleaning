//TODO: Invoice Internal refrence, Payment type
<template>
  <div>
    <v-form @submit.prevent="submitInvoice">
      <v-btn
        v-if="viewState==='edit'"
        @click="editHeader=true"
      >edit</v-btn>
      <v-btn
        v-if="viewState==='editing'"
        @click="editHeader=false"
      >cancel</v-btn>
      <v-btn
        type="submit"
        color="primary"
        v-if="viewState==='editing' || viewState==='new'"
      >{{$t('buttons.register')}}</v-btn>
      <label>اطلاعات موسسه</label>
      <v-row>
        <v-col>
          <form-group
            :validator="$v.invoice.buyer.id"
            :name="$t('labels.officialName')"
          >
            <v-text-field
              slot-scope="{ attrs }"
              v-bind="attrs"
              dense
              readonly
              outlined
              v-model="invoice.buyer.officialName"
              :label="$t('labels.officialName')"
              required
            ></v-text-field>
          </form-group>
        </v-col>
        <v-col>
          <choose-buyer
            :buyer.sync="invoice.buyer"
            :disabled="!editHeader"
          ></choose-buyer>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <form-group
            :validator="$v.invoice.issueDate"
            :name="$t('labels.issueDate')"
          >
            <!-- slot-scope="{ attrs }"
              v-bind="attrs" -->
            <v-text-field
              id="issue-date"
              :disabled="!editHeader"
              outlined
              dense
              v-model="invoice.issueDate"
              :label="$t('labels.issueDate')"
              required
              v-mask="'####-##-##'"
              append-outer-icon="mdi-calendar"
              @click:append-outer="showIssueDate=true"
            ></v-text-field>
            <vue-persian-datetime-picker
              v-model="invoice.issueDate"
              element="issue-date"
              dense
              :editable="true"
              :show="showIssueDate"
              @close="showIssueDate=false"
              format="jYYYY-jMM-jDD"
            >
            </vue-persian-datetime-picker>
          </form-group>
          <form-group
            :validator="$v.invoice.dueDate"
            :name="$t('labels.dueDate')"
          >
            <!-- slot-scope="{ attrs }"
              v-bind="attrs" -->
            <v-text-field
              id="due-date"
              type="text"
              outlined
              :disabled="!editHeader"
              dense
              v-model="invoice.dueDate"
              :label="$t('labels.dueDate')"
              v-mask="'####-##-##'"
              required
              append-outer-icon="mdi-calendar"
              @click:append-outer="showDueDate=true"
            ></v-text-field>
            <vue-persian-datetime-picker
              v-model="invoice.dueDate"
              element="due-date"
              :editable="true"
              :show="showDueDate"
              @close="showDueDate=false"
              format="jYYYY-jMM-jDD"
            >
            </vue-persian-datetime-picker>
          </form-group>
          <form-group
            :validator="$v.invoice.totalPrice"
            :name="$t('labels.totalPrice')"
          >
            <v-text-field
              slot-scope="{ attrs }"
              v-bind="attrs"
              v-model="invoice.totalPrice"
              dense
              :disabled="!editHeader"
              outlined
              :label="$t('labels.totalPrice')"
              required
            >
            </v-text-field>
          </form-group>
          <payment-methods
            :invoice="invoice"
            :disabled="!editHeader"
          ></payment-methods>
          <form-group
            :validator="$v.invoice.buyer.validationGroup"
            :name="$t('labels.officialName')"
          >
            <v-textarea
              dense
              filled
              :disabled="!editHeader"
              :label="$t('labels.description')"
              auto-grow
              v-model="invoice.description"
            ></v-textarea>
          </form-group>
        </v-col>
      </v-row>
    </v-form>
    <v-row>
      <new-line-item
        :lineItem.sync="lineItem"
        :addLineItem="addLineItem"
        :edit-mode="viewState==='edit'"
      ></new-line-item>
    </v-row>
    <v-row>
      <v-col>
        <line-items :lineItems.sync="invoice.lineItems"></line-items>
      </v-col>
    </v-row>

  </div>
</template>

<script>
import {
  ValidationLength,
  ProprietorshipType
} from "../../constants";
import {
  required,
  minLength,
  maxLength,
  requiredIf
} from "vuelidate/lib/validators";

import ScrollToInvalidInput from "../../components/ScrollToInvalidInput";
import recipientApi from "../../api/recipient";
import invoiceApi from "../../api/invoice";
import ChooseBuyer from "./components/ChooseBuyer";
import NewLineItem from "./components/NewLineItem";
import PaymentMethods from "./components/PaymentMethods";
import LineItems from "./components/LineItems";
import { PaymentMethod } from "../../constants";
import VuePersianDatetimePicker from "vue-persian-datetime-picker";
import { EmptyGuid, ApiResultStatus } from "../../constants";
import { mask } from "vue-the-mask";

export default {
  name: "Invoice",
  mixins: [ScrollToInvalidInput],
  props: {
    operation: { String, default: "view" }
  },
  data () {
    return {
      showIssueDate: false,
      showDueDate: false,
      invoice: {
        id: EmptyGuid,
        buyer: { id: 0, officialName: "" },
        dueDate: null,
        issueDate: null,
        totalPrice: null,
        lineItems: [],
        description: "",
        paymentMethod: null,
      },
      lineItem: {
        irc: "",
        brandEnglish: "",
        brandPersian: "",
        licenseOwner: "",
        quantity: 0
      },
      loading: false,
      search: null,
      editHeader: false
    };
  },
  computed: {
    viewState () {
      if (this.operation.toLowerCase() === "new")
        return "new";
      else if (this.operation.toLowerCase() === "edit" && !this.editHeader)
        return "edit";
      else if (this.operation.toLowerCase() === "edit" && this.editHeader)
        return "editing";
      else
        return "view";
    }
  },
  mounted: function () {
    if (this.operation === "new")
      this.editHeader = true;
  },
  methods: {
    submitInvoice: async function () {
      debugger;
      this.$v.$touch();
      if (this.$v.$error) {
        //this.goToInvalidInput();
        return false;
      }
      //if (this.invoice.id === EmptyGuid) {
      if (this.viewState === "new") {
        debugger;
        this.invoice.id = await invoiceApi.registerInvoice(this.invoice);
        this.$router.replace({ name: "Registration‌‌Done", params: { operation: "edit", id: "" } });
        debugger;
      }
      //    await retailerApi.register(this.recipient);
      //  router.push({ name: "Registration‌‌Done" });
    },
    addLineItem: async function (newLineItem) {
      if (this.invoice.lineItems.find(item => item.irc === newLineItem.irc)) {
        this.$root.alert("تکراری");
        return;
      } else {
        this.invoice.lineItems.push(newLineItem);
        debugger;
        if (this.invoice.id === EmptyGuid) {
          this.invoice.id = await invoiceApi.registerInvoice(this.invoice);
          debugger;
        }
        else {
          newLineItem.invoiceId = this.invoice.id;
          newLineItem.id = await invoiceApi.addLineItem(newLineItem);
          debugger;
        }
      }
    },
  },
  provide () {
    return { $vInvoice: this.$v.invoice, };
  },
  validations: {
    invoice: {
      buyer: {
        id: {
          required: (value) => { return value > 0; }
        }/*,
        officialName: {},
        validationGroup: ["id", "officialName"]*/
      },
      issueDate: { required },
      dueDate: { required },
      totalPrice: { required: (value) => { return value > 0; } },
      paymentMethod: { required }
    }
  },
  components: {
    ChooseBuyer, LineItems, NewLineItem, PaymentMethods, VuePersianDatetimePicker
  },
  directives: {
    mask,
  }
};
</script>