using System;
namespace WSS.Model
{
	/// <summary>
	/// 即时窗口提醒信息表
	/// </summary>
	[Serializable]
	public partial class Notfiy
	{
		public Notfiy()
		{}
		#region Model
		private int _f_id;
		private string _f_title;
		private string _f_note;
		private string _f_url;
		private DateTime _f_datetime= DateTime.Now;
		private DateTime? _f_seetime;
		private bool? _f_isseed= false;
		private int? _f_userid;
		private int? _f_type;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int F_ID
		{
			set{ _f_id=value;}
			get{return _f_id;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string F_Title
		{
			set{ _f_title=value;}
			get{return _f_title;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string F_Note
		{
			set{ _f_note=value;}
			get{return _f_note;}
		}
		/// <summary>
		/// 指向的网页地址
		/// </summary>
		public string F_URL
		{
			set{ _f_url=value;}
			get{return _f_url;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime F_DateTime
		{
			set{ _f_datetime=value;}
			get{return _f_datetime;}
		}
		/// <summary>
		/// 查看时间
		/// </summary>
		public DateTime? F_SeeTime
		{
			set{ _f_seetime=value;}
			get{return _f_seetime;}
		}
		/// <summary>
		/// 是否已查看
		/// </summary>
		public bool? F_IsSeed
		{
			set{ _f_isseed=value;}
			get{return _f_isseed;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public int? F_UserID
		{
			set{ _f_userid=value;}
			get{return _f_userid;}
		}
		/// <summary>
		/// 即时窗口类型
		/// </summary>
		public int? F_Type
		{
			set{ _f_type=value;}
			get{return _f_type;}
		}
		#endregion Model

	}
}

