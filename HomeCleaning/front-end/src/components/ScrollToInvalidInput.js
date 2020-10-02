//TODO: ASK RAMIN ABOUT THE OPTIONS. SHOULD IT BE DIFIEND ONE TIME AS A CONS?
const ScrollToInvalidInput = {
  methods: {
    goToInvalidInput: function () {
      const component = this;
      component.$nextTick()
        .then(function () {
          let domElement = document.querySelector(".error--text");
          component.$vuetify.goTo(domElement, {
            duration: 1000,
            offset: 100,
            easing: "easeInOutQuad",
          });
        });
    }
  }
};

export default ScrollToInvalidInput;
