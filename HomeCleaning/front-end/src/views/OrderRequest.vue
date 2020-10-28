<template>
 <v-container style="padding: 150px 0 0 0">
    <h1>Confirm Your Request</h1>
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
          disabled
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
       {{order.cleaningPackageSelected.name}}
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Extra Options</h3>
        <ul class="extra">
          <li v-for="item in order.cleaningExtraOptionSelected" :key="item.id">
            <label> {{ item.name }}</label
            >
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Extra Options</h3>
        <customer-information v-bind:customer.sync="order.customer"
         :disabled="true"/>
      </v-col>
    </v-row>
    <v-row>
      <v-btn large color="primary" v-if="isNotEditable" @click="submit()">{{
        $t("buttons.goForPayment")
      }}</v-btn>
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
import CustomerInformation from "./CustomerInformation";
export default {
  props: {
   // order1: {},
  },
  components: {
    CustomerInformation,
  },
  data: function () {
    return {
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
      spaceSizes: [],
      cleaningPackages: [],
      cleaningExtraOptions: [],
      spaceSizeSelected: "",
      cleaningPackageSelected: "",
      cleaningExtraOptionSelected: [],
      isNotEditable: true,
      dialog: false,
      order: {
        customer: {
          firstName: "",
          surname: "",
          email: "",
          cellphone: "",
        },
        cleaningCategorySelected: "",
        spaceSizeSelected: "",
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
        (a) => a.id == this.order.cleaningCategorySelected
      );
      this.loading = false;
    },

    async getSpaceSizesByCategoryId() {
      this.loading = true;
      //  var response = await spaceSizeApi.getByCategoryId(this.categoryId);
      this.spaceSizes = [this.order.spaceSizeSelected];
      //  response.data.filter(
      //   (a) => a.id == this.categoryId
      // );
      this.loading = false;
    },

    async getCleaningPackagesByCategoryId() {
      this.loading = true;
      // var response = await cleaningPackageApi.getByCategoryId(this.categoryId);
      this.cleaningPackages = [this.order.cleaningPackageSelected];

      // response.data.filter(
      //   (a) => a.id == this.categoryId
      // );
      this.loading = false;
    },

    async getCleaningExtraOptionByCategoryId() {
      this.loading = true;
      // var response = await cleaningExtraOptionApi.getByCategoryId( this.categoryId  );
      this.cleaningExtraOptions = this.order.cleaningExtraOptionSelected;
      // response.data.filter( (a) => a.id == this.categoryId );
      this.loading = false;
    },

    submit() {
      const orderInstance = new orderModel.order(
        this.spaceSizeSelected,
        this.cleaningPackageSelected,
        this.cleaningExtraOptionSelected
      );

      var order = this.order;
      //  await orderApi.add(orderInstance);
    },
  },

  async mounted() {
    var order = JSON.parse(localStorage.getItem("order"));
    this.order = order;
    await this.getAllCleaningCategory();
    await this.getSpaceSizesByCategoryId();
    await this.getCleaningPackagesByCategoryId();
    await this.getCleaningExtraOptionByCategoryId();
  },
};
</script>
