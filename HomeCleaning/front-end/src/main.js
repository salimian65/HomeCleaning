import Vue from 'vue'
import App from './App.vue'

import router from "./router";
import vuetify from "./plugins/vuetify";
import i18n from "./i18n";
import "./main.scss";
import Vuelidate from "vuelidate";
import vuelidateErrorExtractor from "vuelidate-error-extractor";
import VuelidateErrorTemplate from "./VuelidateErrorTemplate";

import axios from "axios";
import ValidMember from "./layouts/ValidMember";
import DefaultLayout from "./layouts/Default";
import * as Keycloak from "keycloak-js";
import AlertDialog from "./components/AlertDialog";
import vueAwesomeCountdown from "vue-awesome-countdown";

Vue.config.productionTip = false

let ValidMemberLayouts = {
    en: "ValidMember",
    tr: "ValidMember",

};
Vue.use(Vuelidate);
Vue.use(vuelidateErrorExtractor, {
    /**
     * Optionally provide the template in the configuration.
     * or register like so Vue.component("FormField", templates.singleErrorExtractor.foundation6)
     */
    template: VuelidateErrorTemplate, //templates.singleErrorExtractor.foundation6, // you can also pass your own custom template
    i18n: "validation",
});

Vue.use(vueAwesomeCountdown, "vac");

Vue.prototype.$baseUrl = "http://localhost:6001";
Vue.prototype.$baseApiUrl = "http://localhost:6001/api";
axios.defaults.baseURL = "http://localhost:6001/api";

Vue.component("ValidMember-layout", ValidMember);
Vue.component("default-layout", DefaultLayout);

new Vue({
    router,
    vuetify,
    i18n,
    components: {
        AlertDialog,
    },
    render: (h) => h(App),
    methods: {
        alert: function(message, title, onOk) {
            this.$children[0].$refs.alertDialog.show(message, title, onOk);
        },
        getConfirmation: function(message, title, onAccept, onCancel) {
            this.$children[0].$refs.confirmationDialog.show(
                message,
                title,
                onAccept,
                onCancel
            );
        },
        showInfoToast: function(message) {
            this.$children[0].$refs.toast.show(message, "info");
        },
        showErrorToast: function(message) {
            this.$children[0].$refs.toast.show(message, "error");
        },
        showSuccessToast: function(message) {
            this.$children[0].$refs.toast.show(message, "success");
        },
        setCurrentLanguage: function() {
            if (this.$route.params.locale) {
                i18n.locale = this.$route.params.locale;
            }
        },
        changeActiveLanguage: function(locale) {
            this.$router.push({
                name: this.$route.name,
                params: { locale: locale },
            });
            this.$router.go(0);
        },
        getSuitableLayout: function(key) {
            switch (key) {
                case "validMember":
                    return ValidMemberLayouts[i18n.locale];
                case "default":
                    return "default";
                default:
                    return "default";
            }
        },
        getLocale: function() {
            return i18n.locale;
        },
    },
    created() {
        //  var self = this;

        this.setCurrentLanguage();

        router.beforeEach((to, from, next) => {
            this.setCurrentLanguage();
            next();
        });

    },
    whatch: {
        "$route.params.locale": function(newValue) {
            alert(newValue);
        },
    },
}).$mount("#app");



let initOptions = {
    url: "http://31.47.53.46:8597/auth",
    realm: "moneymaker",
    clientId: "moneymaker",
    onLoad: "login-required",
};

let keycloak = Keycloak(initOptions);

keycloak
    .init({
        //onLoad: initOptions.onLoad,
    })
    .success((auth) => {
        if (!auth) {
            //  window.location.reload();
        } else {
            console.log("Authenticated");
        }

        Vue.prototype.$keycloak = keycloak;

        // router.beforEach((to, from, next) => {
        //
        // });

        localStorage.setItem("vue-token", keycloak.token);
        localStorage.setItem("vue-refresh-token", keycloak.refreshToken);

        setTimeout(() => {
            keycloak
                .updateToken(70)
                .success((refreshed) => {
                    if (refreshed) {
                        console.log("Token refreshed" + refreshed);
                    } else {
                        console.log(
                            "Warn: Token not refreshed, valid for " +
                            Math.round(
                                keycloak.tokenParsed.exp +
                                keycloak.timeSkew -
                                new Date().getTime() / 1000
                            ) +
                            " seconds"
                        );
                    }
                })
                .error(() => {
                    console.log("Err: Failed to refresh token");
                });
        }, 60000);

        axios.interceptors.request.use(
            (config) => {
                if (this.$route.params.locale) {
                    config.headers.culture = this.$route.params.locale;
                }
                config.headers.Authorization = `Bearer ${keycloak.token}`;
                return config;
            },
            (error) => {
                return Promise.reject(error);
            }
        );

        axios.interceptors.response.use(
            function(response) {
                // Any status code that lie within the range of 2xx cause this function to trigger
                return response;
            },
            function(error) {
                // Any status codes that falls outside the range of 2xx cause this function to trigger
                var message = error.message;
                if (error.response && error.response.data)
                    message = error.response.data.Message;

                self.showErrorToast(message);
                return Promise.reject(error);
            }
        );

    })

.error(() => {
    console.log("Err: Authenticated Failed");
});