<?php
    namespace PrimeSlider;
    
    if ( !defined( 'ABSPATH' ) ) exit; // Exit if accessed directly
    
    class Admin {
        
        public function __construct() {
            add_action( 'admin_init', [$this, 'admin_script'], 99 );
            add_action( 'admin_enqueue_scripts', [$this, 'enqueue_styles'] );
            add_action( 'after_setup_theme', [$this, 'whitelabel'] );
        }
        
        /**
         * You can easily add white label branding for extended license or multi site license. Don't try for regular license otherwise your license will be invalid.
         * @return [type] [description]
         * Define BDTPS_WL for execute white label branding
         */
        public function whitelabel() {
            if ( defined( 'BDTPS_WL' ) ) {
                
                add_filter( 'gettext', [$this, 'prime_slider_name_change'], 20, 3 );
                
                if ( defined( 'BDTPS_HIDE' ) ) {
                    add_action( 'pre_current_active_plugins', [$this, 'hide_prime_slider'] );
                }
                
            } else {
                add_filter( 'plugin_row_meta', [$this, 'plugin_row_meta'], 10, 2 );
                add_filter( 'plugin_action_links_' . BDTPS_PBNAME, [$this, 'plugin_action_meta'] );
            }
        }
        
        public function enqueue_styles() {
            
            $suffix = is_rtl() ? '.rtl' : '';
            
            // stylesheet enqueue
            wp_enqueue_style( 'bdt-uikit', BDTPS_ASSETS_URL . 'css/bdt-uikit' . $suffix . '.css', [], '3.2' );
            wp_enqueue_style( 'bdthemes-prime-slider-admin', BDTPS_ASSETS_URL . 'css/admin' . $suffix . '.css', [], BDTPS_VER );
            wp_enqueue_style( 'prime-slider-font', BDTPS_ASSETS_URL . 'css/prime-slider-font' . $suffix . '.css', [], BDTPS_VER );
            
            // javascript enqueue
            wp_enqueue_script( 'bdt-uikit', BDTPS_ASSETS_URL . 'js/bdt-uikit.js', ['jquery'], BDTPS_VER );
            
            
        }
        
        
        public function plugin_row_meta( $plugin_meta, $plugin_file ) {
            if ( BDTPS_PBNAME === $plugin_file ) {
                $row_meta = [
                    'docs'  => '<a href="https://primeslider.pro/contact/" aria-label="' . esc_attr( __( 'Go for Get Support', 'bdthemes-prime-slider' ) ) . '" target="_blank">' . __( 'Get Support', 'bdthemes-prime-slider' ) . '</a>',
                    'video' => '<a href="https://www.youtube.com/playlist?list=PLP0S85GEw7DP3-yJrkgwpIeDFoXy0PDlM" aria-label="' . esc_attr( __( 'View Prime Slider Video Tutorials', 'bdthemes-prime-slider' ) ) . '" target="_blank">' . __( 'Video Tutorials', 'bdthemes-prime-slider' ) . '</a>',
                ];
                
                $plugin_meta = array_merge( $plugin_meta, $row_meta );
            }
            
            return $plugin_meta;
        }
        
        public function plugin_action_meta( $links ) {
            
            $links = array_merge( [sprintf( '<a href="%s">%s</a>', prime_slider_dashboard_link(), esc_html__( 'Settings', 'bdthemes-prime-slider' ) )], $links );
            
            return $links;
            
        }
        
        //Change Prime Slider Name
        public function prime_slider_name_change( $translated_text, $text, $domain ) {
            switch ($translated_text) {
                case 'Prime Slider' :
                    $translated_text = BDTPS_TITLE;
                    break;
            }
            return $translated_text;
        }
        
        //hiding plugins //still in testing purpose
        public function hide_prime_slider() {
            global $wp_list_table;
            $hide_plg_array = array('bdthemes-prime-slider/bdthemes-prime-slider.php');
            $all_plugins = $wp_list_table->items;
            
            foreach ( $all_plugins as $key => $val ) {
                if ( in_array( $key, $hide_plg_array ) ) {
                    unset( $wp_list_table->items[$key] );
                }
            }
        }
        
        
        public function admin_script() {
            $suffix = defined( 'SCRIPT_DEBUG' ) && SCRIPT_DEBUG ? '' : '.min';
            if ( is_admin() ) { // for Admin Dashboard Only
                // Embed the Script on our Plugin's Option Page Only
                if ( isset( $_GET['page'] ) && $_GET['page'] == 'prime_slider_options' ) {
                    wp_enqueue_script( 'jquery' );
                    wp_enqueue_script( 'jquery-form' );
                }
                
                wp_enqueue_script( 'prime-slider-admin-script', BDTPS_ASSETS_URL . 'js/prime-slider-admin' . $suffix . '.js', ['jquery'], BDTPS_VER, true );
            }
        }      
        
    }
