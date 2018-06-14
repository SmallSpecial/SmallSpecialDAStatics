using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using WebWSS.Model;
namespace WebWSS.Stats
{
    public partial class StaticTemplate :BasePage
    {
        public class PMHResponse 
        {
            public int Top { get; set; }
            public int Money { get; set; }
            public string User { get; set; }
            public string Role { get; set; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static JsonData GetAjaxData(string tag, string time, string otherParam, int start,int end)
        {
            JsonData json = new JsonData() { Result=true};
            DateTime date = DateTime.Parse(time);
            GridCategory grid;
            Enum.TryParse(tag, out grid);
            int zoneID = 0;
            string[] param = otherParam.Split('&');//提取战区ID
            foreach (string item in param)
            {
                string[] paramValue = item.Split('=');
                if (paramValue[0] == "zoneid")
                {
                    int.TryParse(paramValue[1], out zoneID);
                    break;
                }
            }
            if (!string.IsNullOrEmpty(otherParam))
            { 
                otherParam=otherParam.Trim();
            }
            RequestParam rp = new RequestParam()
            {
                Begin = start,
                End = end,
                BigZoneId = bigZoneId,
                ZoneId = zoneID,
                Date = int.Parse(date.ToString("yyyyMMdd"))
            };
            switch (grid)
            {
                case GridCategory.AuctionRoom:
                    List<PMHResponse> lis = new List<PMHResponse>();
                    for (int i = 0; i < 20; i++)
                    {
                        PMHResponse r = new PMHResponse() 
                        {
                            Top=i+1,
                            Money=(int)(i*5.6),
                            User="zs"+i,
                            Role="role"+i
                        };
                        lis.Add(r);
                    }
                    json.Data = lis;
                    json.Count = lis.Count;
                    break;
                case GridCategory.GMUser:
                    Model.GMUserManage gm = new Model.GMUserManage();
                    json = gm.QueryGMUser(zoneID, start, end);
                    break;
                case GridCategory.UserEmailData:
                    UserEmailDataManage udm = new UserEmailDataManage(DigDbConnString);
                    json = udm.QueryData(new UserEmailRequestParam() { Begin = start, End = end, BigZoneId = bigZoneId, ZoneId = zoneID });
                    if (string.IsNullOrEmpty(json.Message))
                    {
                        json.Result = true;
                    }
                    break;
                case GridCategory.UserActiveMountStatics:
                    UserRelatedDataStatis relate = new UserRelatedDataStatis(DigDbConnString);
                    json= relate.TheDayActiveMount(new RequestParam() {
                        Begin = start,
                        End = end,
                        BigZoneId = bigZoneId,
                        ZoneId = zoneID,
                        Date = int.Parse(date.ToString("yyyyMMdd"))
                    });
                    break;
                case GridCategory.UserMountLevelStatic:
                    relate = new UserRelatedDataStatis(DigDbConnString);
                    json= relate.UserMountLevelStatic(new RequestParam() {
                        Begin = start,
                        End = end,
                        BigZoneId = bigZoneId,
                        ZoneId = zoneID,
                        Date = int.Parse(date.ToString("yyyyMMdd"))
                    });
                    break;
                case GridCategory.UserAddFriendLog:
                    relate = new UserRelatedDataStatis(DigDbConnString);
                    json = relate.GetUserFriendLogs(rp);
                    break;
                case GridCategory.Ememy:
                    relate = new UserRelatedDataStatis(DigDbConnString);
                    json= relate.GetEmemy(rp);
                    break;
                default:
                    break;
            }
            return json;
        }
        [WebMethod]
        public static JsonData GetAjaxCache() 
        {
            JsonData json = new JsonData() { Result=true};
            Dictionary<string, string> zone = AddGameZoneCache();//不能再后台补充全选项，static修改将同步到数据来源中。。。。
            Dictionary<string, object> cache = new Dictionary<string, object>();
            cache.Add(CacheItem.GameZone.ToString(), zone);
            json.Data = cache;
            json.Count = cache.Count;
            return json;
        }
    }
    public enum GridCategory 
    {
        AuctionRoom=1,
        GMUser=2,//授权GM的用户列表
        UserEmailData=3,
        UserActiveMountStatics = 4 ,//当天用户激活坐骑统计
        UserMountLevelStatic=5,//坐骑进阶变化
        UserAddFriendLog=6,
        /// <summary>
        /// 黑名单
        /// </summary>
        Ememy=7 //黑名单
    }
    public enum CacheItem 
    {
        GameZone=1
    }
}