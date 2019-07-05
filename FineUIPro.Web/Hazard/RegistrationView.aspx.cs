using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class RegistrationView : PageBase
    {
        #region  定义项
        /// <summary>
        /// 危险观察登记主键
        /// </summary>
        public string RegistrationId
        {
            get
            {
                return (string)ViewState["RegistrationId"];
            }
            set
            {
                ViewState["RegistrationId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)ViewState["ImageUrl"];
            }
            set
            {
                ViewState["ImageUrl"] = value;
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

                RegistrationId = Request.Params["RegistrationId"];
                if (!string.IsNullOrEmpty(RegistrationId))
                {
                    Model.Hazard_Registration registration = BLL.Hazard_RegistrationService.GetRegistrationByRegistrationId(RegistrationId);

                    this.txtRegistrationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RegistrationId);
                    Model.ProjectData_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(registration.WorkAreaId);
                    if (workArea != null)
                    {
                        this.txtWorkArea.Text = workArea.WorkAreaName;
                    }
                    Model.Sys_User checkMan = BLL.UserService.GetUserByUserId(registration.CheckManId);
                    if (checkMan != null)
                    {
                        this.txtCheckMan.Text = checkMan.UserName;
                    }
                    if (registration.CheckTime != null)
                    {
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", registration.CheckTime);
                    }
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(registration.ResponsibilityUnitId);
                    if (unit != null)
                    {
                        this.txtUnit.Text = unit.UnitName;
                    }
                    this.txtProblemTypes.Text = registration.ProblemTypes;
                    this.txtProblemDescription.Text = registration.ProblemDescription;
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(registration.ResponsibilityManId);
                    if (user != null)
                    {
                        this.txtResponsibilityMan.Text = user.UserName;
                    }
                    if (registration.RectificationPeriod != null)
                    {
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationPeriod);
                    }
                    this.ImageUrl = registration.ImageUrl;
                    this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
                    this.txtTakeSteps.Text = registration.TakeSteps;
                    this.txtIsEffective.Text = registration.IsEffective == true ? "是" : "否";
                    this.txtHazardCode.Text = registration.HazardCode;
                    this.txtDefectsType.Text = registration.DefectsType;
                    this.txtMayLeadAccidents.Text = registration.MayLeadAccidents;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HazardRegisterMenuId;
                this.ctlAuditFlow.DataId = this.RegistrationId;
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion
    }
}