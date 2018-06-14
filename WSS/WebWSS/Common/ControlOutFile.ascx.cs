using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS.Common
{
    public partial class ControlOutFile : System.Web.UI.UserControl
    {
        private bool _visibleExcel = true;
        /// <summary>
        /// 是否显示EXCEL
        /// </summary>
        public bool VisibleExcel
        {
            get { return _visibleExcel; }
            set { _visibleExcel = value; }
        }

        private bool _visibleDoc = true;
        /// <summary>
        /// 是否显示DOC
        /// </summary>
        public bool VisibleDoc
        {
            get { return _visibleDoc; }
            set { _visibleDoc = value; }
        }

        private string _fileName = "ShenLongYou";
        /// <summary>
        /// 导出文件名
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private Control _controlOut;
        /// <summary>
        /// 需要导出的控件
        /// </summary>
        public Control ControlOut
        {
            get { return _controlOut; }
            set { _controlOut = value; }
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        private string _fileType = "ms-excel";

        /// <summary>
        /// 文件后缀
        /// </summary>
        private string _fileSuffix = "xls";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitBtn();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitBtn()
        {
            btnOutExcel.Visible = _visibleExcel;
            btnOutDoc.Visible = _visibleDoc;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (_controlOut==null)
                {
                    return;
                }
                Button btn = (Button)sender;
                switch (btn.ID)
                {
                    case "btnOutExcel":
                        _fileType = "ms-excel";
                        _fileSuffix = "xls";
                        break;
                    case "btnOutDoc":
                        _fileType = "ms-doc";
                        _fileSuffix = "doc";
                        break;
                    default:
                        break;
                }
                OutFile();
            //}
            //catch (System.Exception ex)
            //{

            //}
        }

        private void OutFile()
        {
            //定义输出流
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            System.Web.UI.Page oPage = new System.Web.UI.Page();
            System.Web.UI.HtmlControls.HtmlForm oHtmlForm = new System.Web.UI.HtmlControls.HtmlForm();
            oPage.EnableEventValidation = false;
            oPage.DesignerInitialize();
            oPage.Controls.Add(oHtmlForm);
            oPage.EnableViewState = false;

            oHtmlForm.Controls.Add(_controlOut);
            oPage.RenderControl(oHtmlTextWriter);

            //定义文档类型 字符编码
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";
            string oDisposition = string.Format("attachment;filename=WSS_{0}.{1}",_fileName,_fileSuffix);
            Response.AppendHeader("Content-disposition", oDisposition);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.ContentType = string.Format( "application/{0}",_fileType);
            this.EnableViewState = false;

            string oResponseStr = oStringWriter.ToString().Replace("\r\n", "").Replace("bold;'></th>", "bold;'>");
            oResponseStr = System.Text.RegularExpressions.Regex.Replace(oResponseStr, @"<input.*?/>", "");
            Response.Write(oResponseStr);
            Response.End();
        }




    }
}