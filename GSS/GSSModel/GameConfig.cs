using System;
namespace GSSModel
{
    /// <summary>
    /// �ֵ��
    /// </summary>
    [Serializable]
    public partial class GameConfig
    {
        public GameConfig()
        { }
        #region Model
        private int _f_id;
        private int? _f_parentid;
        private string _f_name;
        private string _f_value;
        private string _f_value1;
        private string _f_valuegame;
        private bool _f_isused = true;
        private int _f_sort = 0;
        /// <summary>
        /// ���
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public int? F_ParentID
        {
            set { _f_parentid = value; }
            get { return _f_parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_Name
        {
            set { _f_name = value; }
            get { return _f_name; }
        }
        /// <summary>
        /// ֵ
        /// </summary>
        public string F_Value
        {
            set { _f_value = value; }
            get { return _f_value; }
        }
        /// <summary>
        /// ֵ(�ڶ���ֵ,�����߲�ѯURL)
        /// </summary>
        public string F_Value1
        {
            set { _f_value1 = value; }
            get { return _f_value1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_ValueGame
        {
            set { _f_valuegame = value; }
            get { return _f_valuegame; }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool F_IsUsed
        {
            set { _f_isused = value; }
            get { return _f_isused; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int F_Sort
        {
            set { _f_sort = value; }
            get { return _f_sort; }
        }
        #endregion Model

    }
}

