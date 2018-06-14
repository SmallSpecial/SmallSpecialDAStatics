/****
警告
  为了防止任何潜在的数据丢失问题，您应该在运行脚本之前
对其进行详细检查。

此 SQL 脚本是由“配置数据同步”对话框
生成的。此脚本补充了可用于创建跟踪更改所需的
必要数据库对象的脚本。此脚本
包含用于移除此类更改的语句。

有关更多信息，请参见帮助中的“如何: 配置数据库服务器进行同步”。
****/


IF @@TRANCOUNT > 0
set ANSI_NULLS ON 
set QUOTED_IDENTIFIER ON 

GO
BEGIN TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] DROP CONSTRAINT [DF_T_Version_LastEditDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] DROP COLUMN [LastEditDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] DROP CONSTRAINT [DF_T_Version_CreationDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] DROP COLUMN [CreationDate]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_Tombstone]') and TYPE = N'U') 
   DROP TABLE [dbo].[T_Version_Tombstone];


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_DeletionTrigger] 

GO


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_UpdateTrigger] 

GO


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_InsertTrigger] 

GO
COMMIT TRANSACTION;
