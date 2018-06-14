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
        private const int UploadFileLimit = 1;//上传文件数量限制

        private string _msg = "上传成功！";//返回信息

        private double _MaxContentLenght = Utils.GetMaxUpContentLength();//单文件最大上传


        public void ProcessRequest(HttpContext context)
        {


            //如果是Flash提交
            if (Utils.StrIsNullOrEmpty(GSSRequest.GetUrlReferrer()))
                return;
            else if (GSSRequest.IsCrossSitePost(GSSRequest.GetUrlReferrer(), GSSRequest.GetHost())) //如果是跨站提交...
                return;

            string _url = "";
            int dd = HttpContext.Current.Request.ContentLength;
            int s = dd;


            string vercode = GSSRequest.GetString("v");

            if (context.Session["vercode"] == null || vercode != context.Session["vercode"].ToString())
            {
                _msg = "上传失败,验证码不正确或失效!";

            }
            //else if (context.Request.ContentLength > 2097150)
            //{
            //    _msg = "上传失败,文件太大!";
            //}
            else if (context.Request.Files.Count == 0)
            {
                _msg = "上传失败,没有数据!";
            }
            else
            {
                int iCount = 0;
                int iTotal = context.Request.Files.Count;

                if (!SessionAllow(context.Session))
                {
                    _msg = "30秒内只允许提交一次,请稍候再试!!";
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
                                    _msg = "上传失败,不允许的文件格式!";
                                    break;
                                }

                                //保存文件
                                // file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("./GameUpfile/" + Path.GetFileName(file.FileName)));
                                DateTime time = DateTime.Now;
                                string path = System.Web.HttpContext.Current.Server.MapPath("./GameUpfile/" + time.ToString("yyyyMMdd") + "/");

                                string picurl = path + "" + time.ToString("yyyyMMddHHmmssfffffff") + fileatt;

                                _url = "http://" + GSSRequest.GetHostP() + "/GameUpfile/" + time.ToString("yyyyMMdd") + "/" + time.ToString("yyyyMMddHHmmssfffffff") + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();

                                if (file.ContentLength > _MaxContentLenght * 1024 * 1024)
                                {
                                    _msg = "上传失败,超过限制" + _MaxContentLenght + "M!";
                                    break;
                                }

                                if (!Directory.Exists(path))
                                    Utils.CreateDir(path);
                                file.SaveAs(picurl);


                                //这里可以根据实际设置其他限制
                                if (++iCount > UploadFileLimit)
                                {
                                    _msg = "上传失败,超过限制：" + UploadFileLimit;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { _msg = "上传失败,Server Error" + ex.Message; }
                }


            }

            if (_msg.IndexOf("上传失败") == -1 && _msg.IndexOf("再试") == -1)
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