<template>
  <v-form
    @submit.prevent="register"
    style="width:100%"
  >
    <v-container fluid>
      <v-row>
        <v-col
          cols="12"
          xs="12"
          sm="12"
          md="12"
          lg="12"
          xl="12"
        >
          <v-row justify="center">
            <v-card class="ma-2 pa-4">
              <legal-person v-bind:legalPerson.sync="recipient.legalPerson"></legal-person>
              <recipient-identity v-bind:recipient.sync="recipient"></recipient-identity>
            </v-card>
          </v-row>
        </v-col>
      </v-row>
      <v-row>
        <v-col
          cols="12"
          xs="12"
          sm="12"
          md="12"
          lg="12"
          xl="12"
        >
          <v-card class="ma-2 pa-4">
            <retailer-share-holder
              v-bind:legalPerson.sync="recipient.legalPerson"
              getOwnerFromServer
            ></retailer-share-holder>
          </v-card>
        </v-col>
      </v-row>
      <v-row v-if="showCeo">
        <v-col
          cols="12"
          xs="12"
          sm="12"
          md="12"
          lg="12"
          xl="12"
        >
          <v-card class="ma-2 pa-4">
            <ceo
              v-bind:ceo.sync="recipient.legalPerson.ceo"
              getCeoFromServer
            ></ceo>
          </v-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col
          cols="12"
          xs="12"
          sm="12"
          md="12"
          lg="12"
          xl="12"
        >
          <v-card class="ma-2 pa-4">
            <shifts v-bind:recipient.sync="recipient"></shifts>
          </v-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col
          cols="12"
          xs="12"
          sm="12"
          md="12"
          lg="12"
          xl="12"
        >
          <v-card class="ma-2 pa-4">
            <contact :contact="recipient.legalPerson.contact"></contact>
          </v-card>
        </v-col>
      </v-row>
      <v-btn
        type="submit"
        color="primary"
      >{{$t('buttons.register')}}</v-btn>
    </v-container>
  </v-form>
</template>

<script src="./registeration.js"></script>
