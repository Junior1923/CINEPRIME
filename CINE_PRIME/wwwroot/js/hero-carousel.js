new Swiper('.hero-carousel', {
    effect: 'fade',
    loop: true,
    autoplay: {
        delay: 1500,
        disableOnInteraction: false
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    },
    fadeEffect: {
        crossFade: true
    }
});