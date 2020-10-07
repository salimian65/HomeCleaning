<template>
  <div>
    <component :is="layout">
      <router-view/>
    </component>
    <alert-dialog ref="alertDialog"></alert-dialog>
    <confirmation-dialog ref="confirmationDialog"></confirmation-dialog>
    <toast ref="toast"></toast>
  </div>
</template>

<script>
import AlertDialog from "./components/AlertDialog";
import ConfirmationDialog from "./components/ConfirmationDialog";
import Toast from "./components/Toast";

const defaultLayout = "default";
export default {
  components: { AlertDialog, ConfirmationDialog, Toast },
  data: function () {
    return {};
  },
  computed: {
    layout () {
      return (this.$route.meta.layout || defaultLayout) + "-layout";
    }
  }
};
</script>
