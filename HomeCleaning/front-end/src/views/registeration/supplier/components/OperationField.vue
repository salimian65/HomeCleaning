<template>
  <div>
    <label>حوزه کاری</label>
    <form-group
      :validator="$v.operationField"
      :name="$t('labels.operationField')"
    >
      <v-select
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.operationField"
        :items="operationFields"
        item-text="label"
        item-value="value"
        :label="$t('labels.operationField')"
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
import { OperationField } from "../../../../constants";

export default {
  name: "OperationField",
  props: {
    recipient: { type: Object, required },
  },
  data () {
    return {
      operationFields: [
        {
          label: "تولید",
          value: OperationField.MANUFACTURING
        },
        {
          label: "واردات",
          value: OperationField.IMPORT
        },
        {
          label: "تولید و واردات",
          value: OperationField.MANUFACTURING_IMPORT
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
