<template>
  <div>
    <label>سهامدار</label>
    <form-group
      :validator="$v.proprietorship"
      :name="$t('labels.proprietorship')"
    >
      <v-select
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model="localstate.proprietorship"
        :items="proprietorshipTypes"
        item-text="label"
        item-value="value"
        :label="$t('labels.proprietorship')"
      ></v-select>
    </form-group>
  </div>
</template>

<script>
import {
  required
} from "vuelidate/lib/validators";
import { ProprietorshipType } from "../../../constants";

export default {
  name: "ShareHolder",
  props: {
    legalPerson: { type: Object, required },
  },
  inject: { $v: "$vLegalPerson" },
  data () {
    return {
      ProprietorshipType: ProprietorshipType,
      proprietorshipTypes: [
        {
          label: "خصوصی-شخص حقوقی",
          value: ProprietorshipType.PRIVATE_LEGAL_PERSON
        },
        {
          label: "دولتی",
          value: ProprietorshipType.PUBLIC
        }
      ]
    };
  },
  computed: {
    localstate: {
      get: function () {
        return this.legalPerson;
      },
      set: function (newValue) {
        this.$emit("update:legalPerson", newValue);
      }
    },
  }
};
</script>
