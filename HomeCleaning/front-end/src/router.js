//FIXME: Lazy load some routes like registrations
import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Package from "./views/Package.vue";
import ServiceOrder from "./views/ServiceOrder.vue";
import Login from "./views/Login.vue";
Vue.use(Router);

export default new Router({
    mode: "history",
    base: process.env.BASE_URL,
    routes: [{
            path: "/login",
            name: "login",
            component: Login,
            meta: {
                layout: "default"
            }
        },
        {
            path: "/",
            name: "home",
            component: Home,
            meta: {
                layout: "default"
            }
        },
        {
            path: "/package",
            name: "package",
            component: Package,
            meta: {
                layout: "default"
            }
        },
        {
            path: "/serviceOrder",
            name: "serviceOrder",
            component: ServiceOrder,
            meta: {
                layout: "validMember"
            }
        },
        {
            path: "/about",
            name: "about",
            // route level code-splitting
            // this generates a separate chunk (about.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () =>
                import ( /* webpackChunkName: "about" */ "./views/About.vue")
        },
    ]
});