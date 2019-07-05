using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class InUnitEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string InUnitId
        {
            get
            {
                return (string)ViewState["InUnitId"];
            }
            set
            {
                ViewState["InUnitId"] = value;
            }
        }

        /// <summary>
        /// 培训记录附件
        /// </summary>
        private string TrainRecordsUrl
        {
            get
            {
                return (string)ViewState["TrainRecordsUrl"];
            }
            set
            {
                ViewState["TrainRecordsUrl"] = value;
            }
        }
        
        /// <summary>
        /// 方案及资质审查附件
        /// </summary>
        private string PlanUrl
        {
            get
            {
                return (string)ViewState["PlanUrl"];
            }
            set
            {
                ViewState["PlanUrl"] = value;
            }
        }

        /// <summary>
        /// 临时到场人员培训附件
        /// </summary>
        private string TemporaryPersonUrl
        {
            get
            {
                return (string)ViewState["TemporaryPersonUrl"];
            }
            set
            {
                ViewState["TemporaryPersonUrl"] = value;
            }
        }

        /// <summary>
        /// 厂家入场安全人员培训附件
        /// </summary>
        private string InPersonTrainUrl
        {
            get
            {
                return (string)ViewState["InPersonTrainUrl"];
            }
            set
            {
                ViewState["InPersonTrainUrl"] = value;
            }
        }

        /// <summary>
        /// HSE协议附件
        /// </summary>
        private string HSEAgreementUrl
        {
            get
            {
                return (string)ViewState["HSEAgreementUrl"];
            }
            set
            {
                ViewState["HSEAgreementUrl"] = value;
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
                this.InUnitId = Request.Params["InUnitId"];
                if (!string.IsNullOrEmpty(this.InUnitId))
                {
                    Model.QualityAudit_InUnit inUnit = BLL.InUnitService.GetInUnitById(this.InUnitId);
                    if (inUnit !=null)
                    {
                        this.txtInUnitCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.InUnitId);
                        this.txtManufacturerName.Text = inUnit.ManufacturerName;
                        this.txtHSEMan.Text = inUnit.HSEMan;
                        this.txtHeadTel.Text = inUnit.HeadTel;
                        if (inUnit.InDate!=null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", inUnit.InDate);
                        }
                        if (inUnit.PersonCount!=null)
                        {
                            this.txtPersonCount.Text = Convert.ToString(inUnit.PersonCount);
                        }
                        if (inUnit.TrainNum!=null)
                        {
                            this.txtTrainNum.Text = Convert.ToString(inUnit.TrainNum);
                        }
                        if (inUnit.OutDate!=null)
                        {
                            this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", inUnit.OutDate);
                        }
                        this.txtBadgesIssued.Text = inUnit.BadgesIssued;
                        if (inUnit.JobTicketValidity!=null)
                        {
                            this.txtJobTicketValidity.Text = string.Format("{0:yyyy-MM-dd}", inUnit.JobTicketValidity);
                        }
                        this.TrainRecordsUrl = inUnit.TrainRecordsUrl;
                        this.divTrainRecordsUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.TrainRecordsUrl);
                        this.PlanUrl = inUnit.PlanUrl;
                        this.divPlanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.PlanUrl);
                        this.TemporaryPersonUrl = inUnit.TemporaryPersonUrl;
                        this.divTemporaryPersonUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.TemporaryPersonUrl);
                        this.InPersonTrainUrl = inUnit.InPersonTrainUrl;
                        this.divInPersonTrainUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.InPersonTrainUrl);
                        this.txtAccommodation.Text = inUnit.Accommodation;
                        this.txtOperationTicket.Text = inUnit.OperationTicket;
                        this.txtLaborSituation.Text = inUnit.LaborSituation;
                        this.HSEAgreementUrl = inUnit.HSEAgreementUrl;
                        this.divHSEAgreementUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.HSEAgreementUrl);
                    }
                }
                else
                {
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}",DateTime.Now );
                    ////自动生成编码
                    this.txtInUnitCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.InUnitMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }
            }
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
            Model.QualityAudit_InUnit inUnit = new Model.QualityAudit_InUnit
            {
                ProjectId = this.CurrUser.LoginProjectId,
                InUnitCode = this.txtInUnitCode.Text.Trim(),
                ManufacturerName = this.txtManufacturerName.Text.Trim(),
                HSEMan = this.txtHSEMan.Text.Trim(),
                HeadTel = this.txtHeadTel.Text.Trim(),
                InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim()),
                PersonCount = Funs.GetNewInt(this.txtPersonCount.Text.Trim()),
                TrainNum = Funs.GetNewInt(this.txtTrainNum.Text.Trim()),
                OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim()),
                BadgesIssued = this.txtBadgesIssued.Text.Trim(),
                JobTicketValidity = Funs.GetNewDateTime(this.txtJobTicketValidity.Text.Trim()),
                TrainRecordsUrl = this.TrainRecordsUrl,
                PlanUrl = this.PlanUrl,
                TemporaryPersonUrl = this.TemporaryPersonUrl,
                InPersonTrainUrl = this.InPersonTrainUrl,
                Accommodation = this.txtAccommodation.Text.Trim(),
                OperationTicket = this.txtOperationTicket.Text.Trim(),
                LaborSituation = this.txtLaborSituation.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
                HSEAgreementUrl = this.HSEAgreementUrl
            };
            if (!string.IsNullOrEmpty(this.InUnitId))
            {
                inUnit.InUnitId = this.InUnitId;
                BLL.InUnitService.UpdateInUnit(inUnit);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改采购供货厂家管理", inUnit.InUnitId);
            }
            else
            {
                this.InUnitId = SQLHelper.GetNewID(typeof(Model.QualityAudit_InUnit));
                inUnit.InUnitId = this.InUnitId;
                BLL.InUnitService.AddInUnit(inUnit);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加采购供货厂家管理", inUnit.InUnitId);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 培训记录附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTrainRecordsUrl_Click(object sender, EventArgs e)
        {
            if (this.btnTrainRecordsUrl.HasFile)
            {
                this.TrainRecordsUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnTrainRecordsUrl, this.TrainRecordsUrl, UploadFileService.TrainRecordsUrlFilePath);
                this.divTrainRecordsUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.TrainRecordsUrl);
            }
        }

        /// <summary>
        /// 方案及资质审查附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlanUrl_Click(object sender, EventArgs e)
        {
            if (this.btnPlanUrl.HasFile)
            {
                this.PlanUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnPlanUrl, this.PlanUrl, UploadFileService.PlanUrlFilePath);
                this.divPlanUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.PlanUrl);
            }
        }

        /// <summary>
        /// 临时到场人员培训附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTemporaryPersonUrl_Click(object sender, EventArgs e)
        {
            if (this.btnTemporaryPersonUrl.HasFile)
            {
                this.TemporaryPersonUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnTemporaryPersonUrl, this.TemporaryPersonUrl, UploadFileService.TemporaryPersonUrlFilePath);
                this.divTemporaryPersonUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.TemporaryPersonUrl);
            }
        }

        /// <summary>
        /// 厂家入场安全人员培训附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInPersonTrainUrl_Click(object sender, EventArgs e)
        {
            if (this.btnInPersonTrainUrl.HasFile)
            {
                this.InPersonTrainUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnInPersonTrainUrl, this.InPersonTrainUrl, UploadFileService.InPersonTrainUrlFilePath);
                this.divInPersonTrainUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.InPersonTrainUrl);
            }
        }

        /// <summary>
        /// HSE协议附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHSEAgreementUrl_Click(object sender, EventArgs e)
        {
            if (this.btnHSEAgreementUrl.HasFile)
            {
                this.HSEAgreementUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnHSEAgreementUrl, this.HSEAgreementUrl, UploadFileService.HSEAgreementUrlFilePath);
                this.divHSEAgreementUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.HSEAgreementUrl);
            }
        }
        #endregion
    }
}