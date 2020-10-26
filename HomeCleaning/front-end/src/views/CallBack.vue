<template>
  <h3>Please wait, you are being redirected to the Home page</h3> 
</template>

<script>
 export default {
        async created() {
            try {
                var result = await this.$root.mgr.signinRedirectCallback();
                var returnToUrl = '/orderRequest';
                if (result.state !== undefined) { returnToUrl = result.state;}
                this.$router.push({ path: returnToUrl });
            } catch (e) {
                this.$router.push({ name: 'Unauthorized' });
            }
        }
    }
</script>

<style>

</style>