<template>
  <div>
    <v-icon
      size="80"
      color="green darken-2"
    >mdi-checkbox-marked-circle-outline</v-icon>
    <p> <span>{{registrant.firstName + ' ' + registrant.surname}}</span>&nbsp;{{$t('views.registrant.registrationFinished')}}</p>
    <v-btn
      color="primary"
      :to="recipientRegistrationUrl"
    >{{$t('buttons.start')}}</v-btn>
  </div>
</template>

<script>
import {
  required
} from "vuelidate/lib/validators";

export default {
  name: "Finished",
  props: {
    registrant: { type: Object, required },
    recipientType: { type: String, required },
  },
  computed: {
    recipientRegistrationUrl: {
      get: function () {
        return "/" + this.$root.getLocale() + "/register/" + this.recipientType + "/" + this.registrant.id;
      }
    }
  }
};
</script>