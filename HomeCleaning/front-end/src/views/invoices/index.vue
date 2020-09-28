<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="desserts"
      :items-per-page="5"
      :footer-props="{
      showFirstLastPage: true,
      firstIcon: 'mdi-arrow-collapse-left',
      lastIcon: 'mdi-arrow-collapse-right',
      prevIcon: 'mdi-minus',
      nextIcon: 'mdi-plus'
    }"
      class="elevation-1 dblue-style"
    >
      <template v-slot:item.statusTitle="{ item }">
        <div class="action-btns">
          <v-btn class="statusTitle" rounded outlined color="#6bb9c1">{{item.statusTitle}}</v-btn>
        </div>
      </template>

      <template v-slot:item.action="{ item }">
        <div class="action-btns">
          <v-btn class small rounded outlined color="#0a8ac7">
            <v-icon class="mr-2">mdi-file-edit-outline</v-icon>
          </v-btn>

          <v-btn class small rounded outlined color="#d0abab">
            <v-icon>mdi-delete-outline</v-icon>
          </v-btn>
        </div>

        <!-- <v-icon v-if="!item.financeRequested" medium class="mr-2" @click="makeFinanceRequest(item)">attach_money</v-icon>
        <v-icon color="red" v-if="item.financeRequested" medium class="mr-2" @click="cancelFinanceRequest(item)">attach_money</v-icon>-->
      </template>
    </v-data-table>
  </v-container>
</template>



<script src="./invoices.js"></script>

<style lang="scss" >
.dblue-style {
  thead.v-data-table-header {
    background-color: #aae8e3;
  }

  .v-data-footer {
    background-color: #aec5c4;
  }
  .v-data-table th {
    text-align: center !important;
    padding: 0px 5px;
  }

  .v-data-table th {
    height: 75px;
    padding: 0px 10px;
  }
  .action-btns {
    width: 200px;
    button {
      display: inline-block;
      margin-left: 10px;

      &.statusTitle {
        width: 100%;
        margin: 10px 0 0 0;
      }
    }
  }

  .v-data-table-header th.sortable {
    pointer-events: auto;
    cursor: pointer;
    outline: 0;
    min-width: 130px;
    text-align: center !important;
  }
}
</style>
