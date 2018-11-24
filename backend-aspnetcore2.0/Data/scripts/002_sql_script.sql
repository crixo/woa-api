CREATE TABLE "user" ( 
    [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
    [username] nvarchar(50), 
    [pwd_hash] nvarchar(100),
    [created_at_utc] DATETIME DEFAULT CURRENT_TIMESTAMP,
    [updated_at_utc] DATETIME NULL
);

CREATE TRIGGER user_after_update AFTER UPDATE ON user
BEGIN
    UPDATE user SET updated_at_utc = datetime('NOW') WHERE ID = NEW.ID;
END;
