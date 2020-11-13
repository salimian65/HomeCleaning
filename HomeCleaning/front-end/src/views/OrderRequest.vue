<template>
  <v-container class="orderRequest" style="padding: 100px 0 0 0">
    <v-card class="mx-auto my-12">
      <v-card-title>Confirm Your Request</v-card-title>
      <v-card-actions>
        <v-simple-table style="width: 600px">
          <template v-slot:default>
            <tbody>
              <tr>
                <td style="width: 200px">Cleaning Category</td>
                <td style="width: 400px">
                  {{ order.cleaningCategorySelected.name }}
                </td>
              </tr>
              <tr>
                <td>Space Size</td>
                <td>
                  {{ order.cleaningSpaceSizeSelected.name }}
                </td>
              </tr>
              <tr>
                <td>Cleaning Package</td>
                <td>
                  {{ order.cleaningPackageSelected.name }}
                </td>
              </tr>
              <tr>
                <td>Extra Options</td>
                <td>
                  {{ order.cleaningExtraOptionSelected.name }}
                </td>
              </tr>
               <tr>
                <td>Schedule Time</td>
                <td>
                  {{ order.scheduledDate }}  {{ order.scheduledTime }}
                </td>
              </tr>
              <tr>
                <td style="background-color: silver" colspan="2">Customer</td>
              </tr>
              <tr>
                <td>First Name</td>
                <td>
                  {{ order.customer.firstName }}
                </td>
              </tr>
              <tr>
                <td>Last Name</td>
                <td>
                  {{ order.customer.lastName }}
                </td>
              </tr>
              <tr>
                <td>First Name</td>
                <td>
                  {{ order.customer.firstName }}
                </td>
              </tr>
              <tr>
                <td>Cellphone</td>
                <td>
                  {{ order.customer.cellphone }}
                </td>
              </tr>
              <tr>
                <td>Address</td>
                <td>
                  {{ order.customer.address }}
                </td>
              </tr>
              <tr>
                <td style="background-color: silver">price</td>
                <td style="background-color: silver">
                  <font size="13px">TRY 99.99 </font>
                </td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-card-actions>
      <v-card-actions>
        <ul class="packages">
          <li>
            <label :for="1">
              <img src="../assets/img/visa.jpg" />
              <input type="radio" id="1" value="true" name="cleaningPackage" />
            </label>
          </li>
          <li>
            <label :for="2">
              <img src="../assets/img/masterCard.jpg" />
              <input type="radio" id="2" value="False" name="cleaningPackage" />
            </label>
          </li>
          <li>
            <label :for="3">
              <img src="../assets/img/paypal.jpg" />
              <input type="radio" id="3" value="False" name="cleaningPackage" />
            </label>
          </li>
        </ul>
      </v-card-actions>
      <v-card-actions>
        <v-btn large color="primary" v-if="isNotEditable" @click="submit()">{{
          $t("buttons.goForPayment")
        }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>
<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf,
} from "vuelidate/lib/validators";
import { ValidationLength, ProprietorshipType } from "../constants";

import cleaningCategoryApi from "../api/cleaningCategoryApi";
import orderApi from "../api/orderApi";
import orderModel from "../models/orderModel";

export default {
  props: {
    // order: {},
  },
  components: {},
  data: function () {
    return {
      isNotEditable: true,
      dialog: false,
      order: {
        customer: {
          firstName: "",
          lastName: "",
          cellphone: "",
        },
        scheduledDate:"",
        scheduledTime:"",
        cleaningCategorySelected: {
          name: "",
          id: "",
        },
        cleaningSpaceSizeSelected: {
          name: "",
          id: "",
        },
        cleaningPackageSelected: {
          name: "",
          id: "",
        },
        cleaningExtraOptionSelected: {
          name: "",
          id: "",
        },
        cleaningExtraOptionArraySelected: [],
      },
    };
  },
  methods: {
    async submit() {
      // const orderInstance = new orderModel.order(
      //   this.spaceSizeSelected,
      //   this.cleaningPackageSelected,
      //   this.cleaningExtraOptionSelected
      // );

      var orderdto = {
        spaceSizeId: this.order.cleaningSpaceSizeSelected.id,
        cleaningPackageId: this.order.cleaningPackageSelected.id,
        CleaningExtraOptionId: this.order.cleaningExtraOptionArraySelected[0]
          .id,
        price: 99.99,
        discount: 0.0,
        totalPrice: 99.99,
        tax: 0.0,
        scheduledTime:this.order.scheduledDate +"T"+ this.order.scheduledTime+":00",
        address: {
          addressStr: this.order.customer.address,
        },
      };

      var response = await orderApi.add(orderdto);
      if (response.status == 201) {
        this.$root.showSuccessToast("your order successfully registered");
      }
    },
  },

  async mounted() {
    var order = JSON.parse(localStorage.getItem("order"));

    let extraOptionName = "";
    let extraOptionId = "";
    order.cleaningExtraOptionArraySelected = order.cleaningExtraOptionSelected;
    order.cleaningExtraOptionSelected.forEach((a) => {
      extraOptionName += a.name + ",";
      extraOptionId += a.id + ",";
    });

    order.cleaningExtraOptionSelected = {
      name: extraOptionName,
      id: extraOptionId,
    };

    this.order = order;
  },
};
</script>
<style scoped>
.orderRequest td {
  border-left: 1px solid silver;
  border-bottom: 1px solid silver;
}

.orderRequest td:last-child {
  border-right: 1px solid silver;
}

.orderRequest tr:first-child {
  border-top: 1px solid silver;
}
</style>
