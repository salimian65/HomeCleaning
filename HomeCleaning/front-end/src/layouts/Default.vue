<template>
  <div>
    <slot />
  </div>
</template>

<script>
export default {
  props: {
    source: String,
  },
  data: () => ({
    show: false,
    dialog: false,
    drawer: null,
  }),
  methods: {
    handleLogout: function () {
      this.$keycloak.logout();
    },
  },
};
</script>

<style lang="scss">
</style>
