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
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.surname')">
      <v-text-field
        v-model.trim="localstate.surname"
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        :label="$t('labels.surname')"
        required
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
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.address')">
      <v-text-field
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model.trim="localstate.address"
        :label="$t('labels.address')"
        required
      ></v-text-field>
    </form-group>
    <form-group :name="$t('labels.email')">
      <v-text-field
        type="email"
        v-model.trim="localstate.email"
        :label="$t('labels.email')"
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        required
      ></v-text-field>
    </form-group>
  </v-container>
</template>
<script>
import cleaningCategoryApi from "../api/cleaningCategoryApi";
import spaceSizeApi from "../api/spaceSizeApi";
import cleaningPackageApi from "../api/cleaningPackageApi";
import cleaningExtraOptionApi from "../api/cleaningExtraOptionApi";
import orderApi from "../api/orderApi";
import orderModel from "../models/orderModel";
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
  },
  data: function () {
    return {
      categoryId: this.$route.params["categoryId"],
      // localstate: {
      //   firstName: "",
      //   surname: "",
      //   email: "",
      //   cellphone: "",
      // },
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
      surname: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15),
      },
      email: {
        required,
        email,
        maxLength: maxLength(50),
      },
      cellphone: {
        required,
        minLength: minLength(13),
        maxLength: maxLength(13),
      },
    },
  },
};
</script>
