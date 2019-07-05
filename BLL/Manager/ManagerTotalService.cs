using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 工作总结
    /// </summary>
    public static class ManagerTotalService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取工作总结
        /// </summary>
        /// <param name="managerTotalId"></param>
        /// <returns></returns>
        public static Model.Manager_ManagerTotal GetManagerTotalById(string managerTotalId)
        {
            return Funs.DB.Manager_ManagerTotal.FirstOrDefault(e => e.ManagerTotalId == managerTotalId);
        }

        /// <summary>
        /// 添加工作总结
        /// </summary>
        /// <param name="managerTotal"></param>
        public static void AddManagerTotal(Model.Manager_ManagerTotal managerTotal)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotal newManagerTotal = new Model.Manager_ManagerTotal
            {
                ManagerTotalId = managerTotal.ManagerTotalId,
                ProjectId = managerTotal.ProjectId,
                ManagerTotalCode = managerTotal.ManagerTotalCode,
                ManagerTotalName = managerTotal.ManagerTotalName,
                FileContent = managerTotal.FileContent,
                CompileMan = managerTotal.CompileMan,
                CompileDate = managerTotal.CompileDate,
                States = managerTotal.States
            };
            db.Manager_ManagerTotal.InsertOnSubmit(newManagerTotal);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerTotalMenuId, managerTotal.ProjectId, null, managerTotal.ManagerTotalId, managerTotal.CompileDate);
        }

        /// <summary>
        /// 修改工作总结
        /// </summary>
        /// <param name="managerTotal"></param>
        public static void UpdateManagerTotal(Model.Manager_ManagerTotal managerTotal)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotal newManagerTotal = db.Manager_ManagerTotal.FirstOrDefault(e => e.ManagerTotalId == managerTotal.ManagerTotalId);
            if (newManagerTotal != null)
            {
                //newManagerTotal.ProjectId = managerTotal.ProjectId;
                newManagerTotal.ManagerTotalCode = managerTotal.ManagerTotalCode;
                newManagerTotal.ManagerTotalName = managerTotal.ManagerTotalName;
                newManagerTotal.FileContent = managerTotal.FileContent;
                newManagerTotal.CompileMan = managerTotal.CompileMan;
                newManagerTotal.CompileDate = managerTotal.CompileDate;
                newManagerTotal.States = managerTotal.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除工作总结
        /// </summary>
        /// <param name="managerTotalId"></param>
        public static void DeleteManagerTotalById(string managerTotalId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotal managerTotal = db.Manager_ManagerTotal.FirstOrDefault(e => e.ManagerTotalId == managerTotalId);
            if (managerTotal != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(managerTotalId);
                BLL.CommonService.DeleteAttachFileById(managerTotalId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(managerTotalId);//删除审核流程
                db.Manager_ManagerTotal.DeleteOnSubmit(managerTotal);
                db.SubmitChanges();
            }
        }
    }
}
