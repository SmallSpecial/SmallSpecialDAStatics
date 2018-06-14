using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace GSSClient
{
    /// <summary>
    /// 工单状态枚举
    /// </summary>
    public enum WorkOrderStatueEnum
    {
        Prepare=100100100, //准备工单池
        NewReceive = 100100101,//新收工单池
        Dealwithing = 100100102,//处理中
        TurnOut = 100100103,//转出工单池
        WaitFeedbook = 100100104,//待反馈工单池
        WaitScore = 100100105,//待评分工单池
        LeaderAudit = 100100106,//领导审核工单池
        History = 100100107,//历史工单池
        Recovery = 100100108,//回收
    }
    public class QueryLanguageResource
    {
        public static string GetWorkorderStatueDesc(ResourceManager rm, WorkOrderStatueEnum enumField)
        {
            string fix = typeof(WorkOrderStatueEnum).Name;
            return rm.GetString(fix + "_" + enumField);
        }
    }
}
