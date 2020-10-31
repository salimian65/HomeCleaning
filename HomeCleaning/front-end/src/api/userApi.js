import axios from "axios";

let userApi = {
    urls: {

        getAll: "/user",
        createServer: "/user/createServer",
        createCustomer: "/user/createCustomer",
    },
    getAll: async function() {
        return await axios.get(this.urls.getAll, {});
    },
    createServer: async function(user) {
        return await axios.post(this.urls.createServer, user, { headers: { "Content-Type": "application/json" } });
    },
    createCustomer: async function(user) {
        return await axios.post(this.urls.createCustomer, user, { headers: { "Content-Type": "application/json" } });
    }
};
export default userApi;