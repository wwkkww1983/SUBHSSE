using System;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayDetailEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 日常巡检明细id
        /// </summary>
        public string CheckDayDetailId
        {
            get
            {
                return (string)ViewState["CheckDayDetailId"];
            }
            set
            {
                ViewState["CheckDayDetailId"] = value;
            }
        }

        /// <summary>
        /// 项目id
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

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.CheckDayDetailId = Request.Params["CheckDayDetailId"];
                if (!string.IsNullOrEmpty(this.CheckDayDetailId))
                {
                    var checkDayDetail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(this.CheckDayDetailId);
                    if (checkDayDetail != null)
                    {
                        var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(checkDayDetail.CheckDayId);
                        if (checkDay != null)
                        {
                            this.ProjectId = checkDay.ProjectId;
                            if (this.ProjectId != this.CurrUser.LoginProjectId)
                            {
                                this.InitDropDownList();
                            }
                        }

                        this.txtCheckItemType.Text = BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(checkDayDetail.CheckItem);
                        this.txtCheckItem.Text = checkDayDetail.CheckContent;
                        this.txtUnqualified.Text = checkDayDetail.Unqualified;
                        this.txtSuggestions.Text = checkDayDetail.Suggestions;
                        this.txtWorkArea.Text = checkDayDetail.WorkArea;
                        if (!string.IsNullOrEmpty(checkDayDetail.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkDayDetail.UnitId;
                        }
                        if (!string.IsNullOrEmpty(checkDayDetail.HandleStep))
                        {
                            this.drpHandleStep.SelectedValue = checkDayDetail.HandleStep;
                            if (checkDayDetail.HandleStep == "5")  //限时整改
                            {
                                this.txtLimitedDate.Enabled = true;
                                if (checkDayDetail.LimitedDate != null)
                                {
                                    this.txtLimitedDate.Text = string.Format("{0:yyyy-MM-dd}", checkDayDetail.LimitedDate);
                                }
                            }
                        }
                        if (checkDayDetail.CompleteStatus == true)
                        {
                            this.drpCompleteStatus.SelectedValue = "True";
                        }
                        else
                        {
                            this.drpCompleteStatus.SelectedValue = "False";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            ///区域下拉框
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkArea, this.ProjectId, true);
            ///单位下拉框
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
            ///处理措施
            BLL.ConstValue.InitConstValueDropDownList(this.drpHandleStep, ConstValue.Group_HandleStep, false);
            ///整改完成情况
            BLL.ConstValue.InitConstValueDropDownList(this.drpCompleteStatus, ConstValue.Group_0001, false);
            this.drpCompleteStatus.SelectedValue = "False";
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpHandleStep.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择处理措施！", MessageBoxIcon.Warning);
                return;
            }
            Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(this.CheckDayDetailId);
            if (detail != null)
            {
                detail.CheckContent = this.txtCheckItem.Text.Trim();
                detail.Unqualified = this.txtUnqualified.Text.Trim();
                detail.Suggestions = this.txtSuggestions.Text.Trim();
                detail.WorkArea = this.txtWorkArea.Text.Trim();
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    detail.UnitId = this.drpUnit.SelectedValue;
                }
                if (this.drpHandleStep.SelectedValue != BLL.Const._Null)
                {
                    detail.HandleStep = this.drpHandleStep.SelectedValue;
                }
                if (this.txtLimitedDate.Enabled == true)
                {
                    detail.LimitedDate = Funs.GetNewDateTime(this.txtLimitedDate.Text.Trim());
                }
                detail.CompleteStatus = Convert.ToBoolean(this.drpCompleteStatus.SelectedValue);
                BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}", this.CheckDayDetailId, BLL.Const.ProjectCheckDayMenuId)));
        }
        #endregion

        #region 选择处理结果
        /// <summary>
        /// 选择处理结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHandleStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpHandleStep.SelectedValue == "5")
            {
                this.txtLimitedDate.Enabled = true;
            }
            else
            {
                this.txtLimitedDate.Text = string.Empty;
                this.txtLimitedDate.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 区域选择框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWorkArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                this.txtWorkArea.Text = this.drpWorkArea.SelectedText;
            }
            else
            {
                this.txtWorkArea.Text = string.Empty;
            }
        }
    }
}