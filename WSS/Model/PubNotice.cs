using System;
namespace WSS.Model
{
	/// <summary>
	/// �����
	/// </summary>
	[Serializable]
	public partial class PubNotice
	{
		public PubNotice()
		{}
		#region Model
		private int _f_id;
		private string _f_title;
		private string _f_note;
		private string _f_datetime;
		/// <summary>
		/// ����ID
		/// </summary>
		public int F_ID
		{
			set{ _f_id=value;}
			get{return _f_id;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string F_Title
		{
			set{ _f_title=value;}
			get{return _f_title;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string F_Note
		{
			set{ _f_note=value;}
			get{return _f_note;}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public string F_DateTime
		{
			set{ _f_datetime=value;}
			get{return _f_datetime;}
		}
		#endregion Model

	}
}

