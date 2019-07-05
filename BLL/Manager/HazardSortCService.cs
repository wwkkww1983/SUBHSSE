using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HazardSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告危险源信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_HazardSortC> GetHazardSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_HazardSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告危险源信息
        /// </summary>
        /// <param name="checkSort">月报告危险源实体</param>
        public static void AddHazardSort(Model.Manager_HazardSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HazardSortC));
            Model.Manager_HazardSortC newHazardSort = new Model.Manager_HazardSortC
            {
                HazardSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                HazardName = checkSort.HazardName,
                UnitAndArea = checkSort.UnitAndArea,
                StationDef = checkSort.StationDef,
                HandleWay = checkSort.HandleWay
            };

            db.Manager_HazardSortC.InsertOnSubmit(newHazardSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告危险源信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteHazardSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_HazardSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_HazardSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
