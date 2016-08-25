/* jshint jquery:true */
/* global anchors:true */

(function() {
    "use strict";

    (function() {
        var docEl = document.documentElement;
        var classRE = new RegExp("(^|\\s)no-js(\\s|$)");
        var className = docEl.className;

        docEl.className = className.replace(classRE, "$1js$2");
    })();

    (function() {

        var testimonialsContainer = document.getElementById("testimonials");

        var testimonialImages = [
            "twitter-levgimelfarb.png",
            "twitter-menonHari.png",
            "twitter-nathanchere.png",
            "twitter-patrickroos.png",
            "twitter-ritasker.png",
            "twitter-twith2sugars.png"
        ];

        if (testimonialsContainer) {
            var testimonialEl = testimonialsContainer.querySelector("img");
            var testimonialImage = testimonialImages[Math.floor(Math.random() * testimonialImages.length)];
            var imgPath = "/assets/images/testimonials/" + testimonialImage;

            var imgDefer = function imgDefer () {
                testimonialEl.setAttribute("src", imgPath);
                testimonialEl.className += " loaded";
            };

            window.addEventListener("load", imgDefer, false);

        }

    })();

    anchors.add(".docs-content h2, .docs-content h3, .docs-content h4, .docs-content h5, .docs-content h6");
})();
