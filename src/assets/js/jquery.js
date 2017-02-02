/* eslint-env jquery */

$(function() {
    'use strict';

    $(document).foundation();

    var scrollIfAnchor = function (href) {
        var $fromTop = $('.top-bar').height();

        href = typeof href === 'string' ? href : $(this).attr('href');

        if (href !== '#' && href.indexOf('#') === 0) {
            $('html, body').animate({
                scrollTop: $(href).offset().top - $fromTop
            });
        }
    };

    scrollIfAnchor(window.location.hash);

    $('body').on('click', 'a', scrollIfAnchor);
});
