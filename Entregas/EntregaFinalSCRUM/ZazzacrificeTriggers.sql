DELIMITER //
CREATE TRIGGER insert_default_checkpoint AFTER INSERT ON players
FOR EACH ROW
BEGIN
  INSERT INTO checkpoints (player_id) VALUES (NEW.player_id);
END;
 //
DELIMITER ;

DELIMITER //
CREATE TRIGGER insert_default_attacks AFTER INSERT ON players
FOR EACH ROW
BEGIN
  INSERT INTO players_attacks (player_id, attack_id) 
	VALUES 
		(NEW.player_id, 1),
    (NEW.player_id, 5),
    (NEW.player_id, 6)
						;
END;
 //
DELIMITER ;
