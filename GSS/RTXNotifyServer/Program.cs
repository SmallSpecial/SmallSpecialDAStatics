using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace RTXNotifyServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool bCreatedNew;
            

            Mutex m = new Mutex(false, "GSSClient", out bCreatedNew);
            if (bCreatedNew)
            {
                try
                {
                    Application.Run(new FormMain());
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message+"\r\n"+ex.TargetSite+"\r\n"+ex.Source,"出错提示");
                }
                
            }
            else
            {
                MessageBox.Show("程序已经在运行", "信息提示");
            }
            
        }
    }
}
