using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayDetailWHEdit :PageBase
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
                this.InitDropDownList();
                this.CheckDayDetailId = Request.Params["CheckDayDetailId"];
                if (!string.IsNullOrEmpty(this.CheckDayDetailId))
                {
                    var checkDayDetail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(this.CheckDayDetailId);
                    if (checkDayDetail != null)
                    {
                        this.txtUnqualified.Text = checkDayDetail.Unqualified;
                        this.txtWorkArea.Text = checkDayDetail.WorkArea;
                        if (checkDayDetail.LimitedDate.HasValue)
                        {
                            this.txtLimitedDate.Text = string.Format("{0:yyyy-MM-dd}", checkDayDetail.LimitedDate);
                        }
                        this.txtHiddenDangerType.Text = checkDayDetail.HiddenDangerType;
                        if (!string.IsNullOrEmpty(checkDayDetail.HiddenDangerLevel))
                        {
                            this.drpHiddenDangerLevel.SelectedValue = checkDayDetail.HiddenDangerLevel;
                            //this.txtHiddenDangerLevel.Text = checkDayDetail.HiddenDangerLevel;
                        }
                        if (!string.IsNullOrEmpty(checkDayDetail.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkDayDetail.UnitId;
                        }
                        if (!string.IsNullOrEmpty(checkDayDetail.PersonId))
                        {
                            this.drpPerson.SelectedValue = checkDayDetail.PersonId;
                        }
                        this.txtSuggestions.Text = checkDayDetail.Suggestions;
                        if (!string.IsNullOrEmpty(checkDayDetail.HandleStep))
                        {
                            this.drpHandleStep.SelectedValueArray = checkDayDetail.HandleStep.Split('|');
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
            //检查区域
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkArea, this.CurrUser.LoginProjectId, true);
            ///处理措施
            BLL.ConstValue.InitConstValueDropDownList(this.drpHandleStep, ConstValue.Group_HandleStep, false);
            ///单位下拉框
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, false);
            //责任人
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpPerson, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, true);
            //隐患级别
            BLL.ConstValue.InitConstValueDropDownList(this.drpHiddenDangerLevel, ConstValue.Group_HiddenDangerLevel, true);
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
            if (this.drpHiddenDangerLevel.SelectedValue==BLL.Const._Null)
            {
                Alert.ShowInTop("请选择隐患级别！", MessageBoxIcon.Warning);
                return;
            }
            Model.Check_CheckDayDetail detail = new Model.Check_CheckDayDetail();
            detail.Unqualified = this.txtUnqualified.Text.Trim();
            detail.WorkArea = this.txtWorkArea.Text.Trim();
            detail.LimitedDate = Funs.GetNewDateTime(this.txtLimitedDate.Text.Trim());
            detail.HiddenDangerType = this.txtHiddenDangerType.Text.Trim();
            if (this.drpHiddenDangerLevel.SelectedValue != BLL.Const._Null)
            {
                detail.HiddenDangerLevel = this.drpHiddenDangerLevel.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                detail.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpPerson.SelectedValue != BLL.Const._Null)
            {
                detail.PersonId = this.drpPerson.SelectedValue;
            }
            detail.Suggestions = this.txtSuggestions.Text.Trim();
            if (drpHandleStep.SelectedItem != null)
            {
                string hs = string.Empty;
                foreach (ListItem item in drpHandleStep.SelectedItemArray)
                {
                    hs += item.Value + "|";
                }
                if (!string.IsNullOrEmpty(hs))
                {
                    hs = hs.Substring(0, hs.LastIndexOf('|'));
                }
                detail.HandleStep = hs;
            }
            if (!string.IsNullOrEmpty(this.CheckDayDetailId))
            {
                detail.CheckDayDetailId = this.CheckDayDetailId;
                BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
            }
            else
            {
                detail.CheckDayId = Request.Params["CheckDayId"];
                this.CheckDayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayDetail));
                detail.CheckDayDetailId = this.CheckDayDetailId;
                BLL.Check_CheckDayDetailService.AddCheckDayDetail(detail);
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}", this.CheckDayDetailId, BLL.Const.ProjectCheckDayWHMenuId)));
        }
        #endregion

        #region 下拉选择事件
        /// <summary>
        /// 下拉选择责任单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                this.drpPerson.Items.Clear();
                BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpPerson, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, false);
            }
        }

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
        #endregion
    }
}