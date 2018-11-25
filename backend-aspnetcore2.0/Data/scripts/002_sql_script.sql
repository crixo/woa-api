CREATE TABLE "user" ( 
    [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
    [username] nvarchar(50), 
    [pwd_hash] nvarchar(100),
    [salt] nvarchar(100),
    [created_at_utc] DATETIME DEFAULT CURRENT_TIMESTAMP,
    [updated_at_utc] DATETIME NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS IUX_user_username ON user (username);

CREATE TRIGGER user_after_update AFTER UPDATE ON user
BEGIN
    UPDATE user SET updated_at_utc = datetime('NOW') WHERE ID = NEW.ID;
END;
