using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.ComponentModel;
namespace GSS.DBUtility
{
    public static class ModelReflector
    {
        public static Dictionary<string, string> GetEntityMapColumn<T>(this T obj) where T : class
        {//ColumnAttribute 特效匹配的实体与数据库字段关系
            Type type = typeof(T);
            PropertyInfo[] pis = type.GetProperties();
            Dictionary<string, string> mapColumn = new Dictionary<string, string>();
            foreach (PropertyInfo item in pis)
            {
                if (item.GetSetMethod() == null)
                {
                    continue;
                }
                object[] attr = item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (attr == null)
                {
                    continue;
                }
                ColumnAttribute column = attr[0] as ColumnAttribute;
                mapColumn.Add(column.Name, item.Name);
            }
            return mapColumn;
        }
        /// <summary>
        /// Get the model map data by ColumnAttribute set of Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> TableConvertColumnAtrributeObject<T>(this T obj, DataTable table) where T : class 
        {  
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
                        p.SetValue(r, Convert.ChangeType(obj, nt), null);
                        continue;
                    }
                    p.SetValue(r, Convert.ChangeType(objc, p.PropertyType), null);
                }
                rows.Add(r);
            }
            return rows;
        }
        /// <summary>
        /// object convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="source"></param>
        /// <param name="ingnoreUpper">ingnore the field lower or upper</param>
        /// <returns></returns>
        public static R MapObject<T, R>(this T source, bool ingnoreUpper = false)
            where T : class
            where R : class
        {
            R result = System.Activator.CreateInstance<R>();
            List<string> commonProperty = new List<string>();
            Type st = source.GetType();
            PropertyInfo[] sp = st.GetProperties();
            Type rt = result.GetType();
            PropertyInfo[] rp = rt.GetProperties();
            foreach (PropertyInfo item in sp)
            {
                PropertyInfo pi ;
                if (!ingnoreUpper)
                {
                    pi = rp.Where(p => p.Name == item.Name).FirstOrDefault();
                }
                else 
                {
                    pi = rp.Where(p => p.Name.ToLower() == item.Name.ToLower()).FirstOrDefault();
                }
                if (pi == null)
                {
                    continue;
                }
                object obj = item.GetValue(source, null);
                //如果是字符串，需要对空串进行过滤
                if (item.PropertyType.Name == typeof(string).Name && (string.IsNullOrEmpty(obj as string)))
                {
                    continue;
                }
                if (obj == null) { continue; }
                //如果类型不一致需要强制类型转换【如果类型不一致且可空】
                if (pi.PropertyType.Name == typeof(Nullable<>).Name)
                {
                    NullableConverter nullableConverter = new NullableConverter(pi.PropertyType);//如何获取可空类型属性非空时的数据类型
                    Type nt = nullableConverter.UnderlyingType;
                    pi.SetValue(result, Convert.ChangeType(obj, nt), null);
                    continue;

                }
                pi.SetValue(result, Convert.ChangeType(obj, pi.PropertyType), null);
            }
            return result;
        }
    }
}
