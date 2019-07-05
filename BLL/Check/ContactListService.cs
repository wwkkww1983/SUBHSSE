using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 联系单
    /// </summary>
    public static class ContactListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取联系单信息
        /// </summary>
        /// <param name="ContactListId"></param>
        /// <returns></returns>
        public static Model.Check_ContactList GetContactListById(string ContactListId)
        {
            return Funs.DB.Check_ContactList.FirstOrDefault(e => e.ContactListId == ContactListId);
        }

        /// <summary>
        /// 添加联系单
        /// </summary>
        /// <param name="ContactList"></param>
        public static void AddContactList(Model.Check_ContactList ContactList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ContactList newContactList = new Model.Check_ContactList
            {
                ContactListId = ContactList.ContactListId,
                ProjectId = ContactList.ProjectId,
                Code = ContactList.Code,
                SponsorUnitId = ContactList.SponsorUnitId,
                ReceivingUnits = ContactList.ReceivingUnits,
                ReceivingUnitNames = ContactList.ReceivingUnitNames,
                CompileDate = ContactList.CompileDate,
                CompileMan = ContactList.CompileMan,
                Remark = ContactList.Remark,
                AttachUrl = ContactList.AttachUrl,
                SeeFile = ContactList.SeeFile
            };

            db.Check_ContactList.InsertOnSubmit(newContactList);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectContactListMenuId, newContactList.ProjectId, ContactList.SponsorUnitId, newContactList.ContactListId, newContactList.CompileDate);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ContactList"></param>
        public static void UpdateContactList(Model.Check_ContactList ContactList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ContactList newContactList = db.Check_ContactList.FirstOrDefault(e => e.ContactListId == ContactList.ContactListId);
            if (newContactList != null)
            {
                newContactList.Code = ContactList.Code;
                newContactList.SponsorUnitId = ContactList.SponsorUnitId;
                newContactList.ReceivingUnits = ContactList.ReceivingUnits;
                newContactList.ReceivingUnitNames = ContactList.ReceivingUnitNames;
                newContactList.CompileDate = ContactList.CompileDate;
                newContactList.CompileMan = ContactList.CompileMan;
                newContactList.Remark = ContactList.Remark;
                newContactList.AttachUrl = ContactList.AttachUrl;
                newContactList.SeeFile = ContactList.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ContactListId"></param>
        public static void DeleteContactListById(string ContactListId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ContactList ContactList = db.Check_ContactList.FirstOrDefault(e => e.ContactListId == ContactListId);
            if (ContactList != null)
            {
                if (!string.IsNullOrEmpty(ContactList.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, ContactList.AttachUrl);//删除附件
                }

                CodeRecordsService.DeleteCodeRecordsByDataId(ContactListId);
                CommonService.DeleteAttachFileById(ContactListId);  
                db.Check_ContactList.DeleteOnSubmit(ContactList);
                db.SubmitChanges();
            }
        }
    }
}