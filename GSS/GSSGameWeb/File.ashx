<%@ WebHandler Language="c#" Class="GSSGameWeb.File_WebHandler" Debug="true" %>

using System;
using System.Web;
using System.IO;
using GSSGameWeb;
using System.Web.SessionState;

namespace GSSGameWeb
{

    public class File_WebHandler : IHttpHandler, IRequiresSessionState
    {
        private const int UploadFileLimit = 1;//�ϴ��ļ���������

        private string _msg = "�ϴ��ɹ���";//������Ϣ

        private double _MaxContentLenght = Utils.GetMaxUpContentLength();//���ļ�����ϴ�


        public void ProcessRequest(HttpContext context)
        {


            //�����Flash�ύ
            if (Utils.StrIsNullOrEmpty(GSSRequest.GetUrlReferrer()))
                return;
            else if (GSSRequest.IsCrossSitePost(GSSRequest.GetUrlReferrer(), GSSRequest.GetHost())) //����ǿ�վ�ύ...
                return;

            string _url = "";
            int dd = HttpContext.Current.Request.ContentLength;
            int s = dd;


            string vercode = GSSRequest.GetString("v");

            if (context.Session["vercode"] == null || vercode != context.Session["vercode"].ToString())
            {
                _msg = "�ϴ�ʧ��,��֤�벻��ȷ��ʧЧ!";

            }
            //else if (context.Request.ContentLength > 2097150)
            //{
            //    _msg = "�ϴ�ʧ��,�ļ�̫��!";
            //}
            else if (context.Request.Files.Count == 0)
            {
                _msg = "�ϴ�ʧ��,û������!";
            }
            else
            {
                int iCount = 0;
                int iTotal = context.Request.Files.Count;

                if (!SessionAllow(context.Session))
                {
                    _msg = "30����ֻ�����ύһ��,���Ժ�����!!";
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < iTotal; i++)
                        {
                            HttpPostedFile file = context.Request.Files[i];
                            if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                            {
                                string fileatt = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                                if (".jpg.gif.bmp.png".IndexOf(fileatt) == -1)
                                {
                                    _msg = "�ϴ�ʧ��,��������ļ���ʽ!";
                                    break;
                                }

                                //�����ļ�
                                // file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("./GameUpfile/" + Path.GetFileName(file.FileName)));
                                DateTime time = DateTime.Now;
                                string path = System.Web.HttpContext.Current.Server.MapPath("./GameUpfile/" + time.ToString("yyyyMMdd") + "/");

                                string picurl = path + "" + time.ToString("yyyyMMddHHmmssfffffff") + fileatt;

                                _url = "http://" + GSSRequest.GetHostP() + "/GameUpfile/" + time.ToString("yyyyMMdd") + "/" + time.ToString("yyyyMMddHHmmssfffffff") + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();

                                if (file.ContentLength > _MaxContentLenght * 1024 * 1024)
                                {
                                    _msg = "�ϴ�ʧ��,��������" + _MaxContentLenght + "M!";
                                    break;
                                }

                                if (!Directory.Exists(path))
                                    Utils.CreateDir(path);
                                file.SaveAs(picurl);


                                //������Ը���ʵ��������������
                                if (++iCount > UploadFileLimit)
                                {
                                    _msg = "�ϴ�ʧ��,�������ƣ�" + UploadFileLimit;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { _msg = "�ϴ�ʧ��,Server Error" + ex.Message; }
                }


            }

            if (_msg.IndexOf("�ϴ�ʧ��") == -1 && _msg.IndexOf("����") == -1)
            {
                _msg = _url;

            }

            context.ClearError();
            context.Response.Clear();
            context.Response.Write("<script>window.parent.Finish('" + _msg + "');window.parent.savetaska();</script>");
        }


        private bool SessionAllow(HttpSessionState session)
        {

            if (session["requestTime"] != null)
            {
                TimeSpan ts = DateTime.Now -
                Convert.ToDateTime(session["requestTime"]);
                if (ts.TotalSeconds > 30)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }

}