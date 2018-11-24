ALTER TABLE paziente ADD COLUMN created_at_utc DATETIME NULL;

ALTER TABLE paziente ADD COLUMN updated_at_utc DATETIME NULL;

CREATE TRIGGER paziente_after_insert AFTER INSERT ON paziente
BEGIN
    UPDATE paziente SET created_at_utc = datetime('NOW') WHERE ID= NEW.ID;
END;

CREATE TRIGGER paziente_after_update AFTER UPDATE ON paziente
BEGIN
    UPDATE paziente SET updated_at_utc = datetime('NOW') WHERE ID= NEW.ID;
END;
