import axios from "axios";

export default {
  urls: {
    getPersonByNationalCode: "/naturalperson/GetByNationalCode",
    getById: "/naturalperson/GetById",
    register: "/naturalperson/Register",
    remove: "/naturalperson/Remove",
    searchPeople: "/naturalperson/SearchPeople"
  },
  getByNationalCode: async function (nationalCode) {
    return (await axios.get(this.urls.getPersonByNationalCode, {
      params: {
        nationalCode: nationalCode
      }
    })).data;
  }
};
