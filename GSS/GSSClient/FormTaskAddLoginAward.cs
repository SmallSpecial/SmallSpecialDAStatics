using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSModel.Response;
using GSSModel.Request;
using GSSUI;
using GSSModel;
using LanguageItems;
using GSS.DBUtility;
namespace GSSClient
{
    public class FormTaskAddLoginAward : GSSUI.AForm.FormMain
    {
        List<GSSModel.Response.GameConfig> zoneGameconfig = new List<GSSModel.Response.GameConfig>();
        string timeFormat = SystemConfig.DateTimeFormat;
        string workOrder;
        public int workOrderType = 0;
        private System.Windows.Forms.TextBox txtMinLevel;
        private System.Windows.Forms.RichTextBox rtbRemark;
        private System.Windows.Forms.Label LblMinLevel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label LblMaxLevel;
        private System.Windows.Forms.Label LblAwardID;
        private System.Windows.Forms.Label LblRemark;
        private System.Windows.Forms.ComboBox cmbZone;
        private System.Windows.Forms.Label LblZone;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label LblStart;
        private System.Windows.Forms.Label LblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TextBox txtAwardID;
        private System.Windows.Forms.Button btnCancel;
        private Label lblBigZone;
        private ComboBox cmbBigZone;
        private TextBox txtActiveName;
        private Label LblActiveName;
        private System.Windows.Forms.TextBox txtMaxLevel;
        private Label lblActiveText;
        private RichTextBox rtbActiveTex;
        private Label lblEmailSendBy;
        private TextBox txtSendBy;
        private Label lblBlueDiamond;
        private TextBox txtBlueDiamond;
        private Label lblMoney;
        private TextBox txtMoney;
        private Label lbItem1;
        private Label lbItemNum1;
        private Label lbItem2;
        private Label lbItemNum2;
        private Label lbItem3;
        private Label lbItemNum5;
        private Label lbItem5;
        private Label lbItemNum4;
        private Label lbItem4;
        private Label lbItemNum3;
        private TextBox tbItem1;
        private TextBox tbItemNum1;
        private TextBox tbItem2;
        private TextBox tbItemNum2;
        private TextBox tbItem3;
        private TextBox tbItemNum3;
        private TextBox tbItem4;
        private TextBox tbItemNum4;
        private TextBox tbItem5;
        private TextBox tbItemNum5;
        private ClientHandles _clienthandle;
        public FormTaskAddLoginAward(ClientHandles clienthandle, string title, int workorderTypeID)
        {
            InitializeComponent();
            _clienthandle = clienthandle;
            this.Text = title;
            workOrder = title;
            workOrderType = workorderTypeID;
            this.dtpStart.CustomFormat = timeFormat;
            this.dtpStart.Format = DateTimePickerFormat.Custom;
            this.dtpEnd.CustomFormat = timeFormat;
            dtpEnd.Format = DateTimePickerFormat.Custom;
            InitElement();
            InitLanguageText();
            InitElementData();
        }
        void InitLanguageText()
        {
            this.btnSave.Text = global::GSSClient.LanguageResource.Language.LblSave;
            this.LblMaxLevel.Text = LanguageResource.Language.LblMaxLevel;
            this.LblZone.Text = LanguageResource.Language.LblZone;
            this.btnCancel.Text = global::GSSClient.LanguageResource.Language.BtnCancel;
            this.LblAwardID.Text = LanguageResource.Language.LblAwardID;
            this.LblMinLevel.Text = LanguageResource.Language.LblMinLevel;
            this.LblRemark.Text = LanguageResource.Language.LblRemark;
            lblBigZone.Text = LanguageResource.Language.LblBigZone;
            LblStart.Text = LanguageResource.Language.LblStartTime;
            LblEnd.Text = LanguageResource.Language.LblEndTime;
            this.LblActiveName.Text = LanguageResource.Language.LblActiveName;
            this.StartPosition = FormStartPosition.Manual;
            int x = SystemInformation.PrimaryMonitorSize.Width;
            int y = SystemInformation.PrimaryMonitorSize.Height;
            this.Location = new System.Drawing.Point(x / 2 - 108, y / 2 - 108);
            lblEmailSendBy.Text = BaseLanguageItem.LblSendBy;
            lblActiveText.Text = BaseLanguageItem.LblEmailInfo;
            lblMoney.Text = BaseLanguageItem.LblRedDiamond;
            lblBlueDiamond.Text = BaseLanguageItem.LblBlueDiamond;


            lbItem1.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp1;
            lbItem2.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp2;
            lbItem3.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp3;
            lbItem4.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp4;
            lbItem5.Text = LanguageItems.BaseLanguageItem.Lbl_AwardProp5;
            lbItemNum1.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum1;
            lbItemNum2.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum2;
            lbItemNum3.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum3;
            lbItemNum4.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum4;
            lbItemNum5.Text = LanguageItems.BaseLanguageItem.Lbl_AwardPropNum5;

        }
        void InitElement()
        {
            rtbActiveTex.MaxLength = 200;
            rtbRemark.MaxLength = 100;
            txtActiveName.MaxLength = 40;
            txtSendBy.MaxLength = 20;
        }
        #region draw ui
        private void InitializeComponent()
        {
            this.txtMinLevel = new System.Windows.Forms.TextBox();
            this.rtbRemark = new System.Windows.Forms.RichTextBox();
            this.LblMinLevel = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.LblMaxLevel = new System.Windows.Forms.Label();
            this.LblAwardID = new System.Windows.Forms.Label();
            this.LblRemark = new System.Windows.Forms.Label();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.LblZone = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.LblStart = new System.Windows.Forms.Label();
            this.LblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txtAwardID = new System.Windows.Forms.TextBox();
            this.txtMaxLevel = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblBigZone = new System.Windows.Forms.Label();
            this.cmbBigZone = new System.Windows.Forms.ComboBox();
            this.txtActiveName = new System.Windows.Forms.TextBox();
            this.LblActiveName = new System.Windows.Forms.Label();
            this.lblActiveText = new System.Windows.Forms.Label();
            this.rtbActiveTex = new System.Windows.Forms.RichTextBox();
            this.lblEmailSendBy = new System.Windows.Forms.Label();
            this.txtSendBy = new System.Windows.Forms.TextBox();
            this.lblBlueDiamond = new System.Windows.Forms.Label();
            this.txtBlueDiamond = new System.Windows.Forms.TextBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.lbItem1 = new System.Windows.Forms.Label();
            this.lbItemNum1 = new System.Windows.Forms.Label();
            this.lbItem2 = new System.Windows.Forms.Label();
            this.lbItemNum2 = new System.Windows.Forms.Label();
            this.lbItem3 = new System.Windows.Forms.Label();
            this.lbItemNum5 = new System.Windows.Forms.Label();
            this.lbItem5 = new System.Windows.Forms.Label();
            this.lbItemNum4 = new System.Windows.Forms.Label();
            this.lbItem4 = new System.Windows.Forms.Label();
            this.lbItemNum3 = new System.Windows.Forms.Label();
            this.tbItem1 = new System.Windows.Forms.TextBox();
            this.tbItemNum1 = new System.Windows.Forms.TextBox();
            this.tbItem2 = new System.Windows.Forms.TextBox();
            this.tbItemNum2 = new System.Windows.Forms.TextBox();
            this.tbItem3 = new System.Windows.Forms.TextBox();
            this.tbItemNum3 = new System.Windows.Forms.TextBox();
            this.tbItem4 = new System.Windows.Forms.TextBox();
            this.tbItemNum4 = new System.Windows.Forms.TextBox();
            this.tbItem5 = new System.Windows.Forms.TextBox();
            this.tbItemNum5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMinLevel
            // 
            this.txtMinLevel.Location = new System.Drawing.Point(115, 108);
            this.txtMinLevel.Name = "txtMinLevel";
            this.txtMinLevel.Size = new System.Drawing.Size(101, 21);
            this.txtMinLevel.TabIndex = 2223336;
            // 
            // rtbRemark
            // 
            this.rtbRemark.Location = new System.Drawing.Point(115, 381);
            this.rtbRemark.Name = "rtbRemark";
            this.rtbRemark.Size = new System.Drawing.Size(302, 81);
            this.rtbRemark.TabIndex = 2223337;
            this.rtbRemark.Text = "";
            // 
            // LblMinLevel
            // 
            this.LblMinLevel.AutoSize = true;
            this.LblMinLevel.BackColor = System.Drawing.Color.White;
            this.LblMinLevel.Location = new System.Drawing.Point(24, 108);
            this.LblMinLevel.Name = "LblMinLevel";
            this.LblMinLevel.Size = new System.Drawing.Size(53, 12);
            this.LblMinLevel.TabIndex = 2223338;
            this.LblMinLevel.Text = "最低等级";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(112, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2223339;
            this.btnSave.Text = global::GSSClient.LanguageResource.Language.LblSave;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LblMaxLevel
            // 
            this.LblMaxLevel.AutoSize = true;
            this.LblMaxLevel.BackColor = System.Drawing.Color.White;
            this.LblMaxLevel.Location = new System.Drawing.Point(236, 111);
            this.LblMaxLevel.Name = "LblMaxLevel";
            this.LblMaxLevel.Size = new System.Drawing.Size(53, 12);
            this.LblMaxLevel.TabIndex = 2223343;
            this.LblMaxLevel.Text = "最高等级";
            // 
            // LblAwardID
            // 
            this.LblAwardID.AutoSize = true;
            this.LblAwardID.BackColor = System.Drawing.Color.White;
            this.LblAwardID.Location = new System.Drawing.Point(338, 65);
            this.LblAwardID.Name = "LblAwardID";
            this.LblAwardID.Size = new System.Drawing.Size(77, 12);
            this.LblAwardID.TabIndex = 2223344;
            this.LblAwardID.Text = "附加道具设定";
            // 
            // LblRemark
            // 
            this.LblRemark.AutoSize = true;
            this.LblRemark.BackColor = System.Drawing.Color.White;
            this.LblRemark.Location = new System.Drawing.Point(24, 415);
            this.LblRemark.Name = "LblRemark";
            this.LblRemark.Size = new System.Drawing.Size(29, 12);
            this.LblRemark.TabIndex = 2223346;
            this.LblRemark.Text = "备注";
            // 
            // cmbZone
            // 
            this.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.Location = new System.Drawing.Point(319, 144);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(104, 20);
            this.cmbZone.TabIndex = 2223347;
            // 
            // LblZone
            // 
            this.LblZone.AutoSize = true;
            this.LblZone.BackColor = System.Drawing.Color.White;
            this.LblZone.Location = new System.Drawing.Point(236, 144);
            this.LblZone.Name = "LblZone";
            this.LblZone.Size = new System.Drawing.Size(29, 12);
            this.LblZone.TabIndex = 2223348;
            this.LblZone.Text = "战区";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(115, 195);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(302, 21);
            this.dtpStart.TabIndex = 2223349;
            // 
            // LblStart
            // 
            this.LblStart.AutoSize = true;
            this.LblStart.BackColor = System.Drawing.Color.White;
            this.LblStart.Location = new System.Drawing.Point(24, 204);
            this.LblStart.Name = "LblStart";
            this.LblStart.Size = new System.Drawing.Size(53, 12);
            this.LblStart.TabIndex = 2223350;
            this.LblStart.Text = "开始时间";
            // 
            // LblEnd
            // 
            this.LblEnd.AutoSize = true;
            this.LblEnd.BackColor = System.Drawing.Color.White;
            this.LblEnd.Location = new System.Drawing.Point(24, 250);
            this.LblEnd.Name = "LblEnd";
            this.LblEnd.Size = new System.Drawing.Size(53, 12);
            this.LblEnd.TabIndex = 2223352;
            this.LblEnd.Text = "结束时间";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(115, 241);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(302, 21);
            this.dtpEnd.TabIndex = 2223351;
            // 
            // txtAwardID
            // 
            this.txtAwardID.Location = new System.Drawing.Point(309, 62);
            this.txtAwardID.Name = "txtAwardID";
            this.txtAwardID.ReadOnly = true;
            this.txtAwardID.Size = new System.Drawing.Size(23, 21);
            this.txtAwardID.TabIndex = 2223353;
            this.txtAwardID.Visible = false;
            // 
            // txtMaxLevel
            // 
            this.txtMaxLevel.Location = new System.Drawing.Point(319, 111);
            this.txtMaxLevel.Name = "txtMaxLevel";
            this.txtMaxLevel.Size = new System.Drawing.Size(98, 21);
            this.txtMaxLevel.TabIndex = 2223354;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(342, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2223355;
            this.btnCancel.Text = global::GSSClient.LanguageResource.Language.BtnCancel;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblBigZone
            // 
            this.lblBigZone.AutoSize = true;
            this.lblBigZone.BackColor = System.Drawing.Color.White;
            this.lblBigZone.Location = new System.Drawing.Point(24, 144);
            this.lblBigZone.Name = "lblBigZone";
            this.lblBigZone.Size = new System.Drawing.Size(29, 12);
            this.lblBigZone.TabIndex = 2223357;
            this.lblBigZone.Text = "大区";
            // 
            // cmbBigZone
            // 
            this.cmbBigZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBigZone.FormattingEnabled = true;
            this.cmbBigZone.Location = new System.Drawing.Point(115, 144);
            this.cmbBigZone.Name = "cmbBigZone";
            this.cmbBigZone.Size = new System.Drawing.Size(101, 20);
            this.cmbBigZone.TabIndex = 2223356;
            this.cmbBigZone.SelectedIndexChanged += new System.EventHandler(this.cmbBigZone_SelectedIndexChanged);
            // 
            // txtActiveName
            // 
            this.txtActiveName.Location = new System.Drawing.Point(118, 59);
            this.txtActiveName.Name = "txtActiveName";
            this.txtActiveName.Size = new System.Drawing.Size(98, 21);
            this.txtActiveName.TabIndex = 2223359;
            // 
            // LblActiveName
            // 
            this.LblActiveName.AutoSize = true;
            this.LblActiveName.BackColor = System.Drawing.Color.White;
            this.LblActiveName.Location = new System.Drawing.Point(24, 62);
            this.LblActiveName.Name = "LblActiveName";
            this.LblActiveName.Size = new System.Drawing.Size(53, 12);
            this.LblActiveName.TabIndex = 2223358;
            this.LblActiveName.Text = "活动名称";
            // 
            // lblActiveText
            // 
            this.lblActiveText.AutoSize = true;
            this.lblActiveText.BackColor = System.Drawing.Color.White;
            this.lblActiveText.Location = new System.Drawing.Point(22, 304);
            this.lblActiveText.Name = "lblActiveText";
            this.lblActiveText.Size = new System.Drawing.Size(65, 12);
            this.lblActiveText.TabIndex = 2223361;
            this.lblActiveText.Text = "email body";
            // 
            // rtbActiveTex
            // 
            this.rtbActiveTex.Location = new System.Drawing.Point(113, 270);
            this.rtbActiveTex.Name = "rtbActiveTex";
            this.rtbActiveTex.Size = new System.Drawing.Size(302, 81);
            this.rtbActiveTex.TabIndex = 2223360;
            this.rtbActiveTex.Text = "";
            // 
            // lblEmailSendBy
            // 
            this.lblEmailSendBy.AutoSize = true;
            this.lblEmailSendBy.BackColor = System.Drawing.Color.White;
            this.lblEmailSendBy.Location = new System.Drawing.Point(24, 358);
            this.lblEmailSendBy.Name = "lblEmailSendBy";
            this.lblEmailSendBy.Size = new System.Drawing.Size(89, 12);
            this.lblEmailSendBy.TabIndex = 2223362;
            this.lblEmailSendBy.Text = "email  send by";
            // 
            // txtSendBy
            // 
            this.txtSendBy.Location = new System.Drawing.Point(115, 354);
            this.txtSendBy.Name = "txtSendBy";
            this.txtSendBy.Size = new System.Drawing.Size(300, 21);
            this.txtSendBy.TabIndex = 2223363;
            // 
            // lblBlueDiamond
            // 
            this.lblBlueDiamond.AutoSize = true;
            this.lblBlueDiamond.BackColor = System.Drawing.Color.White;
            this.lblBlueDiamond.Location = new System.Drawing.Point(24, 176);
            this.lblBlueDiamond.Name = "lblBlueDiamond";
            this.lblBlueDiamond.Size = new System.Drawing.Size(77, 12);
            this.lblBlueDiamond.TabIndex = 2223364;
            this.lblBlueDiamond.Text = "blue diamond";
            // 
            // txtBlueDiamond
            // 
            this.txtBlueDiamond.Location = new System.Drawing.Point(115, 168);
            this.txtBlueDiamond.Name = "txtBlueDiamond";
            this.txtBlueDiamond.Size = new System.Drawing.Size(101, 21);
            this.txtBlueDiamond.TabIndex = 2223365;
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.BackColor = System.Drawing.Color.White;
            this.lblMoney.Location = new System.Drawing.Point(236, 171);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(35, 12);
            this.lblMoney.TabIndex = 2223366;
            this.lblMoney.Text = "money";
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(321, 170);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(101, 21);
            this.txtMoney.TabIndex = 2223367;
            // 
            // lbItem1
            // 
            this.lbItem1.AutoSize = true;
            this.lbItem1.BackColor = System.Drawing.Color.White;
            this.lbItem1.Location = new System.Drawing.Point(433, 65);
            this.lbItem1.Name = "lbItem1";
            this.lbItem1.Size = new System.Drawing.Size(59, 12);
            this.lbItem1.TabIndex = 2223368;
            this.lbItem1.Text = "附加道具1";
            // 
            // lbItemNum1
            // 
            this.lbItemNum1.AutoSize = true;
            this.lbItemNum1.BackColor = System.Drawing.Color.White;
            this.lbItemNum1.Location = new System.Drawing.Point(433, 108);
            this.lbItemNum1.Name = "lbItemNum1";
            this.lbItemNum1.Size = new System.Drawing.Size(59, 12);
            this.lbItemNum1.TabIndex = 2223369;
            this.lbItemNum1.Text = "道具1个数";
            // 
            // lbItem2
            // 
            this.lbItem2.AutoSize = true;
            this.lbItem2.BackColor = System.Drawing.Color.White;
            this.lbItem2.Location = new System.Drawing.Point(433, 152);
            this.lbItem2.Name = "lbItem2";
            this.lbItem2.Size = new System.Drawing.Size(59, 12);
            this.lbItem2.TabIndex = 2223368;
            this.lbItem2.Text = "附加道具2";
            // 
            // lbItemNum2
            // 
            this.lbItemNum2.AutoSize = true;
            this.lbItemNum2.BackColor = System.Drawing.Color.White;
            this.lbItemNum2.Location = new System.Drawing.Point(433, 195);
            this.lbItemNum2.Name = "lbItemNum2";
            this.lbItemNum2.Size = new System.Drawing.Size(59, 12);
            this.lbItemNum2.TabIndex = 2223369;
            this.lbItemNum2.Text = "道具2个数";
            // 
            // lbItem3
            // 
            this.lbItem3.AutoSize = true;
            this.lbItem3.BackColor = System.Drawing.Color.White;
            this.lbItem3.Location = new System.Drawing.Point(433, 241);
            this.lbItem3.Name = "lbItem3";
            this.lbItem3.Size = new System.Drawing.Size(59, 12);
            this.lbItem3.TabIndex = 2223370;
            this.lbItem3.Text = "附加道具3";
            // 
            // lbItemNum5
            // 
            this.lbItemNum5.AutoSize = true;
            this.lbItemNum5.BackColor = System.Drawing.Color.White;
            this.lbItemNum5.Location = new System.Drawing.Point(433, 457);
            this.lbItemNum5.Name = "lbItemNum5";
            this.lbItemNum5.Size = new System.Drawing.Size(59, 12);
            this.lbItemNum5.TabIndex = 2223375;
            this.lbItemNum5.Text = "道具5个数";
            // 
            // lbItem5
            // 
            this.lbItem5.AutoSize = true;
            this.lbItem5.BackColor = System.Drawing.Color.White;
            this.lbItem5.Location = new System.Drawing.Point(433, 411);
            this.lbItem5.Name = "lbItem5";
            this.lbItem5.Size = new System.Drawing.Size(59, 12);
            this.lbItem5.TabIndex = 2223373;
            this.lbItem5.Text = "附加道具5";
            // 
            // lbItemNum4
            // 
            this.lbItemNum4.AutoSize = true;
            this.lbItemNum4.BackColor = System.Drawing.Color.White;
            this.lbItemNum4.Location = new System.Drawing.Point(433, 368);
            this.lbItemNum4.Name = "lbItemNum4";
            this.lbItemNum4.Size = new System.Drawing.Size(59, 12);
            this.lbItemNum4.TabIndex = 2223371;
            this.lbItemNum4.Text = "道具4个数";
            // 
            // lbItem4
            // 
            this.lbItem4.AutoSize = true;
            this.lbItem4.BackColor = System.Drawing.Color.White;
            this.lbItem4.Location = new System.Drawing.Point(433, 324);
            this.lbItem4.Name = "lbItem4";
            this.lbItem4.Size = new System.Drawing.Size(59, 12);
            this.lbItem4.TabIndex = 2223374;
            this.lbItem4.Text = "附加道具4";
            // 
            // lbItemNum3
            // 
            this.lbItemNum3.AutoSize = true;
            this.lbItemNum3.BackColor = System.Drawing.Color.White;
            this.lbItemNum3.Location = new System.Drawing.Point(433, 281);
            this.lbItemNum3.Name = "lbItemNum3";
            this.lbItemNum3.Size = new System.Drawing.Size(59, 12);
            this.lbItemNum3.TabIndex = 2223372;
            this.lbItemNum3.Text = "道具3个数";
            // 
            // tbItem1
            // 
            this.tbItem1.Location = new System.Drawing.Point(529, 65);
            this.tbItem1.Name = "tbItem1";
            this.tbItem1.Size = new System.Drawing.Size(61, 21);
            this.tbItem1.TabIndex = 2223376;
            // 
            // tbItemNum1
            // 
            this.tbItemNum1.Location = new System.Drawing.Point(529, 102);
            this.tbItemNum1.Name = "tbItemNum1";
            this.tbItemNum1.Size = new System.Drawing.Size(61, 21);
            this.tbItemNum1.TabIndex = 2223377;
            // 
            // tbItem2
            // 
            this.tbItem2.Location = new System.Drawing.Point(529, 149);
            this.tbItem2.Name = "tbItem2";
            this.tbItem2.Size = new System.Drawing.Size(61, 21);
            this.tbItem2.TabIndex = 2223378;
            // 
            // tbItemNum2
            // 
            this.tbItemNum2.Location = new System.Drawing.Point(529, 192);
            this.tbItemNum2.Name = "tbItemNum2";
            this.tbItemNum2.Size = new System.Drawing.Size(61, 21);
            this.tbItemNum2.TabIndex = 2223379;
            // 
            // tbItem3
            // 
            this.tbItem3.Location = new System.Drawing.Point(529, 232);
            this.tbItem3.Name = "tbItem3";
            this.tbItem3.Size = new System.Drawing.Size(61, 21);
            this.tbItem3.TabIndex = 2223380;
            // 
            // tbItemNum3
            // 
            this.tbItemNum3.Location = new System.Drawing.Point(529, 278);
            this.tbItemNum3.Name = "tbItemNum3";
            this.tbItemNum3.Size = new System.Drawing.Size(61, 21);
            this.tbItemNum3.TabIndex = 2223381;
            // 
            // tbItem4
            // 
            this.tbItem4.Location = new System.Drawing.Point(529, 321);
            this.tbItem4.Name = "tbItem4";
            this.tbItem4.Size = new System.Drawing.Size(61, 21);
            this.tbItem4.TabIndex = 2223382;
            // 
            // tbItemNum4
            // 
            this.tbItemNum4.Location = new System.Drawing.Point(529, 365);
            this.tbItemNum4.Name = "tbItemNum4";
            this.tbItemNum4.Size = new System.Drawing.Size(61, 21);
            this.tbItemNum4.TabIndex = 2223383;
            // 
            // tbItem5
            // 
            this.tbItem5.Location = new System.Drawing.Point(529, 406);
            this.tbItem5.Name = "tbItem5";
            this.tbItem5.Size = new System.Drawing.Size(61, 21);
            this.tbItem5.TabIndex = 2223384;
            // 
            // tbItemNum5
            // 
            this.tbItemNum5.Location = new System.Drawing.Point(529, 448);
            this.tbItemNum5.Name = "tbItemNum5";
            this.tbItemNum5.Size = new System.Drawing.Size(61, 21);
            this.tbItemNum5.TabIndex = 2223385;
            // 
            // FormTaskAddLoginAward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(628, 513);
            this.Controls.Add(this.tbItemNum5);
            this.Controls.Add(this.tbItem5);
            this.Controls.Add(this.tbItemNum4);
            this.Controls.Add(this.tbItem4);
            this.Controls.Add(this.tbItemNum3);
            this.Controls.Add(this.tbItem3);
            this.Controls.Add(this.tbItemNum2);
            this.Controls.Add(this.tbItem2);
            this.Controls.Add(this.tbItemNum1);
            this.Controls.Add(this.tbItem1);
            this.Controls.Add(this.lbItemNum5);
            this.Controls.Add(this.lbItem5);
            this.Controls.Add(this.lbItemNum4);
            this.Controls.Add(this.lbItem4);
            this.Controls.Add(this.lbItemNum3);
            this.Controls.Add(this.lbItem3);
            this.Controls.Add(this.lbItemNum2);
            this.Controls.Add(this.lbItem2);
            this.Controls.Add(this.lbItemNum1);
            this.Controls.Add(this.lbItem1);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.txtBlueDiamond);
            this.Controls.Add(this.lblBlueDiamond);
            this.Controls.Add(this.txtSendBy);
            this.Controls.Add(this.lblEmailSendBy);
            this.Controls.Add(this.lblActiveText);
            this.Controls.Add(this.rtbActiveTex);
            this.Controls.Add(this.txtActiveName);
            this.Controls.Add(this.LblActiveName);
            this.Controls.Add(this.lblBigZone);
            this.Controls.Add(this.cmbBigZone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtMaxLevel);
            this.Controls.Add(this.txtAwardID);
            this.Controls.Add(this.LblEnd);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.LblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.LblZone);
            this.Controls.Add(this.cmbZone);
            this.Controls.Add(this.LblRemark);
            this.Controls.Add(this.LblAwardID);
            this.Controls.Add(this.LblMaxLevel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.LblMinLevel);
            this.Controls.Add(this.rtbRemark);
            this.Controls.Add(this.txtMinLevel);
            this.Name = "FormTaskAddLoginAward";
            this.Controls.SetChildIndex(this.txtMinLevel, 0);
            this.Controls.SetChildIndex(this.rtbRemark, 0);
            this.Controls.SetChildIndex(this.LblMinLevel, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.LblMaxLevel, 0);
            this.Controls.SetChildIndex(this.LblAwardID, 0);
            this.Controls.SetChildIndex(this.LblRemark, 0);
            this.Controls.SetChildIndex(this.cmbZone, 0);
            this.Controls.SetChildIndex(this.LblZone, 0);
            this.Controls.SetChildIndex(this.dtpStart, 0);
            this.Controls.SetChildIndex(this.LblStart, 0);
            this.Controls.SetChildIndex(this.dtpEnd, 0);
            this.Controls.SetChildIndex(this.LblEnd, 0);
            this.Controls.SetChildIndex(this.txtAwardID, 0);
            this.Controls.SetChildIndex(this.txtMaxLevel, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.cmbBigZone, 0);
            this.Controls.SetChildIndex(this.lblBigZone, 0);
            this.Controls.SetChildIndex(this.LblActiveName, 0);
            this.Controls.SetChildIndex(this.txtActiveName, 0);
            this.Controls.SetChildIndex(this.rtbActiveTex, 0);
            this.Controls.SetChildIndex(this.lblActiveText, 0);
            this.Controls.SetChildIndex(this.lblEmailSendBy, 0);
            this.Controls.SetChildIndex(this.txtSendBy, 0);
            this.Controls.SetChildIndex(this.lblBlueDiamond, 0);
            this.Controls.SetChildIndex(this.txtBlueDiamond, 0);
            this.Controls.SetChildIndex(this.lblMoney, 0);
            this.Controls.SetChildIndex(this.txtMoney, 0);
            this.Controls.SetChildIndex(this.lbItem1, 0);
            this.Controls.SetChildIndex(this.lbItemNum1, 0);
            this.Controls.SetChildIndex(this.lbItem2, 0);
            this.Controls.SetChildIndex(this.lbItemNum2, 0);
            this.Controls.SetChildIndex(this.lbItem3, 0);
            this.Controls.SetChildIndex(this.lbItemNum3, 0);
            this.Controls.SetChildIndex(this.lbItem4, 0);
            this.Controls.SetChildIndex(this.lbItemNum4, 0);
            this.Controls.SetChildIndex(this.lbItem5, 0);
            this.Controls.SetChildIndex(this.lbItemNum5, 0);
            this.Controls.SetChildIndex(this.tbItem1, 0);
            this.Controls.SetChildIndex(this.tbItemNum1, 0);
            this.Controls.SetChildIndex(this.tbItem2, 0);
            this.Controls.SetChildIndex(this.tbItemNum2, 0);
            this.Controls.SetChildIndex(this.tbItem3, 0);
            this.Controls.SetChildIndex(this.tbItemNum3, 0);
            this.Controls.SetChildIndex(this.tbItem4, 0);
            this.Controls.SetChildIndex(this.tbItemNum4, 0);
            this.Controls.SetChildIndex(this.tbItem5, 0);
            this.Controls.SetChildIndex(this.tbItemNum5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #region logic
        void InitElementData()
        {
            zoneGameconfig = ClientCache.GetGameZoneCache();
            //bind bigzone
            GSSModel.Response.GameConfig[] big = zoneGameconfig.Where(s => s.ParentId == SystemConfig.BigZoneParentId).ToArray();
            BindComboBox(big, cmbBigZone);
            txtMinLevel.MaxLength = 4;
            txtMaxLevel.MaxLength = 4;
            rtbRemark.MaxLength = 100;
            dtpEnd.Value = DateTime.Now.AddDays(1);
            txtAwardID.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtMinLevel.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtMaxLevel.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtMoney.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtBlueDiamond.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);

            //只能输入数字
            tbItem1.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem2.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem3.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem4.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItem5.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum1.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum2.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum3.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum4.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            tbItemNum5.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);


        }
        void BindComboBox(GSSModel.Response.GameConfig[] items, ComboBox cmb)
        {
            cmb.Items.Clear();
            cmb.Items.AddRange(items);
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "GameValue";
            if (cmb.Items.Count > 0)
            {
                cmb.SelectedIndex = 0;
            }
        }
        #endregion
        GSSModel.Response.GameConfig GetComboBoxSelectValue(ComboBox cmb)
        {
            GSSModel.Response.GameConfig config = cmb.SelectedItem as GSSModel.Response.GameConfig;
            return config;
        }
        private void cmbBigZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            GSSModel.Response.GameConfig gc = GetComboBoxSelectValue(sender as ComboBox);
            GSSModel.Response.GameConfig[] zones = zoneGameconfig.Where(z => z.ParentId == gc.Id).ToArray();
            List<GSSModel.Response.GameConfig> allZone = new List<GSSModel.Response.GameConfig>();
            allZone.Add(new GSSModel.Response.GameConfig() { GameValue = "-1", Name = LanguageResource.Language.LblAllZone });
            allZone.AddRange(zones);
            BindComboBox(allZone.ToArray(), cmbZone);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LoginAward award = new LoginAward();
            award.ActiveName = txtActiveName.Text;
            if (string.IsNullOrEmpty(award.ActiveName))
            {
                MsgBox.Show(string.Format(LanguageResource.Language.Tip_ItemIsRequiredFormat, LanguageResource.Language.LblActiveName),
                    LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int t;
            bool can = int.TryParse(txtMinLevel.Text, out t);
            award.MinLevel = t;
            can = can & int.TryParse(txtMaxLevel.Text, out t);
            award.MaxLevel = t;
            // can = can & int.TryParse(txtAwardID.Text, out t);
            //award.AwardID = t;

            #region 道具赋值
            StringBuilder sbItem = new StringBuilder();

            if (int.TryParse(tbItem1.Text, out t))
            {
                award.Item1 = t;
                sbItem.Append(t);

                if (int.TryParse(tbItemNum1.Text, out t))
                {
                    award.ItemNum1 = t; sbItem.Append("_" + t);
                }
                else
                {
                    award.ItemNum1 = 0; sbItem.Append("_" + 0);
                }
            }
            else
            {
                award.Item1 = 0; award.ItemNum1 = 0; sbItem.Append(0 + "_" + 0);
            }


            if (int.TryParse(tbItem2.Text, out t))
            {
                award.Item2 = t; sbItem.Append("_" + t);
                if (int.TryParse(tbItemNum2.Text, out t))
                {
                    award.ItemNum2 = t;
                    sbItem.Append("_" + t);
                }
                else
                {
                    award.ItemNum2 = 0; sbItem.Append("_" + 0);
                }
            }
            else
            {
                award.Item2 = 0; award.ItemNum2 = 0; sbItem.Append("_" + 0 + "_" + 0);
            }

            if (int.TryParse(tbItem3.Text, out t))
            {
                award.Item3 = t;
                sbItem.Append("_" + t);
                if (int.TryParse(tbItemNum3.Text, out t))
                {
                    award.ItemNum3 = t;
                    sbItem.Append("_" + t);
                }
                else
                {
                    award.ItemNum3 = 0; sbItem.Append("_" + 0);
                }
            }
            else
            {
                award.Item3 = 0; award.ItemNum3 = 0; sbItem.Append("_" + 0 + "_" + 0);
            }

            if (int.TryParse(tbItem4.Text, out t))
            {
                award.Item4 = t;
                sbItem.Append("_" + t);
                if (int.TryParse(tbItemNum4.Text, out t))
                {
                    award.ItemNum4 = t;
                    sbItem.Append("_" + t);
                }
                else
                {
                    award.ItemNum4 = 0; sbItem.Append("_" + 0);
                }
            }
            else
            {
                award.Item4 = 0; award.ItemNum4 = 0; sbItem.Append("_" + 0 + "_" + 0);
            }

            if (int.TryParse(tbItem5.Text, out t))
            {
                award.Item5 = t;
                sbItem.Append("_" + t);
                if (int.TryParse(tbItemNum5.Text, out t))
                {
                    award.ItemNum5 = t;
                    sbItem.Append("_" + t);
                }
                else
                {
                    award.ItemNum5 = 0; sbItem.Append("_" + 0);
                }
            }
            else
            {
                award.Item5 = 0; award.ItemNum5 = 0; sbItem.Append("_" + 0 + "_" + 0);
            }

            #endregion

            award.Remark = rtbRemark.Text;
            if (string.IsNullOrEmpty(award.Remark))
            {
                award.Remark = "Gss Create Login award";
            }
            award.SendBy = txtSendBy.Text;
            if (string.IsNullOrEmpty(award.SendBy))
            {
                can = false;
            }
            award.EmailBody = rtbActiveTex.Text;
            if (string.IsNullOrEmpty(award.EmailBody))
            {
                can = false;
            }
            if (!can)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format(LanguageResource.Language.Tip_ItemIsRequiredFormat, LanguageResource.Language.LblMinLevel));
                sb.AppendLine(string.Format(LanguageResource.Language.Tip_ItemIsRequiredFormat, LanguageResource.Language.LblMaxLevel));
                sb.AppendLine(string.Format(LanguageResource.Language.Tip_ItemIsRequiredFormat, BaseLanguageItem.LblSendBy));
                sb.AppendLine(string.Format(LanguageResource.Language.Tip_ItemIsRequiredFormat, BaseLanguageItem.LblEmailInfo));
                MsgBox.Show(sb.ToString(), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GSSModel.Response.GameConfig big = GetComboBoxSelectValue(cmbBigZone);
            if (big == null)
            {
                MsgBox.Show(LanguageResource.Language.Tip_LossLogicData, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            can = can & int.TryParse(big.GameValue, out t);
            award.BigZoneID = t;
            big = GetComboBoxSelectValue(cmbZone);
            string bigZone = big.Name;
            can = can & int.TryParse(big.GameValue, out t);
            string zone = big.Name;
            award.ZoneID = t;
            if (!can)
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSelectBigZoneAndZone, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            award.StartTime = dtpStart.Value;
            award.EndTime = dtpEnd.Value;
            TimeSpan span = award.StartTime - award.EndTime;
            int min = (int)span.TotalMinutes;
            if (min >= 0)
            {//比较大小忽略秒级别（毫秒以及秒的差距忽略不计）
                MsgBox.Show(LanguageResource.Language.Tip_StartTimeShouldLessEndTime, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (award.MinLevel > award.MaxLevel)
            {
                MsgBox.Show(LanguageResource.Language.Tip_MinLevelShouldLessMaxLevel, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string temp = txtMoney.Text;
            int inttemp = 0;
            if (int.TryParse(temp, out inttemp))
            {
                award.Money = inttemp;
            }
            else
            {
                award.Money = 0;
            }
            temp = txtBlueDiamond.Text;
            if (int.TryParse(temp, out inttemp))
            {
                award.BlueDiamond = inttemp;
            }
            else
            {
                award.BlueDiamond = 0;
            }

            Tasks task = new Tasks();
            StringBuilder note = new StringBuilder();
            note.Append(LanguageResource.Language.LblWorkOrderType + ":" + workOrder);
            note.Append(LanguageResource.Language.LblDetail + "&\r\n");
            note.Append(LanguageResource.Language.LblActiveName + ":" + award.ActiveName + "&");
            note.Append(LanguageResource.Language.LblAwardID + ":" + sbItem.ToString() + "&");
            note.Append(BaseLanguageItem.LblBlueDiamond + ":" + award.BlueDiamond + "&");
            note.Append(BaseLanguageItem.LblMoney + ":" + award.Money + "&");
            note.Append(LanguageResource.Language.LblMinLevel + ":" + award.MinLevel + "&" + LanguageResource.Language.LblMaxLevel + ":" + award.MaxLevel + "&");
            note.Append(LanguageResource.Language.LblStartTime + ":" + award.StartTime.ToString(SystemConfig.DateTimeFormat) + "&"
                + LanguageResource.Language.LblEndTime + ":" + award.EndTime.ToString(SystemConfig.DateTimeFormat) + "&");
            task.F_Note = note.ToString();
            task.F_Title = award.ActiveName;
            task.F_From = SystemConfig.AppID;
            task.F_Type = workOrderType;
            task.F_GameName = SystemConfig.GameID;//游戏ID号
            task.F_GameBigZone = bigZone;
            task.F_GameBigZone = award.ZoneID.ToString();
            task.F_State = WorkOrderStatueEnum.Recovery.GetHashCode();//等待处理
            task.F_CreatMan = int.Parse(ShareData.UserID);//工单创建人
            task.F_CreatTime = DateTime.Now;
            task.F_GameZone = zone;
            LoginAwardTask awardTask = new LoginAwardTask()
            {
                Task = task,
                Award = award
            };
            "Will create login award".Logger();
            awardTask.ConvertJson().Logger();
            //提交工单>写入奖励数据到MySQL
            _clienthandle.AddLoginAward(this.Handle.ToInt32(), awardTask);//provider  the  form  of ID，call back show  msg
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {//save data response
                case 601:
                    this.Activate();
                    string index = m.WParam.ToString();
                    int ind = int.Parse(index) - 1;
                    object obj = ShareData.Msg[ind];
                    ShareData.Msg.RemoveAt(ind);
                    GSSModel.Request.ClientData data = obj as GSSModel.Request.ClientData;
                    if (data.Success)
                    {
                        MsgBox.Show(string.Format(LanguageResource.Language.Tip_WorkOrderCreateSucc), LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MsgBox.Show(data.Message, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
