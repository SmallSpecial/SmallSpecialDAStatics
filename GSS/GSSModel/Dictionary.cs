using System;
namespace GSSModel
{
	/// <summary>
	/// ◊÷µ‰±Ì
	/// </summary>
	[Serializable]
	public partial class Dictionary
	{
		public Dictionary()
		{}
		#region Model
		private int _f_dicid;
		private int? _f_parentid;
		private string _f_value;
		private bool _f_isused= true;
		private int _f_sort=0;
		/// <summary>
		/// ±‡∫≈
		/// </summary>
		public int F_DicID
		{
			set{ _f_dicid=value;}
			get{return _f_dicid;}
		}
		/// <summary>
		/// ∏∏±‡∫≈
		/// </summary>
		public int? F_ParentID
		{
			set{ _f_parentid=value;}
			get{return _f_parentid;}
		}
		/// <summary>
		/// ÷µ
		/// </summary>
		public string F_Value
		{
			set{ _f_value=value;}
			get{return _f_value;}
		}
		/// <summary>
		///  «∑Ò∆Ù”√
		/// </summary>
		public bool F_IsUsed
		{
			set{ _f_isused=value;}
			get{return _f_isused;}
		}
		/// <summary>
		/// ≈≈–Ú
		/// </summary>
		public int F_Sort
		{
			set{ _f_sort=value;}
			get{return _f_sort;}
		}
		#endregion Model

	}
}

