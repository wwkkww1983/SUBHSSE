using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 隐患整改通知单
    /// </summary>
    public static class RectifyNoticesService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取隐患整改通知单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public static Model.Check_RectifyNotices GetRectifyNoticesById(string rectifyNoticesId)
        {
            return Funs.DB.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNoticesId);
        }

        /// <summary>
        /// 添加隐患整改通知单
        /// </summary>
        /// <param name="rectifyNotices"></param>
        public static void AddRectifyNotices(Model.Check_RectifyNotices rectifyNotices)
        {
            Model.Check_RectifyNotices newRectifyNotices = new Model.Check_RectifyNotices
            {
                RectifyNoticesId = rectifyNotices.RectifyNoticesId,
                ProjectId = rectifyNotices.ProjectId,
                RectifyNoticesCode = rectifyNotices.RectifyNoticesCode,
                UnitId = rectifyNotices.UnitId,
                WorkAreaId = rectifyNotices.WorkAreaId,
                CheckedDate = rectifyNotices.CheckedDate,
                WrongContent = rectifyNotices.WrongContent,
                SignPerson = rectifyNotices.SignPerson,
                SignDate = rectifyNotices.SignDate,
                CompleteStatus = rectifyNotices.CompleteStatus,
                DutyPerson = rectifyNotices.DutyPerson,
                DutyPersonId =rectifyNotices.DutyPersonId,
                CompleteDate = rectifyNotices.CompleteDate,
                IsRectify = rectifyNotices.IsRectify,
                CheckPerson = rectifyNotices.CheckPerson,
                ReCheckDate = rectifyNotices.ReCheckDate,
        };
            db.Check_RectifyNotices.InsertOnSubmit(newRectifyNotices);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRectifyNoticeMenuId, rectifyNotices.ProjectId, rectifyNotices.UnitId, rectifyNotices.RectifyNoticesId, rectifyNotices.CompleteDate);
        }

        /// <summary>
        /// 修改隐患整改通知单
        /// </summary>
        /// <param name="rectifyNotices"></param>
        public static void UpdateRectifyNotices(Model.Check_RectifyNotices rectifyNotices)
        {
            Model.Check_RectifyNotices newRectifyNotices = db.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNotices.RectifyNoticesId);
            if (newRectifyNotices != null)
            {
                newRectifyNotices.ProjectId = rectifyNotices.ProjectId;
                newRectifyNotices.RectifyNoticesCode = rectifyNotices.RectifyNoticesCode;
                newRectifyNotices.UnitId = rectifyNotices.UnitId;
                newRectifyNotices.WorkAreaId = rectifyNotices.WorkAreaId;
                newRectifyNotices.CheckedDate = rectifyNotices.CheckedDate;
                newRectifyNotices.WrongContent = rectifyNotices.WrongContent;
                newRectifyNotices.SignPerson = rectifyNotices.SignPerson;
                newRectifyNotices.SignDate = rectifyNotices.SignDate;
                newRectifyNotices.CompleteStatus = rectifyNotices.CompleteStatus;
                newRectifyNotices.DutyPerson = rectifyNotices.DutyPerson;
                newRectifyNotices.CompleteDate = rectifyNotices.CompleteDate;
                newRectifyNotices.IsRectify = rectifyNotices.IsRectify;
                newRectifyNotices.CheckPerson = rectifyNotices.CheckPerson;
                newRectifyNotices.DutyPersonId = rectifyNotices.DutyPersonId;
                newRectifyNotices.ReCheckDate = rectifyNotices.ReCheckDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除隐患整改通知单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        public static void DeleteRectifyNoticesById(string rectifyNoticesId)
        {
            Model.Check_RectifyNotices rectifyNotices = db.Check_RectifyNotices.FirstOrDefault(e => e.RectifyNoticesId == rectifyNoticesId);
            if (rectifyNotices != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(rectifyNoticesId);
                UploadFileService.DeleteFile(Funs.RootPath, rectifyNotices.AttachUrl);
                CommonService.DeleteAttachFileById(rectifyNoticesId);
                db.Check_RectifyNotices.DeleteOnSubmit(rectifyNotices);
                db.SubmitChanges();
            }
        }
    }
}
