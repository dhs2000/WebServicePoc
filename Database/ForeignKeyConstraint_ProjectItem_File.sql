ALTER TABLE [dbo].[ProjectItems]
	ADD CONSTRAINT [ForeignKeyConstraint_ProjectItem_File]
	FOREIGN KEY ([FileId])
	REFERENCES [dbo].[Files] ([Id])
