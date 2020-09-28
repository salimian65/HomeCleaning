import axios from "axios";

export default {
  urls: {
    registerInvoice: "/invoice/RegisterInvoice",
    addLineItem: "/invoice/AddLineItem",
  },

  registerInvoice: async function (invoice) {
    return (await axios.post(this.urls.registerInvoice, invoice, {
      headers: {
        "Content-Type": "application/json"
      }
    })).data;
  },
  addLineItem: async function (lineItem) {
    return (await axios.post(this.urls.addLineItem, lineItem, {
      headers: {
        "Content-Type": "application/json"
      }
    })).data;
  },

};
