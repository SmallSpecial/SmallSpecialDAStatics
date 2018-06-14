using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace WebWSS.Model
{
    public class BaseVocate
    {//职业角色信息
        public short BigZoneId { get; set; }
        public short ZoneId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class UserMountResponse : BaseVocate
    {//每日激活坐骑统计
        public DateTime Day { get; set; }
        public int DayInt { get; set; }
        public List<string> ActiveMountIds = new List<string>();//激活的坐骑ID列表
    }
    public class OtherLog 
    {
        [ColumnAttribute(Name = "Userid")]
        public int userid { get; set; }
        [ColumnAttribute(Name = "roleid")]
        public int roleid { get; set; }
        [ColumnAttribute(Name = "Opid")]
        public int opid { get; set; }
        [ColumnAttribute(Name = "param1")]
        public int param1 { get; set; }
        [ColumnAttribute(Name = "param2")]
        public int param2 { get; set; }
        [ColumnAttribute(Name = "opBak")]
        public string opbak { get; set; }
        [ColumnAttribute(Name = "OpTime")]
        public DateTime optime { get; set; }
    }
    public class UserMountLevel :SampleMountAttribute
    {
        public int userid { get; set; }
        public int roleid { get; set; }
        public string opbak { get; set; }
       
        public DateTime optime { get; set; }
    }
    public class SampleMountAttribute 
    {
        public int level { get; set; }
        public string mountId { get; set; }//坐骑ID（这个值对应坐骑类别ID）
    }
    public class UserMountLevelStatisc : SampleMountAttribute
    {
        public int mountNumber { get; set; }
    }
    public class UserLog
    {
        [Column(Name = "UserId")]
        public int Userid { get; set; }
        [Column(Name = "RoleId")]
        public int roleid { get; set; }
        [Column(Name = "RoleLevel")]
        public int Level { get; set; }
        [Column(Name = "OpBak")]//此标志中含有好友动作（参数分割符_）第一项参数1 新增，2 删除
        public string OpBak { get; set; }
        public int ActionType { get; set; }
        [Column(Name = "OpTime")]
        public DateTime OpTime { get; set; }
    }
    public class FriendLog :UserLog
    {
    
    }
    public class Ememy : FriendLog 
    {
    
    }
}