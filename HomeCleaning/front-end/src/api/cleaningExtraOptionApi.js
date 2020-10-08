import axios from "axios";

let invoiceApi = {
    urls: {
        getAll: "/CleaningExtraOptions",
        getByCategoryId: "/CleaningExtraOptions/GetCleaningExtraOptionsByCategoryId",
        makeFinanceRequest: "/CleaningExtraOptions/MakeFinanceRequest",
        cancelFinanceRequest: "/CleaningExtraOptions/CancelFinanceRequest"
    },

    getAll: async function() {
        return await axios.get(this.urls.getAll, {});
    },
    getByCategoryId: async function(categoryId) {
        return await axios.get(this.urls.getByCategoryId, {
            params: { categoryId }
        });
    },
    makeFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.makeFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    },
    cancelFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.cancelFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    }
};
export default invoiceApi;