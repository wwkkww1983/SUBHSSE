using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class SubPayRegistrationEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string SubPayRegistrationId
        {
            get
            {
                return (string)ViewState["SubPayRegistrationId"];
            }
            set
            {
                ViewState["SubPayRegistrationId"] = value;
            }
        }

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
                this.SubPayRegistrationId = Request.Params["SubPayRegistrationId"];
                ///单位下拉框
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, false);
                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                if (unit == null || (unit != null && unit.IsThisUnit == true))
                {
                    this.txtSMainApproveType1.Readonly = false;
                    this.txtSMainApproveType2.Readonly = false;
                    this.txtSMainApproveType3.Readonly = false;
                    this.txtSMainApproveType4.Readonly = false;
                    this.txtSMainApproveType5.Readonly = false;
                    this.txtSMainApproveType6.Readonly = false;
                    this.txtSMainApproveType7.Readonly = false;
                    this.txtSMainApproveType8.Readonly = false;
                    this.txtSMainApproveType9.Readonly = false;
                    this.txtSMainApproveType10.Readonly = false;
                    this.txtSMainApproveType11.Readonly = false;
                    this.txtSMainApproveType12.Readonly = false;
                    this.txtSMainApproveType13.Readonly = false;
                    this.txtSMainApproveType14.Readonly = false;
                    this.txtSMainApproveType15.Readonly = false;
                    this.txtSMainApproveType16.Readonly = false;
                    this.txtSMainApproveType17.Readonly = false;
                    this.txtSMainApproveType18.Readonly = false;
                    this.txtSMainApproveType19.Readonly = false;
                    this.txtSMainApproveType20.Readonly = false;
                    this.txtSMainApproveType21.Readonly = false;
                    this.txtSMainApproveType22.Readonly = false;
                    this.txtSMainApproveType23.Readonly = false;
                    this.txtSMainApproveType24.Readonly = false;
                    this.txtSMainApproveType25.Readonly = false;
                    this.txtSMainApproveType26.Readonly = false;
                    this.txtSMainApproveType27.Readonly = false;
                    this.txtSMainApproveType28.Readonly = false;
                    this.txtSMainApproveType29.Readonly = false;
                }
                if (!string.IsNullOrEmpty(this.SubPayRegistrationId))
                {
                    Model.CostGoods_SubPayRegistration subPayRegistration = BLL.SubPayRegistrationService.GetSubPayRegistrationById(this.SubPayRegistrationId);
                    if (subPayRegistration != null)
                    {
                        this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", subPayRegistration.PayDate);
                        if (!string.IsNullOrEmpty(subPayRegistration.UnitId))
                        {
                            this.drpUnit.SelectedValue = subPayRegistration.UnitId;
                        }
                        this.txtContractNum.Text = subPayRegistration.ContractNum;
                        this.txtSMonthType1.Text = Convert.ToString(subPayRegistration.SMonthType1);
                        this.txtSMonthType2.Text = Convert.ToString(subPayRegistration.SMonthType2);
                        this.txtSMonthType3.Text = Convert.ToString(subPayRegistration.SMonthType3);
                        this.txtSMonthType4.Text = Convert.ToString(subPayRegistration.SMonthType4);
                        this.txtSMonthType5.Text = Convert.ToString(subPayRegistration.SMonthType5);
                        this.txtSMonthType6.Text = Convert.ToString(subPayRegistration.SMonthType6);
                        this.txtSMonthType7.Text = Convert.ToString(subPayRegistration.SMonthType7);
                        this.txtSMonthType8.Text = Convert.ToString(subPayRegistration.SMonthType8);
                        this.txtSMonthType9.Text = Convert.ToString(subPayRegistration.SMonthType9);
                        this.txtSMonthType10.Text = Convert.ToString(subPayRegistration.SMonthType10);
                        this.txtSMonthType11.Text = Convert.ToString(subPayRegistration.SMonthType11);
                        this.txtSMonthType12.Text = Convert.ToString(subPayRegistration.SMonthType12);
                        this.txtSMonthType13.Text = Convert.ToString(subPayRegistration.SMonthType13);
                        this.txtSMonthType14.Text = Convert.ToString(subPayRegistration.SMonthType14);
                        this.txtSMonthType15.Text = Convert.ToString(subPayRegistration.SMonthType15);
                        this.txtSMonthType16.Text = Convert.ToString(subPayRegistration.SMonthType16);
                        this.txtSMonthType17.Text = Convert.ToString(subPayRegistration.SMonthType17);
                        this.txtSMonthType18.Text = Convert.ToString(subPayRegistration.SMonthType18);
                        this.txtSMonthType19.Text = Convert.ToString(subPayRegistration.SMonthType19);
                        this.txtSMonthType20.Text = Convert.ToString(subPayRegistration.SMonthType20);
                        this.txtSMonthType21.Text = Convert.ToString(subPayRegistration.SMonthType21);
                        this.txtSMonthType22.Text = Convert.ToString(subPayRegistration.SMonthType22);
                        this.txtSMonthType23.Text = Convert.ToString(subPayRegistration.SMonthType23);
                        this.txtSMonthType24.Text = Convert.ToString(subPayRegistration.SMonthType24);
                        this.txtSMonthType25.Text = Convert.ToString(subPayRegistration.SMonthType25);
                        this.txtSMonthType26.Text = Convert.ToString(subPayRegistration.SMonthType26);
                        this.txtSMonthType27.Text = Convert.ToString(subPayRegistration.SMonthType27);
                        this.txtSMonthType28.Text = Convert.ToString(subPayRegistration.SMonthType28);
                        this.txtSMonthType29.Text = Convert.ToString(subPayRegistration.SMonthType29);
                        this.txtSMainApproveType1.Text = Convert.ToString(subPayRegistration.SMainApproveType1);
                        this.txtSMainApproveType2.Text = Convert.ToString(subPayRegistration.SMainApproveType2);
                        this.txtSMainApproveType3.Text = Convert.ToString(subPayRegistration.SMainApproveType3);
                        this.txtSMainApproveType4.Text = Convert.ToString(subPayRegistration.SMainApproveType4);
                        this.txtSMainApproveType5.Text = Convert.ToString(subPayRegistration.SMainApproveType5);
                        this.txtSMainApproveType6.Text = Convert.ToString(subPayRegistration.SMainApproveType6);
                        this.txtSMainApproveType7.Text = Convert.ToString(subPayRegistration.SMainApproveType7);
                        this.txtSMainApproveType8.Text = Convert.ToString(subPayRegistration.SMainApproveType8);
                        this.txtSMainApproveType9.Text = Convert.ToString(subPayRegistration.SMainApproveType9);
                        this.txtSMainApproveType10.Text = Convert.ToString(subPayRegistration.SMainApproveType10);
                        this.txtSMainApproveType11.Text = Convert.ToString(subPayRegistration.SMainApproveType11);
                        this.txtSMainApproveType12.Text = Convert.ToString(subPayRegistration.SMainApproveType12);
                        this.txtSMainApproveType13.Text = Convert.ToString(subPayRegistration.SMainApproveType13);
                        this.txtSMainApproveType14.Text = Convert.ToString(subPayRegistration.SMainApproveType14);
                        this.txtSMainApproveType15.Text = Convert.ToString(subPayRegistration.SMainApproveType15);
                        this.txtSMainApproveType16.Text = Convert.ToString(subPayRegistration.SMainApproveType16);
                        this.txtSMainApproveType17.Text = Convert.ToString(subPayRegistration.SMainApproveType17);
                        this.txtSMainApproveType18.Text = Convert.ToString(subPayRegistration.SMainApproveType18);
                        this.txtSMainApproveType19.Text = Convert.ToString(subPayRegistration.SMainApproveType19);
                        this.txtSMainApproveType20.Text = Convert.ToString(subPayRegistration.SMainApproveType20);
                        this.txtSMainApproveType21.Text = Convert.ToString(subPayRegistration.SMainApproveType21);
                        this.txtSMainApproveType22.Text = Convert.ToString(subPayRegistration.SMainApproveType22);
                        this.txtSMainApproveType23.Text = Convert.ToString(subPayRegistration.SMainApproveType23);
                        this.txtSMainApproveType24.Text = Convert.ToString(subPayRegistration.SMainApproveType24);
                        this.txtSMainApproveType25.Text = Convert.ToString(subPayRegistration.SMainApproveType25);
                        this.txtSMainApproveType26.Text = Convert.ToString(subPayRegistration.SMainApproveType26);
                        this.txtSMainApproveType27.Text = Convert.ToString(subPayRegistration.SMainApproveType27);
                        this.txtSMainApproveType28.Text = Convert.ToString(subPayRegistration.SMainApproveType28);
                        this.txtSMainApproveType29.Text = Convert.ToString(subPayRegistration.SMainApproveType29);
                        this.txtSMonthTypeTotal1.Text = Convert.ToString(subPayRegistration.SMonthType1 + subPayRegistration.SMonthType2 + subPayRegistration.SMonthType3 + subPayRegistration.SMonthType4 + subPayRegistration.SMonthType5);//基础管理 当月累计 费用小计
                        this.txtSMonthTypeTotal2.Text = Convert.ToString(subPayRegistration.SMonthType6);//安全技术 当月累计 费用小计
                        this.txtSMonthTypeTotal3.Text = Convert.ToString(subPayRegistration.SMonthType7);//职业健康 当月累计 费用小计
                        this.txtSMonthTypeTotal4.Text = Convert.ToString(subPayRegistration.SMonthType8 + subPayRegistration.SMonthType9 + subPayRegistration.SMonthType10 + subPayRegistration.SMonthType11 + subPayRegistration.SMonthType12 + subPayRegistration.SMonthType13 + subPayRegistration.SMonthType14 + subPayRegistration.SMonthType15 + subPayRegistration.SMonthType16 + subPayRegistration.SMonthType17 + subPayRegistration.SMonthType18 + subPayRegistration.SMonthType19 + subPayRegistration.SMonthType20 + subPayRegistration.SMonthType21);//防护措施 当月累计 费用小计
                        this.txtSMonthTypeTotal5.Text = Convert.ToString(subPayRegistration.SMonthType22 + subPayRegistration.SMonthType23 + subPayRegistration.SMonthType24 + subPayRegistration.SMonthType25 + subPayRegistration.SMonthType26 + subPayRegistration.SMonthType27);//化工试车 当月累计 费用小计
                        this.txtSMonthTypeTotal6.Text = Convert.ToString(subPayRegistration.SMonthType28);//教育培训 当月累计 费用小计
                        this.txtSMonthTypeTotal7.Text = Convert.ToString(subPayRegistration.SMonthType29);//文明施工和环境保护 当月累计 费用小计
                        this.txtSMainApproveTypeTotal1.Text = Convert.ToString(subPayRegistration.SMainApproveType1 + subPayRegistration.SMainApproveType2 + subPayRegistration.SMainApproveType3 + subPayRegistration.SMainApproveType4 + subPayRegistration.SMainApproveType5);//基础管理 五环审核 费用小计
                        this.txtSMainApproveTypeTotal2.Text = Convert.ToString(subPayRegistration.SMainApproveType6);//安全技术 五环审核 费用小计
                        this.txtSMainApproveTypeTotal3.Text = Convert.ToString(subPayRegistration.SMainApproveType7);//职业健康 五环审核 费用小计
                        this.txtSMainApproveTypeTotal4.Text = Convert.ToString(subPayRegistration.SMainApproveType8 + subPayRegistration.SMainApproveType9 + subPayRegistration.SMainApproveType10 + subPayRegistration.SMainApproveType11 + subPayRegistration.SMainApproveType12 + subPayRegistration.SMainApproveType13 + subPayRegistration.SMainApproveType14 + subPayRegistration.SMainApproveType15 + subPayRegistration.SMainApproveType16 + subPayRegistration.SMainApproveType17 + subPayRegistration.SMainApproveType18 + subPayRegistration.SMainApproveType19 + subPayRegistration.SMainApproveType20 + subPayRegistration.SMainApproveType21);//防护措施 五环审核 费用小计
                        this.txtSMainApproveTypeTotal5.Text = Convert.ToString(subPayRegistration.SMainApproveType22 + subPayRegistration.SMainApproveType23 + subPayRegistration.SMainApproveType24 + subPayRegistration.SMainApproveType25 + subPayRegistration.SMainApproveType26 + subPayRegistration.SMainApproveType27);//化工试车 五环审核 费用小计
                        this.txtSMainApproveTypeTotal6.Text = Convert.ToString(subPayRegistration.SMainApproveType28);//教育培训 五环审核 费用小计
                        this.txtSMainApproveTypeTotal7.Text = Convert.ToString(subPayRegistration.SMainApproveType29);//文明施工和环境保护 五环审核 费用小计

                        this.txtSMonthTypeTotal.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal3.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal4.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal5.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal6.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal7.Text));
                        this.txtSMainApproveTypeTotal.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal1.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal2.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal3.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal4.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal5.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal6.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal7.Text));
                    }
                }
                else
                {
                    if (unit != null)
                    {
                        this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    }
                    this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var costManageItems = BLL.CostManageItemService.GetCostManageItemByUnitIdAndDate(this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, DateTime.Now);
                    decimal sMonthType1 = 0, sMonthType2 = 0, sMonthType3 = 0, sMonthType4 = 0, sMonthType5 = 0, sMonthType6 = 0, sMonthType7 = 0, sMonthType8 = 0,
                        sMonthType9 = 0, sMonthType10 = 0, sMonthType11 = 0, sMonthType12 = 0, sMonthType13 = 0, sMonthType14 = 0, sMonthType15 = 0,
                        sMonthType16 = 0, sMonthType17 = 0, sMonthType18 = 0, sMonthType19 = 0, sMonthType20 = 0, sMonthType21 = 0, sMonthType22 = 0, sMonthType23 = 0,
                        sMonthType24 = 0, sMonthType25 = 0, sMonthType26 = 0, sMonthType27 = 0, sMonthType28 = 0, sMonthType29 = 0;
                    var item1 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "内业管理");
                    if (item1 != null)
                    {
                        sMonthType1 = (item1.Counts ?? 0) * (item1.PriceMoney ?? 0);
                        this.txtSMonthType1.Text = Convert.ToString(sMonthType1);
                    }
                    var item2 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "检测器材");
                    if (item2 != null)
                    {
                        sMonthType2 = (item2.Counts ?? 0) * (item2.PriceMoney ?? 0);
                        this.txtSMonthType2.Text = Convert.ToString(sMonthType2);
                    }
                    var item3 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "警示警戒");
                    if (item3 != null)
                    {
                        sMonthType3 = (item3.Counts ?? 0) * (item3.PriceMoney ?? 0);
                        this.txtSMonthType3.Text = Convert.ToString(sMonthType3);
                    }
                    var item4 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "安全奖励");
                    if (item4 != null)
                    {
                        sMonthType4 = (item4.Counts ?? 0) * (item4.PriceMoney ?? 0);
                        this.txtSMonthType4.Text = Convert.ToString(sMonthType4);
                    }
                    var item5 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "其他");
                    if (item5 != null)
                    {
                        sMonthType5 = (item5.Counts ?? 0) * (item5.PriceMoney ?? 0);
                        this.txtSMonthType5.Text = Convert.ToString(sMonthType5);
                    }
                    var item6 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "安全技术");
                    if (item6 != null)
                    {
                        sMonthType6 = (item6.Counts ?? 0) * (item6.PriceMoney ?? 0);
                        this.txtSMonthType6.Text = Convert.ToString(sMonthType6);
                    }
                    var item7 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "工业卫生");
                    if (item7 != null)
                    {
                        sMonthType7 = (item7.Counts ?? 0) * (item7.PriceMoney ?? 0);
                        this.txtSMonthType7.Text = Convert.ToString(sMonthType7);
                    }
                    var item8 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "安全用电");
                    if (item8 != null)
                    {
                        sMonthType8 = (item8.Counts ?? 0) * (item8.PriceMoney ?? 0);
                        this.txtSMonthType8.Text = Convert.ToString(sMonthType8);
                    }
                    var item9 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "高处作业及基坑");
                    if (item9 != null)
                    {
                        sMonthType9 = (item9.Counts ?? 0) * (item9.PriceMoney ?? 0);
                        this.txtSMonthType9.Text = Convert.ToString(sMonthType9);
                    }
                    var item10 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "临边洞口防护");
                    if (item10 != null)
                    {
                        sMonthType10 = (item10.Counts ?? 0) * (item10.PriceMoney ?? 0);
                        this.txtSMonthType10.Text = Convert.ToString(sMonthType10);
                    }
                    var item11 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "受限空间内作业");
                    if (item11 != null)
                    {
                        sMonthType11 = (item11.Counts ?? 0) * (item11.PriceMoney ?? 0);
                        this.txtSMonthType11.Text = Convert.ToString(sMonthType11);
                    }
                    var item12 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "动火作业");
                    if (item12 != null)
                    {
                        sMonthType12 = (item12.Counts ?? 0) * (item12.PriceMoney ?? 0);
                        this.txtSMonthType12.Text = Convert.ToString(sMonthType12);
                    }
                    var item13 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "机械装备防护");
                    if (item13 != null)
                    {
                        sMonthType13 = (item13.Counts ?? 0) * (item13.PriceMoney ?? 0);
                        this.txtSMonthType13.Text = Convert.ToString(sMonthType13);
                    }
                    var item14 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "吊装运输和起重");
                    if (item14 != null)
                    {
                        sMonthType14 = (item14.Counts ?? 0) * (item14.PriceMoney ?? 0);
                        this.txtSMonthType14.Text = Convert.ToString(sMonthType14);
                    }
                    var item15 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "硼砂作业");
                    if (item15 != null)
                    {
                        sMonthType15 = (item15.Counts ?? 0) * (item15.PriceMoney ?? 0);
                        this.txtSMonthType15.Text = Convert.ToString(sMonthType15);
                    }
                    var item16 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "拆除工程");
                    if (item16 != null)
                    {
                        sMonthType16 = (item16.Counts ?? 0) * (item16.PriceMoney ?? 0);
                        this.txtSMonthType16.Text = Convert.ToString(sMonthType16);
                    }
                    var item17 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "试压试车与有害介质作业");
                    if (item17 != null)
                    {
                        sMonthType17 = (item17.Counts ?? 0) * (item17.PriceMoney ?? 0);
                        this.txtSMonthType17.Text = Convert.ToString(sMonthType17);
                    }
                    var item18 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "特种作业防护");
                    if (item18 != null)
                    {
                        sMonthType18 = (item18.Counts ?? 0) * (item18.PriceMoney ?? 0);
                        this.txtSMonthType18.Text = Convert.ToString(sMonthType18);
                    }
                    var item19 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "应急管理");
                    if (item19 != null)
                    {
                        sMonthType19 = (item19.Counts ?? 0) * (item19.PriceMoney ?? 0);
                        this.txtSMonthType19.Text = Convert.ToString(sMonthType19);
                    }
                    var item20 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "非常措施");
                    if (item20 != null)
                    {
                        sMonthType20 = (item20.Counts ?? 0) * (item20.PriceMoney ?? 0);
                        this.txtSMonthType20.Text = Convert.ToString(sMonthType20);
                    }
                    var item21 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "其他安全措施");
                    if (item21 != null)
                    {
                        sMonthType21 = (item21.Counts ?? 0) * (item21.PriceMoney ?? 0);
                        this.txtSMonthType21.Text = Convert.ToString(sMonthType21);
                    }
                    var item22 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "装置区封闭管理");
                    if (item22 != null)
                    {
                        sMonthType22 = (item22.Counts ?? 0) * (item22.PriceMoney ?? 0);
                        this.txtSMonthType22.Text = Convert.ToString(sMonthType22);
                    }
                    var item23 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "防爆施工器具");
                    if (item23 != null)
                    {
                        sMonthType23 = (item23.Counts ?? 0) * (item23.PriceMoney ?? 0);
                        this.txtSMonthType23.Text = Convert.ToString(sMonthType23);
                    }
                    var item24 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "标识标签与锁定");
                    if (item24 != null)
                    {
                        sMonthType24 = (item24.Counts ?? 0) * (item24.PriceMoney ?? 0);
                        this.txtSMonthType24.Text = Convert.ToString(sMonthType24);
                    }
                    var item25 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "关键场所封闭");
                    if (item25 != null)
                    {
                        sMonthType25 = (item25.Counts ?? 0) * (item25.PriceMoney ?? 0);
                        this.txtSMonthType25.Text = Convert.ToString(sMonthType25);
                    }
                    var item26 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "催化剂加装还原");
                    if (item26 != null)
                    {
                        sMonthType26 = (item26.Counts ?? 0) * (item26.PriceMoney ?? 0);
                        this.txtSMonthType26.Text = Convert.ToString(sMonthType26);
                    }
                    var item27 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "联动和化工试车");
                    if (item27 != null)
                    {
                        sMonthType27 = (item27.Counts ?? 0) * (item27.PriceMoney ?? 0);
                        this.txtSMonthType27.Text = Convert.ToString(sMonthType27);
                    }
                    var item28 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "教育培训");
                    if (item28 != null)
                    {
                        sMonthType28 = (item28.Counts ?? 0) * (item28.PriceMoney ?? 0);
                        this.txtSMonthType28.Text = Convert.ToString(sMonthType28);
                    }
                    var item29 = costManageItems.FirstOrDefault(x => x.InvestCostProject == "防护控制和排放");
                    if (item29 != null)
                    {
                        sMonthType29 = (item29.Counts ?? 0) * (item29.PriceMoney ?? 0);
                        this.txtSMonthType29.Text = Convert.ToString(sMonthType29);
                    }
                    this.txtSMonthTypeTotal1.Text = Convert.ToString(sMonthType1 + sMonthType2 + sMonthType3 + sMonthType4 + sMonthType5);//基础管理 当月累计 费用小计
                    this.txtSMonthTypeTotal2.Text = Convert.ToString(sMonthType6);//安全技术 当月累计 费用小计
                    this.txtSMonthTypeTotal3.Text = Convert.ToString(sMonthType7);//职业健康 当月累计 费用小计
                    this.txtSMonthTypeTotal4.Text = Convert.ToString(sMonthType8 + sMonthType9 + sMonthType10 + sMonthType11 + sMonthType12 + sMonthType13 + sMonthType14 + sMonthType15 + sMonthType16 + sMonthType17 + sMonthType18 + sMonthType19 + sMonthType20 + sMonthType21);//防护措施 当月累计 费用小计
                    this.txtSMonthTypeTotal5.Text = Convert.ToString(sMonthType22 + sMonthType23 + sMonthType24 + sMonthType25 + sMonthType26 + sMonthType27);//化工试车 当月累计 费用小计
                    this.txtSMonthTypeTotal6.Text = Convert.ToString(sMonthType28);//教育培训 当月累计 费用小计
                    this.txtSMonthTypeTotal7.Text = Convert.ToString(sMonthType29);//文明施工和环境保护 当月累计 费用小计
                    this.txtSMonthTypeTotal.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal3.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal4.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal5.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal6.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal7.Text));
                }
                if (unit != null && unit.IsThisUnit != true)
                {
                    this.drpUnit.Enabled = false;
                }
                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }

        #region 费用统计
        /// <summary>
        /// 当月费用统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SMonthTypeText_TextChanged(object sender, EventArgs e)
        {
            decimal sMonthType1 = 0, sMonthType2 = 0, sMonthType3 = 0, sMonthType4 = 0, sMonthType5 = 0, sMonthType6 = 0, sMonthType7 = 0, sMonthType8 = 0, sMonthType9 = 0, sMonthType10 = 0, sMonthType11 = 0, sMonthType12 = 0, sMonthType13 = 0, sMonthType14 = 0, sMonthType15 = 0, sMonthType16 = 0,
                sMonthType17 = 0, sMonthType18 = 0, sMonthType19 = 0, sMonthType20 = 0, sMonthType21 = 0, sMonthType22 = 0, sMonthType23 = 0, sMonthType24 = 0, sMonthType25 = 0, sMonthType26 = 0, sMonthType27 = 0, sMonthType28 = 0, sMonthType29 = 0;
            sMonthType1 = Funs.GetNewDecimalOrZero(this.txtSMonthType1.Text.Trim());
            sMonthType2 = Funs.GetNewDecimalOrZero(this.txtSMonthType2.Text.Trim());
            sMonthType3 = Funs.GetNewDecimalOrZero(this.txtSMonthType3.Text.Trim());
            sMonthType4 = Funs.GetNewDecimalOrZero(this.txtSMonthType4.Text.Trim());
            sMonthType5 = Funs.GetNewDecimalOrZero(this.txtSMonthType5.Text.Trim());
            sMonthType6 = Funs.GetNewDecimalOrZero(this.txtSMonthType6.Text.Trim());
            sMonthType7 = Funs.GetNewDecimalOrZero(this.txtSMonthType7.Text.Trim());
            sMonthType8 = Funs.GetNewDecimalOrZero(this.txtSMonthType8.Text.Trim());
            sMonthType9 = Funs.GetNewDecimalOrZero(this.txtSMonthType9.Text.Trim());
            sMonthType10 = Funs.GetNewDecimalOrZero(this.txtSMonthType10.Text.Trim());
            sMonthType11 = Funs.GetNewDecimalOrZero(this.txtSMonthType11.Text.Trim());
            sMonthType12 = Funs.GetNewDecimalOrZero(this.txtSMonthType12.Text.Trim());
            sMonthType13 = Funs.GetNewDecimalOrZero(this.txtSMonthType13.Text.Trim());
            sMonthType14 = Funs.GetNewDecimalOrZero(this.txtSMonthType14.Text.Trim());
            sMonthType15 = Funs.GetNewDecimalOrZero(this.txtSMonthType15.Text.Trim());
            sMonthType16 = Funs.GetNewDecimalOrZero(this.txtSMonthType16.Text.Trim());
            sMonthType17 = Funs.GetNewDecimalOrZero(this.txtSMonthType17.Text.Trim());
            sMonthType18 = Funs.GetNewDecimalOrZero(this.txtSMonthType18.Text.Trim());
            sMonthType19 = Funs.GetNewDecimalOrZero(this.txtSMonthType19.Text.Trim());
            sMonthType20 = Funs.GetNewDecimalOrZero(this.txtSMonthType20.Text.Trim());
            sMonthType21 = Funs.GetNewDecimalOrZero(this.txtSMonthType21.Text.Trim());
            sMonthType22 = Funs.GetNewDecimalOrZero(this.txtSMonthType22.Text.Trim());
            sMonthType23 = Funs.GetNewDecimalOrZero(this.txtSMonthType23.Text.Trim());
            sMonthType24 = Funs.GetNewDecimalOrZero(this.txtSMonthType24.Text.Trim());
            sMonthType25 = Funs.GetNewDecimalOrZero(this.txtSMonthType25.Text.Trim());
            sMonthType26 = Funs.GetNewDecimalOrZero(this.txtSMonthType26.Text.Trim());
            sMonthType27 = Funs.GetNewDecimalOrZero(this.txtSMonthType27.Text.Trim());
            sMonthType28 = Funs.GetNewDecimalOrZero(this.txtSMonthType28.Text.Trim());
            sMonthType29 = Funs.GetNewDecimalOrZero(this.txtSMonthType29.Text.Trim());
            this.txtSMonthTypeTotal1.Text = Convert.ToString(sMonthType1 + sMonthType2 + sMonthType3 + sMonthType4 + sMonthType5);//基础管理 当月累计 费用小计
            this.txtSMonthTypeTotal2.Text = Convert.ToString(sMonthType6);//安全技术 当月累计 费用小计
            this.txtSMonthTypeTotal3.Text = Convert.ToString(sMonthType7);//职业健康 当月累计 费用小计
            this.txtSMonthTypeTotal4.Text = Convert.ToString(sMonthType8 + sMonthType9 + sMonthType10 + sMonthType11 + sMonthType12 + sMonthType13 + sMonthType14 + sMonthType15 + sMonthType16 + sMonthType17 + sMonthType18 + sMonthType19 + sMonthType20 + sMonthType21);//防护措施 当月累计 费用小计
            this.txtSMonthTypeTotal5.Text = Convert.ToString(sMonthType22 + sMonthType23 + sMonthType24 + sMonthType25 + sMonthType26 + sMonthType27);//化工试车 当月累计 费用小计
            this.txtSMonthTypeTotal6.Text = Convert.ToString(sMonthType28);//教育培训 当月累计 费用小计
            this.txtSMonthTypeTotal7.Text = Convert.ToString(sMonthType29);//文明施工和环境保护 当月累计 费用小计

            //费用累计
            this.txtSMonthTypeTotal.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal3.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal4.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal5.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal6.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthTypeTotal7.Text));
        }
        #endregion

        #region 审核费用统计
        /// <summary>
        /// 审核费用统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SMainApproveTypeText_TextChanged(object sender, EventArgs e)
        {
            decimal sMainApproveType1 = 0, sMainApproveType2 = 0, sMainApproveType3 = 0, sMainApproveType4 = 0, sMainApproveType5 = 0, sMainApproveType6 = 0, sMainApproveType7 = 0, sMainApproveType8 = 0, sMainApproveType9 = 0, sMainApproveType10 = 0, sMainApproveType11 = 0, sMainApproveType12 = 0, sMainApproveType13 = 0, sMainApproveType14 = 0, sMainApproveType15 = 0, sMainApproveType16 = 0,
                sMainApproveType17 = 0, sMainApproveType18 = 0, sMainApproveType19 = 0, sMainApproveType20 = 0, sMainApproveType21 = 0, sMainApproveType22 = 0, sMainApproveType23 = 0, sMainApproveType24 = 0, sMainApproveType25 = 0, sMainApproveType26 = 0, sMainApproveType27 = 0, sMainApproveType28 = 0, sMainApproveType29 = 0;
            sMainApproveType1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1.Text.Trim());
            sMainApproveType2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2.Text.Trim());
            sMainApproveType3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3.Text.Trim());
            sMainApproveType4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4.Text.Trim());
            sMainApproveType5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5.Text.Trim());
            sMainApproveType6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6.Text.Trim());
            sMainApproveType7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType7.Text.Trim());
            sMainApproveType8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType8.Text.Trim());
            sMainApproveType9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType9.Text.Trim());
            sMainApproveType10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType10.Text.Trim());
            sMainApproveType11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType11.Text.Trim());
            sMainApproveType12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType12.Text.Trim());
            sMainApproveType13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType13.Text.Trim());
            sMainApproveType14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType14.Text.Trim());
            sMainApproveType15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType15.Text.Trim());
            sMainApproveType16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType16.Text.Trim());
            sMainApproveType17 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType17.Text.Trim());
            sMainApproveType18 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType18.Text.Trim());
            sMainApproveType19 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType19.Text.Trim());
            sMainApproveType20 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType20.Text.Trim());
            sMainApproveType21 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType21.Text.Trim());
            sMainApproveType22 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType22.Text.Trim());
            sMainApproveType23 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType23.Text.Trim());
            sMainApproveType24 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType24.Text.Trim());
            sMainApproveType25 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType25.Text.Trim());
            sMainApproveType26 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType26.Text.Trim());
            sMainApproveType27 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType27.Text.Trim());
            sMainApproveType28 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType28.Text.Trim());
            sMainApproveType29 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType29.Text.Trim());
            this.txtSMainApproveTypeTotal1.Text = Convert.ToString(sMainApproveType1 + sMainApproveType2 + sMainApproveType3 + sMainApproveType4 + sMainApproveType5);//基础管理 审核累计 费用小计
            this.txtSMainApproveTypeTotal2.Text = Convert.ToString(sMainApproveType6);//安全技术 审核累计 费用小计
            this.txtSMainApproveTypeTotal3.Text = Convert.ToString(sMainApproveType7);//职业健康 审核累计 费用小计
            this.txtSMainApproveTypeTotal4.Text = Convert.ToString(sMainApproveType8 + sMainApproveType9 + sMainApproveType10 + sMainApproveType11 + sMainApproveType12 + sMainApproveType13 + sMainApproveType14 + sMainApproveType15 + sMainApproveType16 + sMainApproveType17 + sMainApproveType18 + sMainApproveType19 + sMainApproveType20 + sMainApproveType21);//防护措施 审核累计 费用小计
            this.txtSMainApproveTypeTotal5.Text = Convert.ToString(sMainApproveType22 + sMainApproveType23 + sMainApproveType24 + sMainApproveType25 + sMainApproveType26 + sMainApproveType27);//化工试车 审核累计 费用小计
            this.txtSMainApproveTypeTotal6.Text = Convert.ToString(sMainApproveType28);//教育培训 审核累计 费用小计
            this.txtSMainApproveTypeTotal7.Text = Convert.ToString(sMainApproveType29);//文明施工和环境保护 审核累计 费用小计

            //审核累计
            this.txtSMainApproveTypeTotal.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal1.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal2.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal3.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal4.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal5.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal6.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveTypeTotal7.Text));
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
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///    保存方法
        /// </summary>
        private void SaveData()
        {
            Model.CostGoods_SubPayRegistration subPayRegistration = new Model.CostGoods_SubPayRegistration
            {
                ProjectId = this.CurrUser.LoginProjectId,
                UnitId = this.drpUnit.SelectedValue,
                PayDate = Funs.GetNewDateTime(this.txtDate.Text.Trim()),
                ContractNum = this.txtContractNum.Text.Trim(),
                SMonthType1 = Funs.GetNewDecimalOrZero(this.txtSMonthType1.Text.Trim()),
                SMonthType2 = Funs.GetNewDecimalOrZero(this.txtSMonthType2.Text.Trim()),
                SMonthType3 = Funs.GetNewDecimalOrZero(this.txtSMonthType3.Text.Trim()),
                SMonthType4 = Funs.GetNewDecimalOrZero(this.txtSMonthType4.Text.Trim()),
                SMonthType5 = Funs.GetNewDecimalOrZero(this.txtSMonthType5.Text.Trim()),
                SMonthType6 = Funs.GetNewDecimalOrZero(this.txtSMonthType6.Text.Trim()),
                SMonthType7 = Funs.GetNewDecimalOrZero(this.txtSMonthType7.Text.Trim()),
                SMonthType8 = Funs.GetNewDecimalOrZero(this.txtSMonthType8.Text.Trim()),
                SMonthType9 = Funs.GetNewDecimalOrZero(this.txtSMonthType9.Text.Trim()),
                SMonthType10 = Funs.GetNewDecimalOrZero(this.txtSMonthType10.Text.Trim()),
                SMonthType11 = Funs.GetNewDecimalOrZero(this.txtSMonthType11.Text.Trim()),
                SMonthType12 = Funs.GetNewDecimalOrZero(this.txtSMonthType12.Text.Trim()),
                SMonthType13 = Funs.GetNewDecimalOrZero(this.txtSMonthType13.Text.Trim()),
                SMonthType14 = Funs.GetNewDecimalOrZero(this.txtSMonthType14.Text.Trim()),
                SMonthType15 = Funs.GetNewDecimalOrZero(this.txtSMonthType15.Text.Trim()),
                SMonthType16 = Funs.GetNewDecimalOrZero(this.txtSMonthType16.Text.Trim()),
                SMonthType17 = Funs.GetNewDecimalOrZero(this.txtSMonthType17.Text.Trim()),
                SMonthType18 = Funs.GetNewDecimalOrZero(this.txtSMonthType18.Text.Trim()),
                SMonthType19 = Funs.GetNewDecimalOrZero(this.txtSMonthType19.Text.Trim()),
                SMonthType20 = Funs.GetNewDecimalOrZero(this.txtSMonthType20.Text.Trim()),
                SMonthType21 = Funs.GetNewDecimalOrZero(this.txtSMonthType21.Text.Trim()),
                SMonthType22 = Funs.GetNewDecimalOrZero(this.txtSMonthType22.Text.Trim()),
                SMonthType23 = Funs.GetNewDecimalOrZero(this.txtSMonthType23.Text.Trim()),
                SMonthType24 = Funs.GetNewDecimalOrZero(this.txtSMonthType24.Text.Trim()),
                SMonthType25 = Funs.GetNewDecimalOrZero(this.txtSMonthType25.Text.Trim()),
                SMonthType26 = Funs.GetNewDecimalOrZero(this.txtSMonthType26.Text.Trim()),
                SMonthType27 = Funs.GetNewDecimalOrZero(this.txtSMonthType27.Text.Trim()),
                SMonthType28 = Funs.GetNewDecimalOrZero(this.txtSMonthType28.Text.Trim()),
                SMonthType29 = Funs.GetNewDecimalOrZero(this.txtSMonthType29.Text.Trim()),
                SMainApproveType1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1.Text.Trim()),
                SMainApproveType2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2.Text.Trim()),
                SMainApproveType3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3.Text.Trim()),
                SMainApproveType4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4.Text.Trim()),
                SMainApproveType5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5.Text.Trim()),
                SMainApproveType6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6.Text.Trim()),
                SMainApproveType7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType7.Text.Trim()),
                SMainApproveType8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType8.Text.Trim()),
                SMainApproveType9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType9.Text.Trim()),
                SMainApproveType10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType10.Text.Trim()),
                SMainApproveType11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType11.Text.Trim()),
                SMainApproveType12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType12.Text.Trim()),
                SMainApproveType13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType13.Text.Trim()),
                SMainApproveType14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType14.Text.Trim()),
                SMainApproveType15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType15.Text.Trim()),
                SMainApproveType16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType16.Text.Trim()),
                SMainApproveType17 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType17.Text.Trim()),
                SMainApproveType18 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType18.Text.Trim()),
                SMainApproveType19 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType19.Text.Trim()),
                SMainApproveType20 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType20.Text.Trim()),
                SMainApproveType21 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType21.Text.Trim()),
                SMainApproveType22 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType22.Text.Trim()),
                SMainApproveType23 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType23.Text.Trim()),
                SMainApproveType24 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType24.Text.Trim()),
                SMainApproveType25 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType25.Text.Trim()),
                SMainApproveType26 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType26.Text.Trim()),
                SMainApproveType27 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType27.Text.Trim()),
                SMainApproveType28 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType28.Text.Trim()),
                SMainApproveType29 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType29.Text.Trim()),

            };
            if (!string.IsNullOrEmpty(this.SubPayRegistrationId))
            {
                subPayRegistration.SubPayRegistrationId = this.SubPayRegistrationId;
                if (this.txtSMainApproveTypeTotal.Text.Trim() != "0")
                {
                    subPayRegistration.State = "2";   //总包审核
                }
                else
                {
                    subPayRegistration.State = "1";   //分包提交
                }
                BLL.SubPayRegistrationService.UpdateSubPayRegistration(subPayRegistration);
                BLL.LogService.AddSys_Log(this.CurrUser, null, subPayRegistration.SubPayRegistrationId, BLL.Const.ProjectSubPayRegistrationMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SubPayRegistrationId = SQLHelper.GetNewID(typeof(Model.CostGoods_SubPayRegistration));
                subPayRegistration.SubPayRegistrationId = this.SubPayRegistrationId;
                subPayRegistration.State = "1";   //分包提交
                subPayRegistration.CompileMan = this.CurrUser.UserId;
                subPayRegistration.CompileDate = DateTime.Now;
                BLL.SubPayRegistrationService.AddSubPayRegistration(subPayRegistration);
                BLL.LogService.AddSys_Log(this.CurrUser, null, subPayRegistration.SubPayRegistrationId, BLL.Const.ProjectSubPayRegistrationMenuId, BLL.Const.BtnAdd);
            }
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubPayRegistrationAttachUrl&type=-1", this.SubPayRegistrationId, BLL.Const.ProjectSubPayRegistrationMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.SubPayRegistrationId))
                {
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubPayRegistrationAttachUrl&menuId={1}", this.SubPayRegistrationId, BLL.Const.ProjectSubPayRegistrationMenuId)));
            }
        }
        #endregion
    }
}