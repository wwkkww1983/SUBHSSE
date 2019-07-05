using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AccidentDetailSortBService
    {
        /// <summary>
        /// 根据月报告主键获取所有事故台账信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_AccidentDetailSortB> GetAccidentDetailSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_AccidentDetailSortB where x.MonthReportId == monthReportId orderby x.AccidentDate select x).ToList();
        }

        /// <summary>
        /// 增加月报告事故台账信息
        /// </summary>
        /// <param name="accidentSort">月报告事故台账实体</param>
        public static void AddAccidentDetailSort(Model.Manager_AccidentDetailSortB accidentSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_AccidentDetailSortB));
            Model.Manager_AccidentDetailSortB newAccidentSort = new Model.Manager_AccidentDetailSortB
            {
                AccidentDetailSortId = newKeyID,
                MonthReportId = accidentSort.MonthReportId,
                Abstract = accidentSort.Abstract,
                AccidentType = accidentSort.AccidentType,
                PeopleNum = accidentSort.PeopleNum,
                WorkingHoursLoss = accidentSort.WorkingHoursLoss,
                EconomicLoss = accidentSort.EconomicLoss,
                AccidentDate = accidentSort.AccidentDate
            };
            db.Manager_AccidentDetailSortB.InsertOnSubmit(newAccidentSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告事故台账信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteAccidentDetailSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_AccidentDetailSortB where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_AccidentDetailSortB.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故台账信息
        /// </summary>
        /// <param name="monthReportId">主键</param>
        public static void DeleteAccidentDetailSortByAccidentDetailSortId(string accidentDetailSortId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_AccidentDetailSortB where x.AccidentDetailSortId == accidentDetailSortId select x).FirstOrDefault();
            db.Manager_AccidentDetailSortB.DeleteOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
