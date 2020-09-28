import axios from "axios";

export default {
  urls: {
    getAllProvinces: "/area/GetAllProvinces",
    getCountiesForProvince: "/area/GetCountiesForProvince",
    getCitiesForCounty: "/area/GetCitiesForCounty"
  },
  getAllProvinces: async function () {
    return (await axios.get(this.urls.getAllProvinces)).data.result;
  },
  getCountiesForProvince: async function (provinceId) {
    return (await axios.get(this.urls.getCountiesForProvince, {
      params: {
        provinceId: provinceId
      }
    })).data.result;
  },
  getCitiesForCounty: async function (countyId) {
    return (await axios.get(this.urls.getCitiesForCounty, {
      params: {
        countyId: countyId
      }
    })).data.result;
  }
};
