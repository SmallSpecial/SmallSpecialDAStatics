using System;
namespace WSS.Model
{
	/// <summary>
	/// ��ɫ��
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
		/// ����ID
		/// </summary>
		public int F_RoleID
		{
			set{ _f_roleid=value;}
			get{return _f_roleid;}
		}
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool F_IsUsed
		{
			set{ _f_isused=value;}
			get{return _f_isused;}
		}
		/// <summary>
		/// Ȩ��
		/// </summary>
		public string F_Power
		{
			set{ _f_power=value;}
			get{return _f_power;}
		}
		#endregion Model

	}
}

