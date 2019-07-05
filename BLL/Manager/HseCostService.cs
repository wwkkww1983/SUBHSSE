using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HseCostService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 增加月报告HSE检查情况信息
        /// </summary>
        /// <param name="hseSort">月报告HSE检查情况实体</param>
        public static void AddHseSort(Model.Manager_HseCost hseSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HseCost));
            Model.Manager_HseCost newHseSort = new Model.Manager_HseCost
            {
                HseCostId = newKeyID,
                MonthReportId = hseSort.MonthReportId,
                HseNumber01 = hseSort.HseNumber01,
                HseNumber02 = hseSort.HseNumber02,
                HseNumber03 = hseSort.HseNumber03,
                HseNumber04 = hseSort.HseNumber04,
                HseNumber05 = hseSort.HseNumber05,
                HseNumber06 = hseSort.HseNumber06,
                HseNumber07 = hseSort.HseNumber07,
                HseNumber08 = hseSort.HseNumber08,
                HseNumber09 = hseSort.HseNumber09,
                HseNumber00 = hseSort.HseNumber00,

                HseNumber10 = hseSort.HseNumber10,
                HseNumber11 = hseSort.HseNumber11,
                SpecialNumber = hseSort.SpecialNumber
            };
            db.Manager_HseCost.InsertOnSubmit(newHseSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteHseSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_HseCost where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_HseCost.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
