<template>
  <v-container style="height: 800px; padding: 150px 0 0 0">
    <h3>Server Dashbord</h3>
    <v-data-table
      dark
      dense
      :headers="headers"
      :items="items"
      :options.sync="options"
      :server-items-length="totalCount"
      :footer-props="{
        'items-per-page-options': rowsPerPageItems,
        showFirstLastPage: true,
        firstIcon: 'mdi-arrow-collapse-left',
        lastIcon: 'mdi-arrow-collapse-right',
        prevIcon: 'mdi-minus',
        nextIcon: 'mdi-plus',
      }"
      :loading="loading"
      :search="search"
      loading-text="Loading... Please wait"
      class="elevation-1 dblue-style"
    >
      <template v-slot:item.orderStatus="{ item }">
        <div class="action-btns">
          <v-btn class="statusTitle" rounded outlined color="#6bb9c1">{{
            item.orderStatus
          }}</v-btn>
        </div>
      </template>

      <template v-slot:item.action="{ item }">
        <div class="action-btns">
          <router-link :to="`CartableDetail/${item.id}`">
            <v-icon class="mr-2">mdi-file-edit</v-icon>
          </router-link>

          <v-btn  @click="makeFinanceRequest(item)" class small text color="#0a8ac7">
            <v-icon class="mr-2">mdi-file-edit</v-icon>
          </v-btn>

          <v-btn class small text color="#d0abab">
            <v-icon>mdi-delete</v-icon>
          </v-btn>
        </div>

        <!-- <v-icon v-if="!item.financeRequested" medium class="mr-2" @click="makeFinanceRequest(item)">attach_money</v-icon>
        <v-icon color="red" v-if="item.financeRequested" medium class="mr-2" @click="cancelFinanceRequest(item)">attach_money</v-icon>-->
      </template>
    </v-data-table>
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
import orderApi from "../api/orderApi";
import serverRequestApi from "../api/serverRequestApi";

export default {
  mixins: [ScrollToInvalidInput],
  props: {
    cartable: Number,
  },
  components: {},

  data() {
    return {
      page: 1,
      pageCount: 0,
      itemsPerPage: 10,
      search: "",
      totalCount: 0,
      loading: false,
      pagination: {},
      options: {},
      rowsPerPageItems: [5, 10, 20],
      headers: [
        { text: "UserName", value: "clientUser.userName" },
        { text: "Email", value: "clientUser.email" },
        { text: "Extra Option", value: "cleaningExtraOption.name" },
        { text: "Package", value: "cleaningPackage.name" },
        { text: "Size", value: "spaceSize.name" },
        { text: "price", value: "price" },
        { text: "discount", value: "discount" },
        { text: "tax", value: "tax" },
        { text: "totalPrice", value: "totalPrice" },
        { text: "Address", value: "address.addressStr" },
        { text: "Status", value: "orderStatus" },
        { text: "action", value: "action", sortable: false },
      ],
      items: [],
    };
  },

  methods: {
    makeFinanceRequest: async function (item) {
      var self = this;
      this.$root.getConfirmation("Would you want to accept this order?", "", async function () {
          await serverRequestApi.add(item.id);
          item.financeRequested = true;
          self.$root.alert("Customer order accepted");
        }
      );
    },
    async getNewOrder() {
      this.loading = true;
      const { sortBy, sortDesc, page, itemsPerPage, filtered } = this.options;
      var response = await orderApi.getNewOrder(page, itemsPerPage);
      this.items = response.data;
      this.totalCount = 10; // response.data.totalCount;
      this.loading = false;
    },
  },

  async mounted() {
    await this.getNewOrder();
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