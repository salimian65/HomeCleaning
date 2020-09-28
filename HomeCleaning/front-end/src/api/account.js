import axios from "axios";

let accountApi = {
  urls: {
    getUserInfo: "/account/GetUserInfo"
  },
  getUserInfo: function () {
    return axios.get(this.urls.getUserInfo);
  }
};
export default accountApi;
