<template>
  <v-app id="inspire">
    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue indigo darken-3" dark>
      <h4 style="fon">سامانه پشتیبانی مالی زنجیره تامین دارو</h4>
      <div class="flex-grow-1"></div>
      <v-btn tile link to="/">
        <router-link to="/">خانه</router-link>
      </v-btn>
      <!--      <v-btn tile link to="/register">-->
      <!--        <router-link to="/register">ثبت نام</router-link>-->
      <!--      </v-btn>-->
      <v-btn tile link to="/dashboard">
        <router-link to="/dashboard">ناحیه کاربری</router-link>
      </v-btn>
      <v-btn tile link @click="handleLogout()">
        <a href="#">خروج</a>
      </v-btn>
    </v-app-bar>
    <v-content>
      <v-container class="fill-height" fluid>
        <v-row align="center" justify="center">
          <slot/>
        </v-row>
      </v-container>
    </v-content>

    
  <!--   <v-footer
  
  padless
    >
  <v-card
    class="flex"
    flat
    tile
  >
    <v-card-title class="blue-grey darken-2">
   
  
      <v-spacer></v-spacer>
  
      <v-btn
        v-for="icon in icons"
        :key="icon"
        class="mx-4"
        dark
        icon
      >
        <v-icon size="24px">{{ icon }}</v-icon>
      </v-btn>
    </v-card-title>
  
    <v-card-text class="py-2 white--text text-center">
      {{ new Date().getFullYear() }} — <strong>All Rights Reserved</strong>
    </v-card-text>
  </v-card>
    </v-footer> -->



    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="grey darken-2">Create contact</v-card-title>
        <v-container>
          <v-row>
            <v-col class="align-center justify-space-between" cols="12">
              <v-row align="center">
                <v-avatar size="40px" class="mr-4">
                  <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt/>
                </v-avatar>
                <v-text-field placeholder="Name"></v-text-field>
              </v-row>
            </v-col>
            <v-col cols="6">
              <v-text-field prepend-icon="business" placeholder="Company"></v-text-field>
            </v-col>
            <v-col cols="6">
              <v-text-field placeholder="Job title"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field prepend-icon="mail" placeholder="Email"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field type="tel" prepend-icon="phone" placeholder="(000) 000 - 0000"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field prepend-icon="notes" placeholder="Notes"></v-text-field>
            </v-col>
          </v-row>
        </v-container>
        <v-card-actions>
          <v-btn text color="primary">More</v-btn>
          <div class="flex-grow-1"></div>
          <v-btn text color="primary" @click="dialog = false">Cancel</v-btn>
          <v-btn text @click="dialog = false">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script>
    export default {
        props: {
            source: String
        },
        data: () => ({
            dialog: false,
            drawer: null,
            items: [
                {icon: "contacts", text: "ثبت نام", path: "/register"},
                {icon: "history", text: "Frequently contacted"},
                {icon: "content_copy", text: "Duplicates"},
                {
                    icon: "keyboard_arrow_up",
                    "icon-alt": "keyboard_arrow_down",
                    text: "Labels",
                    model: true,
                    children: [{icon: "add", text: "Create label"}]
                },
                {
                    icon: "keyboard_arrow_up",
                    "icon-alt": "keyboard_arrow_down",
                    text: "More",
                    model: false,
                    children: [
                        {text: "Import"},
                        {text: "Export"},
                        {text: "Print"},
                        {text: "Undo changes"},
                        {text: "Other contacts"}
                    ]
                },
                {icon: "settings", text: "Settings"},
                {icon: "chat_bubble", text: "Send feedback"},
                {icon: "help", text: "Help"},
                {icon: "phonelink", text: "App downloads"},
                {icon: "keyboard", text: "Go to the old version"}
            ]
        }),
        methods: {
            handleLogout: function () {
                this.$keycloak.logout();
            }
        }
    };
</script>
