using System;
namespace WSS.Model
{
	/// <summary>
	/// 角色表
	/// </summary>
	[Serializable]
	public partial class Roles
	{
		public Roles()
		{}
		#region Model
		private int _f_roleid;
		private bool _f_isused= true;
		private string _f_power;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int F_RoleID
		{
			set{ _f_roleid=value;}
			get{return _f_roleid;}
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
		/// 权限
		/// </summary>
		public string F_Power
		{
			set{ _f_power=value;}
			get{return _f_power;}
		}
		#endregion Model

	}
}

