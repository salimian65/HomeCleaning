<template>
  <div>
    <v-data-table
      class="elevation-1"
      dense
      :headers="headers"
      :items="localstate"
      :loading="loading"
      :search="search"
      disable-pagination
      :loading-text="$t('labels.loading')"
      hide-default-footer
    >
      <template v-slot:item.quantity="{ item }">
        <v-chip
          :color="getColor(item.quantity)"
          dark
        >
          {{ item.quantity }}
        </v-chip>
      </template>
      <template v-slot:item.action="{ item }">
        <v-row>
          <v-btn
            icon
            color="green"
            class="ma-0"
          >
            <v-icon>mdi-cached</v-icon>
          </v-btn>
          <v-btn
            icon
            color="red"
            class="ma-0"
          >
            <v-icon>mdi-trash-can-outline</v-icon>
          </v-btn>
        </v-row>
      </template>
      <!-- <template v-slot:item.type="{ item }">
        <v-icon
          small
          color="green darken-2"
        >
          mdi-watch
        </v-icon>
      </template> -->
    </v-data-table>
  </div>
</template>

<script>
export default {
  name: "LineItems",
  props: {
    lineItems: { Array }
  },
  data () {
    return {
      search: "",
      totalCount: 0,
      loading: false,
      pagination: {},
      rowsPerPageItems: [1, 2, 10, 20, 50, 100],
      headers: [{
        text: "brandEnglish",
        filterable: true,
        value: "brandEnglish",
      },
      {
        text: "brandPersian",
        filterable: true,
        value: "brandPersian",
      },
      {
        text: "irc",
        value: "irc",
        filterable: true,
      },
      {
        text: "licenseOwner",
        value: "licenseOwner",
        filterable: true,
      },
      {
        text: "quantity",
        value: "quantity",
        filterable: true,
      },
      {
        text: "Actions",
        value: "action",
        sortable: false,
      },
      ],
      items: [],
    };
  },
  computed: {
    localstate: {
      get: function () { return this.lineItems; },
      set: function (newValue) { this.$emit("update:lineItems", newValue); }
    }
  },
  methods: {
    finallyFunc () {
      this.loading = false;
    },
    getColor (quantity) {
      if (quantity > 10)
        return "orange";
      else
        return "green";
    }
  }
};
</script>
