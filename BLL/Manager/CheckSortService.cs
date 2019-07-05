using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CheckSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 增加月报告HSE检查情况信息
        /// </summary>
        /// <param name="checkSort">月报告HSE检查情况实体</param>
        public static void AddCheckSort(Model.Manager_CheckSort checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_CheckSort));
            Model.Manager_CheckSort newCheckSort = new Model.Manager_CheckSort
            {
                CheckSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                CheckNumber01 = checkSort.CheckNumber01,
                CheckNumber02 = checkSort.CheckNumber02,
                CheckNumber03 = checkSort.CheckNumber03,
                CheckNumber04 = checkSort.CheckNumber04,
                CheckNumber11 = checkSort.CheckNumber11,
                CheckNumber12 = checkSort.CheckNumber12,
                CheckNumber13 = checkSort.CheckNumber13,
                CheckNumber14 = checkSort.CheckNumber14,
                CheckNumber21 = checkSort.CheckNumber21,
                CheckNumber22 = checkSort.CheckNumber22,
                CheckNumber23 = checkSort.CheckNumber23,
                CheckNumber24 = checkSort.CheckNumber24,
                CheckNumber31 = checkSort.CheckNumber31,
                CheckNumber32 = checkSort.CheckNumber32,
                CheckNumber33 = checkSort.CheckNumber33,
                CheckNumber34 = checkSort.CheckNumber34
            };

            db.Manager_CheckSort.InsertOnSubmit(newCheckSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteCheckSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_CheckSort where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_CheckSort.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
