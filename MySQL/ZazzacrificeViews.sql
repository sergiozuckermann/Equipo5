USE zazzacrifice;

CREATE VIEW sessions_summary AS
SELECT u.user_id as user_id, p.name as player_name, c.name as class_name, p.money FROM users u
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