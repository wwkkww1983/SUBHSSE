using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PromotionalActiviteSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE宣传活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_PromotionalActiviteSortC> GetPromotionalActiviteSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_PromotionalActiviteSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告HSE宣传活动信息
        /// </summary>
        /// <param name="checkSort">月报告HSE宣传活动实体</param>
        public static void AddPromotionalActiviteSort(Model.Manager_PromotionalActiviteSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_PromotionalActiviteSortC));
            Model.Manager_PromotionalActiviteSortC newPromotionalActiviteSort = new Model.Manager_PromotionalActiviteSortC
            {
                PromotionalActiviteSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                PromotionalActivitiesName = checkSort.PromotionalActivitiesName,
                CompileDate = checkSort.CompileDate,
                ParticipatingUnits = checkSort.ParticipatingUnits,
                Remark = checkSort.Remark
            };

            db.Manager_PromotionalActiviteSortC.InsertOnSubmit(newPromotionalActiviteSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE宣传活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeletePromotionalActiviteSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_PromotionalActiviteSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_PromotionalActiviteSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
