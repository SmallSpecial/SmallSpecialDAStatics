using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GSSCSFrameWork
{
    public class PlaySound
    {
        public PlaySound()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, string strReturn, int iReturnLength, IntPtr hwndCallback);
        private static void GetWavAndOpen(string sFileName)
        {
            
            string sCommand = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";	//   MediaFile是选择播放文件类型 
            string ret = null;
            mciSendString(sCommand, ret, 0, IntPtr.Zero);
        }
        public static void play(string sFileName)
        {
            StopPlay();
            GetWavAndOpen(sFileName);
            string sCommand = "play MediaFile";
            string ret = null;
            mciSendString(sCommand, ret, 0, IntPtr.Zero);
        }
        private static void StopPlay()
        {
            string sCommand = "close MediaFile";
            string ret = null;
            mciSendString(sCommand, ret, 0, IntPtr.Zero);
        }
    }
}
