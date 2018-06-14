using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSCSFrameWork
{

    /// <summary>
    /// 消息类，标记为可序列化
    /// </summary>
    [Serializable]
    public class MsgStruts
    {
        public msgCommand command = msgCommand.None;       //命令枚举
        public msgType msgtype = msgType.None;             //消息类型
        public msgSendState msgsendstate = msgSendState.None;
        static MsgParam msgparam = new MsgParam();
        public MsgParam MsgParam = msgparam;
        public byte[] Data;                                //消息内容
    }

    [Serializable]
    public class MsgParam
    {
        public string p0 { get; set; }
        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public string p4 { get; set; }
        public string p5 { get; set; }
        public string p6 { get; set; }
    }

    /// <summary>
    /// 消息命令类型
    /// </summary>
    public enum msgCommand
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        /// <summary>
        /// 退出
        /// </summary>
        Exit,
        /// <summary>
        /// 更改密码
        /// </summary>
        ChangePwd,
        /// <summary>
        /// 登录
        /// </summary>
        GetLogin,
        /// <summary>
        /// 得到数据库在客户端的缓存,如字典表之类.
        /// </summary>
        GetCache,
        /// <summary>
        /// 得到提醒数量用的DATASET
        /// </summary>
        GetAlertNum,
        /// <summary>
        /// 得到所有工单列表
        /// </summary>
        GetAllTasks,
        /// <summary>
        /// 得到工单历史列表
        /// </summary>
        GetTaskLog,
        /// <summary>
        /// 获取游戏用户列表,客服主页用
        /// </summary>
        GetGameUsersC,
        /// <summary>
        /// 获取游戏角色列表,客服主页用
        /// </summary>
        GetGameRolesC,
        /// <summary>
        /// 获取游戏角色列表,客服主页用
        /// </summary>
        GetGameRolesCR,
        /// <summary>
        /// 添加工单
        /// </summary>
        AddTask,
        /// <summary>
        /// 编辑工单
        /// </summary>
        EditTask,
        /// <summary>
        /// 编辑工单历史
        /// </summary>
        EditTaskLog,
        /// <summary>
        /// 编辑工单(不需要服务器回应)
        /// </summary>
        EditTaskNoReturn,
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        ExcSql,
        /// <summary>
        /// 执行存储过程
        /// </summary>
        ExcPro,
        /// <summary>
        /// 游戏工具:封停帐户或角色
        /// </summary>
        GameLockUR,
        /// <summary>
        /// 游戏工具:解封帐户或角色
        /// </summary>
        GameNoLockUR,
        /// <summary>
        /// 游戏工具:借用帐号
        /// </summary>
        GameUserUse,
        /// <summary>
        /// 游戏工具:归还帐号
        /// </summary>
        GameUserNoUse,
        /// <summary>
        /// 游戏工具:清空防沉迷
        /// </summary>
        GameResetChildInfo,
        /// <summary>
        /// 游戏工具:开始公告
        /// </summary>
        GameNoticeStart,
        /// <summary>
        /// 游戏工具:停止公告
        /// </summary>
        GameNoticeStop,
        /// <summary>
        /// 删除全服邮件
        /// </summary>
        DeleteFullServiceEmail,
        /// <summary>
        /// 游戏工具:执行发奖
        /// </summary>
        GameGiftAwardDo,
        /// <summary>
        /// 游戏工具:离线查询GS日志
        /// </summary>
        QuerySynGSLog,
        /// <summary>
        /// 游戏工具:实时查询GS日志
        /// </summary>
        QueryLiveGSLog,
        /// <summary>
        /// 下载模本文件
        /// </summary>
        DownloadTemplateFile,
        /// <summary>
        /// 登录奖励数据录入
        /// </summary>
        AddLoginAward,
        /// <summary>
        /// 全服邮件
        /// </summary>
        AddFullServiceEmail,
        /// <summary>
        /// 已删除的角色恢复
        /// </summary>
        GameRoleRecovery,
        /// <summary>
        /// 批量角色邮件发送
        /// </summary>
        SendEmailToRoles=1111,
        /// <summary>
        /// 游戏物品掉落
        /// </summary>
        ActiveFallGoods=1112,
        /// <summary>
        /// 向服务器提交待创建的工单并包含逻辑数据
        /// </summary>
        CreateTaskContainerLogic=11131
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum msgType
    {
        /// <summary>
        ///空消息
        /// </summary>
        None,

        /// <summary>
        ///文本消息
        /// </summary>
        SendText,
        /// <summary>
        ///发送文件
        /// </summary>
        SendFile,
        /// <summary>
        ///发送DATASET
        /// </summary>
        SendDataset
    }
    /// <summary>
    /// 消息发送状态(用于消息循环接收的开始和技术标识)
    /// </summary>
    public enum msgSendState
    {
        None,   //空
        single, //单条消息，一次性接收
        start,  //消息开始
        sending,//消息发送中
        end     //消息结束
    }

}
