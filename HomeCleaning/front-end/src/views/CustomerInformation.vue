<template>
  <v-container>
    <form-group :name="$t('labels.firstName')">
      <v-text-field
        v-model.trim="localstate.firstName"
        slot-scope="{ attrs }"
        v-bind="attrs"
        :label="$t('labels.firstName')"
        required
        outlined
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.lastName')">
      <v-text-field
        v-model.trim="localstate.lastName"
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        :label="$t('labels.lastName')"
        required
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.cellphone')">
      <v-text-field
        v-model="localstate.cellphone"
        slot-scope="{ attrs }"
        v-bind="attrs"
        :label="$t('labels.cellphone')"
        required
        outlined
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.address')">
      <v-textarea
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model.trim="localstate.address"
        :label="$t('labels.address')"
        required
        :disabled="disabled"
      ></v-textarea>
    </form-group>
    <!-- <form-group :name="$t('labels.email')">
      <v-text-field
        type="email"
        v-model.trim="localstate.email"
        :label="$t('labels.email')"
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        required
        :disabled="disabled"
      ></v-text-field> 
    </form-group>-->
  </v-container>
</template>
<script>
import {
  required,
  minLength,
  maxLength,
  email,
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";
import { async } from "q";

export default {
  components: {},
  props: {
    customer: { type: Object, required },
    disabled: { type: Boolean },
  },
  data: function () {
    return {
    };
  },
  methods: {},

  directives: {
    mask,
  },
  computed: {
    localstate: {
      get: function () {
        return this.customer;
      },
      set: function (newValue) {
        this.$emit("update:customer", newValue);
      },
    },
  },

  async mounted() {},
  validations: {
    localstate: {
      firstName: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15),
      },
      lastName: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15),
      },
      // email: {
      //   required,
      //   email,
      //   maxLength: maxLength(50),
      // },
      cellphone: {
        required,
        minLength: minLength(13),
        maxLength: maxLength(13),
      },
    },
  },
};
</script>
