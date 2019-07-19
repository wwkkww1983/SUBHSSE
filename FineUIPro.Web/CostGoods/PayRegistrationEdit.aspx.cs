using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class PayRegistrationEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PayRegistrationId
        {
            get
            {
                return (string)ViewState["PayRegistrationId"];
            }
            set
            {
                ViewState["PayRegistrationId"] = value;
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
                this.PayRegistrationId = Request.Params["PayRegistrationId"];
                if (!string.IsNullOrEmpty(this.PayRegistrationId))
                {
                    Model.CostGoods_PayRegistration payRegistration = BLL.PayRegistrationService.GetPayRegistrationById(this.PayRegistrationId);
                    if (payRegistration != null)
                    {
                        this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", payRegistration.PayDate);
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(payRegistration.UnitId);
                        if (unit != null)
                        {
                            this.lblUnitName.Text = unit.UnitName;
                        }
                        this.txtSMonthType1_1.Text = Convert.ToString(payRegistration.SMonthType1_1);
                        this.txtSMonthType1_2.Text = Convert.ToString(payRegistration.SMonthType1_2);
                        this.txtSMonthType1_3.Text = Convert.ToString(payRegistration.SMonthType1_3);
                        this.txtSMonthType1_4.Text = Convert.ToString(payRegistration.SMonthType1_4);
                        this.txtSMonthType1_5.Text = Convert.ToString(payRegistration.SMonthType1_5);
                        this.txtSMonthType1_6.Text = Convert.ToString(payRegistration.SMonthType1_6);
                        this.txtSMonthType1_7.Text = Convert.ToString(payRegistration.SMonthType1_7);
                        this.txtSMonthType1_8.Text = Convert.ToString(payRegistration.SMonthType1_8);
                        this.txtSMonthType1_9.Text = Convert.ToString(payRegistration.SMonthType1_9);
                        this.txtSMonthType1_10.Text = Convert.ToString(payRegistration.SMonthType1_10);
                        this.txtSMonthType1_11.Text = Convert.ToString(payRegistration.SMonthType1_11);
                        this.txtSMonthType1_12.Text = Convert.ToString(payRegistration.SMonthType1_12);
                        this.txtSMonthType1_13.Text = Convert.ToString(payRegistration.SMonthType1_13);
                        this.txtSMonthType1_14.Text = Convert.ToString(payRegistration.SMonthType1_14);
                        this.txtSMonthType1_15.Text = Convert.ToString(payRegistration.SMonthType1_15);
                        this.txtSMonthType1_16.Text = Convert.ToString(payRegistration.SMonthType1_16);
                        this.txtSMonthType1.Text = Convert.ToString(payRegistration.SMonthType1_1 + payRegistration.SMonthType1_2 + payRegistration.SMonthType1_3 + payRegistration.SMonthType1_4 + payRegistration.SMonthType1_5 + payRegistration.SMonthType1_6 + payRegistration.SMonthType1_7 + payRegistration.SMonthType1_8 + payRegistration.SMonthType1_9 + payRegistration.SMonthType1_10 + payRegistration.SMonthType1_11 + payRegistration.SMonthType1_12 + payRegistration.SMonthType1_13 + payRegistration.SMonthType1_14 + payRegistration.SMonthType1_15 + payRegistration.SMonthType1_16);//基础管理 当月累计 费用小计

                     
                        this.txtSMonthType2_1.Text = Convert.ToString(payRegistration.SMonthType2_1);
                        this.txtSMonthType2_2.Text = Convert.ToString(payRegistration.SMonthType2_2);
                        this.txtSMonthType2_3.Text = Convert.ToString(payRegistration.SMonthType2_3);
                        this.txtSMonthType2_4.Text = Convert.ToString(payRegistration.SMonthType2_4);
                        this.txtSMonthType2.Text = Convert.ToString(payRegistration.SMonthType2_1 + payRegistration.SMonthType2_2 + payRegistration.SMonthType2_3 + payRegistration.SMonthType2_4);//安全技术 当月费用小计

                        this.txtSMonthType3_1.Text = Convert.ToString(payRegistration.SMonthType3_1);
                        this.txtSMonthType3_2.Text = Convert.ToString(payRegistration.SMonthType3_2);
                        this.txtSMonthType3_3.Text = Convert.ToString(payRegistration.SMonthType3_3);
                        this.txtSMonthType3_4.Text = Convert.ToString(payRegistration.SMonthType3_4);
                        this.txtSMonthType3_5.Text = Convert.ToString(payRegistration.SMonthType3_5);
                        this.txtSMonthType3_6.Text = Convert.ToString(payRegistration.SMonthType3_6);
                        this.txtSMonthType3.Text = Convert.ToString(payRegistration.SMonthType3_1 + payRegistration.SMonthType3_2 + payRegistration.SMonthType3_3 + payRegistration.SMonthType3_4 + payRegistration.SMonthType3_5 + payRegistration.SMonthType3_6);//职业健康 当月费用小计

                        
                        this.txtSMonthType4_1.Text = Convert.ToString(payRegistration.SMonthType4_1);
                        this.txtSMonthType4_2.Text = Convert.ToString(payRegistration.SMonthType4_2);
                        this.txtSMonthType4_3.Text = Convert.ToString(payRegistration.SMonthType4_3);
                        this.txtSMonthType4_4.Text = Convert.ToString(payRegistration.SMonthType4_4);
                        this.txtSMonthType4_5.Text = Convert.ToString(payRegistration.SMonthType4_5);
                        this.txtSMonthType4_6.Text = Convert.ToString(payRegistration.SMonthType4_6);
                        this.txtSMonthType4_7.Text = Convert.ToString(payRegistration.SMonthType4_7);
                        this.txtSMonthType4_8.Text = Convert.ToString(payRegistration.SMonthType4_8);
                        this.txtSMonthType4_9.Text = Convert.ToString(payRegistration.SMonthType4_9);
                        this.txtSMonthType4_10.Text = Convert.ToString(payRegistration.SMonthType4_10);
                        this.txtSMonthType4_11.Text = Convert.ToString(payRegistration.SMonthType4_11);
                        this.txtSMonthType4_12.Text = Convert.ToString(payRegistration.SMonthType4_12);
                        this.txtSMonthType4_13.Text = Convert.ToString(payRegistration.SMonthType4_13);
                        this.txtSMonthType4_14.Text = Convert.ToString(payRegistration.SMonthType4_14);
                        this.txtSMonthType4_15.Text = Convert.ToString(payRegistration.SMonthType4_15);
                        this.txtSMonthType4_16.Text = Convert.ToString(payRegistration.SMonthType4_16);
                        this.txtSMonthType4_17.Text = Convert.ToString(payRegistration.SMonthType4_17);
                        this.txtSMonthType4_18.Text = Convert.ToString(payRegistration.SMonthType4_18);
                        this.txtSMonthType4_19.Text = Convert.ToString(payRegistration.SMonthType4_19);
                        this.txtSMonthType4_20.Text = Convert.ToString(payRegistration.SMonthType4_20);
                        this.txtSMonthType4_21.Text = Convert.ToString(payRegistration.SMonthType4_21);
                        this.txtSMonthType4_22.Text = Convert.ToString(payRegistration.SMonthType4_22);
                        this.txtSMonthType4_23.Text = Convert.ToString(payRegistration.SMonthType4_23);
                        this.txtSMonthType4_24.Text = Convert.ToString(payRegistration.SMonthType4_24);
                        this.txtSMonthType4_25.Text = Convert.ToString(payRegistration.SMonthType4_25);
                        this.txtSMonthType4_26.Text = Convert.ToString(payRegistration.SMonthType4_26);
                        this.txtSMonthType4_27.Text = Convert.ToString(payRegistration.SMonthType4_27);
                        this.txtSMonthType4_28.Text = Convert.ToString(payRegistration.SMonthType4_28);
                        this.txtSMonthType4_29.Text = Convert.ToString(payRegistration.SMonthType4_29);
                        this.txtSMonthType4_30.Text = Convert.ToString(payRegistration.SMonthType4_30);
                        this.txtSMonthType4_31.Text = Convert.ToString(payRegistration.SMonthType4_31);
                        this.txtSMonthType4_32.Text = Convert.ToString(payRegistration.SMonthType4_32);
                        this.txtSMonthType4_33.Text = Convert.ToString(payRegistration.SMonthType4_33);
                        this.txtSMonthType4_34.Text = Convert.ToString(payRegistration.SMonthType4_34);
                        this.txtSMonthType4_35.Text = Convert.ToString(payRegistration.SMonthType4_35);
                        this.txtSMonthType4_36.Text = Convert.ToString(payRegistration.SMonthType4_36);
                        this.txtSMonthType4_37.Text = Convert.ToString(payRegistration.SMonthType4_37);
                        this.txtSMonthType4_38.Text = Convert.ToString(payRegistration.SMonthType4_38);
                        this.txtSMonthType4_39.Text = Convert.ToString(payRegistration.SMonthType4_39);
                        this.txtSMonthType4_40.Text = Convert.ToString(payRegistration.SMonthType4_40);
                        this.txtSMonthType4.Text = Convert.ToString(payRegistration.SMonthType4_1 + payRegistration.SMonthType4_2 + payRegistration.SMonthType4_3 + payRegistration.SMonthType4_4 + payRegistration.SMonthType4_5 + payRegistration.SMonthType4_6 + payRegistration.SMonthType4_7 + payRegistration.SMonthType4_8 + payRegistration.SMonthType4_9 + payRegistration.SMonthType4_10 + payRegistration.SMonthType4_11 + payRegistration.SMonthType4_12 + payRegistration.SMonthType4_13 + payRegistration.SMonthType4_14 + payRegistration.SMonthType4_15 + payRegistration.SMonthType4_16 + payRegistration.SMonthType4_17 + payRegistration.SMonthType4_18 + payRegistration.SMonthType4_19 + payRegistration.SMonthType4_20 + payRegistration.SMonthType4_21 + payRegistration.SMonthType4_22 + payRegistration.SMonthType4_23 + payRegistration.SMonthType4_24 + payRegistration.SMonthType4_25 + payRegistration.SMonthType4_26 + payRegistration.SMonthType4_27 + payRegistration.SMonthType4_28 + payRegistration.SMonthType4_29 + payRegistration.SMonthType4_30 + payRegistration.SMonthType4_31 + payRegistration.SMonthType4_32 + payRegistration.SMonthType4_33 + payRegistration.SMonthType4_34 + payRegistration.SMonthType4_35 + payRegistration.SMonthType4_36 + payRegistration.SMonthType4_37 + payRegistration.SMonthType4_38 + payRegistration.SMonthType4_39 + payRegistration.SMonthType4_40);//防护措施 当月费用小计

                     
                        this.txtSMonthType5_1.Text = Convert.ToString(payRegistration.SMonthType5_1);
                        this.txtSMonthType5_2.Text = Convert.ToString(payRegistration.SMonthType5_2);
                        this.txtSMonthType5_3.Text = Convert.ToString(payRegistration.SMonthType5_3);
                        this.txtSMonthType5_4.Text = Convert.ToString(payRegistration.SMonthType5_4);
                        this.txtSMonthType5_5.Text = Convert.ToString(payRegistration.SMonthType5_5);
                        this.txtSMonthType5_6.Text = Convert.ToString(payRegistration.SMonthType5_6);
                        this.txtSMonthType5.Text = Convert.ToString(payRegistration.SMonthType5_1 + payRegistration.SMonthType5_2 + payRegistration.SMonthType5_3 + payRegistration.SMonthType5_4 + payRegistration.SMonthType5_5 + payRegistration.SMonthType5_6);//化工试车 当月费用小计

                        this.txtSMonthType6_1.Text = Convert.ToString(payRegistration.SMonthType6_1);
                        this.txtSMonthType6_2.Text = Convert.ToString(payRegistration.SMonthType6_2);
                        this.txtSMonthType6_3.Text = Convert.ToString(payRegistration.SMonthType6_3);
                        this.txtSMonthType6.Text = Convert.ToString(payRegistration.SMonthType6_1 + payRegistration.SMonthType6_2 + payRegistration.SMonthType6_3);//教育培训 当月费用小计

                        this.txtTMonthType1_1.Text = Convert.ToString(payRegistration.TMonthType1_1);
                        this.txtTMonthType1_2.Text = Convert.ToString(payRegistration.TMonthType1_2);
                        this.txtTMonthType1_3.Text = Convert.ToString(payRegistration.TMonthType1_3);
                        this.txtTMonthType1_4.Text = Convert.ToString(payRegistration.TMonthType1_4);
                        this.txtTMonthType1_5.Text = Convert.ToString(payRegistration.TMonthType1_5);
                        this.txtTMonthType1_6.Text = Convert.ToString(payRegistration.TMonthType1_6);
                        this.txtTMonthType1_7.Text = Convert.ToString(payRegistration.TMonthType1_7);
                        this.txtTMonthType1_8.Text = Convert.ToString(payRegistration.TMonthType1_8);
                        this.txtTMonthType1_9.Text = Convert.ToString(payRegistration.TMonthType1_9);
                        this.txtTMonthType1_10.Text = Convert.ToString(payRegistration.TMonthType1_10);
                        this.txtTMonthType1_11.Text = Convert.ToString(payRegistration.TMonthType1_11);
                        this.txtTMonthType1.Text = Convert.ToString(payRegistration.TMonthType1_1 + payRegistration.TMonthType1_2 + payRegistration.TMonthType1_3 + payRegistration.TMonthType1_4 + payRegistration.TMonthType1_5 + payRegistration.TMonthType1_6 + payRegistration.TMonthType1_7 + payRegistration.TMonthType1_8 + payRegistration.TMonthType1_9 + payRegistration.TMonthType1_10 + payRegistration.TMonthType1_11);//文明施工和环境保护 当月费用小计

                        this.txtTMonthType2_1.Text = Convert.ToString(payRegistration.TMonthType2_1);
                        this.txtTMonthType2_2.Text = Convert.ToString(payRegistration.TMonthType2_2);
                        this.txtTMonthType2_3.Text = Convert.ToString(payRegistration.TMonthType2_3);
                        this.txtTMonthType2_4.Text = Convert.ToString(payRegistration.TMonthType2_4);
                        this.txtTMonthType2_5.Text = Convert.ToString(payRegistration.TMonthType2_5);
                        this.txtTMonthType2_6.Text = Convert.ToString(payRegistration.TMonthType2_6);
                        this.txtTMonthType2_7.Text = Convert.ToString(payRegistration.TMonthType2_7);
                        this.txtTMonthType2_8.Text = Convert.ToString(payRegistration.TMonthType2_8);
                        this.txtTMonthType2_9.Text = Convert.ToString(payRegistration.TMonthType2_9);
                        this.txtTMonthType2.Text = Convert.ToString(payRegistration.TMonthType2_1 + payRegistration.TMonthType2_2 + payRegistration.TMonthType2_3 + payRegistration.TMonthType2_4 + payRegistration.TMonthType2_5 + payRegistration.TMonthType2_6 + payRegistration.TMonthType2_7 + payRegistration.TMonthType2_8 + payRegistration.TMonthType2_9);//临时设施 费用小计

                        this.txtMonthType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType3.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType4.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType5.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType6.Text) + Funs.GetNewDecimalOrZero(this.txtTMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtTMonthType2.Text));
                    }
                }
                else
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                    if (unit != null)
                    {
                        this.lblUnitName.Text = unit.UnitName;
                    }
                    this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                #region  当年累计
                var payRegistrations = BLL.PayRegistrationService.GetPayRegistrationTotal(this.CurrUser.LoginProjectId,BLL.Funs.GetNewDateTime(this.txtDate.Text.Trim()));
                if (payRegistrations != null)
                {                    
                    decimal? sTotalType1_1 = 0, sTotalType1_2 = 0, sTotalType1_3 = 0, sTotalType1_4 = 0, sTotalType1_5 = 0, sTotalType1_6 = 0, sTotalType1_7 = 0, sTotalType1_8 = 0, sTotalType1_9 = 0, sTotalType1_10 = 0, sTotalType1_11 = 0, sTotalType1_12 = 0, sTotalType1_13 = 0, sTotalType1_14 = 0, sTotalType1_15 = 0, sTotalType1_16 = 0;
                    decimal? sTotalType2_1 = 0, sTotalType2_2 = 0, sTotalType2_3 = 0, sTotalType2_4 = 0;
                    decimal? sTotalType3_1 = 0, sTotalType3_2 = 0, sTotalType3_3 = 0, sTotalType3_4 = 0, sTotalType3_5 = 0, sTotalType3_6 = 0;
                    decimal? sTotalType4_1 = 0, sTotalType4_2 = 0, sTotalType4_3 = 0, sTotalType4_4 = 0, sTotalType4_5 = 0, sTotalType4_6 = 0, sTotalType4_7 = 0, sTotalType4_8 = 0, sTotalType4_9 = 0, sTotalType4_10 = 0, sTotalType4_11 = 0, sTotalType4_12 = 0, sTotalType4_13 = 0, sTotalType4_14 = 0, sTotalType4_15 = 0, sTotalType4_16 = 0, sTotalType4_17 = 0, sTotalType4_18 = 0, sTotalType4_19 = 0, sTotalType4_20 = 0, sTotalType4_21 = 0, sTotalType4_22 = 0, sTotalType4_23 = 0, sTotalType4_24 = 0, sTotalType4_25 = 0, sTotalType4_26 = 0, sTotalType4_27 = 0, sTotalType4_28 = 0, sTotalType4_29 = 0, sTotalType4_30 = 0, sTotalType4_31 = 0, sTotalType4_32 = 0, sTotalType4_33 = 0, sTotalType4_34 = 0, sTotalType4_35 = 0, sTotalType4_36 = 0, sTotalType4_37 = 0, sTotalType4_38 = 0, sTotalType4_39 = 0, sTotalType4_40 = 0;
                    decimal? sTotalType5_1 = 0, sTotalType5_2 = 0, sTotalType5_3 = 0, sTotalType5_4 = 0, sTotalType5_5 = 0, sTotalType5_6 = 0;
                    decimal? sTotalType6_1 = 0, sTotalType6_2 = 0, sTotalType6_3 = 0;
                    decimal? tTotalType1_1 = 0, tTotalType1_2 = 0, tTotalType1_3 = 0, tTotalType1_4 = 0, tTotalType1_5 = 0, tTotalType1_6 = 0, tTotalType1_7 = 0, tTotalType1_8 = 0, tTotalType1_9 = 0, tTotalType1_10 = 0, tTotalType1_11 = 0;
                    decimal? tTotalType2_1 = 0, tTotalType2_2 = 0, tTotalType2_3 = 0, tTotalType2_4 = 0, tTotalType2_5 = 0, tTotalType2_6 = 0, tTotalType2_7 = 0, tTotalType2_8 = 0, tTotalType2_9 = 0;
                    foreach (var item in payRegistrations)
                    {
                        sTotalType1_1 += item.SMonthType1_1;
                        sTotalType1_2 += item.SMonthType1_2;
                        sTotalType1_3 += item.SMonthType1_3;
                        sTotalType1_4 += item.SMonthType1_4;
                        sTotalType1_5 += item.SMonthType1_5;
                        sTotalType1_6 += item.SMonthType1_6;
                        sTotalType1_7 += item.SMonthType1_7;
                        sTotalType1_8 += item.SMonthType1_8;
                        sTotalType1_9 += item.SMonthType1_9;
                        sTotalType1_10 += item.SMonthType1_10;
                        sTotalType1_11 += item.SMonthType1_11;
                        sTotalType1_12 += item.SMonthType1_12;
                        sTotalType1_13 += item.SMonthType1_13;
                        sTotalType1_14 += item.SMonthType1_14;
                        sTotalType1_15 += item.SMonthType1_15;
                        sTotalType1_16 += item.SMonthType1_16;

                        sTotalType2_1 += item.SMonthType2_1;
                        sTotalType2_2 += item.SMonthType2_2;
                        sTotalType2_3 += item.SMonthType2_3;
                        sTotalType2_4 += item.SMonthType2_4;

                        sTotalType3_1 += item.SMonthType3_1;
                        sTotalType3_2 += item.SMonthType3_2;
                        sTotalType3_3 += item.SMonthType3_3;
                        sTotalType3_4 += item.SMonthType3_4;
                        sTotalType3_5 += item.SMonthType3_5;
                        sTotalType3_6 += item.SMonthType3_6;

                        sTotalType4_1 += item.SMonthType4_1;
                        sTotalType4_2 += item.SMonthType4_2;
                        sTotalType4_3 += item.SMonthType4_3;
                        sTotalType4_4 += item.SMonthType4_4;
                        sTotalType4_5 += item.SMonthType4_5;
                        sTotalType4_6 += item.SMonthType4_6;
                        sTotalType4_7 += item.SMonthType4_7;
                        sTotalType4_8 += item.SMonthType4_8;
                        sTotalType4_9 += item.SMonthType4_9;
                        sTotalType4_10 += item.SMonthType4_10;
                        sTotalType4_11 += item.SMonthType4_11;
                        sTotalType4_12 += item.SMonthType4_12;
                        sTotalType4_13 += item.SMonthType4_13;
                        sTotalType4_14 += item.SMonthType4_14;
                        sTotalType4_15 += item.SMonthType4_15;
                        sTotalType4_16 += item.SMonthType4_16;
                        sTotalType4_17 += item.SMonthType4_17;
                        sTotalType4_18 += item.SMonthType4_18;
                        sTotalType4_19 += item.SMonthType4_19;
                        sTotalType4_20 += item.SMonthType4_20;
                        sTotalType4_21 += item.SMonthType4_21;
                        sTotalType4_22 += item.SMonthType4_22;
                        sTotalType4_23 += item.SMonthType4_23;
                        sTotalType4_24 += item.SMonthType4_24;
                        sTotalType4_25 += item.SMonthType4_25;
                        sTotalType4_26 += item.SMonthType4_26;
                        sTotalType4_27 += item.SMonthType4_27;
                        sTotalType4_28 += item.SMonthType4_28;
                        sTotalType4_29 += item.SMonthType4_29;
                        sTotalType4_30 += item.SMonthType4_30;
                        sTotalType4_31 += item.SMonthType4_31;
                        sTotalType4_32 += item.SMonthType4_32;
                        sTotalType4_33 += item.SMonthType4_33;
                        sTotalType4_34 += item.SMonthType4_34;
                        sTotalType4_35 += item.SMonthType4_35;
                        sTotalType4_36 += item.SMonthType4_36;
                        sTotalType4_37 += item.SMonthType4_37;
                        sTotalType4_38 += item.SMonthType4_38;
                        sTotalType4_39 += item.SMonthType4_39;
                        sTotalType4_40 += item.SMonthType4_40;

                        sTotalType5_1 += item.SMonthType5_1;
                        sTotalType5_2 += item.SMonthType5_2;
                        sTotalType5_3 += item.SMonthType5_3;
                        sTotalType5_4 += item.SMonthType5_4;
                        sTotalType5_5 += item.SMonthType5_5;
                        sTotalType5_6 += item.SMonthType5_6;

                        sTotalType6_1 += item.SMonthType6_1;
                        sTotalType6_2 += item.SMonthType6_2;
                        sTotalType6_3 += item.SMonthType6_3;

                        tTotalType1_1 += item.TMonthType1_1;
                        tTotalType1_2 += item.TMonthType1_2;
                        tTotalType1_3 += item.TMonthType1_3;
                        tTotalType1_4 += item.TMonthType1_4;
                        tTotalType1_5 += item.TMonthType1_5;
                        tTotalType1_6 += item.TMonthType1_6;
                        tTotalType1_7 += item.TMonthType1_7;
                        tTotalType1_8 += item.TMonthType1_8;
                        tTotalType1_9 += item.TMonthType1_9;
                        tTotalType1_10 += item.TMonthType1_10;
                        tTotalType1_11 += item.TMonthType1_11;

                        tTotalType2_1 += item.TMonthType2_1;
                        tTotalType2_2 += item.TMonthType2_2;
                        tTotalType2_3 += item.TMonthType2_3;
                        tTotalType2_4 += item.TMonthType2_4;
                        tTotalType2_5 += item.TMonthType2_5;
                        tTotalType2_6 += item.TMonthType2_6;
                        tTotalType2_7 += item.TMonthType2_7;
                        tTotalType2_8 += item.TMonthType2_8;
                        tTotalType2_9 += item.TMonthType2_9;
                    }
                    this.txtSTotalType1_1.Text = Convert.ToString(sTotalType1_1);
                    this.txtSTotalType1_2.Text = Convert.ToString(sTotalType1_2);
                    this.txtSTotalType1_3.Text = Convert.ToString(sTotalType1_3);
                    this.txtSTotalType1_4.Text = Convert.ToString(sTotalType1_4);
                    this.txtSTotalType1_5.Text = Convert.ToString(sTotalType1_5);
                    this.txtSTotalType1_6.Text = Convert.ToString(sTotalType1_6);
                    this.txtSTotalType1_7.Text = Convert.ToString(sTotalType1_7);
                    this.txtSTotalType1_8.Text = Convert.ToString(sTotalType1_8);
                    this.txtSTotalType1_9.Text = Convert.ToString(sTotalType1_9);
                    this.txtSTotalType1_10.Text = Convert.ToString(sTotalType1_10);
                    this.txtSTotalType1_11.Text = Convert.ToString(sTotalType1_11);
                    this.txtSTotalType1_12.Text = Convert.ToString(sTotalType1_12);
                    this.txtSTotalType1_13.Text = Convert.ToString(sTotalType1_13);
                    this.txtSTotalType1_14.Text = Convert.ToString(sTotalType1_14);
                    this.txtSTotalType1_15.Text = Convert.ToString(sTotalType1_15);
                    this.txtSTotalType1_16.Text = Convert.ToString(sTotalType1_16);
                    this.txtSTotalType1.Text = Convert.ToString(sTotalType1_1 + sTotalType1_2 + sTotalType1_3 + sTotalType1_4 + sTotalType1_5 + sTotalType1_6 + sTotalType1_7 + sTotalType1_8 + sTotalType1_9 + sTotalType1_10 + sTotalType1_11 + sTotalType1_12 + sTotalType1_13 + sTotalType1_14 + sTotalType1_15 + sTotalType1_16);

                    this.txtSTotalType2_1.Text = Convert.ToString(sTotalType2_1);
                    this.txtSTotalType2_2.Text = Convert.ToString(sTotalType2_2);
                    this.txtSTotalType2_3.Text = Convert.ToString(sTotalType2_3);
                    this.txtSTotalType2_4.Text = Convert.ToString(sTotalType2_4);
                    this.txtSTotalType2.Text = Convert.ToString(sTotalType2_1 + sTotalType2_2 + sTotalType2_3 + sTotalType2_4);

                    this.txtSTotalType3_1.Text = Convert.ToString(sTotalType3_1);
                    this.txtSTotalType3_2.Text = Convert.ToString(sTotalType3_2);
                    this.txtSTotalType3_3.Text = Convert.ToString(sTotalType3_3);
                    this.txtSTotalType3_4.Text = Convert.ToString(sTotalType3_4);
                    this.txtSTotalType3_5.Text = Convert.ToString(sTotalType3_5);
                    this.txtSTotalType3_6.Text = Convert.ToString(sTotalType3_6);
                    this.txtSTotalType3.Text = Convert.ToString(sTotalType3_1 + sTotalType3_2 + sTotalType3_3 + sTotalType3_4 + sTotalType3_5 + sTotalType3_6);

                    this.txtSTotalType4_1.Text = Convert.ToString(sTotalType4_1);
                    this.txtSTotalType4_2.Text = Convert.ToString(sTotalType4_2);
                    this.txtSTotalType4_3.Text = Convert.ToString(sTotalType4_3);
                    this.txtSTotalType4_4.Text = Convert.ToString(sTotalType4_4);
                    this.txtSTotalType4_5.Text = Convert.ToString(sTotalType4_5);
                    this.txtSTotalType4_6.Text = Convert.ToString(sTotalType4_6);
                    this.txtSTotalType4_7.Text = Convert.ToString(sTotalType4_7);
                    this.txtSTotalType4_8.Text = Convert.ToString(sTotalType4_8);
                    this.txtSTotalType4_9.Text = Convert.ToString(sTotalType4_9);
                    this.txtSTotalType4_10.Text = Convert.ToString(sTotalType4_10);
                    this.txtSTotalType4_11.Text = Convert.ToString(sTotalType4_11);
                    this.txtSTotalType4_12.Text = Convert.ToString(sTotalType4_12);
                    this.txtSTotalType4_13.Text = Convert.ToString(sTotalType4_13);
                    this.txtSTotalType4_14.Text = Convert.ToString(sTotalType4_14);
                    this.txtSTotalType4_15.Text = Convert.ToString(sTotalType4_15);
                    this.txtSTotalType4_16.Text = Convert.ToString(sTotalType4_16);
                    this.txtSTotalType4_17.Text = Convert.ToString(sTotalType4_17);
                    this.txtSTotalType4_18.Text = Convert.ToString(sTotalType4_18);
                    this.txtSTotalType4_19.Text = Convert.ToString(sTotalType4_19);
                    this.txtSTotalType4_20.Text = Convert.ToString(sTotalType4_20);
                    this.txtSTotalType4_21.Text = Convert.ToString(sTotalType4_21);
                    this.txtSTotalType4_22.Text = Convert.ToString(sTotalType4_22);
                    this.txtSTotalType4_23.Text = Convert.ToString(sTotalType4_23);
                    this.txtSTotalType4_24.Text = Convert.ToString(sTotalType4_24);
                    this.txtSTotalType4_25.Text = Convert.ToString(sTotalType4_25);
                    this.txtSTotalType4_26.Text = Convert.ToString(sTotalType4_26);
                    this.txtSTotalType4_27.Text = Convert.ToString(sTotalType4_27);
                    this.txtSTotalType4_28.Text = Convert.ToString(sTotalType4_28);
                    this.txtSTotalType4_29.Text = Convert.ToString(sTotalType4_29);
                    this.txtSTotalType4_30.Text = Convert.ToString(sTotalType4_30);
                    this.txtSTotalType4_31.Text = Convert.ToString(sTotalType4_31);
                    this.txtSTotalType4_32.Text = Convert.ToString(sTotalType4_32);
                    this.txtSTotalType4_33.Text = Convert.ToString(sTotalType4_33);
                    this.txtSTotalType4_34.Text = Convert.ToString(sTotalType4_34);
                    this.txtSTotalType4_35.Text = Convert.ToString(sTotalType4_35);
                    this.txtSTotalType4_36.Text = Convert.ToString(sTotalType4_36);
                    this.txtSTotalType4_37.Text = Convert.ToString(sTotalType4_37);
                    this.txtSTotalType4_38.Text = Convert.ToString(sTotalType4_38);
                    this.txtSTotalType4_39.Text = Convert.ToString(sTotalType4_39);
                    this.txtSTotalType4_40.Text = Convert.ToString(sTotalType4_40);
                    this.txtSTotalType4.Text = Convert.ToString(sTotalType4_1 + sTotalType4_2 + sTotalType4_3 + sTotalType4_4 + sTotalType4_5 + sTotalType4_6 + sTotalType4_7 + sTotalType4_8 + sTotalType4_9 + sTotalType4_10 + sTotalType4_11 + sTotalType4_12 + sTotalType4_13 + sTotalType4_14 + sTotalType4_15 + sTotalType4_16 + sTotalType4_17 + sTotalType4_18 + sTotalType4_19 + sTotalType4_20 + sTotalType4_21 + sTotalType4_22 + sTotalType4_23 + sTotalType4_24 + sTotalType4_25 + sTotalType4_26 + sTotalType4_27 + sTotalType4_28 + sTotalType4_29 + sTotalType4_30 + sTotalType4_31 + sTotalType4_32 + sTotalType4_33 + sTotalType4_34 + sTotalType4_35 + sTotalType4_36 + sTotalType4_37 + sTotalType4_38 + sTotalType4_39 + sTotalType4_40);

                    this.txtSTotalType5_1.Text = Convert.ToString(sTotalType5_1);
                    this.txtSTotalType5_2.Text = Convert.ToString(sTotalType5_2);
                    this.txtSTotalType5_3.Text = Convert.ToString(sTotalType5_3);
                    this.txtSTotalType5_4.Text = Convert.ToString(sTotalType5_4);
                    this.txtSTotalType5_5.Text = Convert.ToString(sTotalType5_5);
                    this.txtSTotalType5_6.Text = Convert.ToString(sTotalType5_6);
                    this.txtSTotalType5.Text = Convert.ToString(sTotalType5_1 + sTotalType5_2 + sTotalType5_3 + sTotalType5_4 + sTotalType5_5 + sTotalType5_6);

                    this.txtSTotalType6_1.Text = Convert.ToString(sTotalType6_1);
                    this.txtSTotalType6_2.Text = Convert.ToString(sTotalType6_2);
                    this.txtSTotalType6_3.Text = Convert.ToString(sTotalType6_3);
                    this.txtSMonthType6.Text = Convert.ToString(sTotalType6_1 + sTotalType6_2 + sTotalType6_3);

                    this.txtTTotalType1_1.Text = Convert.ToString(tTotalType1_1);
                    this.txtTTotalType1_2.Text = Convert.ToString(tTotalType1_2);
                    this.txtTTotalType1_3.Text = Convert.ToString(tTotalType1_3);
                    this.txtTTotalType1_4.Text = Convert.ToString(tTotalType1_4);
                    this.txtTTotalType1_5.Text = Convert.ToString(tTotalType1_5);
                    this.txtTTotalType1_6.Text = Convert.ToString(tTotalType1_6);
                    this.txtTTotalType1_7.Text = Convert.ToString(tTotalType1_7);
                    this.txtTTotalType1_8.Text = Convert.ToString(tTotalType1_8);
                    this.txtTTotalType1_9.Text = Convert.ToString(tTotalType1_9);
                    this.txtTTotalType1_10.Text = Convert.ToString(tTotalType1_10);
                    this.txtTTotalType1_11.Text = Convert.ToString(tTotalType1_11);
                    this.txtTTotalType1.Text = Convert.ToString(tTotalType1_1 + tTotalType1_2 + tTotalType1_3 + tTotalType1_4 + tTotalType1_5 + tTotalType1_6 + tTotalType1_7 + tTotalType1_8 + tTotalType1_9 + tTotalType1_10 + tTotalType1_11);

                    this.txtTTotalType2_1.Text = Convert.ToString(tTotalType2_1);
                    this.txtTTotalType2_2.Text = Convert.ToString(tTotalType2_2);
                    this.txtTTotalType2_3.Text = Convert.ToString(tTotalType2_3);
                    this.txtTTotalType2_4.Text = Convert.ToString(tTotalType2_4);
                    this.txtTTotalType2_5.Text = Convert.ToString(tTotalType2_5);
                    this.txtTTotalType2_6.Text = Convert.ToString(tTotalType2_6);
                    this.txtTTotalType2_7.Text = Convert.ToString(tTotalType2_7);
                    this.txtTTotalType2_8.Text = Convert.ToString(tTotalType2_8);
                    this.txtTTotalType2_9.Text = Convert.ToString(tTotalType2_9);
                    this.txtTTotalType2.Text = Convert.ToString(tTotalType2_1 + tTotalType2_2 + tTotalType2_3 + tTotalType2_4 + tTotalType2_5 + tTotalType2_6 + tTotalType2_7 + tTotalType2_8 + tTotalType2_9);

                    this.txtTotalType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSTotalType1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSTotalType2.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSTotalType3.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSTotalType4.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSTotalType5.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSTotalType6.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtTTotalType1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtTTotalType2.Text.Trim()));
                }
                #endregion

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
            //基础管理当月累计
            decimal sMonthType1_1 = 0, sMonthType1_2 = 0, sMonthType1_3 = 0, sMonthType1_4 = 0, sMonthType1_5 = 0, sMonthType1_6 = 0, sMonthType1_7 = 0, sMonthType1_8 = 0, sMonthType1_9 = 0, sMonthType1_10 = 0, sMonthType1_11 = 0, sMonthType1_12 = 0, sMonthType1_13 = 0, sMonthType1_14 = 0, sMonthType1_15 = 0, sMonthType1_16 = 0;
            sMonthType1_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_1.Text.Trim());
            sMonthType1_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_2.Text.Trim());
            sMonthType1_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_3.Text.Trim());
            sMonthType1_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_4.Text.Trim());
            sMonthType1_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_5.Text.Trim());
            sMonthType1_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_6.Text.Trim());
            sMonthType1_7 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_7.Text.Trim());
            sMonthType1_8 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_8.Text.Trim());
            sMonthType1_9 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_9.Text.Trim());
            sMonthType1_10 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_10.Text.Trim());
            sMonthType1_11 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_11.Text.Trim());
            sMonthType1_12 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_12.Text.Trim());
            sMonthType1_13 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_13.Text.Trim());
            sMonthType1_14 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_14.Text.Trim());
            sMonthType1_15 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_15.Text.Trim());
            sMonthType1_16 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_16.Text.Trim());
            this.txtSMonthType1.Text = Convert.ToString(sMonthType1_1 + sMonthType1_2 + sMonthType1_3 + sMonthType1_4 + sMonthType1_5 + sMonthType1_6 + sMonthType1_7 + sMonthType1_8 + sMonthType1_9 + sMonthType1_10 + sMonthType1_11 + sMonthType1_12 + sMonthType1_13 + sMonthType1_14 + sMonthType1_15 + sMonthType1_16);

            //安全技术当月累计
            decimal sMonthType2_1 = 0, sMonthType2_2 = 0, sMonthType2_3 = 0, sMonthType2_4 = 0;
            sMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_1.Text.Trim());
            sMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_2.Text.Trim());
            sMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_3.Text.Trim());
            sMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_4.Text.Trim());
            this.txtSMonthType2.Text = Convert.ToString(sMonthType2_1 + sMonthType2_2 + sMonthType2_3 + sMonthType2_4);

            //职业健康当月累计
            decimal sMonthType3_1 = 0, sMonthType3_2 = 0, sMonthType3_3 = 0, sMonthType3_4 = 0, sMonthType3_5 = 0, sMonthType3_6 = 0;
            sMonthType3_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_1.Text.Trim());
            sMonthType3_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_2.Text.Trim());
            sMonthType3_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_3.Text.Trim());
            sMonthType3_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_4.Text.Trim());
            sMonthType3_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_5.Text.Trim());
            sMonthType3_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_6.Text.Trim());
            this.txtSMonthType3.Text = Convert.ToString(sMonthType3_1 + sMonthType3_2 + sMonthType3_3 + sMonthType3_4 + sMonthType3_5 + sMonthType3_6);

            //防护措施 当月累计
            decimal sMonthType4_1 = 0, sMonthType4_2 = 0, sMonthType4_3 = 0, sMonthType4_4 = 0, sMonthType4_5 = 0, sMonthType4_6 = 0, sMonthType4_7 = 0, sMonthType4_8 = 0, sMonthType4_9 = 0, sMonthType4_10 = 0, sMonthType4_11 = 0, sMonthType4_12 = 0, sMonthType4_13 = 0, sMonthType4_14 = 0, sMonthType4_15 = 0, sMonthType4_16 = 0, sMonthType4_17 = 0, sMonthType4_18 = 0, sMonthType4_19 = 0, sMonthType4_20 = 0, sMonthType4_21 = 0, sMonthType4_22 = 0, sMonthType4_23 = 0, sMonthType4_24 = 0, sMonthType4_25 = 0, sMonthType4_26 = 0, sMonthType4_27 = 0, sMonthType4_28 = 0, sMonthType4_29 = 0, sMonthType4_30 = 0, sMonthType4_31 = 0, sMonthType4_32 = 0, sMonthType4_33 = 0, sMonthType4_34 = 0, sMonthType4_35 = 0, sMonthType4_36 = 0, sMonthType4_37 = 0, sMonthType4_38 = 0, sMonthType4_39 = 0, sMonthType4_40 = 0;
            sMonthType4_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_1.Text.Trim());
            sMonthType4_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_2.Text.Trim());
            sMonthType4_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_3.Text.Trim());
            sMonthType4_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_4.Text.Trim());
            sMonthType4_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_5.Text.Trim());
            sMonthType4_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_6.Text.Trim());
            sMonthType4_7 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_7.Text.Trim());
            sMonthType4_8 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_8.Text.Trim());
            sMonthType4_9 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_9.Text.Trim());
            sMonthType4_10 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_10.Text.Trim());
            sMonthType4_11 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_11.Text.Trim());
            sMonthType4_12 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_12.Text.Trim());
            sMonthType4_13 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_13.Text.Trim());
            sMonthType4_14 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_14.Text.Trim());
            sMonthType4_15 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_15.Text.Trim());
            sMonthType4_16 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_16.Text.Trim());
            sMonthType4_17 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_17.Text.Trim());
            sMonthType4_18 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_18.Text.Trim());
            sMonthType4_19 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_19.Text.Trim());
            sMonthType4_20 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_20.Text.Trim());
            sMonthType4_21 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_21.Text.Trim());
            sMonthType4_22 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_22.Text.Trim());
            sMonthType4_23 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_23.Text.Trim());
            sMonthType4_24 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_24.Text.Trim());
            sMonthType4_25 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_25.Text.Trim());
            sMonthType4_26 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_26.Text.Trim());
            sMonthType4_27 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_27.Text.Trim());
            sMonthType4_28 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_28.Text.Trim());
            sMonthType4_29 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_29.Text.Trim());
            sMonthType4_30 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_30.Text.Trim());
            sMonthType4_31 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_31.Text.Trim());
            sMonthType4_32 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_32.Text.Trim());
            sMonthType4_33 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_33.Text.Trim());
            sMonthType4_34 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_34.Text.Trim());
            sMonthType4_35 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_35.Text.Trim());
            sMonthType4_36 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_36.Text.Trim());
            sMonthType4_37 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_37.Text.Trim());
            sMonthType4_38 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_38.Text.Trim());
            sMonthType4_39 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_39.Text.Trim());
            sMonthType4_40 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_40.Text.Trim());
            this.txtSMonthType4.Text = Convert.ToString(sMonthType4_1 + sMonthType4_2 + sMonthType4_3 + sMonthType4_4 + sMonthType4_5 + sMonthType4_6 + sMonthType4_7 + sMonthType4_8 + sMonthType4_9 + sMonthType4_10 + sMonthType4_11 + sMonthType4_12 + sMonthType4_13 + sMonthType4_14 + sMonthType4_15 + sMonthType4_16 + sMonthType4_17 + sMonthType4_18 + sMonthType4_19 + sMonthType4_20 + sMonthType4_21 + sMonthType4_22 + sMonthType4_23 + sMonthType4_24 + sMonthType4_25 + sMonthType4_26 + sMonthType4_27 + sMonthType4_28 + sMonthType4_29 + sMonthType4_30 + sMonthType4_31 + sMonthType4_32 + sMonthType4_33 + sMonthType4_34 + sMonthType4_35 + sMonthType4_36 + sMonthType4_37 + sMonthType4_38 + sMonthType4_39 + sMonthType4_40);

            //化工试车 当月累计
            decimal sMonthType5_1 = 0, sMonthType5_2 = 0, sMonthType5_3 = 0, sMonthType5_4 = 0, sMonthType5_5 = 0, sMonthType5_6 = 0;
            sMonthType5_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_1.Text.Trim());
            sMonthType5_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_2.Text.Trim());
            sMonthType5_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_3.Text.Trim());
            sMonthType5_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_4.Text.Trim());
            sMonthType5_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_5.Text.Trim());
            sMonthType5_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_6.Text.Trim());
            this.txtSMonthType5.Text = Convert.ToString(sMonthType5_1 + sMonthType5_2 + sMonthType5_3 + sMonthType5_4 + sMonthType5_5 + sMonthType5_6);

            //教育培训 当月累计
            decimal sMonthType6_1 = 0, sMonthType6_2 = 0, sMonthType6_3 = 0;
            sMonthType6_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_1.Text.Trim());
            sMonthType6_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_2.Text.Trim());
            sMonthType6_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_3.Text.Trim());
            this.txtSMonthType6.Text = Convert.ToString(sMonthType6_1 + sMonthType6_2 + sMonthType6_3);

            //文明施工和环境保护 当月累计
            decimal tMonthType1_1 = 0, tMonthType1_2 = 0, tMonthType1_3 = 0, tMonthType1_4 = 0, tMonthType1_5 = 0, tMonthType1_6 = 0, tMonthType1_7 = 0, tMonthType1_8 = 0, tMonthType1_9 = 0, tMonthType1_10 = 0, tMonthType1_11 = 0;
            tMonthType1_1 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_1.Text.Trim());
            tMonthType1_2 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_2.Text.Trim());
            tMonthType1_3 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_3.Text.Trim());
            tMonthType1_4 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_4.Text.Trim());
            tMonthType1_5 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_5.Text.Trim());
            tMonthType1_6 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_6.Text.Trim());
            tMonthType1_7 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_7.Text.Trim());
            tMonthType1_8 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_8.Text.Trim());
            tMonthType1_9 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_9.Text.Trim());
            tMonthType1_10 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_10.Text.Trim());
            tMonthType1_11 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_11.Text.Trim());
            this.txtTMonthType1.Text = Convert.ToString(tMonthType1_1 + tMonthType1_2 + tMonthType1_3 + tMonthType1_4 + tMonthType1_5 + tMonthType1_6 + tMonthType1_7 + tMonthType1_8 + tMonthType1_9 + tMonthType1_10 + tMonthType1_11);

            //临时设施 当月累计
            decimal tMonthType2_1 = 0, tMonthType2_2 = 0, tMonthType2_3 = 0, tMonthType2_4 = 0, tMonthType2_5 = 0, tMonthType2_6 = 0, tMonthType2_7 = 0, tMonthType2_8 = 0, tMonthType2_9 = 0;
            tMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_1.Text.Trim());
            tMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_2.Text.Trim());
            tMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_3.Text.Trim());
            tMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_4.Text.Trim());
            tMonthType2_5 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_5.Text.Trim());
            tMonthType2_6 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_6.Text.Trim());
            tMonthType2_7 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_7.Text.Trim());
            tMonthType2_8 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_8.Text.Trim());
            tMonthType2_9 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_9.Text.Trim());
            this.txtTMonthType2.Text = Convert.ToString(tMonthType2_1 + tMonthType2_2 + tMonthType2_3 + tMonthType2_4 + tMonthType2_5 + tMonthType2_6 + tMonthType2_7 + tMonthType2_8 + tMonthType2_9);

            //费用累计
            this.txtMonthType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType3.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSMonthType4.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSMonthType5.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSMonthType6.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtTMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtTMonthType2.Text));
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
            Model.CostGoods_PayRegistration payRegistration = new Model.CostGoods_PayRegistration
            {
                ProjectId = this.CurrUser.LoginProjectId,
                UnitId = this.CurrUser.UnitId,
                PayDate = Funs.GetNewDateTime(this.txtDate.Text.Trim()),
                SMonthType1_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_1.Text.Trim()),
                SMonthType1_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_2.Text.Trim()),
                SMonthType1_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_3.Text.Trim()),
                SMonthType1_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_4.Text.Trim()),
                SMonthType1_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_5.Text.Trim()),
                SMonthType1_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_6.Text.Trim()),
                SMonthType1_7 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_7.Text.Trim()),
                SMonthType1_8 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_8.Text.Trim()),
                SMonthType1_9 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_9.Text.Trim()),
                SMonthType1_10 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_10.Text.Trim()),
                SMonthType1_11 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_11.Text.Trim()),
                SMonthType1_12 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_12.Text.Trim()),
                SMonthType1_13 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_13.Text.Trim()),
                SMonthType1_14 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_14.Text.Trim()),
                SMonthType1_15 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_15.Text.Trim()),
                SMonthType1_16 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_16.Text.Trim()),
                SMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_1.Text.Trim()),
                SMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_2.Text.Trim()),
                SMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_3.Text.Trim()),
                SMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_4.Text.Trim()),
                SMonthType3_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_1.Text.Trim()),
                SMonthType3_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_2.Text.Trim()),
                SMonthType3_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_3.Text.Trim()),
                SMonthType3_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_4.Text.Trim()),
                SMonthType3_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_5.Text.Trim()),
                SMonthType3_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_6.Text.Trim()),
                SMonthType4_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_1.Text.Trim()),
                SMonthType4_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_2.Text.Trim()),
                SMonthType4_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_3.Text.Trim()),
                SMonthType4_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_4.Text.Trim()),
                SMonthType4_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_5.Text.Trim()),
                SMonthType4_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_6.Text.Trim()),
                SMonthType4_7 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_7.Text.Trim()),
                SMonthType4_8 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_8.Text.Trim()),
                SMonthType4_9 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_9.Text.Trim()),
                SMonthType4_10 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_10.Text.Trim()),
                SMonthType4_11 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_11.Text.Trim()),
                SMonthType4_12 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_12.Text.Trim()),
                SMonthType4_13 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_13.Text.Trim()),
                SMonthType4_14 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_14.Text.Trim()),
                SMonthType4_15 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_15.Text.Trim()),
                SMonthType4_16 = Funs.GetNewDecimalOrZero(this.txtSMonthType1_16.Text.Trim()),
                SMonthType4_17 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_17.Text.Trim()),
                SMonthType4_18 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_18.Text.Trim()),
                SMonthType4_19 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_19.Text.Trim()),
                SMonthType4_20 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_20.Text.Trim()),
                SMonthType4_21 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_21.Text.Trim()),
                SMonthType4_22 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_22.Text.Trim()),
                SMonthType4_23 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_23.Text.Trim()),
                SMonthType4_24 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_24.Text.Trim()),
                SMonthType4_25 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_25.Text.Trim()),
                SMonthType4_26 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_26.Text.Trim()),
                SMonthType4_27 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_27.Text.Trim()),
                SMonthType4_28 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_28.Text.Trim()),
                SMonthType4_29 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_29.Text.Trim()),
                SMonthType4_30 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_30.Text.Trim()),
                SMonthType4_31 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_31.Text.Trim()),
                SMonthType4_32 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_32.Text.Trim()),
                SMonthType4_33 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_33.Text.Trim()),
                SMonthType4_34 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_34.Text.Trim()),
                SMonthType4_35 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_35.Text.Trim()),
                SMonthType4_36 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_36.Text.Trim()),
                SMonthType4_37 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_37.Text.Trim()),
                SMonthType4_38 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_38.Text.Trim()),
                SMonthType4_39 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_39.Text.Trim()),
                SMonthType4_40 = Funs.GetNewDecimalOrZero(this.txtSMonthType4_40.Text.Trim()),
                SMonthType5_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_1.Text.Trim()),
                SMonthType5_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_2.Text.Trim()),
                SMonthType5_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_3.Text.Trim()),
                SMonthType5_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_4.Text.Trim()),
                SMonthType5_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_5.Text.Trim()),
                SMonthType5_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_6.Text.Trim()),
                SMonthType6_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_1.Text.Trim()),
                SMonthType6_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_2.Text.Trim()),
                SMonthType6_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_3.Text.Trim()),
                TMonthType1_1 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_1.Text.Trim()),
                TMonthType1_2 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_2.Text.Trim()),
                TMonthType1_3 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_3.Text.Trim()),
                TMonthType1_4 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_4.Text.Trim()),
                TMonthType1_5 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_5.Text.Trim()),
                TMonthType1_6 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_6.Text.Trim()),
                TMonthType1_7 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_7.Text.Trim()),
                TMonthType1_8 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_8.Text.Trim()),
                TMonthType1_9 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_9.Text.Trim()),
                TMonthType1_10 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_10.Text.Trim()),
                TMonthType1_11 = Funs.GetNewDecimalOrZero(this.txtTMonthType1_11.Text.Trim()),
                TMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_1.Text.Trim()),
                TMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_2.Text.Trim()),
                TMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_3.Text.Trim()),
                TMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_4.Text.Trim()),
                TMonthType2_5 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_5.Text.Trim()),
                TMonthType2_6 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_6.Text.Trim()),
                TMonthType2_7 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_7.Text.Trim()),
                TMonthType2_8 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_8.Text.Trim()),
                TMonthType2_9 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_9.Text.Trim()),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now
            };

            if (string.IsNullOrEmpty(payRegistration.UnitId))
            {
                var thisUnit = BLL.CommonService.GetIsThisUnit();
                if (thisUnit != null)
                {
                    payRegistration.UnitId = thisUnit.UnitId;
                }
            }

            if (!string.IsNullOrEmpty(this.PayRegistrationId))
            {
                payRegistration.PayRegistrationId = this.PayRegistrationId;
                BLL.PayRegistrationService.UpdatePayRegistration(payRegistration);
                BLL.LogService.AddSys_Log(this.CurrUser, null, payRegistration.PayRegistrationId, BLL.Const.ProjectPayRegistrationMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.PayRegistrationId = SQLHelper.GetNewID(typeof(Model.CostGoods_PayRegistration));
                payRegistration.PayRegistrationId = this.PayRegistrationId;
                BLL.PayRegistrationService.AddPayRegistration(payRegistration);
                BLL.LogService.AddSys_Log(this.CurrUser, null, payRegistration.PayRegistrationId, BLL.Const.ProjectPayRegistrationMenuId, BLL.Const.BtnAdd);
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PayRegistrationAttachUrl&type=-1", this.PayRegistrationId, BLL.Const.ProjectPayRegistrationMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.PayRegistrationId))
                {
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PayRegistrationAttachUrl&menuId={1}", this.PayRegistrationId, BLL.Const.ProjectPayRegistrationMenuId)));
            }
        }
        #endregion
    }
}