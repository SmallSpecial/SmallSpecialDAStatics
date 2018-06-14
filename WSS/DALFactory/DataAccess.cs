using System;
using System.Reflection;
using System.Configuration;
namespace WSS.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL��
    /// ��������ﴴ�����󱨴�����web.config���Ƿ��޸���<add key="DAL" value="WSS.SQLServerDAL" />��
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
		public DataAccess()
		{ }

        #region CreateObject 

		//��ʹ�û���
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// ��¼������־
				return null;
			}			
			
        }
		//ʹ�û���
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// д�뻺��
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// ��¼������־
				}
			}
			return objType;
		}
        #endregion

        #region CreateSysManage
        public static WSS.IDAL.ISysManage CreateSysManage()
		{
			//��ʽ1			
			//return (WSS.IDAL.ISysManage)Assembly.Load(AssemblyPath).CreateInstance(AssemblyPath+".SysManage");

			//��ʽ2 			
			string classNamespace = AssemblyPath+".SysManage";	
			object objType=CreateObject(AssemblyPath,classNamespace);
            return (WSS.IDAL.ISysManage)objType;		
		}
		#endregion
             
        
   
		/// <summary>
		/// ����Department���ݲ�ӿڡ����ű�
		/// </summary>
		public static WSS.IDAL.IDepartment CreateDepartment()
		{

			string ClassNamespace = AssemblyPath +".Department";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IDepartment)objType;
		}


		/// <summary>
		/// ����Dictionary���ݲ�ӿڡ����ű�
		/// </summary>
		public static WSS.IDAL.IDictionary CreateDictionary()
		{

			string ClassNamespace = AssemblyPath +".Dictionary";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IDictionary)objType;
		}


		/// <summary>
		/// ����Menus���ݲ�ӿڡ��˵���
		/// </summary>
		public static WSS.IDAL.IMenus CreateMenus()
		{

			string ClassNamespace = AssemblyPath +".Menus";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IMenus)objType;
		}


		/// <summary>
		/// ����Notfiy���ݲ�ӿڡ���ʱ����������Ϣ��
		/// </summary>
		public static WSS.IDAL.INotfiy CreateNotfiy()
		{

			string ClassNamespace = AssemblyPath +".Notfiy";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.INotfiy)objType;
		}


		/// <summary>
		/// ����PubNotice���ݲ�ӿڡ������
		/// </summary>
		public static WSS.IDAL.IPubNotice CreatePubNotice()
		{

			string ClassNamespace = AssemblyPath +".PubNotice";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IPubNotice)objType;
		}


		/// <summary>
		/// ����Roles���ݲ�ӿڡ���ɫ��
		/// </summary>
		public static WSS.IDAL.IRoles CreateRoles()
		{

			string ClassNamespace = AssemblyPath +".Roles";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IRoles)objType;
		}


		/// <summary>
		/// ����SysLog���ݲ�ӿڡ�ϵͳ��־��
		/// </summary>
		public static WSS.IDAL.ISysLog CreateSysLog()
		{

			string ClassNamespace = AssemblyPath +".SysLog";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.ISysLog)objType;
		}


		/// <summary>
		/// ����Tasks���ݲ�ӿڡ�ϵͳ��־��
		/// </summary>
		public static WSS.IDAL.ITasks CreateTasks()
		{

			string ClassNamespace = AssemblyPath +".Tasks";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.ITasks)objType;
		}


		/// <summary>
		/// ����Users���ݲ�ӿڡ��û���
		/// </summary>
		public static WSS.IDAL.IUsers CreateUsers()
		{

			string ClassNamespace = AssemblyPath +".Users";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (WSS.IDAL.IUsers)objType;
		}

}
}