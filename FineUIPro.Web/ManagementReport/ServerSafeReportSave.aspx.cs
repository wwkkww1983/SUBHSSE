using System;
using System.Linq;
using BLL;
using Model;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ServerSafeReportSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全文件上报主键
        /// </summary>
        public string SafeReportId
        {
            get
            {
                return (string)ViewState["SafeReportId"];
            }
            set
            {
                ViewState["SafeReportId"] = value;
            }
        }
        /// <summary>
        /// 安全文件上报父级节点主键
        /// </summary>
        public string SupSafeReportId
        {
            get
            {
                return (string)ViewState["SupSafeReportId"];
            }
            set
            {
                ViewState["SupSafeReportId"] = value;
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
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsEndLever, BLL.ConstValue.Group_0001, false);
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.CurrUser.LoginProjectId, true);
                this.SafeReportId = Request.QueryString["SafeReportId"];
                this.SupSafeReportId = Request.QueryString["SupSafeReportId"];
                if (!String.IsNullOrEmpty(this.SafeReportId))
                {
                    var safeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(this.SafeReportId);
                    if (safeReport != null)
                    {
                        this.txtSafeReportCode.Text = safeReport.SafeReportCode;
                        this.txtSafeReportName.Text = safeReport.SafeReportName;
                        this.SupSafeReportId = safeReport.SupSafeReportId;
                        if (!string.IsNullOrEmpty(safeReport.CompileManId))
                        {
                            this.drpCompileMan.SelectedValue = safeReport.CompileManId;
                        }
                        if (safeReport.IsEndLever.HasValue)
                        {
                            this.drpIsEndLever.SelectedValue = Convert.ToString(safeReport.IsEndLever);
                        }
                        this.txtCompileTime.Text = string.Format("{0:yyyy-MM-dd}", safeReport.CompileTime);
                        this.txtRequestTime.Text = string.Format("{0:yyyy-MM-dd}", safeReport.RequestTime);
                        this.txtRequirement.Text = safeReport.Requirement;
                        if (safeReport.States == BLL.Const.State_1)
                        {
                            this.btnSave.Hidden = true;
                            this.btnSubmit.Hidden = true;
                        }
                    }
                }
                else
                {
                    this.txtCompileTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                }

                this.SetHidden();
            }
        }
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取权限按钮事件
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerSafeReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 保存方法
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Manager_SafeReport newSafeReport = new Manager_SafeReport
            {
                SafeReportCode = txtSafeReportCode.Text.Trim(),
                SafeReportName = txtSafeReportName.Text.Trim(),
                IsEndLever = Convert.ToBoolean(drpIsEndLever.SelectedValue),
                SupSafeReportId = this.SupSafeReportId,
                CompileTime = Funs.GetNewDateTime(this.txtCompileTime.Text),
                RequestTime = Funs.GetNewDateTime(this.txtRequestTime.Text),
                Requirement = this.txtRequirement.Text.Trim(),
                States = BLL.Const.State_0,
            };

            if (!String.IsNullOrEmpty(this.drpCompileMan.SelectedValue) && this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newSafeReport.CompileManId = this.drpCompileMan.SelectedValue;
            }
            if (type == BLL.Const.BtnSubmit)
            {
                newSafeReport.States = BLL.Const.State_1;
                this.UpdateSupSafeReportStates(this.SupSafeReportId); ///更新父级节点状态
            }

            if (String.IsNullOrEmpty(this.SafeReportId))
            {
                this.SafeReportId = newSafeReport.SafeReportId = SQLHelper.GetNewID(typeof(Model.Manager_SafeReport));
                BLL.SafeReportService.AddSafeReport(newSafeReport);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafeReport.SafeReportCode, newSafeReport.SafeReportId,BLL.Const.ServerSafeReportMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                newSafeReport.SafeReportId = this.SafeReportId;
                BLL.SafeReportService.UpdateSafeReport(newSafeReport);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafeReport.SafeReportCode, newSafeReport.SafeReportId, BLL.Const.ServerSafeReportMenuId, BLL.Const.BtnModify);
            }
            
            ////如果是末级节点则自动生成明细表
            if (newSafeReport.IsEndLever == true && type == BLL.Const.BtnSubmit)
            {
                this.SaveSafeReportItemData();
            }
        }
        #endregion

        #region 如果是末级节点则自动生成明细表
        /// <summary>
        /// 自动生成明细表
        /// </summary>
        private void SaveSafeReportItemData()
        {
            var SafeReportItem = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == this.SafeReportId);
            if (SafeReportItem == null)
            {
                var projects = BLL.ProjectService.GetProjectWorkList(); ///施工中的项目
                if (projects.Count() > 0)
                {
                    foreach (var project in projects)
                    {
                        Model.Manager_SafeReportItem newSafeReportItem = new Model.Manager_SafeReportItem();
                        newSafeReportItem.SafeReportItemId = SQLHelper.GetNewID(typeof(Model.Manager_SafeReportItem));
                        newSafeReportItem.SafeReportId = this.SafeReportId;
                        newSafeReportItem.ProjectId = project.ProjectId;
                        newSafeReportItem.States = BLL.Const.State_0;
                        BLL.SafeReportItemService.AddSafeReportItem(newSafeReportItem);
                    }
                }

                var units = BLL.UnitService.GetBranchUnitList(); ///分公司
                if (units.Count() > 0)
                {
                    foreach (var unitItem in units)
                    {
                        Model.Manager_SafeReportUnitItem newSafeReportUnitItem = new Model.Manager_SafeReportUnitItem();
                        newSafeReportUnitItem.SafeReportUnitItemId = SQLHelper.GetNewID(typeof(Model.Manager_SafeReportUnitItem));
                        newSafeReportUnitItem.SafeReportId = this.SafeReportId;
                        newSafeReportUnitItem.UnitId = unitItem.UnitId;
                        newSafeReportUnitItem.States = BLL.Const.State_0;
                        BLL.SafeReportUnitItemService.AddSafeReportUnitItem(newSafeReportUnitItem);
                    }
                }
            }
        }
        #endregion

        #region 是否末级联动事件
        /// <summary>
        /// 是否末级联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpIsEndLever_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetHidden();
        }

        private void SetHidden()
        {
            if (Convert.ToBoolean(drpIsEndLever.SelectedValue))
            {
                this.SimpleForm2.Hidden = false;
            }
            else
            {
                this.SimpleForm2.Hidden = true;
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
            if (string.IsNullOrEmpty(this.SafeReportId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ServerSafeReportAttachUrl&menuId={1}&type=-1", this.SafeReportId, BLL.Const.ServerSafeReportMenuId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ServerSafeReportAttachUrl&menuId={1}", this.SafeReportId, BLL.Const.ServerSafeReportMenuId)));
            }
        }
        #endregion

        #region 更新父级为已提交状态
        /// <summary>
        /// 子级提交父级状态一并更新
        /// </summary>
        /// <param name="supSafeReportId"></param>
        private void UpdateSupSafeReportStates(string supSafeReportId)
        {
            if (supSafeReportId != "0")
            {
                var supSafeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(supSafeReportId);
                if (supSafeReport != null)
                {
                    if (supSafeReport.States == BLL.Const.State_0)
                    {
                        supSafeReport.States = BLL.Const.State_1;
                        BLL.SafeReportService.UpdateSafeReport(supSafeReport);
                        this.UpdateSupSafeReportStates(supSafeReport.SupSafeReportId);
                    }
                }
            }
        }

        #endregion
    }
}