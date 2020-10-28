<template>
 <v-container style="padding: 150px 0 0 0">
    <h1>Make Your Request</h1>
    <v-row>
      <v-col cols="6">
        <h3>Cleaning Category</h3>
        <ul class="category">
          <li v-for="item in cleaningCategories" :key="item.id">
            {{ item.name }}
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Space Size</h3>
        <v-select
          v-model="order.spaceSizeSelected"
          :items="spaceSizes"
          item-text="name"
          item-value="Id"
          label="Select"
          persistent-hint
          return-object
          filled
        ></v-select>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <h3>Cleaning Package</h3>
        <ul class="packages">
          <li v-for="item in cleaningPackages" :key="item.id">
            <label :for="`cleaningPackage` + item.id">
              <!-- <v-card>
                <v-card-title class="headline grey lighten-2">
                  {{ item.name }}
                </v-card-title>

                <v-card-text> </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <input
                    type="radio"
                    :id="`cleaningPackage` + item.id"
                    :value="item"
                    name="cleaningPackage"
                    v-model="cleaningPackageSelected"
                  />
                </v-card-actions>
              </v-card> -->

              <v-card :loading="loading" class="mx-auto my-12" max-width="374">
                <template slot="progress">
                  <v-progress-linear
                    color="deep-purple"
                    height="10"
                    indeterminate
                  ></v-progress-linear>
                </template>

                <v-img
                  height="250"
                  src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
                ></v-img>

                <v-card-title> {{ item.name }}</v-card-title>

                <v-card-text>
                  <v-row align="center" class="mx-0">
                    <v-rating
                      :value="4.5"
                      color="amber"
                      dense
                      half-increments
                      readonly
                      size="14"
                    ></v-rating>

                    <div class="grey--text ml-4">4.5 (413)</div>
                  </v-row>

                  <div class="my-4 subtitle-1">$ • Customer Stories</div>

                  <div>
                    The jobs for which you choose offers from Armut are under
                    our guarantee so that you can receive service with peace of
                    mind.
                  </div>
                </v-card-text>

                <v-divider class="mx-4"></v-divider>

                <v-card-title>todays's availability</v-card-title>

                <v-card-text>
                  <v-chip-group
                    v-model="selection"
                    active-class="deep-purple accent-4 white--text"
                    column
                  >
                    <v-chip>5:30PM</v-chip>
                    <v-chip>7:30PM</v-chip>
                    <v-chip>8:00PM</v-chip>
                    <v-chip>9:00PM</v-chip>
                  </v-chip-group>
                </v-card-text>

                <v-card-actions>
                  <input
                    type="radio"
                    :id="`cleaningPackage` + item.id"
                    :value="item"
                    name="cleaningPackage"
                    v-model="order.cleaningPackageSelected"
                  />
                </v-card-actions>
              </v-card>
            </label>
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Extra Options</h3>
        <ul class="extra">
          <li v-for="item in cleaningExtraOptions" :key="item.id">
            <input
              type="checkbox"
              :id="`cleaningExtraOption` + item.id"
              :value="item"
              name="cleaningExtraOption"
              v-model="order.cleaningExtraOptionSelected"
            />
            <label :for="`cleaningExtraOption` + item.id">
              {{ item.name }}</label
            >
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Extra Options</h3>
        <customer-information v-bind:customer.sync="order.customer" 
         :disabled="false"/>
      </v-col>
    </v-row>
    <v-row>
      <v-btn large color="primary" v-if="isNotEditable" @click="submit()">{{
        $t("buttons.submit")
      }}</v-btn>

      <!-- <v-dialog v-model="dialog" width="500">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="primary"
            v-if="isNotEditable"
            @click="submit()"
            v-bind="attrs"
            v-on="on"
            >{{ $t("buttons.submit") }}</v-btn
          >
        </template>
        <v-card>
          <v-card-title class="headline grey lighten-2">
            Privacy Policy
          </v-card-title>

          <v-card-text>
            <login-modal> </login-modal>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="dialog = false">
              I accept
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog> -->
    </v-row>
   </v-container>
</template>
<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf,
} from "vuelidate/lib/validators";
import { ValidationLength, ProprietorshipType } from "../constants";

import cleaningCategoryApi from "../api/cleaningCategoryApi";
import spaceSizeApi from "../api/spaceSizeApi";
import cleaningPackageApi from "../api/cleaningPackageApi";
import cleaningExtraOptionApi from "../api/cleaningExtraOptionApi";
import orderApi from "../api/orderApi";
import orderModel from "../models/orderModel";
// import loginModal from "../views/Login";
import CustomerInformation from "./CustomerInformation";

