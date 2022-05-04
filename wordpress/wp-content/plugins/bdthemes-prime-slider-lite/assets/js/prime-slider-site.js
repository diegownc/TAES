(function($, elementor) {

    'use strict';

    var primeSliderScrollButton = function($scope, $) {

        var $primeSlider = $scope.find('.bdt-prime-slider'),
            $scrollButton = $primeSlider.find('.bdt-scroll-down'),
            $selector = $scrollButton.data('selector'),
            $settings = $scrollButton.data('settings');

        //console.log($scrollButton);

        if (!$scrollButton.length) {
            return;
        }

        $($scrollButton).on('click', function(event) {
            event.preventDefault();
            bdtUIkit.scroll($scrollButton, $settings).scrollTo($($selector));
        });

    };

    jQuery(window).on('elementor/frontend/init', function() {
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-general.default', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-general.meteor', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-blog.default', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-blog.coral', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-isolate.default', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-isolate.locate', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-woocommerce.default', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-fluent.default', primeSliderScrollButton);
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-astoria.default', primeSliderScrollButton);
    });

}(jQuery, window.elementorFrontend));