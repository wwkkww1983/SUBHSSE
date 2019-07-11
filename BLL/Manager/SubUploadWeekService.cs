using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商上传周报
    /// </summary>
    public static class SubUploadWeekService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取分包商上传周报
        /// </summary>
        /// <param name="subUploadWeekId"></param>
        /// <returns></returns>
        public static Model.Manager_SubUploadWeek GetSubUploadWeekById(string subUploadWeekId)
        {
            return Funs.DB.Manager_SubUploadWeek.FirstOrDefault(e => e.SubUploadWeekId == subUploadWeekId);
        }

        /// <summary>
        /// 添加分包商上传周报
        /// </summary>
        /// <param name="subUploadWeek"></param>
        public static void AddSubUploadWeek(Model.Manager_SubUploadWeek subUploadWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubUploadWeek newSubUploadWeek = new Model.Manager_SubUploadWeek
            {
                SubUploadWeekId = subUploadWeek.SubUploadWeekId,
                ProjectId = subUploadWeek.ProjectId,
                UnitId = subUploadWeek.UnitId,
                StartDate = subUploadWeek.StartDate,
                EndDate = subUploadWeek.EndDate,
                PersonWeekNum = subUploadWeek.PersonWeekNum,
                ManHours = subUploadWeek.ManHours,
                TotalManHours = subUploadWeek.TotalManHours,
                StartWorkDate = subUploadWeek.StartWorkDate,
                EndWorkDate = subUploadWeek.EndWorkDate,
                Remark = subUploadWeek.Remark,
                HappenNum1 = subUploadWeek.HappenNum1,
                HappenNum2 = subUploadWeek.HappenNum2,
                HappenNum3 = subUploadWeek.HappenNum3,
                HappenNum4 = subUploadWeek.HappenNum4,
                HappenNum5 = subUploadWeek.HappenNum5,
                HappenNum6 = subUploadWeek.HappenNum6,
                HurtPersonNum1 = subUploadWeek.HurtPersonNum1,
                HurtPersonNum2 = subUploadWeek.HurtPersonNum2,
                HurtPersonNum3 = subUploadWeek.HurtPersonNum3,
                HurtPersonNum4 = subUploadWeek.HurtPersonNum4,
                HurtPersonNum5 = subUploadWeek.HurtPersonNum5,
                HurtPersonNum6 = subUploadWeek.HurtPersonNum6,
                LossHours1 = subUploadWeek.LossHours1,
                LossHours2 = subUploadWeek.LossHours2,
                LossHours3 = subUploadWeek.LossHours3,
                LossHours4 = subUploadWeek.LossHours4,
                LossHours5 = subUploadWeek.LossHours5,
                LossHours6 = subUploadWeek.LossHours6,
                LossMoney1 = subUploadWeek.LossMoney1,
                LossMoney2 = subUploadWeek.LossMoney2,
                LossMoney3 = subUploadWeek.LossMoney3,
                LossMoney4 = subUploadWeek.LossMoney4,
                LossMoney5 = subUploadWeek.LossMoney5,
                LossMoney6 = subUploadWeek.LossMoney6,
                HSEPersonNum = subUploadWeek.HSEPersonNum,
                CheckNum = subUploadWeek.CheckNum,
                EmergencyDrillNum = subUploadWeek.EmergencyDrillNum,
                LicenseNum = subUploadWeek.LicenseNum,
                TrainNum = subUploadWeek.TrainNum,
                HazardNum = subUploadWeek.HazardNum,
                MeetingNum = subUploadWeek.MeetingNum,
                HiddenDanger1 = subUploadWeek.HiddenDanger1,
                HiddenDanger2 = subUploadWeek.HiddenDanger2,
                HiddenDanger3 = subUploadWeek.HiddenDanger3,
                RectifyNum1 = subUploadWeek.RectifyNum1,
                RectifyNum2 = subUploadWeek.RectifyNum2,
                RectifyNum3 = subUploadWeek.RectifyNum3,
                ThisWorkSummary = subUploadWeek.ThisWorkSummary,
                NextWorkPlan = subUploadWeek.NextWorkPlan,
                OtherProblems = subUploadWeek.OtherProblems,
                CompileMan = subUploadWeek.CompileMan,
                CompileDate = subUploadWeek.CompileDate
            };
            db.Manager_SubUploadWeek.InsertOnSubmit(newSubUploadWeek);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分包商上传周报
        /// </summary>
        /// <param name="subUploadWeek"></param>
        public static void UpdateSubUploadWeek(Model.Manager_SubUploadWeek subUploadWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubUploadWeek newSubUploadWeek = db.Manager_SubUploadWeek.FirstOrDefault(e => e.SubUploadWeekId == subUploadWeek.SubUploadWeekId);
            if (newSubUploadWeek != null)
            {
                newSubUploadWeek.StartDate = subUploadWeek.StartDate;
                newSubUploadWeek.EndDate = subUploadWeek.EndDate;
                newSubUploadWeek.PersonWeekNum = subUploadWeek.PersonWeekNum;
                newSubUploadWeek.ManHours = subUploadWeek.ManHours;
                newSubUploadWeek.TotalManHours = subUploadWeek.TotalManHours;
                newSubUploadWeek.StartWorkDate = subUploadWeek.StartWorkDate;
                newSubUploadWeek.EndWorkDate = subUploadWeek.EndWorkDate;
                newSubUploadWeek.Remark = subUploadWeek.Remark;
                newSubUploadWeek.HappenNum1 = subUploadWeek.HappenNum1;
                newSubUploadWeek.HappenNum2 = subUploadWeek.HappenNum2;
                newSubUploadWeek.HappenNum3 = subUploadWeek.HappenNum3;
                newSubUploadWeek.HappenNum4 = subUploadWeek.HappenNum4;
                newSubUploadWeek.HappenNum5 = subUploadWeek.HappenNum5;
                newSubUploadWeek.HappenNum6 = subUploadWeek.HappenNum6;
                newSubUploadWeek.HurtPersonNum1 = subUploadWeek.HurtPersonNum1;
                newSubUploadWeek.HurtPersonNum2 = subUploadWeek.HurtPersonNum2;
                newSubUploadWeek.HurtPersonNum3 = subUploadWeek.HurtPersonNum3;
                newSubUploadWeek.HurtPersonNum4 = subUploadWeek.HurtPersonNum4;
                newSubUploadWeek.HurtPersonNum5 = subUploadWeek.HurtPersonNum5;
                newSubUploadWeek.HurtPersonNum6 = subUploadWeek.HurtPersonNum6;
                newSubUploadWeek.LossHours1 = subUploadWeek.LossHours1;
                newSubUploadWeek.LossHours2 = subUploadWeek.LossHours2;
                newSubUploadWeek.LossHours3 = subUploadWeek.LossHours3;
                newSubUploadWeek.LossHours4 = subUploadWeek.LossHours4;
                newSubUploadWeek.LossHours5 = subUploadWeek.LossHours5;
                newSubUploadWeek.LossHours6 = subUploadWeek.LossHours6;
                newSubUploadWeek.LossMoney1 = subUploadWeek.LossMoney1;
                newSubUploadWeek.LossMoney2 = subUploadWeek.LossMoney2;
                newSubUploadWeek.LossMoney3 = subUploadWeek.LossMoney3;
                newSubUploadWeek.LossMoney4 = subUploadWeek.LossMoney4;
                newSubUploadWeek.LossMoney5 = subUploadWeek.LossMoney5;
                newSubUploadWeek.LossMoney6 = subUploadWeek.LossMoney6;
                newSubUploadWeek.HSEPersonNum = subUploadWeek.HSEPersonNum;
                newSubUploadWeek.CheckNum = subUploadWeek.CheckNum;
                newSubUploadWeek.EmergencyDrillNum = subUploadWeek.EmergencyDrillNum;
                newSubUploadWeek.LicenseNum = subUploadWeek.LicenseNum;
                newSubUploadWeek.TrainNum = subUploadWeek.TrainNum;
                newSubUploadWeek.HazardNum = subUploadWeek.HazardNum;
                newSubUploadWeek.MeetingNum = subUploadWeek.MeetingNum;
                newSubUploadWeek.HiddenDanger1 = subUploadWeek.HiddenDanger1;
                newSubUploadWeek.HiddenDanger2 = subUploadWeek.HiddenDanger2;
                newSubUploadWeek.HiddenDanger3 = subUploadWeek.HiddenDanger3;
                newSubUploadWeek.RectifyNum1 = subUploadWeek.RectifyNum1;
                newSubUploadWeek.RectifyNum2 = subUploadWeek.RectifyNum2;
                newSubUploadWeek.RectifyNum3 = subUploadWeek.RectifyNum3;
                newSubUploadWeek.ThisWorkSummary = subUploadWeek.ThisWorkSummary;
                newSubUploadWeek.NextWorkPlan = subUploadWeek.NextWorkPlan;
                newSubUploadWeek.OtherProblems = subUploadWeek.OtherProblems;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商上传周报
        /// </summary>
        /// <param name="subUploadWeekId"></param>
        public static void DeleteSubUploadWeekById(string subUploadWeekId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubUploadWeek subUploadWeek = db.Manager_SubUploadWeek.FirstOrDefault(e => e.SubUploadWeekId == subUploadWeekId);
            if (subUploadWeek != null)
            {
                CommonService.DeleteFlowOperateByID(subUploadWeekId);
                db.Manager_SubUploadWeek.DeleteOnSubmit(subUploadWeek);
                db.SubmitChanges();
            }
        }
    }
}
