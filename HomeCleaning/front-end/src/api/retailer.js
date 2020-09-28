import axios from "axios";

export default {
  urls: {
    getById: "/retailer/GetById",
    getPersonByNationalCode: "/retailer/GetRetailerByNationalCode",
    register: "/retailer/Register",
    remove: "/retailer/Remove",
    searchPeople: "/retailer/SearchRetailers"
  },
  register: async function (retailer) {
    return await axios.post(this.urls.register, JSON.stringify(retailer), {
      headers: {
        "Content-Type": "application/json"
      }
    });
  }
};
