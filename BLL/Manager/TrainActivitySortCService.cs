using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TrainActivitySortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE培训活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_TrainActivitySortC> GetTrainActivitySortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_TrainActivitySortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告HSE培训活动信息
        /// </summary>
        /// <param name="trainSort">月报告HSE培训活动实体</param>
        public static void AddTrainActivitySort(Model.Manager_TrainActivitySortC trainSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_TrainActivitySortC));
            Model.Manager_TrainActivitySortC newTrainActivitySort = new Model.Manager_TrainActivitySortC
            {
                TrainActivitySortId = newKeyID,
                MonthReportId = trainSort.MonthReportId,
                SortIndex = trainSort.SortIndex,
                ActivityName = trainSort.ActivityName,
                TrainDate = trainSort.TrainDate,
                TrainEffect = trainSort.TrainEffect
            };

            db.Manager_TrainActivitySortC.InsertOnSubmit(newTrainActivitySort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE培训活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteTrainActivitySortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_TrainActivitySortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_TrainActivitySortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
