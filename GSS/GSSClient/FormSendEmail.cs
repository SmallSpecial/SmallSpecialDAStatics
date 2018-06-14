using GSSCSFrameWork;
using GSSModel.Response;
using GSSUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GSS.DBUtility;
namespace GSSClient
{
    public partial class FormSendEmail : GSSUI.AForm.FormMain
    {
        TcpCli tcp;
        ClientHandles Client;
        int TaskTypeId;
        string[] excelColumns;
        DataTable rolesTable=new DataTable();
        public FormSendEmail(ClientHandles client,int taskTypeID)
        {
            InitializeComponent();
            Client=client;
            TaskTypeId=taskTypeID;
            InitLanguage();
            InitData();
            Test();
        }
        void InitData() 
        {
            dtpStartTime.CustomFormat = SystemConfig.DateHourMinute;
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = SystemConfig.DateHourMinute;
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            DateTime now = DateTime.Now;
            dtpStartTime.Value = now;
            dtpEndTime.Value = now.AddDays(30);
            excelColumns = new string[2];
            excelColumns[0] = LanguageResource.Language.Map_RoleNo;
            excelColumns[1] = LanguageResource.Language.Map_RoleName;
            InitGridHead();
        }
        void InitGridHead() 
        {
            rolesTable.Clear();
            rolesTable.Columns.Clear();
            foreach (string item in excelColumns)
            {
                rolesTable.Columns.Add(new DataColumn(item));
                //rolesTable.Columns.Add(item, item);
            }
            gridReceiveRole.DataSource = rolesTable;
        }
        void InitLanguage() 
        {
            this.Text = LanguageResource.Language.BtnSendEmail;
            btnSendEmail.Text = LanguageResource.Language.LblSave;
            lblStartTime.Text = LanguageResource.Language.LblStartTime;
            lblEndTime.Text = LanguageResource.Language.LblEndTime;
            lblBigZone.Text = LanguageResource.Language.LblBigZone;
            lblZone.Text = LanguageResource.Language.LblZone;
            btnLodingRoles.Text = LanguageResource.Language.BtnLoad;
            cbHavaEquip.Text = LanguageResource.Language.LblProp;
            lblEquipID.Text = LanguageResource.Language.LblEquipNo;
            lblEquipSizeOf.Text = LanguageResource.Language.LblUnit_Number;
            lblDownTemplae.Text = LanguageResource.Language.LblDownloadTemplate;
            gbZoneWithBelongBig.Text = LanguageResource.Language.LblBelongZoneWithBigZone;
            gbEquip.Text = LanguageResource.Language.LblAwardID;
            gbReceiver.Text = LanguageResource.Language.LblReceiveRange;
            btnSelectFile.Text = LanguageResource.Language.LblExcel;
            btnClearRole.Text = LanguageResource.Language.BtnClear;
            gbValidTime.Text = LanguageResource.Language.LblTimeLimit;
            gbEmail.Text = LanguageResource.Language.LblEmailInfo;
            lblEmailHead.Text = LanguageResource.Language.LblTitle;
            lblEmailBody.Text = LanguageResource.Language.LblBody;
        }
        void TaskInit() 
        {
        
        }
        private void CheckBox_Check(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            string targetName = ck.Tag as string;//这是作用的目标元素
            if (string.IsNullOrEmpty(targetName))
            {
                return;
            }
            Control ele =ControllsReflect.GetElement(targetName,this.Controls);
            if (ck.Checked)
            {
                ele.Visible = true;
            }
            else 
            {
                ele.Visible = false;
            }
        }
        void Test() 
        {
            //联动绑定战区
            cmbBigZone.SelectedIndexChanged += new EventHandler(ComboBox_SelectIndexChanged);
            txtEquipID.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtEquipSizeOf.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            QueryCommboxDataSource(cmbBigZone, SystemConfig.BigZoneParentId.ToString());
            btnSelectFile.Click += new EventHandler(ButtonSelectFile_click);
        }
        private  void ButtonSelectFile_click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            string filePath = string.Empty;
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePath = file.FileName;
                SelectFileComplate(filePath);
            }
            else 
            {
                SelectFileComplate(null);
            }
        }
        void ComboBox_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Control linkage= ControllsReflect.GetElement((string)cmb.Tag, this.Controls);
            if (linkage == null)
            {
                return;
            }
            ComboBox linkEle = linkage as ComboBox;
            string pid = cmb.GetComboBoxSelectItemId();
            QueryCommboxDataSource(linkEle, pid);
        }
        void QueryCommboxDataSource(ComboBox cmb,string parentId) 
        {//名称，游戏值，上级配置ID，自身ID
            List<GameConfig> zones = ClientCache.GetGameZoneCache();//此处数据是加载到内存中还是每次读取性能更好？？
            List<ControllExt.ComboBoxItem> items = new List<ControllExt.ComboBoxItem>();
            items.Add(new ControllExt.ComboBoxItem() { Key = LanguageResource.Language.Tip_PleaseSelect });
            int pid;
            List<ControllExt.ComboBoxItem> select = new List<ControllExt.ComboBoxItem>();
            if (!string.IsNullOrEmpty(parentId))
            {
                pid = int.Parse(parentId);
                select = zones.Where(z => z.ParentId == pid).
                    Select(s =>
                    new ControllExt.ComboBoxItem()
                    {
                        Id = s.Id.ToString(),
                        ParentId = s.ParentId.ToString(),
                        Key = s.Name,
                        Value = s.GameValue
                    }).ToList();
                items.AddRange(select.ToArray());
                zones.Clear();
            }
            cmb.BindComboBoxDataSource(items);
        }
        private void BtnSendEmail(GSSModel.SendEmailToRole roles)
        {
            roles.Head = rtbEmailHead.Text;
            roles.Body = rtbEmailBody.Text;
            if (cbHavaEquip.Checked)
            {
                int temp;
                temp = int.Parse(txtEquipID.Text);
                roles.EquipId = temp;
                temp = int.Parse(txtEquipSizeOf.Text);
                roles.EquipNum = temp;
            }
            string id = cmbBigZone.GetComboBoxSelectValue();//必须选择大区和战区
            bool lackItem = true;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(id))
            {
                roles.BigZoneId = int.Parse(id);
                id = cmbZone.GetComboBoxSelectValue();
                if (!string.IsNullOrEmpty(id))
                {
                    lackItem = false;
                    roles.ZoneId = int.Parse(id);
                }
            }
            if (lackItem)
            {
                sb.AppendLine(LanguageResource.Language.Tip_PleaseSelectBigZoneAndZone);
            }
            if (string.IsNullOrEmpty(roles.Head) || string.IsNullOrEmpty(roles.Body))
            {
                sb.AppendLine(LanguageResource.Language.Tip_EmailInfoIsRequired);
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                MsgBox.Show(sb.ToString(), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            roles.StartTime = dtpStartTime.Value;
            roles.EndTime = dtpEndTime.Value;
            if (roles.StartTime > roles.EndTime)
            {
                MsgBox.Show(LanguageResource.Language.Tip_StartTimeShouldLessEndTime, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //由于导入的角色数据不可控，此处不进行工单建立
            int formId= this.Handle.ToInt32();
            GSSModel.Request.ClientData data = new GSSModel.Request.ClientData() { FormID = formId, Data = roles };
            Client.SendEmail(formId, data);
        }
        private void btnClearRole_Click(object sender, EventArgs e)
        {
            gridReceiveRole.Columns.Clear();
            InitGridHead();
        }
        private void SelectFileComplate(object data)
        {
            string path = data as string;
            gridReceiveRole.Columns.Clear();
            if (string.IsNullOrEmpty(path))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSelectDataFile, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtFile.Text = path;
            StringBuilder error = new StringBuilder();
            //加载文件中的数据
            DataSet ds = OledbFile.GetDataSet(path, SystemConfig.DefaultSheetName, "len([" + MapLanguageManage.GetStringByMapLanguageConfig(MapLanguageConfig.Map_RoleNo) + "])>0", error);
            string msg = error.ToString();
            if (!string.IsNullOrEmpty(msg))
            { 
                MsgBox.Show(msg,LanguageResource.Language.Tip_Tip,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //剔除多余列
            rolesTable = ds.Tables[0].Copy();//是否含有指定的列数据
            int ci = 0;
            DataColumn[]  origin=new DataColumn[rolesTable.Columns.Count];
            rolesTable.Columns.CopyTo(origin, 0);
            foreach (DataColumn item in origin)
            {
                string name=item.ColumnName;
                if (excelColumns.Contains(name.Trim()))
                {
                    ci++;
                }
                else 
                {
                    rolesTable.Columns.Remove(item);//不能直接使用datatable进行遍历移除【 集合已修改；可能无法执行枚举操作】
                }
            }
            if (ci < excelColumns.Length)
            {
                MsgBox.Show(LanguageResource.Language.Tip_ExcelTemplateError, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            gridReceiveRole.DataSource = rolesTable;
        }

        private void lblDownTemplae_Click(object sender, EventArgs e)
        {
            //下载模本(从服务端下载)
            string lang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            string file = typeof(FormSendEmail).Name + ".xlsx";
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            { //从服务器下载模本文件
                GSSModel.TemplateFile tem = new GSSModel.TemplateFile()
                {
                    TemplateName = file,
                    SystemLang = lang
                };
                GSSCSFrameWork.MsgStruts response = Client.DownloadTemplateFile(tem);
                if (response.msgsendstate != GSSCSFrameWork.msgSendState.None)
                {
                    FileStream fs = new FileStream(folderDialog.SelectedPath + "//" + file, FileMode.Create, FileAccess.Write);
                    fs.Write(response.Data, 0, response.Data.Length);
                    fs.Close();
                }
                else 
                {
                    string msg=DataSerialize.GetObjectFromByte(response.Data) as string;
                    MsgBox.Show(msg, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {//角色列表
            if (rolesTable.Rows.Count == 0)
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSelectDataFile, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> rids = new List<string>();
            foreach (DataRow item in rolesTable.Rows)
            {
                object column = item[LanguageResource.Language.Map_RoleNo];
                if (column == null)
                {
                    continue;
                }
                string val = column.ToString().Trim();
                int rid;
                if (int.TryParse(val, out rid))
                {
                   rids.Add(rid.ToString() );
                }
            }
            GSSModel.SendEmailToRole send = new GSSModel.SendEmailToRole();
            send.ReceiveRoles = string.Join(",", rids.ToArray());
            if (string.IsNullOrEmpty(send.ReceiveRoles))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSetEmailReceiver, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BtnSendEmail(send);
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == SystemConfig.BetweenFormChatMsgId)
            {//这是自定义的窗体键信息通讯 
                int msgid = m.WParam.ToInt32()-1;
                GSSModel.Request.ClientData arr = ShareData.Msg[msgid] as GSSModel.Request.ClientData;
                if (arr.Success)
                {
                    MsgBox.Show(LanguageResource.Language.BtnWorkorderDealwithOK, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                string error = arr.Message as string;
                MsgBox.Show(error, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            base.DefWndProc(ref m);
        }
    }
    public class ControllsReflect
    {
        /// <summary>
        /// 根据提供的窗体全部子元素找到指定name的元素
        /// </summary>
        /// <param name="eleName">目标name元素的</param>
        /// <param name="childrs">窗体下的子元素（建议此处参数赋值this.Controls）</param>
        /// <returns>查找到的元素，如果null表示没有找到</returns>
        public static Control GetElement(string eleName, Control.ControlCollection childrs)
        {
            if (string.IsNullOrEmpty(eleName))
            {
                return null;
            }
            Control ele = childrs[eleName];
            if (ele != null)
            {
                return ele;
            }
            foreach (Control item in childrs)
            {
                ele = item.Controls[eleName];
                if (ele == null && item.Controls.Count > 0)
                {
                    ele = GetElement(eleName, item.Controls);
                }
                if (ele != null)
                {
                    break;
                }
            }
            return ele;
        }
        
    }
    public class OledbFile
    {
        public static DataSet GetDataSet(string filename, string tname, string wherestr,StringBuilder error)//返回excel中不把第一行当做标题看待的数据集
        {
            try
            {
                //OleDbDataAdapter read column donot support ko-kr ,first read the sheet
                string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=yes\"";
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(sConnectionString);
                connection.Open();
                DataTable table= connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });//获取全部的sheet页【限制该Excel只能保留一份sheet】
                DataRowCollection rows = table.Rows;
                if (rows.Count > 0)
                {
                    int havaSheet = table.Select(string.Format("table_name='{0}$'", tname)).Count();//是否存在默认的Excel页
                    if (havaSheet == 0)
                    {
                        error.AppendFormat(LanguageResource.Language.Tip_ExcelDefaultSheetNameFormat,tname);
                        connection.Close();
                        return null;
                    }
                }
                string sql_select_commands = "Select * from [" + tname + "$] ";
                if (!string.IsNullOrEmpty(wherestr) )
                {
                    sql_select_commands += "where   " + wherestr;
                }
                System.Data.OleDb.OleDbDataAdapter adp = new System.Data.OleDb.OleDbDataAdapter(sql_select_commands, connection);
                DataSet ds = new DataSet();
                adp.Fill(ds, "table_a");
                adp.Dispose();
                connection.Close();
                return ds;
            }
            catch (Exception e)
            {
                error = new StringBuilder();//如果使用模本错误时给出的提示不准确
                error.AppendLine(e.Message);
                return null;
            }
        }
    }
    public  static class ControllExt
    {
        
        /// <summary>
        /// 为联动的选择按钮设定
        /// </summary>
        public class ComboBoxItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public string Id { get; set; }
            public string ParentId { get; set; }
        }
        enum ComboBoxSampleItem
        { 
            Key,
            Value,
            Id,
            ParentId
        }
        /// <summary>
        /// 为元素绑定key,value的数据源
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="data">key：显示的文本</param>
        public static void BindComboBoxDataSource(this ComboBox cmb,List<ComboBoxItem> data)
        {
            cmb.Items.Clear();
            foreach (ComboBoxItem item in data)
            {
                cmb.Items.Add(item);
            }
            cmb.ValueMember = ComboBoxSampleItem.Value.ToString();
            cmb.DisplayMember = ComboBoxSampleItem.Key.ToString();
            if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 获取元素选择的键值信息【需要使用BindComboBoxDataSource绑定数据源】
        /// </summary>
        /// <param name="cmb"></param>
        /// <returns>返回null表示数据源不是字典格式</returns>
        public static string GetComboBoxSelectItemId(this ComboBox cmb) 
        {
            object select = cmb.SelectedItem;
            if (select.GetType() == typeof(ComboBoxItem))
            {
                ComboBoxItem kv = (ComboBoxItem)select;
                return kv.Id;
            }
            return null;
        }
        public static string GetComboBoxSelectValue(this ComboBox cmb)
        {
            object select = cmb.SelectedItem;
            if (select.GetType() == typeof(ComboBoxItem))
            {
                ComboBoxItem kv = (ComboBoxItem)select;
                return kv.Value;
            }
            return null;
        }
        public static void Text_KeyPress(object sender, KeyPressEventArgs e)
        { // only input digit
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar == (char)46||e.KeyChar==(char)44)
            {//only input   int[ 44 =,]
                e.Handled = true;
            }
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
        public static void SetDatetimePickerTimeMinuteFormat( this DateTimePicker dtp) 
        {
            dtp.CustomFormat = SystemConfig.DateHourMinute;
            dtp.Format = DateTimePickerFormat.Custom;
        }
        public static void GridViewCellNameLog(this DataGridViewCellCollection cells) 
        {//将列值名称输出到日志中进行对比查找
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DataGridView=" + cells[0].DataGridView.Name);
            foreach (DataGridViewCell item in cells)
            {
                sb.AppendLine("Name=" + item.OwningColumn.Name + ",DataPropertyName=" + item.OwningColumn.DataPropertyName);
            }
            sb.ToString().Logger();
        }
        public static void SetDataGridViewTextBoxColumn(this DataGridViewTextBoxColumn cell,string nameExt,string mapColumn,string headText)
        {
            cell.DataPropertyName = mapColumn;
            cell.Name = nameExt;
            cell.HeaderText = headText;
        }
    }
    /// <summary>
    /// 窗体间进行参数回调的实体
    /// </summary>
    public class CallBackEventParam 
    {
        public Form NowForm { get; set; }//提供参数的窗体对象
        public int NowFormHandler { get; set; }
        public object CallData { get; set; }
    }
}
