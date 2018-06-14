using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GSSCSFrameWork
{
     public class FormsMsg
    {
    	public FormsMsg()
		{
		}
        /// <summary>
        /// 提供Win32的消息发送,发送后等到处理完成然后返回
        /// </summary>
        /// <param name="hWnd">要发送消息窗体的句柄</param>
        /// <param name="Msg">需要发送的指定消息</param>
        /// <param name="wParam">需要发送的指定附加消息</param>
        /// <param name="lParam">需要发送的指定附加消息</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        /// <summary>
        /// 提供Win32的消息发送,发送后立即返回
        /// </summary>
        /// <param name="hWnd">要发送消息窗体的句柄</param>
        /// <param name="Msg">需要发送的指定消息</param>
        /// <param name="wParam">需要发送的指定附加消息</param>
        /// <param name="lParam">需要发送的指定附加消息</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(int hWnd, int Msg, int wParam, int lParam);

  
    }
}
