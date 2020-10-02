<template>
  <div>
    <label>نشانی</label>
    <form-group
      :validator="$v.provinceId"
      :name="$t('labels.province')"
    >
      <v-select
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.provinceId"
        :items="provinces"
        item-text="name"
        item-value="id"
        :label="$t('labels.province')"
        @change="loadCountiesForProvince()"
      ></v-select>
    </form-group>
    <form-group
      :validator="$v.countyId"
      :name="$t('labels.county')"
    >
      <v-select
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model="localstate.countyId"
        :items="counties"
        item-text="name"
        item-value="id"
        :label="$t('labels.county')"
        @change="loadCitiesForCounty()"
      ></v-select>
    </form-group>
    <form-group
      :validator="$v.cityId"
      :name="$t('labels.city')"
    >
      <v-select
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.cityId"
        :items="cities"
        item-text="name"
        item-value="id"
        :label="$t('labels.city')"
      ></v-select>
    </form-group>
    <form-group
      :validator="$v.address"
      :name="$t('labels.address')"
    >
      <v-text-field
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model.trim="localstate.address"
        :counter="$v.address.$params.maxLength.max"
        :label="$t('labels.address')"
        required
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.postalCode"
      :name="$t('labels.postalCode')"
    >
      <v-text-field
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        dir="ltr"
        v-model="localstate.postalCode"
        :counter="$v.postalCode.$params.maxLength.max"
        :minLength="$v.postalCode.$params.maxLength.min"
        :maxLength="$v.postalCode.$params.maxLength.max"
        v-mask="'##########'"
        :label="$t('labels.postalCode')"
        required
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.phone"
      :name="$t('labels.phone')"
    >
      <v-text-field
        outlined
        v-model.trim="contact.phone"
        slot-scope="{ attrs }"
        v-bind="attrs"
        dir="ltr"
        :counter="$v.phone.$params.maxLength.max"
        :minLength="$v.phone.$params.minLength.min"
        :maxLength="$v.phone.$params.maxLength.max"
        :label="$t('labels.phone')"
        :hint="$t('localization.phoneSample')"
        v-mask="$t('localization.phoneMask')"
        required
      ></v-text-field>
    </form-group>
  </div>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";
import areaApi from "../api/area";

export default {
  name: "Contact",
  props: {
    contact: { type: Object, required },
  },
  data () {
    return {
      provinceId: null,
      countyId: null,
      cityId: null,
      address: "",
      postalCode: "",
      phone: "",
      provinces: [],
      counties: [],
      cities: []
    };
  },
  computed: {
    localstate: {
      get: function () {
        return this.contact;
      },
      set: function (newValue) {
        this.$emit("update:contact", newValue);
      }
    }
  },
  inject: { $v: "$vContact" },
  validations: {
    contact: {
      provinceId: {
        required
      },
      countyId: {
        required
      },
      cityId: {
        required
      },
      address: {
        required,
        minLength: minLength(5),
        maxLength: maxLength(200)
      },
      postalCode: {
        required,
        minLength: minLength(10),
        maxLength: maxLength(10)
      },
      phone: {
        required,
        minLength: minLength(8),
        maxLength: maxLength(14)
      }
    }
  },
  methods: {
    loadCountiesForProvince: async function () {
      this.counties = await areaApi.getCountiesForProvince(this.contact.provinceId);
    },
    loadCitiesForCounty: async function () {
      this.cities = await areaApi.getCitiesForCounty(this.contact.countyId);
    }
  },
  created: async function () {
    this.provinces = await areaApi.getAllProvinces();
  },
  directives: { mask }
};
</script>
