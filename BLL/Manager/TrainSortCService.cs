using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TrainSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_TrainSortC> GetTrainSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_TrainSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告HSE培训情况信息
        /// </summary>
        /// <param name="trainSort">月报告HSE培训情况实体</param>
        public static void AddTrainSort(Model.Manager_TrainSortC trainSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_TrainSortC));
            Model.Manager_TrainSortC newTrainSort = new Model.Manager_TrainSortC
            {
                TrainSortId = newKeyID,
                MonthReportId = trainSort.MonthReportId,
                SortIndex = trainSort.SortIndex,
                TrainContent = trainSort.TrainContent,
                TeachHour = trainSort.TeachHour,
                TeachMan = trainSort.TeachMan,
                UnitName = trainSort.UnitName,
                PersonNum = trainSort.PersonNum
            };

            db.Manager_TrainSortC.InsertOnSubmit(newTrainSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE培训情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteTrainSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_TrainSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_TrainSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
