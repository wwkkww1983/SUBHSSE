using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class PersonalFolderService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据用户id获取个人文夹主表列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.Personal_PersonalFolder> GetPersonalFolderListByUserId(string userId)
        {
            var personalFolderList = from x in Funs.DB.Personal_PersonalFolder where x.CompileMan ==userId select x;
            return personalFolderList.ToList();
        }

        /// <summary>
        /// 根据主键id获取个人文夹
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.Personal_PersonalFolder GetPersonalFolderByID(string personalFolderId)
        {
            return Funs.DB.Personal_PersonalFolder.FirstOrDefault(x => x.PersonalFolderId == personalFolderId);
        }
        
        /// <summary>
        /// 添加个人文件夹
        /// </summary>
        /// <param name="personalFolder"></param>
        public static void AddPersonalFolder(Model.Personal_PersonalFolder personalFolder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolder newPersonalFolder = new Model.Personal_PersonalFolder
            {
                PersonalFolderId = personalFolder.PersonalFolderId,
                Code = personalFolder.Code,
                Title = personalFolder.Title,
                SupPersonalFolderId = personalFolder.SupPersonalFolderId,
                CompileMan = personalFolder.CompileMan,
                IsEndLever = personalFolder.IsEndLever
            };
            db.Personal_PersonalFolder.InsertOnSubmit(newPersonalFolder);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改个人文件夹
        /// </summary>
        /// <param name="personalFolder"></param>
        public static void UpdatePersonalFolder(Model.Personal_PersonalFolder personalFolder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolder newPersonalFolder = db.Personal_PersonalFolder.FirstOrDefault(e => e.PersonalFolderId == personalFolder.PersonalFolderId);
            if (newPersonalFolder != null)
            {
                newPersonalFolder.Code = personalFolder.Code;
                newPersonalFolder.Title = personalFolder.Title;
                newPersonalFolder.CompileMan = personalFolder.CompileMan;
                newPersonalFolder.IsEndLever = personalFolder.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="personalFolderId"></param>
        public static void DeletePersonalFolderByID(string personalFolderId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Personal_PersonalFolder personalFolder = db.Personal_PersonalFolder.FirstOrDefault(e => e.PersonalFolderId == personalFolderId);
            {
                db.Personal_PersonalFolder.DeleteOnSubmit(personalFolder);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string personalFolderId, string supPersonalFolderId, string title)
        {
            var q = Funs.DB.Personal_PersonalFolder.FirstOrDefault(x => x.SupPersonalFolderId == supPersonalFolderId && x.Title == title
                    && x.PersonalFolderId != personalFolderId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否可删除节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeletePersonalFolder(string personalFolderId)
        {
            bool isDelete = true;
            var personalFolder = GetPersonalFolderByID(personalFolderId);
            if (personalFolder != null)
            {
                if (personalFolder.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Personal_PersonalFolderItem.FirstOrDefault(x => x.PersonalFolderId == personalFolderId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = Funs.DB.Personal_PersonalFolder.FirstOrDefault(x => x.SupPersonalFolderId == personalFolderId);
                    if (supItemSetCount != null)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }
    }
}
