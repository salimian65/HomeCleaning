<template>
  <div class="grey lighten-5">
    <v-toolbar dense>
      <v-app-bar-nav-icon></v-app-bar-nav-icon>
      <v-toolbar-title>Title</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-heart</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
    </v-toolbar>
    <v-row
      v-for="lineItem in lineItems"
      :key="lineItem.id"
      no-gutters
      style="flex-wrap: nowrap;"
    >
      <v-col
        class="flex-grow-0 flex-shrink-0 justify-center d-flex flex-column"
        style="border:1px blue solid"
      >
        <span>46</span>
      </v-col>
      <v-col style="border:1px red solid">
        <v-row
          class="ma-0 pa-1"
          style="border:1px green dashed"
        >
          <v-col
            class="ma-0 pa-0 justify-sm-center justify-xs-center justify-end"
            cols="12"
            xl="6"
            lg="6"
            md="6"
            sm="12"
            xs="12"
          >
            <v-icon>mdi-pill</v-icon><span>{{lineItem.brandPersian}}</span>
          </v-col>

          <v-col
            class="ma-0 pa-0 justify-sm-center justify-xs-center justify-start"
            cols="12"
            xl="6"
            lg="6"
            md="6"
            sm="12"
            xs="12"
            dir="ltr"
          >
            <v-icon>mdi-pill</v-icon><span>{{lineItem.brandEnglish}}</span>
          </v-col>
        </v-row>
        <v-row
          class="ma-0 pa-1"
          style="border:1px purple dashed"
        >
          <v-col
            class="ma-0 pa-0 justify-sm-center justify-xs-center justify-start flex-grow-1 flex-shrink-1"
            cols="12"
            xl="4"
            lg="4"
            md="4"
            sm="12"
            xs="12"
          >
            <v-icon>mdi-office-building</v-icon>
            <span>{{lineItem.licenseOwner}}</span>
          </v-col>
          <v-col
            class="ma-0 pa-0 justify-center flex-grow-1 flex-shrink-1"
            cols="12"
            xl="4"
            lg="4"
            md="4"
            sm="12"
            xs="12"
          >
            <v-icon>mdi-numeric</v-icon>
            <span>{{lineItem.irc}}</span>
          </v-col>
          <v-col
            class="ma-0 pa-0 justify-sm-center justify-xs-center justify-lg-end justify-md-end justify-xl-end flex-grow-1 flex-shrink-1"
            cols="12"
            xl="4"
            lg="4"
            md="4"
            sm="12"
            xs="12"
          >
            <v-icon>mdi-counter</v-icon>
            <span>{{lineItem.quantity}}</span>
          </v-col>
        </v-row>
      </v-col>
      <v-col
        class="flex-grow-0 flex-shrink-0"
        style="border:1px blue solid"
      >
        <v-menu offset-y="">
          <template v-slot:activator="{ on }">
            <v-btn
              color="primary"
              dark
              icon
              v-on="on"
            >
              <v-icon>mdi-dots-horizontal</v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item @click="">
              <v-list-item-title> item.title</v-list-item-title>
            </v-list-item>
            <v-list-item @click="">
              <v-list-item-title> item.title</v-list-item-title>
            </v-list-item>
            <v-list-item @click="">
              <v-list-item-title> item.title</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "LineItems",
  props: { lineItems: {} }
};
</script>
