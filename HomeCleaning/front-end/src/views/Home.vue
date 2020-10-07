<template>
<div>
        <header class="masthead bg-primary text-white text-center">
      <div class="container d-flex align-items-center flex-column">
        <!-- Masthead Avatar Image-->
        <!-- <img class="masthead-avatar mb-5" src="assets/img/avataaars.svg" alt=""> -->
        <!-- Masthead Heading-->
        <h1 class="masthead-heading mb-0">Home Cleaning</h1>
        <!-- Icon Divider-->
        <div class="divider-custom divider-light">
          <div class="divider-custom-line"></div>
          <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
          <div class="divider-custom-line"></div>
        </div>
        <!-- Masthead Subheading-->
        <p class="pre-wrap masthead-subheading font-weight-light mb-0">
          The Most Reliable Name in Home Cleaning
        </p>
      </div>
    </header>
    <section class="page-section portfolio" id="portfolio">
        <div class="container">
            <!-- Portfolio Section Heading-->
            <div class="text-center">
                <h2 class="page-section-heading text-secondary mb-0 d-inline-block">Services</h2>
            </div>
            <!-- Icon Divider-->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
                <div class="divider-custom-line"></div>
            </div>
            <!-- Portfolio Grid Items-->
            <div class="row justify-content-center">
                <!-- Portfolio Items-->
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal0">
                        <!-- <div class="portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100">
                            <div class="portfolio-item-caption-content text-center text-white"><i class="fas fa-plus fa-3x"></i></div>
                        </div> -->
                            <router-link :to="`package/1`">
                              <span class="span1">Home Cleaning</span>
                            </router-link>
                        <!-- <img class="img-fluid" src="assets/img/portfolio/cabin.png" alt="Log Cabin" /> -->
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal1">
                       <router-link :to="`package/2`">
                             <span class="span1">Empty Home Cleaning</span>
                            </router-link>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal2">
                        <router-link :to="`package/3`">
                             <span class="span1">Cleaning after construction</span>
                            </router-link>
                      
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal3">
                          <router-link :to="`package/4`">
                               <span class="span1">Villa Cleaning</span>
                            </router-link>
                       
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal4">
                       <router-link :to="`package/5`">
                                <span class="span1">Office Cleaning</span>
                            </router-link>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4 mb-5">
                    <div class="portfolio-item mx-auto" data-toggle="modal" data-target="#portfolioModal5">
                       <router-link :to="`package/6`">
                               <span class="span1">Empty Home Cleaning</span>
                            </router-link>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Portfolio Modal-->

    <section class="page-section bg-primary text-white mb-0" id="about">
        <div class="container">
            <!-- About Section Heading-->
            <div class="text-center">
                <h2 class="page-section-heading d-inline-block text-white">ABOUT</h2>
            </div>
            <!-- Icon Divider-->
            <div class="divider-custom divider-light">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
                <div class="divider-custom-line"></div>
            </div>
            <!-- About Section Content-->
            <div class="row">
                <div class="col-lg-5 ml-auto">
                    <p class="pre-wrap lead">
                        Anasayfa has been connecting people to professional house cleaning and other home services since 2012. In our short history, house cleaning professionals using the Anasayfa platform have helped clean millions of homes and apartments in NYC, SF, Boston,
                        Chicago, Los Angeles, London, and beyond. Anasayfa is your one-stop shop for whenever you need professional house cleaning help.
                    </p>
                </div>
                <div class="col-lg-5 mr-auto">
                    <p class="pre-wrap lead">
                        When you schedule a cleaner or maid service through Anasayfa to come to your home, you can rest assured knowing that someone will be at your home ready to go at the time you specified. They’ll help return your bedrooms, bathrooms, kitchen, living room,
                        and more to the like-new condition that you remember from when you first moved in!
                    </p>
                </div>
            </div>
        </div>
    </section>
    <section class="page-section" id="contact">
        <div class="container">
            <!-- Contact Section Heading-->
            <div class="text-center">
                <h2 class="page-section-heading text-secondary d-inline-block mb-0">CONTACT</h2>
            </div>
            <!-- Icon Divider-->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
                <div class="divider-custom-line"></div>
            </div>
            <!-- Contact Section Content-->
            <div class="row justify-content-center">
                <div class="col-lg-4">
                    <div class="d-flex flex-column align-items-center">
                        <div class="icon-contact mb-3"><i class="fas fa-mobile-alt"></i></div>
                        <div class="text-muted">Phone</div>
                        <div class="lead font-weight-bold">+90 541 243 8748</div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="d-flex flex-column align-items-center">
                        <div class="icon-contact mb-3"><i class="far fa-envelope"></i></div>
                        <div class="text-muted">Email</div><a class="lead font-weight-bold" href="mailto:behcetkahraman@technorob.com.tr">behcetkahraman@technorob.com.tr</a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="uza-contact-area">
        <div class="container-fluid">
            <div class="row">
                <!-- Google Maps -->
                <div class="col-12" style="padding:0;">
                    <div style="height: 600px;">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3007.995142579475!2d28.80773251478549!3d41.06909882362993!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14caa5cc72f5207b%3A0x89f9403fa0d3cd68!2sTechnorob%20Yaz%C4%B1l%C4%B1m%20Bili%C5%9Fim%20Sistemleri!5e0!3m2!1str!2str!4v1572369872825!5m2!1str!2str"
                            width="100%" height="600" frameborder="0" style="border:0;" allowfullscreen=""></iframe>
                        <div class="home_details">
                            <div class="container">
                                <div class="box_home_details">

                                    <div class="media">
                                        <div class="media-body">

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
  
</div>
</template>

<script>
import cleaningCategory from "../api/cleaningCategory";
export default {
   data: () => ({
             items: []          
        }),

  methods: {
    async getAllCleaningCategory() {
      this.loading = true;
      var response = await cleaningCategory.getAll();
      this.items = response.data.result;
      this.loading = false;
    }
  },
  components: {

  },
  async mounted() {
    await this.getAllCleaningCategory();
  }

};
</script>
