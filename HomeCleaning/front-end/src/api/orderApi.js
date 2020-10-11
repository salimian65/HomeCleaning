import serviceCallApi from "../frameworks/serviceCallApi";

let orderApi = {
    urls: {
        resource: "/Orders/",
    },
    getAll: async function() {
        return await serviceCallApi.get(this.urls.resource, {});
    },

    add: async function(order) {
        return await serviceCallApi.post(this.urls.resource, order);
    },

};
export default orderApi;