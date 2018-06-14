using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSSUI.AClass;

namespace GSSUI.AForm
{
    public partial class FormSkin : ABaseForm
    {
        public FormSkin()
        {
            InitializeComponent();
            InitLanguageText();
            colorAreaAndSliderUserControl1.setColor(SharData.BackColor);
            this.BackgroundImage = null;
            aPictrueBoxColor.NormalImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.TbAdjustColorNormal.png");
            aPictrueBoxShade.NormalImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.TbShadingNormal.png");
            aPictrueBoxColor.OverImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.TbAdjustColorPushed.png");
            aPictrueBoxShade.OverImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.TbShadingPushed.png");
            aPictrueBoxColor_Click(null, null);
            
        }
        void InitLanguageText() 
        {
            this.lblDefaultSkin.Text = LanguageResource.Language.LblDefaultSkin;
            this.lblMostTop.Text = LanguageResource.Language.BtnSetFormFirst;
            this.toolTip2.SetToolTip(this.aPictrueBoxColor, LanguageResource.Language.Tip_Color);
            this.toolTip2.SetToolTip(this.aPictrueBoxShade, LanguageResource.Language.Tip_Skin);
            this.toolTip2.SetToolTip(this.colorSliderUserControl1, LanguageResource.Language.Tip_ChangeFormOpacity);
        }
        private void colorAreaAndSliderUserControl1_ColorChanged(object sender, EventArgs e)
        {
            Color c;
            colorAreaAndSliderUserControl1.GetColor(out c);
            GSSUI.SharData.BackColor = c;
            GSSUI.SharData.BackImage = null;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = c;
                    f.BackgroundImage = null;
                    f.Invalidate(true);
                }

            }

        }
        protected override void OnCreateControl()
        {
            if (panel1.Visible)
            {
                aPictrueBoxShade.Image = aPictrueBoxShade.OverImg;
            }
            else
            {
                aPictrueBoxColor.Image = aPictrueBoxColor.OverImg;
            }
            colorSliderUserControl1.SetHueSaturation(190.1, 30, 85.3);
            colorSliderUserControl1.SetColorSaturation(GSSUI.SharData.Opacity);
            if (GSSUI.SharData.TopMost)
            {
                lblMostTop.Text = LanguageResource.Language.BtnCancelFormFirst;
            }
            else
            {
                lblMostTop.Text = LanguageResource.Language.BtnSetFormFirst;
            }
            base.OnCreateControl();
        }


        private void colorSliderUserControl1_ValueChangedByUser(object sender, EventArgs e)
        {
            double s;
            colorSliderUserControl1.GetHueSaturation(out s);

            if (s<10)
            {
                s = 10;
                colorSliderUserControl1.SetColorSaturation(s);

            }

            GSSUI.SharData.Opacity = s;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.Opacity = s / 100;
                    f.Invalidate(true);
                }

            }
            toolTip2.SetToolTip(colorSliderUserControl1, LanguageResource.Language.Tip_ChangeOpacityWithMin);
        }

        #region 切换选项卡
        private void aPictrueBoxShade_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            colorAreaAndSliderUserControl1.Visible = false;
        }

        private void aPictrueBoxColor_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            colorAreaAndSliderUserControl1.Visible = true;
        }

        private void aPictrueBoxShade_MouseLeave(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                aPictrueBoxShade.Image = aPictrueBoxShade.OverImg;
                aPictrueBoxColor.Image = aPictrueBoxColor.NormalImg;
            }
            else
            {
                aPictrueBoxShade.Image = aPictrueBoxShade.NormalImg;
                aPictrueBoxColor.Image = aPictrueBoxColor.OverImg;
            }
        }
        #endregion

        #region 换肤相关事件
        private void PicB1_Click(object sender, EventArgs e)
        {
            GSSUI.SharData.BackColor = Color.FromArgb(12, 130, 175);
            Bitmap bg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.wallpaper_bliss.jpg");
            GSSUI.SharData.BackImage = bg;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }

        private void PicB2_Click(object sender, EventArgs e)
        {
            GSSUI.SharData.BackColor = Color.FromArgb(12, 130, 175);
            Bitmap bg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.wallpaper_blue_02.jpg");
            GSSUI.SharData.BackImage = bg;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }

        private void PicB3_Click(object sender, EventArgs e)
        {
            GSSUI.SharData.BackColor = Color.FromArgb(12, 130, 175);
            Bitmap bg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.wallpaper_gray_01.jpg");
            GSSUI.SharData.BackImage = bg;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }

        private void PicB4_Click(object sender, EventArgs e)
        {
            GSSUI.SharData.BackColor = Color.FromArgb(12, 130, 175);
            Bitmap bg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.wallpaper_green_01.jpg");
            GSSUI.SharData.BackImage = bg;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }

        private void PicB5_Click(object sender, EventArgs e)
        {
            GSSUI.SharData.BackColor = Color.FromArgb(12, 130, 175);
            Bitmap bg = ImageObject.GetResBitmap("GSSUI.ASkinImg.Shade.wallpaper_black_01.jpg");
            GSSUI.SharData.BackImage = bg;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            SetTopMost();
        }

        /// <summary>
        /// 设置窗体最前
        /// </summary>
        /// <param name="value"></param>
        private void SetTopMost()
        {
            if (this.Owner != null)
            {
                GSSUI.SharData.TopMost = !this.Owner.TopMost;
                //GSSUI.SharData.SetUIInfo();

                this.Owner.TopMost = !this.Owner.TopMost;
                
                if (this.Owner.TopMost)
                {
                    lblMostTop.Text = LanguageResource.Language.BtnCancelFormFirst;
                }
                else
                {
                    lblMostTop.Text = LanguageResource.Language.BtnSetFormFirst;
                }
            }
            else
            {
                lblMostTop.Visible = false;
                GSSUI.SharData.TopMost = false;
            }
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void FormSkin_FormClosed(object sender, FormClosedEventArgs e)
        {
            SharData.SetUIInfo();
        }
        /// <summary>
        /// 恢复默认皮肤
        /// </summary>
        private void lblDefaultSkin_Click(object sender, EventArgs e)
        {
            SharData.BackColor = Color.FromArgb(12, 130, 175);
            SharData.BackImage = null;
            SharData.Opacity = 100;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "FormSkin")
                {
                    f.BackColor = GSSUI.SharData.BackColor;
                    f.BackgroundImage = GSSUI.SharData.BackImage;
                    f.Invalidate(true);
                }
            }
        }

    }
}
