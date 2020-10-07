<template>
  <div class="text-center ma-2">
<!--    <v-btn dark @click="show('gholi')">Open Snackbar</v-btn>-->
    <v-snackbar
      v-model="snackbar"
      :color="color"
    >
      {{ message }}
      <v-btn
        dark
        text
        @click="snackbar = false"
      >
        x
      </v-btn>
    </v-snackbar>
  </div>
</template>

<script>
export default {
  name: "Toast",
  data: function () {
    return {
      snackbar: false,
      message: "Hello, I'm a snackbar",
      color: "info"
    };
  },
  methods: {
    show: function (message, color) {
      this.message = message;
      this.snackbar = true;
      this.color = (color) || "info";
    }
  }
};
</script>

