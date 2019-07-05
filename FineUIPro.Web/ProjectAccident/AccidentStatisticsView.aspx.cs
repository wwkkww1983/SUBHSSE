using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentStatisticsView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentStatisticsId
        {
            get
            {
                return (string)ViewState["AccidentStatisticsId"];
            }
            set
            {
                ViewState["AccidentStatisticsId"] = value;
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
                this.AccidentStatisticsId = Request.Params["AccidentStatisticsId"];
                var accidentStatistics = BLL.AccidentStatisticsService.GetAccidentStatisticsById(this.AccidentStatisticsId);
                if (accidentStatistics != null)
                {
                    this.txtPerson.Text = accidentStatistics.Person;
                    if (accidentStatistics.CompileDate != null)
                    {
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentStatistics.CompileDate);
                    }
                    this.ddlUnitId.Text = BLL.UnitService.GetUnitNameByUnitId(accidentStatistics.UnitId);
                    this.ddlProjectId.Text = BLL.ProjectService.GetProjectNameByProjectId(accidentStatistics.ProjectId);                 
                    this.txtTreatment.Text = accidentStatistics.Treatment;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ServerAccidentStatisticsMenuId;
                this.ctlAuditFlow.DataId = this.AccidentStatisticsId;
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectAccident&type= -1", this.AccidentStatisticsId)));
        }
    }
}