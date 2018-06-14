using System;
namespace GSSModel
{
	/// <summary>
	/// 工单类型模板表
	/// </summary>
	[Serializable]
	public partial class TaskTemplate
	{
		public TaskTemplate()
		{}
        #region Model
        private int _f_id;
        private int? _f_type;
        private string _f_template;
        /// <summary>
        /// 主键ID
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// 工单类型
        /// </summary>
        public int? F_Type
        {
            set { _f_type = value; }
            get { return _f_type; }
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string F_Template
        {
            set { _f_template = value; }
            get { return _f_template; }
        }
        #endregion Model

	}
}

