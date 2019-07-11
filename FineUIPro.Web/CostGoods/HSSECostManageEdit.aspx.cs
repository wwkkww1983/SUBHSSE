using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostManageEdit : PageBase
    {
        #region 定义项    
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
        /// <summary>
        /// 施工单位安全费用主键
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
        #endregion

        #region 加载页面
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.InitDropDownList();
                this.HSSECostManageId = Request.Params["HSSECostManageId"];
                var hsseCostManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(this.HSSECostManageId);
                if (hsseCostManage != null)
                {
                    this.ProjectId = hsseCostManage.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    if (hsseCostManage.Month.HasValue)
                    {
                        this.drpYear.SelectedValue = Convert.ToString(hsseCostManage.Month.Value.Year);
                        this.drpMonths.SelectedValue = Convert.ToString(hsseCostManage.Month.Value.Month);                       
                    }
                }
                else
                {
                    this.drpYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                    this.drpMonths.SelectedValue = Convert.ToString(DateTime.Now.Month);
                    this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHSSECostManageMenuId, this.ProjectId, string.Empty);
                }

                this.InitTextValue();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSSECostManageMenuId;
                this.ctlAuditFlow.DataId = this.HSSECostManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.ProjectId, BLL.Const.ProjectUnitType_2, false);
            BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, false);
            BLL.ConstValue.InitConstValueDropDownList(this.drpMonths, BLL.ConstValue.Group_0009, false);
        }
        
        #region 变化事件
        /// <summary>
        /// 变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                 this.InitTextValue();
            }
        }
        #endregion

        #region 初始化菜单输入框信息
        /// <summary>
        /// 
        /// </summary>
        private void InitTextValue()
        {
            string unitId = this.drpUnitId.SelectedValue;
            int year = Funs.GetNewIntOrZero( this.drpYear.SelectedValue);
            int month = Funs.GetNewIntOrZero(this.drpMonths.SelectedValue);
            DateTime? Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
            this.HSSECostManageId = string.Empty;
            this.HSSECostUnitManageId = string.Empty;
            decimal pMainIncome = 0, pConstructionIncome = 0, pSafetyCosts = 0;
            var sumHsseCostMange = BLL.HSSECostManageService.GetListHSSECostManageByProjectIdMonth(this.ProjectId, Months);
            if (sumHsseCostMange.Count() > 0)
            {
                pMainIncome = sumHsseCostMange.Sum(x => x.MainIncome) ?? 0;
                pConstructionIncome = sumHsseCostMange.Sum(x => x.ConstructionIncome) ?? 0;
                pSafetyCosts = sumHsseCostMange.Sum(x => x.SafetyCosts)?? 0;
            }
            Model.CostGoods_HSSECostUnitManage hsseCostUnitManage = new Model.CostGoods_HSSECostUnitManage();
            var hsseCostManage = BLL.HSSECostManageService.GetHSSECostManageByProjectIdMonth(ProjectId, year, month);
            if (hsseCostManage != null)
            {
                this.HSSECostManageId = hsseCostManage.HSSECostManageId;
                this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(hsseCostManage.HSSECostManageId);
                this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", hsseCostManage.ReportDate);
                this.txtMainIncome.Text =hsseCostManage.MainIncome.ToString();
                this.txtProjectMainIncome.Text = (pMainIncome + (hsseCostManage.MainIncome ?? 0)).ToString();
                this.txtRemark1.Text = hsseCostManage.Remark1;
                this.txtConstructionIncome.Text =hsseCostManage.ConstructionIncome.ToString();
                this.txtProjectConstructionIncome.Text = (pConstructionIncome + (hsseCostManage.ConstructionIncome ?? 0)).ToString();
                this.txtRemark2.Text = hsseCostManage.Remark2;
                this.txtSafetyCosts.Text = hsseCostManage.SafetyCosts.ToString();
                this.txtProjectSafetyCosts.Text = (pSafetyCosts + (hsseCostManage.SafetyCosts ?? 0)).ToString();
                this.txtRemark3.Text = hsseCostManage.Remark3;
                hsseCostUnitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostManageIdUnitId(this.HSSECostManageId, this.drpUnitId.SelectedValue);
                if (hsseCostManage.States == BLL.Const.State_2)
                {
                    this.btnSave.Hidden = true;
                    this.btnSubmit.Hidden = true;
                }
            }
            else
            {
                this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHSSECostManageMenuId, this.ProjectId, string.Empty);
                this.txtMainIncome.Text = "0";
                this.txtConstructionIncome.Text = "0";
                this.txtSafetyCosts.Text = "0";

                this.txtProjectMainIncome.Text = pMainIncome.ToString();
                this.txtProjectConstructionIncome.Text = pConstructionIncome.ToString();
                this.txtProjectSafetyCosts.Text = pSafetyCosts.ToString();
                hsseCostUnitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByProjectIdUnitIdYearMonth(this.ProjectId, this.drpUnitId.SelectedValue, year, month);
            }

            this.InitTextUnitValue(hsseCostUnitManage);
        }

        /// <summary>
        ///  初始化菜单输入框信息
        /// </summary>
        private void InitTextUnitValue(Model.CostGoods_HSSECostUnitManage hsseCostUnitManage)
        {         
            decimal? a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, aall = 0;
            decimal? pa1 = 0, pa2 = 0, pa3 = 0, pa4 = 0, pa5 = 0, pa6 = 0, pa7 = 0, pa8 = 0, paall = 0;
            decimal? EngineeringCost = 0, SubUnitCost = 0, AuditedSubUnitCost = 0;
            decimal? pEngineeringCost = 0, pSubUnitCost = 0, pAuditedSubUnitCost = 0;
            string remark1 = string.Empty, remark2 = string.Empty, remark3 = string.Empty, remark4=string.Empty;
            DateTime? Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
            var sumUnit =  BLL.HSSECostUnitManageService.GetHSSECostUnitManageListByProjectIdUnitIdMonth(this.ProjectId, this.drpUnitId.SelectedValue, Months);
            if (sumUnit.Count() > 0)
            {
                pa1 = sumUnit.Sum(x => x.RatifiedCostA1);
                pa2 = sumUnit.Sum(x => x.RatifiedCostA2);
                pa3 = sumUnit.Sum(x => x.RatifiedCostA3);
                pa4 = sumUnit.Sum(x => x.RatifiedCostA4);
                pa5 = sumUnit.Sum(x => x.RatifiedCostA5);
                pa6 = sumUnit.Sum(x => x.RatifiedCostA6);
                pa7 = sumUnit.Sum(x => x.RatifiedCostA7);
                pa8 = sumUnit.Sum(x => x.RatifiedCostA8);
                paall = pa1 + pa2 + pa3 + pa4 + pa5 + pa6 + pa7 + pa8;               
                pEngineeringCost = sumUnit.Sum(x => EngineeringCost ?? 0);
                pSubUnitCost = sumUnit.Sum(x => SubUnitCost ?? 0);
                pAuditedSubUnitCost = sumUnit.Sum(x => AuditedSubUnitCost ?? 0);
            }

            if (hsseCostUnitManage != null)
            {
                this.HSSECostUnitManageId = hsseCostUnitManage.HSSECostUnitManageId;
                a1 = hsseCostUnitManage.RatifiedCostA1;
                pa1 += a1;
                aall += a1;
                a2 = hsseCostUnitManage.RatifiedCostA2;
                pa2 += a2;
                aall += a2;
                a3 = hsseCostUnitManage.RatifiedCostA3;
                pa3 += a3;
                aall += a3;
                a4 = hsseCostUnitManage.RatifiedCostA4;
                pa4 += a4;
                aall += a4;
                a5 = hsseCostUnitManage.RatifiedCostA5;
                pa5 += a5;
                aall += a5;
                a6 = hsseCostUnitManage.RatifiedCostA6;
                pa6 += a6;
                aall += a6;
                a7 = hsseCostUnitManage.RatifiedCostA7;
                pa7 += a7;
                aall += a7;
                a8 = hsseCostUnitManage.RatifiedCostA8;
                pa8 += a8;
                aall += a8;
                paall += aall;
                EngineeringCost = hsseCostUnitManage.EngineeringCost;
                pEngineeringCost += EngineeringCost;

                SubUnitCost = hsseCostUnitManage.SubUnitCost;
                pSubUnitCost += SubUnitCost;

                AuditedSubUnitCost = hsseCostUnitManage.AuditedSubUnitCost;
                pAuditedSubUnitCost += AuditedSubUnitCost;

                remark1 = hsseCostUnitManage.Remark1;
                remark2 = hsseCostUnitManage.Remark2;
                remark3 = hsseCostUnitManage.Remark3;
                remark4 = hsseCostUnitManage.Remark4;
            }

            this.txtA1.Text = a1.ToString();
            this.txtA2.Text = a2.ToString();
            this.txtA3.Text = a3.ToString();
            this.txtA4.Text = a4.ToString();
            this.txtA5.Text = a5.ToString();
            this.txtA6.Text = a6.ToString();
            this.txtA7.Text = a7.ToString();
            this.txtA8.Text = a8.ToString();
            this.txtAAll.Text = aall.ToString();

            this.txtProjectA1.Text = pa1.ToString();
            this.txtProjectA2.Text = pa2.ToString();
            this.txtProjectA3.Text = pa3.ToString();
            this.txtProjectA4.Text = pa4.ToString();
            this.txtProjectA5.Text = pa5.ToString();
            this.txtProjectA6.Text = pa6.ToString();
            this.txtProjectA7.Text = pa7.ToString();
            this.txtProjectA8.Text = pa8.ToString();
            this.txtProjectAAll.Text = paall.ToString();

            this.txtEngineeringCost.Text = EngineeringCost.ToString();
            this.txtSubUnitCost.Text = SubUnitCost.ToString();
            this.txtAuditedSubUnitCost.Text = AuditedSubUnitCost.ToString();

            this.txtUnitRemark1.Text = remark1;
            this.txtUnitRemark2.Text = remark2;
            this.txtUnitRemark3.Text = remark3;
            this.txtUnitRemark4.Text = remark4;

            this.txtProjectEngineeringCost.Text = pEngineeringCost.ToString();
            this.txtProjectSubUnitCost.Text = pSubUnitCost.ToString();
            this.txtProjectAuditedSubUnitCost.Text = pAuditedSubUnitCost.ToString();
            if (EngineeringCost.HasValue && EngineeringCost != 0 && AuditedSubUnitCost.HasValue)
            {
                this.txtCostRatio.Text = Math.Round((AuditedSubUnitCost / EngineeringCost * 100).Value, 2).ToString() + "%";
            }
            else
            {
                this.txtCostRatio.Text = "0%";
            }

            if (pEngineeringCost.HasValue && pEngineeringCost != 0 && pAuditedSubUnitCost.HasValue)
            {
                this.txtProjectCostRatio.Text = Math.Round((pAuditedSubUnitCost / pEngineeringCost * 100).Value, 2).ToString() + "%";
            }
            else
            {
                this.txtProjectCostRatio.Text = "0%";
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
            if (this.drpYear.SelectedValue == BLL.Const._Null || this.drpMonths.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择月份", MessageBoxIcon.Warning);
                return;
            }

            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SaveData(string type)
        {
            ///添加安全费用
            DateTime? Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
            Model.CostGoods_HSSECostManage newHSSECostManage = new Model.CostGoods_HSSECostManage
            {
                ProjectId=this.ProjectId,
                Month= Months,
                Code=this.txtCode.Text,
                ReportDate=Funs.GetNewDateTime(this.txtReportDate.Text),
                MainIncome =Funs.GetNewDecimal(this.txtMainIncome.Text),
                Remark1=this.txtRemark1.Text,
                ConstructionIncome = Funs.GetNewDecimal(this.txtConstructionIncome.Text),
                Remark2 = this.txtRemark2.Text,
                SafetyCosts = Funs.GetNewDecimal(this.txtSafetyCosts.Text),
                Remark3 = this.txtRemark3.Text,
            };

            ////单据状态
            newHSSECostManage.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                newHSSECostManage.States = this.ctlAuditFlow.NextStep;
            }

            if (!string.IsNullOrEmpty(this.HSSECostManageId))
            {
                newHSSECostManage.HSSECostManageId = this.HSSECostManageId;
                BLL.HSSECostManageService.UpdateHSSECostManage(newHSSECostManage);
                BLL.LogService.AddSys_Log(this.CurrUser, newHSSECostManage.Code, newHSSECostManage.HSSECostManageId, BLL.Const.ProjectHSSECostManageMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.HSSECostManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_HSSECostManage));
                newHSSECostManage.HSSECostManageId = this.HSSECostManageId;
                newHSSECostManage.CompileDate = System.DateTime.Now;
                newHSSECostManage.CompileManId = this.CurrUser.UserId;
                BLL.HSSECostManageService.AddHSSECostManage(newHSSECostManage);
                BLL.LogService.AddSys_Log(this.CurrUser, newHSSECostManage.Code, newHSSECostManage.HSSECostManageId, BLL.Const.ProjectHSSECostManageMenuId, BLL.Const.BtnAdd);
            }

            if (!string.IsNullOrEmpty(this.drpUnitId.SelectedValue) && this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                var updateItem = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(this.HSSECostUnitManageId);
                if (updateItem != null)
                {
                    updateItem.HSSECostManageId = this.HSSECostManageId;
                    updateItem.UnitId = this.drpUnitId.SelectedValue;
                    updateItem.EngineeringCost = Funs.GetNewDecimal(this.txtEngineeringCost.Text);
                    updateItem.Remark1 = this.txtRemark1.Text;
                    updateItem.SubUnitCost = Funs.GetNewDecimal(this.txtSubUnitCost.Text);
                    updateItem.Remark2 = this.txtRemark2.Text;
                    updateItem.AuditedSubUnitCost = Funs.GetNewDecimal(this.txtAuditedSubUnitCost.Text);
                    updateItem.Remark3 = this.txtRemark1.Text;
                    updateItem.CostRatio = Funs.GetNewDecimal(this.txtCostRatio.Text);
                    updateItem.Remark4 = this.txtRemark1.Text;                    
                    BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(updateItem);
                }
                else
                {
                    ///添加施工单位安全费用
                    Model.CostGoods_HSSECostUnitManage newHSSECostUnitManage = new Model.CostGoods_HSSECostUnitManage
                    {
                        HSSECostManageId = this.HSSECostManageId,
                        UnitId = this.drpUnitId.SelectedValue,
                        EngineeringCost = Funs.GetNewDecimal(this.txtEngineeringCost.Text),
                        Remark1 = this.txtRemark1.Text,
                        SubUnitCost = Funs.GetNewDecimal(this.txtSubUnitCost.Text),
                        Remark2 = this.txtRemark2.Text,
                        AuditedSubUnitCost = Funs.GetNewDecimal(this.txtAuditedSubUnitCost.Text),
                        Remark3 = this.txtRemark1.Text,
                        CostRatio = Funs.GetNewDecimal(this.txtCostRatio.Text),
                        Remark4 = this.txtRemark1.Text,
                    };

                    this.HSSECostUnitManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_HSSECostUnitManage));
                    newHSSECostUnitManage.HSSECostUnitManageId = this.HSSECostUnitManageId;
                    BLL.HSSECostUnitManageService.AddHSSECostUnitManage(newHSSECostUnitManage);
                }

                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectHSSECostManageMenuId, this.HSSECostManageId, (type == BLL.Const.BtnSubmit ? true : false), this.drpYear.SelectedText + "年" + this.drpMonths.SelectedText + "月安全费用", "../CostGoods/HSSECostManageView.aspx?HSSECostManageId={0}");
                ////判断单据是否 加入到企业管理资料
                string menuId = BLL.Const.ProjectHSSECostManageMenuId;
                var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                if (safeData != null)
                {
                    BLL.SafetyDataService.AddSafetyData(menuId, this.HSSECostManageId, this.drpYear.SelectedText + "-"+ this.drpMonths.SelectedText + "安全费用", "../CostGoods/HSSECostManageView.aspx?HSSECostManageId={0}", this.ProjectId);
                }
            }
        }

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectHSSECostManageMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
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
            string window = String.Format("HSSECostUnitManageRatifiedItemView.aspx?HSSECostUnitManageId={0}&Type={1}", this.HSSECostUnitManageId, ((FineUIPro.ControlBase)sender).ID.Replace("btn", ""), "增加 - ");
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(window));
        }
        #endregion 

        #region 输入框变化事件
        /// <summary>
        /// 输入框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_TextChanged(object sender, EventArgs e)
        {
            decimal? pMainIncome = 0, pConstructionIncome = 0, pSafetyCosts = 0;
            decimal? pEngineeringCost = 0, pSubUnitCost = 0, pAuditedSubUnitCost = 0;
            DateTime? Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
            var sumHSSECostManage = BLL.HSSECostManageService.GetListHSSECostManageByProjectIdMonth(this.ProjectId, Months);               
            if (sumHSSECostManage.Count() > 0)
            {
                pMainIncome = sumHSSECostManage.Sum(x => x.MainIncome ?? 0);
                pConstructionIncome = sumHSSECostManage.Sum(x => x.ConstructionIncome ?? 0);
                pSafetyCosts = sumHSSECostManage.Sum(x => x.SafetyCosts ?? 0);
            }

            var sumHSSECostUnitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageListByProjectIdUnitIdMonth(this.ProjectId, this.drpUnitId.SelectedValue, Months);               
            if (sumHSSECostUnitManage.Count() > 0)
            {
                pEngineeringCost = sumHSSECostUnitManage.Sum(x => x.EngineeringCost ?? 0);
                pSubUnitCost = sumHSSECostUnitManage.Sum(x => x.SubUnitCost ?? 0);
                pAuditedSubUnitCost = sumHSSECostUnitManage.Sum(x => x.AuditedSubUnitCost ?? 0);
            }

            decimal? EngineeringCost = 0, AuditedSubUnitCost = 0;

            this.txtProjectMainIncome.Text = (pMainIncome + Funs.GetNewDecimalOrZero(this.txtMainIncome.Text)).ToString();
            this.txtProjectConstructionIncome.Text = (pConstructionIncome + Funs.GetNewDecimalOrZero(this.txtConstructionIncome.Text)).ToString();
            this.txtProjectSafetyCosts.Text = (pSafetyCosts + Funs.GetNewDecimalOrZero(this.txtSafetyCosts.Text)).ToString();

            EngineeringCost = Funs.GetNewDecimalOrZero(this.txtEngineeringCost.Text);
            AuditedSubUnitCost = Funs.GetNewDecimalOrZero(this.txtAuditedSubUnitCost.Text);
            pEngineeringCost = pEngineeringCost + EngineeringCost;
            pAuditedSubUnitCost = pAuditedSubUnitCost + AuditedSubUnitCost;

            this.txtProjectEngineeringCost.Text = (pEngineeringCost).ToString();
            this.txtProjectSubUnitCost.Text = (pConstructionIncome + Funs.GetNewDecimalOrZero(this.txtSubUnitCost.Text)).ToString();
            this.txtProjectAuditedSubUnitCost.Text = (pAuditedSubUnitCost).ToString();

            if (EngineeringCost.HasValue && EngineeringCost != 0 && AuditedSubUnitCost.HasValue)
            {
                this.txtCostRatio.Text = Math.Round((AuditedSubUnitCost / EngineeringCost * 100).Value, 2).ToString() + "%"; ;
            }
            else
            {
                this.txtCostRatio.Text = "0%";
            }

            if (pEngineeringCost.HasValue && pEngineeringCost != 0 && pAuditedSubUnitCost.HasValue)
            {
                this.txtProjectCostRatio.Text = Math.Round((pAuditedSubUnitCost / pEngineeringCost * 100).Value, 2).ToString() + "%"; ;
            }
            else
            {
                this.txtProjectCostRatio.Text = "0%";
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}