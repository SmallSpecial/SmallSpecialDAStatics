using System;
namespace GSSModel
{
    /// <summary>
    /// 部门表
    /// </summary>
    [Serializable]
    public partial class Department
    {
        public Department()
        { }
        #region Model
        private int _f_departid;
        private int _f_parentid = 0;
        private string _f_departname;
        private string _f_note;
        /// <summary>
        /// 部门ID
        /// </summary>
        public int F_DepartID
        {
            set { _f_departid = value; }
            get { return _f_departid; }
        }
        /// <summary>
        /// 父编号
        /// </summary>
        public int F_ParentID
        {
            set { _f_parentid = value; }
            get { return _f_parentid; }
        }
        /// <summary>
        /// 部门名
        /// </summary>
        public string F_DepartName
        {
            set { _f_departname = value; }
            get { return _f_departname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Note
        {
            set { _f_note = value; }
            get { return _f_note; }
        }
        #endregion Model

    }
}

