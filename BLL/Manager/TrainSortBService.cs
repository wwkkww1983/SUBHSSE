using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TrainSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_TrainSortB> GetTrainSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_TrainSortB where x.MonthReportId == monthReportId select x).ToList();
        }

        public static Model.Manager_TrainSortB GetTrainSortByMonthReportIdAndTrainType(string monthReportId, string trainType)
        {
            return (from x in Funs.DB.Manager_TrainSortB where x.MonthReportId == monthReportId && x.TrainType == trainType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取总培训次数
        /// </summary>
        /// <param name="trainType">培训类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumTrainNumberByMonthReportId(string monthReportId, string trainType)
        {
            return (from x in Funs.DB.Manager_TrainSortB where x.MonthReportId == monthReportId && x.TrainType == trainType select x.TrainNumber).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总培训人数
        /// </summary>
        /// <param name="trainType">培训类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumTrainPersonNumberByMonthReportId(string monthReportId, string trainType)
        {
            return (from x in Funs.DB.Manager_TrainSortB where x.MonthReportId == monthReportId && x.TrainType == trainType select x.TrainPersonNumber).Sum();
        }

        /// <summary>
        /// 增加月报告HSE培训情况信息
        /// </summary>
        /// <param name="trainSort">月报告HSE培训情况实体</param>
        public static void AddTrainSort(Model.Manager_TrainSortB trainSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_TrainSortB));
            Model.Manager_TrainSortB newTrainSort = new Model.Manager_TrainSortB
            {
                TrainSortId = newKeyID,
                MonthReportId = trainSort.MonthReportId,
                TrainType = trainSort.TrainType,
                TrainNumber = trainSort.TrainNumber,
                TotalTrainNum = trainSort.TotalTrainNum,
                TrainPersonNumber = trainSort.TrainPersonNumber,
                TotalTrainPersonNum = trainSort.TotalTrainPersonNum
            };

            db.Manager_TrainSortB.InsertOnSubmit(newTrainSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteTrainSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_TrainSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_TrainSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
