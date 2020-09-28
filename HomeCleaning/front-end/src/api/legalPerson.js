import axios from "axios";

let legalPersonApi = {
  legalPersonUrls: {
    getByNationalCode: "/legalperson/getByNationalCode",
    GetById: "/legalperson/GetById",
    Register: "/legalperson/Register",
    AddLegalShareHolder: "/legalperson/AddLegalShareHolder",
    AddNaturalShareHolder: "/legalperson/AddNaturalShareHolder",
    Remove: "/legalperson/Remove",
    SearchPeople: "/legalperson/SearchPeople"
  },
  getByNationalCode: async function (nationalCode) {
    return (await axios.get(this.legalPersonUrls.getByNationalCode, {
      params: {
        nationalCode: nationalCode
      }
    })).data;
  }
};
export default legalPersonApi;
