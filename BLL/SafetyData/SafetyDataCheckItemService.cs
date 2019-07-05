using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 企业安全管理资料考核明细
    /// </summary>
    public static class SafetyDataCheckItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataCheckItem GetSafetyDataCheckItemById(string SafetyDataCheckItemId)
        {
            return Funs.DB.SafetyData_SafetyDataCheckItem.FirstOrDefault(e => e.SafetyDataCheckItemId == SafetyDataCheckItemId);
        }

        /// <summary>
        /// 根据项目id获取企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        /// <returns></returns>
        public static List<Model.SafetyData_SafetyDataCheckItem> GetSafetyDataCheckItemListByProjectId(string projectId)
        {
            var list = from x in Funs.DB.SafetyData_SafetyDataCheckItem
                       join y in Funs.DB.SafetyData_SafetyDataCheckProject on x.SafetyDataCheckProjectId equals y.SafetyDataCheckProjectId
                       where y.ProjectId == projectId
                       select x;
            return list.ToList();
        }

        /// <summary>
        /// 根据项目id获取企业安全管理资料考核项目
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        /// <returns></returns>
        public static List<Model.SafetyData_SafetyDataCheckProject> GetSafetyDataCheckProjectListBySafetyDataCheckId(string SafetyDataCheckId)
        {
            var list = from x in Funs.DB.SafetyData_SafetyDataCheckProject
                       where x.SafetyDataCheckId == SafetyDataCheckId
                       select x;
            return list.ToList();
        }

        /// <summary>
        /// 根据项目ID获取企业安全管理资料考核项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataCheckProject GetSafetyDataCheckProjectByProjectIdSafetyDataCheckId(string projectId, string safetyDataCheckId)
        {
            return Funs.DB.SafetyData_SafetyDataCheckProject.FirstOrDefault(e => e.ProjectId == projectId && e.SafetyDataCheckId == safetyDataCheckId);
        }

        /// <summary>
        /// 根据主键获取企业安全管理资料考核项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataCheckProject GetSafetyDataCheckProjectBySafetyDataCheckProjectId(string SafetyDataCheckProjectId)
        {
            return Funs.DB.SafetyData_SafetyDataCheckProject.FirstOrDefault(e => e.SafetyDataCheckProjectId == SafetyDataCheckProjectId);
        }

        #region 企业安全管理资料考核项目
        /// <summary>
        /// 添加企业安全管理资料考核项目
        /// </summary>
        /// <param name="SafetyDataCheckProject"></param>
        public static void AddSafetyDataCheckProject(Model.SafetyData_SafetyDataCheckProject SafetyDataCheckProject)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheckProject newSafetyDataCheckProject = new Model.SafetyData_SafetyDataCheckProject
            {
                SafetyDataCheckProjectId = SafetyDataCheckProject.SafetyDataCheckProjectId,
                SafetyDataCheckId = SafetyDataCheckProject.SafetyDataCheckId,
                ProjectId = SafetyDataCheckProject.ProjectId
            };
            db.SafetyData_SafetyDataCheckProject.InsertOnSubmit(newSafetyDataCheckProject);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料考核项目
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        public static void DeleteSafetyDataCheckProjectByProjectId(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var SafetyDataCheckProject = from x in db.SafetyData_SafetyDataCheckProject where x.ProjectId == projectId select x;
            if (SafetyDataCheckProject.Count() > 0)
            {
                foreach (var item in SafetyDataCheckProject)
                {
                    var safetyDataCheckItem = from x in db.SafetyData_SafetyDataCheckItem where x.SafetyDataCheckProjectId == item.SafetyDataCheckProjectId select x;
                    if (safetyDataCheckItem.Count() > 0)
                    {
                        db.SafetyData_SafetyDataCheckItem.DeleteAllOnSubmit(safetyDataCheckItem);
                    }
 
                }
                db.SafetyData_SafetyDataCheckProject.DeleteAllOnSubmit(SafetyDataCheckProject);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 安全管理资料考核明细
        /// <summary>
        /// 添加企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItem"></param>
        public static void AddSafetyDataCheckItem(Model.SafetyData_SafetyDataCheckItem SafetyDataCheckItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheckItem newSafetyDataCheckItem = new Model.SafetyData_SafetyDataCheckItem
            {
                SafetyDataCheckItemId = SafetyDataCheckItem.SafetyDataCheckItemId,
                SafetyDataCheckProjectId = SafetyDataCheckItem.SafetyDataCheckProjectId,
                SafetyDataId = SafetyDataCheckItem.SafetyDataId,
                CheckDate = SafetyDataCheckItem.CheckDate,
                StartDate = SafetyDataCheckItem.StartDate,
                EndDate = SafetyDataCheckItem.EndDate,
                ReminderDate = SafetyDataCheckItem.ReminderDate,
                SubmitDate = SafetyDataCheckItem.SubmitDate,
                ShouldScore = SafetyDataCheckItem.ShouldScore,
                RealScore = SafetyDataCheckItem.RealScore,
                Remark = SafetyDataCheckItem.Remark,
                SafetyDataPlanId = SafetyDataCheckItem.SafetyDataPlanId
            };
            db.SafetyData_SafetyDataCheckItem.InsertOnSubmit(newSafetyDataCheckItem);
            db.SubmitChanges();

            if (!newSafetyDataCheckItem.SubmitDate.HasValue)
            {
                /// 添加考核明细时 得实际到考核分数
                GetSafetyDataCheckItemRealScore(newSafetyDataCheckItem);
            }
        }

        /// <summary>
        /// 修改企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItem"></param>
        public static void UpdateSafetyDataCheckItem(Model.SafetyData_SafetyDataCheckItem SafetyDataCheckItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheckItem newSafetyDataCheckItem = db.SafetyData_SafetyDataCheckItem.FirstOrDefault(e => e.SafetyDataCheckItemId == SafetyDataCheckItem.SafetyDataCheckItemId);
            if (newSafetyDataCheckItem != null)
            {
                newSafetyDataCheckItem.StartDate = SafetyDataCheckItem.StartDate;
                newSafetyDataCheckItem.EndDate = SafetyDataCheckItem.EndDate;
                newSafetyDataCheckItem.ReminderDate = SafetyDataCheckItem.ReminderDate;
                newSafetyDataCheckItem.SubmitDate = SafetyDataCheckItem.SubmitDate;
                newSafetyDataCheckItem.ShouldScore = SafetyDataCheckItem.ShouldScore;
                newSafetyDataCheckItem.RealScore = SafetyDataCheckItem.RealScore;           
                newSafetyDataCheckItem.Remark = SafetyDataCheckItem.Remark;
                db.SubmitChanges();
                if (!newSafetyDataCheckItem.SubmitDate.HasValue)
                {
                    /// 添加考核明细时 得到实际考核分数
                    GetSafetyDataCheckItemRealScore(newSafetyDataCheckItem);
                }
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        public static void DeleteSafetyDataCheckItemBySafetyDataCheckItemId(string SafetyDataCheckItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheckItem SafetyDataCheckItem = db.SafetyData_SafetyDataCheckItem.FirstOrDefault(e => e.SafetyDataCheckItemId == SafetyDataCheckItemId);
            if (SafetyDataCheckItem != null)
            {
                db.SafetyData_SafetyDataCheckItem.DeleteOnSubmit(SafetyDataCheckItem);
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// 根据考核主键删除企业安全管理资料考核明细
        /// </summary>
        /// <param name="SafetyDataCheckItemId"></param>
        public static void DeleteSafetyDataCheckItemBySafetyDataCheckId(string safetyDataCheckId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataCheckProject = from x in db.SafetyData_SafetyDataCheckProject where x.SafetyDataCheckId == safetyDataCheckId select x;
            if (safetyDataCheckProject.Count() > 0)
            {
                foreach (var item in safetyDataCheckProject)
                {
                    var SafetyDataCheckItem = from x in db.SafetyData_SafetyDataCheckItem where x.SafetyDataCheckProjectId == item.SafetyDataCheckProjectId select x;
                    if (SafetyDataCheckItem.Count() > 0)
                    {
                        db.SafetyData_SafetyDataCheckItem.DeleteAllOnSubmit(SafetyDataCheckItem);
                        db.SubmitChanges();
                    }
                }

                db.SafetyData_SafetyDataCheckProject.DeleteAllOnSubmit(safetyDataCheckProject);
            }
        }

        /// <summary>
        /// 添加考核明细时 得到实际考核分数
        /// </summary>
        /// <param name="safetyDataCheckItem">提交时间未空的考核计划明细集合</param>
        private static void GetSafetyDataCheckItemRealScore(Model.SafetyData_SafetyDataCheckItem safetyDataCheckItem)
        {
             ///考核项目
            var safetyDataCheckProject = BLL.SafetyDataCheckItemService.GetSafetyDataCheckProjectBySafetyDataCheckProjectId(safetyDataCheckItem.SafetyDataCheckProjectId);
            if(safetyDataCheckProject != null)
            {
                  /// 考核项目、考核资料项、考核时间内  是否存在资料
                var safetyDataItem = from x in Funs.DB.SafetyData_SafetyDataItem
                                     where x.ProjectId == safetyDataCheckProject.ProjectId && x.SafetyDataId == safetyDataCheckItem.SafetyDataId && x.CompileDate >= safetyDataCheckItem.StartDate && x.CompileDate <= safetyDataCheckItem.EndDate
                                     orderby x.SubmitDate
                                     select x;
                if (safetyDataItem.Count() > 0)
                {
                    safetyDataCheckItem.SubmitDate = safetyDataItem.FirstOrDefault().SubmitDate;
                    if (safetyDataCheckItem.SubmitDate <= safetyDataCheckItem.EndDate || safetyDataCheckItem.ShouldScore < 0) ///准时提交
                    {
                        safetyDataCheckItem.RealScore = safetyDataCheckItem.ShouldScore;
                    }
                    else   ///超期提交
                    {                       
                        safetyDataCheckItem.RealScore = 0;
                    }                    

                    UpdateSafetyDataCheckItem(safetyDataCheckItem);
                }
            }                                                                                                                                                                                  
        }
    }
}