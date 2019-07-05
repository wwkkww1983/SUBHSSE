using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class ManhoursSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string monthReportId)
        {
            return from x in db.Manager_ManhoursSortB
                   where x.MonthReportId == monthReportId
                   orderby x.UnitId
                   select new
                   {
                       UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                       x.PersonTotal,
                       x.ManhoursTotal,
                       x.TotalManhoursTotal,
                   };
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告人工时分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_ManhoursSortB> GetManhoursSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_ManhoursSortB where x.MonthReportId == monthReportId orderby x.UnitId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总完成人工时
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumManhoursTotalByMonthReportId(string monthReportId, string unitId)
        {
            return (from x in Funs.DB.Manager_ManhoursSortB where x.MonthReportId == monthReportId && x.UnitId == unitId select x.ManhoursTotal).Sum();
        }

        /// <summary>
        /// 根据月报告主键和单位主键获取一条记录
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_ManhoursSortB GetSumManhoursTotalByMonthReportIdAndUnitId(string monthReportId, string unitId)
        {
            return (from x in Funs.DB.Manager_ManhoursSortB where x.MonthReportId == monthReportId && x.UnitId == unitId select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加月报告人工时分类信息
        /// </summary>
        /// <param name="manhoursSort">月报告人工时分类实体</param>
        public static void AddManhoursSort(Model.Manager_ManhoursSortB manhoursSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_ManhoursSortB));
            Model.Manager_ManhoursSortB newManhoursSort = new Model.Manager_ManhoursSortB
            {
                ManhoursSortId = newKeyID,
                MonthReportId = manhoursSort.MonthReportId,
                UnitId = manhoursSort.UnitId,
                PersonTotal = manhoursSort.PersonTotal,
                ManhoursTotal = manhoursSort.ManhoursTotal,
                TotalManhoursTotal = manhoursSort.TotalManhoursTotal
            };

            db.Manager_ManhoursSortB.InsertOnSubmit(newManhoursSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告人工时分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteManhoursSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_ManhoursSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_ManhoursSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
