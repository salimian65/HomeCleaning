<template>
  <div class="server">
    <v-container style="padding: 150px 0 0 0">
      <v-row>
        <v-col cols="1"> </v-col>
        <v-col cols="5">
          <v-card class="mx-auto my-12">
            <v-card-title large> Sign up free</v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-actions>
              <v-btn
                class="signbtn"
                color="primary"
                @click="signIn()"
                x-large
                block
                elevation="2"
                >{{ $t("buttons.signin") }}</v-btn
              >
            </v-card-actions>
            <v-card-actions>
              <v-btn
                class="signbtn"
                color="primary"
                @click="isSignUp = true"
                x-large
                block
                elevation="2"
                >{{ $t("buttons.signup") }}</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-col>

        <v-col cols="5">
          <v-form v-if="isSignUp">
            <v-card class="mx-auto my-12">
              <v-card-title large> Sign up form</v-card-title>
              <v-divider class="mx-4"></v-divider>
             <v-card-actions>
                  <Registrant
                    v-bind:registrant.sync="serverRegistrant.registrant"
                    :register="register"
                    :disabled="false"
                  />
               </v-card-actions>
              <v-card-actions>
                  <v-btn
                    style="padding: 5px"
                    color="primary"
                    v-if="!isNotEditable"
                    @click="update()"
                    x-large
                    block
                    >{{ $t("buttons.submit") }}</v-btn
                  >
               
              </v-card-actions>
            </v-card>
          </v-form>
        </v-col>
        <v-col cols="1"> </v-col>
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
  email,
} from "vuelidate/lib/validators";
import { ValidationLength, ProprietorshipType } from "../constants";
import Registrant from "../components/Registrant";
import ScrollToInvalidInput from "../components/ScrollToInvalidInput";
import userApi from "../api/userApi";

export default {
  mixins: [ScrollToInvalidInput],
  props: {
    returnPath: String,
  },
  components: {
    Registrant,
  },
  data: function () {
    return {
      isNotEditable: false,
      isSignUp: false,
      password: null,
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
      serverRegistrant: {
        registrant: {
          firstName: "",
          lastName: "",
          email: "",
          cellphone: "",
          password: "",
          password2: "",
        },
      },
    };
  },
  methods: {
    async signIn() {
      await this.$root.signIn(this.returnPath);
    },
    register: function () {},
    update: async function () {
      this.$v.$touch();
      if (this.$v.$error) {
        this.goToInvalidInput();
        return false;
      }

      await userApi.createCustomer(this.serverRegistrant.registrant);
      this.$root.showSuccessToast(this.$t("labels.success"));
      this.isNotEditable = true;
    },
  },

  async mounted() {},

  provide() {
    return {
      $vRegistrant: this.$v.serverRegistrant.registrant,
    };
  },

  validations: function () {
    return {
      serverRegistrant: {
        registrant: {
          firstName: {
            required,
            minLength: minLength(3),
            maxLength: maxLength(15),
          },
          lastName: {
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
  background: url(../assets/img/10.jpg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
}
</style>