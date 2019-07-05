using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class EmergencySortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告应急预案信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_EmergencySortC> GetEmergencySortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_EmergencySortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告应急预案信息
        /// </summary>
        /// <param name="checkSort">月报告应急预案实体</param>
        public static void AddEmergencySort(Model.Manager_EmergencySortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_EmergencySortC));
            Model.Manager_EmergencySortC newEmergencySort = new Model.Manager_EmergencySortC
            {
                EmergencySortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                EmergencyName = checkSort.EmergencyName,
                ModefyPerson = checkSort.ModefyPerson,
                ReleaseDate = checkSort.ReleaseDate,
                StateRef = checkSort.StateRef
            };

            db.Manager_EmergencySortC.InsertOnSubmit(newEmergencySort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告应急预案信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteEmergencySortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_EmergencySortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_EmergencySortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
