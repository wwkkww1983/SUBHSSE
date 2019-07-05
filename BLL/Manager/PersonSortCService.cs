using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PersonSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE人力投入信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_PersonSortC> GetPersonSortsByMonthReportId(string monthReportId, string projectId)
        {
            var q = (from x in Funs.DB.Manager_PersonSortC
                     join y in Funs.DB.Project_ProjectUnit
                     on x.UnitId equals y.UnitId
                     where x.MonthReportId == monthReportId && y.ProjectId == projectId
                     orderby y.UnitType
                     select x).Distinct().ToList();
            return q;
        }

        /// <summary>
        /// 增加月报告HSE人力投入信息
        /// </summary>
        /// <param name="checkSort">月报告HSE人力投入实体</param>
        public static void AddPersonSort(Model.Manager_PersonSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_PersonSortC));
            Model.Manager_PersonSortC newPersonSort = new Model.Manager_PersonSortC
            {
                PersonSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                UnitId = checkSort.UnitId,
                SumPersonNum = checkSort.SumPersonNum,
                HSEPersonNum = checkSort.HSEPersonNum,
                ContractRange = checkSort.ContractRange,
                Remark = checkSort.Remark
            };

            db.Manager_PersonSortC.InsertOnSubmit(newPersonSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE人力投入信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeletePersonSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_PersonSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_PersonSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
