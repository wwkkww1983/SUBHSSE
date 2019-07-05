using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ProjectFolderItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
       
        /// <summary>
        /// 根据主键id获取项目明细
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.InformationProject_ProjectFolderItem GetProjectFolderItemByID(string projectFolderItemId)
        {
            return Funs.DB.InformationProject_ProjectFolderItem.FirstOrDefault(x => x.ProjectFolderItemId == projectFolderItemId);
        }

        /// <summary>
        /// 添加项目文件
        /// </summary>
        /// <param name="projectFolderItem"></param>
        public static void AddProjectFolderItem(Model.InformationProject_ProjectFolderItem projectFolderItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolderItem newProjectFolderItem = new Model.InformationProject_ProjectFolderItem
            {
                ProjectFolderItemId = projectFolderItem.ProjectFolderItemId,
                ProjectFolderId = projectFolderItem.ProjectFolderId,
                Code = projectFolderItem.Code,
                Title = projectFolderItem.Title,
                FileContent = projectFolderItem.FileContent,
                CompileMan = projectFolderItem.CompileMan,
                CompileDate = projectFolderItem.CompileDate,
                AttachUrl = projectFolderItem.AttachUrl
            };
            db.InformationProject_ProjectFolderItem.InsertOnSubmit(newProjectFolderItem);
            db.SubmitChanges();

            var projecFolder = BLL.ProjectFolderService.GetProjectFolderByID(projectFolderItem.ProjectFolderId);
            if (projecFolder != null)
            {
                ////增加一条编码记录
                BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectFolderMenuId, projecFolder.ProjectId, null, newProjectFolderItem.ProjectFolderItemId, newProjectFolderItem.CompileDate);
            }
        }

        /// <summary>
        /// 修改项目文件
        /// </summary>
        /// <param name="projectFolderItem"></param>
        public static void UpdateProjectFolderItem(Model.InformationProject_ProjectFolderItem projectFolderItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolderItem newProjectFolderItem = db.InformationProject_ProjectFolderItem.FirstOrDefault(e => e.ProjectFolderItemId == projectFolderItem.ProjectFolderItemId);
            if (newProjectFolderItem != null)
            {
                newProjectFolderItem.Code = projectFolderItem.Code;
                newProjectFolderItem.Title = projectFolderItem.Title;
                newProjectFolderItem.FileContent = projectFolderItem.FileContent;
                newProjectFolderItem.CompileMan = projectFolderItem.CompileMan;
                newProjectFolderItem.CompileDate = projectFolderItem.CompileDate;
                newProjectFolderItem.AttachUrl = projectFolderItem.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="projectFolderItemId"></param>
        public static void DeleteProjectFolderItemByID(string projectFolderItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolderItem projectFolderItem = db.InformationProject_ProjectFolderItem.FirstOrDefault(e => e.ProjectFolderItemId == projectFolderItemId);
            if (projectFolderItem != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(projectFolderItem.ProjectFolderItemId);
                ////删除编码
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(projectFolderItem.ProjectFolderItemId);
                db.InformationProject_ProjectFolderItem.DeleteOnSubmit(projectFolderItem);
                db.SubmitChanges();
            }
        }        
    }
}
