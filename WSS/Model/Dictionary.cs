using System;
namespace WSS.Model
{
    /// <summary>
    /// Dictionary:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public partial class Dictionary 
    {
        public  Dictionary()
        { }


        private int _f_dicid;
        private int _parentid;
        private string _f_value;
        private bool _f_isused = true;
        private int _f_sort = 0;
        /// <summary>
        /// 
        /// </summary>
        public int F_DicID
        {
            set { _f_dicid = value; }
            get { return _f_dicid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int F_ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_Value
        {
            set { _f_value = value; }
            get { return _f_value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool F_IsUsed
        {
            set { _f_isused = value; }
            get { return _f_isused; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int F_Sort
        {
            set { _f_sort = value; }
            get { return _f_sort; }
        }


    }
}

