using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 全国安全月
    /// </summary>
    public static class SafetyMonthService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取全国安全月信息
        /// </summary>
        /// <param name="SafetyMonthId"></param>
        /// <returns></returns>
        public static Model.SafetyActivities_SafetyMonth GetSafetyMonthById(string SafetyMonthId)
        {
            return Funs.DB.SafetyActivities_SafetyMonth.FirstOrDefault(e => e.SafetyMonthId == SafetyMonthId);
        }

        /// <summary>
        /// 添加全国安全月
        /// </summary>
        /// <param name="SafetyMonth"></param>
        public static void AddSafetyMonth(Model.SafetyActivities_SafetyMonth SafetyMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_SafetyMonth newSafetyMonth = new Model.SafetyActivities_SafetyMonth
            {
                SafetyMonthId = SafetyMonth.SafetyMonthId,
                ProjectId = SafetyMonth.ProjectId,
                Title = SafetyMonth.Title,
                CompileDate = SafetyMonth.CompileDate,
                CompileMan = SafetyMonth.CompileMan,
                Remark = SafetyMonth.Remark,
                AttachUrl = SafetyMonth.AttachUrl,
                SeeFile = SafetyMonth.SeeFile
            };

            db.SafetyActivities_SafetyMonth.InsertOnSubmit(newSafetyMonth);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="SafetyMonth"></param>
        public static void UpdateSafetyMonth(Model.SafetyActivities_SafetyMonth SafetyMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_SafetyMonth newSafetyMonth = db.SafetyActivities_SafetyMonth.FirstOrDefault(e => e.SafetyMonthId == SafetyMonth.SafetyMonthId);
            if (newSafetyMonth != null)
            {
                newSafetyMonth.Title = SafetyMonth.Title;
                newSafetyMonth.CompileDate = SafetyMonth.CompileDate;
                newSafetyMonth.CompileMan = SafetyMonth.CompileMan;
                newSafetyMonth.Remark = SafetyMonth.Remark;
                newSafetyMonth.AttachUrl = SafetyMonth.AttachUrl;
                newSafetyMonth.SeeFile = SafetyMonth.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="SafetyMonthId"></param>
        public static void DeleteSafetyMonthById(string SafetyMonthId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyActivities_SafetyMonth SafetyMonth = db.SafetyActivities_SafetyMonth.FirstOrDefault(e => e.SafetyMonthId == SafetyMonthId);
            if (SafetyMonth != null)
            {
                if (!string.IsNullOrEmpty(SafetyMonth.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, SafetyMonth.AttachUrl);//删除附件
                } 
                db.SafetyActivities_SafetyMonth.DeleteOnSubmit(SafetyMonth);
                db.SubmitChanges();
            }
        }
    }
}