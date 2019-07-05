using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全费用投入登记
    /// </summary>
    public static class PayRegistrationService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用投入登记
        /// </summary>
        /// <param name="payRegistrationId"></param>
        /// <returns></returns>
        public static Model.CostGoods_PayRegistration GetPayRegistrationById(string payRegistrationId)
        {
            return Funs.DB.CostGoods_PayRegistration.FirstOrDefault(e => e.PayRegistrationId == payRegistrationId);
        }

        /// <summary>
        /// 获取当年费用小计
        /// </summary>
        /// <param name="paydate"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_PayRegistration> GetPayRegistrationByYear(string projectId, DateTime paydate)
        {
            return (from x in db.CostGoods_PayRegistration where x.ProjectId == projectId && x.PayDate.Value.Year == paydate.Year select x).ToList();
        }

        /// <summary>
        /// 添加安全费用投入登记
        /// </summary>
        /// <param name="payRegistration"></param>
        public static void AddPayRegistration(Model.CostGoods_PayRegistration payRegistration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_PayRegistration newPayRegistration = new Model.CostGoods_PayRegistration();
            newPayRegistration.PayRegistrationId = payRegistration.PayRegistrationId;
            newPayRegistration.ProjectId = payRegistration.ProjectId;
            newPayRegistration.UnitId = payRegistration.UnitId;
            newPayRegistration.PayDate = payRegistration.PayDate;
            newPayRegistration.State = payRegistration.State;
            newPayRegistration.CompileMan = payRegistration.CompileMan;
            newPayRegistration.CompileDate = payRegistration.CompileDate;
            #region
            newPayRegistration.SMonthType1_1 = payRegistration.SMonthType1_1;
            newPayRegistration.SMainApproveType1_1 = payRegistration.SMainApproveType1_1;
            newPayRegistration.SMonthType1_2 = payRegistration.SMonthType1_2;
            newPayRegistration.SMainApproveType1_2 = payRegistration.SMainApproveType1_2;
            newPayRegistration.SMonthType1_3 = payRegistration.SMonthType1_3;
            newPayRegistration.SMainApproveType1_3 = payRegistration.SMainApproveType1_3;
            newPayRegistration.SMonthType1_4 = payRegistration.SMonthType1_4;
            newPayRegistration.SMainApproveType1_4 = payRegistration.SMainApproveType1_4;
            newPayRegistration.SMonthType1_5 = payRegistration.SMonthType1_5;
            newPayRegistration.SMainApproveType1_5 = payRegistration.SMainApproveType1_5;
            newPayRegistration.SMonthType1_6 = payRegistration.SMonthType1_6;
            newPayRegistration.SMainApproveType1_6 = payRegistration.SMainApproveType1_6;
            newPayRegistration.SMonthType1_7 = payRegistration.SMonthType1_7;
            newPayRegistration.SMainApproveType1_7 = payRegistration.SMainApproveType1_7;
            newPayRegistration.SMonthType1_8 = payRegistration.SMonthType1_8;
            newPayRegistration.SMainApproveType1_8 = payRegistration.SMainApproveType1_8;
            newPayRegistration.SMonthType1_9 = payRegistration.SMonthType1_9;
            newPayRegistration.SMainApproveType1_9 = payRegistration.SMainApproveType1_9;
            newPayRegistration.SMonthType1_10 = payRegistration.SMonthType1_10;
            newPayRegistration.SMainApproveType1_10 = payRegistration.SMainApproveType1_10;
            newPayRegistration.SMonthType1_11 = payRegistration.SMonthType1_11;
            newPayRegistration.SMainApproveType1_11 = payRegistration.SMainApproveType1_11;
            newPayRegistration.SMonthType1_12 = payRegistration.SMonthType1_12;
            newPayRegistration.SMainApproveType1_12 = payRegistration.SMainApproveType1_12;
            newPayRegistration.SMonthType1_13 = payRegistration.SMonthType1_13;
            newPayRegistration.SMainApproveType1_13 = payRegistration.SMainApproveType1_13;
            newPayRegistration.SMonthType1_14 = payRegistration.SMonthType1_14;
            newPayRegistration.SMainApproveType1_14 = payRegistration.SMainApproveType1_14;
            newPayRegistration.SMonthType1_15 = payRegistration.SMonthType1_15;
            newPayRegistration.SMainApproveType1_15 = payRegistration.SMainApproveType1_15;
            newPayRegistration.SMonthType1_16 = payRegistration.SMonthType1_16;
            newPayRegistration.SMainApproveType1_16 = payRegistration.SMainApproveType1_16;
            newPayRegistration.SMonthType2_1 = payRegistration.SMonthType2_1;
            newPayRegistration.SMainApproveType2_1 = payRegistration.SMainApproveType2_1;
            newPayRegistration.SMonthType2_2 = payRegistration.SMonthType2_2;
            newPayRegistration.SMainApproveType2_2 = payRegistration.SMainApproveType2_2;
            newPayRegistration.SMonthType2_3 = payRegistration.SMonthType2_3;
            newPayRegistration.SMainApproveType2_3 = payRegistration.SMainApproveType2_3;
            newPayRegistration.SMonthType2_4 = payRegistration.SMonthType2_4;
            newPayRegistration.SMainApproveType2_4 = payRegistration.SMainApproveType2_4;
            newPayRegistration.SMonthType3_1 = payRegistration.SMonthType3_1;
            newPayRegistration.SMainApproveType3_1 = payRegistration.SMainApproveType3_1;
            newPayRegistration.SMonthType3_2 = payRegistration.SMonthType3_2;
            newPayRegistration.SMainApproveType3_2 = payRegistration.SMainApproveType3_2;
            newPayRegistration.SMonthType3_3 = payRegistration.SMonthType3_3;
            newPayRegistration.SMainApproveType3_3 = payRegistration.SMainApproveType3_3;
            newPayRegistration.SMonthType3_4 = payRegistration.SMonthType3_4;
            newPayRegistration.SMainApproveType3_4 = payRegistration.SMainApproveType3_4;
            newPayRegistration.SMonthType3_5 = payRegistration.SMonthType3_5;
            newPayRegistration.SMainApproveType3_5 = payRegistration.SMainApproveType3_5;
            newPayRegistration.SMonthType3_6 = payRegistration.SMonthType3_6;
            newPayRegistration.SMainApproveType3_6 = payRegistration.SMainApproveType3_6;
            newPayRegistration.SMonthType4_1 = payRegistration.SMonthType4_1;
            newPayRegistration.SMainApproveType4_1 = payRegistration.SMainApproveType4_1;
            newPayRegistration.SMonthType4_2 = payRegistration.SMonthType4_2;
            newPayRegistration.SMainApproveType4_2 = payRegistration.SMainApproveType4_2;
            newPayRegistration.SMonthType4_3 = payRegistration.SMonthType4_3;
            newPayRegistration.SMainApproveType4_3 = payRegistration.SMainApproveType4_3;
            newPayRegistration.SMonthType4_4 = payRegistration.SMonthType4_4;
            newPayRegistration.SMainApproveType4_4 = payRegistration.SMainApproveType4_4;
            newPayRegistration.SMonthType4_5 = payRegistration.SMonthType4_5;
            newPayRegistration.SMainApproveType4_5 = payRegistration.SMainApproveType4_5;
            newPayRegistration.SMonthType4_6 = payRegistration.SMonthType4_6;
            newPayRegistration.SMainApproveType4_6 = payRegistration.SMainApproveType4_6;
            newPayRegistration.SMonthType4_7 = payRegistration.SMonthType4_7;
            newPayRegistration.SMainApproveType4_7 = payRegistration.SMainApproveType4_7;
            newPayRegistration.SMonthType4_8 = payRegistration.SMonthType4_8;
            newPayRegistration.SMainApproveType4_8 = payRegistration.SMainApproveType4_8;
            newPayRegistration.SMonthType4_9 = payRegistration.SMonthType4_9;
            newPayRegistration.SMainApproveType4_9 = payRegistration.SMainApproveType4_9;
            newPayRegistration.SMonthType4_10 = payRegistration.SMonthType4_10;
            newPayRegistration.SMainApproveType4_10 = payRegistration.SMainApproveType4_10;
            newPayRegistration.SMonthType4_11 = payRegistration.SMonthType4_11;
            newPayRegistration.SMainApproveType4_11 = payRegistration.SMainApproveType4_11;
            newPayRegistration.SMonthType4_12 = payRegistration.SMonthType4_12;
            newPayRegistration.SMainApproveType4_12 = payRegistration.SMainApproveType4_12;
            newPayRegistration.SMonthType4_13 = payRegistration.SMonthType4_13;
            newPayRegistration.SMainApproveType4_13 = payRegistration.SMainApproveType4_13;
            newPayRegistration.SMonthType4_14 = payRegistration.SMonthType4_14;
            newPayRegistration.SMainApproveType4_14 = payRegistration.SMainApproveType4_14;
            newPayRegistration.SMonthType4_15 = payRegistration.SMonthType4_15;
            newPayRegistration.SMainApproveType4_15 = payRegistration.SMainApproveType4_15;
            newPayRegistration.SMonthType4_16 = payRegistration.SMonthType4_16;
            newPayRegistration.SMainApproveType4_16 = payRegistration.SMainApproveType4_16;
            newPayRegistration.SMonthType4_17 = payRegistration.SMonthType4_17;
            newPayRegistration.SMainApproveType4_17 = payRegistration.SMainApproveType4_17;
            newPayRegistration.SMonthType4_18 = payRegistration.SMonthType4_18;
            newPayRegistration.SMainApproveType4_18 = payRegistration.SMainApproveType4_18;
            newPayRegistration.SMonthType4_19 = payRegistration.SMonthType4_19;
            newPayRegistration.SMainApproveType4_19 = payRegistration.SMainApproveType4_19;
            newPayRegistration.SMonthType4_20 = payRegistration.SMonthType4_20;
            newPayRegistration.SMainApproveType4_20 = payRegistration.SMainApproveType4_20;
            newPayRegistration.SMonthType4_21 = payRegistration.SMonthType4_21;
            newPayRegistration.SMainApproveType4_21 = payRegistration.SMainApproveType4_21;
            newPayRegistration.SMonthType4_22 = payRegistration.SMonthType4_22;
            newPayRegistration.SMainApproveType4_22 = payRegistration.SMainApproveType4_22;
            newPayRegistration.SMonthType4_23 = payRegistration.SMonthType4_23;
            newPayRegistration.SMainApproveType4_23 = payRegistration.SMainApproveType4_23;
            newPayRegistration.SMonthType4_24 = payRegistration.SMonthType4_24;
            newPayRegistration.SMainApproveType4_24 = payRegistration.SMainApproveType4_24;
            newPayRegistration.SMonthType4_25 = payRegistration.SMonthType4_25;
            newPayRegistration.SMainApproveType4_25 = payRegistration.SMainApproveType4_25;
            newPayRegistration.SMonthType4_26 = payRegistration.SMonthType4_26;
            newPayRegistration.SMainApproveType4_26 = payRegistration.SMainApproveType4_26;
            newPayRegistration.SMonthType4_27 = payRegistration.SMonthType4_27;
            newPayRegistration.SMainApproveType4_27 = payRegistration.SMainApproveType4_27;
            newPayRegistration.SMonthType4_28 = payRegistration.SMonthType4_28;
            newPayRegistration.SMainApproveType4_28 = payRegistration.SMainApproveType4_28;
            newPayRegistration.SMonthType4_29 = payRegistration.SMonthType4_29;
            newPayRegistration.SMainApproveType4_29 = payRegistration.SMainApproveType4_29;
            newPayRegistration.SMonthType4_30 = payRegistration.SMonthType4_30;
            newPayRegistration.SMainApproveType4_30 = payRegistration.SMainApproveType4_30;
            newPayRegistration.SMonthType4_31 = payRegistration.SMonthType4_31;
            newPayRegistration.SMainApproveType4_31 = payRegistration.SMainApproveType4_31;
            newPayRegistration.SMonthType4_32 = payRegistration.SMonthType4_32;
            newPayRegistration.SMainApproveType4_32 = payRegistration.SMainApproveType4_32;
            newPayRegistration.SMonthType4_33 = payRegistration.SMonthType4_33;
            newPayRegistration.SMainApproveType4_33 = payRegistration.SMainApproveType4_33;
            newPayRegistration.SMonthType4_34 = payRegistration.SMonthType4_34;
            newPayRegistration.SMainApproveType4_34 = payRegistration.SMainApproveType4_34;
            newPayRegistration.SMonthType4_35 = payRegistration.SMonthType4_35;
            newPayRegistration.SMainApproveType4_35 = payRegistration.SMainApproveType4_35;
            newPayRegistration.SMonthType4_36 = payRegistration.SMonthType4_36;
            newPayRegistration.SMainApproveType4_36 = payRegistration.SMainApproveType4_36;
            newPayRegistration.SMonthType4_37 = payRegistration.SMonthType4_37;
            newPayRegistration.SMainApproveType4_37 = payRegistration.SMainApproveType4_37;
            newPayRegistration.SMonthType4_38 = payRegistration.SMonthType4_38;
            newPayRegistration.SMainApproveType4_38 = payRegistration.SMainApproveType4_38;
            newPayRegistration.SMonthType4_39 = payRegistration.SMonthType4_39;
            newPayRegistration.SMainApproveType4_39 = payRegistration.SMainApproveType4_39;
            newPayRegistration.SMonthType4_40 = payRegistration.SMonthType4_40;
            newPayRegistration.SMainApproveType4_40 = payRegistration.SMainApproveType4_40;
            newPayRegistration.SMonthType5_1 = payRegistration.SMonthType5_1;
            newPayRegistration.SMainApproveType5_1 = payRegistration.SMainApproveType5_1;
            newPayRegistration.SMonthType5_2 = payRegistration.SMonthType5_2;
            newPayRegistration.SMainApproveType5_2 = payRegistration.SMainApproveType5_2;
            newPayRegistration.SMonthType5_3 = payRegistration.SMonthType5_3;
            newPayRegistration.SMainApproveType5_3 = payRegistration.SMainApproveType5_3;
            newPayRegistration.SMonthType5_4 = payRegistration.SMonthType5_4;
            newPayRegistration.SMainApproveType5_4 = payRegistration.SMainApproveType5_4;
            newPayRegistration.SMonthType5_5 = payRegistration.SMonthType5_5;
            newPayRegistration.SMainApproveType5_5 = payRegistration.SMainApproveType5_5;
            newPayRegistration.SMonthType5_6 = payRegistration.SMonthType5_6;
            newPayRegistration.SMainApproveType5_6 = payRegistration.SMainApproveType5_6;
            newPayRegistration.SMonthType6_1 = payRegistration.SMonthType6_1;
            newPayRegistration.SMainApproveType6_1 = payRegistration.SMainApproveType6_1;
            newPayRegistration.SMonthType6_2 = payRegistration.SMonthType6_2;
            newPayRegistration.SMainApproveType6_2 = payRegistration.SMainApproveType6_2;
            newPayRegistration.SMonthType6_3 = payRegistration.SMonthType6_3;
            newPayRegistration.SMainApproveType6_3 = payRegistration.SMainApproveType6_3;
            newPayRegistration.TMonthType1_1 = payRegistration.TMonthType1_1;
            newPayRegistration.TMainApproveType1_1 = payRegistration.TMainApproveType1_1;
            newPayRegistration.TMonthType1_2 = payRegistration.TMonthType1_2;
            newPayRegistration.TMainApproveType1_2 = payRegistration.TMainApproveType1_2;
            newPayRegistration.TMonthType1_3 = payRegistration.TMonthType1_3;
            newPayRegistration.TMainApproveType1_3 = payRegistration.TMainApproveType1_3;
            newPayRegistration.TMonthType1_4 = payRegistration.TMonthType1_4;
            newPayRegistration.TMainApproveType1_4 = payRegistration.TMainApproveType1_4;
            newPayRegistration.TMonthType1_5 = payRegistration.TMonthType1_5;
            newPayRegistration.TMainApproveType1_5 = payRegistration.TMainApproveType1_5;
            newPayRegistration.TMonthType1_6 = payRegistration.TMonthType1_6;
            newPayRegistration.TMainApproveType1_6 = payRegistration.TMainApproveType1_6;
            newPayRegistration.TMonthType1_7 = payRegistration.TMonthType1_7;
            newPayRegistration.TMainApproveType1_7 = payRegistration.TMainApproveType1_7;
            newPayRegistration.TMonthType1_8 = payRegistration.TMonthType1_8;
            newPayRegistration.TMainApproveType1_8 = payRegistration.TMainApproveType1_8;
            newPayRegistration.TMonthType1_9 = payRegistration.TMonthType1_9;
            newPayRegistration.TMainApproveType1_9 = payRegistration.TMainApproveType1_9;
            newPayRegistration.TMonthType1_10 = payRegistration.TMonthType1_10;
            newPayRegistration.TMainApproveType1_10 = payRegistration.TMainApproveType1_10;
            newPayRegistration.TMonthType1_11 = payRegistration.TMonthType1_11;
            newPayRegistration.TMainApproveType1_11 = payRegistration.TMainApproveType1_11;
            newPayRegistration.TMonthType2_1 = payRegistration.TMonthType2_1;
            newPayRegistration.TMainApproveType2_1 = payRegistration.TMainApproveType2_1;
            newPayRegistration.TMonthType2_2 = payRegistration.TMonthType2_2;
            newPayRegistration.TMainApproveType2_2 = payRegistration.TMainApproveType2_2;
            newPayRegistration.TMonthType2_3 = payRegistration.TMonthType2_3;
            newPayRegistration.TMainApproveType2_3 = payRegistration.TMainApproveType2_3;
            newPayRegistration.TMonthType2_4 = payRegistration.TMonthType2_4;
            newPayRegistration.TMainApproveType2_4 = payRegistration.TMainApproveType2_4;
            newPayRegistration.TMonthType2_5 = payRegistration.TMonthType2_5;
            newPayRegistration.TMainApproveType2_5 = payRegistration.TMainApproveType2_5;
            newPayRegistration.TMonthType2_6 = payRegistration.TMonthType2_6;
            newPayRegistration.TMainApproveType2_6 = payRegistration.TMainApproveType2_6;
            newPayRegistration.TMonthType2_7 = payRegistration.TMonthType2_7;
            newPayRegistration.TMainApproveType2_7 = payRegistration.TMainApproveType2_7;
            newPayRegistration.TMonthType2_8 = payRegistration.TMonthType2_8;
            newPayRegistration.TMainApproveType2_8 = payRegistration.TMainApproveType2_8;
            newPayRegistration.TMonthType2_9 = payRegistration.TMonthType2_9;
            newPayRegistration.TMainApproveType2_9 = payRegistration.TMainApproveType2_9;
            #endregion
            db.CostGoods_PayRegistration.InsertOnSubmit(newPayRegistration);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全费用投入登记
        /// </summary>
        /// <param name="payRegistration"></param>
        public static void UpdatePayRegistration(Model.CostGoods_PayRegistration payRegistration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_PayRegistration newPayRegistration = db.CostGoods_PayRegistration.FirstOrDefault(e => e.PayRegistrationId == payRegistration.PayRegistrationId);
            if (newPayRegistration != null)
            {
                newPayRegistration.ProjectId = payRegistration.ProjectId;
                newPayRegistration.UnitId = payRegistration.UnitId;
                newPayRegistration.PayDate = payRegistration.PayDate;
                newPayRegistration.State = payRegistration.State;
                newPayRegistration.CompileMan = payRegistration.CompileMan;
                newPayRegistration.CompileDate = payRegistration.CompileDate;
                #region
                newPayRegistration.SMonthType1_1 = payRegistration.SMonthType1_1;
                newPayRegistration.SMainApproveType1_1 = payRegistration.SMainApproveType1_1;
                newPayRegistration.SMonthType1_2 = payRegistration.SMonthType1_2;
                newPayRegistration.SMainApproveType1_2 = payRegistration.SMainApproveType1_2;
                newPayRegistration.SMonthType1_3 = payRegistration.SMonthType1_3;
                newPayRegistration.SMainApproveType1_3 = payRegistration.SMainApproveType1_3;
                newPayRegistration.SMonthType1_4 = payRegistration.SMonthType1_4;
                newPayRegistration.SMainApproveType1_4 = payRegistration.SMainApproveType1_4;
                newPayRegistration.SMonthType1_5 = payRegistration.SMonthType1_5;
                newPayRegistration.SMainApproveType1_5 = payRegistration.SMainApproveType1_5;
                newPayRegistration.SMonthType1_6 = payRegistration.SMonthType1_6;
                newPayRegistration.SMainApproveType1_6 = payRegistration.SMainApproveType1_6;
                newPayRegistration.SMonthType1_7 = payRegistration.SMonthType1_7;
                newPayRegistration.SMainApproveType1_7 = payRegistration.SMainApproveType1_7;
                newPayRegistration.SMonthType1_8 = payRegistration.SMonthType1_8;
                newPayRegistration.SMainApproveType1_8 = payRegistration.SMainApproveType1_8;
                newPayRegistration.SMonthType1_9 = payRegistration.SMonthType1_9;
                newPayRegistration.SMainApproveType1_9 = payRegistration.SMainApproveType1_9;
                newPayRegistration.SMonthType1_10 = payRegistration.SMonthType1_10;
                newPayRegistration.SMainApproveType1_10 = payRegistration.SMainApproveType1_10;
                newPayRegistration.SMonthType1_11 = payRegistration.SMonthType1_11;
                newPayRegistration.SMainApproveType1_11 = payRegistration.SMainApproveType1_11;
                newPayRegistration.SMonthType1_12 = payRegistration.SMonthType1_12;
                newPayRegistration.SMainApproveType1_12 = payRegistration.SMainApproveType1_12;
                newPayRegistration.SMonthType1_13 = payRegistration.SMonthType1_13;
                newPayRegistration.SMainApproveType1_13 = payRegistration.SMainApproveType1_13;
                newPayRegistration.SMonthType1_14 = payRegistration.SMonthType1_14;
                newPayRegistration.SMainApproveType1_14 = payRegistration.SMainApproveType1_14;
                newPayRegistration.SMonthType1_15 = payRegistration.SMonthType1_15;
                newPayRegistration.SMainApproveType1_15 = payRegistration.SMainApproveType1_15;
                newPayRegistration.SMonthType1_16 = payRegistration.SMonthType1_16;
                newPayRegistration.SMainApproveType1_16 = payRegistration.SMainApproveType1_16;
                newPayRegistration.SMonthType2_1 = payRegistration.SMonthType2_1;
                newPayRegistration.SMainApproveType2_1 = payRegistration.SMainApproveType2_1;
                newPayRegistration.SMonthType2_2 = payRegistration.SMonthType2_2;
                newPayRegistration.SMainApproveType2_2 = payRegistration.SMainApproveType2_2;
                newPayRegistration.SMonthType2_3 = payRegistration.SMonthType2_3;
                newPayRegistration.SMainApproveType2_3 = payRegistration.SMainApproveType2_3;
                newPayRegistration.SMonthType2_4 = payRegistration.SMonthType2_4;
                newPayRegistration.SMainApproveType2_4 = payRegistration.SMainApproveType2_4;
                newPayRegistration.SMonthType3_1 = payRegistration.SMonthType3_1;
                newPayRegistration.SMainApproveType3_1 = payRegistration.SMainApproveType3_1;
                newPayRegistration.SMonthType3_2 = payRegistration.SMonthType3_2;
                newPayRegistration.SMainApproveType3_2 = payRegistration.SMainApproveType3_2;
                newPayRegistration.SMonthType3_3 = payRegistration.SMonthType3_3;
                newPayRegistration.SMainApproveType3_3 = payRegistration.SMainApproveType3_3;
                newPayRegistration.SMonthType3_4 = payRegistration.SMonthType3_4;
                newPayRegistration.SMainApproveType3_4 = payRegistration.SMainApproveType3_4;
                newPayRegistration.SMonthType3_5 = payRegistration.SMonthType3_5;
                newPayRegistration.SMainApproveType3_5 = payRegistration.SMainApproveType3_5;
                newPayRegistration.SMonthType3_6 = payRegistration.SMonthType3_6;
                newPayRegistration.SMainApproveType3_6 = payRegistration.SMainApproveType3_6;
                newPayRegistration.SMonthType4_1 = payRegistration.SMonthType4_1;
                newPayRegistration.SMainApproveType4_1 = payRegistration.SMainApproveType4_1;
                newPayRegistration.SMonthType4_2 = payRegistration.SMonthType4_2;
                newPayRegistration.SMainApproveType4_2 = payRegistration.SMainApproveType4_2;
                newPayRegistration.SMonthType4_3 = payRegistration.SMonthType4_3;
                newPayRegistration.SMainApproveType4_3 = payRegistration.SMainApproveType4_3;
                newPayRegistration.SMonthType4_4 = payRegistration.SMonthType4_4;
                newPayRegistration.SMainApproveType4_4 = payRegistration.SMainApproveType4_4;
                newPayRegistration.SMonthType4_5 = payRegistration.SMonthType4_5;
                newPayRegistration.SMainApproveType4_5 = payRegistration.SMainApproveType4_5;
                newPayRegistration.SMonthType4_6 = payRegistration.SMonthType4_6;
                newPayRegistration.SMainApproveType4_6 = payRegistration.SMainApproveType4_6;
                newPayRegistration.SMonthType4_7 = payRegistration.SMonthType4_7;
                newPayRegistration.SMainApproveType4_7 = payRegistration.SMainApproveType4_7;
                newPayRegistration.SMonthType4_8 = payRegistration.SMonthType4_8;
                newPayRegistration.SMainApproveType4_8 = payRegistration.SMainApproveType4_8;
                newPayRegistration.SMonthType4_9 = payRegistration.SMonthType4_9;
                newPayRegistration.SMainApproveType4_9 = payRegistration.SMainApproveType4_9;
                newPayRegistration.SMonthType4_10 = payRegistration.SMonthType4_10;
                newPayRegistration.SMainApproveType4_10 = payRegistration.SMainApproveType4_10;
                newPayRegistration.SMonthType4_11 = payRegistration.SMonthType4_11;
                newPayRegistration.SMainApproveType4_11 = payRegistration.SMainApproveType4_11;
                newPayRegistration.SMonthType4_12 = payRegistration.SMonthType4_12;
                newPayRegistration.SMainApproveType4_12 = payRegistration.SMainApproveType4_12;
                newPayRegistration.SMonthType4_13 = payRegistration.SMonthType4_13;
                newPayRegistration.SMainApproveType4_13 = payRegistration.SMainApproveType4_13;
                newPayRegistration.SMonthType4_14 = payRegistration.SMonthType4_14;
                newPayRegistration.SMainApproveType4_14 = payRegistration.SMainApproveType4_14;
                newPayRegistration.SMonthType4_15 = payRegistration.SMonthType4_15;
                newPayRegistration.SMainApproveType4_15 = payRegistration.SMainApproveType4_15;
                newPayRegistration.SMonthType4_16 = payRegistration.SMonthType4_16;
                newPayRegistration.SMainApproveType4_16 = payRegistration.SMainApproveType4_16;
                newPayRegistration.SMonthType4_17 = payRegistration.SMonthType4_17;
                newPayRegistration.SMainApproveType4_17 = payRegistration.SMainApproveType4_17;
                newPayRegistration.SMonthType4_18 = payRegistration.SMonthType4_18;
                newPayRegistration.SMainApproveType4_18 = payRegistration.SMainApproveType4_18;
                newPayRegistration.SMonthType4_19 = payRegistration.SMonthType4_19;
                newPayRegistration.SMainApproveType4_19 = payRegistration.SMainApproveType4_19;
                newPayRegistration.SMonthType4_20 = payRegistration.SMonthType4_20;
                newPayRegistration.SMainApproveType4_20 = payRegistration.SMainApproveType4_20;
                newPayRegistration.SMonthType4_21 = payRegistration.SMonthType4_21;
                newPayRegistration.SMainApproveType4_21 = payRegistration.SMainApproveType4_21;
                newPayRegistration.SMonthType4_22 = payRegistration.SMonthType4_22;
                newPayRegistration.SMainApproveType4_22 = payRegistration.SMainApproveType4_22;
                newPayRegistration.SMonthType4_23 = payRegistration.SMonthType4_23;
                newPayRegistration.SMainApproveType4_23 = payRegistration.SMainApproveType4_23;
                newPayRegistration.SMonthType4_24 = payRegistration.SMonthType4_24;
                newPayRegistration.SMainApproveType4_24 = payRegistration.SMainApproveType4_24;
                newPayRegistration.SMonthType4_25 = payRegistration.SMonthType4_25;
                newPayRegistration.SMainApproveType4_25 = payRegistration.SMainApproveType4_25;
                newPayRegistration.SMonthType4_26 = payRegistration.SMonthType4_26;
                newPayRegistration.SMainApproveType4_26 = payRegistration.SMainApproveType4_26;
                newPayRegistration.SMonthType4_27 = payRegistration.SMonthType4_27;
                newPayRegistration.SMainApproveType4_27 = payRegistration.SMainApproveType4_27;
                newPayRegistration.SMonthType4_28 = payRegistration.SMonthType4_28;
                newPayRegistration.SMainApproveType4_28 = payRegistration.SMainApproveType4_28;
                newPayRegistration.SMonthType4_29 = payRegistration.SMonthType4_29;
                newPayRegistration.SMainApproveType4_29 = payRegistration.SMainApproveType4_29;
                newPayRegistration.SMonthType4_30 = payRegistration.SMonthType4_30;
                newPayRegistration.SMainApproveType4_30 = payRegistration.SMainApproveType4_30;
                newPayRegistration.SMonthType4_31 = payRegistration.SMonthType4_31;
                newPayRegistration.SMainApproveType4_31 = payRegistration.SMainApproveType4_31;
                newPayRegistration.SMonthType4_32 = payRegistration.SMonthType4_32;
                newPayRegistration.SMainApproveType4_32 = payRegistration.SMainApproveType4_32;
                newPayRegistration.SMonthType4_33 = payRegistration.SMonthType4_33;
                newPayRegistration.SMainApproveType4_33 = payRegistration.SMainApproveType4_33;
                newPayRegistration.SMonthType4_34 = payRegistration.SMonthType4_34;
                newPayRegistration.SMainApproveType4_34 = payRegistration.SMainApproveType4_34;
                newPayRegistration.SMonthType4_35 = payRegistration.SMonthType4_35;
                newPayRegistration.SMainApproveType4_35 = payRegistration.SMainApproveType4_35;
                newPayRegistration.SMonthType4_36 = payRegistration.SMonthType4_36;
                newPayRegistration.SMainApproveType4_36 = payRegistration.SMainApproveType4_36;
                newPayRegistration.SMonthType4_37 = payRegistration.SMonthType4_37;
                newPayRegistration.SMainApproveType4_37 = payRegistration.SMainApproveType4_37;
                newPayRegistration.SMonthType4_38 = payRegistration.SMonthType4_38;
                newPayRegistration.SMainApproveType4_38 = payRegistration.SMainApproveType4_38;
                newPayRegistration.SMonthType4_39 = payRegistration.SMonthType4_39;
                newPayRegistration.SMainApproveType4_39 = payRegistration.SMainApproveType4_39;
                newPayRegistration.SMonthType4_40 = payRegistration.SMonthType4_40;
                newPayRegistration.SMainApproveType4_40 = payRegistration.SMainApproveType4_40;
                newPayRegistration.SMonthType5_1 = payRegistration.SMonthType5_1;
                newPayRegistration.SMainApproveType5_1 = payRegistration.SMainApproveType5_1;
                newPayRegistration.SMonthType5_2 = payRegistration.SMonthType5_2;
                newPayRegistration.SMainApproveType5_2 = payRegistration.SMainApproveType5_2;
                newPayRegistration.SMonthType5_3 = payRegistration.SMonthType5_3;
                newPayRegistration.SMainApproveType5_3 = payRegistration.SMainApproveType5_3;
                newPayRegistration.SMonthType5_4 = payRegistration.SMonthType5_4;
                newPayRegistration.SMainApproveType5_4 = payRegistration.SMainApproveType5_4;
                newPayRegistration.SMonthType5_5 = payRegistration.SMonthType5_5;
                newPayRegistration.SMainApproveType5_5 = payRegistration.SMainApproveType5_5;
                newPayRegistration.SMonthType5_6 = payRegistration.SMonthType5_6;
                newPayRegistration.SMainApproveType5_6 = payRegistration.SMainApproveType5_6;
                newPayRegistration.SMonthType6_1 = payRegistration.SMonthType6_1;
                newPayRegistration.SMainApproveType6_1 = payRegistration.SMainApproveType6_1;
                newPayRegistration.SMonthType6_2 = payRegistration.SMonthType6_2;
                newPayRegistration.SMainApproveType6_2 = payRegistration.SMainApproveType6_2;
                newPayRegistration.SMonthType6_3 = payRegistration.SMonthType6_3;
                newPayRegistration.SMainApproveType6_3 = payRegistration.SMainApproveType6_3;
                newPayRegistration.TMonthType1_1 = payRegistration.TMonthType1_1;
                newPayRegistration.TMainApproveType1_1 = payRegistration.TMainApproveType1_1;
                newPayRegistration.TMonthType1_2 = payRegistration.TMonthType1_2;
                newPayRegistration.TMainApproveType1_2 = payRegistration.TMainApproveType1_2;
                newPayRegistration.TMonthType1_3 = payRegistration.TMonthType1_3;
                newPayRegistration.TMainApproveType1_3 = payRegistration.TMainApproveType1_3;
                newPayRegistration.TMonthType1_4 = payRegistration.TMonthType1_4;
                newPayRegistration.TMainApproveType1_4 = payRegistration.TMainApproveType1_4;
                newPayRegistration.TMonthType1_5 = payRegistration.TMonthType1_5;
                newPayRegistration.TMainApproveType1_5 = payRegistration.TMainApproveType1_5;
                newPayRegistration.TMonthType1_6 = payRegistration.TMonthType1_6;
                newPayRegistration.TMainApproveType1_6 = payRegistration.TMainApproveType1_6;
                newPayRegistration.TMonthType1_7 = payRegistration.TMonthType1_7;
                newPayRegistration.TMainApproveType1_7 = payRegistration.TMainApproveType1_7;
                newPayRegistration.TMonthType1_8 = payRegistration.TMonthType1_8;
                newPayRegistration.TMainApproveType1_8 = payRegistration.TMainApproveType1_8;
                newPayRegistration.TMonthType1_9 = payRegistration.TMonthType1_9;
                newPayRegistration.TMainApproveType1_9 = payRegistration.TMainApproveType1_9;
                newPayRegistration.TMonthType1_10 = payRegistration.TMonthType1_10;
                newPayRegistration.TMainApproveType1_10 = payRegistration.TMainApproveType1_10;
                newPayRegistration.TMonthType1_11 = payRegistration.TMonthType1_11;
                newPayRegistration.TMainApproveType1_11 = payRegistration.TMainApproveType1_11;
                newPayRegistration.TMonthType2_1 = payRegistration.TMonthType2_1;
                newPayRegistration.TMainApproveType2_1 = payRegistration.TMainApproveType2_1;
                newPayRegistration.TMonthType2_2 = payRegistration.TMonthType2_2;
                newPayRegistration.TMainApproveType2_2 = payRegistration.TMainApproveType2_2;
                newPayRegistration.TMonthType2_3 = payRegistration.TMonthType2_3;
                newPayRegistration.TMainApproveType2_3 = payRegistration.TMainApproveType2_3;
                newPayRegistration.TMonthType2_4 = payRegistration.TMonthType2_4;
                newPayRegistration.TMainApproveType2_4 = payRegistration.TMainApproveType2_4;
                newPayRegistration.TMonthType2_5 = payRegistration.TMonthType2_5;
                newPayRegistration.TMainApproveType2_5 = payRegistration.TMainApproveType2_5;
                newPayRegistration.TMonthType2_6 = payRegistration.TMonthType2_6;
                newPayRegistration.TMainApproveType2_6 = payRegistration.TMainApproveType2_6;
                newPayRegistration.TMonthType2_7 = payRegistration.TMonthType2_7;
                newPayRegistration.TMainApproveType2_7 = payRegistration.TMainApproveType2_7;
                newPayRegistration.TMonthType2_8 = payRegistration.TMonthType2_8;
                newPayRegistration.TMainApproveType2_8 = payRegistration.TMainApproveType2_8;
                newPayRegistration.TMonthType2_9 = payRegistration.TMonthType2_9;
                newPayRegistration.TMainApproveType2_9 = payRegistration.TMainApproveType2_9;
                #endregion
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全费用投入登记
        /// </summary>
        /// <param name="payRegistrationId"></param>
        public static void DeletePayRegistrationById(string payRegistrationId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_PayRegistration payRegistration = db.CostGoods_PayRegistration.FirstOrDefault(e => e.PayRegistrationId == payRegistrationId);
            if (payRegistration != null)
            {
                CommonService.DeleteFlowOperateByID(payRegistrationId);
                db.CostGoods_PayRegistration.DeleteOnSubmit(payRegistration);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据时间、项目获取安全费用投入登记信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_PayRegistration> GetPayRegistrationByPayDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in db.CostGoods_PayRegistration where x.PayDate >= startTime && x.PayDate <= endTime && x.ProjectId == projectId select x).ToList();
        }
    }
}