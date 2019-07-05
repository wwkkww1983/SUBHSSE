using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.IO;

namespace FineUIPro.Web.Supervise
{
    public partial class ShowFileUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
            }
        }
        #endregion

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
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(FullAttachUrl)
                    + ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpFile_Click(object sender, EventArgs e)
        {
            if (fuAttachUrl.HasFile)
            {
                this.lbAttachUrl.Text = fuAttachUrl.ShortFileName;
                if (ValidateFileTypes(this.lbAttachUrl.Text))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.FullAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuAttachUrl, this.FullAttachUrl, UploadFileService.SuperviseCheckReportFilePath);
                if (string.IsNullOrEmpty(this.FullAttachUrl))
                {
                    ShowNotify("文件名已经存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ShowNotify("文件上传成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("上传文件不存在！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            string filePath = BLL.Funs.RootPath + this.FullAttachUrl;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                Response.Clear();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(filePath, 0, fileSize);
                Response.Flush();
                Response.Close();
                this.SimpleForm1.Reset();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('模板不存在，请联系管理员！')", true);
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.fuAttachUrl.Reset();
            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;
        }
        #endregion
    }
}