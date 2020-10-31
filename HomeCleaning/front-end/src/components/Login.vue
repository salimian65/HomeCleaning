<template>
  <v-btn icon @click="signIn" v-if="!$root.isAuthenticated" depressed>
    <v-icon>mdi-account</v-icon>
    {{ $t("labels.loginBtn") }}
  </v-btn>
  <v-btn text @click="signout" v-else depressed>
    <v-icon color="white">mdi-account-off</v-icon>
    {{ $root.user.name }}
  </v-btn>
</template>

<script>
export default {
  data() {
    return {
      // user: this.$root.user,
    };
  },
  methods: {
    async signIn() {
      try {
        await this.$root.signIn();
      } catch (error) {
        console.log(error);
      }
    },
    async signout() {
      try {
        await this.$root.signout();
      } catch (error) {
        console.log(error);
      }
    },
  },
  computed: {
    user: function () {
      return this.$root.getUser();
    },
  },
  // watch: {
  //   item: {
  //     handler(val) {
  //       this.$root.getUser();
  //     },
  //   },
  // },
  //  async mounted() {
  //     //this.user = await this.$root.mgr.getUser()
  //   },
};
</script>
<style>
.theme--light.v-btn.v-btn--icon {
  color: #fff;
  padding: 25px 30px;
}

.theme--light.v-btn {
  color: white;
}
</style>
