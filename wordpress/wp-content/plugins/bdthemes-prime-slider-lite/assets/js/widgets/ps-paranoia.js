(function($, elementor) {

    'use strict';
 
    var widgetParanoia = function($scope, $) {

        var $paranoia = $scope.find('.bdt-paranoia-slider');
        if (!$paranoia.length) {
            return;
        }
        var  $settings = $paranoia.data('settings');

        class Slide {
            // DOM elements
            DOM = {
                // Wrapper element (outer element)
                outer: null,
                // Image element (inner element)
                inner: null
            };
       
            constructor(DOM_el) {
       
                this.DOM.outer = DOM_el;
                this.DOM.inner = this.DOM.outer.children[0];
            }
        }
       
        class Slideshow {
       
            // DOM elements
            DOM = {
                // Main element .slides
                el: null,
                // .slides__img elements
                slides: null
            };
            // Slide instances array
            slidesArr = [];
            // Current slide's index
            current = 0;
            // Total number of slides
            slidesTotal;
            // Current Slide
            currentSlide;
            // Animation's direction (left or right)
            direction;
            // Checks if the slideshow is running
            isAnimating = false;
            // Animation's duration and easing
            duration = 1.2;
            ease = 'power3.inOut';
            // Use a filter (brighness) animation when transitioning from one slide to another
            filtersAnimation = true;
       
            constructor(DOM_el, options = {}) {
       
                this.DOM.el = DOM_el;
       
                // Some options
                this.duration = options.duration != undefined ? options.duration : this.duration;
                this.ease = options.ease != undefined ? options.ease : this.ease;
                this.filtersAnimation = options.filtersAnimation != undefined ? options.filtersAnimation : this.filtersAnimation;
       
                this.DOM.slides = this.DOM.el.querySelectorAll('.bdt-slides-img');
       
                // Create a Slide for each .slides__img element
                this.DOM.slides.forEach(slideEl => this.slidesArr.push(new Slide(slideEl)));
       
                this.slidesTotal = this.DOM.slides.length;
       
            }
       
            /**
             * Set the current slide
             * @param {number} position - The position of the slide.
             */
            setInitialSlide(position = this.current) {
       
                // Update current
                this.current = position;
                // The current Slide
                this.currentSlide = this.slidesArr[this.current];
                // Set current image
                this.DOM.slides[this.current].classList.add('bdt__slides__img--current');
       
            }
       
            /**
             * Navigate the slideshow to the next slide.
             */
            next() {
       
                // Return if anything is running
                if (this.isAnimating) return;
       
                // direction
                this.direction = 'next';
       
                // Update current
                this.current = this.current < this.slidesTotal - 1 ? this.current + 1 : 0;
       
                // Animate to a different slide
                this.navigate();
       
            }
       
            /**
             * Navigate the slideshow to the previous slide.
             */
            prev() {
       
                // Return if anything is running
                if (this.isAnimating) return;
       
                // direction
                this.direction = 'prev';
       
                // Update current
                this.current = this.current > 0 ? this.current - 1 : this.slidesTotal - 1;
       
                // Animate to a different slide
                this.navigate();
       
            }
       
            /**
             * Navigate to a different slide
             * @param {number} position - The position of the new slide.
             */
            navigate(position = this.current) {
       
                this.isAnimating = true;
       
                // Update current
                this.current = position;
       
                // Upcoming Slide
                this.upcomingSlide = this.slidesArr[this.current];
       
                // Animation
                this.timeline = gsap.timeline({
                        defaults: {
                            duration: this.duration,
                            ease: this.ease
                        },
                        onComplete: () => {
                            // Current class switch
                            this.currentSlide.DOM.outer.classList.remove('bdt__slides__img--current');
                            this.upcomingSlide.DOM.outer.classList.add('bdt__slides__img--current');
                            // Update current Slide
                            this.currentSlide = this.slidesArr[this.current];
       
                            this.isAnimating = false;
                        }
                    })
                    .addLabel('start', 0)
       
                // Upcoming Slide gets shown behind the current Slide animates out
                .set(this.upcomingSlide.DOM.outer, {
                    opacity: 1,
                    // 'z-index' : 1
                }, 'start')
       
                // outer/inner opposite translations (Reveal effect)
                .to(this.currentSlide.DOM.outer, {
                        x: this.direction === 'next' ? '-101%' : '101%',
                        onComplete: () => gsap.set(this.currentSlide.DOM.outer, { x: '0%', opacity: 0 })
                    }, 'start')
                    .to(this.currentSlide.DOM.inner, {
                        x: this.direction === 'next' ? '101%' : '-101%',
                        onComplete: () => gsap.set(this.currentSlide.DOM.inner, { x: '0%' })
                    }, 'start')
       
                // Filters animation
                if (this.filtersAnimation) {
       
                    this.timeline.to(this.currentSlide.DOM.inner, {
                            startAt: { filter: 'brightness(100%)' },
                            filter: 'brightness(800%)',
                            onComplete: () => gsap.set(this.currentSlide.DOM.inner, { filter: 'brightness(100%)' })
                        }, 'start')
                        .to(this.upcomingSlide.DOM.inner, {
                            startAt: { filter: 'brightness(800%)' },
                            filter: 'brightness(100%)'
                        }, 'start');
                }
       
       
            }
        }
       
        // body element
        const bodyEl = document.body;
        // body color
        // const bodyColor = getComputedStyle(bodyEl).getPropertyValue('--color-bg');
        // Three Slideshow instances: main, and two for the navigation items
        const slideshowMain = new Slideshow(document.querySelector($settings.id + ' .bdt-slideshow > div.slides'));
        const slideshowNavNext = new Slideshow(document.querySelector($settings.id + ' .bdt-slideshow nav.bdt-nav--next .slides'), { duration: 1, filtersAnimation: false });
        const slideshowNavPrev = new Slideshow(document.querySelector($settings.id + ' .bdt-slideshow nav.bdt-nav--prev .slides'), { duration: 1, filtersAnimation: false });
        // Nav controls to navigate the main slideshow
        const navCtrls = {
            prev: document.querySelector($settings.id + ' .bdt-slideshow nav.bdt-nav--prev'),
            next: document.querySelector($settings.id + ' .bdt-slideshow nav.bdt-nav--next')
        };
        // title elements
        const titleElems = [...document.querySelectorAll($settings.id + ' .bdt-meta-content-wrap > .bdt-meta-item-content')];
       
        // Animates the body color
        const animateBodyBGColor = () => {
       
            gsap.timeline()
                .to(bodyEl, {
                    duration: slideshowMain.duration / 2,
                    ease: 'power3.in',
                }, 'start')
                .to(bodyEl, {
                    duration: slideshowMain.duration,
                    ease: 'power3',
                }, 'start+=' + slideshowMain.duration / 2);
       
        }
       
        // Set the current slide
        slideshowMain.setInitialSlide();
        // Set up the current slide values for the navigation elements, which are based on the slideshowMain's current value
        slideshowNavPrev.setInitialSlide(slideshowMain.current ? slideshowMain.current - 1 : slideshowMain.slidesTotal - 1);
        slideshowNavNext.setInitialSlide(slideshowMain.current < slideshowMain.slidesTotal - 1 ? slideshowMain.current + 1 : 0);
       
        // Set initial title
        gsap.set(titleElems[slideshowMain.current], { 'opacity': 1, 'z-index': 1});
       
        // Change slides for the three slideshows
        const onClickNavCtrlEv = (dir) => {
       
            if (slideshowMain.isAnimating) return;
       
            // Slide out current title
            gsap.to(titleElems[slideshowMain.current], {
                duration: slideshowMain.duration / 2,
                ease: 'power3.in',
                y: dir === 'next' ? '-100%' : '100%',
                opacity: 1,
                'z-index' : 1
            });
       
            slideshowMain[dir]();
            slideshowNavPrev[dir]();
            slideshowNavNext[dir]();
            animateBodyBGColor();
       
            // Slide in the new (current) title
            gsap.to(titleElems[slideshowMain.current], {
                duration: slideshowMain.duration / 2,
                ease: 'power3',
                startAt: { y: dir === 'next' ? '100%' : '-100%' },
                y: '0%',
                opacity: 1,
                'z-index' : 1,
                delay: slideshowMain.duration / 2
            });
       
        };
        navCtrls.prev.addEventListener('click', () => onClickNavCtrlEv('prev'));
        navCtrls.next.addEventListener('click', () => onClickNavCtrlEv('next'));
        
//    const meta =  $scope.find('.bdt-link-btn').css("z-index", "99");
    };



    jQuery(window).on('elementor/frontend/init', function() {
        elementorFrontend.hooks.addAction('frontend/element_ready/prime-slider-paranoia.default', widgetParanoia);
    });

}(jQuery, window.elementorFrontend));