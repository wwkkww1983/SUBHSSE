using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class HSECertificateEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSECertificateId
        {
            get
            {
                return (string)ViewState["HSECertificateId"];
            }
            set
            {
                ViewState["HSECertificateId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.HSECertificateId = Request.Params["HSECertificateId"];
                if (!string.IsNullOrEmpty(this.HSECertificateId))
                {
                    Model.Check_HSECertificate hseCertificate = BLL.HSECertificateService.GetHSECertificateById(this.HSECertificateId);
                    if (hseCertificate!=null)
                    {
                        this.ProjectId = hseCertificate.ProjectId;
                        this.txtHSECertificateCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HSECertificateId);
                        this.txtHSECertificateName.Text = hseCertificate.HSECertificateName;                        
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtHSECertificateCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHSECertificateMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSECertificateMenuId;
                this.ctlAuditFlow.DataId = this.HSECertificateId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HSECertificateId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSECertificate&menuId=9A034CAD-C7D5-4DE4-9FF5-828D35FFEE28", this.HSECertificateId)));
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.Check_HSECertificate hseCertificate = new Model.Check_HSECertificate
            {
                ProjectId = this.ProjectId,
                HSECertificateCode = this.txtHSECertificateCode.Text.Trim(),
                HSECertificateName = this.txtHSECertificateName.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
                States = BLL.Const.State_0
            };
            if (type==BLL.Const.BtnSubmit)
            {
                hseCertificate.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.HSECertificateId))
            {
                hseCertificate.HSECertificateId = this.HSECertificateId;
                BLL.HSECertificateService.UpdateHSECertificate(hseCertificate);
                BLL.LogService.AddSys_Log(this.CurrUser, hseCertificate.HSECertificateCode, hseCertificate.HSECertificateId,BLL.Const.ProjectHSECertificateMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                this.HSECertificateId = SQLHelper.GetNewID(typeof(Model.Check_HSECertificate));
                hseCertificate.HSECertificateId = this.HSECertificateId;
                BLL.HSECertificateService.AddHSECertificate(hseCertificate);
                BLL.LogService.AddSys_Log(this.CurrUser, hseCertificate.HSECertificateCode, hseCertificate.HSECertificateId, BLL.Const.ProjectHSECertificateMenuId, BLL.Const.BtnModify);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSECertificateMenuId, this.HSECertificateId, (type == BLL.Const.BtnSubmit ? true : false), hseCertificate.HSECertificateName, "../Check/HSECertificateView.aspx?HSECertificateId={0}");
        }
        #endregion
    }
}