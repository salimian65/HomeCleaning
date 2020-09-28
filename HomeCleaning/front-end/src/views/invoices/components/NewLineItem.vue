<template>
  <v-dialog
    v-model="dialog"
    persistent
    max-width="600px"
  >
    <template v-slot:activator="{ on }">
      <v-btn
        color="primary"
        dark
        v-on="on"
        v-show="editMode"
      >
        <v-icon>mdi-plus</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="headline grey lighten-3">
        {{$t('labels.newInvoice')}}
      </v-card-title>
      <v-tabs
        v-model="tab"
        fixed-tabs
      >
        <v-tab>
          Name
        </v-tab>
        <v-tab>
          Irc
        </v-tab>
      </v-tabs>
      <v-tabs-items v-model="tab">
        <v-tab-item>
          <v-autocomplete
            v-model="localstate"
            :loading="loading"
            :items="items"
            item-text="productName"
            item-value="productId"
            return-object
            :search-input.sync="search"
            flat
            hide-no-data
            hide-details
            label="Name"
            id="brand"
          ></v-autocomplete>
        </v-tab-item>
        <v-tab-item>
          <v-text-field
            v-model="search"
            id="irc"
            label="IRC"
            v-mask="'################'"
            loading
            :append-outer-icon="'mdi-magnify'"
            @click:append-outer="getProductByIrc"
            @keypress.enter="getProductByIrc"
          ></v-text-field>
        </v-tab-item>
      </v-tabs-items>
      <v-divider>
      </v-divider>
      <v-card class="pa-2">
        <v-card-subtitle class="grey lighten-3">
          {{$t('labels.product')}}
        </v-card-subtitle>
        <v-row class="pa-2">
          <label>BrandEnglish:</label> {{currentLineItem.brandEnglish}}
        </v-row>
        <v-row class="pa-2">
          <label>BrandPersian:</label> {{currentLineItem.brandPersian}}
        </v-row>
        <v-row class="pa-2">
          <v-col>
            <label>LicenseOwner:</label> {{currentLineItem.licenseOwner}}
          </v-col>
          <v-col>
            <label>Irc:</label> {{currentLineItem.irc}}
          </v-col>
        </v-row>
        <v-row>
          <form-group :name="$t('labels.quantity')">
            <v-text-field
              slot-scope="{ attrs }"
              v-bind="attrs"
              type="number"
              v-model="currentLineItem.quantity"
              :label="$t('labels.quantity')"
              required
            ></v-text-field>
          </form-group>
        </v-row>
      </v-card>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="blue darken-1"
          text
          @click="dialog = false"
        >Close</v-btn>
        <v-btn
          color="blue darken-1"
          text
          @click="ok"
        >Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import ttacApi from "../../../api/ttac";
import { mask } from "vue-the-mask";

export default {
  name: "NewLineItem",
  props: {
    lineItem: { Object },
    addLineItem: { Function },
    editMode: { Boolean, default: false },
    
  },
  data () {
    return {
      loading: false,
      entries: [],
      items: [],
      currentLineItem: {
        irc: "",
        brandEnglish: "",
        brandPersian: "",
        licenseOwner: "",
        quantity: 0
      },
      search: null,
      searchTerm: "",
      dialog: false,
      //searchByName: true,
      tab: 1
    };
  },
  computed: {
    localstate: {
      get: function () {
        return this.lineItem;
      },
      set: function (newValue) {
        this.currentLineItem = newValue;
      }
    }/*,
    items () {
      return this.entries.filter(function (item) {
        return item.officialName.include(this.searchTerm);
      });
    }*/
  },
  watch: {
    search: async function (val) {
      // debugger;
      // this.searchByName = (!val || isNaN(val.charAt(0)));
      // console.log("entries: " + this.entries.length);
      // console.log("items: " + this.items.length);
      // if (val.length < 3) return;

      // this.items = this.entries.filter(function (item) {
      //   return item.officialName.includes(val);
      // });
      // if (this.items.length > 0) return;
      // if (this.loading) return;

      // this.loading = true;
      // let response = await recipientApi.search("LegalPerson.OfficialName LIKE " + val);
      // this.entries = response.result;
      // this.items = this.entries;
      // this.loading = false;
    }
  },
  methods: {
    getProductByIrc: async function () {
      this.localstate = await ttacApi.getbyIrc(this.search);
    },
    ok: function () {
      this.$emit("update:lineItem", this.currentLineItem);
      if (this.addLineItem) {
        this.addLineItem(this.currentLineItem);
      }
      this.dialog = false;
    }


  },
  directives: { mask }
};
</script>