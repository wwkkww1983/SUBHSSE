using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataPlanService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        #region 根据项目id获取企业安全管理资料计划列表
        /// <summary>
        /// 根据项目id获取企业安全管理资料计划列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.SafetyData_SafetyDataPlan> GetSafetyDataPlanList(string projectId)
        {
            var SafetyDataPlanList = from x in Funs.DB.SafetyData_SafetyDataPlan
                                     join y in Funs.DB.SafetyData_SafetyData on x.SafetyDataId equals y.SafetyDataId
                                     where x.ProjectId == projectId
                                     orderby y.Code
                                     select x;
            return SafetyDataPlanList.ToList();
        }
        #endregion

        #region 根据主键id获取企业安全管理资料
        /// <summary>
        /// 根据主键id获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataPlan GetSafetyDataPlanBySafetyDataPlanId(string safetyDataPlanId)
        {
            return Funs.DB.SafetyData_SafetyDataPlan.FirstOrDefault(x => x.SafetyDataPlanId == safetyDataPlanId);
        }
        #endregion

        #region 增、删、改企业安全管理资料计划总表
        /// <summary>
        /// 添加企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlan"></param>
        public static void AddSafetyDataPlan(Model.SafetyData_SafetyDataPlan safetyDataPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataPlan newSafetyDataPlan = new Model.SafetyData_SafetyDataPlan
            {
                SafetyDataPlanId = safetyDataPlan.SafetyDataPlanId,
                ProjectId = safetyDataPlan.ProjectId,
                SafetyDataId = safetyDataPlan.SafetyDataId,
                CheckDate = safetyDataPlan.CheckDate,
                RealStartDate = safetyDataPlan.RealStartDate,
                RealEndDate = safetyDataPlan.RealEndDate,
                Score = safetyDataPlan.Score,
                ShouldScore = safetyDataPlan.ShouldScore,
                Remark = safetyDataPlan.Remark,
                ReminderDate=safetyDataPlan.ReminderDate,
                IsManual=safetyDataPlan.IsManual,
            };
            db.SafetyData_SafetyDataPlan.InsertOnSubmit(newSafetyDataPlan);
            db.SubmitChanges();
            ///当前计划项 没有提交时间时
            if (!newSafetyDataPlan.SubmitDate.HasValue)
            {
                GetSafetyDataPlanRealScore(newSafetyDataPlan);
            }
        }

        /// <summary>
        /// 修改企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlan"></param>
        public static void UpdateSafetyDataPlan(Model.SafetyData_SafetyDataPlan safetyDataPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataPlan newSafetyDataPlan = db.SafetyData_SafetyDataPlan.FirstOrDefault(e => e.SafetyDataPlanId == safetyDataPlan.SafetyDataPlanId);
            if (newSafetyDataPlan != null)
            {
                newSafetyDataPlan.CheckDate = safetyDataPlan.CheckDate;
                newSafetyDataPlan.RealStartDate = safetyDataPlan.RealStartDate;
                newSafetyDataPlan.RealEndDate = safetyDataPlan.RealEndDate;
                newSafetyDataPlan.Score = safetyDataPlan.Score;
                newSafetyDataPlan.Remark = safetyDataPlan.Remark;
                newSafetyDataPlan.ReminderDate = safetyDataPlan.ReminderDate;
                db.SubmitChanges();

                ///当前计划项 没有提交时间时
                if (!newSafetyDataPlan.SubmitDate.HasValue)
                {
                    GetSafetyDataPlanRealScore(newSafetyDataPlan);
                }
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanByID(string safetyDataPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataPlan SafetyDataPlan = db.SafetyData_SafetyDataPlan.FirstOrDefault(e => e.SafetyDataPlanId == safetyDataPlanId);
            {
                db.SafetyData_SafetyDataPlan.DeleteOnSubmit(SafetyDataPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanByProjectId(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataPlan = from x in db.SafetyData_SafetyDataPlan
                                 where x.ProjectId == projectId
                                 && (x.IsManual == false || x.IsManual == null)
                                 select x;
            if (safetyDataPlan.Count() > 0)
            {
                db.SafetyData_SafetyDataPlan.DeleteAllOnSubmit(safetyDataPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全资料项主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanBySafetyDataId(string safetyDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataPlan = from x in db.SafetyData_SafetyDataPlan where x.SafetyDataId == safetyDataId select x;
            if (safetyDataPlan.Count() > 0)
            {
                db.SafetyData_SafetyDataPlan.DeleteAllOnSubmit(safetyDataPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanByProjectIdSafetyDataId(string projectId, string safetyDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataPlan = from x in db.SafetyData_SafetyDataPlan
                                 where x.ProjectId == projectId && x.SafetyDataId == safetyDataId 
                                 && (x.IsManual == false || x.IsManual == null)
                                 select x;
            if (safetyDataPlan.Count() > 0)
            {
                db.SafetyData_SafetyDataPlan.DeleteAllOnSubmit(safetyDataPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全资料项主键删除企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanByProjectDateId(string projectId, DateTime? projectDate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataPlan = from x in db.SafetyData_SafetyDataPlan
                                 where x.ProjectId == projectId && x.CheckDate > projectDate
                                 && (x.IsManual == false || x.IsManual == null)
                                 select x;
            if (safetyDataPlan.Count() > 0)
            {
                db.SafetyData_SafetyDataPlan.DeleteAllOnSubmit(safetyDataPlan);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 根据项目和安全资料项生成企业安全管理资料计划总表
        /// <summary>
        /// 根据项目和安全资料项生成企业安全管理资料计划总表
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void GetSafetyDataPlanByProjectInfo(string projectId, string safetyDataId, DateTime? startTime, DateTime? endTime)
        {
            Model.SUBHSSEDB db = Funs.DB;
            List<Model.SafetyData_SafetyData> safetyDataEnd = new List<Model.SafetyData_SafetyData>();
            ///是否更新某一个安全资料项 先删除 再重新生成
            DeleteSafetyDataPlanBySafetyDataId(safetyDataId);
            //if (!isDelete(projectId, safetyDataId))
            //{
            if (!string.IsNullOrEmpty(safetyDataId))
            {
                safetyDataEnd = (from x in db.SafetyData_SafetyData
                                 where x.IsEndLever == true && x.SafetyDataId == safetyDataId && x.IsCheck == true
                                 orderby x.Code
                                 select x).ToList();

            }
            else
            {
                safetyDataEnd = (from x in db.SafetyData_SafetyData
                                 where x.IsEndLever == true && x.IsCheck == true
                                 orderby x.Code
                                 select x).ToList();
            }

            ///是否存在安全资料项
            if (safetyDataEnd.Count() > 0)
            {
                Model.Base_Project project = new Model.Base_Project();
                if (!string.IsNullOrEmpty(projectId))
                {
                    project = BLL.ProjectService.GetProjectByProjectId(projectId);
                    if (project != null)
                    {
                        GetSafetyDataPlanMethod(project, safetyDataEnd, startTime, endTime);
                    }
                }
                else
                {
                    ///取竣工时间大于当前时间 项目
                    var projects = from x in db.Base_Project
                                   where x.EndDate >= System.DateTime.Now && x.ProjectType != "5"
                                   select x;
                    if (projects.Count() > 0)
                    {
                        foreach (var item in projects)
                        {
                            GetSafetyDataPlanMethod(item, safetyDataEnd, startTime, endTime);
                        }
                    }
                }
            }
            //}
        }

        /// <summary>
        /// 生成安全资料计划总表 方法
        /// </summary>
        /// <param name="project"></param>
        /// <param name="safetyDataEnd"></param>
        public static void GetSafetyDataPlanMethod(Model.Base_Project project, List<Model.SafetyData_SafetyData> safetyDataEnd, DateTime? startTime, DateTime? endTime)
        {
            ////第一步 判断是否存在此项目的计划表
            ////第二步 不存在增加这个项目时间范围内的 存在取不存在时间段
            ////第三步 项目时间 是否为空？ 现在默认都不能为空
            Model.SUBHSSEDB db = Funs.DB;
            string projectId = project.ProjectId;
            DateTime startDatep = project.StartDate.HasValue ? project.StartDate.Value : System.DateTime.Now;
            if (startTime.HasValue)
            {
                startDatep = startTime.Value;
            }
            DateTime startDate = startDatep;
            DateTime endDate = project.EndDate.HasValue ? project.EndDate.Value : System.DateTime.Now.AddMonths(6);
            if (endTime.HasValue)
            {
                endDate = endTime.Value;
            }
            DeleteSafetyDataPlanByProjectDateId(projectId, endDate);   ///删除竣工后的考核计划
            foreach (var item in safetyDataEnd)
            {
                var safetyDataPlan = db.SafetyData_SafetyDataPlan.FirstOrDefault(x => x.ProjectId == project.ProjectId && x.SafetyDataId == item.SafetyDataId && (x.IsManual == null || x.IsManual == false));
                if (safetyDataPlan != null)
                {
                    ///取结束时间 最大值作为开始时间
                    DateTime maxEndDate = Funs.DB.SafetyData_SafetyDataPlan.Where(x => x.ProjectId == projectId && x.RealEndDate.HasValue && x.SafetyDataId == item.SafetyDataId).Select(x => x.RealEndDate.Value).Max();
                    if (endDate > maxEndDate) ////如果计划单最大时间小于项目结束时间 则追加时间 否则删去
                    {
                        startDate = maxEndDate;
                    }
                    else
                    { ///项目提前结束 则删除计划时间
                        var delSafetyDataPlan = from x in db.SafetyData_SafetyDataPlan where x.RealEndDate > endDate && x.SafetyDataId == item.SafetyDataId select x;
                        if (delSafetyDataPlan.Count() > 0)
                        {
                            db.SafetyData_SafetyDataPlan.DeleteAllOnSubmit(delSafetyDataPlan);
                        }
                    }
                }
                ////算出 开始、结束时间跨度 然后循环增加一个月 并把在此时间段的 考核项写入计划表
                for (int i = 0; startDate.AddMonths(i) <= endDate; i++)
                {
                    Model.SafetyData_SafetyDataPlan newSafetyDataPlan = new Model.SafetyData_SafetyDataPlan
                    {
                        SafetyDataPlanId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataPlan)),
                        ProjectId = projectId,
                        SafetyDataId = item.SafetyDataId,
                        Score = item.Score,
                        ShouldScore = item.Score,
                        Remark = item.Remark,
                    };

                    int monthValue = 0;  ///设置月数
                    if (item.CheckTypeValue1.HasValue)
                    {
                        monthValue = item.CheckTypeValue1.Value;
                    }

                    int dateValue = 1;  ///设置天
                    if (item.CheckTypeValue2.HasValue)
                    {
                        dateValue = item.CheckTypeValue2.Value;
                        if (dateValue > 30)
                        {
                            dateValue = 30;
                        }
                    }

                    ////TODO：通过判断是月报、季报、定时报等情况 是否落在 当前时间范围内 写入到计划总表
                    if (item.CheckType == BLL.Const.SafetyDataCheckType_1) /// 月报
                    {
                        if (startDate.AddMonths(i + monthValue).Month == 2 && dateValue > 28)
                        {
                            dateValue = 28;
                        }
                        DateTime? checkDate = Funs.GetNewDateTime(startDate.AddMonths(i + monthValue).Year + "-" + startDate.AddMonths(i + monthValue).Month + "-" + dateValue);
                        if (checkDate.HasValue && checkDate <= endDate && checkDate >= startDatep)
                        {
                            newSafetyDataPlan.CheckDate = checkDate;
                            newSafetyDataPlan.RealStartDate = checkDate.Value.AddMonths(-1); ///月报开始日期
                            newSafetyDataPlan.RealStartDate =  new DateTime(newSafetyDataPlan.RealStartDate.Value.Year, newSafetyDataPlan.RealStartDate.Value.Month, 1);
                            newSafetyDataPlan.RealEndDate = newSafetyDataPlan.RealStartDate.Value.AddMonths(1).AddDays(-1);                            
                        }
                    }
                    else if (item.CheckType == BLL.Const.SafetyDataCheckType_2) /// 季报
                    {
                        int month = startDate.AddMonths(i).Month; ///当前月份                                                                 
                        if ((month == 3 + monthValue) || (month == 6 + monthValue) || (month == 9 + monthValue) || (month == monthValue) || (month == 12 && monthValue == 0)) ///考核季度时间
                        {
                            if (startDate.AddMonths(i + monthValue).Month == 2 && dateValue > 28)
                            {
                                dateValue = 28;
                            }
                            DateTime? checkDate = Funs.GetNewDateTime(startDate.AddMonths(i).Year + "-" + startDate.AddMonths(i).Month + "-" + dateValue);
                            if ((month == monthValue) && monthValue != 0)
                            {
                                checkDate = checkDate.Value.AddYears(1);
                            }

                            if (checkDate.HasValue && checkDate <= endDate && checkDate >= startDatep)
                            {
                                newSafetyDataPlan.CheckDate = checkDate;
                                newSafetyDataPlan.RealStartDate = checkDate.Value.AddMonths(-3); ///开始日期
                                newSafetyDataPlan.RealStartDate = new DateTime(newSafetyDataPlan.RealStartDate.Value.Year, newSafetyDataPlan.RealStartDate.Value.Month, 1);
                                newSafetyDataPlan.RealEndDate = newSafetyDataPlan.RealStartDate.Value.AddMonths(3).AddDays(-1);
                                
                            }
                        }
                    }
                    else if (item.CheckType == BLL.Const.SafetyDataCheckType_3) /// 定时
                    {
                        if (startDate.AddMonths(i).Month == monthValue) ///定时月份
                        {
                            if (startDate.AddMonths(i + monthValue).Month == 2 && dateValue > 28)
                            {
                                dateValue = 28;
                            }
                            DateTime? checkDate = Funs.GetNewDateTime(startDate.AddMonths(i).Year + "-" + startDate.AddMonths(i).Month + "-" + dateValue);
                            if (checkDate.HasValue && checkDate <= endDate && checkDate >= startDatep)
                            {
                                newSafetyDataPlan.CheckDate = checkDate;
                                newSafetyDataPlan.RealStartDate = checkDate.Value.AddMonths(-12); ///开始日期
                                newSafetyDataPlan.RealStartDate = new DateTime(newSafetyDataPlan.RealStartDate.Value.Year, newSafetyDataPlan.RealStartDate.Value.Month, 1);
                                newSafetyDataPlan.RealEndDate = checkDate.Value;
                               
                            }
                        }
                    }
                    else if (item.CheckType == BLL.Const.SafetyDataCheckType_4) /// 开工后报
                    {
                        DateTime? checkDate = startDate.AddMonths(i);
                        if (checkDate.HasValue && checkDate <= endDate && BLL.Funs.CompareMonths(startDatep, checkDate.Value) == monthValue && checkDate >= startDatep)
                        {
                            newSafetyDataPlan.CheckDate = checkDate;
                            newSafetyDataPlan.RealStartDate = startDate; ///开始日期
                            newSafetyDataPlan.RealEndDate = checkDate.Value;
                           
                        }
                    }
                    else if (item.CheckType == BLL.Const.SafetyDataCheckType_5) /// 半年报
                    {
                        if (startDate.AddMonths(i).Month == monthValue || startDate.AddMonths(i).Month == monthValue + 6)
                        {
                            if (startDate.AddMonths(i + monthValue).Month == 2 && dateValue > 28)
                            {
                                dateValue = 28;
                            }
                            DateTime? checkDate = Funs.GetNewDateTime(startDate.AddMonths(i).Year + "-" + startDate.AddMonths(i).Month + "-" + dateValue);
                            if (checkDate.HasValue && checkDate <= endDate && checkDate >= startDatep)
                            {
                                newSafetyDataPlan.CheckDate = checkDate;
                                newSafetyDataPlan.RealStartDate = checkDate.Value.AddMonths(-6); ///开始日期
                                newSafetyDataPlan.RealStartDate = new DateTime(newSafetyDataPlan.RealStartDate.Value.Year, newSafetyDataPlan.RealStartDate.Value.Month, 1);
                                newSafetyDataPlan.RealEndDate = newSafetyDataPlan.RealStartDate.Value.AddMonths(6).AddDays(-1);
                                
                            }
                        }
                    }
                    else  /// 其他
                    {
                        if (monthValue > 0 && startDate.AddMonths(i).Year == System.DateTime.Now.Year && startDate.AddMonths(i).Month == monthValue)
                        {
                            if (startDate.AddMonths(i + monthValue).Month == 2 && dateValue > 28)
                            {
                                dateValue = 28;
                            }
                            DateTime? checkDate = Funs.GetNewDateTime(startDate.AddMonths(i).Year + "-" + startDate.AddMonths(i).Month + "-" + dateValue);
                            if (checkDate.HasValue && checkDate <= endDate && checkDate >= startDatep)
                            {
                                newSafetyDataPlan.CheckDate = checkDate;
                                newSafetyDataPlan.RealStartDate = startDate; ///开始日期
                                newSafetyDataPlan.RealStartDate = new DateTime(newSafetyDataPlan.RealStartDate.Value.Year, newSafetyDataPlan.RealStartDate.Value.Month, 1);
                                newSafetyDataPlan.RealEndDate = checkDate.Value;
                                
                            }
                        }
                    }
                    if (newSafetyDataPlan.RealEndDate.HasValue)
                    {
                        newSafetyDataPlan.ReminderDate = newSafetyDataPlan.CheckDate.Value.AddDays(-7);
                        AddSafetyDataPlan(newSafetyDataPlan);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        ///  是否当前项目不考核项
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="safetyDataId"></param>
        /// <returns></returns>
        public static bool isDelete(string projectId, string safetyDataId)
        {
            bool isDelete = false;
            var safetyDataPlanDelete = Funs.DB.SafetyData_SafetyDataPlanDelete.FirstOrDefault(x => x.ProjectId == projectId && x.SafetyDataId == safetyDataId);
            if (safetyDataPlanDelete != null)
            {
                isDelete = true;
            }
            return isDelete;
        }
        
        #region 增、删 企业安全管理资料计划项
        /// <summary>
        /// 添加企业安全管理资料计划项
        /// </summary>
        /// <param name="safetyDataPlan"></param>
        public static void AddSafetyDataPlanDelete(string projectId, string safetyDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var delete = db.SafetyData_SafetyDataPlanDelete.FirstOrDefault(x => x.ProjectId == projectId && x.SafetyDataId == safetyDataId);
            if (delete == null)
            {
                Model.SafetyData_SafetyDataPlanDelete newSafetyDataPlanDelete = new Model.SafetyData_SafetyDataPlanDelete
                {
                    SafetyDataPlanDeleteId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataPlanDelete)),
                    ProjectId = projectId,
                    SafetyDataId = safetyDataId,
                    DeleteDate = System.DateTime.Now
                };
                db.SafetyData_SafetyDataPlanDelete.InsertOnSubmit(newSafetyDataPlanDelete);
                db.SubmitChanges();
            }

            DeleteSafetyDataPlanByProjectIdSafetyDataId(projectId, safetyDataId);
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料计划项
        /// </summary>
        /// <param name="safetyDataPlanId"></param>
        public static void DeleteSafetyDataPlanDelete(string projectId, string safetyDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataPlan = from x in db.SafetyData_SafetyDataPlanDelete where x.ProjectId == projectId && x.SafetyDataId == safetyDataId select x;
            if (safetyDataPlan.Count() > 0)
            {
                db.SafetyData_SafetyDataPlanDelete.DeleteAllOnSubmit(safetyDataPlan);
                db.SubmitChanges();
            }
            GetSafetyDataPlanByProjectInfo(projectId, safetyDataId, null, null);
            
        }
        #endregion
        
        /// <summary>
        /// 添加考核明细时 得到实际考核分数
        /// </summary>
        /// <param name="SafetyDataPlan">提交时间未空的考核计划明细集合</param>
        private static void GetSafetyDataPlanRealScore(Model.SafetyData_SafetyDataPlan safetyDataPlan)
        {
            /// 考核项目、考核资料项、考核时间内  是否存在资料
            var safetyDataItem = from x in Funs.DB.SafetyData_SafetyDataItem
                                 where x.ProjectId == safetyDataPlan.ProjectId && x.SafetyDataId == safetyDataPlan.SafetyDataId 
                                 && x.CompileDate >= safetyDataPlan.RealStartDate && x.CompileDate <= safetyDataPlan.RealEndDate                               
                                 orderby x.SubmitDate
                                 select x;
            if (safetyDataItem.Count() > 0)
            {
                safetyDataPlan.SubmitDate = safetyDataItem.FirstOrDefault().SubmitDate;
                if (safetyDataPlan.SubmitDate <= safetyDataPlan.CheckDate || safetyDataPlan.ShouldScore < 0) ///准时提交
                {
                    safetyDataPlan.RealScore = safetyDataPlan.ShouldScore;
                }
                else   ///超期提交
                {
                    safetyDataPlan.RealScore = 0;
                }

                UpdateSafetyDataPlan(safetyDataPlan);
            }
        }
    }
}
