using System;
namespace WSS.Model
{
	/// <summary>
	/// �˵���
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
		/// �˵����
		/// </summary>
		public int F_MenuID
		{
			set{ _f_menuid=value;}
			get{return _f_menuid;}
		}
		/// <summary>
		/// �˵�����
		/// </summary>
		public string F_Name
		{
			set{ _f_name=value;}
			get{return _f_name;}
		}
		/// <summary>
		/// �����
		/// </summary>
		public int F_ParentID
		{
			set{ _f_parentid=value;}
			get{return _f_parentid;}
		}
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool F_IsUsed
		{
			set{ _f_isused=value;}
			get{return _f_isused;}
		}
		#endregion Model

	}
}

