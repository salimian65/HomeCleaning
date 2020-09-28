import axios from "axios";

export default {
  urls: {
    getById: "/recipient/GetById",
    search: "/recipient/Search"
  },
  search: async function (term, pageNumber, pageSize, sortBy, sortDesc) {
    return (await axios.get(this.urls.search, {
      params: {
        term: term,
        pageNumber: pageNumber,
        pageSize: pageSize,
        sortBy: sortBy,
        sortDesc: sortDesc
      }
    })).data;
  }
};
