<?php

namespace PrimeSlider;

if ( !defined( 'ABSPATH' ) ) {
    exit;
}
// Exit if accessed directly
if ( !function_exists( 'is_plugin_active' ) ) {
    include_once ABSPATH . 'wp-admin/includes/plugin.php';
}
final class Manager
{
    private  $_modules = null ;
    private function is_module_active( $module_id )
    {
        $module_data = $this->get_module_data( $module_id );
        $options = get_option( 'prime_slider_active_modules', [] );
        
        if ( !isset( $options[$module_id] ) ) {
            return $module_data['default_activation'];
        } else {
            
            if ( $options[$module_id] == "on" ) {
                return true;
            } else {
                return false;
            }
        
        }
    
    }
    
    private function has_module_style( $module_id )
    {
        $module_data = $this->get_module_data( $module_id );
        
        if ( isset( $module_data['has_style'] ) ) {
            return $module_data['has_style'];
        } else {
            return false;
        }
    
    }
    
    private function has_module_script( $module_id )
    {
        $module_data = $this->get_module_data( $module_id );
        
        if ( isset( $module_data['has_script'] ) ) {
            return $module_data['has_script'];
        } else {
            return false;
        }
    
    }
    
    private function get_module_data( $module_id )
    {
        return ( isset( $this->_modules[$module_id] ) ? $this->_modules[$module_id] : false );
    }
    
    public function __construct()
    {
        $modules = [];
        if ( ps_is_blog_enabled() ) {
            $modules[] = 'blog';
        }
        if ( ps_is_dragon_enabled() ) {
            $modules[] = 'dragon';
        }
        if ( ps_is_elysium_enabled() ) {
            $modules[] = 'elysium';
        }
        if ( ps_is_fiestar_enabled() ) {
            $modules[] = 'fiestar';
        }
        if ( ps_is_flogia_enabled() ) {
            $modules[] = 'flogia';
        }
        if ( ps_is_general_enabled() ) {
            $modules[] = 'general';
        }
        if ( ps_is_isolate_enabled() ) {
            $modules[] = 'isolate';
        }
        if ( ps_is_mercury_enabled() ) {
            $modules[] = 'mercury';
        }
        if ( ps_is_mount_enabled() ) {
            $modules[] = 'mount';
        }
        if ( ps_is_multiscroll_enabled() ) {
            $modules[] = 'multiscroll';
        }
        if ( ps_is_pacific_enabled() ) {
            $modules[] = 'pacific';
        }
        if ( ps_is_pagepiling_enabled() ) {
            $modules[] = 'pagepiling';
        }
        if ( ps_is_paranoia_enabled() ) {
            $modules[] = 'paranoia';
        }
        if ( ps_is_pieces_enabled() ) {
            $modules[] = 'pieces';
        }
        // if ( ps_is_diagonal_enabled() ) {
        //     $modules[] = 'diagonal';
        // }
        // if ( ps_is_rasher_enabled() ) {
        // 	$modules[] = 'rasher';
        // }
        if ( ps_is_rubix_enabled() ) {
            $modules[] = 'rubix';
        }
        if ( ps_is_sequester_enabled() ) {
            $modules[] = 'sequester';
        }
        if ( ps_is_storker_enabled() ) {
            $modules[] = 'storker';
        }
        if ( ps_is_vertex_enabled() ) {
            $modules[] = 'vertex';
        }
        if ( is_plugin_active( 'woocommerce/woocommerce.php' ) and ps_is_woocommerce_enabled() ) {
            $modules[] = 'woocommerce';
        }
        if ( is_plugin_active( 'woocommerce/woocommerce.php' ) and ps_is_woocircle_enabled() ) {
            $modules[] = 'woocircle';
        }
        if ( is_plugin_active( 'woocommerce/woocommerce.php' ) and ps_is_woolamp_enabled() ) {
            $modules[] = 'woolamp';
        }
        // Fetch all modules data
        foreach ( $modules as $module ) {
            $this->_modules[$module] = (require BDTPS_MODULES_PATH . $module . '/module.info.php');
        }
        $direction_suffix = ( is_rtl() ? '.rtl' : '' );
        $suffix = ( defined( 'SCRIPT_DEBUG' ) && SCRIPT_DEBUG ? '' : '.min' );
        foreach ( $this->_modules as $module_id => $module_data ) {
            if ( !$this->is_module_active( $module_id ) ) {
                continue;
            }
            $class_name = str_replace( '-', ' ', $module_id );
            $class_name = str_replace( ' ', '', ucwords( $class_name ) );
            $class_name = __NAMESPACE__ . '\\Modules\\' . $class_name . '\\Module';
            // register widget css
            if ( $this->has_module_style( $module_id ) ) {
                wp_register_style(
                    'ps-' . $module_id,
                    BDTPS_URL . 'assets/css/ps-' . $module_id . $direction_suffix . '.css',
                    [],
                    BDTPS_VER
                );
            }
            // register widget javascript
            if ( $this->has_module_script( $module_id ) ) {
                wp_register_script(
                    'ps-' . $module_id,
                    BDTPS_URL . 'assets/js/widgets/ps-' . $module_id . $suffix . '.js',
                    [
                    'jquery',
                    'bdt-uikit',
                    'elementor-frontend',
                    'prime-slider-site'
                ],
                    BDTPS_VER,
                    true
                );
            }
            $class_name::instance();
        }
    }

}