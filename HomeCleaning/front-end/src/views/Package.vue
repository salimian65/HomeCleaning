<template>
  <div class="cartableDetail" style="padding: 150px">
    <h1>Make Your Request</h1>
    <v-row>
      <v-col cols="6">
        <h3>Cleaning Category</h3>
        <ul id="example-1">
          <li v-for="item in cleaningCategories" :key="item.id">
            {{ item.name }}
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Space Size</h3>
        <ul id="example-1">
          <li v-for="item in spaceSizes" :key="item.id">
            <input
              type="radio"
              :id="`spaceSize` + item.id"
              :value="item"
              name="spaceSize"
              v-model="spaceSizeSelected"
            />
            <label :for="`spaceSize` + item.id"> {{ item.name }}</label>
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Cleaning Package</h3>
        <ul id="example-1">
          <li v-for="item in cleaningPackages" :key="item.id">
            <input
              type="radio"
              :id="`cleaningPackage` + item.id"
              :value="item"
              name="cleaningPackage"
              v-model="cleaningPackageSelected"
            />
            <label :for="`cleaningPackage` + item.id"> {{ item.name }}</label>
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <h3>Extra Options</h3>
        <ul id="example-1">
          <li v-for="item in cleaningExtraOptions" :key="item.id">
            <input
              type="checkbox"
              :id="`cleaningExtraOption` + item.id"
              :value="item"
              name="cleaningExtraOption"
              v-model="cleaningExtraOptionSelected"
            />
            <label :for="`cleaningExtraOption` + item.id">
              {{ item.name }}</label
            >
          </li>
        </ul>
      </v-col>
    </v-row>
    <v-row>
      <v-dialog v-model="dialog" width="500">
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
      </v-dialog>
    </v-row>
  </div>
</template>

<script>
import cleaningCategoryApi from "../api/cleaningCategoryApi";
import spaceSizeApi from "../api/spaceSizeApi";
import cleaningPackageApi from "../api/cleaningPackageApi";
import cleaningExtraOptionApi from "../api/cleaningExtraOptionApi";
import orderApi from "../api/orderApi";
import orderModel from "../models/orderModel";
import loginModal from "../views/Login";

export default {
  props: {
    cartable: Number,
  },
  components: {
    loginModal,
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
      const orderInstance = new orderModel.order(
        this.spaceSizeSelected,
        this.cleaningPackageSelected,
        this.cleaningExtraOptionSelected
      );
      this.showModal();
      this.$router.push({ name: 'orderRequest' , params: { order: orderInstance }})
      //  await orderApi.add(orderInstance);
    },

    showModal() {
      //let element = this.$refs.modal.$el
      // $(element).modal('show')
      // this.$root.$emit("bv::show::modal", "ssss");
    },
  },

  async mounted() {
    await this.getAllCleaningCategory();
    await this.getSpaceSizesByCategoryId();
    await this.getCleaningPackagesByCategoryId();
    await this.getCleaningExtraOptionByCategoryId();
  },
};
</script>
