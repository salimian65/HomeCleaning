<template>
  <div></div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import * as Keycloak from "keycloak-js";

export default {
  props: {
    source: String,
  },
  data: () => ({
    sidebarOpen: true,
    show: false,
  }),
  computed: {},

  methods: {
    handleLogout: function () {
      this.$keycloak.logout();
    },
    showMenu() {},
  },
  mounted: function () {
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

        localStorage.setItem("vue-token", keycloak.token);
        localStorage.setItem("vue-refresh-token", keycloak.refreshToken);

        axios.interceptors.request.use(
          (config) => {
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
      })

      .error(() => {
        console.log("Err: Authenticated Failed");
      });
  },
};
</script>

<style lang="scss">
</style>
