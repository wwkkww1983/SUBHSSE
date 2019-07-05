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

                        this.txtSMainApproveType1_1.Text = Convert.ToString(payRegistration.SMainApproveType1_1);
                        this.txtSMainApproveType1_2.Text = Convert.ToString(payRegistration.SMainApproveType1_2);
                        this.txtSMainApproveType1_3.Text = Convert.ToString(payRegistration.SMainApproveType1_3);
                        this.txtSMainApproveType1_4.Text = Convert.ToString(payRegistration.SMainApproveType1_4);
                        this.txtSMainApproveType1_5.Text = Convert.ToString(payRegistration.SMainApproveType1_5);
                        this.txtSMainApproveType1_6.Text = Convert.ToString(payRegistration.SMainApproveType1_6);
                        this.txtSMainApproveType1_7.Text = Convert.ToString(payRegistration.SMainApproveType1_7);
                        this.txtSMainApproveType1_8.Text = Convert.ToString(payRegistration.SMainApproveType1_8);
                        this.txtSMainApproveType1_9.Text = Convert.ToString(payRegistration.SMainApproveType1_9);
                        this.txtSMainApproveType1_10.Text = Convert.ToString(payRegistration.SMainApproveType1_10);
                        this.txtSMainApproveType1_11.Text = Convert.ToString(payRegistration.SMainApproveType1_11);
                        this.txtSMainApproveType1_12.Text = Convert.ToString(payRegistration.SMainApproveType1_12);
                        this.txtSMainApproveType1_13.Text = Convert.ToString(payRegistration.SMainApproveType1_13);
                        this.txtSMainApproveType1_14.Text = Convert.ToString(payRegistration.SMainApproveType1_14);
                        this.txtSMainApproveType1_15.Text = Convert.ToString(payRegistration.SMainApproveType1_15);
                        this.txtSMainApproveType1_16.Text = Convert.ToString(payRegistration.SMainApproveType1_16);
                        this.txtSMainApproveType1.Text = Convert.ToString(payRegistration.SMainApproveType1_1 + payRegistration.SMainApproveType1_2 + payRegistration.SMainApproveType1_3 + payRegistration.SMainApproveType1_4 + payRegistration.SMainApproveType1_5 + payRegistration.SMainApproveType1_6 + payRegistration.SMainApproveType1_7 + payRegistration.SMainApproveType1_8 + payRegistration.SMainApproveType1_9 + payRegistration.SMainApproveType1_10 + payRegistration.SMainApproveType1_11 + payRegistration.SMainApproveType1_12 + payRegistration.SMainApproveType1_13 + payRegistration.SMainApproveType1_14 + payRegistration.SMainApproveType1_15 + payRegistration.SMainApproveType1_16);//基础管理 总包审核值 费用小计

                        this.txtSMonthType2_1.Text = Convert.ToString(payRegistration.SMonthType2_1);
                        this.txtSMonthType2_2.Text = Convert.ToString(payRegistration.SMonthType2_2);
                        this.txtSMonthType2_3.Text = Convert.ToString(payRegistration.SMonthType2_3);
                        this.txtSMonthType2_4.Text = Convert.ToString(payRegistration.SMonthType2_4);
                        this.txtSMonthType2.Text = Convert.ToString(payRegistration.SMonthType2_1 + payRegistration.SMonthType2_2 + payRegistration.SMonthType2_3 + payRegistration.SMonthType2_4);//安全技术 当月费用小计

                        this.txtSMainApproveType2_1.Text = Convert.ToString(payRegistration.SMainApproveType2_1);
                        this.txtSMainApproveType2_2.Text = Convert.ToString(payRegistration.SMainApproveType2_2);
                        this.txtSMainApproveType2_3.Text = Convert.ToString(payRegistration.SMainApproveType2_3);
                        this.txtSMainApproveType2_4.Text = Convert.ToString(payRegistration.SMainApproveType2_4);
                        this.txtSMainApproveType2.Text = Convert.ToString(payRegistration.SMainApproveType2_1 + payRegistration.SMainApproveType2_2 + payRegistration.SMainApproveType2_3 + payRegistration.SMainApproveType2_4);//安全技术 总包审核值 费用小计

                        this.txtSMonthType3_1.Text = Convert.ToString(payRegistration.SMonthType3_1);
                        this.txtSMonthType3_2.Text = Convert.ToString(payRegistration.SMonthType3_2);
                        this.txtSMonthType3_3.Text = Convert.ToString(payRegistration.SMonthType3_3);
                        this.txtSMonthType3_4.Text = Convert.ToString(payRegistration.SMonthType3_4);
                        this.txtSMonthType3_5.Text = Convert.ToString(payRegistration.SMonthType3_5);
                        this.txtSMonthType3_6.Text = Convert.ToString(payRegistration.SMonthType3_6);
                        this.txtSMonthType3.Text = Convert.ToString(payRegistration.SMonthType3_1 + payRegistration.SMonthType3_2 + payRegistration.SMonthType3_3 + payRegistration.SMonthType3_4 + payRegistration.SMonthType3_5 + payRegistration.SMonthType3_6);//职业健康 当月费用小计

                        this.txtSMainApproveType3_1.Text = Convert.ToString(payRegistration.SMainApproveType3_1);
                        this.txtSMainApproveType3_2.Text = Convert.ToString(payRegistration.SMainApproveType3_2);
                        this.txtSMainApproveType3_3.Text = Convert.ToString(payRegistration.SMainApproveType3_3);
                        this.txtSMainApproveType3_4.Text = Convert.ToString(payRegistration.SMainApproveType3_4);
                        this.txtSMainApproveType3_5.Text = Convert.ToString(payRegistration.SMainApproveType3_5);
                        this.txtSMainApproveType3_6.Text = Convert.ToString(payRegistration.SMainApproveType3_6);
                        this.txtSMainApproveType3.Text = Convert.ToString(payRegistration.SMainApproveType3_1 + payRegistration.SMainApproveType3_2 + payRegistration.SMainApproveType3_3 + payRegistration.SMainApproveType3_4 + payRegistration.SMainApproveType3_5 + payRegistration.SMainApproveType3_6); //职业健康 总包审核值 费用小计

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

                        this.txtSMainApproveType4_1.Text = Convert.ToString(payRegistration.SMainApproveType4_1);
                        this.txtSMainApproveType4_2.Text = Convert.ToString(payRegistration.SMainApproveType4_2);
                        this.txtSMainApproveType4_3.Text = Convert.ToString(payRegistration.SMainApproveType4_3);
                        this.txtSMainApproveType4_4.Text = Convert.ToString(payRegistration.SMainApproveType4_4);
                        this.txtSMainApproveType4_5.Text = Convert.ToString(payRegistration.SMainApproveType4_5);
                        this.txtSMainApproveType4_6.Text = Convert.ToString(payRegistration.SMainApproveType4_6);
                        this.txtSMainApproveType4_7.Text = Convert.ToString(payRegistration.SMainApproveType4_7);
                        this.txtSMainApproveType4_8.Text = Convert.ToString(payRegistration.SMainApproveType4_8);
                        this.txtSMainApproveType4_9.Text = Convert.ToString(payRegistration.SMainApproveType4_9);
                        this.txtSMainApproveType4_10.Text = Convert.ToString(payRegistration.SMainApproveType4_10);
                        this.txtSMainApproveType4_11.Text = Convert.ToString(payRegistration.SMainApproveType4_11);
                        this.txtSMainApproveType4_12.Text = Convert.ToString(payRegistration.SMainApproveType4_12);
                        this.txtSMainApproveType4_13.Text = Convert.ToString(payRegistration.SMainApproveType4_13);
                        this.txtSMainApproveType4_14.Text = Convert.ToString(payRegistration.SMainApproveType4_14);
                        this.txtSMainApproveType4_15.Text = Convert.ToString(payRegistration.SMainApproveType4_15);
                        this.txtSMainApproveType4_16.Text = Convert.ToString(payRegistration.SMainApproveType4_16);
                        this.txtSMainApproveType4_17.Text = Convert.ToString(payRegistration.SMainApproveType4_17);
                        this.txtSMainApproveType4_18.Text = Convert.ToString(payRegistration.SMainApproveType4_18);
                        this.txtSMainApproveType4_19.Text = Convert.ToString(payRegistration.SMainApproveType4_19);
                        this.txtSMainApproveType4_20.Text = Convert.ToString(payRegistration.SMainApproveType4_20);
                        this.txtSMainApproveType4_21.Text = Convert.ToString(payRegistration.SMainApproveType4_21);
                        this.txtSMainApproveType4_22.Text = Convert.ToString(payRegistration.SMainApproveType4_22);
                        this.txtSMainApproveType4_23.Text = Convert.ToString(payRegistration.SMainApproveType4_23);
                        this.txtSMainApproveType4_24.Text = Convert.ToString(payRegistration.SMainApproveType4_24);
                        this.txtSMainApproveType4_25.Text = Convert.ToString(payRegistration.SMainApproveType4_25);
                        this.txtSMainApproveType4_26.Text = Convert.ToString(payRegistration.SMainApproveType4_26);
                        this.txtSMainApproveType4_27.Text = Convert.ToString(payRegistration.SMainApproveType4_27);
                        this.txtSMainApproveType4_28.Text = Convert.ToString(payRegistration.SMainApproveType4_28);
                        this.txtSMainApproveType4_29.Text = Convert.ToString(payRegistration.SMainApproveType4_29);
                        this.txtSMainApproveType4_30.Text = Convert.ToString(payRegistration.SMainApproveType4_30);
                        this.txtSMainApproveType4_31.Text = Convert.ToString(payRegistration.SMainApproveType4_31);
                        this.txtSMainApproveType4_32.Text = Convert.ToString(payRegistration.SMainApproveType4_32);
                        this.txtSMainApproveType4_33.Text = Convert.ToString(payRegistration.SMainApproveType4_33);
                        this.txtSMainApproveType4_34.Text = Convert.ToString(payRegistration.SMainApproveType4_34);
                        this.txtSMainApproveType4_35.Text = Convert.ToString(payRegistration.SMainApproveType4_35);
                        this.txtSMainApproveType4_36.Text = Convert.ToString(payRegistration.SMainApproveType4_36);
                        this.txtSMainApproveType4_37.Text = Convert.ToString(payRegistration.SMainApproveType4_37);
                        this.txtSMainApproveType4_38.Text = Convert.ToString(payRegistration.SMainApproveType4_38);
                        this.txtSMainApproveType4_39.Text = Convert.ToString(payRegistration.SMainApproveType4_39);
                        this.txtSMainApproveType4_40.Text = Convert.ToString(payRegistration.SMainApproveType4_40);
                        this.txtSMainApproveType4.Text = Convert.ToString(payRegistration.SMainApproveType4_1 + payRegistration.SMainApproveType4_2 + payRegistration.SMainApproveType4_3 + payRegistration.SMainApproveType4_4 + payRegistration.SMainApproveType4_5 + payRegistration.SMainApproveType4_6 + payRegistration.SMainApproveType4_7 + payRegistration.SMainApproveType4_8 + payRegistration.SMainApproveType4_9 + payRegistration.SMainApproveType4_10 + payRegistration.SMainApproveType4_11 + payRegistration.SMainApproveType4_12 + payRegistration.SMainApproveType4_13 + payRegistration.SMainApproveType4_14 + payRegistration.SMainApproveType4_15 + payRegistration.SMainApproveType4_16 + payRegistration.SMainApproveType4_17 + payRegistration.SMainApproveType4_18 + payRegistration.SMainApproveType4_19 + payRegistration.SMainApproveType4_20 + payRegistration.SMainApproveType4_21 + payRegistration.SMainApproveType4_22 + payRegistration.SMainApproveType4_23 + payRegistration.SMainApproveType4_24 + payRegistration.SMainApproveType4_25 + payRegistration.SMainApproveType4_26 + payRegistration.SMainApproveType4_27 + payRegistration.SMainApproveType4_28 + payRegistration.SMainApproveType4_29 + payRegistration.SMainApproveType4_30 + payRegistration.SMainApproveType4_31 + payRegistration.SMainApproveType4_32 + payRegistration.SMainApproveType4_33 + payRegistration.SMainApproveType4_34 + payRegistration.SMainApproveType4_35 + payRegistration.SMainApproveType4_36 + payRegistration.SMainApproveType4_37 + payRegistration.SMainApproveType4_38 + payRegistration.SMainApproveType4_39 + payRegistration.SMainApproveType4_40);//防护措施 总包审核值 费用小计

                        this.txtSMonthType5_1.Text = Convert.ToString(payRegistration.SMonthType5_1);
                        this.txtSMonthType5_2.Text = Convert.ToString(payRegistration.SMonthType5_2);
                        this.txtSMonthType5_3.Text = Convert.ToString(payRegistration.SMonthType5_3);
                        this.txtSMonthType5_4.Text = Convert.ToString(payRegistration.SMonthType5_4);
                        this.txtSMonthType5_5.Text = Convert.ToString(payRegistration.SMonthType5_5);
                        this.txtSMonthType5_6.Text = Convert.ToString(payRegistration.SMonthType5_6);
                        this.txtSMonthType5.Text = Convert.ToString(payRegistration.SMonthType5_1 + payRegistration.SMonthType5_2 + payRegistration.SMonthType5_3 + payRegistration.SMonthType5_4 + payRegistration.SMonthType5_5 + payRegistration.SMonthType5_6);//化工试车 当月费用小计

                        this.txtSMainApproveType5_1.Text = Convert.ToString(payRegistration.SMainApproveType5_1);
                        this.txtSMainApproveType5_2.Text = Convert.ToString(payRegistration.SMainApproveType5_2);
                        this.txtSMainApproveType5_3.Text = Convert.ToString(payRegistration.SMainApproveType5_3);
                        this.txtSMainApproveType5_4.Text = Convert.ToString(payRegistration.SMainApproveType5_4);
                        this.txtSMainApproveType5_5.Text = Convert.ToString(payRegistration.SMainApproveType5_5);
                        this.txtSMainApproveType5_6.Text = Convert.ToString(payRegistration.SMainApproveType5_6);
                        this.txtSMainApproveType5.Text = Convert.ToString(payRegistration.SMainApproveType5_1 + payRegistration.SMainApproveType5_2 + payRegistration.SMainApproveType5_3 + payRegistration.SMainApproveType5_4 + payRegistration.SMainApproveType5_5 + payRegistration.SMainApproveType5_6);//化工试车 总包审核值 费用小计

                        this.txtSMonthType6_1.Text = Convert.ToString(payRegistration.SMonthType6_1);
                        this.txtSMonthType6_2.Text = Convert.ToString(payRegistration.SMonthType6_2);
                        this.txtSMonthType6_3.Text = Convert.ToString(payRegistration.SMonthType6_3);
                        this.txtSMonthType6.Text = Convert.ToString(payRegistration.SMonthType6_1 + payRegistration.SMonthType6_2 + payRegistration.SMonthType6_3);//教育培训 当月费用小计

                        this.txtSMainApproveType6_1.Text = Convert.ToString(payRegistration.SMainApproveType6_1);
                        this.txtSMainApproveType6_2.Text = Convert.ToString(payRegistration.SMainApproveType6_2);
                        this.txtSMainApproveType6_2.Text = Convert.ToString(payRegistration.SMainApproveType6_3);
                        this.txtSMainApproveType6.Text = Convert.ToString(payRegistration.SMainApproveType6_1 + payRegistration.SMainApproveType6_2 + payRegistration.SMainApproveType6_3);//教育培训 总包审核值 费用小计

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

                        this.txtTMainApproveType1_1.Text = Convert.ToString(payRegistration.TMainApproveType1_1);
                        this.txtTMainApproveType1_2.Text = Convert.ToString(payRegistration.TMainApproveType1_2);
                        this.txtTMainApproveType1_3.Text = Convert.ToString(payRegistration.TMainApproveType1_3);
                        this.txtTMainApproveType1_4.Text = Convert.ToString(payRegistration.TMainApproveType1_4);
                        this.txtTMainApproveType1_5.Text = Convert.ToString(payRegistration.TMainApproveType1_5);
                        this.txtTMainApproveType1_6.Text = Convert.ToString(payRegistration.TMainApproveType1_6);
                        this.txtTMainApproveType1_7.Text = Convert.ToString(payRegistration.TMainApproveType1_7);
                        this.txtTMainApproveType1_8.Text = Convert.ToString(payRegistration.TMainApproveType1_8);
                        this.txtTMainApproveType1_9.Text = Convert.ToString(payRegistration.TMainApproveType1_9);
                        this.txtTMainApproveType1_10.Text = Convert.ToString(payRegistration.TMainApproveType1_10);
                        this.txtTMainApproveType1_11.Text = Convert.ToString(payRegistration.TMainApproveType1_11);
                        this.txtTMainApproveType1.Text = Convert.ToString(payRegistration.TMainApproveType1_1 + payRegistration.TMainApproveType1_2 + payRegistration.TMainApproveType1_3 + payRegistration.TMainApproveType1_4 + payRegistration.TMainApproveType1_5 + payRegistration.TMainApproveType1_6 + payRegistration.TMainApproveType1_7 + payRegistration.TMainApproveType1_8 + payRegistration.TMainApproveType1_9 + payRegistration.TMainApproveType1_10 + payRegistration.TMainApproveType1_11);//文明施工和环境保护 总包审核值 费用小计

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

                        this.txtTMainApproveType2_1.Text = Convert.ToString(payRegistration.TMainApproveType2_1);
                        this.txtTMainApproveType2_2.Text = Convert.ToString(payRegistration.TMainApproveType2_2);
                        this.txtTMainApproveType2_3.Text = Convert.ToString(payRegistration.TMainApproveType2_3);
                        this.txtTMainApproveType2_4.Text = Convert.ToString(payRegistration.TMainApproveType2_4);
                        this.txtTMainApproveType2_5.Text = Convert.ToString(payRegistration.TMainApproveType2_5);
                        this.txtTMainApproveType2_6.Text = Convert.ToString(payRegistration.TMainApproveType2_6);
                        this.txtTMainApproveType2_7.Text = Convert.ToString(payRegistration.TMainApproveType2_7);
                        this.txtTMainApproveType2_8.Text = Convert.ToString(payRegistration.TMainApproveType2_8);
                        this.txtTMainApproveType2_9.Text = Convert.ToString(payRegistration.TMainApproveType2_9);
                        this.txtTMainApproveType2.Text = Convert.ToString(payRegistration.TMainApproveType2_1 + payRegistration.TMainApproveType2_2 + payRegistration.TMainApproveType2_3 + payRegistration.TMainApproveType2_4 + payRegistration.TMainApproveType2_5 + payRegistration.TMainApproveType2_6 + payRegistration.TMainApproveType2_7 + payRegistration.TMainApproveType2_8 + payRegistration.TMainApproveType2_9);//临时设施 总包审核值 费用小计

                        this.txtMonthType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType2.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType3.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType4.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType5.Text) + Funs.GetNewDecimalOrZero(this.txtSMonthType6.Text) + Funs.GetNewDecimalOrZero(this.txtTMonthType1.Text) + Funs.GetNewDecimalOrZero(this.txtTMonthType2.Text));
                        this.txtMainApproveType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMainApproveType1.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType2.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType3.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType4.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType5.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType6.Text) + Funs.GetNewDecimalOrZero(this.txtTMainApproveType1.Text) + Funs.GetNewDecimalOrZero(this.txtTMainApproveType2.Text));
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
                var payRegistrations = BLL.PayRegistrationService.GetPayRegistrationByYear(this.CurrUser.LoginProjectId,DateTime.Now);
                if (payRegistrations != null)
                {                    
                    decimal? sYearType1_1 = 0, sYearType1_2 = 0, sYearType1_3 = 0, sYearType1_4 = 0, sYearType1_5 = 0, sYearType1_6 = 0, sYearType1_7 = 0, sYearType1_8 = 0, sYearType1_9 = 0, sYearType1_10 = 0, sYearType1_11 = 0, sYearType1_12 = 0, sYearType1_13 = 0, sYearType1_14 = 0, sYearType1_15 = 0, sYearType1_16 = 0;
                    decimal? sYearType2_1 = 0, sYearType2_2 = 0, sYearType2_3 = 0, sYearType2_4 = 0;
                    decimal? sYearType3_1 = 0, sYearType3_2 = 0, sYearType3_3 = 0, sYearType3_4 = 0, sYearType3_5 = 0, sYearType3_6 = 0;
                    decimal? sYearType4_1 = 0, sYearType4_2 = 0, sYearType4_3 = 0, sYearType4_4 = 0, sYearType4_5 = 0, sYearType4_6 = 0, sYearType4_7 = 0, sYearType4_8 = 0, sYearType4_9 = 0, sYearType4_10 = 0, sYearType4_11 = 0, sYearType4_12 = 0, sYearType4_13 = 0, sYearType4_14 = 0, sYearType4_15 = 0, sYearType4_16 = 0, sYearType4_17 = 0, sYearType4_18 = 0, sYearType4_19 = 0, sYearType4_20 = 0, sYearType4_21 = 0, sYearType4_22 = 0, sYearType4_23 = 0, sYearType4_24 = 0, sYearType4_25 = 0, sYearType4_26 = 0, sYearType4_27 = 0, sYearType4_28 = 0, sYearType4_29 = 0, sYearType4_30 = 0, sYearType4_31 = 0, sYearType4_32 = 0, sYearType4_33 = 0, sYearType4_34 = 0, sYearType4_35 = 0, sYearType4_36 = 0, sYearType4_37 = 0, sYearType4_38 = 0, sYearType4_39 = 0, sYearType4_40 = 0;
                    decimal? sYearType5_1 = 0, sYearType5_2 = 0, sYearType5_3 = 0, sYearType5_4 = 0, sYearType5_5 = 0, sYearType5_6 = 0;
                    decimal? sYearType6_1 = 0, sYearType6_2 = 0, sYearType6_3 = 0;
                    decimal? tYearType1_1 = 0, tYearType1_2 = 0, tYearType1_3 = 0, tYearType1_4 = 0, tYearType1_5 = 0, tYearType1_6 = 0, tYearType1_7 = 0, tYearType1_8 = 0, tYearType1_9 = 0, tYearType1_10 = 0, tYearType1_11 = 0;
                    decimal? tYearType2_1 = 0, tYearType2_2 = 0, tYearType2_3 = 0, tYearType2_4 = 0, tYearType2_5 = 0, tYearType2_6 = 0, tYearType2_7 = 0, tYearType2_8 = 0, tYearType2_9 = 0;
                    foreach (var item in payRegistrations)
                    {
                        sYearType1_1 += item.SMonthType1_1;
                        sYearType1_2 += item.SMonthType1_2;
                        sYearType1_3 += item.SMonthType1_3;
                        sYearType1_4 += item.SMonthType1_4;
                        sYearType1_5 += item.SMonthType1_5;
                        sYearType1_6 += item.SMonthType1_6;
                        sYearType1_7 += item.SMonthType1_7;
                        sYearType1_8 += item.SMonthType1_8;
                        sYearType1_9 += item.SMonthType1_9;
                        sYearType1_10 += item.SMonthType1_10;
                        sYearType1_11 += item.SMonthType1_11;
                        sYearType1_12 += item.SMonthType1_12;
                        sYearType1_13 += item.SMonthType1_13;
                        sYearType1_14 += item.SMonthType1_14;
                        sYearType1_15 += item.SMonthType1_15;
                        sYearType1_16 += item.SMonthType1_16;

                        sYearType2_1 += item.SMonthType2_1;
                        sYearType2_2 += item.SMonthType2_2;
                        sYearType2_3 += item.SMonthType2_3;
                        sYearType2_4 += item.SMonthType2_4;

                        sYearType3_1 += item.SMonthType3_1;
                        sYearType3_2 += item.SMonthType3_2;
                        sYearType3_3 += item.SMonthType3_3;
                        sYearType3_4 += item.SMonthType3_4;
                        sYearType3_5 += item.SMonthType3_5;
                        sYearType3_6 += item.SMonthType3_6;

                        sYearType4_1 += item.SMonthType4_1;
                        sYearType4_2 += item.SMonthType4_2;
                        sYearType4_3 += item.SMonthType4_3;
                        sYearType4_4 += item.SMonthType4_4;
                        sYearType4_5 += item.SMonthType4_5;
                        sYearType4_6 += item.SMonthType4_6;
                        sYearType4_7 += item.SMonthType4_7;
                        sYearType4_8 += item.SMonthType4_8;
                        sYearType4_9 += item.SMonthType4_9;
                        sYearType4_10 += item.SMonthType4_10;
                        sYearType4_11 += item.SMonthType4_11;
                        sYearType4_12 += item.SMonthType4_12;
                        sYearType4_13 += item.SMonthType4_13;
                        sYearType4_14 += item.SMonthType4_14;
                        sYearType4_15 += item.SMonthType4_15;
                        sYearType4_16 += item.SMonthType4_16;
                        sYearType4_17 += item.SMonthType4_17;
                        sYearType4_18 += item.SMonthType4_18;
                        sYearType4_19 += item.SMonthType4_19;
                        sYearType4_20 += item.SMonthType4_20;
                        sYearType4_21 += item.SMonthType4_21;
                        sYearType4_22 += item.SMonthType4_22;
                        sYearType4_23 += item.SMonthType4_23;
                        sYearType4_24 += item.SMonthType4_24;
                        sYearType4_25 += item.SMonthType4_25;
                        sYearType4_26 += item.SMonthType4_26;
                        sYearType4_27 += item.SMonthType4_27;
                        sYearType4_28 += item.SMonthType4_28;
                        sYearType4_29 += item.SMonthType4_29;
                        sYearType4_30 += item.SMonthType4_30;
                        sYearType4_31 += item.SMonthType4_31;
                        sYearType4_32 += item.SMonthType4_32;
                        sYearType4_33 += item.SMonthType4_33;
                        sYearType4_34 += item.SMonthType4_34;
                        sYearType4_35 += item.SMonthType4_35;
                        sYearType4_36 += item.SMonthType4_36;
                        sYearType4_37 += item.SMonthType4_37;
                        sYearType4_38 += item.SMonthType4_38;
                        sYearType4_39 += item.SMonthType4_39;
                        sYearType4_40 += item.SMonthType4_40;

                        sYearType5_1 += item.SMonthType5_1;
                        sYearType5_2 += item.SMonthType5_2;
                        sYearType5_3 += item.SMonthType5_3;
                        sYearType5_4 += item.SMonthType5_4;
                        sYearType5_5 += item.SMonthType5_5;
                        sYearType5_6 += item.SMonthType5_6;

                        sYearType6_1 += item.SMonthType6_1;
                        sYearType6_2 += item.SMonthType6_2;
                        sYearType6_3 += item.SMonthType6_3;

                        tYearType1_1 += item.TMonthType1_1;
                        tYearType1_2 += item.TMonthType1_2;
                        tYearType1_3 += item.TMonthType1_3;
                        tYearType1_4 += item.TMonthType1_4;
                        tYearType1_5 += item.TMonthType1_5;
                        tYearType1_6 += item.TMonthType1_6;
                        tYearType1_7 += item.TMonthType1_7;
                        tYearType1_8 += item.TMonthType1_8;
                        tYearType1_9 += item.TMonthType1_9;
                        tYearType1_10 += item.TMonthType1_10;
                        tYearType1_11 += item.TMonthType1_11;

                        tYearType2_1 += item.TMonthType2_1;
                        tYearType2_2 += item.TMonthType2_2;
                        tYearType2_3 += item.TMonthType2_3;
                        tYearType2_4 += item.TMonthType2_4;
                        tYearType2_5 += item.TMonthType2_5;
                        tYearType2_6 += item.TMonthType2_6;
                        tYearType2_7 += item.TMonthType2_7;
                        tYearType2_8 += item.TMonthType2_8;
                        tYearType2_9 += item.TMonthType2_9;
                    }
                    this.txtSYearType1_1.Text = Convert.ToString(sYearType1_1);
                    this.txtSYearType1_2.Text = Convert.ToString(sYearType1_2);
                    this.txtSYearType1_3.Text = Convert.ToString(sYearType1_3);
                    this.txtSYearType1_4.Text = Convert.ToString(sYearType1_4);
                    this.txtSYearType1_5.Text = Convert.ToString(sYearType1_5);
                    this.txtSYearType1_6.Text = Convert.ToString(sYearType1_6);
                    this.txtSYearType1_7.Text = Convert.ToString(sYearType1_7);
                    this.txtSYearType1_8.Text = Convert.ToString(sYearType1_8);
                    this.txtSYearType1_9.Text = Convert.ToString(sYearType1_9);
                    this.txtSYearType1_10.Text = Convert.ToString(sYearType1_10);
                    this.txtSYearType1_11.Text = Convert.ToString(sYearType1_11);
                    this.txtSYearType1_12.Text = Convert.ToString(sYearType1_12);
                    this.txtSYearType1_13.Text = Convert.ToString(sYearType1_13);
                    this.txtSYearType1_14.Text = Convert.ToString(sYearType1_14);
                    this.txtSYearType1_15.Text = Convert.ToString(sYearType1_15);
                    this.txtSYearType1_16.Text = Convert.ToString(sYearType1_16);
                    this.txtSYearType1.Text = Convert.ToString(sYearType1_1 + sYearType1_2 + sYearType1_3 + sYearType1_4 + sYearType1_5 + sYearType1_6 + sYearType1_7 + sYearType1_8 + sYearType1_9 + sYearType1_10 + sYearType1_11 + sYearType1_12 + sYearType1_13 + sYearType1_14 + sYearType1_15 + sYearType1_16);

                    this.txtSYearType2_1.Text = Convert.ToString(sYearType2_1);
                    this.txtSYearType2_2.Text = Convert.ToString(sYearType2_2);
                    this.txtSYearType2_3.Text = Convert.ToString(sYearType2_3);
                    this.txtSYearType2_4.Text = Convert.ToString(sYearType2_4);
                    this.txtSYearType2.Text = Convert.ToString(sYearType2_1 + sYearType2_2 + sYearType2_3 + sYearType2_4);

                    this.txtSYearType3_1.Text = Convert.ToString(sYearType3_1);
                    this.txtSYearType3_2.Text = Convert.ToString(sYearType3_2);
                    this.txtSYearType3_3.Text = Convert.ToString(sYearType3_3);
                    this.txtSYearType3_4.Text = Convert.ToString(sYearType3_4);
                    this.txtSYearType3_5.Text = Convert.ToString(sYearType3_5);
                    this.txtSYearType3_6.Text = Convert.ToString(sYearType3_6);
                    this.txtSYearType3.Text = Convert.ToString(sYearType3_1 + sYearType3_2 + sYearType3_3 + sYearType3_4 + sYearType3_5 + sYearType3_6);

                    this.txtSYearType4_1.Text = Convert.ToString(sYearType4_1);
                    this.txtSYearType4_2.Text = Convert.ToString(sYearType4_2);
                    this.txtSYearType4_3.Text = Convert.ToString(sYearType4_3);
                    this.txtSYearType4_4.Text = Convert.ToString(sYearType4_4);
                    this.txtSYearType4_5.Text = Convert.ToString(sYearType4_5);
                    this.txtSYearType4_6.Text = Convert.ToString(sYearType4_6);
                    this.txtSYearType4_7.Text = Convert.ToString(sYearType4_7);
                    this.txtSYearType4_8.Text = Convert.ToString(sYearType4_8);
                    this.txtSYearType4_9.Text = Convert.ToString(sYearType4_9);
                    this.txtSYearType4_10.Text = Convert.ToString(sYearType4_10);
                    this.txtSYearType4_11.Text = Convert.ToString(sYearType4_11);
                    this.txtSYearType4_12.Text = Convert.ToString(sYearType4_12);
                    this.txtSYearType4_13.Text = Convert.ToString(sYearType4_13);
                    this.txtSYearType4_14.Text = Convert.ToString(sYearType4_14);
                    this.txtSYearType4_15.Text = Convert.ToString(sYearType4_15);
                    this.txtSYearType4_16.Text = Convert.ToString(sYearType4_16);
                    this.txtSYearType4_17.Text = Convert.ToString(sYearType4_17);
                    this.txtSYearType4_18.Text = Convert.ToString(sYearType4_18);
                    this.txtSYearType4_19.Text = Convert.ToString(sYearType4_19);
                    this.txtSYearType4_20.Text = Convert.ToString(sYearType4_20);
                    this.txtSYearType4_21.Text = Convert.ToString(sYearType4_21);
                    this.txtSYearType4_22.Text = Convert.ToString(sYearType4_22);
                    this.txtSYearType4_23.Text = Convert.ToString(sYearType4_23);
                    this.txtSYearType4_24.Text = Convert.ToString(sYearType4_24);
                    this.txtSYearType4_25.Text = Convert.ToString(sYearType4_25);
                    this.txtSYearType4_26.Text = Convert.ToString(sYearType4_26);
                    this.txtSYearType4_27.Text = Convert.ToString(sYearType4_27);
                    this.txtSYearType4_28.Text = Convert.ToString(sYearType4_28);
                    this.txtSYearType4_29.Text = Convert.ToString(sYearType4_29);
                    this.txtSYearType4_30.Text = Convert.ToString(sYearType4_30);
                    this.txtSYearType4_31.Text = Convert.ToString(sYearType4_31);
                    this.txtSYearType4_32.Text = Convert.ToString(sYearType4_32);
                    this.txtSYearType4_33.Text = Convert.ToString(sYearType4_33);
                    this.txtSYearType4_34.Text = Convert.ToString(sYearType4_34);
                    this.txtSYearType4_35.Text = Convert.ToString(sYearType4_35);
                    this.txtSYearType4_36.Text = Convert.ToString(sYearType4_36);
                    this.txtSYearType4_37.Text = Convert.ToString(sYearType4_37);
                    this.txtSYearType4_38.Text = Convert.ToString(sYearType4_38);
                    this.txtSYearType4_39.Text = Convert.ToString(sYearType4_39);
                    this.txtSYearType4_40.Text = Convert.ToString(sYearType4_40);
                    this.txtSYearType4.Text = Convert.ToString(sYearType4_1 + sYearType4_2 + sYearType4_3 + sYearType4_4 + sYearType4_5 + sYearType4_6 + sYearType4_7 + sYearType4_8 + sYearType4_9 + sYearType4_10 + sYearType4_11 + sYearType4_12 + sYearType4_13 + sYearType4_14 + sYearType4_15 + sYearType4_16 + sYearType4_17 + sYearType4_18 + sYearType4_19 + sYearType4_20 + sYearType4_21 + sYearType4_22 + sYearType4_23 + sYearType4_24 + sYearType4_25 + sYearType4_26 + sYearType4_27 + sYearType4_28 + sYearType4_29 + sYearType4_30 + sYearType4_31 + sYearType4_32 + sYearType4_33 + sYearType4_34 + sYearType4_35 + sYearType4_36 + sYearType4_37 + sYearType4_38 + sYearType4_39 + sYearType4_40);

                    this.txtSYearType5_1.Text = Convert.ToString(sYearType5_1);
                    this.txtSYearType5_2.Text = Convert.ToString(sYearType5_2);
                    this.txtSYearType5_3.Text = Convert.ToString(sYearType5_3);
                    this.txtSYearType5_4.Text = Convert.ToString(sYearType5_4);
                    this.txtSYearType5_5.Text = Convert.ToString(sYearType5_5);
                    this.txtSYearType5_6.Text = Convert.ToString(sYearType5_6);
                    this.txtSYearType5.Text = Convert.ToString(sYearType5_1 + sYearType5_2 + sYearType5_3 + sYearType5_4 + sYearType5_5 + sYearType5_6);

                    this.txtSYearType6_1.Text = Convert.ToString(sYearType6_1);
                    this.txtSYearType6_2.Text = Convert.ToString(sYearType6_2);
                    this.txtSYearType6_3.Text = Convert.ToString(sYearType6_3);
                    this.txtSMonthType6.Text = Convert.ToString(sYearType6_1 + sYearType6_2 + sYearType6_3);

                    this.txtTYearType1_1.Text = Convert.ToString(tYearType1_1);
                    this.txtTYearType1_2.Text = Convert.ToString(tYearType1_2);
                    this.txtTYearType1_3.Text = Convert.ToString(tYearType1_3);
                    this.txtTYearType1_4.Text = Convert.ToString(tYearType1_4);
                    this.txtTYearType1_5.Text = Convert.ToString(tYearType1_5);
                    this.txtTYearType1_6.Text = Convert.ToString(tYearType1_6);
                    this.txtTYearType1_7.Text = Convert.ToString(tYearType1_7);
                    this.txtTYearType1_8.Text = Convert.ToString(tYearType1_8);
                    this.txtTYearType1_9.Text = Convert.ToString(tYearType1_9);
                    this.txtTYearType1_10.Text = Convert.ToString(tYearType1_10);
                    this.txtTYearType1_11.Text = Convert.ToString(tYearType1_11);
                    this.txtTYearType1.Text = Convert.ToString(tYearType1_1 + tYearType1_2 + tYearType1_3 + tYearType1_4 + tYearType1_5 + tYearType1_6 + tYearType1_7 + tYearType1_8 + tYearType1_9 + tYearType1_10 + tYearType1_11);

                    this.txtTYearType2_1.Text = Convert.ToString(tYearType2_1);
                    this.txtTYearType2_2.Text = Convert.ToString(tYearType2_2);
                    this.txtTYearType2_3.Text = Convert.ToString(tYearType2_3);
                    this.txtTYearType2_4.Text = Convert.ToString(tYearType2_4);
                    this.txtTYearType2_5.Text = Convert.ToString(tYearType2_5);
                    this.txtTYearType2_6.Text = Convert.ToString(tYearType2_6);
                    this.txtTYearType2_7.Text = Convert.ToString(tYearType2_7);
                    this.txtTYearType2_8.Text = Convert.ToString(tYearType2_8);
                    this.txtTYearType2_9.Text = Convert.ToString(tYearType2_9);
                    this.txtTYearType2.Text = Convert.ToString(tYearType2_1 + tYearType2_2 + tYearType2_3 + tYearType2_4 + tYearType2_5 + tYearType2_6 + tYearType2_7 + tYearType2_8 + tYearType2_9);

                    this.txtYearType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSYearType1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSYearType2.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSYearType3.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSYearType4.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSYearType5.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtSYearType6.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtTYearType1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.txtTYearType2.Text.Trim()));
                }
                #endregion
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

        #region 总包审核统计
        /// <summary>
        /// 总包审核值统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MainApproveTypeText__TextChanged(object sender, EventArgs e)
        {
            //基础管理 总包审核值
            decimal sMainApproveType1_1 = 0, sMainApproveType1_2 = 0, sMainApproveType1_3 = 0, sMainApproveType1_4 = 0, sMainApproveType1_5 = 0, sMainApproveType1_6 = 0, sMainApproveType1_7 = 0, sMainApproveType1_8 = 0, sMainApproveType1_9 = 0, sMainApproveType1_10 = 0, sMainApproveType1_11 = 0, sMainApproveType1_12 = 0, sMainApproveType1_13 = 0, sMainApproveType1_14 = 0, sMainApproveType1_15 = 0, sMainApproveType1_16 = 0;
            sMainApproveType1_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_1.Text.Trim());
            sMainApproveType1_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_2.Text.Trim());
            sMainApproveType1_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_3.Text.Trim());
            sMainApproveType1_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_4.Text.Trim());
            sMainApproveType1_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_5.Text.Trim());
            sMainApproveType1_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_6.Text.Trim());
            sMainApproveType1_7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_7.Text.Trim());
            sMainApproveType1_8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_8.Text.Trim());
            sMainApproveType1_9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_9.Text.Trim());
            sMainApproveType1_10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_10.Text.Trim());
            sMainApproveType1_11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_11.Text.Trim());
            sMainApproveType1_12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_12.Text.Trim());
            sMainApproveType1_13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_13.Text.Trim());
            sMainApproveType1_14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_14.Text.Trim());
            sMainApproveType1_15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_15.Text.Trim());
            sMainApproveType1_16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_16.Text.Trim());
            this.txtSMainApproveType1.Text = Convert.ToString(sMainApproveType1_1 + sMainApproveType1_2 + sMainApproveType1_3 + sMainApproveType1_4 + sMainApproveType1_5 + sMainApproveType1_6 + sMainApproveType1_7 + sMainApproveType1_8 + sMainApproveType1_9 + sMainApproveType1_10 + sMainApproveType1_11 + sMainApproveType1_12 + sMainApproveType1_13 + sMainApproveType1_14 + sMainApproveType1_15 + sMainApproveType1_16);

            //安全技术 总包审核值
            decimal sMainApproveType2_1 = 0, sMainApproveType2_2 = 0, sMainApproveType2_3 = 0, sMainApproveType2_4 = 0;
            sMainApproveType2_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_1.Text.Trim());
            sMainApproveType2_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_2.Text.Trim());
            sMainApproveType2_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_3.Text.Trim());
            sMainApproveType2_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_4.Text.Trim());
            this.txtSMainApproveType2.Text = Convert.ToString(sMainApproveType2_1 + sMainApproveType2_2 + sMainApproveType2_3 + sMainApproveType2_4);

            //职业健康 总包审核值
            decimal sMainApproveType3_1 = 0, sMainApproveType3_2 = 0, sMainApproveType3_3 = 0, sMainApproveType3_4 = 0, sMainApproveType3_5 = 0, sMainApproveType3_6 = 0;
            sMainApproveType3_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_1.Text.Trim());
            sMainApproveType3_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_2.Text.Trim());
            sMainApproveType3_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_3.Text.Trim());
            sMainApproveType3_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_4.Text.Trim());
            sMainApproveType3_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_5.Text.Trim());
            sMainApproveType3_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_6.Text.Trim());
            this.txtSMainApproveType3.Text = Convert.ToString(sMainApproveType3_1 + sMainApproveType3_2 + sMainApproveType3_3 + sMainApproveType3_4 + sMainApproveType3_5 + sMainApproveType3_6);

            //防护措施 总包审核值
            decimal sMainApproveType4_1 = 0, sMainApproveType4_2 = 0, sMainApproveType4_3 = 0, sMainApproveType4_4 = 0, sMainApproveType4_5 = 0, sMainApproveType4_6 = 0, sMainApproveType4_7 = 0, sMainApproveType4_8 = 0, sMainApproveType4_9 = 0, sMainApproveType4_10 = 0, sMainApproveType4_11 = 0, sMainApproveType4_12 = 0, sMainApproveType4_13 = 0, sMainApproveType4_14 = 0, sMainApproveType4_15 = 0, sMainApproveType4_16 = 0, sMainApproveType4_17 = 0, sMainApproveType4_18 = 0, sMainApproveType4_19 = 0, sMainApproveType4_20 = 0, sMainApproveType4_21 = 0, sMainApproveType4_22 = 0, sMainApproveType4_23 = 0, sMainApproveType4_24 = 0, sMainApproveType4_25 = 0, sMainApproveType4_26 = 0, sMainApproveType4_27 = 0, sMainApproveType4_28 = 0, sMainApproveType4_29 = 0, sMainApproveType4_30 = 0, sMainApproveType4_31 = 0, sMainApproveType4_32 = 0, sMainApproveType4_33 = 0, sMainApproveType4_34 = 0, sMainApproveType4_35 = 0, sMainApproveType4_36 = 0, sMainApproveType4_37 = 0, sMainApproveType4_38 = 0, sMainApproveType4_39 = 0, sMainApproveType4_40 = 0;
            sMainApproveType4_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_1.Text.Trim());
            sMainApproveType4_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_2.Text.Trim());
            sMainApproveType4_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_3.Text.Trim());
            sMainApproveType4_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_4.Text.Trim());
            sMainApproveType4_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_5.Text.Trim());
            sMainApproveType4_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_6.Text.Trim());
            sMainApproveType4_7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_7.Text.Trim());
            sMainApproveType4_8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_8.Text.Trim());
            sMainApproveType4_9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_9.Text.Trim());
            sMainApproveType4_10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_10.Text.Trim());
            sMainApproveType4_11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_11.Text.Trim());
            sMainApproveType4_12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_12.Text.Trim());
            sMainApproveType4_13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_13.Text.Trim());
            sMainApproveType4_14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_14.Text.Trim());
            sMainApproveType4_15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_15.Text.Trim());
            sMainApproveType4_16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_16.Text.Trim());
            sMainApproveType4_17 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_17.Text.Trim());
            sMainApproveType4_18 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_18.Text.Trim());
            sMainApproveType4_19 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_19.Text.Trim());
            sMainApproveType4_20 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_20.Text.Trim());
            sMainApproveType4_21 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_21.Text.Trim());
            sMainApproveType4_22 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_22.Text.Trim());
            sMainApproveType4_23 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_23.Text.Trim());
            sMainApproveType4_24 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_24.Text.Trim());
            sMainApproveType4_25 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_25.Text.Trim());
            sMainApproveType4_26 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_26.Text.Trim());
            sMainApproveType4_27 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_27.Text.Trim());
            sMainApproveType4_28 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_28.Text.Trim());
            sMainApproveType4_29 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_29.Text.Trim());
            sMainApproveType4_30 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_30.Text.Trim());
            sMainApproveType4_31 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_31.Text.Trim());
            sMainApproveType4_32 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_32.Text.Trim());
            sMainApproveType4_33 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_33.Text.Trim());
            sMainApproveType4_34 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_34.Text.Trim());
            sMainApproveType4_35 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_35.Text.Trim());
            sMainApproveType4_36 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_36.Text.Trim());
            sMainApproveType4_37 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_37.Text.Trim());
            sMainApproveType4_38 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_38.Text.Trim());
            sMainApproveType4_39 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_39.Text.Trim());
            sMainApproveType4_40 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_40.Text.Trim());
            this.txtSMainApproveType4.Text = Convert.ToString(sMainApproveType4_1 + sMainApproveType4_2 + sMainApproveType4_3 + sMainApproveType4_4 + sMainApproveType4_5 + sMainApproveType4_6 + sMainApproveType4_7 + sMainApproveType4_8 + sMainApproveType4_9 + sMainApproveType4_10 + sMainApproveType4_11 + sMainApproveType4_12 + sMainApproveType4_13 + sMainApproveType4_14 + sMainApproveType4_15 + sMainApproveType4_16 + sMainApproveType4_17 + sMainApproveType4_18 + sMainApproveType4_19 + sMainApproveType4_20 + sMainApproveType4_21 + sMainApproveType4_22 + sMainApproveType4_23 + sMainApproveType4_24 + sMainApproveType4_25 + sMainApproveType4_26 + sMainApproveType4_27 + sMainApproveType4_28 + sMainApproveType4_29 + sMainApproveType4_30 + sMainApproveType4_31 + sMainApproveType4_32 + sMainApproveType4_33 + sMainApproveType4_34 + sMainApproveType4_35 + sMainApproveType4_36 + sMainApproveType4_37 + sMainApproveType4_38 + sMainApproveType4_39 + sMainApproveType4_40);

            //化工试车 总包审核值
            decimal sMainApproveType5_1 = 0, sMainApproveType5_2 = 0, sMainApproveType5_3 = 0, sMainApproveType5_4 = 0, sMainApproveType5_5 = 0, sMainApproveType5_6 = 0;
            sMainApproveType5_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_1.Text.Trim());
            sMainApproveType5_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_2.Text.Trim());
            sMainApproveType5_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_3.Text.Trim());
            sMainApproveType5_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_4.Text.Trim());
            sMainApproveType5_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_5.Text.Trim());
            sMainApproveType5_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_6.Text.Trim());
            this.txtSMainApproveType5.Text = Convert.ToString(sMainApproveType5_1 + sMainApproveType5_2 + sMainApproveType5_3 + sMainApproveType5_4 + sMainApproveType5_5 + sMainApproveType5_6);

            //教育培训 总包审核值
            decimal sMainApproveType6_1 = 0, sMainApproveType6_2 = 0, sMainApproveType6_3 = 0;
            sMainApproveType6_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_1.Text.Trim());
            sMainApproveType6_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_2.Text.Trim());
            sMainApproveType6_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_3.Text.Trim());
            this.txtSMainApproveType6.Text = Convert.ToString(sMainApproveType6_1 + sMainApproveType6_2 + sMainApproveType6_3);

            //文明施工和环境保护 总包审核值
            decimal tMainApproveType1_1 = 0, tMainApproveType1_2 = 0, tMainApproveType1_3 = 0, tMainApproveType1_4 = 0, tMainApproveType1_5 = 0, tMainApproveType1_6 = 0, tMainApproveType1_7 = 0, tMainApproveType1_8 = 0, tMainApproveType1_9 = 0, tMainApproveType1_10 = 0, tMainApproveType1_11 = 0;
            tMainApproveType1_1 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_1.Text.Trim());
            tMainApproveType1_2 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_2.Text.Trim());
            tMainApproveType1_3 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_3.Text.Trim());
            tMainApproveType1_4 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_4.Text.Trim());
            tMainApproveType1_5 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_5.Text.Trim());
            tMainApproveType1_6 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_6.Text.Trim());
            tMainApproveType1_7 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_7.Text.Trim());
            tMainApproveType1_8 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_8.Text.Trim());
            tMainApproveType1_9 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_9.Text.Trim());
            tMainApproveType1_10 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_10.Text.Trim());
            tMainApproveType1_11 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_11.Text.Trim());
            this.txtTMainApproveType1.Text = Convert.ToString(tMainApproveType1_1 + tMainApproveType1_2 + tMainApproveType1_3 + tMainApproveType1_4 + tMainApproveType1_5 + tMainApproveType1_6 + tMainApproveType1_7 + tMainApproveType1_8 + tMainApproveType1_9 + tMainApproveType1_10 + tMainApproveType1_11);

            //临时设施
            decimal tMainApproveType2_1 = 0, tMainApproveType2_2 = 0, tMainApproveType2_3 = 0, tMainApproveType2_4 = 0, tMainApproveType2_5 = 0, tMainApproveType2_6 = 0, tMainApproveType2_7 = 0, tMainApproveType2_8 = 0, tMainApproveType2_9 = 0;
            tMainApproveType2_1 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_1.Text.Trim());
            tMainApproveType2_2 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_2.Text.Trim());
            tMainApproveType2_3 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_3.Text.Trim());
            tMainApproveType2_4 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_4.Text.Trim());
            tMainApproveType2_5 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_5.Text.Trim());
            tMainApproveType2_6 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_6.Text.Trim());
            tMainApproveType2_7 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_7.Text.Trim());
            tMainApproveType2_8 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_8.Text.Trim());
            tMainApproveType2_9 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_9.Text.Trim());
            this.txtTMainApproveType2.Text = Convert.ToString(tMainApproveType2_1 + tMainApproveType2_2 + tMainApproveType2_3 + tMainApproveType2_4 + tMainApproveType2_5 + tMainApproveType2_6 + tMainApproveType2_7 + tMainApproveType2_8 + tMainApproveType2_9);

            //累计总和
            this.txtMainApproveType.Text = Convert.ToString(Funs.GetNewDecimalOrZero(this.txtSMainApproveType1.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType2.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType3.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType4.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType5.Text) + Funs.GetNewDecimalOrZero(this.txtSMainApproveType6.Text) + Funs.GetNewDecimalOrZero(this.txtTMainApproveType1.Text) + Funs.GetNewDecimalOrZero(this.txtTMainApproveType2.Text));
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
                SMainApproveType1_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_1.Text.Trim()),
                SMainApproveType1_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_2.Text.Trim()),
                SMainApproveType1_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_3.Text.Trim()),
                SMainApproveType1_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_4.Text.Trim()),
                SMainApproveType1_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_5.Text.Trim()),
                SMainApproveType1_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_6.Text.Trim()),
                SMainApproveType1_7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_7.Text.Trim()),
                SMainApproveType1_8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_8.Text.Trim()),
                SMainApproveType1_9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_9.Text.Trim()),
                SMainApproveType1_10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_10.Text.Trim()),
                SMainApproveType1_11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_11.Text.Trim()),
                SMainApproveType1_12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_12.Text.Trim()),
                SMainApproveType1_13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_13.Text.Trim()),
                SMainApproveType1_14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_14.Text.Trim()),
                SMainApproveType1_15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_15.Text.Trim()),
                SMainApproveType1_16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType1_16.Text.Trim()),
                SMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_1.Text.Trim()),
                SMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_2.Text.Trim()),
                SMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_3.Text.Trim()),
                SMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType2_4.Text.Trim()),
                SMainApproveType2_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_1.Text.Trim()),
                SMainApproveType2_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_2.Text.Trim()),
                SMainApproveType2_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_3.Text.Trim()),
                SMainApproveType2_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType2_4.Text.Trim()),
                SMonthType3_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_1.Text.Trim()),
                SMonthType3_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_2.Text.Trim()),
                SMonthType3_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_3.Text.Trim()),
                SMonthType3_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_4.Text.Trim()),
                SMonthType3_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_5.Text.Trim()),
                SMonthType3_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType3_6.Text.Trim()),
                SMainApproveType3_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_1.Text.Trim()),
                SMainApproveType3_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_2.Text.Trim()),
                SMainApproveType3_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_3.Text.Trim()),
                SMainApproveType3_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_4.Text.Trim()),
                SMainApproveType3_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_5.Text.Trim()),
                SMainApproveType3_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType3_6.Text.Trim()),
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
                SMainApproveType4_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_1.Text.Trim()),
                SMainApproveType4_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_2.Text.Trim()),
                SMainApproveType4_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_3.Text.Trim()),
                SMainApproveType4_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_4.Text.Trim()),
                SMainApproveType4_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_5.Text.Trim()),
                SMainApproveType4_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_6.Text.Trim()),
                SMainApproveType4_7 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_7.Text.Trim()),
                SMainApproveType4_8 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_8.Text.Trim()),
                SMainApproveType4_9 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_9.Text.Trim()),
                SMainApproveType4_10 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_10.Text.Trim()),
                SMainApproveType4_11 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_11.Text.Trim()),
                SMainApproveType4_12 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_12.Text.Trim()),
                SMainApproveType4_13 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_13.Text.Trim()),
                SMainApproveType4_14 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_14.Text.Trim()),
                SMainApproveType4_15 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_15.Text.Trim()),
                SMainApproveType4_16 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_16.Text.Trim()),
                SMainApproveType4_17 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_17.Text.Trim()),
                SMainApproveType4_18 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_18.Text.Trim()),
                SMainApproveType4_19 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_19.Text.Trim()),
                SMainApproveType4_20 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_20.Text.Trim()),
                SMainApproveType4_21 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_21.Text.Trim()),
                SMainApproveType4_22 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_22.Text.Trim()),
                SMainApproveType4_23 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_23.Text.Trim()),
                SMainApproveType4_24 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_24.Text.Trim()),
                SMainApproveType4_25 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_25.Text.Trim()),
                SMainApproveType4_26 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_26.Text.Trim()),
                SMainApproveType4_27 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_27.Text.Trim()),
                SMainApproveType4_28 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_28.Text.Trim()),
                SMainApproveType4_29 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_29.Text.Trim()),
                SMainApproveType4_30 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_30.Text.Trim()),
                SMainApproveType4_31 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_31.Text.Trim()),
                SMainApproveType4_32 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_32.Text.Trim()),
                SMainApproveType4_33 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_33.Text.Trim()),
                SMainApproveType4_34 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_34.Text.Trim()),
                SMainApproveType4_35 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_35.Text.Trim()),
                SMainApproveType4_36 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_36.Text.Trim()),
                SMainApproveType4_37 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_37.Text.Trim()),
                SMainApproveType4_38 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_38.Text.Trim()),
                SMainApproveType4_39 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_39.Text.Trim()),
                SMainApproveType4_40 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType4_40.Text.Trim()),
                SMonthType5_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_1.Text.Trim()),
                SMonthType5_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_2.Text.Trim()),
                SMonthType5_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_3.Text.Trim()),
                SMonthType5_4 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_4.Text.Trim()),
                SMonthType5_5 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_5.Text.Trim()),
                SMonthType5_6 = Funs.GetNewDecimalOrZero(this.txtSMonthType5_6.Text.Trim()),
                SMainApproveType5_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_1.Text.Trim()),
                SMainApproveType5_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_2.Text.Trim()),
                SMainApproveType5_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_3.Text.Trim()),
                SMainApproveType5_4 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_4.Text.Trim()),
                SMainApproveType5_5 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_5.Text.Trim()),
                SMainApproveType5_6 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType5_6.Text.Trim()),
                SMonthType6_1 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_1.Text.Trim()),
                SMonthType6_2 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_2.Text.Trim()),
                SMonthType6_3 = Funs.GetNewDecimalOrZero(this.txtSMonthType6_3.Text.Trim()),
                SMainApproveType6_1 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_1.Text.Trim()),
                SMainApproveType6_2 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_2.Text.Trim()),
                SMainApproveType6_3 = Funs.GetNewDecimalOrZero(this.txtSMainApproveType6_3.Text.Trim()),
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
                TMainApproveType1_1 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_1.Text.Trim()),
                TMainApproveType1_2 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_2.Text.Trim()),
                TMainApproveType1_3 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_3.Text.Trim()),
                TMainApproveType1_4 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_4.Text.Trim()),
                TMainApproveType1_5 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_5.Text.Trim()),
                TMainApproveType1_6 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_6.Text.Trim()),
                TMainApproveType1_7 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_7.Text.Trim()),
                TMainApproveType1_8 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_8.Text.Trim()),
                TMainApproveType1_9 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_9.Text.Trim()),
                TMainApproveType1_10 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_10.Text.Trim()),
                TMainApproveType1_11 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType1_11.Text.Trim()),
                TMonthType2_1 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_1.Text.Trim()),
                TMonthType2_2 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_2.Text.Trim()),
                TMonthType2_3 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_3.Text.Trim()),
                TMonthType2_4 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_4.Text.Trim()),
                TMonthType2_5 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_5.Text.Trim()),
                TMonthType2_6 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_6.Text.Trim()),
                TMonthType2_7 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_7.Text.Trim()),
                TMonthType2_8 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_8.Text.Trim()),
                TMonthType2_9 = Funs.GetNewDecimalOrZero(this.txtTMonthType2_9.Text.Trim()),
                TMainApproveType2_1 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_1.Text.Trim()),
                TMainApproveType2_2 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_2.Text.Trim()),
                TMainApproveType2_3 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_3.Text.Trim()),
                TMainApproveType2_4 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_4.Text.Trim()),
                TMainApproveType2_5 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_5.Text.Trim()),
                TMainApproveType2_6 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_6.Text.Trim()),
                TMainApproveType2_7 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_7.Text.Trim()),
                TMainApproveType2_8 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_8.Text.Trim()),
                TMainApproveType2_9 = Funs.GetNewDecimalOrZero(this.txtTMainApproveType2_9.Text.Trim()),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now
            };
            if (!string.IsNullOrEmpty(this.PayRegistrationId))
            {
                payRegistration.PayRegistrationId = this.PayRegistrationId;
                BLL.PayRegistrationService.UpdatePayRegistration(payRegistration);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改安全费用投入登记");
            }
            else
            {
                this.PayRegistrationId = SQLHelper.GetNewID(typeof(Model.CostGoods_PayRegistration));
                payRegistration.PayRegistrationId = this.PayRegistrationId;
                BLL.PayRegistrationService.AddPayRegistration(payRegistration);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加安全费用投入登记");
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
            if (string.IsNullOrEmpty(this.PayRegistrationId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PayRegistrationAttachUrl&menuId={1}", this.PayRegistrationId, BLL.Const.ProjectPayRegistrationMenuId)));
        }
        #endregion
    }
}