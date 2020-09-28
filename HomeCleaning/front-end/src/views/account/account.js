import Edit from "./components/Edit";
import ChangePassword from "./components/ChangePassword";
import accountApi from "../../api/account";

export default {
  components: { Edit, ChangePassword },
  data () {
    return {
      userInfo: {},
      editDialogOpen: false,
      changePasswordDialogOpen: false
    };
  },
  methods: {
    getUserInfo: function () {
      var self = this;
      accountApi.getUserInfo().then(response => {
        self.userInfo = response.data;
      });
    }
  },
  mounted: function () {
    this.getUserInfo();
  }
};
