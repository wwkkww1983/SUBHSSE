using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DrillSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告应急演练信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_DrillSortC> GetDrillSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_DrillSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告应急演练信息
        /// </summary>
        /// <param name="checkSort">月报告应急演练实体</param>
        public static void AddDrillSort(Model.Manager_DrillSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_DrillSortC));
            Model.Manager_DrillSortC newDrillSort = new Model.Manager_DrillSortC
            {
                DrillSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                DrillContent = checkSort.DrillContent,
                DrillDate = checkSort.DrillDate,
                JointUnit = checkSort.JointUnit,
                JoinPerson = checkSort.JoinPerson
            };

            db.Manager_DrillSortC.InsertOnSubmit(newDrillSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告应急演练信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteDrillSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_DrillSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_DrillSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
