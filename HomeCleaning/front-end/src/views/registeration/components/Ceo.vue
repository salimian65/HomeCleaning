<template>
  <div>
    <label>مدیرعامل/مدیریت</label>
    <form-group
      :validator="$v.nationalCode"
      :name="$t('labels.nationalCode')"
    >
      <v-text-field
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.nationalCode"
        :counter="$v.nationalCode.$params.maxLength.max"
        :maxlength="$v.nationalCode.$params.maxLength.max"
        :minlength="$v.nationalCode.$params.minLength.min"
        v-on:blur="getNaturalPerson"
        :label="$t('labels.nationalCode')"
        v-mask="'##########'"
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.firstName"
      :name="$t('labels.firstName')"
    >
      <v-text-field
        outlined
        slot-scope="{ attrs }"
        v-model.trim="localstate.firstName"
        :counter="$v.firstName.$params.maxLength.max"
        :maxlength="$v.firstName.$params.maxLength.max"
        :readonly="readonly"
        :minlength="$v.firstName.$params.minLength.min"
        :label="$t('labels.firstName')"
        v-bind="attrs"
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.surname"
      :name="$t('labels.surname')"
    >
      <v-text-field
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model.trim="localstate.surname"
        :counter="$v.surname.$params.maxLength.max"
        :maxlength="$v.surname.$params.maxLength.max"
        :minlength="$v.surname.$params.minLength.min"
        :label="$t('labels.surname')"
        :readonly="readonly"
      ></v-text-field>
    </form-group>
  </div>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  email
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";
import registrantApi from "../../../api/registrant";
import naturalPersonApi from "../../../api/naturalPerson";

export default {
  name: "Ceo",
  props: {
    ceo: { Object, required },
    getCeoFromServer: {},
    noLockOnAutoLoad: {},
  },
  data () {
    return {
      readonly: false,
    };
  },
  directives: {
    mask,
  },
  inject: { $v: "$vCeo" },
  computed: {
    localstate: {
      get: function () {
        return this.ceo;
      },
      set: function (newValue) {
        this.$emit("update:ceo", newValue);
      }
    },
  },
  methods: {
    getNaturalPerson: async function () {
      if (this.getCeoFromServer !== undefined) {
        this.readonly = false;
        this.$v.nationalCode.$touch();
        if (!this.$v.nationalCode.$error) {
          const person = await naturalPersonApi.getByNationalCode(this.localstate.nationalCode);
          if (person !== "") {
            this.localstate.firstName = person.firstName;
            this.localstate.surname = person.surname;
            this.localstate.id = person.id;
            this.readonly = this.noLockOnAutoLoad === undefined;
            if (typeof this.getCeoFromServer === "function")
              this.getCeoFromServer(person);
          }
        }
        // else {
        //   this.localstate.firstName = "";
        //   this.localstate.surname = "";
        //   this.localstate.id = 0;
        // }
      }
    }
  }
};
</script>
