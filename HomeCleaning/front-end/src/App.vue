<template>
  <v-app>
    <nav class="navbar navbar-expand-lg bg-secondary fixed-top" id="mainNav">
      <div class="container">
        <a class="navbar-brand js-scroll-trigger" href="#page-top">ANASAYFA</a>
        <button
          class="navbar-toggler navbar-toggler-right font-weight-bold bg-primary text-white rounded"
          type="button"
          data-toggle="collapse"
          data-target="#navbarResponsive"
          aria-controls="navbarResponsive"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          Menu <i class="fas fa-bars"></i>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive">
          <ul class="navbar-nav ml-auto">
            <li class="nav-item mx-0 mx-lg-1">
              <a
                class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger"
                href="#service"
                >Services</a
              >
            </li>
            <li class="nav-item mx-0 mx-lg-1">
              <!-- <a
                class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger"
                href="#contact"
                >Contact</a
              > -->
              <router-link
                to="/#contact"
                class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger"
              >
                Contact
              </router-link>
            </li>
            <li class="nav-item mx-0 mx-lg-1">
              <router-link
                to="/#about"
                class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger"
              >
                ABOUT
              </router-link>
            </li>
            <li class="nav-item mx-0 mx-lg-1">
              <login />
            </li>
            <li class="nav-item mx-0 mx-lg-1">
              <router-link
                to="/server"
                class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger"
              >
                Server
              </router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <component :is="layout">
      <router-view />
    </component>
    <alert-dialog ref="alertDialog"></alert-dialog>
    <confirmation-dialog ref="confirmationDialog"></confirmation-dialog>
    <toast ref="toast"></toast>
    <footer class="footer text-center">
      <div class="container">
        <div class="row">
          <!-- Footer Location-->
          <div class="col-lg-4 mb-5 mb-lg-0">
            <h4 class="mb-4">LOCATION</h4>
            <p class="pre-wrap lead mb-0">
              İkitelli OSB Mh. Ytü Teknopark Sk. Ytü Technopark Apt. No: 1 /
              1B23 Basaksehir / Istanbul
            </p>
          </div>
          <!-- Footer Social Icons-->
          <div class="col-lg-4 mb-5 mb-lg-0">
            <h4 class="mb-4">AROUND THE WEB</h4>
            <a
              class="btn btn-outline-light btn-social mx-1"
              href="https://www.facebook.com/TechnorobYazilim/"
              ><i class="fab fa-fw fa-facebook-f"></i
            ></a>
            <a
              class="btn btn-outline-light btn-social mx-1"
              href="https://www.youtube.com/channel/UCi-7zG2ii15gjoan_MzFS9w"
              ><i class="fab fa-fw fa-youtube"></i
            ></a>
            <a
              class="btn btn-outline-light btn-social mx-1"
              href="https://tr.linkedin.com/company/technorob"
              ><i class="fab fa-fw fa-linkedin-in"></i
            ></a>
          </div>
          <!-- Footer About Text-->
          <div class="col-lg-4">
            <h4 class="mb-4">ABOUT FREELANCER</h4>
            <p class="pre-wrap lead mb-0">
              With the support of Yıldız Technical University, we want to
              increase and continue our struggle to be beneficial to both the
              commercial sector and our country in many projects.
            </p>
          </div>
        </div>
      </div>
    </footer>
    <!-- Copyright Section-->
    <section class="copyright py-4 text-center text-white">
      <div class="container">
        <small class="pre-wrap"
          >Copyright © 2019 TechnoRob Yazılım Bilişim Sistemleri Ltd. Şti. All
          Rights Reserved</small
        >
      </div>
    </section>
    <!-- Scroll to Top Button (Only visible on small and extra-small screen sizes)-->
    <div class="scroll-to-top d-lg-none position-fixed">
      <a
        class="js-scroll-trigger d-block text-center text-white rounded"
        href="#page-top"
      >
        <i class="fa fa-chevron-up"></i
      ></a>
    </div>
  </v-app>
</template>

<script>
import AlertDialog from "./components/AlertDialog";
import ConfirmationDialog from "./components/ConfirmationDialog";
import Login from "@/components/Login.vue";
import Toast from "./components/Toast";
const defaultLayout = "default";
export default {
  components: {
    AlertDialog,
    ConfirmationDialog,
    Login,
    Toast
  },
  data: function () {
    return {};
  },
  computed: {
    layout() {
      return (this.$route.meta.layout || defaultLayout) + "-layout";
    },
  },
};
</script>
<style scoped>
.theme--light.v-btn {
  color: rgba(0, 0, 0, 0.87);
}

.theme--light.v-btn {
  color: white !important;
}
</style>
