let cards = document.querySelectorAll('.container-card-room');
if (cards) {

window.addEventListener("scroll",()=>{
    cards.forEach(sec=>{
        let top = window.scrollY + window.innerHeight;
        let offset = sec.offsetTop;

        if(top >= offset){
            sec.classList.remove('hide-card');
        }
    })
})
}

let sldroom = document.querySelectorAll(".ck");
if (sldroom) {

let imgsl = document.querySelector(".sldf");
let mnbtnv = document.querySelectorAll(".mn-bt-nvg");
document.querySelector(".sld-s").style.width = document.querySelectorAll(".sld").length * 100 + "%";
let nbmg = 100 / document.querySelectorAll(".sld").length;
document.querySelectorAll(".sld").forEach((e) => {
    e.style.width = nbmg + "%";
})
for (let i = 0; i < sldroom.length; i++) {
    sldroom[i].addEventListener("change", () => {
        let index = Array.from(sldroom).indexOf(
            document.querySelector(".ck:checked")
        );
        imgsl.style.marginLeft = "-" + nbmg * index + "%";
        for (let i = 0; i < mnbtnv.length; i++) {
            mnbtnv[i].style.backgroundColor = "transparent";
        }
        mnbtnv[index].style.backgroundColor = "#fff";
    });
}
}




