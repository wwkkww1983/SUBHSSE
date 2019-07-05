using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class ManageCheckView : PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ManageCheckId = Request.Params["ManageCheckId"];
                if (!string.IsNullOrEmpty(this.ManageCheckId))
                {
                    Model.Administrative_ManageCheck manageCheck = BLL.ManageCheckService.GetManageCheckById(this.ManageCheckId);
                    if (manageCheck != null)
                    {
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
                            var checkType = BLL.CheckTypeSetService.GetCheckTypeSetByCheckTypeCode(manageCheck.CheckTypeCode);
                            if (checkType != null)
                            {
                                this.txtCheckTypeCode.Text = checkType.CheckTypeContent;
                            }
                        }
                        this.txtSupplyCheck.Text = manageCheck.SupplyCheck;
                        if (manageCheck.IsSupplyCheck==true)
                        {
                            this.txtIsSupplyCheck.Text = "是";
                        }
                        else
                        {
                            this.txtIsSupplyCheck.Text = "否";
                        }
                        if (manageCheck.ViolationRule!=null)
                        {
                            var violationRule = BLL.ViolationRuleService.GetViolationRuleById(manageCheck.ViolationRule);
                            if (violationRule!=null)
                            {
                                this.txtViolationRule.Text = violationRule.ViolationRule;
                            }
                        }
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
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ManageCheckMenuId;
                this.ctlAuditFlow.DataId = this.ManageCheckId;
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
    }
}