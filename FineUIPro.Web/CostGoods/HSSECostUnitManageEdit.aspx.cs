using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageEdit : PageBase
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

                string unitId = this.CurrUser.UnitId;
                if (BLL.CommonService.GetIsThisUnit() != null)
                {
                    unitId = BLL.CommonService.GetIsThisUnit().UnitId;
                }
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpAuditManId, this.ProjectId, unitId, true);

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostUnitManageMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostUnitManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
                this.ctlAuditFlow.Visible = false;                
            }
            else
            {
                if (GetRequestEventArgument() == "HSSECostUnitManageItemWindowClose")
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpAuditManId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择总包费用审核安全工程师！", MessageBoxIcon.Warning);
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
            var updateCost = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(this.HSSECostUnitManageId);
            if (updateCost != null)
            {
                updateCost.CostA1 = Funs.GetNewDecimalOrZero(this.txtA1.Text.Trim());
                updateCost.CostA2 = Funs.GetNewDecimalOrZero(this.txtA2.Text.Trim());
                updateCost.CostA3 = Funs.GetNewDecimalOrZero(this.txtA3.Text.Trim());
                updateCost.CostA4 = Funs.GetNewDecimalOrZero(this.txtA4.Text.Trim());
                updateCost.CostA5 = Funs.GetNewDecimalOrZero(this.txtA5.Text.Trim());
                updateCost.CostA6 = Funs.GetNewDecimalOrZero(this.txtA6.Text.Trim());
                updateCost.CostA7 = Funs.GetNewDecimalOrZero(this.txtA7.Text.Trim());
                updateCost.CostA8 = Funs.GetNewDecimalOrZero(this.txtA8.Text.Trim());
                updateCost.CostB1 = Funs.GetNewDecimalOrZero(this.txtB1.Text.Trim());
                updateCost.CostB2 = Funs.GetNewDecimalOrZero(this.txtB2.Text.Trim());
                updateCost.CostC1 = Funs.GetNewDecimalOrZero(this.txtC1.Text.Trim());
                updateCost.CostD1 = Funs.GetNewDecimalOrZero(this.txtD1.Text.Trim());
                updateCost.CostD2 = Funs.GetNewDecimalOrZero(this.txtD2.Text.Trim());
                updateCost.CostD3 = Funs.GetNewDecimalOrZero(this.txtD3.Text.Trim());
                updateCost.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text);

                updateCost.AuditCostA1 = Funs.GetNewDecimalOrZero(this.txtA1.Text.Trim());
                updateCost.AuditCostA2 = Funs.GetNewDecimalOrZero(this.txtA2.Text.Trim());
                updateCost.AuditCostA3 = Funs.GetNewDecimalOrZero(this.txtA3.Text.Trim());
                updateCost.AuditCostA4 = Funs.GetNewDecimalOrZero(this.txtA4.Text.Trim());
                updateCost.AuditCostA5 = Funs.GetNewDecimalOrZero(this.txtA5.Text.Trim());
                updateCost.AuditCostA6 = Funs.GetNewDecimalOrZero(this.txtA6.Text.Trim());
                updateCost.AuditCostA7 = Funs.GetNewDecimalOrZero(this.txtA7.Text.Trim());
                updateCost.AuditCostA8 = Funs.GetNewDecimalOrZero(this.txtA8.Text.Trim());

                updateCost.AuditedSubUnitCost = (updateCost.CostA1 + updateCost.CostA2 + updateCost.CostA3 + updateCost.CostA4 + updateCost.CostA5
                                         + updateCost.CostA6 + updateCost.CostA7 + updateCost.CostA8) / 10000;

                updateCost.SubUnitCost = updateCost.AuditedSubUnitCost;
                if (this.drpAuditManId.SelectedValue != BLL.Const._Null)
                {
                    updateCost.AuditManId = this.drpAuditManId.SelectedValue;
                }

                ////单据状态
                updateCost.States = BLL.Const.State_0;
                if (type == BLL.Const.BtnSubmit)
                {
                    updateCost.States = BLL.Const.State_2;
                    updateCost.StateType = "2";
                   
                    Model.Sys_FlowOperate newNextFlowOperate = new Model.Sys_FlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate)),
                        MenuId = BLL.Const.ProjectHSSECostUnitManageAuditMenuId,
                        DataId = this.HSSECostUnitManageId + "#2",
                        ProjectId = this.ProjectId,
                        Url = "../CostGoods/HSSECostUnitManageAuditView.aspx?HSSECostUnitManageId={0}",
                        SortIndex = 1,
                        OperaterTime = System.DateTime.Now,
                        OperaterId = this.drpAuditManId.SelectedValue,
                        AuditFlowName = "安全工程师审核",
                        IsClosed = false,
                        State = BLL.Const.State_1,
                    };
                    Funs.DB.Sys_FlowOperate.InsertOnSubmit(newNextFlowOperate);
                    Funs.DB.SubmitChanges();
                }
                BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(updateCost);

                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSSECostUnitManageMenuId, this.HSSECostUnitManageId, (type == BLL.Const.BtnSubmit ? true : false),
                    BLL.UnitService.GetUnitNameByUnitId(updateCost.UnitId) + "费用上报", "../CostGoods/HSSECostUnitManageView.aspx?HSSECostUnitManageId={0}");

                if (type == BLL.Const.BtnSubmit)
                {
                    var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                                    where x.DataId == this.HSSECostUnitManageId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                                    select x;
                    if (updateNoClosedFlowOperate.Count() > 0)
                    {
                        foreach (var itemClosed in updateNoClosedFlowOperate)
                        {
                            itemClosed.OperaterId = this.CurrUser.UserId;
                            itemClosed.IsClosed = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
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
            string window = String.Format("HSSECostUnitManageItem.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn",""), "增加 - ");
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdHSSECostUnitManageId.ClientID)
              + Window1.GetShowReference(window));
        }
        #endregion
        
        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.InitTextValue();
        }
        #endregion

    }
}