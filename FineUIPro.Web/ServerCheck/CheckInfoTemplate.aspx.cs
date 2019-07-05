using System;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.ServerCheck
{
    public partial class CheckInfoTemplate : PageBase
    {
        
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sysSet = BLL.Funs.DB.Sys_Set.FirstOrDefault(x => x.SetId == -100);
                if (sysSet != null)
                {
                    this.HtmlEditor1.Text = sysSet.SetValue;
                }
            }
        }
        #endregion        

        #region 关闭按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
        #endregion

        /// <summary>
        /// 下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnUpload_Click(object sender, EventArgs e)
        {
            string templatePath = BLL.Const.Check_CheckInfoTemplateUrl;
            string uploadfilepath = Server.MapPath("~/") + templatePath;
            string fileName = Path.GetFileName(templatePath);
            FileInfo info = new FileInfo(uploadfilepath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                System.Web.HttpContext.Current.Response.TransmitFile(uploadfilepath, 0, fileSize);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Close();
            }
            else
            {
                ShowNotify("文件不存在！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
