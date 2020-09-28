<template>
 <div>
 <v-row align="center" justify="center">
 <v-col >
    <v-card

    class="mx-auto"
    max-width="344"
  >
  <v-img  class="d-none d-sm-flex"
      src="../assets/img/drugstore.jpg"
      height="200px"
    ></v-img>

   <v-list-item three-line>
      <v-list-item-avatar
        tile
        size="80"
        color="grey"
        class="d-flex d-sm-none"
      >
       <v-img
      src="../assets/img/drugstore.jpg"
    ></v-img>
    </v-list-item-avatar>

      <v-list-item-content>

        <v-list-item-title class="headline1 mb-1">
          <router-link to="register/retailer" class="register-card-link">
          <v-icon color="gray">mdi-bottle-tonic-plus-outline</v-icon>   {{$t("labels.pharmacy")}}
        </router-link>
        </v-list-item-title>

      </v-list-item-content>


    </v-list-item>
    <v-card-actions>

       <v-btn
        icon
        @click="show = !show"
      >
        <v-icon>{{ show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
      </v-btn>
      <v-spacer></v-spacer>

        <v-btn to="register/delegate/retailer"
         depressed small dark color="teal darken-1"
      >
        شروع
      </v-btn>

    </v-card-actions>

    <v-expand-transition>
      <div v-show="show">
        <v-divider></v-divider>

        <v-card-text>
          I'm a thing. But, like most politicians, he promised more than he could deliver. You won't have time for sleeping, soldier, not with all the bed making you'll be doing. Then we'll go with that data file! Hey, you add a one and two zeros to that or we walk! You're going to do his laundry? I've got to find a way to escape.
        </v-card-text>
      </div>
    </v-expand-transition>
  </v-card>
     </v-col>


 <v-col>
    <v-card
    class="mx-auto"
    max-width="344"
  >


  <v-img  class="d-none d-sm-flex"
      src="../assets/img/provider.jpg"
      height="200px"
    ></v-img>

   <v-list-item three-line>
      <v-list-item-avatar
        tile
        size="80"
        color="grey"
        class="d-flex d-sm-none"
      >
       <v-img
      src="../assets/img/provider.jpg"
    ></v-img>
    </v-list-item-avatar>

      <v-list-item-content>

        <v-list-item-title class="headline1 mb-1">
       <router-link to="register/supplier" class="register-card-link">
          <v-icon color="gray">mdi-domain</v-icon>  {{$t("labels.supplier")}}
        </router-link>
        </v-list-item-title>

      </v-list-item-content>


    </v-list-item>


    <v-card-actions>

       <v-btn
        icon
        @click="show = !show"
      >
        <v-icon>{{ show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
      </v-btn>
      <v-spacer></v-spacer>

        <v-btn to="register/delegate/supplier"
         depressed small dark color="teal darken-1"
      >
        شروع
      </v-btn>

    </v-card-actions>

    <v-expand-transition>
      <div v-show="show">
        <v-divider></v-divider>

        <v-card-text>
          I'm a thing. But, like most politicians, he promised more than he could deliver. You won't have time for sleeping, soldier, not with all the bed making you'll be doing. Then we'll go with that data file! Hey, you add a one and two zeros to that or we walk! You're going to do his laundry? I've got to find a way to escape.
        </v-card-text>
      </div>
    </v-expand-transition>
  </v-card>
     </v-col>



      <v-col>
    <v-card
    class="mx-auto"
    max-width="344"
  >
    <v-img  class="d-none d-sm-flex"
      src="../assets/img/drug.jpg"
      height="200px"
    ></v-img>

   <v-list-item three-line>
      <v-list-item-avatar
        tile
        size="80"
        color="grey"
        class="d-flex d-sm-none"
      >
       <v-img
      src="../assets/img/drug.jpg"
    ></v-img>
    </v-list-item-avatar>

      <v-list-item-content>

        <v-list-item-title class="headline1 mb-1">

     <router-link to="register/distributor" class="register-card-link">
          <v-icon color="gray">mdi-truck-outline</v-icon>  {{$t("labels.distributor")}}
        </router-link>
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>
    <v-card-actions>
       <v-btn
        icon
        @click="show = !show"
      >
        <v-icon>{{ show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
      </v-btn>
      <v-spacer></v-spacer>
        <v-btn to="register/delegate/distributor"
         depressed small dark color="teal darken-1"
      >
        شروع
      </v-btn>
    </v-card-actions>
    <v-expand-transition>
      <div v-show="show">
        <v-divider></v-divider>
        <v-card-text>
          I'm a thing. But, like most politicians, he promised more than he could deliver. You won't have time for sleeping, soldier, not with all the bed making you'll be doing. Then we'll go with that data file! Hey, you add a one and two zeros to that or we walk! You're going to do his laundry? I've got to find a way to escape.
        </v-card-text>
      </div>
    </v-expand-transition>
  </v-card>
     </v-col>
  </v-row>


  </div>
</template>

<script>
import router from "../router";

const ID_TOKEN_KEY = "id_token";
const ACCESS_TOKEN_KEY = "access_token";

export default {
   data: () => ({
           show: false,
            dialog: false,
            drawer: null,

        }),

  methods: {
    handleLogout: function () {
      this.$keycloak.logout();
    }
  },
  components: {

  }
};
</script>

<style lang="scss" >
.v-card__title a{
  font-size: 16px;
}

.register-card-link{
  color: #329688;
}

</style>
