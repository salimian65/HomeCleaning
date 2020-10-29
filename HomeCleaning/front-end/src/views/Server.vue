<template>
  <div class="server">
    <v-container style="padding: 150px 0 0 0">
      <v-row>
        <v-col cols="6"> </v-col>
        <v-col cols="6">
          <v-card class="mx-auto my-12" max-width="550">
            <!-- <template slot="progress">
              <v-progress-linear
                color="deep-purple"
                height="10"
                indeterminate
              ></v-progress-linear>
            </template> -->
            <v-card-title large> Reach more customers, earn more</v-card-title>
            <v-card-text>
              <div>
                EVERY MONTH, 150,000 CUSTOMERS ARE SEEKING A SERVICE FOR
                PROJECTS WORTH 300 MILLION TL
              </div>
            </v-card-text>
            <v-divider class="mx-4"></v-divider>
            <v-card-actions>
              <v-btn x-large block color="primary" :to="{name:`serverRegistration`}"
                >SIGN UP FOR FREE</v-btn
              >
            </v-card-actions>
            <v-card-text>
              Do you already have an account?
              <v-btn text @click="$root.signIn();"   Normal > Login! </v-btn>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row> </v-row>
    </v-container>
  </div>
</template>
<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf,
} from "vuelidate/lib/validators";
import { ValidationLength, ProprietorshipType } from "../constants";

export default {
  props: {
    cartable: Number,
  },
  components: {},
  data: function () {
    return {
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
    };
  },
  methods: {
    submit() {},
    register: function () {},
  },

  async mounted() {},
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