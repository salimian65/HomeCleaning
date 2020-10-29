<template>
  <div class="server">
    <v-container style="padding: 150px 0 0 0">
      <v-form class="dashboardMainContent">
        <v-card class="mx-auto my-12" max-width="550">
          <v-card-title large> Enroll Yourself</v-card-title>
          <v-divider class="mx-4"></v-divider>
          <!-- <v-card-actions> -->
          <v-row>
            <v-col cols="12">
              <Registrant
                v-bind:registrant.sync="serverRegistrant.registrant"
                :register="register"
                :disabled="false"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-btn
                style="padding: 5px"
                color="primary"
                v-if="!isNotEditable"
                @click="update()"
                x-large
                block
                >{{ $t("buttons.submit") }}</v-btn
              >
            </v-col>
          </v-row>
          <!-- </v-card-actions> -->
        </v-card>
      </v-form>
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
    cartable: Number,
  },
  components: {
    Registrant,
  },
  data: function () {
    return {
      isNotEditable: false,
      password: null,
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
      serverRegistrant: {
        registrant: {
          firstName: "",
          surname: "",
          email: "",
          cellphone: "",
          password: "",
          password2: "",
        },
      },
    };
  },
  methods: {
    submit() {},
    register: function () {},
    update: async function () {
      this.$v.$touch();
      if (this.$v.$error) {
        this.goToInvalidInput();
        return false;
      }

      await userApi.add(this.recipient);
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
          surname: {
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
  padding-top: calc(6rem + 74px);
  padding-bottom: 6rem;
  background: url(../assets/img/pro-onboarding-img-1.jpg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
}
</style>