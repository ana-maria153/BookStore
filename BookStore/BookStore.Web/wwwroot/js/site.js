(function ($) {
    "use strict";
    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });

    // Owl Carousel
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: false,
        responsive: {
            0: {
                items: 1
            }
        },
        autoplay: true,
        autoplayTimeout: 5000 // Set autoplay timeout to 5 seconds
    })

})(jQuery);
