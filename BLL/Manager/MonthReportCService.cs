using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MonthReportCService
    {
        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = from x in Funs.DB.Manager_MonthReportC where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据月份获取当月月报
        /// </summary>
        /// <param name="months">月份</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportC GetMonthReportByMonths(DateTime months, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportC where x.ProjectId == projectId && x.Months == months select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据时间获取最近时间的月报
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetFreezeMonthReportByDate(DateTime date, string projectId, int day)
        {
            var a = from x in Funs.DB.Manager_MonthReportC where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.AddMonths(1).Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) && date.Day < (day + 1) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportC GetLastMonthReportByDate(DateTime date, string projectId)
        {
            Model.Manager_MonthReportC LastMonth = null;

            var q = from x in Funs.DB.Manager_MonthReportC where x.ProjectId == projectId && x.Months <= date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                var month = from x in q where x.Months == date select x;
                if (month.Count() > 0)  // 表示存在当月记录
                {
                    if (q.Count() > 1)
                    {
                        LastMonth = q.ToList()[1];
                    }
                }
                else    // 表示不存在当月记录
                {
                    LastMonth = q.ToList()[0];
                }
            }
            return LastMonth;
        }

        /// <summary>
        /// 根据日期获得月报告信息集合
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<string> GetMonthReportIdsByDate(DateTime date)
        {
            return (from x in Funs.DB.Manager_MonthReportC where x.MonthReportDate < date select x.MonthReportId).ToList();
        }

        /// <summary>
        /// 根据年份获得月报告信息集合
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportC> GetMonthReportsByYear(DateTime date)
        {
            return (from x in Funs.DB.Manager_MonthReportC where x.MonthReportDate < date select x).ToList();
        }

        ///// <summary>
        ///// 根据日期获得最近的一条月报告信息
        ///// </summary>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public static Model.Manager_MonthReportC GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        //{
        //    Model.Manager_MonthReportC LastMonth = null;
        //    if (date.Day < freezeDay)
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportC where x.Months <= date.AddMonths(-2) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    else
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportC where x.Months < date.AddMonths(-1) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    return LastMonth;
        //}

        /// <summary>
        /// 根据日期获得最近的一条月报告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportC GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        {
            Model.Manager_MonthReportC LastMonth = null;
            DateTime? monthDate = null;

            var q = from x in Funs.DB.Manager_MonthReportC where x.ProjectId == projectId && x.Months < date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                if (date.Day < freezeDay)
                {
                    DateTime c = date.AddMonths(-1).Date;
                    monthDate = Convert.ToDateTime(c.Year + "-" + c.Month + "-01");  //当月
                }
                else
                {
                    monthDate = Convert.ToDateTime(date.Year + "-" + date.Month + "-01");  //当月
                }

                var month = from x in q where x.Months == monthDate select x;

                if (month.Count() > 0)  // 表示存在当月记录
                {
                    if (q.Count() > 1)
                    {
                        LastMonth = q.ToList()[1];
                    }
                }
                else    // 表示不存在当月记录
                {
                    LastMonth = q.ToList()[0];
                }
            }
            return LastMonth;
        }

        /// <summary>
        /// 根据月报告主键获取月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReportC GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MonthReportC.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据月报告编号获取月报告信息
        /// </summary>
        /// <param name="monthReportCode">月报告编号</param>
        /// <returns>月报告信息</returns>
        public static Model.Manager_MonthReportC GetMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_MonthReportC.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        /// <summary>
        /// 增加月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void AddMonthReport(Model.Manager_MonthReportC monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportC newMonthReport = new Model.Manager_MonthReportC
            {
                MonthReportId = monthReport.MonthReportId,
                MonthReportCode = monthReport.MonthReportCode,
                ProjectId = monthReport.ProjectId,
                Months = monthReport.Months,
                MonthReportDate = monthReport.MonthReportDate,
                ReportMan = monthReport.ReportMan,
                HseManhours = monthReport.HseManhours,
                TotalHseManhours = monthReport.TotalHseManhours,
                SafetyManhours = monthReport.SafetyManhours,
                HseActiveReview = monthReport.HseActiveReview,
                HseActiveKey = monthReport.HseActiveKey,
                TotalManNum = monthReport.TotalManNum,
                ScanUrl = monthReport.ScanUrl,
                MonthHSEDay = monthReport.MonthHSEDay,
                SumHSEDay = monthReport.SumHSEDay,
                MonthHSEWorkDay = monthReport.MonthHSEWorkDay,
                YearHSEWorkDay = monthReport.YearHSEWorkDay,
                SumHSEWorkDay = monthReport.SumHSEWorkDay,
                HazardNum = monthReport.HazardNum,
                YearHazardNum = monthReport.YearHazardNum,
                MeetingNum = monthReport.MeetingNum,
                YearMeetingNum = monthReport.YearMeetingNum,
                PromotionalActiviteNum = monthReport.PromotionalActiviteNum,
                YearPromotionalActiviteNum = monthReport.YearPromotionalActiviteNum,
                ComplexEmergencyNum = monthReport.ComplexEmergencyNum,
                YearComplexEmergencyNum = monthReport.YearComplexEmergencyNum,
                SpecialEmergencyNum = monthReport.SpecialEmergencyNum,
                YearSpecialEmergencyNum = monthReport.YearSpecialEmergencyNum,
                DrillRecordNum = monthReport.DrillRecordNum,
                YearDrillRecordNum = monthReport.YearDrillRecordNum,
                LicenseNum = monthReport.LicenseNum,
                YearLicenseNum = monthReport.YearLicenseNum,
                EquipmentNum = monthReport.EquipmentNum,
                YearEquipmentNum = monthReport.YearEquipmentNum,
                LicenseRemark = monthReport.LicenseRemark,
                EquipmentRemark = monthReport.EquipmentRemark,
                RewardNum = monthReport.RewardNum,
                YearRewardNum = monthReport.YearRewardNum,
                RewardMoney = monthReport.RewardMoney,
                YearRewardMoney = monthReport.YearRewardMoney,
                PunishNum = monthReport.PunishNum,
                YearPunishNum = monthReport.YearPunishNum,
                PunishMoney = monthReport.PunishMoney,
                YearPunishMoney = monthReport.YearPunishMoney,
                ActionPlanNum = monthReport.ActionPlanNum,
                YearActionPlanNum = monthReport.YearActionPlanNum,
                MonthSolutionNum = monthReport.MonthSolutionNum,
                YearSolutionNum = monthReport.YearSolutionNum,
                AccidentDes = monthReport.AccidentDes,
                Question = monthReport.Question,
                SubcontractManHours = monthReport.SubcontractManHours
            };

            db.Manager_MonthReportC.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerMonthCMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.MonthReportDate);
        }

        /// <summary>
        /// 修改月报告信息
        /// </summary>
        /// <param name="monthReport">月报告实体</param>
        public static void UpdateMonthReport(Model.Manager_MonthReportC monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportC newMonthReport = db.Manager_MonthReportC.First(e => e.MonthReportId == monthReport.MonthReportId);
            newMonthReport.MonthReportCode = monthReport.MonthReportCode;
            newMonthReport.ProjectId = monthReport.ProjectId;
            newMonthReport.Months = monthReport.Months;
            newMonthReport.MonthReportDate = monthReport.MonthReportDate;
            newMonthReport.ReportMan = monthReport.ReportMan;
            newMonthReport.HseManhours = monthReport.HseManhours;
            newMonthReport.TotalHseManhours = monthReport.TotalHseManhours;
            newMonthReport.SafetyManhours = monthReport.SafetyManhours;
            newMonthReport.HseActiveReview = monthReport.HseActiveReview;
            newMonthReport.HseActiveKey = monthReport.HseActiveKey;
            newMonthReport.TotalManNum = monthReport.TotalManNum;
            newMonthReport.ScanUrl = monthReport.ScanUrl;
            newMonthReport.PlanCost = monthReport.PlanCost;
            newMonthReport.RealCost = monthReport.RealCost;
            newMonthReport.TotalRealCost = monthReport.TotalRealCost;
            newMonthReport.MonthHSEDay = monthReport.MonthHSEDay;
            newMonthReport.SumHSEDay = monthReport.SumHSEDay;
            newMonthReport.MonthHSEWorkDay = monthReport.MonthHSEWorkDay;
            newMonthReport.YearHSEWorkDay = monthReport.YearHSEWorkDay;
            newMonthReport.SumHSEWorkDay = monthReport.SumHSEWorkDay;
            newMonthReport.HazardNum = monthReport.HazardNum;
            newMonthReport.YearHazardNum = monthReport.YearHazardNum;
            newMonthReport.MeetingNum = monthReport.MeetingNum;
            newMonthReport.YearMeetingNum = monthReport.YearMeetingNum;
            newMonthReport.PromotionalActiviteNum = monthReport.PromotionalActiviteNum;
            newMonthReport.YearPromotionalActiviteNum = monthReport.YearPromotionalActiviteNum;
            newMonthReport.ComplexEmergencyNum = monthReport.ComplexEmergencyNum;
            newMonthReport.YearComplexEmergencyNum = monthReport.YearComplexEmergencyNum;
            newMonthReport.SpecialEmergencyNum = monthReport.SpecialEmergencyNum;
            newMonthReport.YearSpecialEmergencyNum = monthReport.YearSpecialEmergencyNum;
            newMonthReport.DrillRecordNum = monthReport.DrillRecordNum;
            newMonthReport.YearDrillRecordNum = monthReport.YearDrillRecordNum;
            newMonthReport.LicenseNum = monthReport.LicenseNum;
            newMonthReport.YearLicenseNum = monthReport.YearLicenseNum;
            newMonthReport.EquipmentNum = monthReport.EquipmentNum;
            newMonthReport.YearEquipmentNum = monthReport.YearEquipmentNum;
            newMonthReport.LicenseRemark = monthReport.LicenseRemark;
            newMonthReport.EquipmentRemark = monthReport.EquipmentRemark;
            newMonthReport.RewardNum = monthReport.RewardNum;
            newMonthReport.YearRewardNum = monthReport.YearRewardNum;
            newMonthReport.RewardMoney = monthReport.RewardMoney;
            newMonthReport.YearRewardMoney = monthReport.YearRewardMoney;
            newMonthReport.PunishNum = monthReport.PunishNum;
            newMonthReport.YearPunishNum = monthReport.YearPunishNum;
            newMonthReport.PunishMoney = monthReport.PunishMoney;
            newMonthReport.YearPunishMoney = monthReport.YearPunishMoney;
            newMonthReport.ActionPlanNum = monthReport.ActionPlanNum;
            newMonthReport.YearActionPlanNum = monthReport.YearActionPlanNum;
            newMonthReport.MonthSolutionNum = monthReport.MonthSolutionNum;
            newMonthReport.YearSolutionNum = monthReport.YearSolutionNum;
            newMonthReport.AccidentDes = monthReport.AccidentDes;
            newMonthReport.Question = monthReport.Question;
            newMonthReport.SubcontractManHours = monthReport.SubcontractManHours;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除一个月报告信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportC monthReport = db.Manager_MonthReportC.First(e => e.MonthReportId == monthReportId);
            ///删除编码表记录
            BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
            BLL.CommonService.DeleteAttachFileById(monthReportId);//删除附件
            db.Manager_MonthReportC.DeleteOnSubmit(monthReport);
            db.SubmitChanges();
        }
    }
}
