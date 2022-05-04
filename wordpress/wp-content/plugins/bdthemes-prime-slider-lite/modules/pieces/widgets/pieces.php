<?php

namespace PrimeSlider\Modules\Pieces\Widgets;

use Elementor\Widget_Base;
use Elementor\Controls_Manager;
use Elementor\Group_Control_Border;
use Elementor\Group_Control_Typography;
use Elementor\Group_Control_Background;
use Elementor\Group_Control_Box_Shadow;
use Elementor\Group_Control_Image_Size;
use Elementor\Repeater;
use PrimeSlider\Utils;

if (!defined('ABSPATH')) exit; // Exit if accessed directly

class Pieces extends Widget_Base {

	public function get_name()
	{
		return 'prime-slider-pieces';
	}

	public function get_title()
	{
		return BDTPS . esc_html__('Pieces', 'bdthemes-prime-slider');
	}

	public function get_icon()
	{
		return 'bdt-widget-icon ps-wi-pieces bdt-new';
	}

	public function get_categories()
	{
		return ['prime-slider'];
	}

	public function get_keywords()
	{
		return ['prime slider', 'slider', 'pieces', 'prime'];
	}

	public function get_style_depends()
	{
		return ['prime-slider-font', 'ps-pieces'];
	}

	public function get_script_depends() {
		return ['anime', 'pieces', 'ps-pieces'];
	}

	protected function register_controls() {

		$this->start_controls_section(
			'section_content_sliders',
			[
				'label' => esc_html__('Sliders', 'bdthemes-prime-slider'),
			]
		);

		$repeater = new Repeater();

		$repeater->add_control(
			'title',
			[
				'label'       => esc_html__('Title', 'bdthemes-prime-slider'),
				'type'        => Controls_Manager::TEXT,
				'label_block' => true,
				'dynamic'     => ['active' => true],
			]
		);

		$repeater->add_control(
			'title_link',
			[
				'label'         => esc_html__('Title Link', 'bdthemes-prime-slider'),
				'type'          => Controls_Manager::URL,
				'default'       => ['url' => ''],
				'show_external' => false,
				'dynamic'       => ['active' => true],
				'condition'     => [
					'title!' => ''
				]
			]
		);

		$repeater->add_control(
			'image',
			[
				'label'     => esc_html__('Image', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::MEDIA,
				'default' => [
					'url' => Utils::get_placeholder_image_src(),
				],
				'dynamic'     => ['active' => true],
			]
		);

		$this->add_control(
			'slides',
			[
				'label'   => esc_html__('Slider Items', 'bdthemes-prime-slider'),
				'type'    => Controls_Manager::REPEATER,
				'fields'  => $repeater->get_controls(),
				'default' => [
					[
						'title'     => esc_html__('Pastiche Skin', 'bdthemes-prime-slider'),
						'image'     => ['url' => BDTPS_ASSETS_URL . 'images/gallery/item-1.png']
					],
					[
						'title'     => esc_html__('Enfilade Trees', 'bdthemes-prime-slider'),
						'image'     => ['url' => BDTPS_ASSETS_URL . 'images/gallery/item-2.png']
					],
					[
						'title'     => esc_html__('Vernacular Blue', 'bdthemes-prime-slider'),
						'image'     => ['url' => BDTPS_ASSETS_URL . 'images/gallery/item-3.png']
					],
				],
				'title_field' => '{{{ title }}}',
			]
		);

		$this->end_controls_section();

		$this->start_controls_section(
			'section_additional_settings',
			[
				'label' => esc_html__('Additional Settings', 'bdthemes-prime-slider'),
			]
		);

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
				'label'   => __('Title HTML Tag', 'bdthemes-prime-slider'),
				'type'    => Controls_Manager::SELECT,
				'default' => 'div',
				'options' => prime_slider_title_tags(),
				'condition' => [
					'show_title' => 'yes'
				]
			]
		);

		$this->add_group_control(
			Group_Control_Image_Size::get_type(),
			[
				'name'         => 'thumbnail_size',
				'label'        => esc_html__('Image Size', 'bdthemes-prime-slider'),
				'exclude'      => ['custom'],
				'default'      => 'full',
			]
		);

