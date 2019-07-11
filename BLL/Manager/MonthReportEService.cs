using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MonthReportEService
    {
        /// <summary>
        /// 根据时间获取最近时间的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetMonthReportByDate(DateTime date, string projectId)
        {
            var a = from x in Funs.DB.Manager_MonthReportE where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 获取项目时间段内的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportE> GetMonthReportsByStartAndEndTimeAndProjectId(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportE where x.Months >= startTime && x.Months < endTime && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 获取所有项目时间段内的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportE> GetMonthReportsByStartAndEndTime(DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.Manager_MonthReportE where x.Months >= startTime && x.Months < endTime select x).ToList();
        }

        /// <summary>
        /// 获取所有项目当月的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static List<Model.Manager_MonthReportE> GetMonthReportsByMonths(DateTime months)
        {
            return (from x in Funs.DB.Manager_MonthReportE where x.Months == months select x).ToList();
        }

        /// <summary>
        /// 根据时间获取最近时间的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool GetFreezeMonthReportByDate(DateTime date, string projectId, int day)
        {
            var a = from x in Funs.DB.Manager_MonthReportE where x.ProjectId == projectId && (x.Months.Value.Year.ToString() + x.Months.Value.AddMonths(1).Month.ToString()).Contains(date.Year.ToString() + date.Month.ToString()) && date.Day < (day + 1) select x;
            return a.Count() > 0;
        }

        /// <summary>
        /// 根据日期获得海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="month">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportE GetMonthReportByMonth(DateTime month, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportE where x.Months == month && x.ProjectId == projectId select x).FirstOrDefault();
        }

        ///// <summary>
        ///// 根据日期获得最近的一条海外工程项目月度HSSE统计表告信息
        ///// </summary>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public static Model.Manager_MonthReportE GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        //{
        //    Model.Manager_MonthReportE LastMonth = null;
        //    if (date.Day < freezeDay)
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportE where x.Months <= date.AddMonths(-2) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    else
        //    {
        //        LastMonth = (from x in Funs.DB.Manager_MonthReportE where x.Months < date.AddMonths(-1) && x.ProjectId == projectId orderby x.MonthReportDate descending select x).FirstOrDefault();
        //    }
        //    return LastMonth;
        //}

        /// <summary>
        /// 根据日期获得最近的一条海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static Model.Manager_MonthReportE GetLastMonthReportByDate(DateTime date, int freezeDay, string projectId)
        {
            Model.Manager_MonthReportE LastMonth = null;
            DateTime? monthDate = null;

            var q = from x in Funs.DB.Manager_MonthReportE where x.ProjectId == projectId && x.Months < date orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                if (date.Day <= freezeDay)
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
        /// 根据海外工程项目月度HSSE统计表告主键获取海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="monthReportId">海外工程项目月度HSSE统计表告主键</param>
        /// <returns>海外工程项目月度HSSE统计表告信息</returns>
        public static Model.Manager_MonthReportE GetMonthReportByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MonthReportE.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 根据海外工程项目月度HSSE统计表告编号获取海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="monthReportCode">海外工程项目月度HSSE统计表告编号</param>
        /// <returns>海外工程项目月度HSSE统计表告信息</returns>
        public static Model.Manager_MonthReportE GetMonthReportByMonthReportCode(string monthReportCode)
        {
            return Funs.DB.Manager_MonthReportE.FirstOrDefault(x => x.MonthReportCode == monthReportCode);
        }

        /// <summary>
        /// 增加海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="monthReport">海外工程项目月度HSSE统计表告实体</param>
        public static void AddMonthReport(Model.Manager_MonthReportE monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportE));
            Model.Manager_MonthReportE newMonthReport = new Model.Manager_MonthReportE
            {
                MonthReportId = monthReport.MonthReportId,
                MonthReportCode = monthReport.MonthReportCode,
                ProjectId = monthReport.ProjectId,
                Months = monthReport.Months,
                MonthReportDate = monthReport.MonthReportDate,
                ReportMan = monthReport.ReportMan,
                CountryCities = monthReport.CountryCities,
                StartEndDate = monthReport.StartEndDate,
                ContractType = monthReport.ContractType,
                ContractAmount = monthReport.ContractAmount,
                ThisMajorWork = monthReport.ThisMajorWork,
                NextMajorWork = monthReport.NextMajorWork,
                ThisIncome = monthReport.ThisIncome,
                YearIncome = monthReport.YearIncome,
                TotalIncome = monthReport.TotalIncome,
                ThisImageProgress = monthReport.ThisImageProgress,
                YearImageProgress = monthReport.YearImageProgress,
                TotalImageProgress = monthReport.TotalImageProgress,
                ThisPersonNum = monthReport.ThisPersonNum,
                YearPersonNum = monthReport.YearPersonNum,
                TotalPersonNum = monthReport.TotalPersonNum,
                ThisForeignPersonNum = monthReport.ThisForeignPersonNum,
                YearForeignPersonNum = monthReport.YearForeignPersonNum,
                TotalForeignPersonNum = monthReport.TotalForeignPersonNum,
                ThisTrainPersonNum = monthReport.ThisTrainPersonNum,
                YearTrainPersonNum = monthReport.YearTrainPersonNum,
                TotalTrainPersonNum = monthReport.TotalTrainPersonNum,
                ThisCheckNum = monthReport.ThisCheckNum,
                YearCheckNum = monthReport.YearCheckNum,
                TotalCheckNum = monthReport.TotalCheckNum,
                ThisViolationNum = monthReport.ThisViolationNum,
                YearViolationNum = monthReport.YearViolationNum,
                TotalViolationNum = monthReport.TotalViolationNum,
                ThisInvestment = monthReport.ThisInvestment,
                YearInvestment = monthReport.YearInvestment,
                TotalInvestment = monthReport.TotalInvestment,
                ThisReward = monthReport.ThisReward,
                YearReward = monthReport.YearReward,
                TotalReward = monthReport.TotalReward,
                ThisPunish = monthReport.ThisPunish,
                YearPunish = monthReport.YearPunish,
                TotalPunish = monthReport.TotalPunish,
                ThisEmergencyDrillNum = monthReport.ThisEmergencyDrillNum,
                YearEmergencyDrillNum = monthReport.YearEmergencyDrillNum,
                TotalEmergencyDrillNum = monthReport.TotalEmergencyDrillNum,
                ThisHSEManhours = monthReport.ThisHSEManhours,
                YearHSEManhours = monthReport.YearHSEManhours,
                TotalHSEManhours = monthReport.TotalHSEManhours,
                ThisRecordEvent = monthReport.ThisRecordEvent,
                YearRecordEvent = monthReport.YearRecordEvent,
                TotalRecordEvent = monthReport.TotalRecordEvent,
                ThisNoRecordEvent = monthReport.ThisNoRecordEvent,
                YearNoRecordEvent = monthReport.YearNoRecordEvent,
                TotalNoRecordEvent = monthReport.TotalNoRecordEvent,
                ProjectManagerName = monthReport.ProjectManagerName,
                ProjectManagerPhone = monthReport.ProjectManagerPhone,
                HSEManagerName = monthReport.HSEManagerName,
                HSEManagerPhone = monthReport.HSEManagerPhone,
            };

            db.Manager_MonthReportE.InsertOnSubmit(newMonthReport);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerMonthEMenuId, monthReport.ProjectId, null, monthReport.MonthReportId, monthReport.MonthReportDate);
        }

        /// <summary>
        /// 修改海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="monthReport">海外工程项目月度HSSE统计表告实体</param>
        public static void UpdateMonthReport(Model.Manager_MonthReportE monthReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportE newMonthReport = db.Manager_MonthReportE.First(e => e.MonthReportId == monthReport.MonthReportId);
            newMonthReport.MonthReportCode = monthReport.MonthReportCode;
            newMonthReport.ProjectId = monthReport.ProjectId;
            newMonthReport.Months = monthReport.Months;
            newMonthReport.MonthReportDate = monthReport.MonthReportDate;
            newMonthReport.ReportMan = monthReport.ReportMan;
            newMonthReport.CountryCities = monthReport.CountryCities;
            newMonthReport.StartEndDate = monthReport.StartEndDate;
            newMonthReport.ContractType = monthReport.ContractType;
            newMonthReport.ContractAmount = monthReport.ContractAmount;
            newMonthReport.ThisMajorWork = monthReport.ThisMajorWork;
            newMonthReport.NextMajorWork = monthReport.NextMajorWork;
            newMonthReport.ThisIncome = monthReport.ThisIncome;
            newMonthReport.YearIncome = monthReport.YearIncome;
            newMonthReport.TotalIncome = monthReport.TotalIncome;
            newMonthReport.ThisImageProgress = monthReport.ThisImageProgress;
            newMonthReport.YearImageProgress = monthReport.YearImageProgress;
            newMonthReport.TotalImageProgress = monthReport.TotalImageProgress;
            newMonthReport.ThisPersonNum = monthReport.ThisPersonNum;
            newMonthReport.YearPersonNum = monthReport.YearPersonNum;
            newMonthReport.TotalPersonNum = monthReport.TotalPersonNum;
            newMonthReport.ThisForeignPersonNum = monthReport.ThisForeignPersonNum;
            newMonthReport.YearForeignPersonNum = monthReport.YearForeignPersonNum;
            newMonthReport.TotalForeignPersonNum = monthReport.TotalForeignPersonNum;
            newMonthReport.ThisTrainPersonNum = monthReport.ThisTrainPersonNum;
            newMonthReport.YearTrainPersonNum = monthReport.YearTrainPersonNum;
            newMonthReport.TotalTrainPersonNum = monthReport.TotalTrainPersonNum;
            newMonthReport.ThisCheckNum = monthReport.ThisCheckNum;
            newMonthReport.YearCheckNum = monthReport.YearCheckNum;
            newMonthReport.TotalCheckNum = monthReport.TotalCheckNum;
            newMonthReport.ThisViolationNum = monthReport.ThisViolationNum;
            newMonthReport.YearViolationNum = monthReport.YearViolationNum;
            newMonthReport.TotalViolationNum = monthReport.TotalViolationNum;
            newMonthReport.ThisInvestment = monthReport.ThisInvestment;
            newMonthReport.YearInvestment = monthReport.YearInvestment;
            newMonthReport.TotalInvestment = monthReport.TotalInvestment;
            newMonthReport.ThisReward = monthReport.ThisReward;
            newMonthReport.YearReward = monthReport.YearReward;
            newMonthReport.TotalReward = monthReport.TotalReward;
            newMonthReport.ThisPunish = monthReport.ThisPunish;
            newMonthReport.YearPunish = monthReport.YearPunish;
            newMonthReport.TotalPunish = monthReport.TotalPunish;
            newMonthReport.ThisEmergencyDrillNum = monthReport.ThisEmergencyDrillNum;
            newMonthReport.YearEmergencyDrillNum = monthReport.YearEmergencyDrillNum;
            newMonthReport.TotalEmergencyDrillNum = monthReport.TotalEmergencyDrillNum;
            newMonthReport.ThisHSEManhours = monthReport.ThisHSEManhours;
            newMonthReport.YearHSEManhours = monthReport.YearHSEManhours;
            newMonthReport.TotalHSEManhours = monthReport.TotalHSEManhours;
            newMonthReport.ThisRecordEvent = monthReport.ThisRecordEvent;
            newMonthReport.YearRecordEvent = monthReport.YearRecordEvent;
            newMonthReport.TotalRecordEvent = monthReport.TotalRecordEvent;
            newMonthReport.ThisNoRecordEvent = monthReport.ThisNoRecordEvent;
            newMonthReport.YearNoRecordEvent = monthReport.YearNoRecordEvent;
            newMonthReport.TotalNoRecordEvent = monthReport.TotalNoRecordEvent;
            newMonthReport.ProjectManagerName = monthReport.ProjectManagerName;
            newMonthReport.ProjectManagerPhone = monthReport.ProjectManagerPhone;
            newMonthReport.HSEManagerName = monthReport.HSEManagerName;
            newMonthReport.HSEManagerPhone = monthReport.HSEManagerPhone;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据海外工程项目月度HSSE统计表告主键删除一个海外工程项目月度HSSE统计表告信息
        /// </summary>
        /// <param name="monthReportId">海外工程项目月度HSSE统计表告主键</param>
        public static void DeleteMonthReportByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_MonthReportE monthReport = db.Manager_MonthReportE.First(e => e.MonthReportId == monthReportId);
            ///删除编码表记录
            BLL.CodeRecordsService.DeleteCodeRecordsByDataId(monthReportId);
            BLL.CommonService.DeleteAttachFileById(monthReportId);//删除附件
            db.Manager_MonthReportE.DeleteOnSubmit(monthReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取项目当月的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="months"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Manager_MonthReportE GetMonthReportsByMonthsAndProjectId(DateTime months, string projectId)
        {
            return (from x in Funs.DB.Manager_MonthReportE where x.Months == months && x.ProjectId == projectId select x).FirstOrDefault();
        }

        /// <summary>
        /// 获取项目最近的的海外工程项目月度HSSE统计表
        /// </summary>
        /// <param name="months"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.Manager_MonthReportE GetLateMonthReportByMonths(DateTime months, string projectId)
        {
            var q = from x in Funs.DB.Manager_MonthReportE where x.Months < months && x.ProjectId == projectId orderby x.Months descending select x;
            if (q.Count() > 0)
            {
                return q.First();
            }
            else
            {
                return null;
            }
        }
    }
}
