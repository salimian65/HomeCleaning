<template>
  <v-container>
    <v-form @submit.prevent="register">
      <v-card>
        <v-col>
          <v-row>
            <legal-person v-bind:legalPerson.sync="recipient.legalPerson"></legal-person>
          </v-row>
        </v-col>
      </v-card>
      <v-card>
        <v-col>
          <v-row>
            <share-holder v-bind:legalPerson.sync="recipient.legalPerson"></share-holder>
          </v-row>
        </v-col>
      </v-card>
      <v-card>
        <v-col>
          <v-row>
            <ceo
              v-bind:ceo.sync="recipient.legalPerson.ceo"
              getCeoFromServer
            ></ceo>
          </v-row>
        </v-col>
      </v-card>
      <v-card>
        <v-col>
          <v-row>
            <operation-area v-bind:recipient.sync="recipient"></operation-area>
          </v-row>
        </v-col>
      </v-card>
      <v-card>
        <v-col>
          <v-row>
            <contact :contact="recipient.legalPerson.contact"></contact>
          </v-row>
        </v-col>
      </v-card>
      <v-btn
        type="submit"
        color="primary"
      >{{$t('buttons.register')}}</v-btn>
    </v-form>
  </v-container>
</template>

<script src="./registeration.js"></script>
