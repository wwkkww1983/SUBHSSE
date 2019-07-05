using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AccidentSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 增加月报告事故分类信息
        /// </summary>
        /// <param name="accidentSort">月报告事故分类实体</param>
        public static void AddAccidentSort(Model.Manager_AccidentSort accidentSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_AccidentSort));
            Model.Manager_AccidentSort newAccidentSort = new Model.Manager_AccidentSort
            {
                AccidentSortId = newKeyID,
                MonthReportId = accidentSort.MonthReportId,
                AccidentTypeId = accidentSort.AccidentTypeId,
                AccidentNumber01 = accidentSort.AccidentNumber01,
                AccidentNumber02 = accidentSort.AccidentNumber02
            };
            db.Manager_AccidentSort.InsertOnSubmit(newAccidentSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteAccidentSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_AccidentSort where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_AccidentSort.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
