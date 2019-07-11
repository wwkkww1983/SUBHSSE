using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageAuditEdit : PageBase
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
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpRatifiedManId, this.ProjectId, unitId, true);

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostUnitManageAuditMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostUnitManageId + "#2";
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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

                this.txtAuditCostAAll.Text = (uCost.AuditCostA1 + uCost.AuditCostA2 + uCost.AuditCostA3
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
                ShowNotify("安全费用明细未审核完成，不能提交！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpRatifiedManId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择总包费用核定费控工程师！", MessageBoxIcon.Warning);
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
                updateCost.RatifiedCostA1 = updateCost.AuditCostA1;
                updateCost.RatifiedCostA2 = updateCost.AuditCostA2;
                updateCost.RatifiedCostA3 = updateCost.AuditCostA3;
                updateCost.RatifiedCostA4 = updateCost.AuditCostA4;
                updateCost.RatifiedCostA5 = updateCost.AuditCostA5;
                updateCost.RatifiedCostA6 = updateCost.AuditCostA6;
                updateCost.RatifiedCostA7 = updateCost.AuditCostA7;
                updateCost.RatifiedCostA8 = updateCost.AuditCostA8;

                updateCost.AuditedSubUnitCost = (updateCost.AuditCostA1 + updateCost.AuditCostA2 + updateCost.AuditCostA3 + updateCost.AuditCostA4 + updateCost.AuditCostA5
                                             + updateCost.AuditCostA6 + updateCost.AuditCostA7 + updateCost.AuditCostA8) / 10000;
                updateCost.SubUnitCost = updateCost.AuditedSubUnitCost;
                if (this.drpRatifiedManId.SelectedValue != BLL.Const._Null)
                {
                    updateCost.RatifiedManId = this.drpRatifiedManId.SelectedValue;
                }

                ////单据状态
                updateCost.States = BLL.Const.State_0;
                if (type == BLL.Const.BtnSubmit)
                {
                    updateCost.States = BLL.Const.State_2;
                    updateCost.StateType = "3";

                    Model.Sys_FlowOperate newNextFlowOperate = new Model.Sys_FlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate)),
                        MenuId = BLL.Const.ProjectHSSECostUnitManageAuditMenuId,
                        DataId = this.HSSECostUnitManageId + "#3",
                        ProjectId = this.ProjectId,
                        Url = "../CostGoods/HSSECostUnitManageRatifiedView.aspx?HSSECostUnitManageId={0}",
                        SortIndex = 1,
                        OperaterTime = System.DateTime.Now,
                        OperaterId = this.drpRatifiedManId.SelectedValue,
                        AuditFlowName = "费控工程师审核",
                        IsClosed = false,
                        State = BLL.Const.State_1,
                    };
                    Funs.DB.Sys_FlowOperate.InsertOnSubmit(newNextFlowOperate);
                    Funs.DB.SubmitChanges();
                }
                BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(updateCost);

                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSSECostUnitManageAuditMenuId, this.HSSECostUnitManageId + "#2", (type == BLL.Const.BtnSubmit ? true : false),
                    BLL.UnitService.GetUnitNameByUnitId(updateCost.UnitId) + "费用审核", "../CostGoods/HSSECostUnitManageAuditView.aspx?HSSECostUnitManageId={0}");

                if (type == BLL.Const.BtnSubmit)
                {
                    var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                                    where x.DataId == this.HSSECostUnitManageId + "#2" && (x.IsClosed == false || !x.IsClosed.HasValue)
                                                    select x;
                    if (updateNoClosedFlowOperate.Count() > 0)
                    {
                        foreach (var item in updateNoClosedFlowOperate)
                        {
                            item.OperaterId = this.CurrUser.UserId;
                            item.IsClosed = true;
                            Funs.DB.SubmitChanges();
                        }
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
            string window = String.Format("HSSECostUnitManageAuditItem.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn",""), "增加 - ");
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

        #region 判断是否所有明细已审核完成
        /// <summary>
        /// 判断是否所有明细已审核完成
        /// </summary>
        /// <returns></returns>
        private bool IsEnbleSubmit()
        {
            bool isEnble = true;
            var item = Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageId == this.HSSECostUnitManageId && x.Type.Contains("A") && !x.IsAgree.HasValue);
            if (item != null)
            {
                isEnble = false;
            }

            return isEnble; 
        }
        #endregion
    }
}