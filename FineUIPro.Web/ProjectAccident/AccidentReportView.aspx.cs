using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentReportView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentReportId
        {
            get
            {
                return (string)ViewState["AccidentReportId"];
            }
            set
            {
                ViewState["AccidentReportId"] = value;
            }
        }       

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

                this.AccidentReportId = Request.Params["AccidentReportId"];
                var accidentReport = BLL.AccidentReportService.GetAccidentReportById(this.AccidentReportId);
                if (accidentReport != null)
                {
                    this.txtWorkArea.Text = accidentReport.WorkArea;
                    if (accidentReport.CompileDate != null)
                    {
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(accidentReport.UnitId))
                    {
                        this.ddlUnitId.Text = BLL.UnitService.GetUnitNameByUnitId(accidentReport.UnitId);
                    }
                    if (!string.IsNullOrEmpty(accidentReport.ProjectId))
                    {
                        this.ddlProjectId.Text = BLL.ProjectService.GetProjectNameByProjectId(accidentReport.ProjectId);
                    }
                    this.txtAccidentDescription.Text = accidentReport.AccidentDescription;
                    this.txtCasualties.Text = accidentReport.Casualties;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ServerAccidentReportMenuId;
                this.ctlAuditFlow.DataId = this.AccidentReportId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {            
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectAccident&type=-1", this.AccidentReportId)));
        }
    }
}