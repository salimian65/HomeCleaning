<template>
  <v-container>
    <form-group
      :validator="$v.firstName"
      :name="$t('labels.firstName')"
    >
      <v-text-field
        v-model.trim="localstate.firstName"
        slot-scope="{ attrs }"
        v-bind="attrs"
        :counter="$v.firstName.$params.maxLength.max"
        :label="$t('labels.firstName')"
        :maxlength="$v.firstName.$params.maxLength.max"
        required
        outlined
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :validator="$v.lastName" :name="$t('labels.lastName')">
      <v-text-field
        v-model.trim="localstate.lastName"
        :counter="$v.lastName.$params.maxLength.max"
        slot-scope="{ attrs }"
        v-bind="attrs"
        :maxLength="$v.lastName.$params.maxLength.max"
        outlined
        :label="$t('labels.lastName')"
        required
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.cellphone"
      :name="$t('labels.cellphone')"
    >
      <v-text-field
        v-model="localstate.cellphone"
        slot-scope="{ attrs }"
        v-bind="attrs"
        :label="$t('labels.cellphone')"
        :counter="  $v.cellphone.$params.maxLength.max"
        :maxlength="$v.cellphone.$params.maxLength.max"
        :minlength="$v.cellphone.$params.minLength.min"
        required
        outlined
        :hint="$t('localization.cellphoneSample')"
        v-mask="$t('localization.cellphoneMask')"
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :validator="$v.email" :name="$t('labels.email')">
      <v-text-field
        type="email"
        v-model.trim="localstate.email"
        :label="$t('labels.email')"
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        :counter="$v.email.$params.maxLength.max"
        :maxLength="$v.email.$params.maxLength.max"
        required
        :disabled="disabled"
      ></v-text-field>
    </form-group>
    <form-group :validator="$v.password" :name="$t('labels.password')" >
      <password v-model="localstate.password" placeholder="password" />
    </form-group>
    <form-group
      :validator="$v.password2"
      :name="$t('labels.password2')"
    >
      <v-text-field
        v-on:blur="validate"
        type="password"
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.password2"
        :label="$t('labels.confirmPasswrd')"
         outlined
      ></v-text-field>
    </form-group>
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
import Password from "vue-password-strength-meter";
export default {
  name: "Registrant",
  props: {
    registrant: { type: Object, required },
    register: { type: Function, required },
    disabled: { type: Boolean },
  },
  components: { Password },
  directives: {
    mask,
  },
  
  computed: {
    localstate: {
      get: function () {
        return this.registrant;
      },
      set: function (newValue) {
        this.$emit("update:registrant", newValue);
      },
    },
  },
  methods: {
    tryRegister: function () {
      this.$v.localstate.$touch();
      if (this.$v.$error) return false;
      this.register();
      return true;
    },
    validate: function () {
      console.log(this.localstate.password === this.localstate.password2);
    },
  },
  inject: { $v: "$vRegistrant" }
  
  
//   validations: {
//     localstate: {
//       firstName: {
//         required,
//         minLength: minLength(3),
//         maxLength: maxLength(15),
//       },
//       lastName: {
//         required,
//         minLength: minLength(3),
//         maxLength: maxLength(15),
//       },
//       email: {
//         required,
//         email,
//         maxLength: maxLength(50),
//       },
//       cellphone: {
//         required,
//         minLength: minLength(13),
//         maxLength: maxLength(13),
//       },
//     },
//   },
 };
</script>
