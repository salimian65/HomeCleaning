<template>
  <div class="server">
    <v-container style="padding: 150px 0 0 0">
      <h1>Make Your Request</h1>
      <v-row>
        <v-col cols="6"> </v-col>
      </v-row>

      <v-row>
        <v-btn large color="primary" @click="submit()">{{
          $t("buttons.submit")
        }}</v-btn>
      </v-row>
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