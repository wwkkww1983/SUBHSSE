using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 监理管理文档
    /// </summary>
    public static class FinalFileListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取监理管理文档
        /// </summary>
        /// <param name="FinalFileListId"></param>
        /// <returns></returns>
        public static Model.FinalFileManage_FinalFileList GetFinalFileListById(string fileId)
        {
            return Funs.DB.FinalFileManage_FinalFileList.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加监理管理文档
        /// </summary>
        /// <param name="FinalFileList"></param>
        public static void AddFinalFileList(Model.FinalFileManage_FinalFileList FinalFileList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_FinalFileList newFinalFileList = new Model.FinalFileManage_FinalFileList
            {
                FileId = FinalFileList.FileId,
                ProjectId = FinalFileList.ProjectId,
                FileCode = FinalFileList.FileCode,
                FileName = FinalFileList.FileName,
                KeyWords = FinalFileList.KeyWords,
                FileContent = FinalFileList.FileContent,
                CompileMan = FinalFileList.CompileMan,
                CompileDate = FinalFileList.CompileDate,
                AttachUrl = FinalFileList.AttachUrl,
                States = FinalFileList.States
            };
            db.FinalFileManage_FinalFileList.InsertOnSubmit(newFinalFileList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.FinalFileListMenuId, FinalFileList.ProjectId, null, FinalFileList.FileId, FinalFileList.CompileDate);
        }

        /// <summary>
        /// 修改监理管理文档
        /// </summary>
        /// <param name="FinalFileList"></param>
        public static void UpdateFinalFileList(Model.FinalFileManage_FinalFileList FinalFileList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_FinalFileList newFinalFileList = db.FinalFileManage_FinalFileList.FirstOrDefault(e => e.FileId == FinalFileList.FileId);
            if (newFinalFileList != null)
            {
                newFinalFileList.FileCode = FinalFileList.FileCode;
                newFinalFileList.FileName = FinalFileList.FileName;
                newFinalFileList.KeyWords = FinalFileList.KeyWords;
                newFinalFileList.FileContent = FinalFileList.FileContent;
                newFinalFileList.CompileMan = FinalFileList.CompileMan;
                newFinalFileList.CompileDate = FinalFileList.CompileDate;
                newFinalFileList.AttachUrl = FinalFileList.AttachUrl;
                newFinalFileList.States = FinalFileList.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除监理管理文档
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteFinalFileListById(string FileId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_FinalFileList FinalFileList = db.FinalFileManage_FinalFileList.FirstOrDefault(e => e.FileId == FileId);
            if (FinalFileList != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(FinalFileList.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(FinalFileList.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(FinalFileList.FileId);
                db.FinalFileManage_FinalFileList.DeleteOnSubmit(FinalFileList);
                db.SubmitChanges();
            }
        }
    }
}
