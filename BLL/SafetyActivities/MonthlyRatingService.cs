using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 月度安全评比
    /// </summary>
    public static class MonthlyRatingService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取月度安全评比信息
        /// </summary>
        /// <param name="MonthlyRatingId"></param>
        /// <returns></returns>
        public static Model.SafetyActivities_MonthlyRating GetMonthlyRatingById(string MonthlyRatingId)
        {
            return Funs.DB.SafetyActivities_MonthlyRating.FirstOrDefault(e => e.MonthlyRatingId == MonthlyRatingId);
        }

        /// <summary>
        /// 添加月度安全评比
        /// </summary>
        /// <param name="MonthlyRating"></param>
        public static void AddMonthlyRating(Model.SafetyActivities_MonthlyRating MonthlyRating)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_MonthlyRating newMonthlyRating = new Model.SafetyActivities_MonthlyRating
            {
                MonthlyRatingId = MonthlyRating.MonthlyRatingId,
                ProjectId = MonthlyRating.ProjectId,
                Title = MonthlyRating.Title,
                CompileDate = MonthlyRating.CompileDate,
                CompileMan = MonthlyRating.CompileMan,
                Remark = MonthlyRating.Remark,
                AttachUrl = MonthlyRating.AttachUrl,
                SeeFile = MonthlyRating.SeeFile
            };

            db.SafetyActivities_MonthlyRating.InsertOnSubmit(newMonthlyRating);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="MonthlyRating"></param>
        public static void UpdateMonthlyRating(Model.SafetyActivities_MonthlyRating MonthlyRating)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_MonthlyRating newMonthlyRating = db.SafetyActivities_MonthlyRating.FirstOrDefault(e => e.MonthlyRatingId == MonthlyRating.MonthlyRatingId);
            if (newMonthlyRating != null)
            {
                newMonthlyRating.Title = MonthlyRating.Title;
                newMonthlyRating.CompileDate = MonthlyRating.CompileDate;
                newMonthlyRating.CompileMan = MonthlyRating.CompileMan;
                newMonthlyRating.Remark = MonthlyRating.Remark;
                newMonthlyRating.AttachUrl = MonthlyRating.AttachUrl;
                newMonthlyRating.SeeFile = MonthlyRating.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="MonthlyRatingId"></param>
        public static void DeleteMonthlyRatingById(string MonthlyRatingId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_MonthlyRating MonthlyRating = db.SafetyActivities_MonthlyRating.FirstOrDefault(e => e.MonthlyRatingId == MonthlyRatingId);
            if (MonthlyRating != null)
            {
                if (!string.IsNullOrEmpty(MonthlyRating.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, MonthlyRating.AttachUrl);//删除附件
                } 
                db.SafetyActivities_MonthlyRating.DeleteOnSubmit(MonthlyRating);
                db.SubmitChanges();
            }
        }
    }
}