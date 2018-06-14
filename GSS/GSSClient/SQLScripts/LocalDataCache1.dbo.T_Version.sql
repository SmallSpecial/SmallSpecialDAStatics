/****
此 SQL 脚本由“配置数据同步”对话框生成。
此脚本包含在服务器数据库上创建更改跟踪列、已删除
项表和触发器的语句。这些数据库对象对于同步服务在
客户端和服务器数据库之间进行成功同步是必需的。
有关详细信息，请参阅帮助中的“如何: 配置数据库
服务器进行同步”主题。

****/


IF @@TRANCOUNT > 0
set ANSI_NULLS ON 
set QUOTED_IDENTIFIER ON 

GO
BEGIN TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] 
ADD [LastEditDate] DateTime NULL CONSTRAINT [DF_T_Version_LastEditDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version] 
ADD [CreationDate] DateTime NULL CONSTRAINT [DF_T_Version_CreationDate] DEFAULT (GETUTCDATE()) WITH VALUES
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_Tombstone]')) 
BEGIN 
CREATE TABLE [dbo].[T_Version_Tombstone]( 
    [F_id] SmallInt NOT NULL,
    [DeletionDate] DateTime NULL
) ON [PRIMARY] 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
ALTER TABLE [dbo].[T_Version_Tombstone] ADD CONSTRAINT [PKDEL_T_Version_Tombstone_F_id]
   PRIMARY KEY CLUSTERED
    ([F_id])
    ON [PRIMARY]
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_DeletionTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_DeletionTrigger] 

GO
CREATE TRIGGER [dbo].[T_Version_DeletionTrigger] 
    ON [dbo].[T_Version] 
    AFTER DELETE 
AS 
SET NOCOUNT ON 
UPDATE [dbo].[T_Version_Tombstone] 
    SET [DeletionDate] = GETUTCDATE() 
    FROM deleted 
    WHERE deleted.[F_id] = [dbo].[T_Version_Tombstone].[F_id] 
IF @@ROWCOUNT = 0 
BEGIN 
    INSERT INTO [dbo].[T_Version_Tombstone] 
    ([F_id], DeletionDate)
    SELECT [F_id], GETUTCDATE()
    FROM deleted 
END 

GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_UpdateTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_UpdateTrigger] 

GO
CREATE TRIGGER [dbo].[T_Version_UpdateTrigger] 
    ON [dbo].[T_Version] 
    AFTER UPDATE 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[T_Version] 
    SET [LastEditDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[F_id] = [dbo].[T_Version].[F_id] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;


IF @@TRANCOUNT > 0
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Version_InsertTrigger]') AND type = 'TR') 
   DROP TRIGGER [dbo].[T_Version_InsertTrigger] 

GO
CREATE TRIGGER [dbo].[T_Version_InsertTrigger] 
    ON [dbo].[T_Version] 
    AFTER INSERT 
AS 
BEGIN 
    SET NOCOUNT ON 
    UPDATE [dbo].[T_Version] 
    SET [CreationDate] = GETUTCDATE() 
    FROM inserted 
    WHERE inserted.[F_id] = [dbo].[T_Version].[F_id] 
END;
GO
IF @@ERROR <> 0 
     ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
