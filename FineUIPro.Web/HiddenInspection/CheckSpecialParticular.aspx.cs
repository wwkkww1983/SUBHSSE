using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class CheckSpecialParticular : PageBase
    {
        #region 定义项
        /// <summary>
        /// APP专项检查主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
            }
        }
        #endregion

        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                CheckSpecialId = Request.Params["CheckSpecialId"];
                Model.HSSE_Hazard_CheckSpecial checkSpecial = BLL.HSSE_Hazard_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.txtCheckSpecialCode.Text = checkSpecial.CheckSpecialCode;
                    if (checkSpecial.Date != null)
                    {
                        this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.Date);
                    }
                    this.txtCheckMan.Text = checkSpecial.CheckMan;
                    this.txtJointCheckMan.Text = checkSpecial.JointCheckMan;
                    this.Grid1.DataSource = from x in Funs.DB.View_Hazard_HazardRegister
                                            where x.CheckSpecialId == CheckSpecialId
                                            orderby x.ResponsibilityUnitName, x.RectificationTime
                                            select x;
                    this.Grid1.DataBind();
                }
                else
                {
                    this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                this.Grid2.DataSource = from x in Funs.DB.HSSE_Hazard_CheckSpecialAudit
                                        where x.CheckSpecialId == CheckSpecialId && x.AuditDate != null
                                        orderby x.AuditDate
                                        select new
                                        {
                                            x.CheckSpecialAuditId,
                                            x.CheckSpecialId,
                                            x.AuditMan,
                                            x.AuditDate,
                                            x.AuditStep,
                                            AuditStepStr = GetAuditStepStr(x.AuditStep),
                                            UserName = (from y in Funs.DB.Sys_User where y.UserId == x.AuditMan select y.UserName).First(),
                                        };
                this.Grid2.DataBind();
            }
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string GetAuditStepStr(string state)
        {
            if (state == BLL.Const.APPCheckSpecial_ReCompile)
            {
                return "重报";
            }
            else if (state == BLL.Const.APPCheckSpecial_Compile)
            {
                return "编制";
            }
            else if (state == BLL.Const.APPCheckSpecial_Check)
            {
                return "办理";
            }
            else if (state == BLL.Const.APPCheckSpecial_ApproveCompleted)
            {
                return "审批完成";
            }
            else
            {
                return "";
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(string unitId, DateTime? date)
        {
            this.Grid1.DataSource = from x in Funs.DB.View_Hazard_HazardRegister
                                    where x.ProblemTypes == "4" && x.ProjectId == this.CurrUser.LoginProjectId
                                    && x.CheckTime.Value.Date == date
                                    && x.CheckItemDetailId != null
                                    orderby x.ResponsibilityUnitName, x.RectificationTime
                                    select x;
            this.Grid1.DataBind();
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationView.aspx?HazardRegisterId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }
        #endregion
    }
}