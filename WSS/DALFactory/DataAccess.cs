using System;
using System.Reflection;
using System.Configuration;
namespace WSS.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="WSS.SQLServerDAL" />。
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
		public DataAccess()
		{ }

        #region CreateObject 

		//不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// 记录错误日志
				return null;
			}			
			
        }
		//使用缓存
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// 写入缓存
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// 记录错误日志
				}
			}
			return objType;
		}
        #endregion

        #region CreateSysManage
        public static WSS.IDAL.ISysManage CreateSysManage()
		{
			//方式1			
			//return (WSS.IDAL.ISysManage)Assembly.Load(AssemblyPath).CreateInstance(AssemblyPath+".SysManage");

			//方式2 			
			string classNamespace = AssemblyPath+".SysManage";	
			object objType=CreateObject(AssemblyPath,classNamespace);
            return (WSS.IDAL.ISysManage)objType;		
		}
		#endregion
             
        
   
		/// <summary>
		/// 创建Department数据层接口。部门表
		/// </summary>
		public static WSS.IDAL.IDepartment CreateDepartment()
		{

			string ClassNamespace = AssemblyPath +".Department";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IDepartment)objType;
		}


		/// <summary>
		/// 创建Dictionary数据层接口。部门表
		/// </summary>
		public static WSS.IDAL.IDictionary CreateDictionary()
		{

			string ClassNamespace = AssemblyPath +".Dictionary";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IDictionary)objType;
		}


		/// <summary>
		/// 创建Menus数据层接口。菜单表
		/// </summary>
		public static WSS.IDAL.IMenus CreateMenus()
		{

			string ClassNamespace = AssemblyPath +".Menus";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IMenus)objType;
		}


		/// <summary>
		/// 创建Notfiy数据层接口。即时窗口提醒信息表
		/// </summary>
		public static WSS.IDAL.INotfiy CreateNotfiy()
		{

			string ClassNamespace = AssemblyPath +".Notfiy";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.INotfiy)objType;
		}


		/// <summary>
		/// 创建PubNotice数据层接口。公告表
		/// </summary>
		public static WSS.IDAL.IPubNotice CreatePubNotice()
		{

			string ClassNamespace = AssemblyPath +".PubNotice";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IPubNotice)objType;
		}


		/// <summary>
		/// 创建Roles数据层接口。角色表
		/// </summary>
		public static WSS.IDAL.IRoles CreateRoles()
		{

			string ClassNamespace = AssemblyPath +".Roles";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IRoles)objType;
		}


		/// <summary>
		/// 创建SysLog数据层接口。系统日志表
		/// </summary>
		public static WSS.IDAL.ISysLog CreateSysLog()
		{

			string ClassNamespace = AssemblyPath +".SysLog";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.ISysLog)objType;
		}


		/// <summary>
		/// 创建Tasks数据层接口。系统日志表
		/// </summary>
		public static WSS.IDAL.ITasks CreateTasks()
		{

			string ClassNamespace = AssemblyPath +".Tasks";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.ITasks)objType;
		}


		/// <summary>
		/// 创建Users数据层接口。用户表
		/// </summary>
		public static WSS.IDAL.IUsers CreateUsers()
		{

			string ClassNamespace = AssemblyPath +".Users";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IUsers)objType;
		}

}
}