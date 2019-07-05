using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckSpecialDetailEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 专项检查明细id
        /// </summary>
        public string CheckSpecialDetailId
        {
            get
            {
                return (string)ViewState["CheckSpecialDetailId"];
            }
            set
            {
                ViewState["CheckSpecialDetailId"] = value;
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
                this.CheckSpecialDetailId = Request.Params["CheckSpecialDetailId"];
                if (!string.IsNullOrEmpty(this.CheckSpecialDetailId))
                {
                    var checkSpecialDetail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(this.CheckSpecialDetailId);
                    if (checkSpecialDetail != null)
                    {
                        var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(checkSpecialDetail.CheckSpecialId);
                        if (checkSpecial != null)
                        {
                            this.ProjectId = checkSpecial.ProjectId;
                            if (this.ProjectId != this.CurrUser.LoginProjectId)
                            {
                                this.InitDropDownList();
                            }
                        }

                        this.txtCheckItemType.Text = BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(checkSpecialDetail.CheckItem);
                        this.txtCheckItem.Text = checkSpecialDetail.CheckContent;
                        this.txtUnqualified.Text = checkSpecialDetail.Unqualified;
                        this.txtSuggestions.Text = checkSpecialDetail.Suggestions;
                        this.txtWorkArea.Text = checkSpecialDetail.WorkArea;
                        if (!string.IsNullOrEmpty(checkSpecialDetail.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkSpecialDetail.UnitId;
                        }
                        if (!string.IsNullOrEmpty(checkSpecialDetail.HandleStep))
                        {
                            this.drpHandleStep.SelectedValue = checkSpecialDetail.HandleStep;
                            if (checkSpecialDetail.HandleStep == "5")  //限时整改
                            {
                                this.txtLimitedDate.Enabled = true;
                                this.txtLimitedDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecialDetail.LimitedDate);
                            }
                        }
                        if (checkSpecialDetail.CompleteStatus == true)
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
        /// 下拉框
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
            Model.Check_CheckSpecialDetail detail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(this.CheckSpecialDetailId);
            if (detail != null)
            {
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
                detail.CheckContent = this.txtCheckItem.Text.Trim();
                BLL.Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(detail);
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckSpecial&menuId={1}", this.CheckSpecialDetailId, BLL.Const.ProjectCheckSpecialMenuId)));
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