USE zazzacrifice;

#Tablas Users 
INSERT INTO users (username, password) VALUE ('Ian', 'zazza');
INSERT INTO users (username, password) VALUE ('Fran', 'shaggy');
INSERT INTO users (username, password) VALUE ('Sunday', 'berries');
INSERT INTO users (username, password) VALUE ('Tuch', 'Master');
INSERT INTO users (username, password) VALUE ('Tena', 'calistena');




#Tablas estadisticas 
INSERT INTO stats (name) values ('DEF'); #id 1 
INSERT INTO stats (name) values ('ATK') ; #id 2 
INSERT INTO stats (name) values ('MP') ; #id 3 
INSERT INTO stats (name) values ('AGL'); #id 4 
INSERT INTO stats (name) values ('LCK') ; #id 5 
INSERT INTO stats (name) values ('CHAR') ; #id 6 
INSERT INTO stats (name) values ('HP') ; #id 7 

#Tablas Clases 
INSERT INTO classes (name) value ('Ligera');
INSERT INTO classes (name) value ('Mediana');
INSERT INTO classes (name) value ('Pesada');

#Tablas Consumibles
INSERT INTO consumable (name, description, stat_id, value, time_in_second) VALUES 
('Health Potion', 'Restores 50 HP', 1, 20, 5),
('Mana Potion', 'Restores 50 MP', 2, 20, 5),
('Elixir of Strength', 'Increases strength by 10%', 3, 30, 10),
('Elixir of Defense', 'Increases defense by 10%', 4, 30, 10),
('Elixir of Agility', 'Increases agility by 10%', 5, 30, 10),
('Elixir of Intelligence', 'Increases intelligence by 10%', 6, 30, 10);

#Tablas Armas
INSERT INTO weapons (name, description, stat_id, value, class_id) VALUES 
('Sword', 'A sharp, two-handed blade', 1, 50, 1),
('Dagger', 'A small, fast weapon for close combat', 2, 30, 1),
('Bow', 'A ranged weapon for precise attacks', 3, 70, 2),
('Staff', 'A magical weapon used to cast spells', 4, 80, 3),
('Mace', 'A heavy blunt weapon for crushing armor', 5, 60, 1),
('Crossbow', 'A powerful ranged weapon for taking down enemies', 6, 90, 2);

#Tablas Scenes
INSERT INTO scenes (name) VALUES 
('The Forest'),
('The Castle'),
('The Village');

#Tablas Elementos
INSERT INTO elements (name, weakness) VALUES 
('Earth', NULL),
('Thunder', 1),
('Fire', 2),
('Water', 3),
('Ice', 4);

#Tablas Ataques
INSERT INTO attacks (name, description, value) VALUES 
('Quick Strike', 'A swift and precise attack', 10),
('Charge', 'A powerful charge attack', 20),
('Fireball', 'A fiery projectile attack', 15),
('Heal', 'A restorative spell to heal damage', 5),
('Poison Sting', 'A poisonous attack that weakens enemies over time', 12),
('Thunderbolt', 'A powerful bolt of lightning that strikes enemies', 18),
('Ice Shard', 'A sharp shard of ice that deals damage and slows enemies', 13),
('Flamethrower', 'A continuous stream of flames that damages enemies', 16),
('Wind Blast', 'A blast of wind that knocks enemies back and deals damage', 14),
('Earthquake', 'A powerful quake that damages enemies and shakes the ground', 20),
('Sword Slash', 'A strong and precise slash attack', 12),
('Magic Missile', 'A guided missile of magical energy that deals damage', 15),
('Lifesteal', 'An attack that drains the enemy\'s life force and heals the user', 10),
('Whirlwind', 'A powerful whirlwind that damages and knocks back enemies', 17),
('Shadow Strike', 'A sneaky attack that deals extra damage from behind', 13),
('Axe Chop', 'A powerful and heavy axe swing attack', 19),
('Charge Up', 'A charging attack that increases damage on the next attack', 5);

#Tablas Game session 
INSERT INTO game_sessions (user_id, time_on_seconds, number_of_battles, number_of_damaged_made, elements_obtained, finished)
VALUES
(1, 3600, 10, 5, 20, 1),
(2, 1800, 5, 2, 2, 0),
(3, 5400, 15, 8, 5, 1),
(4, 7200, 20, 10, 4, 1),
(5, 7200, 20, 10, 3, 0),
(2, 7200, 20, 10, 1, 0),
(2, 7200, 20, 10, 1, 0),
(3, 7200, 20, 10, 1, 0),
(5, 9000, 25, 12, 4, 1);


INSERT INTO events (name) VALUES 
('Nights at shaggies'),
('Stroyteller NPC'), 
('Final Boss');

