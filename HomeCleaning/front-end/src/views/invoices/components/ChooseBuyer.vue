//TODO: Check if the search approach is decent enough.
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
        dense
        v-on="on"
        :disabled="disabled"
      >{{$t('labels.buyer')}}</v-btn>
    </template>
    <v-card>
      <v-card-title class="headline grey lighten-2">
        {{$t('labels.newInvoice')}} {{localstate.officialName}}
      </v-card-title>
      <v-autocomplete
        v-model="localstate"
        :loading="loading"
        :items="items"
        clearable
        dense
        item-text="officialName"
        item-value="id"
        return-object
        :search-input.sync="search"
        flat
        hide-details
        label="What state are you from?"
      ></v-autocomplete>
      <v-divider>
      </v-divider>
      <v-alert
        class="ma-1"
        type="error"
        colored-border
        dense
        border="left"
        elevation="2"
      >
        این شرکت حد اعتباری ندارد.
      </v-alert>
      <v-alert
        class="ma-1"
        type="warning"
        colored-border
        border="left"
        prominent
        elevation="2"
      >
        این شرکت حد اعتباری ندارد.
      </v-alert>
      <v-alert
        class="ma-1"
        type="info"
        colored-border
        border="left"
        prominent
        dense
        elevation="2"
      >
        این شرکت حد اعتباری ندارد.
      </v-alert>
      <v-alert
        class="ma-1"
        type="success"
        colored-border
        border="left"
        elevation="2"
      >
        این شرکت حد اعتباری ندارد.
      </v-alert>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="blue darken-1"
          text
          dense
          @click="dialog = false"
        >Close</v-btn>
        <v-btn
          color="blue darken-1"
          text
          dense
          @click="ok"
        >Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import recipientApi from "../../../api/recipient";

export default {
  name: "ChooseBuyer",
  props: {
    buyer: { Object },
    disabled: { Boolean, default: false }
  },
  data () {
    return {
      loading: false,
      entries: [],
      items: [],
      search: null,
      searchTerm: "",
      dialog: false,
      selectedBuyer: null,
    };
  },
  computed: {
    localstate: {
      get: function () {
        return this.buyer;
      },
      set: function (newValue) {
        this.selectedBuyer = newValue;
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
      //debugger;
      console.log("entries: " + this.entries.length);
      console.log("items: " + this.items.length);
      if (val.length < 3) return;

      this.items = this.entries.filter(function (item) {
        return item.officialName.includes(val);
      });
      if (this.items.length > 0) return;
      if (this.loading) return;

      this.loading = true;
      let response = await recipientApi.search("LegalPerson.OfficialName LIKE " + val);
      this.entries = response.result;
      this.items = this.entries;
      this.loading = false;
    },
  },
  methods: {
    ok: function () {
      this.$emit("update:buyer", this.selectedBuyer);
      this.dialog = false;
    }
  }
};
</script>