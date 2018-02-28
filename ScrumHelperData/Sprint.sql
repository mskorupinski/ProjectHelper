CREATE TABLE [dbo].[Sprint]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DateTime NOT NULL,
	[Duration] INT NOT NULL,
	[ProjectID] INT NOT NULL,
	CONSTRAINT [FK_dbo.Sprint_dbo.Sprint_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE
)
