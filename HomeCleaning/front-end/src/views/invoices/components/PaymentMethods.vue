<template>
  <div>
    <label>PaymentMethods</label>
    <form-group
      :validator="$v.paymentMethod"
      :name="$t('labels.paymentMethod')"
    >
      <v-select
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.paymentMethod"
        :items="paymentMethods"
        item-text="label"
        item-value="value"
        :label="$t('labels.paymentMethod')"
        :disabled="disabled"
        required
        dense
      ></v-select>
    </form-group>
  </div>
</template>

<script>
import {
  required,
} from "vuelidate/lib/validators";
import { PaymentMethod } from "../../../constants";

export default {
  name: "PaymentMethods",
  props: {
    invoice: { type: Object, required },
    disabled: { Boolean, default: false }
  },
  data () {
    return {
      paymentMethods: [
        {
          label: "چک",
          value: PaymentMethod.CHEQUE
        },
        {
          label: "نقد",
          value: PaymentMethod.CASH
        }
      ],
    };
  },
  inject: { $v: "$vInvoice" },
  computed: {
    localstate: {
      get: function () {
        return this.invoice;
      },
      set: function (newValue) {
        this.$emit("update:invoice", newValue);
      }
    },
  }
};
</script>
