using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSModel.Request
{
    [Serializable]
    public class RoleData
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int BigZoneId { get; set; }
        public int? ZoneId { get; set; }
    }
}
