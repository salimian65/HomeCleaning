<template>
  <v-container>
    <v-form @submit.prevent="tryRegister">
      <form-group
        :validator="$v.localstate.cellphone"
        :name="$t('labels.cellphone')"
      >
        <v-text-field
          v-model="localstate.cellphone"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :label="$t('labels.cellphone')"
          :counter="$v.localstate.cellphone.$params.maxLength.max"
          dir="ltr"
          :maxlength="$v.localstate.cellphone.$params.maxLength.max"
          :minlength="$v.localstate.cellphone.$params.minLength.min"
          required
          outlined
          :hint="$t('localization.cellphoneSample')"
          v-mask="$t('localization.cellphoneMask')"
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.localstate.firstName"
        :name="$t('labels.firstName')"
      >
        <v-text-field
          v-model.trim="localstate.firstName"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :counter="$v.localstate.firstName.$params.maxLength.max"
          :label="$t('labels.firstName')"
          :maxlength="$v.localstate.firstName.$params.maxLength.max"
          required
          outlined
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.localstate.surname"
        :name="$t('labels.surname')"
      >
        <v-text-field
          v-model.trim="localstate.surname"
          :counter="$v.localstate.surname.$params.maxLength.max"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :maxLength="$v.localstate.surname.$params.maxLength.max"
          outlined
          :label="$t('labels.surname')"
          required
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.localstate.email"
        :name="$t('labels.email')"
      >
        <v-text-field
          type="email"
          v-model.trim="localstate.email"
          :label="$t('labels.email')"
          slot-scope="{ attrs }"
          v-bind="attrs"
          dir="ltr"
          outlined
          :counter="$v.localstate.email.$params.maxLength.max"
          :maxLength="$v.localstate.email.$params.maxLength.max"
          required
        ></v-text-field>
      </form-group>
      <div :class="`d-flex justify-end mb-6`">
        <v-btn
          type="submit"
          color="primary"
        >{{$t('buttons.register')}}</v-btn>
      </div>
    </v-form>
  </v-container>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  email
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";
import { async } from "q";

export default {
  name: "Registrant",
  props: {
    registrant: { type: Object, required },
    register: { type: Function, required },
  },
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
      }
    },
  },
  methods: {
    tryRegister: function () {
      this.$v.localstate.$touch();
      if (this.$v.$error) return false;
      this.register();
      return true;
    },
  },
  validations: {
    localstate: {
      firstName: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15)
      },
      surname: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15)
      },
      email: {
        required,
        email,
        maxLength: maxLength(50)
      },
      cellphone: {
        required,
        minLength: minLength(13),
        maxLength: maxLength(13)
      }
    }
  }
};
</script>
