<template>
  <div>
    <div>
      <form-group
        :validator="$v.hixCode"
        :name="$t('labels.hixCode')"
      >
        <v-text-field
          outlined
          v-model.trim="localstate.hixCode"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :counter="$v.hixCode.$params.maxLength.max"
          :minlength="$v.hixCode.$params.minLength.min"
          :maxlength="$v.hixCode.$params.maxLength.max"
          dir="ltr"
          v-mask="'##########'"
          :label="$t('labels.hixCode')"
          required
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.gln"
        :name="$t('labels.gln')"
      >
        <v-text-field
          outlined
          slot-scope="{ attrs }"
          v-bind="attrs"
          v-model.trim="localstate.gln"
          :counter="$v.gln.$params.maxLength.max"
          :minlength="$v.gln.$params.minLength.min"
          :maxlength="$v.gln.$params.maxLength.max"
          v-mask="'#############'"
          :label="$t('labels.gln')"
          required
          dir="ltr"
        ></v-text-field>
      </form-group>
    </div>
  </div>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";

export default {
  name: "RecipientIdentity",
  props: {
    recipient: { type: Object, required },
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
