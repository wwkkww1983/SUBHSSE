using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Administrative
{
    public partial class ManageCheckEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ManageCheckId
        {
            get
            {
                return (string)ViewState["ManageCheckId"];
            }
            set
            {
                ViewState["ManageCheckId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.drpCheckTypeCode.DataValueField = "CheckTypeCode";
                this.drpCheckTypeCode.DataTextField = "CheckTypeContent";
                this.drpCheckTypeCode.DataSource = BLL.CheckTypeSetService.GetCheckTypeSetList();
                this.drpCheckTypeCode.DataBind();
                Funs.FineUIPleaseSelect(this.drpCheckTypeCode);

                this.drpViolationRule.DataValueField = "ViolationRuleId";
                this.drpViolationRule.DataTextField = "ViolationRule";
                this.drpViolationRule.DataSource = ViolationRuleService.GetViolationRuleList();
                this.drpViolationRule.DataBind();
                Funs.FineUIPleaseSelect(this.drpViolationRule);

                this.ManageCheckId = Request.Params["ManageCheckId"];
                if (!string.IsNullOrEmpty(this.ManageCheckId))
                {
                    Model.Administrative_ManageCheck manageCheck = BLL.ManageCheckService.GetManageCheckById(this.ManageCheckId);
                    if (manageCheck != null)
                    {
                        this.ProjectId = manageCheck.ProjectId;
                        this.Grid1.DataSource = BLL.CheckTypeSetService.GetCheckTypeSetsBySupCheckTypeCode(manageCheck.CheckTypeCode, ManageCheckId);
                        this.Grid1.PageIndex = 0;
                        this.Grid1.DataBind();
                        if (Grid1.Rows.Count > 0)
                        {
                            this.Grid1.Hidden = false;
                        }

                        this.txtManageCheckCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManageCheckId);
                        if (manageCheck.CheckTypeCode != null)
                        {
                            this.drpCheckTypeCode.SelectedValue = manageCheck.CheckTypeCode;
                        }
                        this.txtSupplyCheck.Text = manageCheck.SupplyCheck;
                        this.drpIsSupplyCheck.SelectedValue = Convert.ToString(manageCheck.IsSupplyCheck);
                        this.drpViolationRule.SelectedValue = Convert.ToString(manageCheck.ViolationRule);
                        this.txtCheckPerson.Text = manageCheck.CheckPerson;
                        if (manageCheck.CheckTime != null)
                        {
                            this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", manageCheck.CheckTime);
                        }
                        this.txtVerifyPerson.Text = manageCheck.VerifyPerson;
                        if (manageCheck.VerifyTime != null)
                        {
                            this.txtVerifyTime.Text = string.Format("{0:yyyy-MM-dd}", manageCheck.VerifyTime);
                        }
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtManageCheckCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ManageCheckMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtVerifyTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ManageCheckMenuId;
                this.ctlAuditFlow.DataId = this.ManageCheckId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Administrative_ManageCheck manageCheck = new Model.Administrative_ManageCheck
            {
                ProjectId = this.ProjectId,
                ManageCheckCode = this.txtManageCheckCode.Text.Trim()
            };
            if (this.drpCheckTypeCode.SelectedValue != BLL.Const._Null)
            {
                manageCheck.CheckTypeCode = this.drpCheckTypeCode.SelectedValue;
            }
            manageCheck.SupplyCheck = this.txtSupplyCheck.Text.Trim();
            manageCheck.IsSupplyCheck = Convert.ToBoolean(this.drpIsSupplyCheck.SelectedValue);
            manageCheck.ViolationRule = Funs.GetNewInt(this.drpViolationRule.SelectedValue);
            manageCheck.CheckPerson = this.txtCheckPerson.Text.Trim();
            manageCheck.CheckTime = Funs.GetNewDateTime(this.txtCheckTime.Text.Trim());
            manageCheck.VerifyPerson = this.txtVerifyPerson.Text.Trim();
            manageCheck.VerifyTime = Funs.GetNewDateTime(this.txtVerifyTime.Text.Trim());
            manageCheck.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                manageCheck.States = this.ctlAuditFlow.NextStep;
            }
            manageCheck.CompileMan = this.CurrUser.UserId;
            manageCheck.CompileDate = DateTime.Now;

            BLL.ManageCheckItemService.DeleteMangeCheckItemByManageCheckId(this.ManageCheckId);

            if (!string.IsNullOrEmpty(this.ManageCheckId))
            {
                manageCheck.ManageCheckId = this.ManageCheckId;
                BLL.ManageCheckService.UpdateManageCheck(manageCheck);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改行政管理检查记录");
            }
            else
            {
                this.ManageCheckId = SQLHelper.GetNewID(typeof(Model.Administrative_ManageCheck));
                manageCheck.ManageCheckId = this.ManageCheckId;
                BLL.ManageCheckService.AddManageCheck(manageCheck);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加行政管理检查记录");
            }
            GetGvManageCheck(this.ManageCheckId);
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ManageCheckMenuId, this.ManageCheckId, (type == BLL.Const.BtnSubmit ? true : false), this.drpCheckTypeCode.SelectedText, "../Administrative/ManageCheckView.aspx?ManageCheckId={0}");
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 添加行政管理检查内容
        /// </summary>
        private void GetGvManageCheck(string manageCheckId)
        {
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.Administrative_ManageCheckItem manageCheckItem = new Model.Administrative_ManageCheckItem();
                System.Web.UI.WebControls.HiddenField hdCheckTypeCode = (System.Web.UI.WebControls.HiddenField)(this.Grid1.Rows[i].FindControl("hdCheckTypeCode"));
                string isCheck = ((System.Web.UI.WebControls.DropDownList)(this.Grid1.Rows[i].FindControl("drpIsCheck"))).SelectedValue;
                manageCheckItem.ManageCheckId = manageCheckId;
                manageCheckItem.CheckTypeCode = hdCheckTypeCode.Value;
                manageCheckItem.IsCheck = Convert.ToBoolean(isCheck);
                manageCheckItem.ManageCheckItemId = SQLHelper.GetNewID(typeof(Model.Administrative_ManageCheckItem));
                BLL.ManageCheckItemService.AddManageCheckItem(manageCheckItem);
            }
        }
        #endregion

        #region Grid绑定行
        /// <summary>
        /// Grid绑定行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                System.Web.UI.WebControls.HiddenField hdIsCheck = (System.Web.UI.WebControls.HiddenField)(this.Grid1.Rows[i].FindControl("hdIsCheck"));
                System.Web.UI.WebControls.DropDownList drpIsCheck = (System.Web.UI.WebControls.DropDownList)(this.Grid1.Rows[i].FindControl("drpIsCheck"));
                if (hdIsCheck.Value == "True")
                {
                    drpIsCheck.SelectedValue = "True";
                }
                else
                {
                    drpIsCheck.SelectedValue = "False";
                }
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取检查内容
        /// </summary>
        /// <param name="checkTypeCode"></param>
        /// <returns></returns>
        protected string ConvertCheckType(object checkTypeCode)
        {
            string checkTypeName = string.Empty;
            if (checkTypeCode != null)
            {
                var checkType = BLL.CheckTypeSetService.GetCheckTypeSetByCheckTypeCode(checkTypeCode.ToString());
                if (checkType != null)
                {
                    checkTypeName = checkType.CheckTypeContent;
                }
            }
            return checkTypeName;
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        /// 检查类别下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCheckTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpCheckTypeCode.SelectedValue != BLL.Const._Null)
            {
                this.Grid1.DataSource = BLL.CheckTypeSetService.GetCheckTypeSetsBySupCheckTypeCode(this.drpCheckTypeCode.SelectedValue, this.ManageCheckId);
                this.Grid1.PageIndex = 0;
                this.Grid1.DataBind();
                if (this.Grid1.Rows.Count > 0)
                {
                    this.Grid1.Hidden = false;
                }
                else
                {
                    this.Grid1.Hidden = true;
                }
            }
            else
            {
                this.Grid1.DataSource = null;
                this.Grid1.DataBind();
                this.Grid1.Hidden = true;
            }
        }

        /// <summary>
        /// 选择是否补充行政管理检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpIsSupplyCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.drpIsSupplyCheck.SelectedValue))
            {
                this.txtSupplyCheck.Enabled = true;
            }
            else
            {
                this.txtSupplyCheck.Text = string.Empty;
                this.txtSupplyCheck.Enabled = false;
            }
        }
        #endregion
    }
}