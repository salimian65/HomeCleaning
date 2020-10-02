<template>
  <v-row justify="center">
    <v-dialog v-model="open" max-width="290">
      <v-card>
        <v-card-title class="headline">{{title}}</v-card-title>

        <v-card-text>{{message}}</v-card-text>

        <v-card-actions>
          <div class="flex-grow-1"></div>

          <v-btn color="green darken-1" text @click="cancel()">{{$t("buttons.cancel")}}</v-btn>

          <v-btn color="green darken-1" text @click="accept()">{{$t("buttons.accept")}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
export default {
  data () {
    return {
      open: false,
      title: "",
      message: "",
      onAccept: null,
      onCancel: null
    };
  },
  methods: {
    show: function (message, title, onAccept, onCancel) {
      this.title = title;
      this.message = message;
      this.onAccept = onAccept;
      this.onCancel = onCancel;
      this.open = true;
    },
    accept: function () {
      if (this.onAccept && typeof this.onAccept === "function") {
        this.onAccept();
      }
      this.open = false;
    },
    cancel: function () {
      if (this.onCancel && typeof this.onCancel === "function") {
        this.onCancel();
      }
      this.open = false;
    }
  }
};
</script>
