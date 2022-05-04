<?php
namespace PrimeSlider\Modules\Paranoia;

use PrimeSlider\Base\Prime_Slider_Module_Base;

if ( ! defined( 'ABSPATH' ) ) exit; // Exit if accessed directly

class Module extends Prime_Slider_Module_Base {

	public function get_name() {
		return 'paranoia';
	}

	public function get_widgets() {
		$widgets = [
			'Paranoia',
		];

		return $widgets;
	}
}
