using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理季报
    /// </summary>
    public static class ManagerQuarterlyService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理季报
        /// </summary>
        /// <param name="managerQuarterlyId"></param>
        /// <returns></returns>
        public static Model.Manager_ManagerQuarterly GetManagerQuarterlyById(string managerQuarterlyId)
        {
            return Funs.DB.Manager_ManagerQuarterly.FirstOrDefault(e => e.ManagerQuarterlyId == managerQuarterlyId);
        }

        /// <summary>
        /// 添加管理季报
        /// </summary>
        /// <param name="managerQuarterly"></param>
        public static void AddManagerQuarterly(Model.Manager_ManagerQuarterly managerQuarterly)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerQuarterly newManagerQuarterly = new Model.Manager_ManagerQuarterly
            {
                ManagerQuarterlyId = managerQuarterly.ManagerQuarterlyId,
                ProjectId = managerQuarterly.ProjectId,
                ManagerQuarterlyCode = managerQuarterly.ManagerQuarterlyCode,
                ManagerQuarterlyName = managerQuarterly.ManagerQuarterlyName,
                FileContent = managerQuarterly.FileContent,
                CompileMan = managerQuarterly.CompileMan,
                CompileDate = managerQuarterly.CompileDate,
                States = managerQuarterly.States
            };
            db.Manager_ManagerQuarterly.InsertOnSubmit(newManagerQuarterly);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerQuarterlyMenuId, managerQuarterly.ProjectId, null, managerQuarterly.ManagerQuarterlyId, managerQuarterly.CompileDate);
        }

        /// <summary>
        /// 修改管理季报
        /// </summary>
        /// <param name="managerQuarterly"></param>
        public static void UpdateManagerQuarterly(Model.Manager_ManagerQuarterly managerQuarterly)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerQuarterly newManagerQuarterly = db.Manager_ManagerQuarterly.FirstOrDefault(e => e.ManagerQuarterlyId == managerQuarterly.ManagerQuarterlyId);
            if (newManagerQuarterly != null)
            {
                //newManagerQuarterly.ProjectId = managerQuarterly.ProjectId;
                newManagerQuarterly.ManagerQuarterlyCode = managerQuarterly.ManagerQuarterlyCode;
                newManagerQuarterly.ManagerQuarterlyName = managerQuarterly.ManagerQuarterlyName;
                newManagerQuarterly.FileContent = managerQuarterly.FileContent;
                newManagerQuarterly.CompileMan = managerQuarterly.CompileMan;
                newManagerQuarterly.CompileDate = managerQuarterly.CompileDate;
                newManagerQuarterly.States = managerQuarterly.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除管理季报
        /// </summary>
        /// <param name="managerQuarterlyId"></param>
        public static void DeleteManagerQuarterlyById(string managerQuarterlyId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerQuarterly managerQuarterly = db.Manager_ManagerQuarterly.FirstOrDefault(e => e.ManagerQuarterlyId == managerQuarterlyId);
            if (managerQuarterly != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(managerQuarterlyId);
                BLL.CommonService.DeleteAttachFileById(managerQuarterlyId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(managerQuarterlyId);//删除审核流程
                db.Manager_ManagerQuarterly.DeleteOnSubmit(managerQuarterly);
                db.SubmitChanges();
            }
        }
    }
}
