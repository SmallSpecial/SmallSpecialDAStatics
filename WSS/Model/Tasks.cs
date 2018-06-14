using System;
namespace WSS.Model
{
    /// <summary>
    /// 工单表
    /// </summary>
    [Serializable]
    public partial class Tasks
    {
        public Tasks()
        { }
        #region Model
        private int _f_id;
        private string _f_title;
        private string _f_note;
        private int? _f_from;
        private int? _f_type;
        private int? _f_jinjilevel;
        private int? _f_gamename;
        private string _f_gamezone;
        private string _f_guserid;
        private string _f_grolename;
        private string _f_tag;
        private int? _f_state;
        private string _f_telphone;
        private int? _f_dutyman;
        private int? _f_predutyman;
        private DateTime _f_datetime;
        private int? _f_editman;
        private int? _f_rowtype = 0;
        /// <summary>
        /// 
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string F_Title
        {
            set { _f_title = value; }
            get { return _f_title; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        /// <summary>
        /// 工单来源
        /// </summary>
        public int? F_From
        {
            set { _f_from = value; }
            get { return _f_from; }
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
        /// 紧急程度
        /// </summary>
        public int? F_JinjiLevel
        {
            set { _f_jinjilevel = value; }
            get { return _f_jinjilevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_GameName
        {
            set { _f_gamename = value; }
            get { return _f_gamename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GameZone
        {
            set { _f_gamezone = value; }
            get { return _f_gamezone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GUserID
        {
            set { _f_guserid = value; }
            get { return _f_guserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GRoleName
        {
            set { _f_grolename = value; }
            get { return _f_grolename; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string F_Tag
        {
            set { _f_tag = value; }
            get { return _f_tag; }
        }
        /// <summary>
        /// 工单状态
        /// </summary>
        public int? F_State
        {
            set { _f_state = value; }
            get { return _f_state; }
        }
        /// <summary>
        /// 玩家电话
        /// </summary>
        public string F_Telphone
        {
            set { _f_telphone = value; }
            get { return _f_telphone; }
        }
        /// <summary>
        /// 当前职责人
        /// </summary>
        public int? F_DutyMan
        {
            set { _f_dutyman = value; }
            get { return _f_dutyman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_PreDutyMan
        {
            set { _f_predutyman = value; }
            get { return _f_predutyman; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime F_DateTime
        {
            set { _f_datetime = value; }
            get { return _f_datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_EditMan
        {
            set { _f_editman = value; }
            get { return _f_editman; }
        }
        /// <summary>
        /// 行状态（0:正常;1:删除;2:历史）
        /// </summary>
        public int? F_Rowtype
        {
            set { _f_rowtype = value; }
            get { return _f_rowtype; }
        }
        #endregion Model

    }
}

