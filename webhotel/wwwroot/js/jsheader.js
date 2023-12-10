        let burger_chk = document.querySelector('.ciao-burger');
        let x = document.querySelector('.container-hd-list');
        let ciao_close = document.querySelector('.ciao-close');
        burger_chk.addEventListener('click',showmenu);
        ciao_close.addEventListener('click',showmenu);

        function showmenu(){
            if(x.style.left == '-100%'){
                x.style.left = '0';
            }
            else{
                x.style.left = '-100%'
            }
        }
        const hd = document.querySelector('.container-hd');
        let lastScrollY = window.scrollY;
        function scrollShow(){
            const currentScrollY =  window.scrollY;
            if(currentScrollY>lastScrollY){
                hd.style.top = '-100px';
            }
            else{
                hd.style.top = '0';
            }
            lastScrollY = currentScrollY;
        }
        window.addEventListener('scroll',scrollShow);