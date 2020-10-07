import serviceCallApi from "../framework/serviceCallApi";

let orderApi = {
    urls: {

        getAll: "/Orders",
        Add: "/Orders/",
        cancelFinanceRequest: "/Orders/CancelFinanceRequest"
    },
    getAll: async function() {
        return await serviceCallApi.get(this.urls.getAll, {});
    },

    makeFinanceRequest: async function(tradeId) {
        return await serviceCallApi.post(this.urls.makeFinanceRequest, tradeId, { headers: { "Content-Type": "application/json" } });
    },

};
export default orderApi;