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
    public partial class CheckSpecialEdit : PageBase
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
                this.drpAuditMan.DataTextField = "UserName";
                drpAuditMan.DataValueField = "UserId";
                this.drpAuditMan.DataSource = (from x in Funs.DB.Sys_User
                                               join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                                               join z in Funs.DB.Sys_Role on y.RoleId equals z.RoleId
                                               where y.ProjectId == this.CurrUser.LoginProjectId
                                               orderby z.RoleCode, x.UserCode
                                               select new Model.SpSysUserItem
                                               {
                                                   UserName = "[" + z.RoleName + "] " + x.UserName,
                                                   UserId = x.UserId,
                                               });
                this.drpAuditMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpAuditMan);

                this.drpAuditStep.DataTextField = "Name";
                this.drpAuditStep.DataValueField = "Id";
                this.drpAuditStep.DataSource = BLL.HSSE_Hazard_CheckSpecialService.GetNetAuditStepByState(BLL.Const.APPCheckSpecial_Compile);
                this.drpAuditStep.DataBind();
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
                                            orderby x.ResponsibilityUnitName,x.RectificationTime
                                            select x;
                    this.Grid1.DataBind();
                }
                else
                {
                    this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.BindGrid(Funs.GetNewDateTime(this.txtDate.Text.Trim()));
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

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid(Funs.GetNewDateTime(this.txtDate.Text.Trim()));
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(DateTime? date)
        {
            this.Grid1.DataSource = from x in Funs.DB.View_Hazard_HazardRegister
                                    where x.ProblemTypes == "4" && x.ProjectId == this.CurrUser.LoginProjectId
                                    && x.CheckTime.Value.Date == date
                                    && x.CheckItemDetailId != null && x.CheckSpecialId == null
                                    orderby x.ResponsibilityUnitName, x.RectificationTime
                                    select x;
            this.Grid1.DataBind();
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

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnSave))
            {
                SaveCheckSpecial("save");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnSubmit))
            {
                SaveCheckSpecial("submit");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveCheckSpecial(string saveType)
        {
            if (saveType == "submit")
            {
                if (this.drpAuditMan.SelectedValue == BLL.Const._Null && this.drpAuditStep.SelectedValue != BLL.Const.APPCheckSpecial_ApproveCompleted)
                {
                    ShowNotify("请选择办理人员！", MessageBoxIcon.Warning);
                    return;
                }
            }
            Model.HSSE_Hazard_CheckSpecial checkSpecial = new Model.HSSE_Hazard_CheckSpecial();
            Model.HSSE_Hazard_CheckSpecialAudit checkSpecialAudit = new Model.HSSE_Hazard_CheckSpecialAudit();
            checkSpecial.CheckSpecialCode = this.txtCheckSpecialCode.Text.Trim();
            checkSpecial.ProjectId = this.CurrUser.LoginProjectId;
            if (!string.IsNullOrEmpty(this.txtDate.Text.Trim()))
            {
                checkSpecial.Date = Convert.ToDateTime(this.txtDate.Text.Trim());
            }
            checkSpecial.CheckMan = this.txtCheckMan.Text.Trim();
            checkSpecial.JointCheckMan = this.txtJointCheckMan.Text.Trim();
            if (saveType == "submit")
            {
                checkSpecial.States = this.drpAuditStep.SelectedValue;
            }
            else
            {
                checkSpecial.States = BLL.Const.APPCheckSpecial_Compile;
            }
            if (!string.IsNullOrEmpty(CheckSpecialId))
            {
                checkSpecial.CheckSpecialId = CheckSpecialId;
                Model.HSSE_Hazard_CheckSpecial checkSpecial1 = BLL.HSSE_Hazard_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
                string state = checkSpecial1.States;
                BLL.HSSE_Hazard_CheckSpecialService.UpdateCheckSpecial(checkSpecial);
                if (saveType == "submit")
                {
                    Model.HSSE_Hazard_CheckSpecialAudit checkSpecialAudit1 = BLL.HSSE_Hazard_CheckSpecialAuditService.GetCheckSpecialAuditByCheckSpecialId(CheckSpecialId);
                    checkSpecialAudit1.AuditDate = DateTime.Now;
                    BLL.HSSE_Hazard_CheckSpecialAuditService.UpdateCheckSpecialAudit(checkSpecialAudit1);
                }
                BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial1.CheckSpecialCode, checkSpecial1.CheckSpecialId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnModify);
            }
            else
            {
                Model.HSSE_Hazard_CheckSpecial checkSpecial1 = (from x in Funs.DB.HSSE_Hazard_CheckSpecial
                                                                where x.Date.Value.Date == Convert.ToDateTime(this.txtDate.Text.Trim())
                                                                select x).FirstOrDefault();
                if (checkSpecial1 != null)
                {
                    ShowNotify("所选单位及日期的专项检查已经存在,不能重复！", MessageBoxIcon.Warning);
                    return;
                }
                Model.HSSE_Hazard_CheckSpecial cs = BLL.HSSE_Hazard_CheckSpecialService.GetCheckSpecialByCheckSpecialCode(this.txtCheckSpecialCode.Text.Trim());
                if (cs != null)
                {
                    ShowNotify("专项检查编号已经存在,不能重复！", MessageBoxIcon.Warning);
                    return;
                }
                checkSpecial.CheckSpecialId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_CheckSpecial));
                BLL.HSSE_Hazard_CheckSpecialService.AddCheckSpecial(checkSpecial);
                BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnAdd);
                Model.HSSE_Hazard_CheckSpecialAudit checkSpecialCompileAudit = new Model.HSSE_Hazard_CheckSpecialAudit
                {
                    CheckSpecialId = checkSpecial.CheckSpecialId,
                    AuditStep = BLL.Const.APPCheckSpecial_Compile,
                    AuditMan = this.CurrUser.UserId
                };
                if (saveType == "submit")
                {
                    checkSpecialCompileAudit.AuditDate = DateTime.Now;
                }
                BLL.HSSE_Hazard_CheckSpecialAuditService.AddCheckSpecialAudit(checkSpecialCompileAudit);
            }

            if (saveType == "submit")
            {
                checkSpecialAudit.CheckSpecialId = checkSpecial.CheckSpecialId;
                if (this.drpAuditStep.SelectedValue != Const.APPCheckSpecial_ApproveCompleted)
                {
                    checkSpecialAudit.AuditStep = this.drpAuditStep.SelectedValue;
                    checkSpecialAudit.AuditMan = this.drpAuditMan.SelectedValue;
                }
                BLL.HSSE_Hazard_CheckSpecialAuditService.AddCheckSpecialAudit(checkSpecialAudit);
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)   //循环将专项检查主表Id更新至所有明细检查项
            {
                string id = this.Grid1.Rows[i].DataKeys[0].ToString();
                Model.HSSE_Hazard_HazardRegister hazardRegister = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(id);
                if (hazardRegister != null)
                {
                    hazardRegister.CheckSpecialId = checkSpecial.CheckSpecialId;
                    Funs.DB.SubmitChanges();
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
        /// 获取整改后图片
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

        #region  办理步骤变化事件
        /// <summary>
        /// 办理步骤变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpAuditStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpAuditStep.SelectedValue == Const.APPCheckSpecial_ApproveCompleted)
            {
                this.drpAuditMan.SelectedIndex = 0;
                this.drpAuditMan.Enabled = false;
            }
            else
            {
                this.drpAuditMan.Enabled = true;
                this.drpAuditMan.SelectedIndex = 0;
            }
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSee_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationView.aspx?HazardRegisterId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 右键删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        Model.HSSE_Hazard_HazardRegister hazardRegister = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(rowID);
                        if (hazardRegister != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, hazardRegister.HazardCode, hazardRegister.CheckSpecialId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnModify);
                            BLL.HSSE_Hazard_HazardRegisterService.DeleteHazardRegisterByHazardRegisterId(rowID);
                        }
                    }
                    this.BindGrid(Funs.GetNewDateTime(this.txtDate.Text.Trim()));
                    ShowNotify("删除成功!", MessageBoxIcon.Success);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string RegistrationId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.HSSE_Hazard_HazardRegister hazardRegister = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (e.CommandName == "del")
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, hazardRegister.HazardCode, hazardRegister.HazardRegisterId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete);
                    BLL.HSSE_Hazard_HazardRegisterService.DeleteHazardRegisterByHazardRegisterId(RegistrationId);                    
                    this.BindGrid(Funs.GetNewDateTime(this.txtDate.Text.Trim()));
                    ShowNotify("删除成功!", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion
    }
}