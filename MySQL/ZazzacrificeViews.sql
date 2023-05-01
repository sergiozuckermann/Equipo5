USE zazzacrifice;

CREATE VIEW sessions_summary AS
SELECT u.user_id as user_id, c.name as class_name, p.money FROM users u
INNER JOIN game_sessions gs ON u.user_id = gs.user_id
INNER JOIN players p ON p.game_session_id = gs.game_session_id
INNER JOIN classes c ON c.class_id = p.class_id;

CREATE VIEW class_percentage AS 
SELECT c.name, COUNT(*) * 100.0 / (SELECT COUNT(*) FROM players) AS percentage
FROM players
INNER JOIN classes c
USING (class_id)
GROUP BY class_id;

CREATE VIEW damage_made_vs_received AS
SELECT player_id, total_damage_made, total_damage_received FROM battles
order by player_id, battle_id;

CREATE VIEW enemy_win_rate AS
SELECT enemy, battle_result, COUNT(*) AS count  
FROM battles 
GROUP BY enemy, battle_result order by enemy, battle_result;


CREATE VIEW attack_uses AS
SELECT sum(times_used)as times, attacks.name as attack  FROM battles_attacks
INNER JOIN attacks
USING (attack_id)
group by attack_id;


CREATE VIEW criticals_vs_missed AS
SELECT 
    SUM(IF(battle_result=1, critical_attacks, 0)) as critical_attacks_won,
    SUM(IF(battle_result=1, attacks_missed, 0)) as attacks_missed_won,
    SUM(IF(battle_result=0, critical_attacks, 0)) as critical_attacks_lost,
    SUM(IF(battle_result=0, attacks_missed, 0)) as attacks_missed_lost
FROM battles

