using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    /// <summary>
    /// HSE日志
    /// </summary>
    public static class APIHSEDiaryService
    {
        public static List<Model.Sys_FlowOperate> getFlowOperteList;
        public static string getProjectId;
        public static string getUserId;
        public static DateTime getDate;

        #region 获取HSE日志信息
        /// <summary>
        /// 获取HSE日志信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="diaryDate"></param>
        /// <returns></returns>
        public static Model.HSEDiaryItem getHSEDiary(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            Model.HSEDiaryItem getItem = new Model.HSEDiaryItem();
            if (getDiaryDate.HasValue && !string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(userId))
            {
                getProjectId = projectId;
                getUserId = userId;
                getDate = getDiaryDate.Value;
                getFlowOperteList = (from x in Funs.DB.Sys_FlowOperate
                                     where x.ProjectId == projectId
                                     && x.OperaterId == userId && x.IsClosed == true
                                     && getDiaryDate > x.OperaterTime.Value.AddDays(-1) && getDiaryDate < x.OperaterTime.Value.AddDays(1)
                                     select x).ToList();
                getItem.ProjectId = projectId;
                getItem.UserId = userId;
                getItem.UserName = UserService.GetUserNameByUserId(userId);
                getItem.DiaryDate = diaryDate;
                getItem.HSEDiaryId = SQLHelper.GetNewID();
                getItem.Value1 = getValues1();
                getItem.Value2 = getValues2();
                getItem.Value3 = getValues3();
                getItem.Value4 = getValues4();
                getItem.Value5 = getValues5();
                getItem.Value6 = getValues6();
                getItem.Value7 = getValues7();
                getItem.Value8 = getValues8();
                getItem.Value9 = getValues9();
                getItem.Value10 = getValues10();
                var getInfo = Funs.DB.Project_HSEDiary.FirstOrDefault(x => x.UserId == userId && x.DiaryDate == getDiaryDate);
                if (getInfo != null)
                {
                    getItem.HSEDiaryId = getInfo.HSEDiaryId;
                    getItem.DailySummary = getInfo.DailySummary;
                    getItem.TomorrowPlan = getInfo.TomorrowPlan;
                }
            }
            return getItem;
        }
        #endregion        

        #region 获取HSE日志列表信息
        /// <summary>
        /// 获取HSE日志列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="diaryDate"></param>
        /// <returns></returns>
        public static List<Model.HSEDiaryItem> getHSEDiaryList(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            var getDataList = from x in Funs.DB.Project_HSEDiary
                              where x.ProjectId == projectId && (userId == null || x.UserId == userId)
                              && (diaryDate == null || x.DiaryDate == getDiaryDate)
                              orderby x.DiaryDate descending
                              select new Model.HSEDiaryItem
                              {
                                  HSEDiaryId = x.HSEDiaryId,
                                  ProjectId = x.ProjectId,
                                  DiaryDate = string.Format("{0:yyyy-MM-dd}", x.DiaryDate),
                                  UserId = x.UserId,
                                  UserName = Funs.DB.Sys_User.First(u => u.UserId == x.UserId).UserName,
                                  DailySummary = x.DailySummary,
                                  TomorrowPlan = x.TomorrowPlan,
                              };
            return getDataList.ToList();
        }
        #endregion

        #region 保存HSE日志
        /// <summary>
        /// 保存HSE日志
        /// </summary>
        /// <param name="item"></param>
        public static void SaveHSEDiary(Model.HSEDiaryItem item)
        {
            DeleteHSEDiary(item.HSEDiaryId);
            Model.Project_HSEDiary newHSEDiary = new Model.Project_HSEDiary
            {
                HSEDiaryId = item.HSEDiaryId,
                ProjectId = item.ProjectId,
                DiaryDate = Funs.GetNewDateTime(item.DiaryDate),
                UserId = item.UserId,
                DailySummary = item.DailySummary,
                TomorrowPlan = item.TomorrowPlan,
            };
            if (string.IsNullOrEmpty(newHSEDiary.HSEDiaryId))
            {
                newHSEDiary.HSEDiaryId = SQLHelper.GetNewID();
            }
            Funs.DB.Project_HSEDiary.InsertOnSubmit(newHSEDiary);
            Funs.SubmitChanges();
        }
        #endregion

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="hseDiaryId"></param>
        public static void DeleteHSEDiary(string hseDiaryId)
        {
            var getInfo = Funs.DB.Project_HSEDiary.FirstOrDefault(x => x.HSEDiaryId == hseDiaryId);
            if (getInfo != null)
            {
                Funs.DB.Project_HSEDiary.DeleteOnSubmit(getInfo);
                Funs.SubmitChanges();
            }
        }

        #region 获取日志信息
        /// <summary>
        /// 1HSE检查情况及检查次数
        /// </summary>
        private static string getValues1()
        {
            string strValues = string.Empty;
            var getRegister = (from x in Funs.DB.HSSE_Hazard_HazardRegister
                               where x.ProjectId == getProjectId && x.CheckManId == getUserId
                                 && getDate > x.CheckTime.Value.AddDays(-1) && getDate < x.CheckTime.Value.AddDays(1)
                               select x).Count();
            if (getRegister > 0)
            {
                strValues += "巡检：" + getRegister.ToString() + "；";
            }
            if (getFlowOperteList.Count() > 0)
            {
                var getDayCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckDayMenuId).Count();
                if (getDayCount > 0)
                {
                    strValues += "日常：" + getDayCount.ToString();
                }
                var getSpecialCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckSpecialMenuId).Count();
                if (getSpecialCount > 0)
                {
                    strValues += "专项：" + getSpecialCount.ToString();
                }
                var getColligationCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectCheckColligationMenuId).Count();
                if (getColligationCount > 0)
                {
                    strValues += "综合：" + getColligationCount.ToString();
                }
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 2隐患整改情况及隐患整改数量
        /// </summary>
        private static string getValues2()
        {
            string strValues = string.Empty;
            var getRegister = from x in Funs.DB.HSSE_Hazard_HazardRegister
                              where x.ProjectId == getProjectId && x.ResponsibleMan == getUserId
                              && getDate > x.CheckTime.Value.AddDays(-1) && getDate < x.CheckTime.Value.AddDays(1)
                              select x;
            if (getRegister.Count() > 0)
            {
                strValues += "隐患：" + getRegister.Count().ToString() + "；";
                var getC = getRegister.Where(x => x.States == Const.State_3);
                if (getC.Count() > 0)
                {
                    strValues += "整改：" + getC.Count().ToString() + "；";
                }
            }
            //var getColligationCount = (from x in Funs.DB.Sys_FlowOperate
            //                           where x.MenuId == Const.ProjectCheckColligationMenuId && x.ProjectId == getProjectId
            //                           && x.OperaterId == getUserId && x.IsClosed == true
            //                           && getDate > x.OperaterTime.Value.AddDays(-1) && getDate < x.OperaterTime.Value.AddDays(1)
            //                           select x).Count();
            //if (getColligationCount > 0)
            //{
            //    strValues += "综合大检查：" + getColligationCount.ToString();
            //}

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 3作业许可情况及作业票数量
        /// </summary>
        private static string getValues3()
        {
            string strValues = string.Empty;
            var getLicense = from x in Funs.DB.License_FlowOperate
                             join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                             where x.ProjectId == getProjectId && x.OperaterId == getUserId
                                 && getDate > x.OperaterTime.Value.AddDays(-1) && getDate < x.OperaterTime.Value.AddDays(1)
                             select new { x.DataId, y.MenuName };

            if (getLicense.Count() > 0)
            {
                var getNames = getLicense.Select(x => x.MenuName).Distinct();
                foreach (var item in getNames)
                {

                    strValues += item.Replace("作业票","") + "：" + getLicense.Where(x => x.MenuName == item).Select(x => x.DataId).Distinct().Count().ToString() + "；";
                }
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }

        /// <summary>
        /// 4施工机具、安全设施检查、验收情况及检查验收数量
        /// </summary>
        private static string getValues4()
        {
            string strValues = string.Empty;
            //var getCompileCount = (from x in Funs.DB.License_EquipmentSafetyList
            //                    where x.ProjectId == getProjectId && x.CompileMan == getUserId 
            //                        && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                    select x).Count();
            //if (getCompileCount > 0)
            //{
            //    strValues += "申请:" + getCompileCount.ToString() + "；";
            //}
            var getAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEquipmentSafetyListMenuId).Count();
            if (getAuditCount > 0)
            {
                strValues = getAuditCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 5危险源辨识工作情况及次数
        /// </summary>
        private static string getValues5()
        {
            string strValues = string.Empty;
            //var getHCompileCount = (from x in Funs.DB.Hazard_HazardList
            //                       where x.ProjectId == getProjectId && x.CompileMan == getUserId
            //                           && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                       select x).Count();
            //if (getHCompileCount > 0)
            //{
            //    strValues += "编制职业健康危险源:" + getHCompileCount.ToString() + "；";
            //}
            var getHAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectHazardListMenuId).Count();
            if (getHAuditCount > 0)
            {
                strValues += "职业健康:" + getHAuditCount.ToString() + "；";
            }

            //var getECompileCount = (from x in Funs.DB.Hazard_EnvironmentalRiskList
            //                        where x.ProjectId == getProjectId && x.CompileMan == getUserId
            //                            && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
            //                        select x).Count();
            //if (getECompileCount > 0)
            //{
            //    strValues += "编制环境危险源:" + getECompileCount.ToString() + "；";
            //}
            var getEAuditCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEnvironmentalRiskListMenuId).Count();
            if (getEAuditCount > 0)
            {
                strValues += "环境:" + getEAuditCount.ToString() + "；";
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 6应急计划修编、演练及物资准备情况及次数
        /// </summary>
        private static string getValues6()
        {
            string strValues = string.Empty;
            var getCompileCount = (from x in Funs.DB.Emergency_EmergencyList
                                   where x.ProjectId == getProjectId && (x.AuditMan == getUserId || x.ApproveMan == getUserId)
                                       && getDate > x.CompileDate.Value.AddDays(-1) && getDate < x.CompileDate.Value.AddDays(1)
                                   select x).Count();
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEmergencyListMenuId).Count();
            if (getCompileCount > 0)
            {
                strValues += "预案:" + (getCompileCount + getCompileCount).ToString() + "；";
            }

            var getDrillCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectDrillRecordListMenuId).Count();
            if (getDrillCount > 0)
            {
                strValues += "演练:" + getDrillCount.ToString() + "；";
            }
            var getSupplyCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectEmergencySupplyMenuId).Count();
            if (getSupplyCount > 0)
            {
                strValues += "物资:" + getSupplyCount.ToString() + "；";
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        /// 7教育培训情况及人次
        /// </summary>
        private static string getValues7()
        {
            string strValues = string.Empty;
            var getFlows = getFlowOperteList.Where(x => x.MenuId == Const.ProjectTrainRecordMenuId).ToList();
            if (getFlows.Count() > 0)
            {
                List<string> listIds = getFlows.Select(x => x.DataId).ToList();
                strValues += "次数:" + getFlows.Count().ToString() + "；";
                var getPersonCount = (from x in Funs.DB.EduTrain_TrainRecord
                                      join y in Funs.DB.EduTrain_TrainRecordDetail on x.TrainingId equals y.TrainingId
                                      where listIds.Contains(x.TrainingId)
                                      select y).Count();
                if (getPersonCount > 0)
                {
                    strValues += "人数:" + getPersonCount.ToString() + "。";
                }
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  8 HSE会议情况及次数
        /// </summary>
        private static string getValues8()
        {
            string strValues = string.Empty;
            var getClassMeeting = getFlowOperteList.Where(x => x.MenuId == Const.ProjectClassMeetingMenuId).Count();
            if (getClassMeeting > 0)
            {
                strValues += "班前会：" + getClassMeeting.ToString() + "；";
            }
            var getWeekMeeting = getFlowOperteList.Where(x => x.MenuId == Const.ProjectWeekMeetingMenuId).Count();
            if (getWeekMeeting > 0)
            {
                strValues += "周例会：" + getWeekMeeting.ToString() + "；";
            }
            var getMonthMeeting = getFlowOperteList.Where(x => x.MenuId == Const.ProjectMonthMeetingMenuId).Count();
            if (getMonthMeeting > 0)
            {
                strValues += "月例会：" + getMonthMeeting.ToString() + "；";
            }
            var getSpecialMeeting = getFlowOperteList.Where(x => x.MenuId == Const.ProjectSpecialMeetingMenuId).Count();
            if (getSpecialMeeting > 0)
            {
                strValues += "专题会：" + getSpecialMeeting.ToString() + "；";
            }

            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  9 HSE宣传工作情况
        /// </summary>
        private static string getValues9()
        {
            string strValues = string.Empty;
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectPromotionalActivitiesMenuId).Count();
            if (getFlowCount > 0)
            {
                strValues += getFlowCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        /// <summary>
        ///  10 HSE奖惩工作情况、HSE奖励次数、HSE处罚次数
        /// </summary>
        private static string getValues10()
        {
            string strValues = string.Empty;
            var getFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectIncentiveNoticeMenuId).Count();
            if (getFlowCount > 0)
            {
                strValues += "奖励单：" + getFlowCount.ToString();
            }

            var getPFlowCount = getFlowOperteList.Where(x => x.MenuId == Const.ProjectPunishNoticeMenuId).Count();
            if (getPFlowCount > 0)
            {
                strValues += "处罚单：" + getPFlowCount.ToString();
            }
            if (string.IsNullOrEmpty(strValues))
            {
                return "0";
            }
            else
            {
                return strValues;
            }
        }
        #endregion
    }
}
