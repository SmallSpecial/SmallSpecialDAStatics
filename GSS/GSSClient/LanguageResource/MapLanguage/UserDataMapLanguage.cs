using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using GSSClient.LanguageResource;
namespace GSSClient
{
    /*
     此处枚举的内容说明:用于一些特殊的信息匹配资源文件内的文本内容
     * 
     */
    public enum MapLanguageConfig
    {
        //[Description("发奖用户")]
        Map_AwardUserSheetName = 0,
        //[Description("用户编号")]
        Map_UserNo = 2,
        //[Description("角色编号")]
        Map_RoleNo = 3,
        //[Description("战区编号")]
        Map_ZoneNo = 4
    }
    public class MapLanguageManage 
    {
        public static string GetString(string resxItem) 
        {
            return Language.ResourceManager.GetString(resxItem);
        }
        public static string GetStringByMapLanguageConfig(MapLanguageConfig config)
        {
            return Language.ResourceManager.GetString(config.ToString());
        }
    }
}
