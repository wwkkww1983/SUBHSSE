using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 其他安全活动
    /// </summary>
    public static class OtherActivitiesService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取其他安全活动信息
        /// </summary>
        /// <param name="OtherActivitiesId"></param>
        /// <returns></returns>
        public static Model.SafetyActivities_OtherActivities GetOtherActivitiesById(string OtherActivitiesId)
        {
            return Funs.DB.SafetyActivities_OtherActivities.FirstOrDefault(e => e.OtherActivitiesId == OtherActivitiesId);
        }

        /// <summary>
        /// 添加其他安全活动
        /// </summary>
        /// <param name="OtherActivities"></param>
        public static void AddOtherActivities(Model.SafetyActivities_OtherActivities OtherActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_OtherActivities newOtherActivities = new Model.SafetyActivities_OtherActivities
            {
                OtherActivitiesId = OtherActivities.OtherActivitiesId,
                ProjectId = OtherActivities.ProjectId,
                Title = OtherActivities.Title,
                CompileDate = OtherActivities.CompileDate,
                CompileMan = OtherActivities.CompileMan,
                Remark = OtherActivities.Remark,
                AttachUrl = OtherActivities.AttachUrl,
                SeeFile = OtherActivities.SeeFile
            };

            db.SafetyActivities_OtherActivities.InsertOnSubmit(newOtherActivities);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="OtherActivities"></param>
        public static void UpdateOtherActivities(Model.SafetyActivities_OtherActivities OtherActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_OtherActivities newOtherActivities = db.SafetyActivities_OtherActivities.FirstOrDefault(e => e.OtherActivitiesId == OtherActivities.OtherActivitiesId);
            if (newOtherActivities != null)
            {
                newOtherActivities.Title = OtherActivities.Title;
                newOtherActivities.CompileDate = OtherActivities.CompileDate;
                newOtherActivities.CompileMan = OtherActivities.CompileMan;
                newOtherActivities.Remark = OtherActivities.Remark;
                newOtherActivities.AttachUrl = OtherActivities.AttachUrl;
                newOtherActivities.SeeFile = OtherActivities.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="OtherActivitiesId"></param>
        public static void DeleteOtherActivitiesById(string OtherActivitiesId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_OtherActivities OtherActivities = db.SafetyActivities_OtherActivities.FirstOrDefault(e => e.OtherActivitiesId == OtherActivitiesId);
            if (OtherActivities != null)
            {
                if (!string.IsNullOrEmpty(OtherActivities.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, OtherActivities.AttachUrl);//删除附件
                } 
                db.SafetyActivities_OtherActivities.DeleteOnSubmit(OtherActivities);
                db.SubmitChanges();
            }
        }
    }
}