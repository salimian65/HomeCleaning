<template>
  <div>
    <div class="text-center">
      <v-dialog
        v-model="showIntroDialog"
        persistent
        width="500"
      >
        <v-card>
          <v-card-title
            class="headline grey lighten-2"
            primary-title
          >
            {{$t('labels.notice')}}
          </v-card-title>
          <v-card-text>
            {{$t("views.registrant.registrationIntro")}}
          </v-card-text>
          <v-divider></v-divider>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              text
              @click="showIntroDialog = false"
            >
              {{$t("buttons.accept")}}
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
    <v-row>
      <v-col cols="3"></v-col>
      <v-col cols="6">
        <registrant
          v-if="registrant.verificationStatus===RegistrantStatus.NOT_REGISTERED"
          v-bind:registrant.sync="registrant"
          :register="register"
        ></registrant>
        <cell-verification
          v-if="registrant.verificationStatus===RegistrantStatus.CELLPHONE_NOT_VERIFIED"
          v-bind:registrant.sync="registrant"
          :changeInfo="changeInfo"
          :resendVerificationCode="resendCellVerificationCode"
          :verifyCellphone="verifyCellphone"
        ></cell-verification>
        <email-verification
          v-if="registrant.verificationStatus===RegistrantStatus.EMAIL_NOT_VERIFIED"
          v-bind:registrant.sync="registrant"
          :changeInfo="changeInfo"
          :resendVerificationEmail="resendVerificationEmail"
        ></email-verification>
        <finished
          v-if="registrant.verificationStatus===RegistrantStatus.FINISHED"
          :registrant="registrant"
          :recipientType="recipientType"
        ></finished>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import Registrant from "./components/Registrant";
import CellVerification from "./components/CellVerification";
import EmailVerification from "./components/EmailVerification";
import Finished from "./components/Finished";
import registrantApi from "../../../api/registrant";
import { RegistrantStatus, ApiResultStatus, EmptyGuid } from "../../../constants";

export default {
  props: {
    id: { type: String },
    recipientType: { type: String }
  },
  data: () => {
    return {
      registrant: {
        id: EmptyGuid,
        firstName: "",
        surname: "",
        cellphone: "",
        email: "",
        verificationCode: "",
        verificationStatus: RegistrantStatus.UNKNOWN
      },
      RegistrantStatus: RegistrantStatus,
      pageFirstTimeLoaded: true
    };
  },
  computed: {
    showIntroDialog: {
      get: function () {
        return this.pageFirstTimeLoaded && this.registrant.verificationStatus === RegistrantStatus.NOT_REGISTERED;
      },
      set: function (newValue) {
        this.pageFirstTimeLoaded = newValue;
      }
    }
  },
  components: {
    Registrant,
    CellVerification,
    EmailVerification,
    Finished
  },
  methods: {
    register: async function () {
      try {
        this.registrant = await registrantApi.register(this.registrant);
      } catch (error) {
        debugger;
      }
    },
    changeInfo: function () {
      this.registrant.verificationStatus = RegistrantStatus.NOT_REGISTERED;
    },
    resendCellVerificationCode: async function () {
      await registrantApi.sendSmsVerificationCode(this.registrant.id);
    },
    resendVerificationEmail: async function () {
      await registrantApi.sendEmailVerificationCode(this.registrant.id);
    },
    verifyCellphone: async function () {
      try {
        this.registrant = await registrantApi.verifyCellphone(this.registrant.id, this.registrant.verificationCode);
      } catch (error) {
        if (error.response && error.response.status == ApiResultStatus.BAD_REQUEST) {
          this.registrant.verificationCode = "";
        }
      }
    }
  },
  created: async function () {
    if (this.id) {
      this.registrant = await registrantApi.getRegistrant(this.id);
    } else {
      this.registrant.verificationStatus = RegistrantStatus.NOT_REGISTERED;
    }
  }
};
</script>
