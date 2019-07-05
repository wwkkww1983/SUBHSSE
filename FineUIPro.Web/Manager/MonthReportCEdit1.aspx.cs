using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCEdit1 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                Model.Base_Unit mainUnit = BLL.UnitService.GetThisUnitDropDownList()[0];
                this.lbProjectName.Text = project.ProjectName;
                if (mainUnit != null)
                {
                    this.lblMainUnitName.Text = mainUnit.UnitName;
                }
                this.lblProjectAddress.Text = project.ProjectAddress;
                this.lblProjectCode.Text = project.ProjectCode;
                this.lblContractNo.Text = project.ContractNo;  //合同号
                if (!string.IsNullOrEmpty(project.ProjectType))
                {
                    Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_ProjectType).FirstOrDefault(x => x.ConstValue == project.ProjectType);
                    if (c != null)
                    {
                        this.lblProjectType.Text = c.ConstText;
                    }
                }
                this.lblWorkRange.Text = project.WorkRange;    //工程范围
                if (project.Duration != null)
                {
                    this.lblDuration.Text = project.Duration.ToString();      //工期（月）
                }
                if (project.StartDate != null)
                {
                    this.lblStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                }
                if (project.EndDate != null)
                {
                    this.lblEndDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                }
            }
        }
    }
}