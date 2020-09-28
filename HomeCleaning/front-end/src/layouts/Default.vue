<template>
  <v-app id="setam-inspire">

    <v-app-bar
      :clipped-left="$vuetify.breakpoint.lgAndUp"
      app
      elevate-on-scroll
      height="90"
      class="app-bar app-bar-bg"
    >

      <div class="language-menu">
        <ul>
          <li>
            <a href="#" @click="$root.changeActiveLanguage('en')">English</a>
          </li>
          <li>
            <a href="#" @click="$root.changeActiveLanguage('ar')">عربی</a>
          </li>
          <li>
            <a href="#" @click="$root.changeActiveLanguage('fa')" class="active">فارسی</a>
          </li>
        </ul>
      </div>

      <div class="menu-bar">

        <v-row no-gutters>

          <v-col
            cols="6"
            md="9"
          >
            <div class="logo-area">
              <a href="#"><img
                src="../assets/logo.png"
                alt=""
              ></a>
            </div>
          </v-col>
          <v-col
            align-self="start"
            cols="6"
            md="3"
          >
            <div class="login-btn-wrapper">
              <v-btn
                rounded
                color="pink darken-2"
                dark
                :to="'/' + $root.getLocale() + '/dashboard'"
              >ورود به سایت
              </v-btn>
            </div>
          </v-col>
        </v-row>

        <!-- <div class="flex-grow-1"></div>
      <router-link to="/">خانه</router-link> -->
        <!--      <v-btn tile link to="/register">-->
        <!--        <router-link to="/register">ثبت نام</router-link>-->
        <!--      </v-btn>-->
        <!-- <v-btn tile link to="/dashboard">
        <router-link to="/dashboard">ناحیه کاربری</router-link>
      </v-btn>
      <v-btn tile link @click="handleLogout()">
        <a href="#">خروج</a>
      </v-btn> -->

      </div>

    </v-app-bar>

    <v-content class="content-bg">
      <v-row
        align="center"
        justify="center"
      >
        <v-container>
          <v-card class="mx-auto">
            <slot/>
          </v-card>
        </v-container>
      </v-row>
    </v-content>

    <!-- <v-btn bottom color="pink" dark fab fixed right @click="dialog = !dialog">
      <v-icon>mdi-plus</v-icon>
    </v-btn> -->

    <v-footer
      dark
      padless
      color=""
    >
      <v-container
        fluid
        class="pa-0 ma-0  "
        padless
      >
        <v-card
          height="200"
          padless
          class=" white--text text-center footer-bg"
        >

          <v-card-text>
            <v-btn
              v-for="icon in icons"
              :key="icon"
              class="mx-4 white--text "
              icon
            >
              <v-icon size="24px">{{ icon }}</v-icon>
            </v-btn>
          </v-card-text>

          <v-card-text class="white--text pt-0 ">
            سامانه تامین مالی
          </v-card-text>

          <v-divider></v-divider>

          <v-card-text class="white--text ">
            version {{ new Date().getFullYear() }} — <strong>Setam system</strong>
          </v-card-text>

        </v-card>

      </v-container>

    </v-footer>

    <v-dialog
      v-model="dialog"
      width="800px"
    >
      <v-card>
        <v-card-title class="grey darken-2">Create contact</v-card-title>
        <v-container>
          <v-row>
            <v-col
              class="align-center justify-space-between"
              cols="12"
            >
              <v-row align="center">
                <v-avatar
                  size="40px"
                  class="mr-4"
                >
                  <img
                    src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png"
                    alt
                  />
                </v-avatar>
                <v-text-field placeholder="Name"></v-text-field>
              </v-row>
            </v-col>
            <v-col cols="6">
              <v-text-field
                prepend-icon="business"
                placeholder="Company"
              ></v-text-field>
            </v-col>
            <v-col cols="6">
              <v-text-field placeholder="Job title"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                prepend-icon="mail"
                placeholder="Email"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                type="tel"
                prepend-icon="phone"
                placeholder="(000) 000 - 0000"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                prepend-icon="notes"
                placeholder="Notes"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
        <v-card-actions>
          <v-btn
            text
            color="primary"
          >More
          </v-btn>
          <div class="flex-grow-1"></div>
          <v-btn
            text
            color="primary"
            @click="dialog = false"
          >Cancel
          </v-btn>
          <v-btn
            text
            @click="dialog = false"
          >Save
          </v-btn>
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
      show: false,
      dialog: false,
      drawer: null,
      icons: [
        "fab fa-facebook",
        "fab fa-twitter",
        "fab fa-google-plus",
        "fab fa-linkedin",
        "fab fa-instagram",
      ],
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

<style lang="scss">
  //@include font-face(IRANSans, ../assets/fonts/IRANSans);
  //@include font-face(IRANSans, ../assets/fonts/IRANSans, null, null, woff);

  body {
    font-family: "IRANSans", tahoma, arial, sans-serif !important;
  }

  .v-application {
    font-family: "IRANSans", sans-serif !important;
    /* .title {
      // To pin point specific classes of some components
      font-family: $title-font, sans-serif !important;
    } */
  }

  .hasError label {
    border-bottom: 1px solid red !important;
  }

  .dashboardMainContent {
    width: 100% !important;
  }

  body {
    a {
      text-decoration: none;
    }
  }

  .app-bar-bg {
    background-color: #0d3f5d !important;
  }

  .logo-area {
    margin-top: 35px;
  }

  .menu-bar {
    position: relative;
    width: 100%;
  }

  .login-btn-wrapper {
    margin-top: 40px;
    text-align: center;
  }

  .language-menu {
    position: absolute;
    z-index: 2;
    display: block;
    width: 100%;
    height: 25px;
    text-align: left;
    top: 0;
    font-size: 11px;
    background: rgba(0, 0, 0, 0.21);
    left: 0;
    padding: 2px 5%;

    ul {
      display: inline-block;

      li {
        display: inline-block;
        margin: 3px 20px;

        a {
          font-size: 11px;
          color: #fff;
        }
      }
    }
  }

  .content-bg {
    background-image: url("../assets/img/pbg.png");
    background-size: cover;
    background-position: center;
  }

  .footer-bg {
    background-color: #0a3249 !important;
    position: relative;
    //  background-image: url('../assets/img/footer-bg.png');
    //   background-size: cover;
    //   background-position: center;
  }

  .footer-bg:before {
    content: " ";
    display: block;
    position: absolute;
    left: 0;
    top: 0;
    width: 120%;
    height: 100%;
    z-index: 1;
    opacity: 0.6;
    background-image: url("../assets/img/footer-bg.png");
    background-repeat: no-repeat;
    background-position: 100% 20%;
    -ms-background-size: cover;
    -o-background-size: cover;
    -moz-background-size: cover;
    -webkit-background-size: cover;
    background-size: cover;
  }

  .v-application,
  .v-application .headline {
    font-family: "IRANSans", sans-serif !important;
  }

  .v-application .headline {
    font-size: 1.2rem !important;
    color: rgb(44, 117, 93);
  }

  .v-label {
    font-size: 14px;
  }
</style>
