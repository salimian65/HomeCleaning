// import serviceCallApi from "../frameworks/serviceCallApi";
import axios from "axios";
let emailApi = {
    urls: {
        resource: "/email",
    },
    confirmEmail: async function(token, email) {
        return await axios.get(this.urls.resource, {
            params: {
                token,
                email
            }
        }, { headers: { "Content-Type": "application/json" } });
    },
};
export default emailApi;