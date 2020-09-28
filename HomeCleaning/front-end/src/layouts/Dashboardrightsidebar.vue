<template>
  <v-app id="setam-inspire">
    <v-system-bar color="rgb(10, 50, 73)" dark app>
      <v-spacer></v-spacer>
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
    </v-system-bar>
    <v-app-bar app fixed1 elevate-on-scroll class="app-bar1 app-bar-bg">
      <v-app-bar-nav-icon @click="drawer=!drawer" v-if="!drawer"></v-app-bar-nav-icon>
      <v-toolbar dark flat color="#0d3f5d" class="header-toolbar">
        <!-- <v-toolbar-title>Title</v-toolbar-title> -->
        <v-menu offset-y flat v-model="showMenu">
          <template v-slot:activator="{ on }">
            <v-card v-on="on" dark color="#0d3f5d" class="user-menu">
              <v-icon>mdi-chevron-down</v-icon>
              <v-avatar>
                <img src="https://randomuser.me/api/portraits/women/81.jpg" alt="novin"/>
              </v-avatar>

              <span>سرمایه گذاری نوین دارو</span>
            </v-card>
          </template>
          <v-list class="user-menu-list">
            <v-list-item v-for="(item, index) in menuitems" :key="index">
              <v-list-item-title>
                <router-link :to="item.url" class @click="handleLogout()">
                  <v-icon>{{ item.icon }}</v-icon>
                  {{ item.title }}
                </router-link>
              </v-list-item-title>
            </v-list-item>
            <v-list-item>
              <v-icon>mdi-logout</v-icon>
              <a href="#" @click="handleLogout()">خروج از سیستم</a>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-spacer></v-spacer>
      </v-toolbar>
    </v-app-bar>

    <!-- --------sidebar---------------- -->
    <v-navigation-drawer
      class="sidebar"
      v-model="drawer"
      app
      :src="bg"
      right
      dark
      color="rgb(41, 46, 84)"
    >
      <!-- ------------------------ -->

      <v-list-item @click="drawer=!drawer" class="sidebar-header">
        <v-list-item-content>
          <v-list-item-title>
            <v-icon>mdi-arrow-collapse-right</v-icon>
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <router-link to="/account" class>
        <v-list-item two-line>
          <v-list-item-avatar>
            <img src="https://randomuser.me/api/portraits/women/81.jpg"/>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>سرمایه گذاری نوین دارو</v-list-item-title>
            <v-list-item-subtitle>حساب کاربری</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </router-link>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-item v-for="item in dashitems" :key="item.title" :href="item.url">
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>

      <v-card-text>
        <v-treeview :items="treeitems">
          <template v-slot:prepend="{ item }">
            <v-icon
              v-if="item.children"
              v-text="`mdi-${item.id === 1 ? 'mdi-file-tree' : 'mdi-chevron-left'}`"
            ></v-icon>
          </template>
        </v-treeview>
      </v-card-text>
    </v-navigation-drawer>

    <!-- --------end sidebar---------------- -->

    <v-content class="content-bg">
      <v-container fluid>
        <slot/>
      </v-container>
    </v-content>

    <!-- <v-btn bottom color="pink" dark fab fixed right @click="dialog = !dialog">
      <v-icon>mdi-plus</v-icon>
    </v-btn>-->

    <v-footer app absolute dark padless color>
      <v-container fluid class="pa-0 ma-0" padless>
        <v-card height="180" padless class="white--text text-center footer-bg">
          <v-card-text>
            <v-btn v-for="icon in icons" :key="icon" class="mx-4 white--text" icon>
              <v-icon size="24px">{{ icon }}</v-icon>
            </v-btn>
          </v-card-text>

          <v-card-text class>
            <router-link to="/" class>سامانه تامین مالی</router-link>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-text class="white--text">
            version {{ new Date().getFullYear() }} —
            <strong>Setam system</strong>
          </v-card-text>
        </v-card>
      </v-container>
    </v-footer>

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
      showMenu: false,
      sidebarOpen: true,
      show: false,
      dialog: false,
      drawer: true,
      icons: [
        "fab fa-facebook",
        "fab fa-twitter",
        "fab fa-google-plus",
        "fab fa-linkedin",
        "fab fa-instagram"
      ],
      menuitems: [
        {
          title: "تنظیمات پروفایل",
          icon: "mdi-account-edit-outline",
          url: "/account"
        }
        //{title: 'خروج از سیستم', icon: 'mdi-logout', url: '/logout'},
      ],

      items: [
        {icon: "contacts", text: "ثبت نام", path: "/register"},
        {icon: "history", text: "Frequently contacted"},
        {icon: "content_copy", text: "Duplicates"}
      ],
      dashitems: [
        {title: "داشبورد", icon: "mdi-home-city", url: "/dashboard"},
        {title: "حساب من", icon: "mdi-account", url: "/account"},
        {title: "لیست تست", icon: "mdi-account-group-outline", url: "/invoices"}
      ],

      treeitems: [
        {
          id: 1,
          name: "دسترسی درختی",
          children: [
            {
              id: 2,
              name: "شاخه یکم",
              children: [
                {
                  id: 201,
                  name: "برگ یکم"
                }
              ]
            },
            {
              id: 3,
              name: "مدیران",
              children: [
                {
                  id: 301,
                  name: "رعناسیستم"
                }
              ]
            },
            {
              id: 4,
              name: "شرکا",
              children: [
                {
                  id: 401,
                  name: "برند یکم"
                },
                {
                  id: 402,
                  name: "برند دوم"
                },
                {
                  id: 403,
                  name: "برند سوم"
                }
              ]
            }
          ]
        }
      ]
    }),
    computed: {
      bg() {
        return "https://randomuser.me/api/portraits/women/81.jpg";
      }

      //   sidebarOpenFns() {
      //     switch (this.$vuetify.breakpoint.name) {
      //       case 'xs': return true
      //       case 'sm': return true
      //       case 'md': return true
      //       case 'lg': return false
      //       case 'xl': return false
      //   }

      // }
    },

    methods: {
      handleLogout: function () {
        this.$keycloak.logout();
      }
    }
  };
</script>

<style lang="scss">
  body {
    a {
      text-decoration: none;
    }
  }

  .app-bar-bg {
    background-color: #0d3f5d !important;
  }

  .app-bar-btn {
    display: inline-block;
    position: relative;
    color: #fff !important;
    margin: 0 !important;
  }

  .logo-area {
    margin-top: 25px !important;
    margin-right: 35px;
    min-width: 250px;
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

  .v-navigation-drawer__image {
    opacity: 0.1;
  }

  .v-list-item__subtitle {
    font-size: 0.775rem;
    margin-top: 5px;
  }

  .header-toolbar {
    margin-right: 15px;
  }

  .sidebar-header {
    padding: 7px 30px;
    text-align: left;
  }

  .theme--light.v-btn.v-btn--icon {
    color: rgb(255, 255, 255);
    margin: 0 10px;
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

  .user-menu {
    &.v-card {
      background: transparent;
      border: none;
      transition: none;
      box-shadow: none;
      padding: 5px 10px;
    }
  }

  .user-menu-list a {
    color: #469a92 !important;
    font-size: 14px;
  }

  .form-card {
    padding: 10px;
    margin: 10px 0;

    .headline {
      font-family: "IRANSans", sans-serif !important;
      font-size: 16px !important;
      display: block;
      color: #40af9f;

      .subtitle {
        font-family: "IRANSans", sans-serif !important;
        font-size: 12px !important;
        display: block;
        color: #69706f;
      }
    }
  }
</style>
