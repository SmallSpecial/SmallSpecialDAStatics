using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GSSModel.Request
{

    #region LoginAward 登录奖励
    /// <summary>
    /// 登录奖励 实体类, edit hexw 2017-9-11 道具id更改为5各道具id及num
    /// </summary>
    [Serializable]
    public class LoginAward
    {//登录奖励
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActiveName { get; set; }
        public int BigZoneID { get; set; }
        public int ZoneID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 道具id1-5及num1-5
        /// </summary>
        public int Item1 { get; set; }
        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public int Item4 { get; set; }
        public int Item5 { get; set; }

        public int ItemNum1 { get; set; }
        public int ItemNum2 { get; set; }
        public int ItemNum3 { get; set; }
        public int ItemNum4 { get; set; }
        public int ItemNum5 { get; set; }

        public int AwardID { get; set; }//界面输入
        /// <summary>
        /// 备注，最大长度100【此处有坑 varchar定义长度为100，但是一个中文/韩文占用两个字节，如果输入不是英文则最大可输入长度为50】
        /// </summary>
        public string Remark { get; set; }
        public string EmailBody { get; set; }//登录奖励先将数据写入到MySQL数据库gspara_db库下playerloadgameawardinfo中，然后在客户端游戏用户在邮件中可以查看登录奖励【实际上登录奖励有显示控制规则，gss只能处理到数据存储】
        public string SendBy { get; set; }
        public int TaskId { get; set; }
        /// <summary>
        /// 金钱
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 蓝钻
        /// </summary>
        public int BlueDiamond { get; set; }
    } 
    #endregion

    #region LoginAwardLogicData 废弃
    /// <summary>
    /// edit hexw 2017-9-11 废弃,都使用 LoginAward
    /// </summary>
    public class LoginAwardLogicData
    {// [该实体类的作用]将奖励数据存储到SQL server中存档，减少内存占用。Serializable序列化的实体将会增加相应的属性字符串【对于要接收的奖励内容请同样在此处添加】
        public string ActiveName { get; set; }
        public int BigZoneID { get; set; }
        public int ZoneID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AwardID { get; set; }
        public string Remark { get; set; }
        public string EmailBody { get; set; }
        public string SendBy { get; set; }
        public int TaskId { get; set; }
        public int Money { get; set; }
        /// <summary>
        /// 蓝钻
        /// </summary>
        public int BlueDiamond { get; set; }
    } 
    #endregion

    [Serializable]
    public class LoginAwardTask
    {
        public LoginAward Award { get; set; }//登录奖励数据
        public Tasks Task { get; set; }
    }
    [Serializable]
    public class ClientData
    {
        /// <summary>
        /// the  window of handler id
        /// </summary>
        public int FormID { get; set; }
        /// <summary>
        /// logic data
        /// </summary>
        public object Data { get; set; }
        public string Message { get; set; }
        public int TaskID { get; set; }
        public bool Success { get; set; }
    }
    [Description("解封")]
    [Serializable]
    public class Unlock
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }
        public int UnLockTarget { get; set; }//用户:1,角色:2
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
    [Description("解封实体类的逻辑数据类")]
    public class UnlockLogic
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }

    }
    [Serializable]
    public class RunTask
    {
        /// <summary>
        /// 存储的是msgCommand枚举成员
        /// </summary>
        public string Command { get; set; }
    }
    public class StatueCode
    {
        public static int Ok = 200;
        public static int ServiceUnavaliable = 503;//服务不可用
    }
}
