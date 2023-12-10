let hds = document.querySelectorAll(".cd-csr-y");
if (hds != null) {
        let dl = 0.3;
        window.addEventListener("scroll", () => {
          hds.forEach((h) => {
            let bt = window.scrollY + window.innerHeight;
            let offset = h.offsetTop;
            if (bt >= offset) {
              h.classList.remove("hide-ab");
              h.style.transitionDelay = dl + "s";
              dl = dl + dl;
            }
          });
        });
}

        let dlx = 0.1;
let svs = document.querySelectorAll(".sv-it");
if (svs != null) {

        console.log(svs);
        window.addEventListener("scroll", () => {
          svs.forEach((h) => {
            let bt = window.scrollY + window.innerHeight;
            let offset = h.offsetTop;
            if (bt >= offset) {
              h.classList.remove("hide-ab");
              h.style.transitionDelay = dlx + "s";
              dlx = dlx + 0.2;
            }
          });
        });
}
let hdsv = document.querySelector(".sv-bn-hd");
if (hdsv != null) {

        window.addEventListener("scroll", () => {
          let bt = window.scrollY + window.innerHeight;
          let offset = hdsv.offsetTop;
          if (bt >= offset) {
            hdsv.classList.remove("hide-ab");
          }
        });
}


        let dlds = 0.3;
let desfs = document.querySelectorAll(".ds-hd");
if (desfs != null) {

        window.addEventListener("scroll", () => {
          desfs.forEach((s) => {
            let bt = window.scrollY + window.innerHeight;
            let offset = document.querySelector('.bn-find-best').offsetTop + document.querySelector('.bn-find-best').offsetHeight;
            if (bt >= offset) {
              s.classList.remove("hide-ds");
              s.style.transitionDelay = dlds + 's';
              dlds = dlds + 0.3;
            }
          });
        });
}

let fthome = document.querySelector('.ft-home');
if (fthome != null) {

        window.addEventListener("scroll", () => {
          let bt = window.scrollY + window.innerHeight;
          let offset = fthome.offsetTop + fthome.offsetHeight;
          if (bt >= offset) {
            fthome.classList.remove("hide-ab");
          }
        });
}