export default {
  props: {
    cartable: Number,
  },
  components: {
    // loginModal,
    CustomerInformation,
  },
  data: function () {
    return {
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
      spaceSizes: [],
      cleaningPackages: [],
      cleaningExtraOptions: [],
      selection: [],

      isNotEditable: true,
      dialog: false,
      order: {
        customer: {
          firstName: "",
          surname: "",
          email: "",
          cellphone: "",
        },
        cleaningCategorySelected:"",
        spaceSizeSelected: "",
        cleaningPackageSelected: "",
        cleaningExtraOptionSelected: [],
      },
      emailRules: [
        (v) => !!v || "E-mail is required",
        (v) => /.+@.+/.test(v) || "E-mail must be valid",
      ],
    };
  },
  methods: {
    async getAllCleaningCategory() {
      this.loading = true;
      var response = await cleaningCategoryApi.getAll();
      this.cleaningCategories = response.data.filter(
        (a) => a.id == this.categoryId
      );
      this.loading = false;
    },

    async getSpaceSizesByCategoryId() {
      this.loading = true;
      var response = await spaceSizeApi.getByCategoryId(this.categoryId);
      this.spaceSizes = response.data;
      this.loading = false;
    },

    async getCleaningPackagesByCategoryId() {
      this.loading = true;
      var response = await cleaningPackageApi.getByCategoryId(this.categoryId);
      this.cleaningPackages = response.data;
      this.loading = false;
    },

    async getCleaningExtraOptionByCategoryId() {
      this.loading = true;
      var response = await cleaningExtraOptionApi.getByCategoryId(
        this.categoryId
      );
      this.cleaningExtraOptions = response.data;
      this.loading = false;
    },

    submit() {
      this.order.cleaningCategorySelected=this.categoryId;
      localStorage.setItem("order", JSON.stringify(this.order));
      this.$router.push({
        name: "orderRequest",
        params: { order: this.order },
      });

      //  await orderApi.add(orderInstance);
    },

    register: function () {},
  },

  async mounted() {
    await this.getAllCleaningCategory();
    await this.getSpaceSizesByCategoryId();
    await this.getCleaningPackagesByCategoryId();
    await this.getCleaningExtraOptionByCategoryId();
  },

  validations: function () {
    return {
      recipient: {
        gln: {
          required,
          minLength: minLength(ValidationLength.GLN.MIN),
          maxLength: maxLength(ValidationLength.GLN.MAX),
          glnStructure: (value) => {
            if (!value) {
              return false;
            }
            const checksum = value.charCodeAt(12) - 48;
            const invertedChecksum = checksum === 0 ? 0 : 10 - checksum;
            console.log(checksum);
            let sum = 0;
            for (let i = 0; i < 12; i++) {
              sum += (value.charCodeAt(i) - 48) * (i % 2 === 0 ? 1 : 3);
            }
            return sum % 10 === invertedChecksum;
          },
        },
        hixCode: {
          required,
          minLength: minLength(ValidationLength.HIX.MIN),
          maxLength: maxLength(ValidationLength.HIX.MAX),
        },
        operationTime: {
          required,
        },
        legalPerson: {
          proprietorship: {
            required,
          },
          nationalCode: {
            required,
            minLength: minLength(ValidationLength.LEGAL_NATIONALCODE.MIN),
            maxLength: maxLength(ValidationLength.LEGAL_NATIONALCODE.MAX),
            retailerIsExist: (value) => {
              return !this.isExistRetailer;
            },
          },
          officialName: {
            required,
            minLength: minLength(ValidationLength.INSTITUE_NAME.MIN),
            maxLength: maxLength(ValidationLength.INSTITUE_NAME.MAX),
          },
          ceo: {
            firstName: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship !==
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? minLength(ValidationLength.FIRSTNAME.MIN)
                  : false,
              maxLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? maxLength(ValidationLength.FIRSTNAME.MAX)
                  : false,
            },
            surname: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship !==
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? minLength(ValidationLength.SURNAME.MIN)
                  : false,
              maxLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? maxLength(ValidationLength.SURNAME.MAX)
                  : false,
            },
            nationalCode: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship !==
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? minLength(ValidationLength.NATURAL_NATIONALCODE.MIN)
                  : false,
              maxLength:
                this.recipient.legalPerson.proprietorship !==
                ProprietorshipType.PRIVATE_NATURAL_PERSON
                  ? maxLength(ValidationLength.NATURAL_NATIONALCODE.MAX)
                  : false,
            },
          },
          contact: {
            provinceId: {
              required,
            },
            countyId: {
              required,
            },
            cityId: {
              required,
            },
            address: {
              required,
              minLength: minLength(ValidationLength.ADDRESS.MIN),
              maxLength: maxLength(ValidationLength.ADDRESS.MAX),
            },
            postalCode: {
              required,
              minLength: minLength(ValidationLength.POSTALCODE.MIN),
              maxLength: maxLength(ValidationLength.POSTALCODE.MAX),
            },
            phone: {
              required,
              minLength: minLength(ValidationLength.PHONE.MIN),
              maxLength: maxLength(ValidationLength.PHONE.MAX),
            },
          },
          naturalShareHolder: {
            firstName: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship ===
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength: minLength(ValidationLength.FIRSTNAME.MIN),
              maxLength: maxLength(ValidationLength.FIRSTNAME.MAX),
            },
            surname: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship ===
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength: minLength(ValidationLength.SURNAME.MIN),
              maxLength: maxLength(ValidationLength.SURNAME.MAX),
            },
            nationalCode: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship ===
                  ProprietorshipType.PRIVATE_NATURAL_PERSON
                );
              }),
              minLength: minLength(ValidationLength.NATURAL_NATIONALCODE.MIN),
              maxLength: maxLength(ValidationLength.NATURAL_NATIONALCODE.MAX),
            },
          },
          legalShareHolder: {
            officialName: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship ===
                    ProprietorshipType.PRIVATE_LEGAL_PERSON ||
                  this.recipient.legalPerson.proprietorship ===
                    ProprietorshipType.PUBLIC
                );
              }),
              minLength: minLength(ValidationLength.INSTITUE_NAME.MIN),
              maxLength: maxLength(ValidationLength.INSTITUE_NAME.MAX),
            },
            nationalCode: {
              required: requiredIf(function () {
                return (
                  this.recipient.legalPerson.proprietorship ===
                    ProprietorshipType.PRIVATE_LEGAL_PERSON ||
                  this.recipient.legalPerson.proprietorship ===
                    ProprietorshipType.PUBLIC
                );
              }),
              minLength: minLength(ValidationLength.LEGAL_NATIONALCODE.MIN),
              maxLength: maxLength(ValidationLength.LEGAL_NATIONALCODE.MAX),
            },
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
</style>