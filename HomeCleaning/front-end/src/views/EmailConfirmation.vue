<template>
  <v-container style="height: 800px; padding: 150px 0 0 0">
    <h3>Email Confirmation</h3>
    <v-card>
      {{message}}
    </v-card>
  </v-container>
</template>
<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf,
  email,
} from "vuelidate/lib/validators";
import { ValidationLength, ProprietorshipType } from "../constants";
import ScrollToInvalidInput from "../components/ScrollToInvalidInput";
import emailApi from "../api/emailApi";

export default {
  mixins: [ScrollToInvalidInput],
  props: {
    token: String,
    email: String,
  },
  components: {},

  data() {
    return {
      message:""
    };
  },

  methods: {
    async confirmEmail() {
      this.loading = true;
      var response = (await emailApi.confirmEmail(
        this.$route.query.token,
        this.$route.query.email
      )).data;

      if (response.succeeded == true) {
        this.message="Your Email is confirmed ";
        this.$root.showSuccessToast(this.message);
      }
      else if(response.succeeded == false){
         this.message=response.errors[0].description;
         this.$root.showInfoToast(this.message);
      }

      this.loading = false;
    },
  },

  async mounted() {
    await this.confirmEmail();
  },
};
</script>
<style>
ul.packages > li {
  display: inline-block;
  zoom: 1;
  *display: inline;
  margin: 0px 30px;
}

ul.packages label:hover {
  cursor: pointer;
}
.server {
  padding-top: calc(6rem + 74px);
  padding-bottom: 6rem;
  background: url(../assets/img/pro-onboarding-img-1.jpg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
}
</style>