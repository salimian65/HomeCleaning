import axios from "axios";

let userApi = {
    urls: {

        getAll: "/users",
        add: "/user",
    },
    getAll: async function() {
        return await axios.get(this.urls.getAll, {});
    },
    add: async function(user) {
        return await axios.post(this.urls.add, user, { headers: { "Content-Type": "application/json" } });
    }
};
export default userApi;