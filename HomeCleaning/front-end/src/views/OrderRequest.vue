<template>
  <div class="about">
    <h1>This is an about page</h1>
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
  components: {
 
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
      this.$router.push({ name: 'routename' , params: { order: orderInstance }})
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
