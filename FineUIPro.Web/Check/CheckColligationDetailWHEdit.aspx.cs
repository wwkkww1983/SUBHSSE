using System;
using BLL;

namespace FineUIPro.Web.Check
{ 
    public partial class CheckColligationDetailWHEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 综合检查明细id
        /// </summary>
        public string CheckColligationDetailId
        {
            get
            {
                return (string)ViewState["CheckColligationDetailId"];
            }
            set
            {
                ViewState["CheckColligationDetailId"] = value;
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

        #region 加载
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
                this.CheckColligationDetailId = Request.Params["CheckColligationDetailId"];
                if (!string.IsNullOrEmpty(this.CheckColligationDetailId))
                {
                    var checkColligationDetail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(this.CheckColligationDetailId);
                    if (checkColligationDetail != null)
                    {
                        this.txtUnqualified.Text = checkColligationDetail.Unqualified;
                        this.txtWorkArea.Text = checkColligationDetail.WorkArea;
                        if (checkColligationDetail.LimitedDate.HasValue)
                        {
                            this.txtLimitedDate.Text = string.Format("{0:yyyy-MM-dd}", checkColligationDetail.LimitedDate);
                        }
                        this.txtHiddenDangerType.Text = checkColligationDetail.HiddenDangerType;
                        if (!string.IsNullOrEmpty(checkColligationDetail.HiddenDangerLevel))
                        {
                            this.drpHiddenDangerLevel.SelectedValue = checkColligationDetail.HiddenDangerLevel;
                            //this.txtHiddenDangerLevel.Text = checkColligationDetail.HiddenDangerLevel;
                        }
                        if (!string.IsNullOrEmpty(checkColligationDetail.UnitId))
                        {
                            this.drpUnit.SelectedValue = checkColligationDetail.UnitId;
                        }
                        if (!string.IsNullOrEmpty(checkColligationDetail.PersonId))
                        {
                            this.drpPerson.SelectedValue = checkColligationDetail.PersonId;
                        }
                        this.txtSuggestions.Text = checkColligationDetail.Suggestions;
                        if (!string.IsNullOrEmpty(checkColligationDetail.HandleStep))
                        {
                            this.drpHandleStep.SelectedValueArray = checkColligationDetail.HandleStep.Split('|');
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
        #endregion

        #region 保存
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

            if (this.drpHiddenDangerLevel.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择隐患级别！", MessageBoxIcon.Warning);
                return;
            }
            Model.Check_CheckColligationDetail detail = new Model.Check_CheckColligationDetail();
            detail.Unqualified = this.txtUnqualified.Text.Trim();
            detail.WorkArea = this.txtWorkArea.Text.Trim();
            detail.LimitedDate = Funs.GetNewDateTime(this.txtLimitedDate.Text.Trim());
            detail.HiddenDangerType = this.txtHiddenDangerType.Text.Trim();
            if (this.drpHiddenDangerLevel.SelectedValue!=BLL.Const._Null)
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
            if (!string.IsNullOrEmpty(this.CheckColligationDetailId))
            {
                detail.CheckColligationDetailId = this.CheckColligationDetailId;
                BLL.Check_CheckColligationDetailService.UpdateCheckColligationDetail(detail);
            }
            else
            {
                detail.CheckColligationId = Request.Params["CheckColligationId"];
                this.CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail));
                detail.CheckColligationDetailId = this.CheckColligationDetailId;
                BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(detail);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckColligation&menuId={1}", this.CheckColligationDetailId, BLL.Const.ProjectCheckColligationWHMenuId)));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 下拉选择责任单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
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