using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageAuditView : PageBase
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
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostUnitManageAuditMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostUnitManageId + "#2";
                this.ctlAuditFlow.Visible = false;
            }
            else
            {
                if (GetRequestEventArgument() == "AuditItemWindowClose")
                {                    
                    this.InitTextValue();
                }
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
                
                this.txtAuditCostA1.Text = uCost.AuditCostA1.ToString();
                this.txtAuditCostA2.Text = uCost.AuditCostA2.ToString();
                this.txtAuditCostA3.Text = uCost.AuditCostA3.ToString();
                this.txtAuditCostA4.Text = uCost.AuditCostA4.ToString();
                this.txtAuditCostA5.Text = uCost.AuditCostA5.ToString();
                this.txtAuditCostA6.Text = uCost.AuditCostA6.ToString();
                this.txtAuditCostA7.Text = uCost.AuditCostA7.ToString();
                this.txtAuditCostA8.Text = uCost.AuditCostA8.ToString();
                this.txtAuditCostAAll.Text = (uCost.CostA1 + uCost.AuditCostA2 + uCost.AuditCostA3
                    + uCost.AuditCostA4 + uCost.AuditCostA5 + uCost.AuditCostA6 + uCost.AuditCostA7
                    + uCost.AuditCostA8).ToString();

                var itemA1 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A1");
                if (itemA1 == null)
                {
                    this.lbA1.Text = "审核完成";
                }
                var itemA2 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A2");
                if (itemA2 == null)
                {
                    this.lbA2.Text = "审核完成";
                }
                var itemA3 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A3");
                if (itemA3 == null)
                {
                    this.lbA3.Text = "审核完成";
                }
                var itemA4 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A4");
                if (itemA4 == null)
                {
                    this.lbA4.Text = "审核完成";
                }
                var itemA5 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A5");
                if (itemA5 == null)
                {
                    this.lbA5.Text = "审核完成";
                }
                var itemA6 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A6");
                if (itemA6 == null)
                {
                    this.lbA6.Text = "审核完成";
                }
                var itemA7 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A7");
                if (itemA7 == null)
                {
                    this.lbA7.Text = "审核完成";
                }
                var itemA8 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsAgree.HasValue && x.Type == "A8");
                if (itemA8 == null)
                {
                    this.lbA8.Text = "审核完成";
                }
            }
        }
        #endregion


        #region 费用明细
        /// <summary>
        /// 费用明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUrl_Click(object sender, EventArgs e)
        {
            string window = String.Format("HSSECostUnitManageAuditItemView.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn", ""), "增加 - ");
            PageContext.RegisterStartupScript(Window1.GetShowReference(window));
        }
        #endregion
    }
}