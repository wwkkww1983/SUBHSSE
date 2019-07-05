using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class PersonalFolderItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
       
        /// <summary>
        /// 根据主键id获取个人明细
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.Personal_PersonalFolderItem GetPersonalFolderItemByID(string personalFolderItemId)
        {
            return Funs.DB.Personal_PersonalFolderItem.FirstOrDefault(x => x.PersonalFolderItemId == personalFolderItemId);
        }

        /// <summary>
        /// 添加个人文件
        /// </summary>
        /// <param name="personalFolderItem"></param>
        public static void AddPersonalFolderItem(Model.Personal_PersonalFolderItem personalFolderItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolderItem newPersonalFolderItem = new Model.Personal_PersonalFolderItem
            {
                PersonalFolderItemId = personalFolderItem.PersonalFolderItemId,
                PersonalFolderId = personalFolderItem.PersonalFolderId,
                Code = personalFolderItem.Code,
                Title = personalFolderItem.Title,
                FileContent = personalFolderItem.FileContent,
                CompileDate = personalFolderItem.CompileDate,
                AttachUrl = personalFolderItem.AttachUrl
            };
            db.Personal_PersonalFolderItem.InsertOnSubmit(newPersonalFolderItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改个人文件
        /// </summary>
        /// <param name="personalFolderItem"></param>
        public static void UpdatePersonalFolderItem(Model.Personal_PersonalFolderItem personalFolderItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolderItem newPersonalFolderItem = db.Personal_PersonalFolderItem.FirstOrDefault(e => e.PersonalFolderItemId == personalFolderItem.PersonalFolderItemId);
            if (newPersonalFolderItem != null)
            {
                newPersonalFolderItem.Code = personalFolderItem.Code;
                newPersonalFolderItem.Title = personalFolderItem.Title;
                newPersonalFolderItem.FileContent = personalFolderItem.FileContent;
                newPersonalFolderItem.CompileDate = personalFolderItem.CompileDate;
                newPersonalFolderItem.AttachUrl = personalFolderItem.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="personalFolderItemId"></param>
        public static void DeletePersonalFolderItemByID(string personalFolderItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolderItem personalFolderItem = db.Personal_PersonalFolderItem.FirstOrDefault(e => e.PersonalFolderItemId == personalFolderItemId);
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(personalFolderItem.PersonalFolderItemId);
                db.Personal_PersonalFolderItem.DeleteOnSubmit(personalFolderItem);
                db.SubmitChanges();
            }
        }        
    }
}
