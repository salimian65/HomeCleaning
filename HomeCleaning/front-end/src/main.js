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

import AuthorizedCustomer from "./layouts/AuthorizedCustomer";
import DefaultLayout from "./layouts/Default";
import AlertDialog from "./components/AlertDialog";
import vueAwesomeCountdown from "vue-awesome-countdown";

Vue.config.productionTip = false

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

Vue.prototype.$baseUrl = "http://localhost:6003/";
Vue.prototype.$baseApiUrl = "http://localhost:6003/api";
axios.defaults.baseURL = "http://localhost:6003/api";

Vue.component("authorizedCustomer-layout", AuthorizedCustomer);
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
            this.$children[0].$refs.confirmationDialog.show(message, title, onAccept, onCancel);
        },
        showInfoToast: function(message) {
            this.$children[0].$refs.toast.show(message, "info");
        },
        showErrorToast: function(message) {
            this.$children[0].$refs.toast.show(message, "error");
        },
        showSuccessToast: function(message) {
            this.$children[0].$refs.toast.show(message, "success");
        }
    },
    created() {
        var self = this;
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
    }
}).$mount("#app");