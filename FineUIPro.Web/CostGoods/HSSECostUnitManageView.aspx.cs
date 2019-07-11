using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSSECostUnitManageId
        {
            get
            {
                return (string)ViewState["HSSECostUnitManageId"];
            }
            set
            {
                ViewState["HSSECostUnitManageId"] = value;
            }
        }
        /// <summary>
        /// 安全费用主键
        /// </summary>
        private string HSSECostManageId
        {
            get
            {
                return (string)ViewState["HSSECostManageId"];
            }
            set
            {
                ViewState["HSSECostManageId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.HSSECostUnitManageId = Request.Params["HSSECostUnitManageId"];
                if (!string.IsNullOrEmpty(this.HSSECostUnitManageId))
                {
                    this.InitTextValue();
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostUnitManageMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostUnitManageId;
            }
        }
        #endregion

        #region 初始化菜单输入框信息
        /// <summary>
        ///  初始化菜单输入框信息
        /// </summary>
        private void InitTextValue()
        {
            var uCost = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(this.HSSECostUnitManageId);
            if (uCost != null)
            {
                this.HSSECostManageId = uCost.HSSECostManageId;
                var hsseCostManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(uCost.HSSECostManageId);
                if (hsseCostManage != null)
                {
                    this.ProjectId = hsseCostManage.ProjectId;
                    this.lbYearMonth.Text = string.Format("{0:yyyy-MM}", hsseCostManage.Month);

                }

                this.lbUnit.Text = BLL.UnitService.GetUnitNameByUnitId(uCost.UnitId);
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", uCost.CompileDate);
                this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(uCost.CompileManId);                
                this.txtA1.Text = uCost.CostA1.ToString();
                this.txtA2.Text = uCost.CostA2.ToString();
                this.txtA3.Text = uCost.CostA3.ToString();
                this.txtA4.Text = uCost.CostA4.ToString();
                this.txtA5.Text = uCost.CostA5.ToString();
                this.txtA6.Text = uCost.CostA6.ToString();
                this.txtA7.Text = uCost.CostA7.ToString();
                this.txtA8.Text = uCost.CostA8.ToString();
                this.txtAAll.Text = (uCost.CostA1 + uCost.CostA2 + uCost.CostA3
                    + uCost.CostA4 + uCost.CostA5 + uCost.CostA6 + uCost.CostA7
                    + uCost.CostA8).ToString();

                this.txtB1.Text = uCost.CostB1.ToString();
                this.txtB2.Text = uCost.CostB2.ToString();
                this.txtBAll.Text = (uCost.CostB1 + uCost.CostB2).ToString();

                this.txtC1.Text = uCost.CostC1.ToString();
                this.txtCAll.Text = uCost.CostC1.ToString();

                this.txtD1.Text = uCost.CostD1.ToString();
                this.txtD2.Text = uCost.CostD2.ToString();
                this.txtD3.Text = uCost.CostD3.ToString();
                this.txtDAll.Text = (uCost.CostD1 + uCost.CostD2 + uCost.CostD3).ToString();
            }
            else
            {
                this.ContentPanel1.Hidden = true;
            }
        }
        #endregion
        
        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUrl_Click(object sender, EventArgs e)
        {
            string window = String.Format("HSSECostUnitManageItemView.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn",""), "增加 - ");
            PageContext.RegisterStartupScript(Window1.GetShowReference(window));
        }
        #endregion
        
    }
}