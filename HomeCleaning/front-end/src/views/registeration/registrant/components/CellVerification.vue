<template>
  <v-container>
    <v-row>
      <v-text-field
        v-model="localstate.cellphone"
        :label="$t('labels.cellphone')"
        dir="ltr"
        readonly
        outlined
      ></v-text-field>
    </v-row>
    <v-row>
      <i18n
        path="views.registrant.changeCellphone"
        tag="label"
      >
        <a
          color="primary"
          @click="changeInfo"
          place="action"
        >{{$t('buttons.click')}}</a></i18n>
    </v-row>
    <v-row>
      <countdown
        :left-time="90000"
        ref="countdown"
      >
        <template
          slot="process"
          slot-scope="{ timeObj }"
        >
          <span>{{$t('views.registrant.resendVerificationCode')}}‍‍{{`: ${timeObj.m}:${timeObj.s}` }} {{$t('labels.seconds')}}</span>
        </template>
        <template slot="finish">
          <v-btn
            color="primary"
            @click="tryResendVerificationCode"
          >{{$t('views.registrant.sendVerificationCode')}}</v-btn>
        </template>
      </countdown>
    </v-row>
    <v-row>
      <label>{{$t('views.registrant.enterVerificationCode')}}</label>
    </v-row>
    <v-form @submit.prevent="tryVerifyCellphone">
      <form-group :validator="$v.localstate.verificationCode">
        <v-text-field
          v-model="localstate.verificationCode"
          :label="$t('views.registrant.verificationCode')"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :counter="$v.localstate.verificationCode.$params.maxLength.max"
          required
          dir="ltr"
          outlined
          v-mask="'######'"
        ></v-text-field>
      </form-group>
      <v-btn
        type="submit"
        color="primary"
      >{{$t('buttons.register')}}</v-btn>
    </v-form>
  </v-container>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";
import vueAwesomeCountdown from "vue-awesome-countdown";

export default {
  name: "CellVerification",
  props: {
    registrant: { type: Object, required },
    changeInfo: { type: Function, required },
    resendVerificationCode: { type: Function, required },
    verifyCellphone: { type: Function, required },
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
    tryVerifyCellphone: function () {
      this.$v.localstate.$touch();
      if (this.$v.$error) return false;
      this.verifyCellphone();
      return true;
    },
    tryResendVerificationCode: async function () {
      await this.resendVerificationCode();
      debugger;
      this.$refs.countdown.startCountdown(true);
    }
  },
  validations: {
    localstate: {
      verificationCode: {
        required,
        minLength: minLength(6),
        maxLength: maxLength(6)
      }
    }
  }
};
</script>
