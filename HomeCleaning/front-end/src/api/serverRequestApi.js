// import serviceCallApi from "../frameworks/serviceCallApi";
import axios from "axios";
let serverRequestApi = {
    urls: {
        resource: "/ServerRequests",
    },
    getNewOrder: async function(pageNumber, pageSize) {
        return await axios.get(this.urls.resource, {
            params: {
                pageNumber,
                pageSize
            }
        });
    },

    add: async function(orderId) {
        return await axios.post(this.urls.resource,
            orderId, { headers: { "Content-Type": "application/json" } });
    },

};
export default serverRequestApi;