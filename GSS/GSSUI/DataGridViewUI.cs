using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GSSUI.AClass;

namespace GSSUI
{
    public partial class DataGridViewUI : System.Windows.Forms.DataGridView
    {
        public Bitmap _BackImg = ImageObject.GetResBitmap("GSSUI.ASkinImg.ButtonImg.Botton2.png");//做为按钮图像
        public Rectangle _BacklightLTRB;//图片等比边界
        public DataGridViewUI()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //InitializeComponent();
        }

        //public DataGridViewUI(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}
        protected override void OnCreateControl()
        {
            //this.EnableHeadersVisualStyles = false;

            //this.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 246, 239);
            //this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            //this.ColumnHeadersHeight = 26;
            //this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //this.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          //  this.ColumnHeadersDefaultCellStyle.ForeColor = this.FindForm().ForeColor;
            //this.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            //this.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            //this.RowHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            //this.RowHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            //this.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            //this.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            //this.DefaultCellStyle.SelectionBackColor = Color.Wheat;
            //this.DefaultCellStyle.SelectionForeColor = Color.DarkSlateBlue;
            //this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //this.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            //this.BackgroundColor = System.Drawing.SystemColors.Window;
            //this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //this.AllowUserToOrderColumns = true;
            //this.AutoGenerateColumns = true;

            //base.OnCreateControl();
        }

        /// <summary> 
        /// 重绘Column、Row     
        ///</summary>        
        /// <param name="e"></param> 
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {               
            //如果是Column      
            if (e.RowIndex == -1)
            {
                drawColumnAndRow(e);
                e.Handled = true;
                //如果是Rowheader      
            }
            else if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                drawColumnAndRow(e); e.Handled = true;
            }
        }
        /// <summary>        
        /// Column和RowHeader绘制       
        /// /// </summary>       
        /// /// <param name="e"></param>        
        void drawColumnAndRow(DataGridViewCellPaintingEventArgs e)
        {
            // 绘制背景色         
            Color backimgcorlor = this.FindForm().BackColor;
            if (this.FindForm().BackgroundImage!=null)
            {
                 backimgcorlor= ((Bitmap)this.FindForm().BackgroundImage).GetPixel(10, 10);
            }

           
            using (LinearGradientBrush backbrush = new LinearGradientBrush(e.CellBounds, Color.FromArgb(255, backimgcorlor), Color.White, LinearGradientMode.Vertical))
            {
                Rectangle border = e.CellBounds;
                border.Width -= 1;
                //填充绘制效果        
                e.Graphics.FillRectangle(backbrush, border);
                
          
               //ImageDrawRect.DrawRect(e.Graphics, ((Bitmap)this.FindForm().BackgroundImage), e.CellBounds, Rectangle.FromLTRB(10, 10, 10, 10), 4, 5);
                //ImageDrawRect.DrawRect(e.Graphics, _BackImg, e.CellBounds, Rectangle.FromLTRB(10, 10, 10, 10), 4, 5);
                //绘制Column、Row的Text信息           
                e.PaintContent(e.CellBounds);
                //绘制边框              
                ControlPaint.DrawBorder3D(e.Graphics, e.CellBounds, Border3DStyle.Etched);
            }


           

        }

        ///// <summary>        
        ///// Row重绘前处理       
        ///// /// </summary>        
        ///// <param name="e"></param>        
        // protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)        
        // {            
        //     base.OnRowPrePaint(e);            
        //     //是否是选中状态           
        //     if ((e.State & DataGridViewElementStates.Selected) ==DataGridViewElementStates.Selected)           
        //     {                
        //         // 计算选中区域Size              
        //         //int width = this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)+_RowHeadWidth;
        //         int width = this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
        //         Rectangle rowBounds = new Rectangle(0 , e.RowBounds.Top, width,e.RowBounds.Height);
        //         // 绘制选中背景色                
        //         using (LinearGradientBrush backbrush = new LinearGradientBrush(rowBounds, ProfessionalColors.MenuItemPressedGradientMiddle, e.InheritedRowStyle.ForeColor, 90.0f)) 
        //         {                    
        //             e.Graphics.FillRectangle(backbrush, rowBounds);     
        //             e.PaintCellsContent(rowBounds);                  
        //             e.Handled = true;               
        //         }            
        //     }      
        // }

        ///// <summary>        
        ///// Row重绘后处理        
        ///// </summary>        
        ///// <param name="e"></param>        
        // protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)    
        // {            
        //     base.OnRowPostPaint(e);    
        //    // int width = this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + _RowHeadWidth;
        //     int width = this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
        //     Rectangle rowBounds = new Rectangle(0, e.RowBounds.Top, width, e.RowBounds.Height);
        //     if (this.CurrentCellAddress.Y == e.RowIndex){
        //         //设置选中边框           
        //         e.DrawFocus(rowBounds, true);     
        //     }    
        // }


    }
}
