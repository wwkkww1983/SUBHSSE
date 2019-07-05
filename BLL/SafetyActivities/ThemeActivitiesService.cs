using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 主题安全活动
    /// </summary>
    public static class ThemeActivitiesService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取主题安全活动信息
        /// </summary>
        /// <param name="ThemeActivitiesId"></param>
        /// <returns></returns>
        public static Model.SafetyActivities_ThemeActivities GetThemeActivitiesById(string ThemeActivitiesId)
        {
            return Funs.DB.SafetyActivities_ThemeActivities.FirstOrDefault(e => e.ThemeActivitiesId == ThemeActivitiesId);
        }

        /// <summary>
        /// 添加主题安全活动
        /// </summary>
        /// <param name="ThemeActivities"></param>
        public static void AddThemeActivities(Model.SafetyActivities_ThemeActivities ThemeActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_ThemeActivities newThemeActivities = new Model.SafetyActivities_ThemeActivities
            {
                ThemeActivitiesId = ThemeActivities.ThemeActivitiesId,
                ProjectId = ThemeActivities.ProjectId,
                Title = ThemeActivities.Title,
                CompileDate = ThemeActivities.CompileDate,
                CompileMan = ThemeActivities.CompileMan,
                Remark = ThemeActivities.Remark,
                AttachUrl = ThemeActivities.AttachUrl,
                SeeFile = ThemeActivities.SeeFile
            };

            db.SafetyActivities_ThemeActivities.InsertOnSubmit(newThemeActivities);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ThemeActivities"></param>
        public static void UpdateThemeActivities(Model.SafetyActivities_ThemeActivities ThemeActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_ThemeActivities newThemeActivities = db.SafetyActivities_ThemeActivities.FirstOrDefault(e => e.ThemeActivitiesId == ThemeActivities.ThemeActivitiesId);
            if (newThemeActivities != null)
            {
                newThemeActivities.Title = ThemeActivities.Title;
                newThemeActivities.CompileDate = ThemeActivities.CompileDate;
                newThemeActivities.CompileMan = ThemeActivities.CompileMan;
                newThemeActivities.Remark = ThemeActivities.Remark;
                newThemeActivities.AttachUrl = ThemeActivities.AttachUrl;
                newThemeActivities.SeeFile = ThemeActivities.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ThemeActivitiesId"></param>
        public static void DeleteThemeActivitiesById(string ThemeActivitiesId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_ThemeActivities ThemeActivities = db.SafetyActivities_ThemeActivities.FirstOrDefault(e => e.ThemeActivitiesId == ThemeActivitiesId);
            if (ThemeActivities != null)
            {
                if (!string.IsNullOrEmpty(ThemeActivities.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, ThemeActivities.AttachUrl);//删除附件
                } 
                db.SafetyActivities_ThemeActivities.DeleteOnSubmit(ThemeActivities);
                db.SubmitChanges();
            }
        }
    }
}