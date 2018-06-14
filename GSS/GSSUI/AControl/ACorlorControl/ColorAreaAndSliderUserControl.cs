using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GSSUI.Colors;

namespace GSSUI.AControl.ACorlorControl
{
    public partial class ColorAreaAndSliderUserControl : UserControl
    {
        double h, l, s;
        public ColorAreaAndSliderUserControl()
        {
            InitializeComponent();
        }

        private void colorAreaUserControl1_HueSaturationChanged(object sender, EventArgs e)
        {
            
            colorAreaUserControl.GetHueSaturation(out h,out s, out l);
            colorSliderUserControl.SetHueSaturation(h, s, l);

        }

        private void colorAreaUserControl1_ValueChangedByUser(object sender, EventArgs e)
        {
            notifyColorChanged();
            notifyValueChangedByUser();
            toolTip1.SetToolTip(colorAreaUserControl, "调整颜色");
        }

        private void colorSliderUserControl_ValueChanged(object sender, EventArgs e)
        {
            double s;
            colorSliderUserControl.GetHueSaturation(out s);
            colorAreaUserControl.SetHueSaturation(s);

        }

        private void colorSliderUserControl_ValueChangedByUser(object sender, EventArgs e)
        {
            notifyColorChanged();
            notifyValueChangedByUser();
            toolTip1.SetToolTip(colorSliderUserControl, "调整明暗度");

        }

        /// <summary>
        /// 当用户发生改变颜色.
        /// </summary>
        public event EventHandler ColorChanged;

        /// <summary>
        /// 值时发生了改变.
        /// </summary>
        public event EventHandler ValueChangedByUser;

        private void notifyColorChanged()
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, EventArgs.Empty);
            }
        }

        private void notifyValueChangedByUser()
        {
            if (ValueChangedByUser != null)
            {
                ValueChangedByUser(this, EventArgs.Empty);
            }
        }

        public void setColor(Color color)
        {
            try
            {
                RgbColor rgb = ColorConverting.ColorToRgb(color);
                HslColor hsl = ColorConverting.RgbToHsl(rgb);
                colorAreaUserControl.SetColor((double)hsl.Hue, (double)hsl.Saturation, (double)hsl.Light);
                colorSliderUserControl.SetColorSaturation((double)hsl.Saturation);
            }
            catch (System.Exception ex)
            {
            	
            }

        }
        public void GetColor(out Color color)
        {
            color = ColorConverting.HslToRgb(new HslColor(h,s,l)).ToColor();
        }
    }
}
