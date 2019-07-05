using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 现场施工方案模板
    /// </summary>
    public static class ProjectSolutionTempleteService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取现场施工方案模板
        /// </summary>
        /// <param name="projectSolutionTempleteId"></param>
        /// <returns></returns>
        public static Model.Technique_ProjectSolutionTemplete GetProjectSolutionTempleteById(string projectSolutionTempleteId)
        {
            return Funs.DB.Technique_ProjectSolutionTemplete.FirstOrDefault(e => e.TempleteId == projectSolutionTempleteId);
        }

        /// <summary>
        /// 添加现场施工方案模板
        /// </summary>
        /// <param name="projectSolutionTemplete"></param>
        public static void AddProjectSolutionTemplete(Model.Technique_ProjectSolutionTemplete projectSolutionTemplete)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_ProjectSolutionTemplete newProjectSolutionTemplete = new Model.Technique_ProjectSolutionTemplete
            {
                TempleteId = projectSolutionTemplete.TempleteId,
                TempleteCode = projectSolutionTemplete.TempleteCode,
                TempleteName = projectSolutionTemplete.TempleteName,
                TempleteType = projectSolutionTemplete.TempleteType,
                FileContents = projectSolutionTemplete.FileContents,
                CompileMan = projectSolutionTemplete.CompileMan,
                CompileDate = projectSolutionTemplete.CompileDate
            };
            db.Technique_ProjectSolutionTemplete.InsertOnSubmit(newProjectSolutionTemplete);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改现场施工方案模板
        /// </summary>
        /// <param name="projectSolutionTemplete"></param>
        public static void UpdateProjectSolutionTemplete(Model.Technique_ProjectSolutionTemplete projectSolutionTemplete)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_ProjectSolutionTemplete newProjectSolutionTemplete = db.Technique_ProjectSolutionTemplete.FirstOrDefault(e => e.TempleteId == projectSolutionTemplete.TempleteId);
            if (newProjectSolutionTemplete != null)
            {
                newProjectSolutionTemplete.TempleteCode = projectSolutionTemplete.TempleteCode;
                newProjectSolutionTemplete.TempleteName = projectSolutionTemplete.TempleteName;
                newProjectSolutionTemplete.TempleteType = projectSolutionTemplete.TempleteType;
                newProjectSolutionTemplete.FileContents = projectSolutionTemplete.FileContents;
                newProjectSolutionTemplete.CompileMan = projectSolutionTemplete.CompileMan;
                newProjectSolutionTemplete.CompileDate = projectSolutionTemplete.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除现场施工方案模板
        /// </summary>
        /// <param name="projectSolutionTempleteId"></param>
        public static void DeleteProjectSolutionTempleteById(string projectSolutionTempleteId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_ProjectSolutionTemplete projectSolutionTemplete = db.Technique_ProjectSolutionTemplete.FirstOrDefault(e => e.TempleteId == projectSolutionTempleteId);
            if (projectSolutionTemplete != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(projectSolutionTempleteId);
                db.Technique_ProjectSolutionTemplete.DeleteOnSubmit(projectSolutionTemplete);
                db.SubmitChanges();
            }
        }
    }
}