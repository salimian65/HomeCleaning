import axios from "axios";

let invoiceApi = {
    urls: {

        getAll: "/CleaningCategories",
        makeFinanceRequest: "/CleaningCategories/MakeFinanceRequest",
        cancelFinanceRequest: "/CleaningCategories/CancelFinanceRequest"
    },
    getAll: async function() {
        return await axios.get(this.urls.getAll, {});
    },
    makeFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.makeFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    },
    cancelFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.cancelFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    }
};
export default invoiceApi;