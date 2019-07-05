using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 业主管理文档
    /// </summary>
    public static class OwnerFinalFileService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取业主管理文档
        /// </summary>
        /// <param name="OwnerFinalFileId"></param>
        /// <returns></returns>
        public static Model.FinalFileManage_OwnerFinalFile GetOwnerFinalFileById(string fileId)
        {
            return Funs.DB.FinalFileManage_OwnerFinalFile.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加业主管理文档
        /// </summary>
        /// <param name="OwnerFinalFile"></param>
        public static void AddOwnerFinalFile(Model.FinalFileManage_OwnerFinalFile OwnerFinalFile)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OwnerFinalFile newOwnerFinalFile = new Model.FinalFileManage_OwnerFinalFile
            {
                FileId = OwnerFinalFile.FileId,
                ProjectId = OwnerFinalFile.ProjectId,
                FileCode = OwnerFinalFile.FileCode,
                FileName = OwnerFinalFile.FileName,
                KeyWords = OwnerFinalFile.KeyWords,
                FileContent = OwnerFinalFile.FileContent,
                CompileMan = OwnerFinalFile.CompileMan,
                CompileDate = OwnerFinalFile.CompileDate,
                AttachUrl = OwnerFinalFile.AttachUrl,
                States = OwnerFinalFile.States
            };
            db.FinalFileManage_OwnerFinalFile.InsertOnSubmit(newOwnerFinalFile);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.OwnerFinalFileMenuId, OwnerFinalFile.ProjectId, null, OwnerFinalFile.FileId, OwnerFinalFile.CompileDate);
        }

        /// <summary>
        /// 修改业主管理文档
        /// </summary>
        /// <param name="OwnerFinalFile"></param>
        public static void UpdateOwnerFinalFile(Model.FinalFileManage_OwnerFinalFile OwnerFinalFile)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OwnerFinalFile newOwnerFinalFile = db.FinalFileManage_OwnerFinalFile.FirstOrDefault(e => e.FileId == OwnerFinalFile.FileId);
            if (newOwnerFinalFile != null)
            {
                newOwnerFinalFile.FileCode = OwnerFinalFile.FileCode;
                newOwnerFinalFile.FileName = OwnerFinalFile.FileName;
                newOwnerFinalFile.KeyWords = OwnerFinalFile.KeyWords;
                newOwnerFinalFile.FileContent = OwnerFinalFile.FileContent;
                newOwnerFinalFile.CompileMan = OwnerFinalFile.CompileMan;
                newOwnerFinalFile.CompileDate = OwnerFinalFile.CompileDate;
                newOwnerFinalFile.AttachUrl = OwnerFinalFile.AttachUrl;
                newOwnerFinalFile.States = OwnerFinalFile.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除业主管理文档
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteOwnerFinalFileById(string FileId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_OwnerFinalFile OwnerFinalFile = db.FinalFileManage_OwnerFinalFile.FirstOrDefault(e => e.FileId == FileId);
            if (OwnerFinalFile != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(OwnerFinalFile.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(OwnerFinalFile.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(OwnerFinalFile.FileId);
                db.FinalFileManage_OwnerFinalFile.DeleteOnSubmit(OwnerFinalFile);
                db.SubmitChanges();
            }
        }
    }
}
