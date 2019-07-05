using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HazardCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取危险源动态识别及控制
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_HazardC> GetHazardByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_HazardC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加危险源动态识别及控制
        /// </summary>
        /// <param name="hazard"></param>
        public static void AddHazard(Model.Manager_Month_HazardC hazard)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_HazardC newHazard = new Model.Manager_Month_HazardC
            {
                HazardId = SQLHelper.GetNewID(typeof(Model.Manager_Month_HazardC)),
                MonthReportId = hazard.MonthReportId,
                WorkArea = hazard.WorkArea,
                Subcontractor = hazard.Subcontractor,
                DangerousSource = hazard.DangerousSource,
                ControlMeasures = hazard.ControlMeasures,
                SortIndex = hazard.SortIndex
            };
            db.Manager_Month_HazardC.InsertOnSubmit(newHazard);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关危险源动态识别及控制
        /// </summary>
        /// <param name="monthReportId">月报Id</param>
        public static void DeleteHazardByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_HazardC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_HazardC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
