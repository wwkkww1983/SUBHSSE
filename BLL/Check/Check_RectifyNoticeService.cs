using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 隐患通知单
    /// </summary>
    public static class Check_RectifyNoticeService
    {
        /// <summary>
        /// 根据隐患通知单ID获取隐患通知单信息
        /// </summary>
        /// <param name="RectifyNoticeName"></param>
        /// <returns></returns>
        public static Model.Check_RectifyNotice GetRectifyNoticeByRectifyNoticeId(string rectifyNoticeId)
        {
            return Funs.DB.Check_RectifyNotice.FirstOrDefault(e => e.RectifyNoticeId == rectifyNoticeId);
        }

        /// <summary>
        /// 添加安全隐患通知单
        /// </summary>
        /// <param name="rectifyNotice"></param>
        public static void AddRectifyNotice(Model.Check_RectifyNotice rectifyNotice)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_RectifyNotice newRectifyNotice = new Model.Check_RectifyNotice
            {
                RectifyNoticeId = rectifyNotice.RectifyNoticeId,
                RectifyNoticeCode = rectifyNotice.RectifyNoticeCode,
                ProjectId = rectifyNotice.ProjectId,
                UnitId = rectifyNotice.UnitId,
                CheckArea = rectifyNotice.CheckArea,
                CheckedTime = rectifyNotice.CheckedTime,
                WrongContent = rectifyNotice.WrongContent,
                SignPerson = rectifyNotice.SignPerson,
                RectifyLimit = rectifyNotice.RectifyLimit,
                CompleteStatus = rectifyNotice.CompleteStatus,
                DutyPerson = rectifyNotice.DutyPerson,
                CompleteDate = rectifyNotice.CompleteDate,
                IsRectify = rectifyNotice.IsRectify,
                CheckPerson = rectifyNotice.CheckPerson,
                AttachUrl1 = rectifyNotice.AttachUrl1,
                AttachUrl2 = rectifyNotice.AttachUrl2,
                AttachUrl3 = rectifyNotice.AttachUrl3,
                ChangeContent = rectifyNotice.ChangeContent,
                HSEManager = rectifyNotice.HSEManager,
                ChangeDate = rectifyNotice.ChangeDate,
                CheckDate = rectifyNotice.CheckDate,
                CheckStation = rectifyNotice.CheckStation,
                States = rectifyNotice.States,
                CompileMan = rectifyNotice.CompileMan,
                RectifyType = rectifyNotice.RectifyType,
                RectifyState = rectifyNotice.RectifyState
            };
            db.Check_RectifyNotice.InsertOnSubmit(newRectifyNotice);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectRectifyNoticeMenuId, rectifyNotice.ProjectId, null, rectifyNotice.RectifyNoticeId, rectifyNotice.CheckedTime);
        }

        /// <summary>
        /// 修改安全隐患通知单
        /// </summary>
        /// <param name="rectifyNotice"></param>
        public static void UpdateRectifyNotice(Model.Check_RectifyNotice rectifyNotice)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_RectifyNotice newRectifyNotice = db.Check_RectifyNotice.FirstOrDefault(e => e.RectifyNoticeId == rectifyNotice.RectifyNoticeId);
            if (newRectifyNotice != null)
            {
                newRectifyNotice.UnitId = rectifyNotice.UnitId;
                newRectifyNotice.CheckArea = rectifyNotice.CheckArea;
                newRectifyNotice.CheckedTime = rectifyNotice.CheckedTime;
                newRectifyNotice.WrongContent = rectifyNotice.WrongContent;
                newRectifyNotice.SignPerson = rectifyNotice.SignPerson;
                newRectifyNotice.RectifyLimit = rectifyNotice.RectifyLimit;
                newRectifyNotice.CompleteStatus = rectifyNotice.CompleteStatus;
                newRectifyNotice.DutyPerson = rectifyNotice.DutyPerson;
                newRectifyNotice.CompleteDate = rectifyNotice.CompleteDate;
                newRectifyNotice.IsRectify = rectifyNotice.IsRectify;
                newRectifyNotice.CheckPerson = rectifyNotice.CheckPerson;
                newRectifyNotice.AttachUrl1 = rectifyNotice.AttachUrl1;
                newRectifyNotice.AttachUrl2 = rectifyNotice.AttachUrl2;
                newRectifyNotice.AttachUrl3 = rectifyNotice.AttachUrl3;
                newRectifyNotice.ChangeContent = rectifyNotice.ChangeContent;
                newRectifyNotice.HSEManager = rectifyNotice.HSEManager;
                newRectifyNotice.ChangeDate = rectifyNotice.ChangeDate;
                newRectifyNotice.CheckDate = rectifyNotice.CheckDate;
                newRectifyNotice.CheckStation = rectifyNotice.CheckStation;
                newRectifyNotice.States = rectifyNotice.States;

                newRectifyNotice.RectifyType = rectifyNotice.RectifyType;
                newRectifyNotice.RectifyState = rectifyNotice.RectifyState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据隐患通知单ID删除对应隐患通知单记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteRectifyNotice(string rectifyNoticeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_RectifyNotice where x.RectifyNoticeId == rectifyNoticeId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.RectifyNoticeId);
                ////删除附件表
                //BLL.CommonService.DeleteAttachFileById(q.RectifyNoticeId);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, q.AttachUrl1);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, q.AttachUrl2);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, q.AttachUrl3);
                ///删除工程师日志收集记录
                var flowOperate = from x in db.Sys_FlowOperate where x.DataId == q.RectifyNoticeId select x;
                if (flowOperate.Count() > 0)
                {
                    string value22 = "隐患整改单";  
                    if (!string.IsNullOrEmpty(q.WrongContent))
                    {
                        value22 = q.WrongContent;
                    }
                    foreach (var item in flowOperate)
                    {

                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item.OperaterId, item.OperaterTime, "22", value22, Const.BtnDelete, 1);
                    }
                    ////删除审核流程表
                    BLL.CommonService.DeleteFlowOperateByID(q.RectifyNoticeId);
                } 
                db.Check_RectifyNotice.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
