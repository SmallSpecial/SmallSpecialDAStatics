using System;
namespace GSSModel
{
    /// <summary>
    /// FDBISql
    /// </summary>
    [Serializable]
    public partial class FDBISql
    {
        public FDBISql()
        { }
        #region Model
        private int _f_id;
        private string _f_title;
        private string _f_note;
        private string _f_sql;
        private int? _f_userid = -1;
        private DateTime _f_datetime = DateTime.Now;
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
        /// 备注
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        /// <summary>
        /// SQL语句
        /// </summary>
        public string F_Sql
        {
            set { _f_sql = value; }
            get { return _f_sql; }
        }
        /// <summary>
        /// 用户编号(公用为-1)
        /// </summary>
        public int? F_UserID
        {
            set { _f_userid = value; }
            get { return _f_userid; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime F_DaTeTime
        {
            set { _f_datetime = value; }
            get { return _f_datetime; }
        }
        #endregion Model

    }
}

