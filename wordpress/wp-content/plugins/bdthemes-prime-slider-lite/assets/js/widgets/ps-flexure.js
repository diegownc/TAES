(function($, elementor) {

    'use strict';

    var widgetHoverBox = function($scope, $) {


        var $hoverBox = $scope.find('.bdt-prime-slider-flexure'),
            $settings = $hoverBox.data('settings');

        var iconBx = document.querySelectorAll('#' + $settings.box_id + ' .bdt-ps-flexure-item');
        var contentBx = document.querySelectorAll('#' + $settings.box_id + ' .bdt-ps-flexure-content');

        for (var i = 0; i < iconBx.length; i++) {
            iconBx[i].addEventListener($settings.mouse_event, function() {
                for (var i = 0; i < contentBx.length; i++) {
                    contentBx[i].className = 'bdt-ps-flexure-content'
                }
                document.getElementById(this.dataset.id).className = 'bdt-ps-flexure-content active';

                for (var i = 0; i < iconBx.length; i++) {
                    iconBx[i].className = 'bdt-ps-flexure-item';
                }
                this.className = 'bdt-ps-flexure-item active';

            })
        }

    };


    jQuery(window).on('elementor/frontend/init', function() {
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-flexure.default', widgetHoverBox);
    });

}(jQuery, window.elementorFrontend));