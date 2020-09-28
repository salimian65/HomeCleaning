import axios from "axios";

export default {
  urls: {
    register: "/distributor/Register"
  },
  register: async function (distributor) {
    return await axios.post(this.urls.register, JSON.stringify(distributor), {
      headers: {
        "Content-Type": "application/json"
      }
    });
  }
};
