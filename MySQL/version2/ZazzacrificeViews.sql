USE zazzacrifice;

CREATE VIEW sessions_summary AS
SELECT p.name as player_name, c.name as class_name, p.money FROM users u
INNER JOIN game_sessions gs ON u.user_id = gs.user_id
INNER JOIN players p ON p.game_session_id = gs.game_session_id
INNER JOIN classes c ON c.class_id = p.class_id;