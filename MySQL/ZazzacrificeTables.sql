DROP SCHEMA IF EXISTS zazzacrifice;
CREATE SCHEMA zazzacrifice;
USE zazzacrifice;


CREATE TABLE users (
user_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
username VARCHAR(30) NOT NULL UNIQUE,
password VARCHAR(30) NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE game_sessions (
game_session_id  INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
user_id INT UNSIGNED NOT NULL,
time_on_seconds INT UNSIGNED NOT NULL DEFAULT 0,
finished BIT NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (user_id) REFERENCES users(user_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
 

CREATE TABLE events(
events_id TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
name VARCHAR(40) NOT NULL UNIQUE
)ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4;

CREATE TABLE game_events (
game_events_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
event_id TINYINT UNSIGNED NOT NULL,
game_session_id  INT UNSIGNED NOT NULL,
is_active BIT NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (event_id)  REFERENCES events(events_id) ON UPDATE CASCADE,
FOREIGN KEY (game_session_id)  REFERENCES game_sessions(game_session_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE stats (
stat_id TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
name VARCHAR(10) NOT NULL UNIQUE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE classes (
class_id TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
name VARCHAR(20) NOT NULL UNIQUE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE consumables (
consumable_id SMALLINT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
name VARCHAR(40) NOT NULL UNIQUE,
description VARCHAR(255) NOT NULL,
stat_id TINYINT UNSIGNED NOT NULL,
value SMALLINT UNSIGNED NOT NULL,
#time_in_second SMALLINT UNSIGNED NOT NULL,
FOREIGN KEY (stat_id) REFERENCES stats(stat_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE scenes (
scene_id TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
name VARCHAR(30)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE checkpoints (
checkpoint_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
scene_id TINYINT UNSIGNED NOT NULL DEFAULT 1,
x_postion INT NOT NULL DEFAULT 300,
y_position INT NOT NULL DEFAULT 300,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (scene_id) REFERENCES scenes(scene_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE players (
player_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE NOT NULL,
game_session_id INT UNSIGNED NOT NULL ,
name VARCHAR(15) NOT NULL,
money INT UNSIGNED NOT NULL,
checkpoint_id INT UNSIGNED,
class_id TINYINT UNSIGNED NOT NULL ,
FOREIGN KEY (game_session_id) REFERENCES game_sessions(game_session_id) ON UPDATE CASCADE,
FOREIGN KEY (checkpoint_id) REFERENCES checkpoints(checkpoint_id) ON UPDATE CASCADE,
FOREIGN KEY (class_id) REFERENCES classes(class_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE consumables_players (
consumable_player_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
player_id INT UNSIGNED NOT NULL ,
consumable_id SMALLINT UNSIGNED NOT NULL,
amount SMALLINT UNSIGNED NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (player_id) REFERENCES players(player_id) ON UPDATE CASCADE,
FOREIGN KEY (consumable_id) REFERENCES consumables(consumable_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE stats_players (
stat_player_id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
player_id INT UNSIGNED NOT NULL,
stat_id TINYINT UNSIGNED  NOT NULL ,
value SMALLINT UNSIGNED NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (player_id) REFERENCES players(player_id) ON UPDATE CASCADE,
FOREIGN KEY (stat_id) REFERENCES stats(stat_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;



CREATE TABLE attacks (
attack_id smallint unsigned PRIMARY KEY AUTO_INCREMENT UNIQUE,
name varchar(30) UNIQUE,
description varchar(150),
value smallint unsigned
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE players_attacks (
player_attack_id int unsigned PRIMARY KEY AUTO_INCREMENT UNIQUE,
player_id int unsigned not null,
attack_id smallint unsigned not null,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (attack_id) REFERENCES attacks(attack_id)ON UPDATE CASCADE,
FOREIGN KEY (player_id) REFERENCES players(player_id) ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE battles(
battle_id int unsigned PRIMARY KEY AUTO_INCREMENT UNIQUE,
game_session_id  INT UNSIGNED NOT NULL,
enemy VARCHAR(40) NOT NULL,
total_damage_made INT UNSIGNED DEFAULT 0 NOT NULL,
total_damage_received INT UNSIGNED DEFAULT 0 NOT NULL,
coin_received INT UNSIGNED DEFAULT 0 NOT NULL,
battle_result BIT,
attacks_missed INT UNSIGNED DEFAULT 0 NOT NULL,
critical_attacks INT UNSIGNED DEFAULT 0 NOT NULL,
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
FOREIGN KEY (game_session_id) REFERENCES game_sessions(game_session_id)ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE battle_consumable(
batle_consumable_id int unsigned PRIMARY KEY AUTO_INCREMENT UNIQUE,
battle_id INT UNSIGNED NOT NULL,
consumable_id SMALLINT UNSIGNED NOT NULL,
consumable_taken SMALLINT UNSIGNED NOT NULL, 
FOREIGN KEY (battle_id) REFERENCES battles(battle_id)ON UPDATE CASCADE,
FOREIGN KEY (consumable_id) REFERENCES consumables(consumable_id)ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE battle_attack(
batle_attack_id int unsigned PRIMARY KEY AUTO_INCREMENT UNIQUE,
battle_id INT UNSIGNED NOT NULL,
attack_id SMALLINT UNSIGNED NOT NULL,
times_used SMALLINT UNSIGNED NOT NULL, 
FOREIGN KEY (battle_id) REFERENCES battles(battle_id)ON UPDATE CASCADE,
FOREIGN KEY (attack_id) REFERENCES attacks(attack_id)ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
