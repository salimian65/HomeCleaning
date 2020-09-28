<template>
  <div>
    <v-icon
      size="80"
      color="green darken-2"
    >mdi-checkbox-marked-circle-outline</v-icon>
    <p> </p>
    <v-btn
      color="primary"
      :to="recipientRegistrationUrl"
    >{{$t('buttons.start')}}</v-btn>
  </div>
</template>

<script>
// import {
//   required
// } from "vuelidate/lib/validators";

export default {
  name: "RegistrationDone",
  // props: {
  //   registrant: { type: Object, required },
  //   recipientType: { type: String, required },
  // },
  // computed: {
  //   recipientRegistrationUrl: {
  //     get: function () {
  //       return "/register/" + this.recipientType + "/" + this.registrant.id;
  //     }
  //   }
  // }
};
</script>