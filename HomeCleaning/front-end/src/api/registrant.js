import axios from "axios";

export default {
  urls: {
    register: "/registrant/Register",
    getRegistrant: "/registrant/GetRegistrant",
    sendEmailVerificationCode: "/registrant/SendEmailVerificationCode",
    sendSmsVerificationCode: "/registrant/SendSmsVerificationCode",
    verifyCellphone: "/registrant/VerifyCellphone"
  },
  register: async function (registrant) {
    return (await axios.post(this.urls.register, registrant)).data;
  },
  getRegistrant: async function (id) {
    return (await axios.get(this.urls.getRegistrant, {
      params: {
        id
      }
    })).data;
  },
  sendEmailVerificationCode: async function (cellphone) {
    return await axios.post(this.urls.sendEmailVerificationCode, cellphone);
  },
  sendSmsVerificationCode: async function (registrantId) {
    const config = {
      headers: {
        "Content-Type": "application/json",
      },
    };
    return (await axios.post(this.urls.sendSmsVerificationCode, JSON.stringify(registrantId), config)).data;
  },
  verifyCellphone: async function (registrantId, verificationCode) {
    return (await axios.post(this.urls.verifyCellphone, {
      registrantId,
      verificationCode
    })).data;
  }
};
