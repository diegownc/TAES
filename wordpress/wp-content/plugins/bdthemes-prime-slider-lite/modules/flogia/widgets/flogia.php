<?php

namespace PrimeSlider\Modules\Flogia\Widgets;

use Elementor\Widget_Base;
use Elementor\Controls_Manager;
use Elementor\Group_Control_Typography;
use Elementor\Group_Control_Border;
use Elementor\Group_Control_Image_Size;

use PrimeSlider\Traits\Global_Widget_Controls;
use PrimeSlider\Traits\QueryControls\GroupQuery\Group_Control_Query;
use PrimeSlider\Utils;
use WP_Query;

if ( !defined('ABSPATH') ) exit; // Exit if accessed directly

class Flogia extends Widget_Base {

    use Group_Control_Query;
	use Global_Widget_Controls;

    public function get_name() {
        return 'prime-slider-flogia';
    }

    public function get_title() {
        return BDTPS . esc_html__('Flogia', 'bdthemes-prime-slider');
    }

    public function get_icon() {
        return 'bdt-widget-icon ps-wi-flogia';
    }

    public function get_categories() {
        return ['prime-slider'];
    }

    public function get_keywords() {
        return ['prime slider', 'slider', 'blog', 'prime', 'flogia'];
    }

    public function get_style_depends() {
        return ['ps-flogia'];
    }

    public function get_custom_help_url() {
        return 'https://youtu.be/Ayo1oEALF_8';
    }