#Tabla game events
INSERT INTO game_events (event_id, game_session_id, is_active) VALUES (1, 1, 0),
(2, 1, 1),
(3, 2, 0),
(1, 3, 1),
(2, 3, 0),
(3, 4, 1),
(1, 5, 0),
(2, 5, 1),
(3, 5,1);

#Tabla checkpoint 
INSERT INTO checkpoints (scene_id, x_postion, y_position) 
VALUES 
  (1, 100, 200),
  (2, 50, 300),
  (3, 200, 150),
  (1, 300, 50),
  (2, 150, 250),
  (3, 75, 175),
  (1, 200, 300),
  (2, 100, 100),
  (3, 250, 200);


#Tabla players
INSERT INTO players (game_session_id, name, money, checkpoint_id, class_id)
VALUES 
  (1, 'Alice', 1000, 1, 1),
  (2, 'Bob', 500, 2, 1),
  (3, 'Charlie', 200, 3, 2),
  (4, 'Dave', 800, 4, 2),
  (5, 'Eve', 300, 5, 3),
  (6, 'Frank', 600, 6, 3),
  (7, 'Grace', 400, 7, 1),
  (8, 'Henry', 900, 8, 2),
  (9, 'Isaac', 700, 9, 3);



#Tabla consumibles player
INSERT INTO consumables_players (player_id, consumable_id, amount)
VALUES 
    (1, 2, 2),
    (1, 3, 1),
    (1, 5, 4),
    (2, 1, 1),
    (2, 4, 3),
    (2, 6, 2),
    (3, 2, 3),
    (3, 4, 2),
    (4, 1, 2),
    (4, 3, 4),
    (4, 5, 1),
    (5, 2, 1),
    (5, 3, 3),
    (5, 6, 2),
    (7, 2, 2),
    (7, 3, 1),
    (8, 1, 3),
    (8, 5, 4),
    (9, 2, 2);

#Tabla weapons players
INSERT INTO weapons_players (weapon_id, player_id, equipped)
VALUES
(1, 1, 1),
(2, 1, 1),
(3, 2, 1),
(4, 2, 1),
(5, 3, 1),
(6, 4, 1),
(1, 5, 1),
(2, 6, 1),
(3, 7, 1),
(4, 8, 1),
(5, 9, 1);


#Tabla stats players
INSERT INTO stats_players (player_id, stat_id, value) VALUES
(1, 1, 75), (1, 2, 149), (1, 3, 43), (1, 4, 112), (1, 5, 198), (1, 6, 7), (1, 7, 32),
(2, 1, 85), (2, 2, 23), (2, 3, 167), (2, 4, 56), (2, 5, 92), (2, 6, 132), (2, 7, 18),
(3, 1, 129), (3, 2, 76), (3, 3, 89), (3, 4, 1), (3, 5, 52), (3, 6, 187), (3, 7, 150),
(4, 1, 26), (4, 2, 82), (4, 3, 135), (4, 4, 116), (4, 5, 67), (4, 6, 40), (4, 7, 97),
(5, 1, 121), (5, 2, 165), (5, 3, 19), (5, 4, 53), (5, 5, 8), (5, 6, 162), (5, 7, 142),
(6, 1, 52), (6, 2, 29), (6, 3, 86), (6, 4, 44), (6, 5, 127), (6, 6, 183), (6, 7, 91),
(7, 1, 168), (7, 2, 38), (7, 3, 117), (7, 4, 9), (7, 5, 50), (7, 6, 3), (7, 7, 165),
(8, 1, 74), (8, 2, 150), (8, 3, 112), (8, 4, 43), (8, 5, 198), (8, 6, 7), (8, 7, 31),
(9, 1, 142), (9, 2, 19), (9, 3, 165), (9, 4, 87), (9, 5, 123), (9, 6, 37), (9, 7, 193);

#Tablas elements_players
INSERT INTO elements_players (element_id, player_id) VALUES 
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (1, 6), 
(2, 7), (3, 8), (4, 9), (5, 1), (1, 2), (2, 3), 
(3, 4), (4, 5), (5, 6), (1, 7), (2, 8), (3, 9), 
(4, 1), (5, 2);

#Tabla Players attack 
INSERT INTO players_attacks (player_id, attack_id) VALUES
(1, 2),
(1, 4),
(1, 6),
(2, 1),
(2, 3),
(2, 5),
(2, 7),
(3, 1),
(3, 8),
(3, 9),
(4, 2),
(4, 6),
(4, 10),
(5, 3),
(5, 11),
(5, 12),
(5, 14),
(6, 4),
(6, 7),
(6, 15),
(7, 5),
(7, 8),
(7, 16),
(8, 1),
(8, 9),
(8, 12),
(8, 17),
(9, 2),
(9, 5),
(9, 10),
(9, 13);