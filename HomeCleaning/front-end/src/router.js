//FIXME: Lazy load some routes like registrations
import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";
import Locale from "./views/Locale.vue";
import Registerintro from "./views/Registerintro.vue";
import RetailerRegisteration from "./views/registeration/retailer/";
import DistributorRegisteration from "./views/registeration/distributor/";
import SupplierRegisteration from "./views/registeration/supplier/";
import RegistrationDone from "./views/registeration/RegistrationDone";
import Registrant from "./views/registeration/registrant";
import Invoices from "./views/invoices";
import Invoice from "./views/invoices/Invoice";
import Account from "./views/account";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [{
      path: "/",
      redirect: "/fa/"
    },
    {
      path: "/dashboard",
      redirect: "/fa/dashboard"
    },
    {
      path: "/:locale",
      //name: "locale",
      component: Locale,
      meta: {
        layout: "default"
      },
      children: [{
          path: "login",
          name: "login",
          component: Login,
          meta: {
            layout: "default"
          }
        },
        {
          path: "",
          name: "registerintro",
          component: Registerintro,
          meta: {
            layout: "default"
          }
        },
        {
          path: "dashboard",
          name: "home",
          component: Home
        },
        {
          path: "dashboardleftsidebar",
          name: "dashboardsidebarr",
          component: Home
        },
        {
          path: "dashboardrightsidebar",
          name: "dashboardsidebarl",
          component: Home,
          meta: {
            layout: "dashboardrightsidebar"
          }
        },
        {
          path: "dashboard1",
          name: "dashboard1",
        },
        {
          path: "register/delegate/:recipientType/:id?",
          name: "registrant",
          component: Registrant,
          props: true,
          meta: {
            layout: "default"
          }
        },
        {
          path: "register/retailer/:registrantId?",
          name: "RetailerRegisteration",
          component: RetailerRegisteration,
          props: true,
          meta: {
            layout: "guest"
          }
        },
        {
          path: "register/distributor/:registrantId?",
          name: "DistributorRegisteration",
          component: DistributorRegisteration,
          props: true,
          meta: {
            layout: "guest"
          }
        },
        {
          path: "register/supplier/:registrantId?",
          name: "SupplierRegisteration",
          component: SupplierRegisteration,
          props: true,
          meta: {
            layout: "guest"
          }
        },
        {
          path: "register/done",
          name: "RegistrationDone",
          component: RegistrationDone,
          props: true,
          meta: {
            layout: "guest"
          }
        },
        {
          path: "invoices",
          name: "invoices",
          component: Invoices,
          props: true,
          meta: {
            layout: "dashboardrightsidebar"
          }
        },
        {
          path: "invoice/:operation/:id?",
          name: "invoice",
          component: Invoice,
          props: true,
          meta: {
            layout: "dashboardrightsidebar"
          }
        },
        {
          path: "account",
          name: "account",
          component: Account
        }
      ]
    }
  ]
});
