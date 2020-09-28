<template>
  <div>
    <label>سهامدار</label>
    <form-group
      :validator="$v.proprietorship"
      :name="$t('labels.proprietorship')"
    >
      <v-select
        slot-scope="{ attrs }"
        outlined
        v-bind="attrs"
        v-model="localstate.proprietorship"
        :items="proprietorshipTypes"
        item-text="label"
        item-value="value"
        :label="$t('labels.proprietorship')"
      ></v-select>
    </form-group>
    <div v-if="localstate.proprietorship === ProprietorshipType.PRIVATE_NATURAL_PERSON">
      <form-group
        :validator="$v.naturalShareHolder.nationalCode"
        :name="$t('labels.nationalCode')"
      >
        <v-text-field
          outlined
          slot-scope="{ attrs }"
          v-bind="attrs"
          dir="ltr"
          v-model="localstate.naturalShareHolder.nationalCode"
          :counter="$v.naturalShareHolder.nationalCode.$params.maxLength.max"
          :maxLength="$v.naturalShareHolder.nationalCode.$params.maxLength.max"
          :minlength="$v.naturalShareHolder.nationalCode.$params.minLength.min"
          v-on:blur="getNaturalPerson"
          :label="$t('labels.nationalCode')"
          v-mask="'##########'"
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.naturalShareHolder.firstName"
        :name="$t('labels.shareHolderFirstName')"
      >
        <v-text-field
          outlined
          slot-scope="{ attrs }"
          v-bind="attrs"
          v-model.trim="localstate.naturalShareHolder.firstName"
          :counter="$v.naturalShareHolder.firstName.$params.maxLength.max"
          :maxLength="$v.naturalShareHolder.firstName.$params.maxLength.max"
          :minlength="$v.naturalShareHolder.firstName.$params.minLength.min"
          :readonly="readonly"
          :label="$t('labels.shareHolderFirstName')"
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.naturalShareHolder.surname"
        :name="$t('labels.shareHolderSurName')"
      >
        <v-text-field
          outlined
          v-model.trim="localstate.naturalShareHolder.surname"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :counter="$v.naturalShareHolder.surname.$params.maxLength.max"
          :readonly="readonly"
          :maxLength="$v.naturalShareHolder.surname.$params.maxLength.max"
          :minlength="$v.naturalShareHolder.surname.$params.minLength.min"
          :label="$t('labels.shareHolderSurName')"
        ></v-text-field>
      </form-group>
    </div>
    <div v-if="localstate.proprietorship === ProprietorshipType.PRIVATE_LEGAL_PERSON || localstate.proprietorship === ProprietorshipType.PUBLIC">
      <form-group
        :validator="$v.legalShareHolder.nationalCode"
        :name="$t('labels.legalPersonNationalCode')"
      >
        <v-text-field
          outlined
          v-model="localstate.legalShareHolder.nationalCode"
          :counter="$v.legalShareHolder.nationalCode.$params.maxLength.max"
          slot-scope="{ attrs }"
          dir="ltr"
          v-bind="attrs"
          v-on:blur="getLegalPerson"
          :maxLength="$v.legalShareHolder.nationalCode.$params.maxLength.max"
          :minlength="$v.legalShareHolder.nationalCode.$params.minLength.min"
          :label="$t('labels.legalPersonNationalCode')"
          v-mask="'###########'"
        ></v-text-field>
      </form-group>
      <form-group
        :validator="$v.legalShareHolder.officialName"
        :name="$t('labels.shareHolderName')"
      >
        <v-text-field
          outlined
          v-model.trim="localstate.legalShareHolder.officialName"
          slot-scope="{ attrs }"
          v-bind="attrs"
          :counter="$v.legalShareHolder.officialName.$params.maxLength.max"
          :readonly="readonly"
          :maxLength="$v.legalShareHolder.officialName.$params.maxLength.max"
          :minlength="$v.legalShareHolder.officialName.$params.minLength.min"
          :label="$t('labels.shareHolderName')"
        ></v-text-field>
      </form-group>
    </div>
  </div>
</template>

<script>
import {
  required
} from "vuelidate/lib/validators";
import { ProprietorshipType } from "../../../../constants";
import naturalPersonApi from "../../../../api/naturalPerson";
import legalPersonApi from "../../../../api/legalPerson";
import { mask } from "vue-the-mask";

export default {
  name: "RetailerShareHolder",
  props: {
    legalPerson: { type: Object, required },
    getOwnerFromServer: {}
  },
  inject: { $v: "$vLegalPerson" },
  data () {
    return {
      ProprietorshipType: ProprietorshipType,
      proprietorshipTypes: [
        {
          label: "خصوصی-شخص حقیقی",
          value: ProprietorshipType.PRIVATE_NATURAL_PERSON
        },
        {
          label: "خصوصی-شخص حقوقی",
          value: ProprietorshipType.PRIVATE_LEGAL_PERSON
        },
        {
          label: "دولتی",
          value: ProprietorshipType.PUBLIC
        }
      ],
      readonly: false
    };
  },
  computed: {
    localstate: {
      get: function () {
        return this.legalPerson;
      },
      set: function (newValue) {
        this.$emit("update:legalPerson", newValue);
      }
    },
  },
  methods: {
    getNaturalPerson: async function () {
      if (this.getOwnerFromServer !== undefined) {
        this.readonly = false;
        this.$v.naturalShareHolder.nationalCode.$touch();
        if (!this.$v.naturalShareHolder.nationalCode.$error) {
          const person = await naturalPersonApi.getByNationalCode(this.localstate.naturalShareHolder.nationalCode);
          if (person !== "") {
            this.localstate.naturalShareHolder.firstName = person.firstName;
            this.localstate.naturalShareHolder.surname = person.surname;
            this.localstate.naturalShareHolder.id = person.id;
            this.readonly = this.noLockOnAutoLoad === undefined;
            if (typeof this.getOwnerFromServer === "function")
              this.getOwnerFromServer(person);
          }
          // else {
          // this.localstate.naturalShareHolder.firstName = "";
          // this.localstate.naturalShareHolder.surname = "";
          // this.localstate.naturalShareHolder.id = 0;
          //}
        }
        // else {
        //   this.localstate.naturalShareHolder.firstName = "";
        //   this.localstate.naturalShareHolder.surname = "";
        //   this.localstate.naturalShareHolder.id = 0;
        // }
      }
    },
    getLegalPerson: async function () {
      if (this.getOwnerFromServer !== undefined) {
        this.$v.legalShareHolder.nationalCode.$touch();
        this.readonly = false;
        if (!this.$v.legalShareHolder.nationalCode.$error) {
          const person = await legalPersonApi.getByNationalCode(this.localstate.legalShareHolder.nationalCode);
          if (person !== "") {
            this.localstate.legalShareHolder.officialName = person.officialName;
            this.localstate.legalShareHolder.nationalCode = person.nationalCode;
            this.localstate.legalShareHolder.id = person.id;
            this.readonly = this.noLockOnAutoLoad === undefined;
            if (typeof this.getOwnerFromServer === "function")
              this.getOwnerFromServer(person);
          }
        }
        // else {
        //   this.localstate.legalShareHolder.officialName = "";
        //   this.localstate.legalShareHolder.nationalCode = "";
        //   this.localstate.legalShareHolder.id = 0;
        // }
      }
    }

  },
  directives: {
    mask,
  }
};
</script>
