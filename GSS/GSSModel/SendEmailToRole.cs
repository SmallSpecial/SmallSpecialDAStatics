using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSModel
{
    [Serializable]
    public class SendEmailToRole
    {
        public int BigZoneId { get; set; }
        public int ZoneId { get; set; }
        public int EmailId { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public int EquipId { get; set; }
        public int EquipNum { get; set; }
        /// <summary>
        /// 邮件接收的角色ID列表，使用","分割
        /// </summary>
        public string ReceiveRoles { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
