<?php
    
    /**
     * Prime slider widget activation filters
     * @since 1.12.6
     */
    
    if ( !defined( 'ABSPATH' ) ) exit; // Exit if accessed directly
    
    // Admin Settings Filters
    
    if ( !function_exists( 'ps_is_dashboard_enabled' ) ) {
        function ps_is_dashboard_enabled() {
            if ( bdt_ps()->is_plan('agency') ) {
                return apply_filters( 'primeslider/settings/dashboard', true );
            } else {
                return true;
            }
        }
    }
    if ( !function_exists( 'ps_is_affiliation_enabled' ) ) { //TODO
        function ps_is_affiliation_enabled() {
            if ( bdt_ps()->is_plan('agency') ) {
                return apply_filters( 'primeslider/settings/affiliation', true );
            } else {
                return true;
            }
        }
    }
    
    if ( !function_exists( 'ps_is_account_enabled' ) ) { //TODO
        function ps_is_account_enabled() {
            if ( bdt_ps()->is_plan('agency') ) {
                return apply_filters( 'primeslider/settings/account', true );
            } else {
                return true;
            }
        }
    }
    
    if ( !function_exists( 'ps_is_contact_enabled' ) ) { //TODO
        function ps_is_contact_enabled() {
            if ( bdt_ps()->is_plan('agency') ) {
                return apply_filters( 'primeslider/settings/contact', true );
            } else {
                return true;
            }
        }
    }
    if ( !function_exists( 'ps_is_upgrade_mode_enabled' ) ) { //TODO
        function ps_is_upgrade_mode_enabled() {
            if ( bdt_ps()->is_plan('agency') ) {
                return apply_filters( 'primeslider/settings/upgrade_mode', true );
            } else {
                return true;
            }
        }
    }
    
    if ( !function_exists( 'ps_is_giveaway_notice_enabled' ) ) {
        function ps_is_giveaway_notice_enabled() {
            return apply_filters( 'primeslider/settings/giveaway_notice', true );
        }
    }
    
    // Widgets filters
    if ( !function_exists( 'ps_is_blog_enabled' ) ) {
        function ps_is_blog_enabled() {
            return apply_filters( 'primeslider/widgets/blog', true );
        }
    }
    
    if ( !function_exists( 'ps_is_dragon_enabled' ) ) {
        function ps_is_dragon_enabled() {
            return apply_filters( 'primeslider/widgets/dragon', true );
        }
    }
    
    if ( !function_exists( 'ps_is_flogia_enabled' ) ) {
        function ps_is_flogia_enabled() {
            return apply_filters( 'primeslider/widgets/flogia', true );
        }
    }
    
    if ( !function_exists( 'ps_is_general_enabled' ) ) {
        function ps_is_general_enabled() {
            return apply_filters( 'primeslider/widgets/general', true );
        }
    }
    
    if ( !function_exists( 'ps_is_isolate_enabled' ) ) {
        function ps_is_isolate_enabled() {
            return apply_filters( 'primeslider/widgets/isolate', true );
        }
    }
    
    if ( !function_exists( 'ps_is_mount_enabled' ) ) {
        function ps_is_mount_enabled() {
            return apply_filters( 'primeslider/widgets/mount', true );
        }
    }
    
    if ( !function_exists( 'ps_is_multiscroll_enabled' ) ) {
        function ps_is_multiscroll_enabled() {
            return apply_filters( 'primeslider/widgets/multiscroll', true );
        }
    }

    if ( !function_exists( 'ps_is_pacific_enabled' ) ) {
        function ps_is_pacific_enabled() {
            return apply_filters( 'primeslider/widgets/pacific', true );
        }
    }

    if ( !function_exists( 'ps_is_pagepiling_enabled' ) ) {
        function ps_is_pagepiling_enabled() {
            return apply_filters( 'primeslider/widgets/pagepiling', true );
        }
    }

    if ( !function_exists( 'ps_is_paranoia_enabled' ) ) {
        function ps_is_paranoia_enabled() {
            return apply_filters( 'primeslider/widgets/paranoia', true );
        }
    }
    
    if ( !function_exists( 'ps_is_sequester_enabled' ) ) {
        function ps_is_sequester_enabled() {
            return apply_filters( 'primeslider/widgets/sequester', true );
        }
    }
    
    if ( !function_exists( 'ps_is_custom_enabled' ) ) {
        function ps_is_custom_enabled() {
            return apply_filters( 'primeslider/widgets/custom', true );
        }
    }
    
    if ( !function_exists( 'ps_is_fluent_enabled' ) ) {
        function ps_is_fluent_enabled() {
            return apply_filters( 'primeslider/widgets/fluent', true );
        }
    }
    
    if ( !function_exists( 'ps_is_flexure_enabled' ) ) {
        function ps_is_flexure_enabled() {
            return apply_filters( 'primeslider/widgets/flexure', true );
        }
    }
    
    if ( !function_exists( 'ps_is_monster_enabled' ) ) {
        function ps_is_monster_enabled() {
            return apply_filters( 'primeslider/widgets/monster', true );
        }
    }
    
    if ( !function_exists( 'ps_is_event_calendar_enabled' ) ) {
        function ps_is_event_calendar_enabled() {
            return apply_filters( 'primeslider/widgets/event_calendar', true );
        }
    }
    
    if ( !function_exists( 'ps_is_woostand_enabled' ) ) {
        function ps_is_woostand_enabled() {
            return apply_filters( 'primeslider/widgets/woostand', true );
        }
    }
    
    if ( !function_exists( 'ps_is_woocommerce_enabled' ) ) {
        function ps_is_woocommerce_enabled() {
            return apply_filters( 'primeslider/widgets/woocommerce', true );
        }
    }
    
    if ( !function_exists( 'ps_is_woolamp_enabled' ) ) {
        function ps_is_woolamp_enabled() {
            return apply_filters( 'primeslider/widgets/woolamp', true );
        }
    }
    
    if ( !function_exists( 'ps_is_wooexpand_enabled' ) ) {
        function ps_is_wooexpand_enabled() {
            return apply_filters( 'primeslider/widgets/wooexpand', true );
        }
    }

    if ( !function_exists( 'ps_is_woocircle_enabled' ) ) {
        function ps_is_woocircle_enabled() {
            return apply_filters( 'primeslider/widgets/woocircle', true );
        }
    }
    
    if ( !function_exists( 'ps_is_storker_enabled' ) ) {
        function ps_is_storker_enabled() {
            return apply_filters( 'primeslider/widgets/storker', true );
        }
    }
    
    if ( !function_exists( 'ps_is_knily_enabled' ) ) {
        function ps_is_knily_enabled() {
            return apply_filters( 'primeslider/widgets/knily', true );
        }
    }
    
    if ( !function_exists( 'ps_is_marble_enabled' ) ) {
        function ps_is_marble_enabled() {
            return apply_filters( 'primeslider/widgets/marble', true );
        }
    }
    
    if ( !function_exists( 'ps_is_fiestar_enabled' ) ) {
        function ps_is_fiestar_enabled() {
            return apply_filters( 'primeslider/widgets/fiestar', true );
        }
    }

    if ( !function_exists( 'ps_is_mercury_enabled' ) ) {
        function ps_is_mercury_enabled() {
            return apply_filters( 'primeslider/widgets/mercury', true );
        }
    }

    if ( !function_exists( 'ps_is_vertex_enabled' ) ) {
        function ps_is_vertex_enabled() {
            return apply_filters( 'primeslider/widgets/vertex', true );
        }
    }

    if ( !function_exists( 'ps_is_torque_enabled' ) ) {
        function ps_is_torque_enabled() {
            return apply_filters( 'primeslider/widgets/torque', true );
        }
    }

    if ( !function_exists( 'ps_is_astoria_enabled' ) ) {
        function ps_is_astoria_enabled() {
            return apply_filters( 'primeslider/widgets/astoria', true );
        }
    }

    if ( !function_exists( 'ps_is_crossroad_enabled' ) ) {
        function ps_is_crossroad_enabled() {
            return apply_filters( 'primeslider/widgets/crossroad', true );
        }
    }

    if ( !function_exists( 'ps_is_rubix_enabled' ) ) {
        function ps_is_rubix_enabled() {
            return apply_filters( 'primeslider/widgets/rubix', true );
        }
    }

    if ( !function_exists( 'ps_is_reveal_enabled' ) ) {
        function ps_is_reveal_enabled() {
            return apply_filters( 'primeslider/widgets/reveal', true );
        }
    }

    // if ( !function_exists( 'ps_is_diagonal_enabled' ) ) {
    //     function ps_is_diagonal_enabled() {
    //         return apply_filters( 'primeslider/widgets/diagonal', true );
    //     }
    // }

    // if ( !function_exists( 'ps_is_rasher_enabled' ) ) {
    //     function ps_is_rasher_enabled() {
    //         return apply_filters( 'primeslider/widgets/rasher', true );
    //     }
    // }

    if ( !function_exists( 'ps_is_pieces_enabled' ) ) {
        function ps_is_pieces_enabled() {
            return apply_filters( 'primeslider/widgets/pieces', true );
        }
    }

    if ( !function_exists( 'ps_is_prism_enabled' ) ) {
        function ps_is_prism_enabled() {
            return apply_filters( 'primeslider/widgets/prism', true );
        }
    }

    if ( !function_exists( 'ps_is_elysium_enabled' ) ) {
        function ps_is_elysium_enabled() {
            return apply_filters( 'primeslider/widgets/elysium', true );
        }
    }

    // if ( !function_exists( 'ps_is_landscape_enabled' ) ) {
    //     function ps_is_landscape_enabled() {
    //         return apply_filters( 'primeslider/widgets/landscape', true );
    //     }
    // }
