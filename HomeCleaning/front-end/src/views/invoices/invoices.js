import invoiceApi from "../../api/invoice";

export default {
  data() {
    return {
      headers: [
        {
          text: "ارسال کننده",
          sortable: false,
          value: "sourceName"
        },
        {text: "گیرنده", value: "destinationName"},
        {text: "کالا", value: "productFaName"},
        {text: "IRC", value: "irc"},
        {text: "تعداد/ مقدار", value: "amount"},
        {text: "بچ", value: "batchNo"},
        {text: "ارزش", value: "value"},
        {text: "تاریخ تراکنش", value: "eventFaTime"},
        {text: "سر رسید", value: "usanceDate"},
        {text: "وضعیت", value: "statusTitle"},
        {text: "عملیات", value: "action", sortable: false}
      ],
      desserts: [
        {
          sourceName: "Frozen Yogurt",
          destinationName: 159,
          productFaName: "آترواستاتین",
          irc: "24234242545",
          amount: 4200,
          batchNo: "23584",
          value: 1500,
          eventFaTime: "1398/02/18",
          usanceDate: "1398/09/18",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Ice cream sandwich",
          destinationName: 237,
          productFaName: 9.0,
          irc: 37,
          amount: 4.3,
          batchNo: "1%",
          statusTitle: "در انتضار تامین کننده"
        },
        {
          sourceName: "Eclair",
          destinationName: 262,
          productFaName: 16.0,
          irc: 23,
          amount: 6.0,
          batchNo: "7%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Cupcake",
          destinationName: 305,
          productFaName: 3.7,
          irc: 67,
          amount: 4.3,
          batchNo: "8%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Gingerbread",
          destinationName: 356,
          productFaName: 16.0,
          irc: 49,
          amount: 3.9,
          batchNo: "16%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Jelly bean",
          destinationName: 375,
          productFaName: 0.0,
          irc: 94,
          amount: 0.0,
          batchNo: "0%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Lollipop",
          destinationName: 392,
          productFaName: 0.2,
          irc: 98,
          amount: 0,
          batchNo: "2%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Honeycomb",
          destinationName: 408,
          productFaName: 3.2,
          irc: 87,
          amount: 6.5,
          batchNo: "45%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "Donut",
          destinationName: 452,
          productFaName: 25.0,
          irc: 51,
          amount: 4.9,
          batchNo: "22%",
          statusTitle: "تایید شده"
        },
        {
          sourceName: "KitKat",
          destinationName: 518,
          productFaName: 26.0,
          irc: 65,
          amount: 7,
          batchNo: "6%",
          statusTitle: "تایید شده"
        }
      ]
    };
  },
  methods: {
    makeFinanceRequest1: function () {
      alert("Give me some money");
    },
    makeFinanceRequest: async function (item) {
      var self = this;
      this.$root.getConfirmation("درخواست خرید دین ارسال شود ؟", "", async function () {
          await invoiceApi.makeFinanceRequest(item.id);
          item.financeRequested = true;
          self.$root.alert("درخواست شما با موفقیت انجام شد");
        }
      );
    },
    sendFinanceRequest: async function (trade) {
      await invoiceApi.makeFinanceRequest(trade.id);
      trade.financeRequested = true;
      this.$root.alert("درخواست شما با موفقیت انجام شد");
    },
    cancelFinanceRequest: async function (item) {
      var self = this;
      this.$root.getConfirmation("درخواست خرید دین لغو شود ؟", "", async function () {
          await invoiceApi.cancelFinanceRequest(item.id);
          item.financeRequested = false;
          self.$root.alert("درخواست شما با موفقیت انجام شد");
        }
      );

    }
  },
  async mounted() {
    var trdeResponse = await invoiceApi.getByCompanyId(44);
    this.desserts = trdeResponse.data.result;
  }
};
