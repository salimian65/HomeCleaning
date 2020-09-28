<template>
  <v-container>
    <v-row>
      <v-text-field
        v-model="registrant.email"
        :label="$t('labels.email')"
        readonly
        outlined
      ></v-text-field>
    </v-row>
    <v-row>
      <i18n
        path="views.registrant.changeEmail"
        tag="label"
      >
        <a
          color="primary"
          @click="changeInfo"
          place="action"
        >{{$t('buttons.click')}}</a>
      </i18n>
    </v-row>
    <v-row>
      <label>{{$t('views.registrant.emailVerificationCodeInfo')}}</label>
    </v-row>
    <v-row>
      <countdown
        :left-time="120000"
        ref="countdown"
      >
        <template
          slot="process"
          slot-scope="{ timeObj }"
        >
          <span>{{$t('views.registrant.resendVerificationEmail')}}‍‍{{`: ${timeObj.m}:${timeObj.s}` }} {{$t('labels.seconds')}}</span>
        </template>
        <template slot="finish">
          <v-btn
            color="primary"
            @click="tryResendVerificationEmail"
          >{{$t('views.registrant.sendVerificationCode')}}</v-btn>
        </template>
      </countdown>
    </v-row>
  </v-container>
</template>

<script>
import {
  required
} from "vuelidate/lib/validators";

export default {
  name: "EmailVerification",
  props: {
    registrant: { type: Object, required },
    changeInfo: { type: Function, required },
    resendVerificationEmail: { type: Function, required },
  },
  methods: {
    tryResendVerificationEmail: async function () {
      await this.resendVerificationEmail();
      this.$refs.countdown.startCountdown(true);
    }
  }
};
</script>