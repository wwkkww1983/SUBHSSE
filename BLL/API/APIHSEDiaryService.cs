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
        #region 根据主键ID获取作业票安全措施详细
        /// <summary>
        ///  根据主键ID获取安全措施详细
        /// </summary>
        /// <param name="licenseItemId">主键</param>
        /// <returns></returns>
        public static Model.HSEDiaryItem getHSEDiary(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            Model.HSEDiaryItem getItem = new Model.HSEDiaryItem();
            if (getDiaryDate.HasValue && !string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(userId))
            {
                getItem.ProjectId = projectId;
                getItem.UserId = userId;
                getItem.UserId = UserService.GetUserNameByUserId(userId);
                getItem.DiaryDate = diaryDate;
                getItem.HSEDiaryId = SQLHelper.GetNewID();
                getItem.Value1 = "0";
                getItem.Value2 = "0";
                getItem.Value3 = "0";
                getItem.Value4 = "0";
                getItem.Value5 = "0";
                getItem.Value6 = "0";
                getItem.Value7 = "0";
                getItem.Value8 = "0";
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

        #region 获取作业票安全措施列表信息
        /// <summary>
        /// 获取作业票安全措施列表信息
        /// </summary>
        /// <param name="dataId">单据ID</param>
        /// <returns></returns>
        public static List<Model.HSEDiaryItem> getHSEDiaryList(string projectId, string userId, string diaryDate)
        {
            DateTime? getDiaryDate = Funs.GetNewDateTime(diaryDate);
            var getDataList = from x in Funs.DB.Project_HSEDiary
                              where x.ProjectId == projectId && (userId == null && x.UserId == userId)
                              && (diaryDate == null && x.DiaryDate == getDiaryDate)
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

        #region 保存作业票-安全措施
        /// <summary>
        /// 保存作业票-安全措施
        /// </summary>
        /// <param name="licenseItem">安全措施集合</param>
        /// <returns></returns>
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
        /// 删除
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
    }
}
