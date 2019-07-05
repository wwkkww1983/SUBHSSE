using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class FileCabinetAItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
       
        /// <summary>
        /// 根据主键id获取项目明细
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.InformationProject_FileCabinetAItem GetFileCabinetAItemByID(string FileCabinetAItemId)
        {
            return Funs.DB.InformationProject_FileCabinetAItem.FirstOrDefault(x => x.FileCabinetAItemId == FileCabinetAItemId);
        }

        /// <summary>
        /// 添加项目文件
        /// </summary>
        /// <param name="FileCabinetAItem"></param>
        public static void AddFileCabinetAItem(Model.InformationProject_FileCabinetAItem FileCabinetAItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetAItem newFileCabinetAItem = new Model.InformationProject_FileCabinetAItem
            {
                FileCabinetAItemId = FileCabinetAItem.FileCabinetAItemId,
                FileCabinetAId = FileCabinetAItem.FileCabinetAId,
                Code = FileCabinetAItem.Code,
                Title = FileCabinetAItem.Title,
                FileContent = FileCabinetAItem.FileContent,
                CompileMan = FileCabinetAItem.CompileMan,
                CompileDate = FileCabinetAItem.CompileDate,
                Remark = FileCabinetAItem.Remark,
                AttachUrl = FileCabinetAItem.AttachUrl,
                IsMenu = FileCabinetAItem.IsMenu,
                Url = FileCabinetAItem.Url
            };
            db.InformationProject_FileCabinetAItem.InsertOnSubmit(newFileCabinetAItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目文件
        /// </summary>
        /// <param name="FileCabinetAItem"></param>
        public static void UpdateFileCabinetAItem(Model.InformationProject_FileCabinetAItem FileCabinetAItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetAItem newFileCabinetAItem = db.InformationProject_FileCabinetAItem.FirstOrDefault(e => e.FileCabinetAId == FileCabinetAItem.FileCabinetAId);
            if (newFileCabinetAItem != null)
            {
                newFileCabinetAItem.Code = FileCabinetAItem.Code;
                newFileCabinetAItem.Title = FileCabinetAItem.Title;
                newFileCabinetAItem.FileContent = FileCabinetAItem.FileContent;
                newFileCabinetAItem.CompileMan = FileCabinetAItem.CompileMan;
                newFileCabinetAItem.CompileDate = FileCabinetAItem.CompileDate;
                newFileCabinetAItem.Remark = FileCabinetAItem.Remark;
                newFileCabinetAItem.AttachUrl = FileCabinetAItem.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="FileCabinetAItemId"></param>
        public static void DeleteFileCabinetAItemByID(string FileCabinetAItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetAItem fileCabinetAItem = db.InformationProject_FileCabinetAItem.FirstOrDefault(e => e.FileCabinetAItemId == FileCabinetAItemId);
            if (fileCabinetAItem != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(fileCabinetAItem.FileCabinetAItemId);
                db.InformationProject_FileCabinetAItem.DeleteOnSubmit(fileCabinetAItem);
                db.SubmitChanges();
            }
        }        
    }
}
