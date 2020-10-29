//FIXME: Lazy load some routes like registrations
import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Package from "./views/Package.vue";
import OrderRequest from "./views/OrderRequest.vue";
import CallBack from './views/CallBack.vue'
import Server from './views/Server.vue'
import ServerRegistration from './views/ServerRegistration.vue'

Vue.use(Router);

const router = new Router({
    mode: "history",
    base: process.env.BASE_URL,
    routes: [
        // {
        //     path: "/login",
        //     name: "login",
        //     component: Login,
        //     meta: {
        //         layout: "default"
        //     }
        // },
        {
            path: "/",
            name: "home",
            component: Home,
            meta: {
                layout: "default",
                requiresAuth: false
            }
        },
        {
            path: "/package/:categoryId",
            name: "package",
            component: Package,
            meta: {
                layout: "default",
                requiresAuth: false
            }
        },
        {
            path: "/orderRequest",
            name: "orderRequest",
            component: OrderRequest,
            // props: (route) => ({
            //     ...route.params
            // }),
            meta: {
                layout: "default",
                requiresAuth: true
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
        {
            path: '/callback',
            name: 'callback',
            component: CallBack,
            meta: {
                layout: "default",
                requiresAuth: false
            }
        },
        {
            path: '/server',
            name: 'server',
            component: Server,
            meta: {
                layout: "default",
                requiresAuth: false
            }
        },
        {
            path: '/serverRegistration',
            name: 'serverRegistration',
            component: ServerRegistration,
            meta: {
                layout: "default",
                requiresAuth: false
            }
        },

    ],
});

router.beforeEach(async(to, from, next) => {
    let app = router.app.$data || { isAuthenticated: false };
    if (app.isAuthenticated) {
        //already signed in, we can navigate anywhere
        next()
    } else if (to.matched.some(record => record.meta.requiresAuth)) {
        //authentication is required. Trigger the sign in process, including the return URI
        router.app.authenticate(to.path).then(() => {
            console.log('authenticating a protected url:' + to.path);
            next();
        });
    } else {
        //No auth required. We can navigate
        next()
    }
});

export default router;