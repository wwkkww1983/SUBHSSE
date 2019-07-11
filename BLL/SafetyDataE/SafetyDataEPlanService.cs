using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataEPlanService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        #region 根据项目id获取企业安全管理资料计划列表
        /// <summary>
        /// 根据项目id获取企业安全管理资料计划列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.SafetyDataE_SafetyDataEPlan> GetSafetyDataEPlanList(string projectId)
        {
            var SafetyDataEPlanList = from x in Funs.DB.SafetyDataE_SafetyDataEPlan
                                     join y in Funs.DB.SafetyDataE_SafetyDataE on x.SafetyDataEId equals y.SafetyDataEId
                                     where x.ProjectId == projectId
                                     orderby y.Code
                                     select x;
            return SafetyDataEPlanList.ToList();
        }
        #endregion

        #region 根据主键id获取企业安全管理资料
        /// <summary>
        /// 根据主键id获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyDataE_SafetyDataEPlan GetSafetyDataEPlanBySafetyDataEPlanId(string SafetyDataEPlanId)
        {
            return Funs.DB.SafetyDataE_SafetyDataEPlan.FirstOrDefault(x => x.SafetyDataEPlanId == SafetyDataEPlanId);
        }
        #endregion

        /// <summary>
        /// 根据考核主键id项目ID获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyDataE_SafetyDataEPlan GetSafetyDataEPlanBySafetyDataEIdProjectId(string SafetyDataEId, string ProjectId)
        {
            return Funs.DB.SafetyDataE_SafetyDataEPlan.FirstOrDefault(x => x.SafetyDataEId == SafetyDataEId && x.ProjectId == ProjectId);
        }

        #region 增、删、改企业安全管理资料计划总表
        /// <summary>
        /// 添加企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlan"></param>
        public static void AddSafetyDataEPlan(Model.SafetyDataE_SafetyDataEPlan SafetyDataEPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEPlan newSafetyDataEPlan = new Model.SafetyDataE_SafetyDataEPlan
            {
                SafetyDataEPlanId = SafetyDataEPlan.SafetyDataEPlanId,
                ProjectId = SafetyDataEPlan.ProjectId,
                SafetyDataEId = SafetyDataEPlan.SafetyDataEId,
                CheckDate = SafetyDataEPlan.CheckDate,              
                Score = SafetyDataEPlan.Score,
                ShouldScore = SafetyDataEPlan.ShouldScore,
                Remark = SafetyDataEPlan.Remark,
                ReminderDate= SafetyDataEPlan.ReminderDate,
                IsManual=SafetyDataEPlan.IsManual,
                States = SafetyDataEPlan.States,
            };
            db.SafetyDataE_SafetyDataEPlan.InsertOnSubmit(newSafetyDataEPlan);
            db.SubmitChanges();           
        }

        /// <summary>
        /// 修改企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlan"></param>
        public static void UpdateSafetyDataEPlan(Model.SafetyDataE_SafetyDataEPlan SafetyDataEPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEPlan newSafetyDataEPlan = db.SafetyDataE_SafetyDataEPlan.FirstOrDefault(e => e.SafetyDataEPlanId == SafetyDataEPlan.SafetyDataEPlanId);
            if (newSafetyDataEPlan != null)
            {
                newSafetyDataEPlan.CheckDate = SafetyDataEPlan.CheckDate;              
                newSafetyDataEPlan.Score = SafetyDataEPlan.Score;
                newSafetyDataEPlan.Remark = SafetyDataEPlan.Remark;
                newSafetyDataEPlan.ReminderDate = SafetyDataEPlan.ReminderDate;
                newSafetyDataEPlan.States = SafetyDataEPlan.States;
                db.SubmitChanges();

                ///当前计划项 没有提交时间时
                if (newSafetyDataEPlan.States== BLL.Const.State_2 && !newSafetyDataEPlan.RealScore.HasValue)
                {
                    GetSafetyDataEPlanRealScore(newSafetyDataEPlan);
                }
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlanId"></param>
        public static void DeleteSafetyDataEPlanByID(string SafetyDataEPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEPlan SafetyDataEPlan = db.SafetyDataE_SafetyDataEPlan.FirstOrDefault(e => e.SafetyDataEPlanId == SafetyDataEPlanId);
            if(SafetyDataEPlan != null)
            {
                db.SafetyDataE_SafetyDataEPlan.DeleteOnSubmit(SafetyDataEPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlanId"></param>
        public static void DeleteSafetyDataEPlanByProjectId(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var SafetyDataEPlan = from x in db.SafetyDataE_SafetyDataEPlan
                                  where x.ProjectId == projectId && (x.IsManual == false || x.IsManual == null)
                                  select x;
            if (SafetyDataEPlan.Count() > 0)
            {
                db.SafetyDataE_SafetyDataEPlan.DeleteAllOnSubmit(SafetyDataEPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全资料项主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlanId"></param>
        public static void DeleteSafetyDataEPlanBySafetyDataEId(string SafetyDataEId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var SafetyDataEPlan = from x in db.SafetyDataE_SafetyDataEPlan
                                  where x.SafetyDataEId == SafetyDataEId
                                  select x;
            if (SafetyDataEPlan.Count() > 0)
            {
                db.SafetyDataE_SafetyDataEPlan.DeleteAllOnSubmit(SafetyDataEPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlanId"></param>
        public static void DeleteSafetyDataEPlanByProjectIdSafetyDataEId(string projectId, string SafetyDataEId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var SafetyDataEPlan = from x in db.SafetyDataE_SafetyDataEPlan
                                  where x.ProjectId == projectId && x.SafetyDataEId == SafetyDataEId                                  
                                  select x;
            if (SafetyDataEPlan.Count() > 0)
            {
                db.SafetyDataE_SafetyDataEPlan.DeleteAllOnSubmit(SafetyDataEPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全资料项主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="SafetyDataEPlanId"></param>
        public static void DeleteSafetyDataEPlanByProjectDateId(string projectId, DateTime? projectDate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var SafetyDataEPlan = from x in db.SafetyDataE_SafetyDataEPlan
                                  where x.ProjectId == projectId && x.CheckDate > projectDate && (x.IsManual == false || x.IsManual == null)
                                  select x;
            if (SafetyDataEPlan.Count() > 0)
            {
                db.SafetyDataE_SafetyDataEPlan.DeleteAllOnSubmit(SafetyDataEPlan);
                db.SubmitChanges();
            }
        }
        #endregion
                        
        /// <summary>
        /// 添加考核明细时 得到实际考核分数
        /// </summary>
        /// <param name="SafetyDataEPlan">提交时间未空的考核计划明细集合</param>
        private static void GetSafetyDataEPlanRealScore(Model.SafetyDataE_SafetyDataEPlan SafetyDataEPlan)
        {
            /// 考核项目、考核资料项、考核时间内  是否存在资料
            var SafetyDataEItem = from x in Funs.DB.SafetyDataE_SafetyDataEItem
                                  where x.ProjectId == SafetyDataEPlan.ProjectId && x.SafetyDataEId == SafetyDataEPlan.SafetyDataEId                          
                                  orderby x.SubmitDate
                                  select x;
            if (SafetyDataEItem.Count() > 0)
            {
                SafetyDataEPlan.SubmitDate = SafetyDataEItem.FirstOrDefault().SubmitDate;
                if (SafetyDataEPlan.SubmitDate <= SafetyDataEPlan.CheckDate || SafetyDataEPlan.ShouldScore <= 0) ///准时提交
                {
                    SafetyDataEPlan.RealScore = SafetyDataEPlan.ShouldScore;
                }
                else   ///超期提交
                {
                    SafetyDataEPlan.RealScore = 0;
                }

                UpdateSafetyDataEPlan(SafetyDataEPlan);
            }
        }
    }
}
