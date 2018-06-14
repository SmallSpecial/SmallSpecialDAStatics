using System;

namespace GSSModel
{
    /// <summary>
    /// CountDetail:实体类
    /// </summary>
    [Serializable]
    public partial class CountDetail
    {
        public CountDetail()
        { }
        #region Model
        private long _id;
        private int? _year;
        private int? _month;
        private int? _day;
        private int? _hour;
        private int? _game;
        private int? _type;
        private string _page;
        private string _ip;
        private string _iesoft;
        private string _os;
        private string _clr;
        private string _language;
        private string _winbit;
        private DateTime? _time = DateTime.Now;
        private int screenwidth;

        public int Screenwidth
        {
            get { return screenwidth; }
            set { screenwidth = value; }
        }
        private int screenheight;

        public int Screenheight
        {
            get { return screenheight; }
            set { screenheight = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Month
        {
            set { _month = value; }
            get { return _month; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Day
        {
            set { _day = value; }
            get { return _day; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Hour
        {
            set { _hour = value; }
            get { return _hour; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Game
        {
            set { _game = value; }
            get { return _game; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Page
        {
            set { _page = value; }
            get { return _page; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IESoft
        {
            set { _iesoft = value; }
            get { return _iesoft; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OS
        {
            set { _os = value; }
            get { return _os; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CLR
        {
            set { _clr = value; }
            get { return _clr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WinBit
        {
            set { _winbit = value; }
            get { return _winbit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Time
        {
            set { _time = value; }
            get { return _time; }
        }
        #endregion Model
    }
}
