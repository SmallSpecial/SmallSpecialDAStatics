namespace GSSUI.AControl.AThirTreeView
{
	partial class AThirTreeViewCtr
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AThirTreeViewCtr));
            this.ctlStateImageList = new System.Windows.Forms.ImageList(this.components);
            // 
            // ctlStateImageList
            // 
            this.ctlStateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ctlStateImageList.ImageStream")));
            this.ctlStateImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ctlStateImageList.Images.SetKeyName(0, "CheckNo.png");
            this.ctlStateImageList.Images.SetKeyName(1, "CheckNo.png");
            this.ctlStateImageList.Images.SetKeyName(2, "CheckFull.png");
            this.ctlStateImageList.Images.SetKeyName(3, "CheckHalf.png");

		}

		#endregion

        private System.Windows.Forms.ImageList ctlStateImageList;
	}
}
