//FIXME: Lazy load some routes like registrations
import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";
import Locale from "./views/Locale.vue";

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


            ]
        }
    ]
});