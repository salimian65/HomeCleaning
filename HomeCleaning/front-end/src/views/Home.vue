<template>
 <div>
 <v-row align="center" justify="center" class="row-eq"> 

   <!-- ---------------card1---------------- -->
    <v-col >

  <v-card
    class="mx-auto"
    max-width="400"
  >
    <v-list-item two-line>
      <v-list-item-content>
        <v-list-item-title class="headline">تعداد تراکنش ها</v-list-item-title>
        <v-list-item-subtitle>آخرین تراکنش های این هفته</v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>

    <v-card-text>
      <v-row align="center">
        <v-col class="display-3" cols="6">
         <v-icon>mdi-archive-arrow-down-outline</v-icon> 758 
        </v-col>
        <v-col class="display-3" cols="6">
       223 <v-icon>mdi-archive-arrow-up-outline</v-icon>
        </v-col>
      </v-row>
    </v-card-text>

    
    <v-divider></v-divider>

    <v-card-actions>
      <v-btn text>فهرست تراکنش ها</v-btn>
    </v-card-actions>
  </v-card>

  
     </v-col>











<!-- ---------------card2---------------- -->
     <v-col >

  <v-card
    class="mx-auto"
    max-width="400"
  >
    <v-list-item two-line>
      <v-list-item-content>
        <v-list-item-title class="headline">تعداد تراکنش ها</v-list-item-title>
        <v-list-item-subtitle>آخرین تراکنش های این هفته</v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>

    <v-card-text>
      <v-row align="center">
        <v-col class="display-3" cols="6">
         <v-icon>mdi-archive-arrow-down-outline</v-icon> 758 
        </v-col>
        <v-col class="display-3" cols="6">
       223 <v-icon>mdi-archive-arrow-up-outline</v-icon>
        </v-col>
      </v-row>
    </v-card-text>

    
    <v-divider></v-divider>

    <v-card-actions>
      <v-btn text>فهرست تراکنش ها</v-btn>
    </v-card-actions>
  </v-card>

  
     </v-col>

<!-- ---------------card3---------------- -->

     <v-col >

  <v-card
    class="mx-auto"
    max-width="400"
  >
    <v-list-item two-line>
      <v-list-item-content>
        <v-list-item-title class="headline">تعداد تراکنش ها</v-list-item-title>
        <v-list-item-subtitle>آخرین تراکنش های این هفته</v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>

    <v-card-text>
      <v-row align="center">
        <v-col class="display-3" cols="6">
         <v-icon>mdi-archive-arrow-down-outline</v-icon> 758 
        </v-col>
        <v-col class="display-3" cols="6">
       223 <v-icon>mdi-archive-arrow-up-outline</v-icon>
        </v-col>
      </v-row>
    </v-card-text>

    
    <v-divider></v-divider>

    <v-card-actions>
      <v-btn text>فهرست تراکنش ها</v-btn>
    </v-card-actions>
  </v-card>

  
     </v-col>



  </v-row>
 
    
  </div>
</template>

<script>

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
