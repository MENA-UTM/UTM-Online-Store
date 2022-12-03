<?php

define('DB_SERVER','localhost');
define('DB_USERNAME', 'root');
define('DB_PASSWORD', '');
define('DB_NAME','login');

$mysql=new MySQLi(DB_SERVER,DB_USERNAME,DB_PASSWORD,DB_NAME);

if($mysql===false){
    die("ERROR: Cannot connect".$mysql->connect_error);
}

?>