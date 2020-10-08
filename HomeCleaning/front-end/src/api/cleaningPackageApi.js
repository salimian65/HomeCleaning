import axios from "axios";

let invoiceApi = {
    urls: {
        getAll: "/CleaningPackages",
        getByCategoryId: "/CleaningPackages/GetCleaningPackagesByCategoryId",
        makeFinanceRequest: "/CleaningPackages/MakeFinanceRequest",
        cancelFinanceRequest: "/CleaningPackages/CancelFinanceRequest"
    },

    getAll: async function() {
        return await axios.get(this.urls.getAll, {});
    },
    getByCategoryId: async function(categoryId) {
        var ddd = await axios.get(this.urls.getByCategoryId, {
            params: { categoryId }
        });
        return ddd;
    },
    makeFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.makeFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    },
    cancelFinanceRequest: async function(tradeId) {
        return await axios.post(this.urls.cancelFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    }
};
export default invoiceApi;