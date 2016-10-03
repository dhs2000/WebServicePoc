ALTER TABLE [dbo].[ProjectItems]
	ADD CONSTRAINT [ForeignKeyConstraint_ProjectItem_ProjectId]
	FOREIGN KEY ([ProjectId])
	REFERENCES [dbo].[Projects] ([Id])
