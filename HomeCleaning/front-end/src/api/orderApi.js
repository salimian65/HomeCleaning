// import serviceCallApi from "../frameworks/serviceCallApi";
import axios from "axios";
let orderApi = {
  urls: {
    resource: "/orders/",
  },
  getNewOrder: async function(pageNumber, pageSize) {
    return await axios.get(this.urls.resource, {
      params: {
        pageNumber,
        pageSize,
      },
    });
  },

  add: async function(order) {
    return await axios.post(this.urls.resource, order, {
      headers: { "Content-Type": "application/json" },
    });
  },
};
export default orderApi;
