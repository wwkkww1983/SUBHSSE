using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理周报
    /// </summary>
    public static class ManagerWeekService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理周报
        /// </summary>
        /// <param name="managerWeekId"></param>
        /// <returns></returns>
        public static Model.Manager_ManagerWeek GetManagerWeekById(string managerWeekId)
        {
            return Funs.DB.Manager_ManagerWeek.FirstOrDefault(e => e.ManagerWeekId == managerWeekId);
        }

        /// <summary>
        /// 添加管理周报
        /// </summary>
        /// <param name="managerWeek"></param>
        public static void AddManagerWeek(Model.Manager_ManagerWeek managerWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerWeek newManagerWeek = new Model.Manager_ManagerWeek
            {
                ManagerWeekId = managerWeek.ManagerWeekId,
                ProjectId = managerWeek.ProjectId,
                ManagerWeekCode = managerWeek.ManagerWeekCode,
                ManagerWeekName = managerWeek.ManagerWeekName,
                FileContent = managerWeek.FileContent,
                CompileMan = managerWeek.CompileMan,
                CompileDate = managerWeek.CompileDate,
                States = managerWeek.States
            };
            db.Manager_ManagerWeek.InsertOnSubmit(newManagerWeek);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerWeekMenuId, managerWeek.ProjectId, null, managerWeek.ManagerWeekId, managerWeek.CompileDate);
        }

        /// <summary>
        /// 修改管理周报
        /// </summary>
        /// <param name="managerWeek"></param>
        public static void UpdateManagerWeek(Model.Manager_ManagerWeek managerWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerWeek newManagerWeek = db.Manager_ManagerWeek.FirstOrDefault(e => e.ManagerWeekId == managerWeek.ManagerWeekId);
            if (newManagerWeek != null)
            {
                //newManagerWeek.ProjectId = managerWeek.ProjectId;
                newManagerWeek.ManagerWeekCode = managerWeek.ManagerWeekCode;
                newManagerWeek.ManagerWeekName = managerWeek.ManagerWeekName;
                newManagerWeek.FileContent = managerWeek.FileContent;
                newManagerWeek.CompileMan = managerWeek.CompileMan;
                newManagerWeek.CompileDate = managerWeek.CompileDate;
                newManagerWeek.States = managerWeek.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除管理周报
        /// </summary>
        /// <param name="managerWeekId"></param>
        public static void DeleteManagerWeekById(string managerWeekId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerWeek managerWeek = db.Manager_ManagerWeek.FirstOrDefault(e => e.ManagerWeekId == managerWeekId);
            if (managerWeek != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(managerWeekId);
                BLL.CommonService.DeleteAttachFileById(managerWeekId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(managerWeekId);//删除审核流程
                db.Manager_ManagerWeek.DeleteOnSubmit(managerWeek);
                db.SubmitChanges();
            }
        }
    }
}