using System;
namespace WSS.Model
{
	/// <summary>
	/// ���ű�
	/// </summary>
	[Serializable]
	public partial class Department
	{
		public Department()
		{}
		#region Model
		private int _f_departid;
		private int _f_parentid=0;
		private string _f_departname;
		/// <summary>
		/// ����ID
		/// </summary>
		public int F_DepartID
		{
			set{ _f_departid=value;}
			get{return _f_departid;}
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
		/// ������
		/// </summary>
		public string F_DepartName
		{
			set{ _f_departname=value;}
			get{return _f_departname;}
		}
		#endregion Model

	}
}

