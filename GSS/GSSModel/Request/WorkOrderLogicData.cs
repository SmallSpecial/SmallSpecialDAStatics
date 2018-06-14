using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSModel
{
    public  class WorkOrderLogicData
    {
        public Guid Id { get; set; }
        public int TaskId { get; set; }
        /// <summary>
        /// 工单的业务数据【最大长度为8000】
        /// </summary>
        public string LogicData { get; set; }
        public bool IsDelete { get; set; }
    }
    public class SampleWorkOrder 
    {

        public string LogicData { get; set; }//工单要实现的功能相关数据
        public string ZoneName { get; set; }
        public string BigZoneName { get; set; }
        /// <summary>
        /// 工单创建人
        /// </summary>
        public int CreateBy { get; set; }
        public int AppId { get; set; }//app 名称ID【用途不明】
        public int TypeId { get; set; }//工单类型ID
        public string Title { get; set; }
        public string Note { get; set; }

    }
}
