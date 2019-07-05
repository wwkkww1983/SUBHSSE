using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全制度
    /// </summary>
    public static class ConstructionDynamicService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全制度信息
        /// </summary>
        /// <param name="ConstructionDynamicId"></param>
        /// <returns></returns>
        public static Model.SiteConstruction_ConstructionDynamic GetConstructionDynamicById(string ConstructionDynamicId)
        {
            return Funs.DB.SiteConstruction_ConstructionDynamic.FirstOrDefault(e => e.ConstructionDynamicId == ConstructionDynamicId);
        }

        /// <summary>
        /// 根据主键获取安全制度信息
        /// </summary>
        /// <param name="ConstructionDynamicId"></param>
        /// <returns></returns>
        public static List<Model.SiteConstruction_ConstructionDynamic> GetConstructionDynamicListByProjectId(string projectId)
        {
            return (from x in Funs.DB.SiteConstruction_ConstructionDynamic where x.ProjectId == projectId orderby x.CompileDate descending select x).ToList();
        }

        /// <summary>
        /// 添加安全制度
        /// </summary>
        /// <param name="ConstructionDynamic"></param>
        public static void AddConstructionDynamic(Model.SiteConstruction_ConstructionDynamic ConstructionDynamic)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_ConstructionDynamic newConstructionDynamic = new Model.SiteConstruction_ConstructionDynamic
            {
                ConstructionDynamicId = ConstructionDynamic.ConstructionDynamicId,
                ProjectId = ConstructionDynamic.ProjectId,
                UnitId = ConstructionDynamic.UnitId,
                CompileMan = ConstructionDynamic.CompileMan,
                CompileDate = ConstructionDynamic.CompileDate,
                JobContent = ConstructionDynamic.JobContent,
                AttachUrl = ConstructionDynamic.AttachUrl,
                SeeFile = ConstructionDynamic.SeeFile
            };

            db.SiteConstruction_ConstructionDynamic.InsertOnSubmit(newConstructionDynamic);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ConstructionDynamic"></param>
        public static void UpdateConstructionDynamic(Model.SiteConstruction_ConstructionDynamic ConstructionDynamic)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_ConstructionDynamic newConstructionDynamic = db.SiteConstruction_ConstructionDynamic.FirstOrDefault(e => e.ConstructionDynamicId == ConstructionDynamic.ConstructionDynamicId);
            if (newConstructionDynamic != null)
            {
                newConstructionDynamic.CompileMan = ConstructionDynamic.CompileMan;
                newConstructionDynamic.CompileDate = ConstructionDynamic.CompileDate;
                newConstructionDynamic.JobContent = ConstructionDynamic.JobContent;
                newConstructionDynamic.AttachUrl = ConstructionDynamic.AttachUrl;
                newConstructionDynamic.SeeFile = ConstructionDynamic.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ConstructionDynamicId"></param>
        public static void DeleteConstructionDynamicById(string ConstructionDynamicId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SiteConstruction_ConstructionDynamic ConstructionDynamic = db.SiteConstruction_ConstructionDynamic.FirstOrDefault(e => e.ConstructionDynamicId == ConstructionDynamicId);
            if (ConstructionDynamic != null)
            {
                if (!string.IsNullOrEmpty(ConstructionDynamic.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, ConstructionDynamic.AttachUrl);//删除附件
                } 
                db.SiteConstruction_ConstructionDynamic.DeleteOnSubmit(ConstructionDynamic);
                db.SubmitChanges();
            }
        }
    }
}