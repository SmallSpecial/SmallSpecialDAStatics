using System;
namespace GSSModel
{
	/// <summary>
	/// 用户表
	/// </summary>
	[Serializable]
	public partial class Users
	{
		public Users()
		{}
        #region Model
        private int _f_userid;
        private string _f_username;
        private string _f_password;
        private int? _f_departid;
        private int? _f_roleid;
        private string _f_realname;
        private bool? _f_sex = false;
        private DateTime? _f_birthday;
        private string _f_email;
        private string _f_mobilephone;
        private string _f_telphone;
        private string _f_note;
        private DateTime? _f_regtime = DateTime.Now;
        private DateTime? _f_lastintime = DateTime.Now;
        private bool _f_isused = true;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int F_UserID
        {
            set { _f_userid = value; }
            get { return _f_userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string F_UserName
        {
            set { _f_username = value; }
            get { return _f_username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string F_PassWord
        {
            set { _f_password = value; }
            get { return _f_password; }
        }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int? F_DepartID
        {
            set { _f_departid = value; }
            get { return _f_departid; }
        }
        /// <summary>
        /// 角色名
        /// </summary>
        public int? F_RoleID
        {
            set { _f_roleid = value; }
            get { return _f_roleid; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string F_RealName
        {
            set { _f_realname = value; }
            get { return _f_realname; }
        }
        /// <summary>
        /// 姓别
        /// </summary>
        public bool? F_Sex
        {
            set { _f_sex = value; }
            get { return _f_sex; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? F_Birthday
        {
            set { _f_birthday = value; }
            get { return _f_birthday; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string F_Email
        {
            set { _f_email = value; }
            get { return _f_email; }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string F_MobilePhone
        {
            set { _f_mobilephone = value; }
            get { return _f_mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_Telphone
        {
            set { _f_telphone = value; }
            get { return _f_telphone; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? F_RegTime
        {
            set { _f_regtime = value; }
            get { return _f_regtime; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? F_LastInTime
        {
            set { _f_lastintime = value; }
            get { return _f_lastintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool F_IsUsed
        {
            set { _f_isused = value; }
            get { return _f_isused; }
        }
        #endregion Model

	}
}

