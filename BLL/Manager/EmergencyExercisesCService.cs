using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class EmergencyExercisesCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取应急演练活动
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_EmergencyExercisesC> GetEmergencyExercisesByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_EmergencyExercisesC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加应急演练活动
        /// </summary>
        /// <param name="exercises"></param>
        public static void AddEmergencyExercises(Model.Manager_Month_EmergencyExercisesC exercises)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_EmergencyExercisesC newE = new Model.Manager_Month_EmergencyExercisesC
            {
                EmergencyExercisesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_EmergencyExercisesC)),
                MonthReportId = exercises.MonthReportId,
                DrillContent = exercises.DrillContent,
                DrillDate = exercises.DrillDate,
                UnitName = exercises.UnitName,
                ParticipantsNum = exercises.ParticipantsNum,
                SortIndex = exercises.SortIndex
            };
            db.Manager_Month_EmergencyExercisesC.InsertOnSubmit(newE);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关的应急演练活动
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteEmergencyExercisesByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_EmergencyExercisesC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_EmergencyExercisesC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
