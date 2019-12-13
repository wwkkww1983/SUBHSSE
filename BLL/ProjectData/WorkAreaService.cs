using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 作业区域
    /// </summary>
    public static class WorkAreaService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取作业区域
        /// </summary>
        /// <param name="workAreaId"></param>
        /// <returns></returns>
        public static Model.ProjectData_WorkArea GetWorkAreaByWorkAreaId(string workAreaId)
        {
            return Funs.DB.ProjectData_WorkArea.FirstOrDefault(e => e.WorkAreaId == workAreaId);
        }

        /// <summary>
        /// 添加作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void AddWorkArea(Model.ProjectData_WorkArea workArea)
        {
            Model.ProjectData_WorkArea newWorkArea = new Model.ProjectData_WorkArea
            {
                WorkAreaId = workArea.WorkAreaId,
                ProjectId = workArea.ProjectId,
                UnitId = workArea.UnitId,
                WorkAreaCode = workArea.WorkAreaCode,
                WorkAreaName = workArea.WorkAreaName,
                Remark = workArea.Remark
            };
            db.ProjectData_WorkArea.InsertOnSubmit(newWorkArea);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void UpdateWorkArea(Model.ProjectData_WorkArea workArea)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectData_WorkArea newWorkArea = db.ProjectData_WorkArea.FirstOrDefault(e => e.WorkAreaId == workArea.WorkAreaId);
            if (newWorkArea != null)
            {
                newWorkArea.ProjectId = workArea.ProjectId;
                newWorkArea.UnitId = workArea.UnitId;
                newWorkArea.WorkAreaCode = workArea.WorkAreaCode;
                newWorkArea.WorkAreaName = workArea.WorkAreaName;
                newWorkArea.Remark = workArea.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除作业区域
        /// </summary>
        /// <param name="workAreaId"></param>
        public static void DeleteWorkAreaById(string workAreaId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectData_WorkArea workArea = db.ProjectData_WorkArea.FirstOrDefault(e => e.WorkAreaId == workAreaId);
            if (workArea != null)
            {
                db.ProjectData_WorkArea.DeleteOnSubmit(workArea);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目Id获取作业区域下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.ProjectData_WorkArea> GetWorkAreaByProjectList(string projectId)
        {
            return (from x in Funs.DB.ProjectData_WorkArea where x.ProjectId == projectId orderby x.WorkAreaCode select x).ToList();
        }

        /// <summary>
        /// 根据项目Id获取作业区域下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.ProjectData_WorkArea> GetWorkAreaByProjectIdUnitId(string projectId)
        {
            return (from x in Funs.DB.ProjectData_WorkArea where x.ProjectId == projectId orderby x.WorkAreaCode select x).ToList();
        }

        #region 区域表下拉框
        /// <summary>
        ///  区域表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkAreaDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "WorkAreaId";
            dropName.DataTextField = "WorkAreaName";
            dropName.DataSource = BLL.WorkAreaService.GetWorkAreaByProjectList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  区域表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkAreaProjetcUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "WorkAreaId";
            dropName.DataTextField = "WorkAreaName";
            dropName.DataSource = BLL.WorkAreaService.GetWorkAreaByProjectIdUnitId(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 根据多单位ID得到单位名称字符串
        /// <summary>
        /// 根据多单位ID得到单位名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getWorkAreaNamesIds(object ids)
        {
            string names = string.Empty;
            if (ids != null)
            {
                string[] idls = ids.ToString().Split(',');
                foreach (string id in idls)
                {
                    var q = GetWorkAreaByWorkAreaId(id);
                    if (q != null)
                    {
                        names += q.WorkAreaName + ",";
                    }
                }
                if (names != string.Empty)
                {
                    names = names.Substring(0, names.Length - 1); ;
                }
            }

            return names;
        }
        #endregion
    }
}
