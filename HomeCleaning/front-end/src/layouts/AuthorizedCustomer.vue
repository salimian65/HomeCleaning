<template>
  <div>  
    <slot />
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";


export default {
  props: {
   // source: String,
  },
  data: () => ({
    sidebarOpen: true,
    show: false,
  }),
  computed: {},

  methods: {
   
    showMenu() {},
  },
  mounted: function () {
   
  },
};
</script>

<style lang="scss">
</style>
