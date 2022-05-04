(function($, elementor) {

    'use strict';

    var widgetEventCalendar = function($scope, $) {

        var $eventCalendar = $scope.find('.bdt-event-calendar');

        if (!$eventCalendar.length) {
            return;
        }

        var $eventCalendarContainer = $eventCalendar.find('.swiper-container'),
            $settings = $eventCalendar.data('settings');

        var swiper = new Swiper($eventCalendarContainer, $settings);

        if ($settings.pauseOnHover) {
            $($eventCalendarContainer).hover(function() {
                (this).swiper.autoplay.stop();
            }, function() {
                (this).swiper.autoplay.start();
            });
        }

    };


    jQuery(window).on('elementor/frontend/init', function() {
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-event-calendar.default', widgetEventCalendar);
    });

}(jQuery, window.elementorFrontend));