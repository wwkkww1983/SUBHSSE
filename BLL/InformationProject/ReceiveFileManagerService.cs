using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 一般来文
    /// </summary>
    public static class ReceiveFileManagerService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取一般来文
        /// </summary>
        /// <param name="ReceiveFileManagerId"></param>
        /// <returns></returns>
        public static Model.InformationProject_ReceiveFileManager GetReceiveFileManagerById(string ReceiveFileManagerId)
        {
            return Funs.DB.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManagerId);
        }

        /// <summary>
        /// 添加一般来文
        /// </summary>
        /// <param name="ReceiveFileManager"></param>
        public static void AddReceiveFileManager(Model.InformationProject_ReceiveFileManager ReceiveFileManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager newReceiveFileManager = new Model.InformationProject_ReceiveFileManager
            {
                ReceiveFileManagerId = ReceiveFileManager.ReceiveFileManagerId,
                ProjectId = ReceiveFileManager.ProjectId,
                ReceiveFileCode = ReceiveFileManager.ReceiveFileCode,
                ReceiveFileName = ReceiveFileManager.ReceiveFileName,
                Version = ReceiveFileManager.Version,
                FileUnitId = ReceiveFileManager.FileUnitId,
                FileCode = ReceiveFileManager.FileCode,
                FilePageNum = ReceiveFileManager.FilePageNum,
                GetFileDate = ReceiveFileManager.GetFileDate,
                SendPersonId = ReceiveFileManager.SendPersonId,
                MainContent = ReceiveFileManager.MainContent,
                AttachUrl = ReceiveFileManager.AttachUrl,
                States = ReceiveFileManager.States,
                UnitIds = ReceiveFileManager.UnitIds,
                FileType=ReceiveFileManager.FileType,
            };
            db.InformationProject_ReceiveFileManager.InsertOnSubmit(newReceiveFileManager);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ReceiveFileManagerMenuId, ReceiveFileManager.ProjectId, null, ReceiveFileManager.ReceiveFileManagerId, ReceiveFileManager.GetFileDate);
        }

        /// <summary>
        /// 修改一般来文
        /// </summary>
        /// <param name="ReceiveFileManager"></param>
        public static void UpdateReceiveFileManager(Model.InformationProject_ReceiveFileManager ReceiveFileManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager newReceiveFileManager = db.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManager.ReceiveFileManagerId);
            if (newReceiveFileManager != null)
            {
                newReceiveFileManager.ReceiveFileCode = ReceiveFileManager.ReceiveFileCode;
                newReceiveFileManager.ReceiveFileName = ReceiveFileManager.ReceiveFileName;
                newReceiveFileManager.Version = ReceiveFileManager.Version;
                newReceiveFileManager.FileUnitId = ReceiveFileManager.FileUnitId;
                newReceiveFileManager.FileCode = ReceiveFileManager.FileCode;
                newReceiveFileManager.FilePageNum = ReceiveFileManager.FilePageNum;
                newReceiveFileManager.GetFileDate = ReceiveFileManager.GetFileDate;
                newReceiveFileManager.SendPersonId = ReceiveFileManager.SendPersonId;
                newReceiveFileManager.MainContent = ReceiveFileManager.MainContent;
                newReceiveFileManager.AttachUrl = ReceiveFileManager.AttachUrl;
                newReceiveFileManager.States = ReceiveFileManager.States;
                newReceiveFileManager.UnitIds = ReceiveFileManager.UnitIds;
                newReceiveFileManager.FileType = ReceiveFileManager.FileType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除一般来文
        /// </summary>
        /// <param name="ReceiveFileManagerId"></param>
        public static void DeleteReceiveFileManagerById(string ReceiveFileManagerId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ReceiveFileManager ReceiveFileManager = db.InformationProject_ReceiveFileManager.FirstOrDefault(e => e.ReceiveFileManagerId == ReceiveFileManagerId);
            if (ReceiveFileManager != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(ReceiveFileManager.ReceiveFileManagerId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(ReceiveFileManager.ReceiveFileManagerId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(ReceiveFileManager.ReceiveFileManagerId);
                db.InformationProject_ReceiveFileManager.DeleteOnSubmit(ReceiveFileManager);
                db.SubmitChanges();
            }
        }
    }
}
