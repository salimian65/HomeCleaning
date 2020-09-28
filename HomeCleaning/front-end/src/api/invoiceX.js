import axios from "axios";

let invoiceApi = {
  urls: {
    getByCompanyId: "/invoiceX/GetByCompanyId",
    makeFinanceRequest: "/invoiceX/MakeFinanceRequest",
    cancelFinanceRequest: "/invoiceX/CancelFinanceRequest"
  },
  getByCompanyId: async function (companyId, arrival, sendOut, pageNumber, pageSize) {
    return await axios.get(this.urls.getByCompanyId, {
      params: {
        companyId,
        arrival,
        sendOut,
        pageNumber,
        pageSize
      }
    });
  },
  makeFinanceRequest: async function (tradeId) {
    return await axios.post(this.urls.makeFinanceRequest, tradeId, {headers: {"Content-Type": "application/json"}});
  },
  cancelFinanceRequest: async function (tradeId) {
    return await axios.post(this.urls.cancelFinanceRequest, tradeId, {headers: {"Content-Type": "application/json"}});
  }
};
export default invoiceApi;
