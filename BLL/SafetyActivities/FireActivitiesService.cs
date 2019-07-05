using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 119消防
    /// </summary>
    public static class FireActivitiesService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取119消防信息
        /// </summary>
        /// <param name="FireActivitiesId"></param>
        /// <returns></returns>
        public static Model.SafetyActivities_FireActivities GetFireActivitiesById(string FireActivitiesId)
        {
            return Funs.DB.SafetyActivities_FireActivities.FirstOrDefault(e => e.FireActivitiesId == FireActivitiesId);
        }

        /// <summary>
        /// 添加119消防
        /// </summary>
        /// <param name="FireActivities"></param>
        public static void AddFireActivities(Model.SafetyActivities_FireActivities FireActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_FireActivities newFireActivities = new Model.SafetyActivities_FireActivities
            {
                FireActivitiesId = FireActivities.FireActivitiesId,
                ProjectId = FireActivities.ProjectId,
                Title = FireActivities.Title,
                CompileDate = FireActivities.CompileDate,
                CompileMan = FireActivities.CompileMan,
                Remark = FireActivities.Remark,
                AttachUrl = FireActivities.AttachUrl,
                SeeFile = FireActivities.SeeFile
            };

            db.SafetyActivities_FireActivities.InsertOnSubmit(newFireActivities);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="FireActivities"></param>
        public static void UpdateFireActivities(Model.SafetyActivities_FireActivities FireActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_FireActivities newFireActivities = db.SafetyActivities_FireActivities.FirstOrDefault(e => e.FireActivitiesId == FireActivities.FireActivitiesId);
            if (newFireActivities != null)
            {
                newFireActivities.Title = FireActivities.Title;
                newFireActivities.CompileDate = FireActivities.CompileDate;
                newFireActivities.CompileMan = FireActivities.CompileMan;
                newFireActivities.Remark = FireActivities.Remark;
                newFireActivities.AttachUrl = FireActivities.AttachUrl;
                newFireActivities.SeeFile = FireActivities.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="FireActivitiesId"></param>
        public static void DeleteFireActivitiesById(string FireActivitiesId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_FireActivities FireActivities = db.SafetyActivities_FireActivities.FirstOrDefault(e => e.FireActivitiesId == FireActivitiesId);
            if (FireActivities != null)
            {
                if (!string.IsNullOrEmpty(FireActivities.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, FireActivities.AttachUrl);//删除附件
                } 
                db.SafetyActivities_FireActivities.DeleteOnSubmit(FireActivities);
                db.SubmitChanges();
            }
        }
    }
}