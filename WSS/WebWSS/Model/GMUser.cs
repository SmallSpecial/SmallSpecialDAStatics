using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSS.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.ComponentModel;
namespace WebWSS.Model
{
    public class JsonData
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Result { get; set; }
        public int Count { get; set; }
    }
    public  class GMUser
    {
        [ColumnAttribute(Name = "F_UserID")]
        public int UserId { get; set; }
        [ColumnAttribute(Name = "F_UserName")]
        public string UserName { get; set; }
        [ColumnAttribute(Name = "F_IP")]
        public string IP { get; set; }
        [ColumnAttribute(Name = "F_RightLevel")]
        public string RightLevel { get; set; }
    }
    public class GMUserManage:BasePage
    {
        public JsonData QueryGMUser(int zoneID, int start, int end) 
        {
            JsonData json = new JsonData() ;
            DbHelperSQLP db = new DbHelperSQLP(ConnStrDigGameDB);
            SqlParameter[] param=new SqlParameter[]{
                new SqlParameter("@link",DbType.String){Value=string.Format("LKSV_2_GameCoreDB_{0}_{1}",BigZoneID,zoneID)},
                new SqlParameter("@start",DbType.Int32){Value=start},
                new SqlParameter("@end",DbType.Int32){Value=end},
                new SqlParameter("@total",DbType.Int32)
            };
            try
            {
                param[param.Length - 1].Direction = ParameterDirection.Output;
                DataSet ds = db.RunProcedure("SP_QueryGMUserData", param, "GM");
                //转换为对应的实体
                json.Data=(new GMUser()).GatherEntityDataWithMapColumn(ds);
                json.Count = (int)param[param.Length - 1].Value;
                json.Result = true;
                return json;
            }
            catch (Exception ex) 
            {
                json.Result = false;
                json.Message = ex.Message;
                return json;
            }
        }
    }
    public  static class EntityReflector 
    {
        public static Dictionary<string, string> GetEntityMapColumn<T>(this T obj) where T : class
        {//ColumnAttribute 特效匹配的实体与数据库字段关系
            Type type = typeof(T);
            PropertyInfo[] pis= type.GetProperties();
            Dictionary<string, string> mapColumn = new Dictionary<string, string>();
            foreach (PropertyInfo item in pis)
            {
                if (item.GetSetMethod() == null)
                {
                    continue;
                }
                object[] attr= item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (attr == null||attr.Length==0) 
                {
                    continue;
                }
                ColumnAttribute column = attr[0] as ColumnAttribute;
                mapColumn.Add(column.Name, item.Name);
            }
            return mapColumn;
        }
        public static List<T> GatherEntityDataWithMapColumn<T>(this T obj, DataSet ds) where T : class
        {
            DataTable table = ds.Tables[0];
            List<string> columns = new List<string>();
            Dictionary<string, string> entityColumn = obj.GetEntityMapColumn();
            Type t = typeof(T);
            foreach (DataColumn item in table.Columns)
            {
                if (entityColumn.ContainsKey(item.ColumnName))
                {//绑定成功
                    columns.Add(item.ColumnName);
                }
            }
            List<T> rows = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T r = System.Activator.CreateInstance<T>();

                foreach (string item in columns)
                {
                    object objc = row[item];
                    PropertyInfo p = t.GetProperty(entityColumn[item]);
                    if (p.PropertyType.Name == typeof(Nullable<>).Name)
                    {
                        NullableConverter nullableConverter = new NullableConverter(p.PropertyType);//如何获取可空类型属性非空时的数据类型
                        Type nt = nullableConverter.UnderlyingType;
                        p.SetValue(r, Convert.ChangeType(obj, nt), null);//如果字段为可空类型此处转换失败
                        continue;
                    }
                    p.SetValue(r, Convert.ChangeType(objc, p.PropertyType), null);
                }
                rows.Add(r);
            }
            return rows;
        }
    }
}