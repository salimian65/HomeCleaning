import axios from "axios";
import {
  async
} from "q";

let supplierApi = {
  urls: {
    register: "/supplier/Register",
    uploadFile: "/supplier/UploadFile"
  },
  register: async function (retailer) {
    return await axios.post(this.urls.register, retailer);
  },
  uploadFile: async function (file) {
    file.append("param", "ramin");
    file.param = "gholi";
    return await axios.post(this.urls.uploadFile, file);
  }
};
export default supplierApi;