		$this->add_responsive_control(
			'image_height',
			[
				'label' => esc_html__( 'Image Height', 'bdthemes-prime-slider' ),
				'type'  => Controls_Manager::SLIDER,
				'size_units' => [ 'px', 'vh' ],
				'range' => [
					'px' => [
						'min' => 50,
						'max' => 1080,
					],
					'vh' => [
						'min' => 10,
						'max' => 100,
					],
				],
				'selectors' => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__image' => 'max-height: {{SIZE}}{{UNIT}};',
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__canvas' => 'height: {{SIZE}}{{UNIT}};',
					'{{WRAPPER}} .bdt-pieces-slider' => 'min-height: {{SIZE}}{{UNIT}};',
				],
				'render_type' => 'template'
			]
		);

		$this->add_responsive_control(
			'image_width',
			[
				'label' => esc_html__( 'Image Width', 'bdthemes-prime-slider' ),
				'type'  => Controls_Manager::SLIDER,
				'size_units' => [ 'px', 'vw' ],
				'range' => [
					'px' => [
						'min' => 50,
						'max' => 1200,
					],
					'vw' => [
						'min' => 10,
						'max' => 100,
					],
				],
				'selectors' => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__image' => 'max-width: {{SIZE}}{{UNIT}};',
				],
				'render_type' => 'template'
			]
		);

		$this->end_controls_section();

		//Style
		$this->start_controls_section(
			'section_style_title',
			[
				'label'     => __('Title', 'bdthemes-prime-slider'),
				'tab'       => Controls_Manager::TAB_STYLE,
				'condition' => [
					'show_title' => 'yes',
				],
			]
		);

		$this->add_control(
			'title_color',
			[
				'label'     => esc_html__('Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
			]
		);

		$this->add_control(
			'title_background',
			[
				'label'     => esc_html__('Background Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
			]
		);

		$this->add_control(
			'title_radius',
			[
				'label' => esc_html__( 'Radius', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::SLIDER,
				'range' => [
					'px' => [
						'min' => 0,
						'max' => 500,
					],
				],
			]
		);

		$this->add_responsive_control(
			'title_padding',
			[
				'label'      => esc_html__('Padding', 'bdthemes-prime-slider'),
				'type'       => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', 'em', '%'],
			]
		);

		$this->add_responsive_control(
			'title_font_size',
			[
				'label' => esc_html__( 'Font Size', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::SLIDER,
				'range' => [
					'px' => [
						'min' => 0,
						'max' => 100,
					],
				],
				'devices' => [ 'desktop', 'tablet' ],
				'desktop_default' => [
					'size' => 50,
					'unit' => 'px',
				],
				'tablet_default' => [
					'size' => 30,
					'unit' => 'px',
				],
			]
		);

		$this->add_control(
			'font_family',
			[
				'label' => esc_html__( 'Font Family', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::FONT,
				'selectors' => [
					'{{WRAPPER}} canvas' => 'font-family: {{VALUE}}',
				],
				'render_type' => 'template'
			]
		);

		$this->end_controls_section();

		$this->start_controls_section(
			'section_style_number',
			[
				'label'     => __('Number', 'bdthemes-prime-slider'),
				'tab'       => Controls_Manager::TAB_STYLE,
			]
		);

		$this->add_control(
			'number_color',
			[
				'label'     => esc_html__('Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
			]
		);

		$this->add_control(
			'number_background',
			[
				'label'     => esc_html__('Background Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
			]
		);

		$this->add_control(
			'number_radius',
			[
				'label' => esc_html__( 'Radius', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::SLIDER,
				'range' => [
					'px' => [
						'min' => 0,
						'max' => 500,
					],
				],
			]
		);

		$this->add_responsive_control(
			'number_padding',
			[
				'label'      => esc_html__('Padding', 'bdthemes-prime-slider'),
				'type'       => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', 'em', '%'],
			]
		);

		$this->add_responsive_control(
			'number_font_size',
			[
				'label' => esc_html__( 'Font Size', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::SLIDER,
				'range' => [
					'px' => [
						'min' => 0,
						'max' => 100,
					],
				],
				'devices' => [ 'desktop', 'tablet' ],
				'desktop_default' => [
					'size' => 50,
					'unit' => 'px',
				],
				'tablet_default' => [
					'size' => 30,
					'unit' => 'px',
				],
			]
		);

		$this->add_control(
			'number_font_family',
			[
				'label' => esc_html__( 'Font Family', 'bdthemes-prime-slider' ),
				'type' => Controls_Manager::FONT,
				'selectors' => [
					'{{WRAPPER}} canvas' => 'font-family: {{VALUE}}',
				],
				'render_type' => 'template'
			]
		);

		$this->end_controls_section();


		//navigation
		$this->start_controls_section(
			'section_style_navigation_button',
			[
				'label'     => __('Navigation', 'bdthemes-prime-slider'),
				'tab'       => Controls_Manager::TAB_STYLE,
			]
		);

		$this->start_controls_tabs('tabs_navigation_button_style');

		$this->start_controls_tab(
			'tabs_nav_navigation_button_normal',
			[
				'label'     => __('Normal', 'bdthemes-prime-slider'),
			]
		);

		$this->add_control(
			'navigation_button_text_color',
			[
				'label'     => __('Text Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
				'selectors' => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button' => 'color: {{VALUE}}',
				],
			]
		);

		$this->add_group_control(
			Group_Control_Background::get_type(),
			[
				'name'     => 'navigation_button_background',
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button',
			]
		);

		$this->add_group_control(
			Group_Control_Border::get_type(),
			[
				'name'     => 'navigation_button_border',
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button',
			]
		);

		$this->add_responsive_control(
			'navigation_button_radius',
			[
				'label'      => esc_html__('Border Radius', 'bdthemes-prime-slider'),
				'type'       => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', '%'],
				'selectors'  => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button' => 'border-radius: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
				],
			]
		);

		$this->add_responsive_control(
			'navigation_button_padding',
			[
				'label' 	 => __('Padding', 'bdthemes-prime-slider'),
				'type' 		 => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', 'em', '%'],
				'selectors'  => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button' => 'padding: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
				],
			]
		);


		$this->add_responsive_control(
			'navigation_button_margin',
			[
				'label' 	 => __('Margin', 'bdthemes-prime-slider'),
				'type' 		 => Controls_Manager::DIMENSIONS,
				'size_units' => ['px', 'em', '%'],
				'selectors'  => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button' => 'margin: {{TOP}}{{UNIT}} {{RIGHT}}{{UNIT}} {{BOTTOM}}{{UNIT}} {{LEFT}}{{UNIT}};',
				],
			]
		);


		$this->add_group_control(
			Group_Control_Box_Shadow::get_type(),
			[
				'name'     => 'navigation_button_shadow',
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button',
			]
		);

		$this->add_group_control(
			Group_Control_Typography::get_type(),
			[
				'name'     => 'navigation_button_typography',
				'label'    => esc_html__('Typography', 'bdthemes-prime-slider'),
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button',
			]
		);

		$this->end_controls_tab();

		$this->start_controls_tab(
			'tabs_navigation_button_hover',
			[
				'label'     => __('Hover', 'bdthemes-prime-slider'),
			]
		);

		$this->add_control(
			'navigation_butoon_text_hover_color',
			[
				'label'     => __('Text Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
				'selectors' => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button:hover' => 'color: {{VALUE}}',
				],
			]
		);


		$this->add_group_control(
			Group_Control_Background::get_type(),
			[
				'name'     => 'navigation_button_hover_background',
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button:hover',
			]
		);

		$this->add_control(
			'navigation_button_border_hover',
			[
				'label'     => esc_html__('Border Color', 'bdthemes-prime-slider'),
				'type'      => Controls_Manager::COLOR,
				'selectors' => [
					'{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button:hover' => 'border-color: {{VALUE}};',
				],
			]
		);

		$this->add_group_control(
			Group_Control_Box_Shadow::get_type(),
			[
				'name'     => 'navigation_button_hover_shadow',
				'selector' => '{{WRAPPER}} .bdt-pieces-slider .pieces-slider__button:hover',
			]
		);

		$this->end_controls_tab();

		$this->end_controls_tabs();

		$this->end_controls_section();
	}

	protected function render_header()
	{
		$settings   = $this->get_settings_for_display();
		$id         = 'bdt-prime-slider-' . $this->get_id();

		// start target
		$targets = [];
		if ($settings['show_title'] == 'yes') {
			$targets[0] = '.bdt-title';
		}
		$targets = implode(', ', $targets);
		// end target

		$this->add_render_attribute(
			[
				'ps-pieces' => [
					'id' => $id,
					'class' => ['bdt-pieces-slider'],
					'data-settings' => [
						wp_json_encode(
							array_filter([
								"id"      => '#' . $id,
								'targets' => $targets,
								'title'   => array_filter([
									'color'  => $settings['title_color'],
									'background'  => $settings['title_background'],
									'backgroundRadius'  => $settings['title_radius']['size'],
									'padding'	=> $settings['title_padding'],
									'fontFamily' => !empty(esc_attr( $settings['font_family'] )) ? esc_attr( $settings['font_family'] ) : 'Open Sans',
									'fontSizeDesktop' => (isset($settings['title_font_size'])) ? (int) $settings['title_font_size']['size'] : '',
									'fontSizeTablet' => (isset($settings['title_font_size_tablet'])) ? $settings['title_font_size_tablet']['size'] : ''
								]),
								'number'   => array_filter([
									'color'  => $settings['number_color'],
									'background'  => $settings['number_background'],
									'backgroundRadius'  => $settings['number_radius']['size'],
									'padding'	=> $settings['number_padding'],
									'fontFamily' => !empty(esc_attr( $settings['number_font_family'] )) ? esc_attr( $settings['number_font_family'] ) : 'Open Sans',
									'fontSizeDesktop' => (isset($settings['number_font_size'])) ? (int) $settings['number_font_size']['size'] : '',
									'fontSizeTablet' => (isset($settings['number_font_size_tablet'])) ? $settings['number_font_size_tablet']['size'] : ''
								])

							])
						),
					],
				],
			]
		);

		$this->add_render_attribute('prime-slider', 'class', 'bdt-prime-slider');


?>
		<div <?php $this->print_render_attribute_string('prime-slider'); ?>>
			<div <?php $this->print_render_attribute_string('ps-pieces'); ?>>
			<?php
		}

		public function render_footer()
		{
			$settings = $this->get_settings_for_display();

			?>
				<canvas class="pieces-slider__canvas"></canvas>

				<button class="pieces-slider__button pieces-slider__button--prev"><?php echo esc_html_x('prev', 'Frontend', 'bdthemes-prime-slider') ?></button>
				<button class="pieces-slider__button pieces-slider__button--next"><?php echo esc_html_x('next', 'Frontend', 'bdthemes-prime-slider') ?></button>
			</div>
		</div>
	<?php
		}



		public function render_title($slide)
		{
			$settings = $this->get_settings_for_display();
			if ('' == $settings['show_title']) {
				return;
			}
	?>
		<<?php echo  Utils::get_valid_html_tag($settings['title_html_tag']); ?> class="pieces-slider__text">
			<?php if ('' !== $slide['title_link']['url']) : ?>
				<a href="<?php echo esc_url($slide['title_link']['url']); ?>">
				<?php endif; ?>
				<?php echo esc_html($slide['title']); ?>
				<?php if ('' !== $slide['title_link']['url']) : ?>
				</a>
			<?php endif; ?>
		</<?php echo Utils::get_valid_html_tag($settings['title_html_tag']); ?>>
	<?php

		}

	public function rendar_item_image($item, $alt = '') {
		$settings = $this->get_settings_for_display();
		
		$image_src = Group_Control_Image_Size::get_attachment_image_src($item['image']['id'], 'thumbnail_size', $settings);

		if ( $image_src ) {
			$image_src = $image_src;
		} elseif ( $item['image']['url'] ) {
			$image_src = $item['image']['url'];
		} else {
			return;
		}
		?>

		<img class="pieces-slider__image" src="<?php echo esc_url($image_src); ?>" alt="<?php echo esc_html($alt); ?>">

		<?php
	}


		public function render_slides_loop()
		{
			$settings = $this->get_settings_for_display();

			$i = 0;

			foreach ($settings['slides'] as $slide) : ?>

			<div class="pieces-slider__slide">

				<?php $this->rendar_item_image($slide, $slide['title']); ?>

				<?php $this->render_title($slide); ?>

			</div>
<?php

				$i++;

			endforeach;
		}

		public function render()
		{
			$this->render_header();
			$this->render_slides_loop();
			$this->render_footer();
		}
	}
