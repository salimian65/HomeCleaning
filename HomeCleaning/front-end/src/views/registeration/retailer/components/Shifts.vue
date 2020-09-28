<template>
  <div>
    <label>سهامدار</label>
    <form-group
      :validator="$v.operationTime"
      :name="$t('labels.operationTime')"
    >
      <v-select
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.operationTime"
        :items="operationTimes"
        item-text="label"
        item-value="value"
        :label="$t('labels.operationTime')"
        required
      ></v-select>
    </form-group>
  </div>
</template>

<script>
import {
  required,
} from "vuelidate/lib/validators";
import { OperationTime } from "../../../../constants";

export default {
  name: "Shifts",
  props: {
    recipient: { type: Object, required }
  },
  data () {
    return {
      operationTimes: [
        {
          label: "روزانه",
          value: OperationTime.DAILY
        },
        {
          label: "شبانه‌‌روزی",
          value: OperationTime.TwentyFourSeven
        }
      ],
    };
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
