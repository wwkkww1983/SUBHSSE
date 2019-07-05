using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 已定稿文件
    /// </summary>
    public static class HSEFinalFileListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取已定稿文件
        /// </summary>
        /// <param name="HSEFinalFileListId"></param>
        /// <returns></returns>
        public static Model.FinalFileManage_HSEFinalFileList GetHSEFinalFileListById(string fileId)
        {
            return Funs.DB.FinalFileManage_HSEFinalFileList.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加已定稿文件
        /// </summary>
        /// <param name="HSEFinalFileList"></param>
        public static void AddHSEFinalFileList(Model.FinalFileManage_HSEFinalFileList HSEFinalFileList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_HSEFinalFileList newHSEFinalFileList = new Model.FinalFileManage_HSEFinalFileList
            {
                FileId = HSEFinalFileList.FileId,
                ProjectId = HSEFinalFileList.ProjectId,
                FileCode = HSEFinalFileList.FileCode,
                FileName = HSEFinalFileList.FileName,
                KeyWords = HSEFinalFileList.KeyWords,
                FileContent = HSEFinalFileList.FileContent,
                CompileMan = HSEFinalFileList.CompileMan,
                CompileDate = HSEFinalFileList.CompileDate,
                AttachUrl = HSEFinalFileList.AttachUrl,
                States = HSEFinalFileList.States
            };
            db.FinalFileManage_HSEFinalFileList.InsertOnSubmit(newHSEFinalFileList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.HSEFinalFileListMenuId, HSEFinalFileList.ProjectId, null, HSEFinalFileList.FileId, HSEFinalFileList.CompileDate);
        }

        /// <summary>
        /// 修改已定稿文件
        /// </summary>
        /// <param name="HSEFinalFileList"></param>
        public static void UpdateHSEFinalFileList(Model.FinalFileManage_HSEFinalFileList HSEFinalFileList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_HSEFinalFileList newHSEFinalFileList = db.FinalFileManage_HSEFinalFileList.FirstOrDefault(e => e.FileId == HSEFinalFileList.FileId);
            if (newHSEFinalFileList != null)
            {
                newHSEFinalFileList.FileCode = HSEFinalFileList.FileCode;
                newHSEFinalFileList.FileName = HSEFinalFileList.FileName;
                newHSEFinalFileList.KeyWords = HSEFinalFileList.KeyWords;
                newHSEFinalFileList.FileContent = HSEFinalFileList.FileContent;
                newHSEFinalFileList.CompileMan = HSEFinalFileList.CompileMan;
                newHSEFinalFileList.CompileDate = HSEFinalFileList.CompileDate;
                newHSEFinalFileList.AttachUrl = HSEFinalFileList.AttachUrl;
                newHSEFinalFileList.States = HSEFinalFileList.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除已定稿文件
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteHSEFinalFileListById(string FileId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.FinalFileManage_HSEFinalFileList HSEFinalFileList = db.FinalFileManage_HSEFinalFileList.FirstOrDefault(e => e.FileId == FileId);
            if (HSEFinalFileList != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(HSEFinalFileList.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(HSEFinalFileList.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(HSEFinalFileList.FileId);
                db.FinalFileManage_HSEFinalFileList.DeleteOnSubmit(HSEFinalFileList);
                db.SubmitChanges();
            }
        }
    }
}
