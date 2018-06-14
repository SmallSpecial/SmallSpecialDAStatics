using System;
namespace WSS.Model
{
	/// <summary>
	/// 菜单表
	/// </summary>
	[Serializable]
	public partial class Menus
	{
		public Menus()
		{}
		#region Model
		private int _f_menuid;
		private string _f_name;
		private int _f_parentid=0;
		private bool _f_isused= true;
		/// <summary>
		/// 菜单编号
		/// </summary>
		public int F_MenuID
		{
			set{ _f_menuid=value;}
			get{return _f_menuid;}
		}
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string F_Name
		{
			set{ _f_name=value;}
			get{return _f_name;}
		}
		/// <summary>
		/// 父编号
		/// </summary>
		public int F_ParentID
		{
			set{ _f_parentid=value;}
			get{return _f_parentid;}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public bool F_IsUsed
		{
			set{ _f_isused=value;}
			get{return _f_isused;}
		}
		#endregion Model

	}
}

