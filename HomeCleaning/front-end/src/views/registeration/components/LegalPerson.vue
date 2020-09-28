<template>
  <div>
    <label>اطلاعات موسسه</label>
    <form-group
      :validator="$v.officialName"
      :name="$t('labels.officialName')"
    >
      <v-text-field
        slot-scope="{ attrs }"
        v-bind="attrs"
        outlined
        v-model.trim="localstate.officialName"
        :counter="$v.officialName.$params.maxLength.max"
        :minlength="$v.officialName.$params.maxLength.min"
        :maxlength="$v.officialName.$params.maxLength.max"
        :label="$t('labels.officialName')"
        required
      ></v-text-field>
    </form-group>
    <form-group
      :validator="$v.nationalCode"
      :name="$t('labels.legalPersonNationalCode')"
    >
      <v-text-field
        outlined
        slot-scope="{ attrs }"
        v-bind="attrs"
        v-model="localstate.nationalCode"
        dir="ltr"
        :counter="$v.nationalCode.$params.maxLength.max"
        :maxlength="$v.nationalCode.$params.maxLength.max"
        :minlength="$v.nationalCode.$params.minLength.min"
        v-mask="'###########'"
        :label="$t('labels.legalPersonNationalCode')"
        required
      ></v-text-field>
    </form-group>
    <!-- <v-file-input
      color="cyan darken-3"
      outlined
      :label="$t('labels.fileUploaderDropZone')"
      @change="onFilePicked"
      v-model="file"
      counter
      multiple
      prepend-icon="mdi-paperclip"
    ></v-file-input> -->
  </div>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  requiredIf
} from "vuelidate/lib/validators";
import { mask } from "vue-the-mask";

export default {
  name: "LegalPerson",
  props: {
    legalPerson: { type: Object, required }
  },
  data () {
    return {
      // operations: [
      //   {
      //     label: "روزانه",
      //     value: "1"
      //   },
      //   {
      //     label: "شبانه روزی",
      //     value: "2"
      //   }
      // ],
      file: null
    };
  },
  //methods: {
  // async onFilePicked (file) {
  //   debugger;
  //   let formData = new FormData();
  //   formData.append("files", file, file.name);
  //   var zzz = await supplierApi.uploadFile(formData);
  //   var x = this.file;
  // }
  //},
  directives: {
    mask
  },
  inject: { $v: "$vLegalPerson" },
  computed: {
    localstate: {
      get: function () {
        return this.legalPerson;
      },
      set: function (newValue) {
        this.$emit("update:legalPerson", newValue);
      }
    }
  }
};
</script>
