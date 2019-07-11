using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ProjectFolderService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据用户id获取项目文夹主表列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.InformationProject_ProjectFolder> GetProjectFolderListByProjectId(string projectId)
        {
            var ProjectFolderList = from x in Funs.DB.InformationProject_ProjectFolder where x.ProjectId == projectId select x;
            return ProjectFolderList.ToList();
        }

        /// <summary>
        /// 根据主键id获取项目文夹
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.InformationProject_ProjectFolder GetProjectFolderByID(string projectFolderId)
        {
            return Funs.DB.InformationProject_ProjectFolder.FirstOrDefault(x => x.ProjectFolderId == projectFolderId);
        }
        
        /// <summary>
        /// 添加项目文件夹
        /// </summary>
        /// <param name="projectFolder"></param>
        public static void AddProjectFolder(Model.InformationProject_ProjectFolder projectFolder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolder newProjectFolder = new Model.InformationProject_ProjectFolder
            {
                ProjectFolderId = projectFolder.ProjectFolderId,
                ProjectId = projectFolder.ProjectId,
                Code = projectFolder.Code,
                Title = projectFolder.Title,
                SupProjectFolderId = projectFolder.SupProjectFolderId,
                IsEndLever = projectFolder.IsEndLever
            };
            db.InformationProject_ProjectFolder.InsertOnSubmit(newProjectFolder);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目文件夹
        /// </summary>
        /// <param name="projectFolder"></param>
        public static void UpdateProjectFolder(Model.InformationProject_ProjectFolder projectFolder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolder newProjectFolder = db.InformationProject_ProjectFolder.FirstOrDefault(e => e.ProjectFolderId == projectFolder.ProjectFolderId);
            if (newProjectFolder != null)
            {
                newProjectFolder.Code = projectFolder.Code;
                newProjectFolder.Title = projectFolder.Title;
                newProjectFolder.IsEndLever = projectFolder.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="projectFolderId"></param>
        public static void DeleteProjectFolderByID(string projectFolderId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_ProjectFolder ProjectFolder = db.InformationProject_ProjectFolder.FirstOrDefault(e => e.ProjectFolderId == projectFolderId);
            if(ProjectFolder != null)
            {
                db.InformationProject_ProjectFolder.DeleteOnSubmit(ProjectFolder);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string projectFolderId, string supProjectFolderId, string title)
        {
            var q = Funs.DB.InformationProject_ProjectFolder.FirstOrDefault(x => x.SupProjectFolderId == supProjectFolderId && x.Title == title
                    && x.ProjectFolderId != projectFolderId);
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
        public static bool IsDeleteProjectFolder(string projectFolderId)
        {
            bool isDelete = true;
            var ProjectFolder = GetProjectFolderByID(projectFolderId);
            if (ProjectFolder != null)
            {
                if (ProjectFolder.IsEndLever == true)
                {
                    var detailCout = Funs.DB.InformationProject_ProjectFolderItem.FirstOrDefault(x => x.ProjectFolderId == projectFolderId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = Funs.DB.InformationProject_ProjectFolder.FirstOrDefault(x => x.SupProjectFolderId == projectFolderId);
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
