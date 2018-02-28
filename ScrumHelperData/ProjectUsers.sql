CREATE TABLE [dbo].[ProjectUsers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ProjectID] INT NOT NULL,
	[UserID] INT NOT NULL, 
	CONSTRAINT [FK_dbo.ProjectUser_dbo.ProjectUser_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ProjectUser_dbo.ProjectUser_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
)
