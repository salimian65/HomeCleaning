<template>
  <div>
    <label>حوزه کاری</label>
    <form-group
      :validator="$v.operationArea"
      :name="$t('labels.operationArea')"
    >
      <v-select
        outlined
                      slot-scope="{ attrs }"
        v-bind="attrs"
  v-model="localstate.operationArea"
        :items="operationAreas"
        item-text="label"
        item-value="value"
        :label="$t('labels.operationArea')"
        required
      ></v-select>
    </form-group>
  </div>
</template>

<script>
import {
  required,
} from "vuelidate/lib/validators";
import {
  mask
} from "vue-the-mask";
import { OperationArea } from "../../../../constants";

export default {
  name: "OperationArea",
  props: {
    recipient: { type: Object, required },
  },
  data () {
    return {
      operationAreas: [
        {
          label: "استانی",
          value: OperationArea.PROVINCIAL
        },
        {
          label: "کشوری",
          value: OperationArea.COUNTRYWIDE
        }
      ],
    };
  },
  directives: {
    mask,
  },
  inject: { $v: "$vRecipient" },
  computed: {
    localstate: {
      get: function () {
        return this.recipient;
      },
      set: function (newValue) {
        this.$emit("update:recipient", newValue);
      }
    },
  }
};
</script>