    protected function register_controls() {

        $this->start_controls_section(
            'section_content_layout',
            [
                'label' => esc_html__('Layout', 'bdthemes-prime-slider'),
            ]
        );

        $this->add_control(
            'slider_size_ratio',
            [
                'label'       => esc_html__('Size Ratio', 'bdthemes-prime-slider'),
                'type'        => Controls_Manager::IMAGE_DIMENSIONS,
                'description' => 'Slider ratio to width and height, such as 16:9',
                'separator'   => 'before',
            ]
        );

        $this->add_control(
            'slider_min_height',
            [
                'label' => esc_html__('Minimum Height', 'bdthemes-prime-slider'),
                'type'  => Controls_Manager::SLIDER,
                'range' => [
                    'px' => [
                        'min' => 50,
                        'max' => 1024,
                    ],
                ],
            ]
        );

        $this->add_responsive_control(
            'content_max_width',
            [
                'label'     => esc_html__('Content Max Width', 'bdthemes-prime-slider') . BDTPS_NC,
                'type'      => Controls_Manager::SLIDER,
                'range'     => [
                    'px' => [
                        'min' => 220,
                        'max' => 1600,
                    ],
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider-flogia .bdt-container' => 'width: {{SIZE}}{{UNIT}}; max-width: {{SIZE}}{{UNIT}};',
                ],
            ]
        );

        $this->add_group_control(
            Group_Control_Image_Size::get_type(),
            [
                'name'         => 'thumbnail_size',
                'label'        => esc_html__( 'Image Size', 'bdthemes-prime-slider' ) . BDTPS_NC,
                'exclude'      => [ 'custom' ],
                'default'      => 'full',
                'prefix_class' => 'bdt-prime-slider-thumbnail-size-',
                'separator' => 'before'
            ]
        );

        //Global background settings Controls
        $this->register_background_settings('.bdt-prime-slider .bdt-slideshow-item>.bdt-ps-slide-img');

        $this->add_control(
            'show_title',
            [
                'label'   => esc_html__('Show Title', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
                'separator' => 'before'
            ]
        );

        $this->add_control(
            'title_html_tag',
            [
                'label'     => __('Title HTML Tag', 'bdthemes-element-pack'),
                'type'      => Controls_Manager::SELECT,
                'default'   => 'h1',
                'options'   => prime_slider_title_tags(),
                'condition' => [
                    'show_title' => 'yes'
                ]
            ]
        );

        $this->add_control(
            'show_excerpt',
            [
                'label'     => __( 'Show Text', 'bdthemes-prime-slider' ) . BDTPS_NC,
                'type'      => Controls_Manager::SWITCHER,
                'default'   => 'yes',
                'separator' => 'before'
            ]
        );
        
        $this->add_control(
            'excerpt_length',
            [
                'label'       => __( 'Text Limit', 'bdthemes-prime-slider' ),
                'description' => esc_html__( 'It\'s just work for main content, but not working with excerpt. If you set 0 so you will get full main content.', 'bdthemes-prime-slider' ),
                'type'        => Controls_Manager::NUMBER,
                'default'     => 30,
                'condition'   => [
                    'show_excerpt' => 'yes',
                ],
            ]
        );
        
        $this->add_control(
            'strip_shortcode',
            [
                'label'     => esc_html__( 'Strip Shortcode', 'bdthemes-prime-slider' ),
                'type'      => Controls_Manager::SWITCHER,
                'default'   => 'yes',
                'condition' => [
                    'show_excerpt' => 'yes',
                ],
            ]
        );

        $this->add_control(
            'show_category',
            [
                'label'   => esc_html__('Show Category', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
                'separator' => 'before'
            ]
        );

        $this->add_control(
            'show_admin_info',
            [
                'label'   => esc_html__('Show Author Meta', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
            ]
        );

        $this->add_control(
            'published_by',
            [
                'label'   => esc_html__('Published By', 'bdthemes-prime-slider') . BDTPS_NC,
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
                'condition' => [
                    'show_admin_info' => 'yes'
                ]
            ]
        );

        $this->add_control(
            'show_thumbnav',
            [
                'label'   => esc_html__('Show Thumbnav', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
            ]
        );

        $this->add_control(
            'show_navigation_arrows_dots',
            [
                'label'   => esc_html__('Show Navigation Arrows/Dots', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
            ]
        );

        $this->add_responsive_control(
            'content_alignment',
            [
                'label'     => esc_html__('Alignment', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::CHOOSE,
                'options'   => [
                    'left'   => [
                        'title' => esc_html__('Left', 'bdthemes-prime-slider'),
                        'icon'  => 'eicon-text-align-left',
                    ],
                    'center' => [
                        'title' => esc_html__('Center', 'bdthemes-prime-slider'),
                        'icon'  => 'eicon-text-align-center',
                    ],
                    'right'  => [
                        'title' => esc_html__('Right', 'bdthemes-prime-slider'),
                        'icon'  => 'eicon-text-align-right',
                    ],
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content' => 'text-align: {{VALUE}};',
                ],
            ]
        );

        $this->end_controls_section();

        //New Query Builder Settings
        $this->start_controls_section(
            'section_post_query_builder',
            [
                'label' => __( 'Query', 'bdthemes-prime-slider' ) . BDTPS_NC,
                'tab' => Controls_Manager::TAB_CONTENT,
            ]
        );

        $this->register_query_builder_controls();

        $this->end_controls_section();

        //Global Widget Controls
        $this->start_controls_section(
            'section_content_query',
            [
                'label' => esc_html__( 'Query (deprecated)', 'bdthemes-prime-slider' ),
                'condition' => [
                    'is_replaced_deprecated_query!' => 'yes'
                ]
            ]
        );

        $this->register_query_controls();

        $this->update_control(
            'limit',
            [
                'type'      => Controls_Manager::NUMBER,
                'default'   => 5,
            ]
        );

        $this->end_controls_section();

        $this->start_controls_section(
            'section_style_animation',
            [
                'label' => esc_html__('Animation', 'bdthemes-prime-slider'),
            ]
        );

        $this->add_control(
            'finite',
            [
                'label'   => esc_html__('Loop', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
            ]
        );

        $this->add_control(
            'autoplay',
            [
                'label'   => esc_html__('Autoplay', 'bdthemes-prime-slider'),
                'type'    => Controls_Manager::SWITCHER,
                'default' => 'yes',
            ]
        );

        $this->add_control(
            'autoplay_interval',
            [
                'label'     => esc_html__('Autoplay Interval (ms)', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::NUMBER,
                'default'   => 7000,
                'condition' => [
                    'autoplay' => 'yes',
                ],
            ]
        );

        $this->add_control(
            'pause_on_hover',
            [
                'label' => esc_html__('Pause on Hover', 'bdthemes-prime-slider'),
                'type'  => Controls_Manager::SWITCHER,
            ]
        );

        $this->add_control(
            'velocity',
            [
                'label' => __('Animation Speed', 'bdthemes-element-pack'),
                'type'  => Controls_Manager::SLIDER,
                'range' => [
                    'px' => [
                        'min'  => 0.1,
                        'max'  => 1,
                        'step' => 0.1,
                    ],
                ],
            ]
        );

        $this->add_control(
            'kenburns_animation',
            [
                'label'     => esc_html__('Kenburns Animation', 'bdthemes-prime-slider'),
                'separator' => 'before',
                'type'      => Controls_Manager::SWITCHER,
            ]
        );

        $this->add_control(
            'kenburns_reverse',
            [
                'label'     => esc_html__('Kenburn Reverse', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SWITCHER,
                'condition' => [
                    'kenburns_animation' => 'yes'
                ]
            ]
        );

        $this->end_controls_section();


        //Style Start
        $this->start_controls_section(
            'section_style_sliders',
            [
                'label' => esc_html__('Sliders', 'bdthemes-prime-slider'),
                'tab'   => Controls_Manager::TAB_STYLE,
            ]
        );

        $this->add_control(
            'overlay',
            [
                'label'     => esc_html__('Overlay', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SELECT,
                'default'   => 'background',
                'options'   => [
                    'none'       => esc_html__('None', 'bdthemes-prime-slider'),
                    'background' => esc_html__('Background', 'bdthemes-prime-slider'),
                    'blend'      => esc_html__('Blend', 'bdthemes-prime-slider'),
                ],
                'separator' => 'before',
            ]
        );

        $this->add_control(
            'overlay_color',
            [
                'label'     => esc_html__('Overlay Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'condition' => [
                    'overlay' => ['background', 'blend']
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-slideshow .bdt-overlay-default' => 'background-color: {{VALUE}};'
                ]
            ]
        );

        $this->add_control(
            'blend_type',
            [
                'label'     => esc_html__('Blend Type', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SELECT,
                'default'   => 'multiply',
                'options'   => prime_slider_blend_options(),
                'condition' => [
                    'overlay' => 'blend',
                ],
            ]
        );

        $this->add_control(
            'ps_content_innner_padding',
            [
                'label'      => esc_html__('Content Inner Padding', 'bdthemes-prime-slider'),
                'type'       => Controls_Manager::DIMENSIONS,
                'size_units' => ['px', '%'],
                'selectors'  => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content' => 'padding: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
                ],
            ]
        );

        $this->start_controls_tabs('tabs_slider_style');

        $this->start_controls_tab(
            'tab_slider_title',
            [
                'label' => __('Title', 'bdthemes-prime-slider'),
            ]
        );

        $this->add_responsive_control(
            'title_width',
            [
                'label'     => esc_html__('Title Width', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SLIDER,
                'size_units' => [ 'px', '%' ],
				'range' => [
					'px' => [
						'min' => 220,
						'max' => 1200,
					],
					'%' => [
						'min' => 10,
						'max' => 100,
					],
				],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content .bdt-main-title' => 'width: {{SIZE}}{{UNIT}};',
                ],
                'condition' => [
                    'show_title' => ['yes'],
                ],
            ]
        );

        $this->add_control(
			'show_text_stroke',
			[
				'label'   => esc_html__('Text Stroke', 'bdthemes-prime-slider') . BDTPS_NC,
				'type'    => Controls_Manager::SWITCHER,
				'prefix_class' => 'bdt-text-stroke--',
				'condition' => [
					'show_title' => ['yes'],
				],
			]
		);

        $this->add_control(
            'title_color',
            [
                'label'     => esc_html__('Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content .bdt-main-title h1 a' => 'color: {{VALUE}}; -webkit-text-stroke-color: {{VALUE}};',
                ],
                'condition' => [
                    'show_title' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'first_word_title_color',
            [
                'label'     => esc_html__('First Word Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content .bdt-main-title h1 a span' => 'color: {{VALUE}}; -webkit-text-stroke-color: {{VALUE}};',
                ],
            ]
        );

        $this->add_group_control(
            Group_Control_Typography::get_type(),
            [
                'name'      => 'title_typography',
                'label'     => esc_html__('Typography', 'bdthemes-prime-slider'),
                'selector'  => '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content .bdt-main-title h1',
                'condition' => [
                    'show_title' => ['yes'],
                ],
            ]
        );

        $this->add_responsive_control(
            'prime_slider_title_spacing',
            [
                'label'     => esc_html__('Title Spacing', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SLIDER,
                'range'     => [
                    'px' => [
                        'min' => 0,
                        'max' => 100,
                    ],
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-content .bdt-main-title h1' => 'padding-bottom: {{SIZE}}{{UNIT}};',
                ],
                'condition' => [
                    'show_title' => ['yes'],
                ],
            ]
        );

        $this->end_controls_tab();

        $this->start_controls_tab(
            'slider_style_excerpt',
            [
                'label'     => esc_html__( 'Excerpt', 'bdthemes-prime-slider' ) . BDTPS_NC,
                'condition' => [
                    'show_excerpt' => ['yes'],
                ],
            ]
        );
        
        $this->add_control(
            'excerpt_color',
            [
                'label'     => esc_html__( 'Color', 'bdthemes-prime-slider' ),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-blog-text' => 'color: {{VALUE}};',
                ],
            ]
        );
        
        $this->add_group_control(
            Group_Control_Typography::get_type(),
            [
                'name'     => 'excerpt_typography',
                'label'    => esc_html__( 'Typography', 'bdthemes-prime-slider' ),
                'selector' => '{{WRAPPER}} .bdt-prime-slider .bdt-blog-text',
            ]
        );
        
        $this->add_responsive_control(
            'excerpt_width',
            [
                'label'          => __( 'Width (px)', 'bdthemes-prime-slider' ),
                'type'           => Controls_Manager::SLIDER,
                'default'        => [
                    'unit' => 'px',
                ],
                'tablet_default' => [
                    'unit' => 'px',
                ],
                'mobile_default' => [
                    'unit' => 'px',
                ],
                'size_units'     => ['px'],
                'range'          => [
                    'px' => [
                        'min' => 100,
                        'max' => 800,
                    ],
                ],
                'selectors'      => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-blog-text' => 'max-width: {{SIZE}}{{UNIT}};',
                ],
            ]
        );
        
        $this->add_responsive_control(
            'prime_slider_excerpt_spacing',
            [
                'label'     => esc_html__( 'Excerpt Spacing', 'bdthemes-prime-slider' ),
                'type'      => Controls_Manager::SLIDER,
                'range'     => [
                    'px' => [
                        'min' => 0,
                        'max' => 200,
                    ],
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-blog-text' => 'padding-bottom: {{SIZE}}{{UNIT}};',
                ],
                'condition' => [
                    'show_excerpt' => ['yes'],
                ],
            ]
        );
        
        $this->end_controls_tab();

        $this->start_controls_tab(
            'tab_slider_category',
            [
                'label'     => __('Category', 'bdthemes-prime-slider'),
                'condition' => [
                    'show_category' => 'yes',
                ],
            ]
        );

        $this->add_control(
            'category_icon_color',
            [
                'label'     => __('Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a' => 'color: {{VALUE}}',
                ],
            ]
        );

        $this->add_control(
            'category_icon_background_color',
            [
                'label'     => __('Background', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a' => 'background: {{VALUE}}',
                ],
            ]
        );

        $this->add_group_control(
            Group_Control_Border::get_type(),
            [
                'name'     => 'category_border',
                'label'    => esc_html__('Border', 'bdthemes-prime-slider'),
                'selector' => '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a',
            ]
        );

        $this->add_responsive_control(
            'category_border_radius',
            [
                'label'      => esc_html__('Radius', 'bdthemes-prime-slider'),
                'type'       => Controls_Manager::DIMENSIONS,
                'size_units' => ['px', '%'],
                'selectors'  => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a' => 'border-radius: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
                ],
            ]
        );

        $this->add_responsive_control(
            'category_padding',
            [
                'label'      => esc_html__('Padding', 'bdthemes-prime-slider'),
                'type'       => Controls_Manager::DIMENSIONS,
                'size_units' => ['px', 'em', '%'],
                'selectors'  => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a' => 'padding: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
                ],
            ]
        );

        $this->add_group_control(
            Group_Control_Typography::get_type(),
            [
                'name'     => 'category_typography',
                'label'    => esc_html__('Typography', 'bdthemes-prime-slider'),
                'selector' => '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category a',
            ]
        );

        $this->add_responsive_control(
            'ps_category_spacing',
            [
                'label'     => esc_html__('Spacing', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::SLIDER,
                'range'     => [
                    'px' => [
                        'min' => 0,
                        'max' => 100,
                    ],
                ],
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-category-wrapper' => 'padding-bottom: {{SIZE}}{{UNIT}};',
                ],
            ]
        );

        $this->end_controls_tab();

        $this->start_controls_tab(
            'tab_slider_meta',
            [
                'label'     => __('Meta', 'bdthemes-prime-slider'),
                'condition' => [
                    'show_admin_info' => 'yes',
                ],
            ]
        );

        $this->add_control(
            'meta_text_color',
            [
                'label'     => __('Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-meta .bdt-author' => 'color: {{VALUE}}',
                ],
            ]
        );

        $this->add_group_control(
            Group_Control_Typography::get_type(),
            [
                'name'     => 'meta_typography',
                'label'    => esc_html__('Typography', 'bdthemes-prime-slider'),
                'selector' => '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-meta .bdt-author',
            ]
        );

        $this->end_controls_tab();

        $this->end_controls_tabs();

        $this->end_controls_section();


        $this->start_controls_section(
            'section_style_featured_post',
            [
                'label'     => esc_html__('Featured Post', 'bdthemes-prime-slider'),
                'tab'       => Controls_Manager::TAB_STYLE,
                'condition' => [
                    'show_thumbnav' => 'yes',
                ],
            ]
        );

        $this->add_control(
            'featured_post_title_color',
            [
                'label'     => __('Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-thumbnav>a span' => 'color: {{VALUE}}',
                ],
            ]
        );

        $this->add_control(
            'featured_post_background_color',
            [
                'label'     => __('Background Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-thumbnav>a span' => 'background-color: {{VALUE}}',
                ],
            ]
        );

        $this->add_control(
            'featured_post_overlay_color',
            [
                'label'     => __('Overlay Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-thumbnav .bdt-thumb-content:before' => 'background: {{VALUE}}',
                ],
            ]
        );

        $this-> add_group_control(
            Group_Control_Border::get_type(),
            [
                'name'      => 'featured_thumbs_border',
                'label'     => __( 'Border', 'bdthemes-prime-slider' ),
                'selector'  => '{{WRAPPER}} .bdt-ps-thumbnav.bdt-active .bdt-thumb-content',
            ]
        );

        $this->add_responsive_control(
			'featured_thumbs_border_radius',
			[
				'label'      => esc_html__('Border Radius', 'bdthemes-prime-slider') . BDTPS_NC,
				'type'       => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', '%'],
				'selectors'  => [
					'{{WRAPPER}} .bdt-ps-thumbnav .bdt-thumb-content' => 'border-radius: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
				],
			]
		);

        $this->add_group_control(
            Group_Control_Typography::get_type(),
            [
                'name'     => 'featured_typography',
                'selector' => '{{WRAPPER}} .bdt-prime-slider .bdt-ps-thumbnav>a span',
            ]
        );

        $this->add_control(
            'featured_post_alignment',
            [
                'label'         => __( 'Alignment', 'bdthemes-prime-slider' ),
                'type'          => Controls_Manager::CHOOSE,
                'options'       => [
                    'left'      => [
                        'title' => __( 'Left', 'bdthemes-prime-slider' ),
                        'icon'  => 'eicon-h-align-left',
                    ],
                    'center'    => [
                        'title' => __( 'Center', 'bdthemes-prime-slider' ),
                        'icon'  => 'eicon-h-align-center',
                    ],
                    'right'     => [
                        'title' => __( 'Right', 'bdthemes-prime-slider' ),
                        'icon'  => 'eicon-h-align-right',
                    ],
                ],
                'default'       => 'center',
                'toggle'        => true,
            ]
        );

        $this->end_controls_section();

        $this->start_controls_section(
            'section_style_navigation',
            [
                'label' => __('Navigation', 'bdthemes-prime-slider'),
                'tab'   => Controls_Manager::TAB_STYLE,
            ]
        );

        $this->start_controls_tabs('tabs_navigation_style');

        $this->start_controls_tab(
            'tab_nav_arrows_dots_style',
            [
                'label'     => __('Normal', 'bdthemes-prime-slider'),
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'arrows_color',
            [
                'label'     => __('Arrows Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-previous svg, {{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-next svg' => 'color: {{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'dots_color',
            [
                'label'     => __('Dots Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-dotnav li a:before' => 'background: {{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
                'separator' => 'before',
            ]
        );


        $this->end_controls_tab();

        $this->start_controls_tab(
            'tab_nav_arrows_dots_hover_style',
            [
                'label'     => __('Hover', 'bdthemes-prime-slider'),
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'arrows_hover_color',
            [
                'label'     => __('Arrows Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-previous:hover svg, {{WRAPPER}} .bdt-prime-slider .bdt-prime-slider-next:hover svg' => 'color: {{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'dots_hover_color',
            [
                'label'     => __('Dots Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-dotnav li:hover a:before' => 'background: {{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
                'separator' => 'before',
            ]
        );

        $this->end_controls_tab();

        $this->start_controls_tab(
            'tab_nav_arrows_dots_active_style',
            [
                'label'     => __('Active', 'bdthemes-prime-slider'),
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'dots_active_color',
            [
                'label'     => __('Dots Color', 'bdthemes-prime-slider'),
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-dotnav li.bdt-active a:before' => 'background: {{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->add_control(
            'dots_active_border_color',
            [
                'label'     => __('Dots Border Color', 'bdthemes-prime-slider') . BDTPS_NC,
                'type'      => Controls_Manager::COLOR,
                'selectors' => [
                    '{{WRAPPER}} .bdt-prime-slider .bdt-ps-dotnav li.bdt-active a:after' => 'border-color:{{VALUE}}',
                ],
                'condition' => [
                    'show_navigation_arrows_dots' => ['yes'],
                ],
            ]
        );

        $this->end_controls_tab();

        $this->end_controls_tabs();

        $this->end_controls_section();
    }

    public function query_posts() {
        $settings = $this->get_settings();
    
        if ( isset( $settings['is_replaced_deprecated_query'] ) &&
             $settings['is_replaced_deprecated_query'] == 'yes' ) {
            $args = [];
        
            if ( $settings['posts_limit'] ) {
                $args['posts_per_page'] = $settings['posts_limit'];
                $args['paged']          = max( 1, get_query_var( 'paged' ), get_query_var( 'page' ) );
            }
        
            $default = $this->getGroupControlQueryArgs();
            $args = array_merge( $default, $args );
        
            $query = new WP_Query( $args );
        
            return $query;
        
        } else {
            $args = [
                'post_type'      => 'post',
                'posts_per_page' => $settings['limit'],
                'orderby'        => $settings['orderby'],
                'order'          => $settings['order'],
                'post_status'    => 'publish',
            ];
        
            if ( 'by_name' === $settings['post_source'] and ! empty( $settings['post_categories'] ) ) {
                $args['tax_query'][] = [
                    'taxonomy' => 'category',
                    'field'    => 'slug',
                    'terms'    => $settings['post_categories'],
                ];
            }
        
            $query = new WP_Query( $args );
        
            return $query;
        }
    }

    public function render_header($skin_name = 'flogia') {
        $settings = $this->get_settings_for_display();


        $this->add_render_attribute('slider', 'class', 'bdt-prime-slider-' . $skin_name);

        $ratio = ($settings['slider_size_ratio']['width'] && $settings['slider_size_ratio']['height']) ? $settings['slider_size_ratio']['width'] . ":" . $settings['slider_size_ratio']['height'] : '16:9';

        $this->add_render_attribute(
            [
                'slideshow' => [
                    'bdt-slideshow' => [
                        wp_json_encode([
                            "animation"         => 'fade',
                            "ratio"             => $ratio,
                            "min-height"        => ($settings["slider_min_height"]["size"]) ? $settings["slider_min_height"]["size"] : 520,
                            "autoplay"          => ($settings["autoplay"]) ? true : false,
                            "autoplay-interval" => $settings["autoplay_interval"],
                            "pause-on-hover"    => ("yes" === $settings["pause_on_hover"]) ? true : false,
                            "velocity"          => ($settings["velocity"]["size"]) ? $settings["velocity"]["size"] : 1,
                            "finite"            => ($settings["finite"]) ? false : true,
                        ])
                    ]
                ]
            ]
        );

        ?>
      <div class="bdt-prime-slider">
      <div <?php $this->print_render_attribute_string('slider'); ?>>

      <div class="bdt-position-relative bdt-visible-toggle" <?php $this->print_render_attribute_string('slideshow'); ?>>

      <ul class="bdt-slideshow-items">
        <?php
    }

    public function render_category() {
        ?>
      <span class="bdt-ps-category">
			<span><?php echo get_the_category_list(', '); ?></span>
		</span>
        <?php
    }

    public function render_navigation_arrows_dots() {
        $settings = $this->get_settings_for_display();
        ?>

        <?php if ( $settings['show_navigation_arrows_dots'] ) : ?>
        <div class="bdt-navigation-arrows bdt-position-center-right bdt-position-large bdt-position-z-index">
          <a class="bdt-prime-slider-previous" href="#" bdt-slidenav-previous bdt-slideshow-item="previous"></a>

          <ul class="bdt-slideshow-nav bdt-ps-dotnav bdt-dotnav bdt-dotnav-vertical"></ul>

          <a class="bdt-prime-slider-next" href="#" bdt-slidenav-next bdt-slideshow-item="next"></a>
        </div>
        <?php endif; ?>

        <?php

    }

    public function render_thumbnav() {
        $settings = $this->get_settings_for_display();
        ?>

        <?php if ( 'yes' == $settings['show_thumbnav'] ) : ?>
        <div class="bdt-thumb-wrapper bdt-position-bottom-<?php echo esc_attr( $settings['featured_post_alignment'] ) ;?> bdt-position-large">
          <ul class="bdt-slider-items bdt-child-width-1-2 bdt-child-width-1-3@s bdt-child-width-1-4@m bdt-child-width-1-5@l bdt-child-width-expand@xl  bdt-grid">
              <?php
              $slide_index = 1;

              $wp_query = $this->query_posts();

              if ( !$wp_query->found_posts ) {
                  return;
              }

              while ( $wp_query->have_posts() ) {
                  $wp_query->the_post();

                  ?>

                <li class="bdt-ps-thumbnav bdt-position-relative" bdt-slideshow-item="<?php echo($slide_index - 1); ?>">
                  <a href="#">
                    <div class="bdt-thumb-content">
                        <?php $this->rendar_thumb_image(); ?>
                          <span><?php echo get_the_title(); ?></span>
                    </div>
                  </a>
                    <?php $slide_index++; ?>
                </li>

                  <?php
              }

              wp_reset_postdata(); ?>

          </ul>
        </div>
        <?php endif; ?>

        <?php
    }

public function render_footer() {
    ?>

  </ul>

    <?php $this->render_navigation_arrows_dots(); ?>

    <?php $this->render_thumbnav(); ?>

  </div>

  </div>
    <?php
}

    public function rendar_item_image() {
        $settings = $this->get_settings_for_display();

        $placeholder_image_src = Utils::get_placeholder_image_src();
        $image_src = Group_Control_Image_Size::get_attachment_image_src(get_post_thumbnail_id(), 'thumbnail_size', $settings);
        
        if ( $image_src ) {
            $image_final_src = $image_src;
        } elseif ( $placeholder_image_src ) {
            $image_final_src = $placeholder_image_src;
        } else {
            return;
        }

        ?>

        <div class="bdt-ps-slide-img" style="background-image: url('<?php echo esc_url($image_final_src); ?>')"></div>

        <?php
    }

    public function rendar_thumb_image() {

        $placeholder_image_src = Utils::get_placeholder_image_src();
        $image_src             = wp_get_attachment_image_src(get_post_thumbnail_id(), 'medium');

        if ( $image_src[0] ) {
            $image_src = $image_src[0];
        } else {
            $image_src = $placeholder_image_src;
        }

        ?>

        <img src="<?php echo esc_url($image_src); ?>" alt="<?php echo get_the_title(); ?>">

        <?php
    }

    public function render_excerpt() {
        if ( !$this->get_settings( 'show_excerpt' ) ) {
            return;
        }
        
        $strip_shortcode = $this->get_settings_for_display( 'strip_shortcode' );
        
        ?>
        <div class="bdt-blog-text">
            <?php
                if ( has_excerpt() ) {
                    the_excerpt();
                } else {
                    echo prime_slider_custom_excerpt( $this->get_settings_for_display( 'excerpt_length' ), $strip_shortcode );
                }
            ?>
        </div>
        <?php
    }

    public function render_item_content($post) {
        $settings = $this->get_settings_for_display();

        ?>

      <div class="bdt-container">
        <div class="bdt-prime-slider-content">

            <?php if ( 'yes' == $settings['show_category'] ) : ?>
              <div class="bdt-ps-category-wrapper" bdt-slideshow-parallax="y: 50,0,-50; opacity: 1,1,0">
                  <?php $this->render_category(); ?>
              </div>
            <?php endif; ?>

            <?php if ('yes' == $settings['show_title']) : ?>
          <div class="bdt-main-title" bdt-slideshow-parallax="y: 80,0,-80; opacity: 1,1,0">
            <<?php echo Utils::get_valid_html_tag($settings['title_html_tag']); ?> class="bdt-title-tag">

            <a href="<?php echo esc_url(get_permalink($post->ID)); ?>">
                <?php echo prime_slider_first_word(get_the_title()); ?>
            </a>

          </<?php echo Utils::get_valid_html_tag($settings['title_html_tag']); ?>>

          <?php $this->render_excerpt(); ?>

        </div>
          <?php endif; ?>

          <?php if ( 'yes' == $settings['show_admin_info'] ) : ?>
            <div class="bdt-prime-slider-meta bdt-flex-inline bdt-flex-middle" bdt-slideshow-parallax="y: 70,-30">
              <div class="bdt-post-slider-author bdt-margin-small-right bdt-border-circle bdt-overflow-hidden">
                  <?php echo get_avatar(get_the_author_meta('ID'), 32); ?>
              </div>
              <div class="bdt-meta-author bdt-flex bdt-flex-middle">
                <span class="bdt-author">
                <?php if ($settings['published_by'] == 'yes') : ?>
                <?php echo esc_html_x('Published by ', 'Frontend', 'bdthemes-prime-slider'); ?>
                <?php endif; ?>
                <?php echo esc_attr(get_the_author()); ?> </span>
              </div>
            </div>
          <?php endif; ?>

      </div>
      </div>

        <?php
    }

    public function render_slides_loop() {
        $settings = $this->get_settings_for_display();

        $kenburns_reverse = $settings['kenburns_reverse'] ? ' bdt-animation-reverse' : '';

        $slide_index = 1;

        global $post;

        $wp_query = $this->query_posts();

        if ( !$wp_query->found_posts ) {
            return;
        }

        while ( $wp_query->have_posts() ) {
            $wp_query->the_post();

            ?>

          <li class="bdt-slideshow-item bdt-flex bdt-flex-middle elementor-repeater-item-<?php echo get_the_ID(); ?>">

              <?php if ('yes' == $settings['kenburns_animation']) : ?>
            <div
                class="bdt-position-cover bdt-animation-kenburns<?php echo esc_attr($kenburns_reverse); ?> bdt-transform-origin-center-left">
                <?php endif; ?>

                <?php $this->rendar_item_image(); ?>

                <?php if ('yes' == $settings['kenburns_animation']) : ?>
            </div>
          <?php endif; ?>

              <?php if ( 'none' !== $settings['overlay'] ) :
                  $blend_type = ('blend' == $settings['overlay']) ? ' bdt-blend-' . $settings['blend_type'] : ''; ?>
                <div class="bdt-overlay-default bdt-position-cover<?php echo esc_attr($blend_type); ?>"></div>
              <?php endif; ?>

              <?php $this->render_item_content($post); ?>

              <?php $slide_index++; ?>

          </li>


            <?php
        }

        wp_reset_postdata();

    }

    public function render() {

        $this->render_header();

        $this->render_slides_loop();

        $this->render_footer();
    }

}