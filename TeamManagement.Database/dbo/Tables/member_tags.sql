CREATE TABLE [dbo].[member_tags]
(
	[member_id] INT,
    [tag_id] INT,
    CONSTRAINT PK_member_tags PRIMARY KEY CLUSTERED ([member_id], [tag_id]),
    CONSTRAINT FK_member_tags_members FOREIGN KEY ([member_id]) REFERENCES members([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_member_tags_tags FOREIGN KEY ([tag_id]) REFERENCES tags([id]) ON DELETE CASCADE ON UPDATE CASCADE
)
