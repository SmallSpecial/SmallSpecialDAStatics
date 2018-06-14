using System;
namespace GSSModel
{
    /// <summary>
    /// 工单表
    /// </summary>
    [Serializable]
    public partial class TasksLog
    {
        public TasksLog()
        { }
        #region Model
        private int _f_logid;
        private int _f_id;
        private string _f_title;
        private string _f_note;
        private int? _f_from;
        private int? _f_viplevel;
        private int? _f_limittype;
        private DateTime? _f_limittime;
        private int? _f_type;
        private int? _f_state;
        private int? _f_gamename;
        private string _f_gamebigzone;
        private string _f_gamezone;
        private string _f_guserid;
        private string _f_gusername;
        private string _f_groleid;
        private string _f_grolename;
        private string _f_telphone;
        private string _f_gpeoplename;
        private int? _f_dutyman;
        private int? _f_predutyman;
        private int? _f_creatman;
        private DateTime? _f_creattime;
        private int? _f_editman;
        private DateTime? _f_edittime;
        private string _f_urinfo;
        private int? _f_rowtype = 0;
        private bool? _f_cusername;
        private bool? _f_cpswprotect;
        private bool? _f_cpersonid;
        private string _f_cother;
        private string _f_olastlogintime;
        private bool? _f_ocanrestor;
        private string _f_oalwaysplace;
        private bool? _f_ttoolused;
        private string _f_tusedata;
        /// <summary>
        /// 
        /// </summary>
        public int F_LogID
        {
            set { _f_logid = value; }
            get { return _f_logid; }
        }
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
        /// 
        /// </summary>
        public int? F_VipLevel
        {
            set { _f_viplevel = value; }
            get { return _f_viplevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_LimitType
        {
            set { _f_limittype = value; }
            get { return _f_limittype; }
        }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public DateTime? F_LimitTime
        {
            set { _f_limittime = value; }
            get { return _f_limittime; }
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
        /// 工单状态
        /// </summary>
        public int? F_State
        {
            set { _f_state = value; }
            get { return _f_state; }
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
        public string F_GameBigZone
        {
            set { _f_gamebigzone = value; }
            get { return _f_gamebigzone; }
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
        public string F_GUserName
        {
            set { _f_gusername = value; }
            get { return _f_gusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GRoleID
        {
            set { _f_groleid = value; }
            get { return _f_groleid; }
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
        /// 玩家电话
        /// </summary>
        public string F_Telphone
        {
            set { _f_telphone = value; }
            get { return _f_telphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_GPeopleName
        {
            set { _f_gpeoplename = value; }
            get { return _f_gpeoplename; }
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
        /// 创建者(如来源为外部则为转成工单的人)
        /// </summary>
        public int? F_CreatMan
        {
            set { _f_creatman = value; }
            get { return _f_creatman; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreatTime
        {
            set { _f_creattime = value; }
            get { return _f_creattime; }
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
        /// 更新时间
        /// </summary>
        public DateTime? F_EditTime
        {
            set { _f_edittime = value; }
            get { return _f_edittime; }
        }
        /// <summary>
        /// 玩家和角色信息
        /// </summary>
        public string F_URInfo
        {
            set { _f_urinfo = value; }
            get { return _f_urinfo; }
        }
        /// <summary>
        /// 行状态（0:正常;1:删除;2:历史）
        /// </summary>
        public int? F_Rowtype
        {
            set { _f_rowtype = value; }
            get { return _f_rowtype; }
        }
        /// <summary>
        /// F＿C检验项表里的字段,少量的话暂时放这个表里.
        /// </summary>
        public bool? F_CUserName
        {
            set { _f_cusername = value; }
            get { return _f_cusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? F_CPSWProtect
        {
            set { _f_cpswprotect = value; }
            get { return _f_cpswprotect; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? F_CPersonID
        {
            set { _f_cpersonid = value; }
            get { return _f_cpersonid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_COther
        {
            set { _f_cother = value; }
            get { return _f_cother; }
        }
        /// <summary>
        /// F_O指其它玩家提供的信息
        /// </summary>
        public string F_OLastLoginTime
        {
            set { _f_olastlogintime = value; }
            get { return _f_olastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? F_OCanRestor
        {
            set { _f_ocanrestor = value; }
            get { return _f_ocanrestor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_OAlwaysPlace
        {
            set { _f_oalwaysplace = value; }
            get { return _f_oalwaysplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? F_TToolUsed
        {
            set { _f_ttoolused = value; }
            get { return _f_ttoolused; }
        }
        /// <summary>
        /// 工具使用的数据
        /// </summary>
        public string F_TUseData
        {
            set { _f_tusedata = value; }
            get { return _f_tusedata; }
        }
        #endregion Model

    }
}

