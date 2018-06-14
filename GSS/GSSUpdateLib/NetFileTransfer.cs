using System;
using System.IO;
namespace GSSUpdateLib
{
    /// <summary>
    /// NetFileTransfer 的摘要说明。
    /// 用Remoting 实现文件传输管理
    /// </summary>
    public class NetFileTransfer : MarshalByRefObject
    {
        public NetFileTransfer()
            : base()
        {
        }

        public FileInfo[] GetFileList(string filePath)
        {
            //获得当前目录下的所有文件 
            filePath = AppDomain.CurrentDomain.BaseDirectory + filePath;
            DirectoryInfo curDir = new DirectoryInfo(filePath);
            return curDir.GetFiles("*.*", SearchOption.AllDirectories);
        }

        /// <summary>
        /// 获取文件的数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>数组（默认null）</returns>
        public byte[] GetFileBytes(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    return buffer;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <returns>状态</returns>
        public bool SendFileBytes(byte[] fileBytes, string savePath)
        {

            if (fileBytes == null) return false;

            try
            {
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
