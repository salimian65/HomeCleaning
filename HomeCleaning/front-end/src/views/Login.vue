<template>
  <form style="width: 100%">
    <form-group :name="$t('labels.userName')">
      <v-text-field :label="$t('labels.userName')" outlined></v-text-field>
    </form-group>
    <form-group :name="$t('labels.password')">
      <v-text-field
        :label="$t('labels.password')"
        required
        outlined
      ></v-text-field>
    </form-group>

    <form-group class="text-left">
      <v-btn class="login-btn" large dark="" @click="submit()" color="pink darken-3">{{
        $t("labels.loginBtn")
      }}</v-btn>
    </form-group>
  </form>
</template>

<script>
//import router from "../router";

export default {
  data: () => ({}),

  methods: {
submit(){
    this.$router.push({
        name: "orderRequest",
       // params: { order: orderInstance },
      });
}
  },
  components: {},
};
</script>

<style lang="scss" >
.login-bg {
  position: relative;
  position: absolute;
  display: inline-block;
  left: 0;
  right: 0;
  top: 0;

  &::after {
    background-image: url(/img/drugstore.5e55cc76.jpg);
    background-size: cover;
    width: 100%;
    min-width: 100%;
    height: 100%;
    min-height: 800px;
    position: absolute;
    display: inline-block;
    left: 0;
    right: 0;
    top: 0;
    background-position: center;
  }
}

.login-wrapper {
  background-color: #ffffffeb;
  position: absolute;
  right: 10%;
  top: 10%;
  min-width: 350px;
  padding: 15px;
  min-height: 400px;
}
</style>
