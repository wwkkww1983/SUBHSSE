using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 月度计划
    /// </summary>
    public static class MonthPlanService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取月度计划信息
        /// </summary>
        /// <param name="MonthPlanId"></param>
        /// <returns></returns>
        public static Model.SiteConstruction_MonthPlan GetMonthPlanById(string MonthPlanId)
        {
            return Funs.DB.SiteConstruction_MonthPlan.FirstOrDefault(e => e.MonthPlanId == MonthPlanId);
        }

        /// <summary>
        /// 根据主键获取月度计划信息
        /// </summary>
        /// <param name="MonthPlanId"></param>
        /// <returns></returns>
        public static List<Model.SiteConstruction_MonthPlan> GetMonthPlanListByProjectId(string projectId)
        {
            return (from x in Funs.DB.SiteConstruction_MonthPlan where x.ProjectId == projectId orderby x.CompileDate descending select x).ToList();
        }

        /// <summary>
        /// 添加月度计划
        /// </summary>
        /// <param name="MonthPlan"></param>
        public static void AddMonthPlan(Model.SiteConstruction_MonthPlan MonthPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_MonthPlan newMonthPlan = new Model.SiteConstruction_MonthPlan
            {
                MonthPlanId = MonthPlan.MonthPlanId,
                ProjectId = MonthPlan.ProjectId,
                UnitId = MonthPlan.UnitId,
                Months = MonthPlan.Months,
                CompileMan = MonthPlan.CompileMan,
                CompileDate = MonthPlan.CompileDate,
                JobContent = MonthPlan.JobContent,
                AttachUrl = MonthPlan.AttachUrl,
                SeeFile = MonthPlan.SeeFile,
                States = MonthPlan.States
            };

            db.SiteConstruction_MonthPlan.InsertOnSubmit(newMonthPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="MonthPlan"></param>
        public static void UpdateMonthPlan(Model.SiteConstruction_MonthPlan MonthPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_MonthPlan newMonthPlan = db.SiteConstruction_MonthPlan.FirstOrDefault(e => e.MonthPlanId == MonthPlan.MonthPlanId);
            if (newMonthPlan != null)
            {
                newMonthPlan.CompileDate = MonthPlan.CompileDate;
                newMonthPlan.JobContent = MonthPlan.JobContent;
                newMonthPlan.AttachUrl = MonthPlan.AttachUrl;
                newMonthPlan.SeeFile = MonthPlan.SeeFile;
                newMonthPlan.States = MonthPlan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="MonthPlanId"></param>
        public static void DeleteMonthPlanById(string MonthPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_MonthPlan MonthPlan = db.SiteConstruction_MonthPlan.FirstOrDefault(e => e.MonthPlanId == MonthPlanId);
            if (MonthPlan != null)
            {
                if (!string.IsNullOrEmpty(MonthPlan.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, MonthPlan.AttachUrl);//删除附件
                }
                BLL.CommonService.DeleteFlowOperateByID(MonthPlanId); 
                db.SiteConstruction_MonthPlan.DeleteOnSubmit(MonthPlan);
                db.SubmitChanges();
            }
        }
    }
}
