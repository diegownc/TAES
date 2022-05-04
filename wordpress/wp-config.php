<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the installation.
 * You don't have to use the web site, you can copy this file to "wp-config.php"
 * and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * Database settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://wordpress.org/support/article/editing-wp-config-php/
 *
 * @package WordPress
 */

// ** Database settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'taes' );

/** Database username */
define( 'DB_USER', 'root' );

/** Database password */
define( 'DB_PASSWORD', '' );

/** Database hostname */
define( 'DB_HOST', 'localhost' );

/** Database charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The database collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication unique keys and salts.
 *
 * Change these to different unique phrases! You can generate these using
 * the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}.
 *
 * You can change these at any point in time to invalidate all existing cookies.
 * This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         ':Vqfcyw,<?!~c_>qBl+@ieQB%5mWgZuLo<-;Ww/-%{r2m(}TN~Y5=LuExQWX&#4k' );
define( 'SECURE_AUTH_KEY',  'M<;9ry8C77OH uaJv^}AT;4EP7>iL2nIoc{)x5n^pHIzu#._]u=AwZh@>{_R$!?l' );
define( 'LOGGED_IN_KEY',    '2A$]{EiKcCsWR}-V~klutQ:baW,1LWAX?~i~d!Q-B*M:IdxKj,qMQ!}_)KV0B#-/' );
define( 'NONCE_KEY',        'KfH6:~Sp+-[Ql^AFOF1XyA?nc^oc?.20yo}{;Yd1MJ|x2$rQO$7?$8feh/^<K,CK' );
define( 'AUTH_SALT',        '5xgJ@x8VF-?w_#6Sqm`$[i?Ji9wo2>co;{AOh7!_:IJu=QY,)~dW7!|H-U&}xyB_' );
define( 'SECURE_AUTH_SALT', '#4XSHD22MzS=]#`CF>1]OEE-~[A(kRh]nC}tL2X@zJ#D02e7p]0[6d]gNgyrCvRQ' );
define( 'LOGGED_IN_SALT',   '*;lyg-qnGDp(I{RnB_oaj)$bno^GcN_U`Ph10}T;0#Vx/~m~|gGs|%`n)v-s)Vm!' );
define( 'NONCE_SALT',       '.`dej kr2L_YjU,rvv 8SESs400Zkdr LN+()lKPIj.s3_eLD[JU/!](Y?+_cn0[' );

/**#@-*/

/**
 * WordPress database table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://wordpress.org/support/article/debugging-in-wordpress/
 */
define( 'WP_DEBUG', false );

/* Add any custom values between this line and the "stop editing" line. */



/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';
