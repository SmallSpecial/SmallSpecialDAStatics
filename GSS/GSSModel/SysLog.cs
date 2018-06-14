using System;
namespace GSSModel
{
	/// <summary>
	/// ϵͳ��־��
	/// </summary>
	[Serializable]
	public partial class SysLog
	{
		public SysLog()
		{}
		#region Model
		private int _f_id;
		private int? _f_userid;
		private string _f_username;
		private string _f_note;
		private string _f_data;
		private DateTime? _f_datetime;
		/// <summary>
		/// ����ID
		/// </summary>
		public int F_ID
		{
			set{ _f_id=value;}
			get{return _f_id;}
		}
		/// <summary>
		/// �û�ID
		/// </summary>
		public int? F_UserID
		{
			set{ _f_userid=value;}
			get{return _f_userid;}
		}
		/// <summary>
		/// �û���
		/// </summary>
		public string F_UserName
		{
			set{ _f_username=value;}
			get{return _f_username;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string F_Note
		{
			set{ _f_note=value;}
			get{return _f_note;}
		}
		/// <summary>
		/// ��ҳ��ַ
		/// </summary>
		public string F_Data
		{
			set{ _f_data=value;}
			get{return _f_data;}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public DateTime? F_DateTime
		{
			set{ _f_datetime=value;}
			get{return _f_datetime;}
		}
		#endregion Model

	}
}

