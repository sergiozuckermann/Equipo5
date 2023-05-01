USE zazzacrifice;
DELIMITER //
CREATE TRIGGER game_session_insert_trigger AFTER INSERT ON game_sessions
FOR EACH ROW
BEGIN
  INSERT INTO game_events (game_session_id, event_id, is_active)
  SELECT NEW.game_session_id, events.events_id, 1
  FROM events;
END //
DELIMITER ;

DELIMITER //
CREATE TRIGGER insert_default_checkpoint AFTER INSERT ON players
FOR EACH ROW
BEGIN
  INSERT INTO checkpoints (player_id) VALUES (NEW.player_id);
END;
 //
DELIMITER ;