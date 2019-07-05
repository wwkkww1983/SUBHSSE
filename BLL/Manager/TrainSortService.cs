using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TrainSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_TrainSort GetTrainSortsByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_TrainSort.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }

        /// <summary>
        /// 增加月报告HSE培训情况信息
        /// </summary>
        /// <param name="trainSort">月报告HSE培训情况实体</param>
        public static void AddTrainSort(Model.Manager_TrainSort trainSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort));
            Model.Manager_TrainSort newTrainSort = new Model.Manager_TrainSort
            {
                TrainSortId = newKeyID,
                MonthReportId = trainSort.MonthReportId,
                TrainTypeName = trainSort.TrainTypeName,
                TrainNumber11 = trainSort.TrainNumber11,
                TrainNumber12 = trainSort.TrainNumber12,
                TrainNumber13 = trainSort.TrainNumber13,
                TrainNumber14 = trainSort.TrainNumber14
            };
            db.Manager_TrainSort.InsertOnSubmit(newTrainSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteTrainSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_TrainSort where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_TrainSort.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
