using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SiteConstruction
{
    public partial class MonthPlanView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthPlanId
        {
            get
            {
                return (string)ViewState["MonthPlanId"];
            }
            set
            {
                ViewState["MonthPlanId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///加载单位

                this.MonthPlanId = Request.Params["MonthPlanId"];
                if (!string.IsNullOrEmpty(this.MonthPlanId))
                {
                    Model.SiteConstruction_MonthPlan MonthPlan = BLL.MonthPlanService.GetMonthPlanById(this.MonthPlanId);
                    if (MonthPlan != null)
                    {
                        this.ProjectId = MonthPlan.ProjectId;
                        this.txtJobContent.Text = MonthPlan.JobContent;
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(MonthPlan.UnitId);
                        if (unit != null)
                        {
                            this.txtUnit.Text = unit.UnitName;
                        }
                        this.txtMonths.Text = string.Format("{0:yyyy-MM}", MonthPlan.Months);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", MonthPlan.CompileDate);                                            
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthPlanMenuId;
                this.ctlAuditFlow.DataId = this.MonthPlanId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthPlanAttachUrl&menuId={1}&type=-1", MonthPlanId, BLL.Const.ProjectMonthPlanMenuId)));
        }
        #endregion
    }
}