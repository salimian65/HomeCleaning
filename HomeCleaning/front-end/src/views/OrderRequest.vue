<template>
  <div class="mehrdad" style="padding: 150px">
  <v-row>
    <v-col  cols="6">
      <v-btn
        style="padding: 200px 0 0 0"
        color="primary"
      
        @click="submit()"
      
        >{{ $t("buttons.submit") }}</v-btn
      >
    {{order}}  
    </v-col>
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

export default {
  props: {
    order: {},
  },
  components: {},
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

      var order = this.order;
      //  await orderApi.add(orderInstance);
    },
  },

  async mounted() {
    await this.getAllCleaningCategory();
    await this.getSpaceSizesByCategoryId();
    await this.getCleaningPackagesByCategoryId();
    await this.getCleaningExtraOptionByCategoryId();
    var order = this.order;
  },
};
</script>
