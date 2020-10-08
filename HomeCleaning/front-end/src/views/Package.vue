<template>
  <v-container>
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
                v-model="spaceSizePicked"
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
                v-model="cleaningPackagePicked"
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
                v-model="cleaningExtraOptionChecked"
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
          <v-btn color="primary" v-if="isNotEditable" @click="submit()">{{
            $t("buttons.submit")
          }}</v-btn>
        </v-col>
      </v-row>
    </div>
  </v-container>
</template>

<script>
import cleaningCategoryApi from "../api/cleaningCategoryApi";
import spaceSizeApi from "../api/spaceSizeApi";
import cleaningPackageApi from "../api/cleaningPackageApi";
import cleaningExtraOptionApi from "../api/cleaningExtraOptionApi";
import orderApi from "../api/orderApi";
import order from "../model/order";
export default {
  props: {
    cartable: Number,
  },
  components: {},
  data: function () {
    return {
      categoryId: this.$route.params["categoryId"],
      cleaningCategories: [],
      spaceSizes: [],
      cleaningPackages: [],
      cleaningExtraOptions: [],
      cleaningCategoryChecked: [],
      spaceSizePicked: "",
      cleaningPackagePicked: "",
      cleaningExtraOptionChecked: [],
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

    async submit() {
      const orderInstance=new order();
      var dd = await orderApi.add(orderInstance);
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
