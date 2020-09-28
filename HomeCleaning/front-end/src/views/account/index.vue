<template>
  <v-container>
    <v-row>
      <v-col
        cols="12"
        sm="12"
        md="4"
      >
        <v-card class="form-card ">
          <v-list-item>
            <v-list-item-avatar color="teal lighten-2"></v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">اطلاعات هویتی :</v-list-item-title>
              <!-- <v-list-item-subtitle class="subtitle">لطفا نام ، کدملی و نام شرکت (اختیاری ) را وارد نمایید </v-list-item-subtitle> -->
            </v-list-item-content>
          </v-list-item>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.name"
            flat
            color="red"
            :label="$t('labels.name')"
            hint="نام و نام خانوادگی"
          ></v-text-field>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.nationalCode"
            :label="$t('labels.nationalCode')"
          ></v-text-field>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.organizationName"
            :label="$t('labels.companyName')"
          ></v-text-field>
        </v-card>
      </v-col>
      <v-col
        cols="12"
        sm="12"
        md="4"
      >
        <v-card class="form-card ">
          <v-list-item>
            <v-list-item-avatar color="teal lighten-2"></v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">اطلاعات کاربری :</v-list-item-title>
              <!-- <v-list-item-subtitle class="subtitle"> </v-list-item-subtitle> -->
            </v-list-item-content>
          </v-list-item>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.username"
            :label="$t('labels.userName')"
          ></v-text-field>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.email"
            :label="$t('labels.email')"
          ></v-text-field>
          <v-text-field
            outlined
            readonly
            v-model="userInfo.role"
            :label="$t('labels.role')"
          ></v-text-field>

          <v-btn
            dark
            class="ma-2"
            outlined
            color="indigo"
            @click="changePasswordDialogOpen=true"
          >
            <v-icon>mdi-onepassword</v-icon>
            {{$t("buttons.changePassword")}}
          </v-btn>
        </v-card>
      </v-col>
      <v-col
        cols="12"
        sm="12"
        md="4"
      >
        <v-card class="form-card text-center">
          <v-img
            src="https://picsum.photos/id/11/500/300"
            lazy-src="https://picsum.photos/id/11/10/6"
            aspect-ratio="1"
            class="grey lighten-2"
            max-height="300px"
          ></v-img>
          <br />
          <v-btn
            outlined=""
            color="grey darken-1"
            @click="editDialogOpen=true"
          >
            <v-icon>mdi-image-edit-outline</v-icon>
            {{$t("buttons.edit")}}
          </v-btn>
        </v-card>
      </v-col>
    </v-row>
    <v-dialog v-model="editDialogOpen">
      <v-card>
        <edit />
      </v-card>
    </v-dialog>
    <v-dialog v-model="changePasswordDialogOpen">
      <v-card>
        <changePassword />
      </v-card>
    </v-dialog>
  </v-container>
</template>
<script src="./account.js"></script>
