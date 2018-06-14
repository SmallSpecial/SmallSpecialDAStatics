using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.SqlClient;


namespace GSSCSFrameWork
{
    public abstract class DataSerialize
    {
        public DataSerialize()
        {
        }
        /// <summary>
        /// 序列化转化为二进制数组的DataSet
        /// </summary>
        /// <param name="DS"></param>
        /// <returns></returns>
        public static byte[] GetDataSetSurrogateZipBYtes(DataSet DS)
        {
            DataSetSurrogate dss = new DataSetSurrogate(DS);
            ///二进制方式方式序列化
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, dss);
            byte[] buffer = ms.ToArray();
            ///调用压缩方法
            byte[] Zipbuffer = Compress(buffer);
            return Zipbuffer;
        }

        /// <summary>
        /// 压缩二进制文件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Compress(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            Stream zipStream = null;
            zipStream = new GZipStream(ms, CompressionMode.Compress, true);
            ///从指定的字节数组中将压缩的字节写入基础流
            zipStream.Write(data, 0, data.Length);
            zipStream.Close();
            ms.Position = 0;
            byte[] Compressed_Data = new byte[ms.Length];
            ms.Read(Compressed_Data, 0, int.Parse(ms.Length.ToString()));
            return Compressed_Data;
        }

        /// <summary>
        /// 将压缩后的二进制数组解压
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Decompress(byte[] data)
        {      ///data参数为压缩后的二进制数组
            try
            {
                MemoryStream ms = new MemoryStream(data);
                ms.Position = 0;
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = new byte[1024 * 10000];

                //dc_data = StreamToBytes(zipStream, dc_data.Length);

                int bytesRead = zipStream.Read(dc_data, 0, dc_data.Length);
                zipStream.Close();
                return dc_data;

                ///返回解压后的二进制数组
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// 将二进制文件反序列化后转化为DataSet
        /// </summary>
        /// <param name="ZipByte"></param>
        /// <returns></returns>
        public static DataSet GetDatasetFromByte(byte[] ZipByte)
        {
            byte[] buffer = Decompress(ZipByte);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss;
            dss = (DataSetSurrogate)ser.Deserialize(new MemoryStream(buffer));
            DataSet DS = dss.ConvertToDataSet();
            return DS;
        }

        /// <summary>
        /// 把SqlParameter转成string
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetStringFromSqlParameter(SqlParameter parameters)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, parameters);
            byte[] buffer = ms.GetBuffer();
            string parstr = Convert.ToBase64String(buffer);
            return parstr;
        }
        /// <summary>
        /// 把String转成SqlParameter
        /// </summary>
        /// <param name="parstr"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParameterFromString(string parstr)
        {
            byte[] buffer = Convert.FromBase64String(parstr);
            BinaryFormatter ser = new BinaryFormatter();
            SqlParameter[] sqlpar;
            sqlpar = (SqlParameter[])ser.Deserialize(new MemoryStream(buffer));
            return sqlpar;
        }

        /// <summary>
        /// 把Object转成string
        /// </summary>
        public static string GetStringFromObject(Object value)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, value);
            byte[] buffer = ms.GetBuffer();
            string parstr = Convert.ToBase64String(buffer);
            return parstr;
        }
        /// <summary>
        /// 把String转成Object
        /// </summary>
        public static object GetObjectFromString(string value)
        {
            byte[] buffer = Convert.FromBase64String(value);
            BinaryFormatter ser = new BinaryFormatter();
            Object returnvalue;
            returnvalue = ser.Deserialize(new MemoryStream(buffer));
            return returnvalue;
        }


        /// <summary>
        /// 把Object转成byte
        /// </summary>
        public static byte[] GetByteFromObject(Object value)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, value);
            byte[] buffer = ms.GetBuffer();
            return buffer;
        }
        /// <summary>
        /// 把String转成Object
        /// </summary>
        public static object GetObjectFromByte(byte[] value)
        {

            BinaryFormatter ser = new BinaryFormatter();
            Object returnvalue;
            returnvalue = ser.Deserialize(new MemoryStream(value));
            return returnvalue;
        }


        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        private static byte[] StreamToBytes(Stream stream, long len)
        {
            byte[] bytes = new byte[len];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        private static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        private static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        private static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

    }
}
