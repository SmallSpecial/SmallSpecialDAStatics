using System;
namespace GSSModel
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
		private string _f_formname;
		private int _f_parentid=0;
		private bool _f_isused= true;
		private int _f_sort=0;
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
		/// 窗体名称
		/// </summary>
		public string F_FormName
		{
			set{ _f_formname=value;}
			get{return _f_formname;}
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
		/// <summary>
		/// 排序
		/// </summary>
		public int F_Sort
		{
			set{ _f_sort=value;}
			get{return _f_sort;}
		}
		#endregion Model

	}
}

