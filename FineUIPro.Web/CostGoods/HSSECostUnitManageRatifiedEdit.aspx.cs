using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageRatifiedEdit : PageBase
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

                string unitId = this.CurrUser.UnitId;
                if (BLL.CommonService.GetIsThisUnit() != null)
                {
                    unitId = BLL.CommonService.GetIsThisUnit().UnitId;
                }
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpAuditManId, this.ProjectId, unitId, true);

                ///初始化核定菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostUnitManageRatifiedMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostUnitManageId + "#3";
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
                this.ctlAuditFlow.Visible = false;
            }
            else
            {
                if (GetRequestEventArgument() == "RatifiedItemWindowClose")
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
                this.txtAuditCostAAll.Text = (uCost.AuditCostA1 + uCost.AuditCostA2 + uCost.AuditCostA3
                    + uCost.AuditCostA4 + uCost.AuditCostA5 + uCost.AuditCostA6 + uCost.AuditCostA7
                    + uCost.AuditCostA8).ToString();

                this.txtRatifiedCostA1.Text = uCost.RatifiedCostA1.ToString();
                this.txtRatifiedCostA2.Text = uCost.RatifiedCostA2.ToString();
                this.txtRatifiedCostA3.Text = uCost.RatifiedCostA3.ToString();
                this.txtRatifiedCostA4.Text = uCost.RatifiedCostA4.ToString();
                this.txtRatifiedCostA5.Text = uCost.RatifiedCostA5.ToString();
                this.txtRatifiedCostA6.Text = uCost.RatifiedCostA6.ToString();
                this.txtRatifiedCostA7.Text = uCost.RatifiedCostA7.ToString();
                this.txtRatifiedCostA8.Text = uCost.RatifiedCostA8.ToString();
                this.txtRatifiedCostAAll.Text = (uCost.RatifiedCostA1 + uCost.RatifiedCostA2 + uCost.RatifiedCostA3
                    + uCost.RatifiedCostA4 + uCost.RatifiedCostA5 + uCost.RatifiedCostA6 + uCost.RatifiedCostA7
                    + uCost.RatifiedCostA8).ToString();

                var itemA1 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A1");
                if (itemA1 == null)
                {
                    this.lbA1.Text = "核定完成";
                }
                var itemA2 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A2");
                if (itemA2 == null)
                {
                    this.lbA2.Text = "核定完成";
                }
                var itemA3 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A3");
                if (itemA3 == null)
                {
                    this.lbA3.Text = "核定完成";
                }
                var itemA4 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A4");
                if (itemA4 == null)
                {
                    this.lbA4.Text = "核定完成";
                }
                var itemA5 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A5");
                if (itemA5 == null)
                {
                    this.lbA5.Text = "核定完成";
                }
                var itemA6 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A6");
                if (itemA6 == null)
                {
                    this.lbA6.Text = "核定完成";
                }
                var itemA7 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A7");
                if (itemA7 == null)
                {
                    this.lbA7.Text = "核定完成";
                }
                var itemA8 = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && !x.IsRatified.HasValue && x.Type == "A8");
                if (itemA8 == null)
                {
                    this.lbA8.Text = "核定完成";
                }

                var hSSECostManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(uCost.HSSECostManageId);
                if (hSSECostManage != null && !string.IsNullOrEmpty(hSSECostManage.AuditManId))
                {
                    this.drpAuditManId.SelectedValue = hSSECostManage.AuditManId;
                    this.drpAuditManId.Readonly = true;
                }
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

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            if (!IsEnbleSubmit())
            {
                ShowNotify("安全费用明细未核定完成，不能提交！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpAuditManId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择总包财务经理！", MessageBoxIcon.Warning);
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

                updateCost.AuditedSubUnitCost = (updateCost.RatifiedCostA1 + updateCost.RatifiedCostA2 + updateCost.RatifiedCostA3 + updateCost.RatifiedCostA4 + updateCost.RatifiedCostA5
                                         + updateCost.RatifiedCostA6 + updateCost.RatifiedCostA7 + updateCost.RatifiedCostA8) / 10000;
                updateCost.SubUnitCost = updateCost.AuditedSubUnitCost;
            ////单据状态
            updateCost.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                updateCost.States = BLL.Const.State_2;
                updateCost.StateType = "4";
            }
            BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(updateCost);

            var hSSECostManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(updateCost.HSSECostManageId);
            if (hSSECostManage != null && string.IsNullOrEmpty(hSSECostManage.States))
            {
                ////单据状态
                hSSECostManage.States = BLL.Const.State_0;
                if (type == BLL.Const.BtnSubmit)
                {                  
                    Model.Sys_FlowOperate newNextFlowOperate = new Model.Sys_FlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate)),
                        MenuId = BLL.Const.ProjectHSSECostManageMenuId,
                        DataId = hSSECostManage.HSSECostManageId,
                        ProjectId = this.ProjectId,
                        Url = "../CostGoods/HSSECostManageView.aspx?HSSECostManageId={0}",
                        SortIndex = 1,
                        OperaterTime = System.DateTime.Now,
                        OperaterId = this.drpAuditManId.SelectedValue,
                        AuditFlowName = "总包财务经理",
                        IsClosed = false,
                        State = BLL.Const.State_1,
                    };
                    Funs.DB.Sys_FlowOperate.InsertOnSubmit(newNextFlowOperate);
                    Funs.DB.SubmitChanges();
                }

                if (this.drpAuditManId.SelectedValue != BLL.Const._Null)
                {
                    hSSECostManage.AuditManId = this.drpAuditManId.SelectedValue;                    
                }

                BLL.HSSECostManageService.UpdateHSSECostManage(hSSECostManage);
            }
               
            ////保存流程核定数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSSECostUnitManageRatifiedMenuId, this.HSSECostUnitManageId + "#3", (type == BLL.Const.BtnSubmit ? true : false),
                BLL.UnitService.GetUnitNameByUnitId(updateCost.UnitId) + "费用核定", "../CostGoods/HSSECostUnitManageRatifiedView.aspx?HSSECostUnitManageId={0}");

            if (type == BLL.Const.BtnSubmit)
            {               
                var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                                where x.DataId == this.HSSECostUnitManageId + "#3" && (x.IsClosed == false || !x.IsClosed.HasValue)
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
        #endregion

        #region 费用明细
        /// <summary>
        /// 费用明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUrl_Click(object sender, EventArgs e)
        {
            string window = String.Format("HSSECostUnitManageRatifiedItem.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn",""), "增加 - ");
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

        #region 判断是否所有明细已核定完成
        /// <summary>
        /// 判断是否所有明细已核定完成
        /// </summary>
        /// <returns></returns>
        private bool IsEnbleSubmit()
        {
            bool isEnble = true;
            var item = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && x.Type.Contains("A") && !x.IsRatified.HasValue);
            if (item != null)
            {
                isEnble = false;
            }

            return isEnble; 
        }
        #endregion
    }
}