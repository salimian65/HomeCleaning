import axios from "axios";

export default {
  urls: {
    getByIrc: "/ttac/GetByIrc",
    getByName: "/ttac/GetByName",
  },
  getbyIrc: async function (irc) {
    return (await axios.get(this.urls.getByIrc, {
      params: {
        irc: irc
      }
    })).data;
  }
};
