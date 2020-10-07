<template>
  <v-row justify="center">
    <v-dialog v-model="open" max-width="290">
      <v-card>
        <v-card-title class="headline">{{title}}</v-card-title>

        <v-card-text>{{message}}</v-card-text>

        <v-card-actions>
          <div class="flex-grow-1"></div>
          <v-btn color="green darken-1" text @click="ok()">{{$t("buttons.accept")}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
  name: "AlertDialog",
  data () {
    return {
      open: false,
      title: "",
      message: ""
    };
  },
  methods: {
    show: function (message, title, onOk) {
      this.title = title;
      this.message = message;
      this.onOk = onOk;
      this.open = true;
    },
    ok: function () {
      if (this.onOk && typeof this.onOk === "function") {
        this.onOk();
      }
      this.open = false;
    }
  }
};
</script>
