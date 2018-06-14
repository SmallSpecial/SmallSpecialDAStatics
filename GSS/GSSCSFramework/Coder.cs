using System;
using System.Collections.Generic;
using System.Text;

namespace GSSCSFrameWork
{
    /// <summary>
    /// ͨѶ�����ṩ����ͽ������.
    /// </summary>
    public class Coder
    {
        /// <summary>
        /// ���뷽ʽ
        /// </summary>
        private EncodingMothord _encodingMothord;

        protected Coder()
        {

        }

        public Coder(EncodingMothord encodingMothord)
        {
            _encodingMothord = encodingMothord;
        }

        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
        }

        /// <summary>
        /// ͨѶ���ݽ���
        /// </summary>
        /// <param name="dataBytes">��Ҫ���������</param>
        /// <returns>����������</returns>
        public virtual string GetEncodingString(byte[] dataBytes, int size)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(dataBytes, 0, size);
                    }
                default:
                    {
                        throw (new Exception("δ����ı����ʽ"));
                    }
            }

        }

        /// <summary>
        /// ���ݱ���
        /// </summary>
        /// <param name="datagram">��Ҫ����ı���</param>
        /// <returns>����������</returns>
        public virtual byte[] GetEncodingBytes(string datagram)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetBytes(datagram);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetBytes(datagram);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetBytes(datagram);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetBytes(datagram);
                    }
                default:
                    {
                        throw (new Exception("δ����ı����ʽ"));
                    }
            }
        }

        public virtual byte[] GetEncodBytes(object msgp)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();//���������л���
            bf.Serialize(ms, msgp);//����Ϣ��ת��Ϊ�ڴ���
            ms.Position = 0;
            byte[] data = ms.GetBuffer();

            return data;
        }

        public virtual object GetEncodObject(byte[] msgp)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            ms.Write(msgp, 0, msgp.Length);
            ms.Position = 0;
            return bf.Deserialize(ms);
        }
    }
}
