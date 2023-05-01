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
INSERT INTO stats (name) values ('AGL'); #id 3 
INSERT INTO stats (name) values ('LCK') ; #id 4 
INSERT INTO stats (name) values ('CHAR') ; #id 5 
INSERT INTO stats (name) values ('ACC') ; #id 6
INSERT INTO stats (name) values ('Max_HP') ; #id 7
INSERT INTO stats (name) values ('Current_HP') ; #id 8
INSERT INTO stats (name) values ('Max_MP') ; #id 9
INSERT INTO stats (name) values ('Current_MP') ; #id 10

#Tablas Clases 
INSERT INTO classes (name) value ('Ligera');
INSERT INTO classes (name) value ('Mediana');
INSERT INTO classes (name) value ('Pesada');

#Tablas Consumibles
INSERT INTO consumables (name, description, stat_id, value) VALUES 
('DEFENCE ITEM', 'increase 30% of characters defence', 1, 3),
('ATTACK ITEM', 'increase 30% of characters attack', 2, 3),
('AGILITY ITEM', 'increase 30% of characters agility', 3, 3),
('LUCK ITEM', 'increase 30% of characters luck', 4, 3),
('CHARISMA ITEM', 'increase 30% of characters charisma', 5, 3),
('ACCURACY ITEM', 'increase 30% of characters accuracy', 6, 3),
('TOTAL HP ITEM', 'increase 50% of characters total health points', 7, 5),
('TOTAL MP ITEM', 'increase 50% of characters total mana points', 9, 5);



#Tablas Scenes
INSERT INTO scenes (name) VALUES 
('The Forest'),
('The Castle'),
('The Village');


#Tablas Ataques
INSERT INTO attacks (name, description, value) VALUES  
('Melee Attack', 'A swift and precise attack', 4),
('Fire', 'A fiery spell that deals continious damage', 2),
('Lightning', 'A powerful bolt of lightning that doubles all stats', 8),
('Ice', 'A icy spell that freeces the enemys attack turn', 3),
('Heal', 'A restorative spell to heal damage', 5),
('Recharg', 'A restorative spell to recharge mana points', 5 );



#Tablas Game session 
INSERT INTO game_sessions (user_id, time_on_seconds, finished)
VALUES
(1, 3600, 1),
(2, 1800, 0),
(3, 5400, 1),
(4, 7200, 1),
(5, 7200,0),
(2, 7200, 0),
(2, 7200, 0),
(3, 7200, 0),
(5, 9000, 1);


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



#Tabla players
INSERT INTO players (game_session_id, name, money, checkpoint_id, class_id)
VALUES 
  (1, 'Alice', 1000, 1),
  (2, 'Bob', 500, 1),
  (3, 'Charlie', 200, 2),
  (4, 'Dave', 800, 2),
  (5, 'Eve', 300, 3),
  (6, 'Frank', 600, 3),
  (7, 'Grace', 400, 1),
  (8, 'Henry', 900, 2),
  (9, 'Isaac', 700, 3);
  
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



#Tabla Players attack 
INSERT INTO players_attacks (player_id, attack_id) VALUES
(1, 1),
(1, 4),
(1, 6),
(2, 1),
(2, 3),
(2, 5),
(2, 6),
(3, 1),
(3, 6),
(3, 3),
(4, 2),
(4, 6),
(4, 1),
(5, 3),
(5, 2),
(5, 6),
(5, 4),
(6, 4),
(6, 6),
(6, 2),
(7, 5),
(7, 3),
(7, 2),
(8, 1),
(8, 3),
(8, 2),
(8, 4),
(9, 2),
(9, 5),
(9, 1),
(9, 3);

#Tablas Battle 
INSERT INTO battles (player_id, enemy, total_damage_made, total_damage_received, coin_received, battle_result, attacks_missed, critical_attacks) VALUES
(1, 'Goblin', 120, 60, 15, 1, 10, 40),
(1, 'Orc', 200, 80, 25, 1, 2, 6),
(1, 'Troll', 180, 90, 20, 0, 3, 1),
(1, 'Dragon', 500, 250, 100, 0, 10, 8),
(2, 'Goblin', 80, 40, 10, 1, 2, 1),
(2, 'Orc', 250, 120, 35, 1, 5, 4),
(2, 'Troll', 150, 70, 15, 0, 4, 2),
(3, 'Goblin', 60, 30, 8, 1, 1, 1),
(3, 'Orc', 180, 90, 20, 1, 3, 3),
(3, 'Troll', 200, 100, 25, 0, 6, 2),
(3, 'Dragon', 600, 300, 120, 0, 8, 10),
(4, 'Goblin', 40, 20, 5, 1, 3, 0),
(4, 'Orc', 150, 70, 18, 1, 4, 2),
(5, 'Goblin', 200, 100, 25, 1, 0, 5),
(5, 'Orc', 80, 40, 10, 0, 2, 1),
(5, 'Troll', 300, 150, 50, 1, 4, 5),
(6, 'Goblin', 100, 50, 12, 1, 1, 3),
(6, 'Orc', 300, 150, 45, 1, 3, 6),
(6, 'Troll', 250, 120, 30, 0, 5, 3),
(6, 'Dragon', 700, 350, 150, 0, 12, 12),
(7, 'Goblin', 120, 60, 15, 0, 4, 2),
(7, 'Orc', 100, 50, 12, 1, 2, 2),
(7, 'Troll', 180, 90, 22, 1, 3, 1),
(7, 'Dragon', 800, 400, 200, 0, 15, 15),
(8, 'Goblin', 50, 25, 6, 1, 2, 0),
(8, 'Orc', 200, 100, 28, 1, 5, 3),
(8, 'Troll', 150, 70, 18, 0, 4, 2),
(9, 'Goblin', 70, 35, 9, 0, 1, 1),
(9, 'Orc', 150, 150, 40, 1, 6, 2),
(9, 'Goblin', 190, 50, 15, 0, 2, 0),
(9, 'Troll', 250, 250, 70, 1, 12, 5),
(9, 'Skeleton', 350, 100, 30, 1, 4, 1),
(9, 'Orc', 420, 200, 60, 1, 7, 2);

#Tabla battle_consumables
INSERT INTO battles_consumables (battle_id, consumable_id, consumable_taken) VALUES
(1, 3, 5),
(1, 5, 7),
(1, 1, 3),
(2, 2, 9),
(2, 6, 2),
(2, 4, 5),
(3, 1, 10),
(3, 7, 8),
(4, 5, 4),
(4, 4, 6),
(4, 2, 2),
(4, 3, 3),
(5, 6, 10),
(5, 7, 6),
(6, 1, 8),
(6, 4, 4),
(6, 5, 7),
(6, 6, 1),
(6, 2, 3),
(7, 3, 7),
(7, 7, 4),
(8, 1, 5),
(8, 6, 6),
(8, 3, 8),
(9, 2, 9),
(9, 5, 5),
(10, 4, 2),
(10, 1, 1),
(10, 7, 10);

#Tabla Battles_attack
INSERT INTO battles_attacks (battle_id, attack_id, times_used) VALUES
(1, 1, 3),
(1, 2, 2),
(1, 3, 1),
(2, 2, 4),
(2, 4, 1),
(3, 1, 2),
(3, 5, 3),
(4, 3, 5),
(4, 6, 2),
(5, 1, 1),
(5, 4, 4),
(5, 5, 3);