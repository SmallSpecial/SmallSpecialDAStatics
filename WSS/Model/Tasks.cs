using System;
namespace WSS.Model
{
    /// <summary>
    /// ������
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
        /// ����
        /// </summary>
        public string F_Title
        {
            set { _f_title = value; }
            get { return _f_title; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        /// <summary>
        /// ������Դ
        /// </summary>
        public int? F_From
        {
            set { _f_from = value; }
            get { return _f_from; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int? F_Type
        {
            set { _f_type = value; }
            get { return _f_type; }
        }
        /// <summary>
        /// �����̶�
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
        /// ��ǩ
        /// </summary>
        public string F_Tag
        {
            set { _f_tag = value; }
            get { return _f_tag; }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public int? F_State
        {
            set { _f_state = value; }
            get { return _f_state; }
        }
        /// <summary>
        /// ��ҵ绰
        /// </summary>
        public string F_Telphone
        {
            set { _f_telphone = value; }
            get { return _f_telphone; }
        }
        /// <summary>
        /// ��ǰְ����
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
        /// ����ʱ��
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
        /// ��״̬��0:����;1:ɾ��;2:��ʷ��
        /// </summary>
        public int? F_Rowtype
        {
            set { _f_rowtype = value; }
            get { return _f_rowtype; }
        }
        #endregion Model

    }
}

