using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 其他管理文档
    /// </summary>
    public static class OtherDocumentListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取其他管理文档
        /// </summary>
        /// <param name="OtherDocumentListId"></param>
        /// <returns></returns>
        public static Model.FinalFileManage_OtherDocumentList GetOtherDocumentListById(string fileId)
        {
            return Funs.DB.FinalFileManage_OtherDocumentList.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加其他管理文档
        /// </summary>
        /// <param name="OtherDocumentList"></param>
        public static void AddOtherDocumentList(Model.FinalFileManage_OtherDocumentList OtherDocumentList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OtherDocumentList newOtherDocumentList = new Model.FinalFileManage_OtherDocumentList
            {
                FileId = OtherDocumentList.FileId,
                ProjectId = OtherDocumentList.ProjectId,
                FileCode = OtherDocumentList.FileCode,
                FileName = OtherDocumentList.FileName,
                KeyWords = OtherDocumentList.KeyWords,
                FileContent = OtherDocumentList.FileContent,
                CompileMan = OtherDocumentList.CompileMan,
                CompileDate = OtherDocumentList.CompileDate,
                AttachUrl = OtherDocumentList.AttachUrl,
                States = OtherDocumentList.States
            };
            db.FinalFileManage_OtherDocumentList.InsertOnSubmit(newOtherDocumentList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.OtherDocumentListMenuId, OtherDocumentList.ProjectId, null, OtherDocumentList.FileId, OtherDocumentList.CompileDate);
        }

        /// <summary>
        /// 修改其他管理文档
        /// </summary>
        /// <param name="OtherDocumentList"></param>
        public static void UpdateOtherDocumentList(Model.FinalFileManage_OtherDocumentList OtherDocumentList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OtherDocumentList newOtherDocumentList = db.FinalFileManage_OtherDocumentList.FirstOrDefault(e => e.FileId == OtherDocumentList.FileId);
            if (newOtherDocumentList != null)
            {
                newOtherDocumentList.FileCode = OtherDocumentList.FileCode;
                newOtherDocumentList.FileName = OtherDocumentList.FileName;
                newOtherDocumentList.KeyWords = OtherDocumentList.KeyWords;
                newOtherDocumentList.FileContent = OtherDocumentList.FileContent;
                newOtherDocumentList.CompileMan = OtherDocumentList.CompileMan;
                newOtherDocumentList.CompileDate = OtherDocumentList.CompileDate;
                newOtherDocumentList.AttachUrl = OtherDocumentList.AttachUrl;
                newOtherDocumentList.States = OtherDocumentList.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除其他管理文档
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteOtherDocumentListById(string FileId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OtherDocumentList OtherDocumentList = db.FinalFileManage_OtherDocumentList.FirstOrDefault(e => e.FileId == FileId);
            if (OtherDocumentList != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(OtherDocumentList.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(OtherDocumentList.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(OtherDocumentList.FileId);
                db.FinalFileManage_OtherDocumentList.DeleteOnSubmit(OtherDocumentList);
                db.SubmitChanges();
            }
        }
    }
}
