using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 工程暂停令
    /// </summary>
    public static class Check_PauseNoticeService
    {
        /// <summary>
        /// 根据工程暂停令ID获取工程暂停令信息
        /// </summary>
        /// <param name="PauseNoticeName"></param>
        /// <returns></returns>
        public static Model.Check_PauseNotice GetPauseNoticeByPauseNoticeId(string pauseNoticeId)
        {
            return Funs.DB.Check_PauseNotice.FirstOrDefault(e => e.PauseNoticeId == pauseNoticeId);
        }

        /// <summary>
        /// 添加安全工程暂停令
        /// </summary>
        /// <param name="pauseNotice"></param>
        public static void AddPauseNotice(Model.Check_PauseNotice pauseNotice)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_PauseNotice newPauseNotice = new Model.Check_PauseNotice
            {
                PauseNoticeId = pauseNotice.PauseNoticeId,
                PauseNoticeCode = pauseNotice.PauseNoticeCode,
                ProjectId = pauseNotice.ProjectId,
                UnitId = pauseNotice.UnitId,
                CompileDate = pauseNotice.CompileDate,
                SignPerson = pauseNotice.SignPerson,
                ProjectPlace = pauseNotice.ProjectPlace,
                WrongContent = pauseNotice.WrongContent,
                PauseTime = pauseNotice.PauseTime,
                PauseContent = pauseNotice.PauseContent,
                OneContent = pauseNotice.OneContent,
                SecondContent = pauseNotice.SecondContent,
                ThirdContent = pauseNotice.ThirdContent,
                ProjectHeadConfirm = pauseNotice.ProjectHeadConfirm,
                ProjectHeadConfirmId = pauseNotice.ProjectHeadConfirmId,
                IsConfirm = pauseNotice.IsConfirm,
                ConfirmDate = pauseNotice.ConfirmDate,
                AttachUrl = pauseNotice.AttachUrl,
                States = pauseNotice.States,
                CompileMan = pauseNotice.CompileMan,
                SignMan = pauseNotice.SignMan,
                ApproveMan = pauseNotice.ApproveMan
            };

            db.Check_PauseNotice.InsertOnSubmit(newPauseNotice);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectPauseNoticeMenuId, pauseNotice.ProjectId, null, pauseNotice.PauseNoticeId, pauseNotice.PauseTime);
        }

        /// <summary>
        /// 修改安全工程暂停令
        /// </summary>
        /// <param name="pauseNotice"></param>
        public static void UpdatePauseNotice(Model.Check_PauseNotice pauseNotice)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_PauseNotice newPauseNotice = db.Check_PauseNotice.FirstOrDefault(e => e.PauseNoticeId == pauseNotice.PauseNoticeId);
            if (newPauseNotice != null)
            {
                newPauseNotice.CompileDate = pauseNotice.CompileDate;
                newPauseNotice.UnitId = pauseNotice.UnitId;
                newPauseNotice.SignPerson = pauseNotice.SignPerson;
                newPauseNotice.ProjectPlace = pauseNotice.ProjectPlace;
                newPauseNotice.WrongContent = pauseNotice.WrongContent;
                newPauseNotice.PauseTime = pauseNotice.PauseTime;
                newPauseNotice.PauseContent = pauseNotice.PauseContent;
                newPauseNotice.OneContent = pauseNotice.OneContent;
                newPauseNotice.SecondContent = pauseNotice.SecondContent;
                newPauseNotice.ThirdContent = pauseNotice.ThirdContent;
                newPauseNotice.ProjectHeadConfirm = pauseNotice.ProjectHeadConfirm;
                newPauseNotice.ProjectHeadConfirmId = pauseNotice.ProjectHeadConfirm;
                newPauseNotice.IsConfirm = pauseNotice.IsConfirm;
                newPauseNotice.ConfirmDate = pauseNotice.ConfirmDate;
                newPauseNotice.AttachUrl = pauseNotice.AttachUrl;
                newPauseNotice.States = pauseNotice.States;
                newPauseNotice.SignMan = pauseNotice.SignMan;
                newPauseNotice.ApproveMan = pauseNotice.ApproveMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据工程暂停令ID删除对应工程暂停令记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeletePauseNotice(string pauseNoticeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_PauseNotice where x.PauseNoticeId == pauseNoticeId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.PauseNoticeId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.PauseNoticeId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(q.PauseNoticeId);
                db.Check_PauseNotice.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
