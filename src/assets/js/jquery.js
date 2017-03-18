/* eslint-env jquery */

$(function() {
    'use strict';

    $(document).foundation();

    function scrollIfAnchor(ref) {
        var $fromTop = $('.top-bar').height();

        var href = typeof ref === 'string' ? ref : $(this).attr('href');

        if (href !== '#' && href.indexOf('#') === 0) {
            $('html, body').animate({
                scrollTop: $(href).offset().top - $fromTop
            });
        }
    }

    scrollIfAnchor(window.location.hash);

    $('body').on('click', 'a', scrollIfAnchor);
});
