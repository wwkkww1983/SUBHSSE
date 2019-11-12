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
                getItem.ProjectId = projectId;
                getItem.UserId = userId;
                getItem.UserName = UserService.GetUserNameByUserId(userId);
                getItem.DiaryDate = diaryDate;
                getItem.HSEDiaryId = SQLHelper.GetNewID();
                getItem.Value1 = getValues1(projectId, userId, diaryDate);
                getItem.Value2 = getValues2(projectId, userId, diaryDate);
                getItem.Value3 = getValues3(projectId, userId, diaryDate);
                getItem.Value4 = "0";
                getItem.Value5 = "0";
                getItem.Value6 = "0";
                getItem.Value7 = "0";
                getItem.Value8 = getValues8(projectId, userId, diaryDate);
                getItem.Value9 = "0";
                getItem.Value10 = "0";
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
        private static string getValues1(string projectId, string userId, string diaryDate)
        {
            DateTime? getDate = Funs.GetNewDateTime(diaryDate);
            string strValues = string.Empty;
            var getRegister = (from x in Funs.DB.HSSE_Hazard_HazardRegister
                              where x.ProjectId == projectId && x.CheckManId == userId
                              && getDate > x.CheckTime.Value.AddDays(-1) && getDate < x.CheckTime.Value.AddDays(1)
                              select x).Count();
            if (getRegister > 0)
            {
                strValues += "安全巡检：" + getRegister.ToString() + "；";
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
        private static string getValues2(string projectId, string userId, string diaryDate)
        {
            DateTime? getDate = Funs.GetNewDateTime(diaryDate);
            string strValues = string.Empty;
            var getRegister = from x in Funs.DB.HSSE_Hazard_HazardRegister
                               where x.ProjectId == projectId && x.ResponsibleMan == userId
                               && getDate > x.CheckTime.Value.AddDays(-1) && getDate < x.CheckTime.Value.AddDays(1)
                               select x;
            if (getRegister.Count() > 0)
            {
                strValues += "隐患数：" + getRegister.Count().ToString() + "；";
                var getC = getRegister.Where(x => x.States == Const.State_3);
                if (getC.Count() > 0)
                {
                    strValues += "整改数量：" + getC.Count().ToString() + "；";
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
        /// 3作业许可情况及作业票数量
        /// </summary>
        private static string getValues3(string projectId, string userId, string diaryDate)
        {
            DateTime? getDate = Funs.GetNewDateTime(diaryDate);
            string strValues = string.Empty;
            var getLicense = from x in Funs.DB.License_FlowOperate
                              join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                              where x.ProjectId ==projectId && x.OperaterId == userId && getDate > x.OperaterTime.Value.AddDays(-1) && getDate < x.OperaterTime.Value.AddDays(1)                             
                              select new { x.DataId, y.MenuName };
            if (getLicense.Count() > 0)
            {
                var getNames = getLicense.Select(x => x.MenuName).Distinct();
                foreach (var item in getNames)
                {

                    strValues += item+"：" + getLicense.Where(x=>x.MenuName ==item).Select(x=>x.DataId).Distinct().Count().ToString() + "；";
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
        private static string getValues8(string projectId, string userId, string diaryDate)
        {
            DateTime? getDate = Funs.GetNewDateTime(diaryDate);
            string strValues = string.Empty;
            var getClassMeeting = (from x in Funs.DB.Meeting_ClassMeeting
                               where x.ProjectId == projectId && x.CompileMan == userId
                               && getDate > x.ClassMeetingDate.Value.AddDays(-1) && getDate < x.ClassMeetingDate.Value.AddDays(1)
                               select x).Count();
            if (getClassMeeting > 0)
            {
                strValues += "班前会：" + getClassMeeting.ToString() + "；";
            }
            var getWeekMeeting = (from x in Funs.DB.Meeting_WeekMeeting
                                   where x.ProjectId == projectId && x.CompileMan == userId
                                   && getDate > x.WeekMeetingDate.Value.AddDays(-1) && getDate < x.WeekMeetingDate.Value.AddDays(1)
                                   select x).Count();
            if (getWeekMeeting > 0)
            {
                strValues += "周例会：" + getWeekMeeting.ToString() + "；";
            }
            var getMonthMeeting = (from x in Funs.DB.Meeting_MonthMeeting
                                  where x.ProjectId == projectId && x.CompileMan == userId
                                  && getDate > x.MonthMeetingDate.Value.AddDays(-1) && getDate < x.MonthMeetingDate.Value.AddDays(1)
                                  select x).Count();
            if (getMonthMeeting > 0)
            {
                strValues += "月例会：" + getMonthMeeting.ToString() + "；";
            }
            var getSpecialMeeting = (from x in Funs.DB.Meeting_SpecialMeeting
                                   where x.ProjectId == projectId && x.CompileMan == userId
                                   && getDate > x.SpecialMeetingDate.Value.AddDays(-1) && getDate < x.SpecialMeetingDate.Value.AddDays(1)
                                   select x).Count();
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
        #endregion
    }
}
