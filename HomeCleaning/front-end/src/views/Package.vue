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
          v-model="order.cleaningSpaceSizeSelected"
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
              <v-card :loading="loading" class="mx-auto my-12" max-width="374">
                <template slot="progress">
                  <v-progress-linear
                    color="deep-purple"
                    height="10"
                    indeterminate
                  ></v-progress-linear>
                </template>
                <v-img height="250" src="../assets/img/package.jpg"></v-img>
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
        <h3>Address</h3>
        <customer-information
          v-bind:customer.sync="order.customer"
          :disabled="false"
        />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <v-btn
          large
          color="primary"
          block
          v-if="isNotEditable"
          @click="submit()"
          >{{ $t("buttons.submit") }}</v-btn
        >
      </v-col>
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
          lastName: "",
          cellphone: "",
          address:""
        },
        cleaningCategorySelected: "",
        cleaningSpaceSizeSelected: "",
        cleaningPackageSelected: "",
        cleaningExtraOptionSelected: [],
      },
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
      this.order.cleaningCategorySelected = this.cleaningCategories[0];
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