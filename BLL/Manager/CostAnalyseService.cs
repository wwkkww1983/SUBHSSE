using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CostAnalyseService
    {
        /// <summary>
        /// 获取某一人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="costAnalyseId"></param>
        /// <returns></returns>
        public static Model.Manager_CostAnalyse getCostAnalyseByCostAnalyseId(string analyseId)
        {
            return Funs.DB.Manager_CostAnalyse.FirstOrDefault(e => e.AnalyseId == analyseId);
        }

        /// <summary>
        /// 获取某一人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="months">月份</param>
        /// <returns></returns>
        public static Model.Manager_CostAnalyse getCostAnalyseByMonths(DateTime? months, string projectId)
        {
            return Funs.DB.Manager_CostAnalyse.FirstOrDefault(e => e.Months == months && e.ProjectId == projectId);
        }

        /// <summary>
        /// 添加人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="costAnalyseName"></param>
        /// <param name="def"></param>
        public static void AddCostAnalyse(Model.Manager_CostAnalyse costAnalyse)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_CostAnalyse));
            Model.Manager_CostAnalyse newCostAnalyse = new Model.Manager_CostAnalyse
            {
                AnalyseId = newKeyID,
                ProjectId = costAnalyse.ProjectId,
                Months = costAnalyse.Months,
                TotalRealCostMoney = costAnalyse.TotalRealCostMoney,
                Manhours = costAnalyse.Manhours,
                Analyse = costAnalyse.Analyse
            };

            db.Manager_CostAnalyse.InsertOnSubmit(newCostAnalyse);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="costAnalyseId"></param>
        /// <param name="costAnalyseName"></param>
        /// <param name="def"></param>
        public static void UpdateCostAnalyse(Model.Manager_CostAnalyse costAnalyse)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_CostAnalyse newCostAnalyse = db.Manager_CostAnalyse.First(e => e.AnalyseId == costAnalyse.AnalyseId);
            newCostAnalyse.Months = costAnalyse.Months;
            newCostAnalyse.Analyse = costAnalyse.Analyse;
            newCostAnalyse.TotalRealCostMoney = costAnalyse.TotalRealCostMoney;
            newCostAnalyse.Manhours = costAnalyse.Manhours;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="costAnalyseId"></param>
        public static void DeleteCostAnalyse(string analyseId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_CostAnalyse costAnalyse = db.Manager_CostAnalyse.First(e => e.AnalyseId == analyseId);
            db.Manager_CostAnalyse.DeleteOnSubmit(costAnalyse);
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除人工时和投入安全费用比率信息
        /// </summary>
        /// <param name="months"></param>
        public static void DeleteCostAnalyseByMonths(DateTime? months)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_CostAnalyse costAnalyse = db.Manager_CostAnalyse.First(e => e.Months == months);
            db.Manager_CostAnalyse.DeleteOnSubmit(costAnalyse);
            db.SubmitChanges();
        }
    }
}
