namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using System;

    public static class ProjectService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        ///获取项目信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Project GetProjectByProjectId(string projectId)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
        }

        /// <summary>
        ///获取项目信息
        /// </summary>
        /// <returns></returns>
        public static string GetProjectNameByProjectId(string projectId)
        {
            string name = string.Empty;
            var project = Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                name = project.ProjectName;
            }
            return name;
        }

        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <returns></returns>
        public static void AddProject(Model.Base_Project project)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Project newProject = new Base_Project
            {
                ProjectId = project.ProjectId,
                ProjectCode = project.ProjectCode,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProjectAddress = project.ProjectAddress,
                ContractNo = project.ContractNo,
                WorkRange = project.WorkRange,
                Duration = project.Duration,
                ShortName = project.ShortName,
                ProjectType = project.ProjectType,
                PostCode = project.PostCode,
                Remark = project.Remark,
                ProjectState = project.ProjectState,
                IsUpTotalMonth = project.IsUpTotalMonth,
                UnitId = project.UnitId,
                ProjectMainPerson = project.ProjectMainPerson,
                ProjectLiaisonPerson = project.ProjectLiaisonPerson,
                IsForeign=project.IsForeign,
                FromProjectId = project.FromProjectId,
            };
            db.Base_Project.InsertOnSubmit(newProject);
            db.SubmitChanges();
            ////插入编码/模板规则表
            BLL.ProjectData_CodeTemplateRuleService.InertProjectData_CodeTemplateRuleByProjectId(project.ProjectId);
            if (newProject.ProjectType != "5")
            {
                ////根据项目信息生成企业安全管理资料计划总表
                BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(newProject.ProjectId, string.Empty, null, null);
            }
        }

        /// <summary>
        ///修改项目信息 
        /// </summary>
        /// <param name="project"></param>
        public static void UpdateProject(Model.Base_Project project)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Project newProject = db.Base_Project.FirstOrDefault(e => e.ProjectId == project.ProjectId);
            if (newProject != null)
            {
                newProject.ProjectCode = project.ProjectCode;
                newProject.ProjectName = project.ProjectName;
                newProject.StartDate = project.StartDate;
                newProject.EndDate = project.EndDate;
                newProject.ProjectAddress = project.ProjectAddress;
                newProject.ShortName = project.ShortName;
                newProject.ContractNo = project.ContractNo;
                newProject.WorkRange = project.WorkRange;
                newProject.Duration = project.Duration;
                newProject.ProjectType = project.ProjectType;
                newProject.PostCode = project.PostCode;
                newProject.Remark = project.Remark;
                newProject.ProjectState = project.ProjectState;
                newProject.IsUpTotalMonth = project.IsUpTotalMonth;
                newProject.UnitId = project.UnitId;
                newProject.ProjectMainPerson = project.ProjectMainPerson;
                newProject.ProjectLiaisonPerson = project.ProjectLiaisonPerson;
                newProject.IsForeign = project.IsForeign;
                newProject.FromProjectId = project.FromProjectId;
                db.SubmitChanges();
                if (newProject.ProjectType != "5")
                {
                    ////根据项目信息生成企业安全管理资料计划总表
                    BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(newProject.ProjectId, string.Empty, null, null);
                }
            }
        }

        /// <summary>
        /// 根据项目Id删除一个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteProject(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Project project = db.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
            if (project != null)
            {
                ////删除编码/模板规则表
                BLL.ProjectData_CodeTemplateRuleService.DeleteProjectData_CodeTemplateRule(project.ProjectId);
                ////删除项目安全管理资料计划总表
                BLL.SafetyDataPlanService.DeleteSafetyDataPlanByProjectId(project.ProjectId);
                ////删除E项目安全管理资料计划总表
                BLL.SafetyDataEPlanService.DeleteSafetyDataEPlanByProjectId(project.ProjectId);
                ////删除E项目安全管理
                BLL.SafetyDataEItemService.DeleteSafetyDataEItemByProjectId(project.ProjectId);
                BLL.SafetySystemService.DeleteSafetySystemByProjectid(project.ProjectId);
                db.Base_Project.DeleteOnSubmit(project);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取施工中项目集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectWorkList()
        {
            var list = (from x in Funs.DB.Base_Project
                        where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取施工中月总结项目集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetTotalMonthProjectWorkList(DateTime? monthTime)
        {
            List<Model.Base_Project> projectList = new List<Base_Project>();
            var list = (from x in Funs.DB.Base_Project
                        where x.IsUpTotalMonth == true && (x.StartDate.HasValue && x.StartDate <= monthTime.Value.AddMonths(1))
                        orderby x.ProjectCode descending
                        select x).ToList();
            foreach (var item in list)
            {
                var projectSate = Funs.DB.Base_ProjectSate.FirstOrDefault(x => x.ProjectId == item.ProjectId);
                if (projectSate == null)
                {
                    projectList.Add(item);
                }
                else
                {
                    var projectSate1 = (from x in Funs.DB.Base_ProjectSate
                                        where x.ProjectId == item.ProjectId && x.ShutdownDate.Value <= monthTime.Value.AddMonths(1)
                                        orderby x.ShutdownDate descending
                                        select x).FirstOrDefault();
                    if (projectSate1 == null)
                    {
                        projectList.Add(item);
                    }
                    else if (projectSate1.ProjectState == BLL.Const.ProjectState_1 || projectSate1.ProjectState == null)
                    {
                        projectList.Add(item);
                    }
                }

            }
            return projectList;
        }

        /// <summary>
        /// 获取项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectDropDownListByState(string state)
        {
            if (state == BLL.Const.ProjectState_1)  //施工
            {
                var list = (from x in Funs.DB.Base_Project
                            where x.ProjectState == state || x.ProjectState == null
                            orderby x.ProjectCode descending
                            select x).ToList();
                return list;
            }
            else
            {
                var list = (from x in Funs.DB.Base_Project
                            where x.ProjectState == state
                            orderby x.ProjectCode descending
                            select x).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetAllProjectDropDownList()
        {
            var list = (from x in Funs.DB.Base_Project
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取非设计项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetNoEProjectDropDownList()
        {
            var list = (from x in Funs.DB.Base_Project
                        where x.ProjectType != "5"
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取某类型下项目下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Project> GetProjectByProjectTypeDropDownList(string projectType)
        {
            var list = (from x in Funs.DB.Base_Project
                        where x.ProjectType == projectType
                        orderby x.ProjectCode descending
                        select x).ToList();
            return list;
        }

        #region 项目表下拉框
        /// <summary>
        ///  项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            dropName.DataSource = BLL.ProjectService.GetProjectWorkList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }


        /// <summary>
        ///  项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAllProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            var projectlist = BLL.ProjectService.GetAllProjectDropDownList();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  非设计项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitNoEProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            var projectlist = BLL.ProjectService.GetNoEProjectDropDownList();
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  某类型下项目表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectByProjectTypeDropDownList(FineUIPro.DropDownList dropName, string projectType, bool isShowPlease)
        {
            dropName.DataValueField = "ProjectId";
            dropName.DataTextField = "ProjectName";
            var projectlist = BLL.ProjectService.GetProjectByProjectTypeDropDownList(projectType);
            dropName.DataSource = projectlist;
            dropName.DataBind();
            if (projectlist.Count() == 0)
            {
                isShowPlease = true;
            }
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 获取项目经理、施工经理、安全经理
        /// <summary>
        /// 项目经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetProjectManagerName(string projectId)
        {
            string name = string.Empty;
            if (projectId != null)
            {
                name = (from x in Funs.DB.Base_Project
                        join y in Funs.DB.Project_ProjectUser on x.ProjectId equals y.ProjectId
                        join z in Funs.DB.Sys_User on y.UserId equals z.UserId
                        where x.ProjectId == projectId && y.RoleId == BLL.Const.ProjectManager
                        select z.UserName).FirstOrDefault();
            }
            return name;
        }

        /// <summary>
        /// 施工经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetConstructionManagerName(string projectId)
        {
            string name = string.Empty;
            if (projectId != null)
            {
                name = (from x in Funs.DB.Base_Project
                        join y in Funs.DB.Project_ProjectUser on x.ProjectId equals y.ProjectId
                        join z in Funs.DB.Sys_User on y.UserId equals z.UserId
                        where x.ProjectId == projectId && y.RoleId == BLL.Const.ConstructionManager
                        select z.UserName).FirstOrDefault();
            }
            return name;
        }

        /// <summary>
        /// 安全经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static string GetHSSEManagerName(string projectId)
        {
            string name = string.Empty;
            if (projectId != null)
            {
                name = (from x in Funs.DB.Base_Project
                        join y in Funs.DB.Project_ProjectUser on x.ProjectId equals y.ProjectId
                        join z in Funs.DB.Sys_User on y.UserId equals z.UserId
                        where x.ProjectId == projectId && y.RoleId == BLL.Const.HSSEManager
                        select z.UserName).FirstOrDefault();
            }
            return name;
        }
        #endregion
    }
}
