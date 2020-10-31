<template>
  <v-container style="height: 800px;padding: 150px 0 0 0">
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
      <template v-slot:item.action="{ item }">
        <div class="action-btns">
          <router-link :to="`CartableDetail/${item.id}`">
            <v-icon class="mr-2">mdi-file-edit-outline</v-icon>
          </router-link>
        </div>
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

export default {
  mixins: [ScrollToInvalidInput],
  props: {
    cartable: Number,
  },
  components: {
  
  },
  
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
        { text: "Customer UserName", value: "clientUser.userName" },
        { text: "customer Email", value: "clientUser.email" },
        { text: "cleaningExtraOption", value: "cleaningExtraOption.name" },
        { text: "cleaningPackage", value: "cleaningPackage.name" },
        { text: "spaceSize", value: "spaceSize.name" },
        { text: "price", value: "price" },
        { text: "discount", value: "discount" },
        { text: "tax", value: "tax" },
        { text: "totalPrice", value: "totalPrice" },
        { text: "Address", value: "address.addressStr" },
        { text: "action", value: "action", sortable: false }
      ],
      items: []
    };
  },

   methods: {
    async getByUser() {
      this.loading = true;
      const { sortBy, sortDesc, page, itemsPerPage, filtered } = this.options;
      var response = await orderApi.getOrder(page, itemsPerPage);
      this.items = response.data;
      this.totalCount = 10 // response.data.totalCount;
      this.loading = false;
    }
  },

  async mounted() {
    await this.getByUser();
  }
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