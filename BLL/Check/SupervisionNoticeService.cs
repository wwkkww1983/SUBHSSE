using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 监理整改通知单
    /// </summary>
    public static class SupervisionNoticeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取监理整改通知单
        /// </summary>
        /// <param name="SupervisionNoticeId"></param>
        /// <returns></returns>
        public static Model.Check_SupervisionNotice GetSupervisionNoticeById(string SupervisionNoticeId)
        {
            return Funs.DB.Check_SupervisionNotice.FirstOrDefault(e => e.SupervisionNoticeId == SupervisionNoticeId);
        }

        /// <summary>
        /// 添加监理整改通知单
        /// </summary>
        /// <param name="SupervisionNotice"></param>
        public static void AddSupervisionNotice(Model.Check_SupervisionNotice SupervisionNotice)
        {
            Model.Check_SupervisionNotice newSupervisionNotice = new Model.Check_SupervisionNotice
            {
                SupervisionNoticeId = SupervisionNotice.SupervisionNoticeId,
                ProjectId = SupervisionNotice.ProjectId,
                SupervisionNoticeCode = SupervisionNotice.SupervisionNoticeCode,
                UnitId = SupervisionNotice.UnitId,
                WorkAreaId = SupervisionNotice.WorkAreaId,
                CheckedDate = SupervisionNotice.CheckedDate,
                WrongContent = SupervisionNotice.WrongContent,
                SignPerson = SupervisionNotice.SignPerson,
                SignDate = SupervisionNotice.SignDate,
                CompleteStatus = SupervisionNotice.CompleteStatus,
                DutyPerson = SupervisionNotice.DutyPerson,
                CompleteDate = SupervisionNotice.CompleteDate,
                IsRectify = SupervisionNotice.IsRectify,
                CheckPerson = SupervisionNotice.CheckPerson
            };
            db.Check_SupervisionNotice.InsertOnSubmit(newSupervisionNotice);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectSupervisionNoticeMenuId, SupervisionNotice.ProjectId, SupervisionNotice.UnitId, SupervisionNotice.SupervisionNoticeId, SupervisionNotice.CompleteDate);
        }

        /// <summary>
        /// 修改监理整改通知单
        /// </summary>
        /// <param name="SupervisionNotice"></param>
        public static void UpdateSupervisionNotice(Model.Check_SupervisionNotice SupervisionNotice)
        {
            Model.Check_SupervisionNotice newSupervisionNotice = db.Check_SupervisionNotice.FirstOrDefault(e => e.SupervisionNoticeId == SupervisionNotice.SupervisionNoticeId);
            if (newSupervisionNotice != null)
            {
                newSupervisionNotice.ProjectId = SupervisionNotice.ProjectId;
                newSupervisionNotice.SupervisionNoticeCode = SupervisionNotice.SupervisionNoticeCode;
                newSupervisionNotice.UnitId = SupervisionNotice.UnitId;
                newSupervisionNotice.WorkAreaId = SupervisionNotice.WorkAreaId;
                newSupervisionNotice.CheckedDate = SupervisionNotice.CheckedDate;
                newSupervisionNotice.WrongContent = SupervisionNotice.WrongContent;
                newSupervisionNotice.SignPerson = SupervisionNotice.SignPerson;
                newSupervisionNotice.SignDate = SupervisionNotice.SignDate;
                newSupervisionNotice.CompleteStatus = SupervisionNotice.CompleteStatus;
                newSupervisionNotice.DutyPerson = SupervisionNotice.DutyPerson;
                newSupervisionNotice.CompleteDate = SupervisionNotice.CompleteDate;
                newSupervisionNotice.IsRectify = SupervisionNotice.IsRectify;
                newSupervisionNotice.CheckPerson = SupervisionNotice.CheckPerson;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除监理整改通知单
        /// </summary>
        /// <param name="SupervisionNoticeId"></param>
        public static void DeleteSupervisionNoticeById(string SupervisionNoticeId)
        {
            Model.Check_SupervisionNotice SupervisionNotice = db.Check_SupervisionNotice.FirstOrDefault(e => e.SupervisionNoticeId == SupervisionNoticeId);
            if (SupervisionNotice != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(SupervisionNoticeId);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, SupervisionNotice.AttachUrl);
                BLL.CommonService.DeleteAttachFileById(SupervisionNoticeId);
                db.Check_SupervisionNotice.DeleteOnSubmit(SupervisionNotice);
                db.SubmitChanges();
            }
        }
    }
}
