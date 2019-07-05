using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CostStatisticService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.TC_CostStatistic> cost = from x in db.TC_CostStatistic orderby x.CostStatisticCode descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListCost(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.TC_CostStatistic> q = cost;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.CostStatisticCode,
                       x.ProjectId,
                       Months = Convert.ToDateTime(x.Months).Year + "-" + (Convert.ToInt32(Convert.ToDateTime(x.Months).Month) < 10 ? ("0" + Convert.ToDateTime(x.Months).Month).ToString() : Convert.ToDateTime(x.Months).Month.ToString()),
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId)
        {
            return count;
        }

        /// <summary>
        /// 根据费用汇总编号查询费用汇总信息
        /// </summary>
        /// <param name="checkCode">费用汇总编号</param>
        /// <returns>费用汇总信息</returns>
        public static Model.TC_CostStatistic GetCostStatisticByCostStatisticCode(string costStatisticCode)
        {
            return Funs.DB.TC_CostStatistic.FirstOrDefault(x => x.CostStatisticCode == costStatisticCode);
        }

        /// <summary>
        /// 根据月份和项目号查询费用汇总信息
        /// </summary>
        /// <param name="months">月份</param>
        /// <param name="projectId">项目号</param>
        /// <returns>费用汇总信息</returns>
        public static Model.TC_CostStatistic GetCostStatisticByMonthsAndProjectId(DateTime months, string projectId)
        {
            return Funs.DB.TC_CostStatistic.FirstOrDefault(x => x.Months == months && x.ProjectId == projectId);
        }

        /// <summary>
        /// 根据月份和项目号查询最近的一条费用汇总信息
        /// </summary>
        /// <param name="months">月份</param>
        /// <param name="projectId">项目号</param>
        /// <returns>费用汇总信息</returns>
        public static Model.TC_CostStatistic GetLastCostStatisticByMonthsAndProjectId(DateTime months, string projectId)
        {
            return (from x in Funs.DB.TC_CostStatistic where x.Months < months && x.ProjectId == projectId orderby x.Months descending select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加费用汇总信息
        /// </summary>
        /// <param name="pauseNotice">费用汇总实体</param>
        public static void AddCostStatistic(Model.TC_CostStatistic tc_CostStatistic)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.TC_CostStatistic newtc_CostStatistic = new Model.TC_CostStatistic
            {
                CostStatisticCode = tc_CostStatistic.CostStatisticCode,
                ProjectId = tc_CostStatistic.ProjectId,
                Months = tc_CostStatistic.Months,
                CompileMan = tc_CostStatistic.CompileMan,
                CompileDate = tc_CostStatistic.CompileDate
            };
            db.TC_CostStatistic.InsertOnSubmit(newtc_CostStatistic);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据费用汇总主键删除一个费用汇总信息
        /// </summary>
        /// <param name="pauseNoticeCode">费用汇总主键</param>
        public static void DeleteCostStatisticByCostStatisticCode(string costStatisticCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.TC_CostStatistic tc_CostStatistic = db.TC_CostStatistic.FirstOrDefault(e => e.CostStatisticCode == costStatisticCode);
            if (tc_CostStatistic != null)
            {
                db.TC_CostStatistic.DeleteOnSubmit(tc_CostStatistic);
                db.SubmitChanges();
            }
        }
    }
}
