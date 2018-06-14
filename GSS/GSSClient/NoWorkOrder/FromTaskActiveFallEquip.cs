using GSSModel.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSSUI.AForm;
using GSSUI;
using GSSModel.Request;
using GSSCSFrameWork;
namespace GSSClient
{
    public partial class FromTaskActiveFallEquip : GSSUI.AForm.FormMain
    {
        ClientHandles Client;
        int WorkOrderTypeId;
        public FromTaskActiveFallEquip(ClientHandles client,int workOrderTypeId)
        {
            InitializeComponent();
            Client = client;
            WorkOrderTypeId = workOrderTypeId;
            InitLanguageText();
            InitElementData();
        }
        void InitElementData()
        {
            dtpEndTime.Value = DateTime.Now.AddDays(30);
            dtpStartTime.SetDatetimePickerTimeMinuteFormat();
            dtpEndTime.SetDatetimePickerTimeMinuteFormat();
            //联动绑定战区
            cmbBigZone.SelectedIndexChanged += new EventHandler(ComboBox_SelectIndexChanged);
            QueryCommboxDataSource(cmbBigZone, SystemConfig.BigZoneParentId.ToString());
            txtFallNumber.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtRoleMaxLevel.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtRoleMinLevel.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtNumerator.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtDenominator.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            txtEquipNo.KeyPress += new KeyPressEventHandler(ControllExt.Text_KeyPress);
            string[] items= Enum.GetNames(typeof(EBugbearType));
            int[] values=(int[]) Enum.GetValues(typeof(EBugbearType));
            System.Resources.ResourceManager rm = LanguageResource.Language.ResourceManager;
            List<CheckBox> falltypes = new List<CheckBox>();
            int width = gbFallType.Width;
            //每行最多显示几个选项
            int columns = width / 100;
            for (int i = 0; i < items.Length; i++)
            {
                CheckBox ck = new CheckBox();
                string ckFix = typeof(EBugbearType).Name;
                ck.Text = rm.GetString(ckFix + "_" + items[i]);
                ck.Name = ckFix+values[i];
                ck.Location = new Point(i * 100 + 5, 15 *(1+ i / columns));
                ck.Width = 100;
                falltypes.Add(ck);
            }
            gbFallType.Controls.AddRange(falltypes.ToArray());
            btnSave.Click += new EventHandler(Button_Click);
        }
        void InitLanguageText() 
        {
            lblActiveName.Text = LanguageResource.Language.LblActiveName;
            lblRoleLevelLimit.Text = LanguageResource.Language.LblLevelLimit;
            gbFallType.Text= LanguageResource.Language.LblFallType;
            lblFallNumber.Text = LanguageResource.Language.LblUnit_Number;
            lblChance.Text = LanguageResource.Language.LblFallPer;
            lblSceneNo.Text = LanguageResource.Language.LblSenceNo;
            gbZoneWithBelongBig.Text = LanguageResource.Language.LblBelongZoneWithBigZone;
            lblBigZone.Text = LanguageResource.Language.LblBigZone;
            lblZone.Text = LanguageResource.Language.LblZone;
            lblStartTime.Text = LanguageResource.Language.LblStartTime;
            lblEndTime.Text = LanguageResource.Language.LblEndTime;
            gbValidTime.Text = LanguageResource.Language.LblTimeLimit;
            gbActive.Text = LanguageResource.Language.LblBaseInfo;
            btnSave.Text = LanguageResource.Language.LblSave;
            lblEquipNo.Text = LanguageResource.Language.LblEquipNo;
            btnSaveAndConfig.Text = LanguageResource.Language.BtnSaveWorkOrderAndSyncConfig;
        }
        int GetSelectFallType() 
        {
            int type = 0;
            bool havaCheck = false;//是否存在项被选中
            foreach (Control item in gbFallType.Controls)
            {
                CheckBox ck = item as CheckBox;
                if (ck == null)
                {
                    continue;
                }
                string name = ck.Name.Replace(typeof(EBugbearType).Name, "");
                int index;
                int.TryParse(name, out index);
                havaCheck = havaCheck || ck.Checked;
                if (ck.Checked && index == EBugbearType.All.GetHashCode())
                {//商定数据库中不采用all的hashcode来代表全部的掉落类型
                    int[] all=(int[]) Enum.GetValues(typeof(EBugbearType));
                    return  all.Sum()-EBugbearType.All.GetHashCode();
                }
                else if (ck.Checked)
                {
                    type += index;
                }
            }
            if (!havaCheck)
            {//没有项被选中
                return -1;
            }
            return type;
        }
        void ComboBox_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Control linkage = ControllsReflect.GetElement((string)cmb.Tag, this.Controls);
            if (linkage == null)
            {
                return;
            }
            ComboBox linkEle = linkage as ComboBox;
            string pid = cmb.GetComboBoxSelectItemId();
            QueryCommboxDataSource(linkEle, pid);
        }
        void QueryCommboxDataSource(ComboBox cmb, string parentId)
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
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int falltype = GetSelectFallType();
            if (falltype == -1)
            { //没有选中掉落怪的类型
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSelectFallType, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ActiveFallGoodsData good = new ActiveFallGoodsData();
            good.FallType = falltype;
            good.ActiveName = rtbActiveName.Text;
            if (string.IsNullOrEmpty(good.ActiveName))
            {
                MsgBox.Show(LanguageResource.Language.Tip_ActiveNameIsRequire, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string temp = txtFallNumber.Text;
            if (string.IsNullOrEmpty(temp))
            {
                MsgBox.Show(LanguageResource.Language.Tip_NumberIsRequire, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.FallGoodNum= int.Parse(temp);
            temp = txtRoleMinLevel.Text;
            if (string.IsNullOrEmpty(temp))
            {
                MsgBox.Show(LanguageResource.Language.Tip_RoleLevelIsReqire, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            good.MinRoleLevel = int.Parse(temp);
            temp = txtRoleMaxLevel.Text;
            if (string.IsNullOrEmpty(temp))
            {
                MsgBox.Show(LanguageResource.Language.Tip_RoleLevelIsReqire, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.MaxRoleLevel = int.Parse(temp);
            if (good.MaxRoleLevel < good.MinRoleLevel)
            {
                MsgBox.Show(LanguageResource.Language.Tip_MinLevelShouldLessMaxLevel, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            temp = txtSceneNo.Text;
            if (string.IsNullOrEmpty(temp))
            {
                MsgBox.Show(LanguageResource.Language.Tip_SceneInputLimit, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            temp = txtEquipNo.Text;
            if (string.IsNullOrEmpty(temp))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSetEquip, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.EquipNo = int.Parse(temp);
            //检测输入的场景ID是否合法【 场景ID间使用","分隔】
            List<int> scentids = new List<int>();
            temp = txtSceneNo.Text;
            foreach (string item in temp.Split(','))
            {
                int sid;
                if (!int.TryParse(item, out sid))
                {
                    MsgBox.Show(LanguageResource.Language.Tip_SceneInputLimit, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                scentids.Add(sid);
            }
            good.SceneIds = scentids.ToArray();
            if (string.IsNullOrEmpty(txtDenominator.Text) || string.IsNullOrEmpty(txtNumerator.Text))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PRIsRequire, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.GoodsFallPRNumerator = int.Parse(txtNumerator.Text);
            good.GoodsFallPRDenominator = int.Parse(txtDenominator.Text);
            string big = cmbBigZone.GetComboBoxSelectValue();
            string zone = cmbZone.GetComboBoxSelectValue();
            if (string.IsNullOrEmpty(big) || string.IsNullOrEmpty(zone))
            {
                MsgBox.Show(LanguageResource.Language.Tip_PleaseSelectBigZoneAndZone, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.BigZoneName = cmbBigZone.SelectedText;
            good.BigZoneID = int.Parse(big);
            good.ZoneName = cmbZone.Text;
            good.ZoneID = int.Parse(zone);
            good.StartTime = dtpStartTime.Value;
            good.EndTime = dtpEndTime.Value;
            good.AppId = SystemConfig.AppID;
            good.TaskTypeID = WorkOrderTypeId;
            if (good.StartTime >= good.EndTime)
            {
                MsgBox.Show(LanguageResource.Language.Tip_StartTimeShouldLessEndTime, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            good.CreateBy =int.Parse( ShareData.UserID);
            if (btn.Name == btnSaveAndConfig.Name)
            {
                good.SyncConfig = true;
            }
            int formid = this.Handle.ToInt32();
            ClientData data = new ClientData() { FormID=formid, Data=good};
            Client.SendTextToService(data, msgCommand.ActiveFallGoods);
           
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == SystemConfig.BetweenFormChatMsgId)
            {//这是自定义的窗体键信息通讯 
                int msgid = m.WParam.ToInt32() - 1;
                GSSModel.Request.ClientData arr = ShareData.Msg[msgid] as GSSModel.Request.ClientData;
                if (arr.Success)
                {
                    this.Close();
                    MsgBox.Show(LanguageResource.Language.Tip_WorkOrderCreateSucc);
                    return;
                }
                string error = arr.Data as string;
                MsgBox.Show(error, LanguageResource.Language.Tip_Tip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            base.DefWndProc(ref m);
        }

    }
    enum EBugbearType
    { 
        All=0,//全部
        Normal=1,//普通
        Eliten=2,//精英
        Boss=4
    }
}
