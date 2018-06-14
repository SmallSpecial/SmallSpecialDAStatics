using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace GSSModel.Response
{
    /// <summary>
    ///游戏配置
    /// </summary>
    public class GameConfig
    {
        [ColumnAttribute(Name = "F_ID")]
        public int Id { get; set; }
        [ColumnAttribute(Name = "F_Name")]
        public string Name { get; set; }
        [ColumnAttribute(Name = "F_Value")]
        public string Value { get; set; }
        [ColumnAttribute(Name = "F_ParentID")]
        public int ParentId { get; set; }
        [ColumnAttribute(Name = "F_ValueGame")]
        public string GameValue { get; set; }
        [ColumnAttribute(Name="F_IsUsed")]
        public int IsUsed { get; set; }
    }
}
