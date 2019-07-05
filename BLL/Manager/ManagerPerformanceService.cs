using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 现场HSSE工作顾客评价
    /// </summary>
    public static class ManagerPerformanceService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取现场HSSE工作顾客评价
        /// </summary>
        /// <param name="managerPerformanceId"></param>
        /// <returns></returns>
        public static Model.Manager_ManagerPerformance GetManagerPerformanceById(string managerPerformanceId)
        {
            return Funs.DB.Manager_ManagerPerformance.FirstOrDefault(e => e.ManagerPerformanceId == managerPerformanceId);
        }

        /// <summary>
        /// 添加现场HSSE工作顾客评价
        /// </summary>
        /// <param name="managerPerformance"></param>
        public static void AddManagerPerformance(Model.Manager_ManagerPerformance managerPerformance)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerPerformance newManagerPerformance = new Model.Manager_ManagerPerformance
            {
                ManagerPerformanceId = managerPerformance.ManagerPerformanceId,
                ProjectId = managerPerformance.ProjectId,
                ManagerPerformanceCode = managerPerformance.ManagerPerformanceCode,
                ManagerPerformanceName = managerPerformance.ManagerPerformanceName,
                FileContent = managerPerformance.FileContent,
                CompileMan = managerPerformance.CompileMan,
                CompileDate = managerPerformance.CompileDate,
                States = managerPerformance.States
            };
            db.Manager_ManagerPerformance.InsertOnSubmit(newManagerPerformance);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerPerformanceMenuId, managerPerformance.ProjectId, null, managerPerformance.ManagerPerformanceId, managerPerformance.CompileDate);
        }

        /// <summary>
        /// 修改现场HSSE工作顾客评价
        /// </summary>
        /// <param name="managerPerformance"></param>
        public static void UpdateManagerPerformance(Model.Manager_ManagerPerformance managerPerformance)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerPerformance newManagerPerformance = db.Manager_ManagerPerformance.FirstOrDefault(e => e.ManagerPerformanceId == managerPerformance.ManagerPerformanceId);
            if (newManagerPerformance != null)
            {
                //newManagerPerformance.ProjectId = managerPerformance.ProjectId;
                newManagerPerformance.ManagerPerformanceCode = managerPerformance.ManagerPerformanceCode;
                newManagerPerformance.ManagerPerformanceName = managerPerformance.ManagerPerformanceName;
                newManagerPerformance.FileContent = managerPerformance.FileContent;
                newManagerPerformance.CompileMan = managerPerformance.CompileMan;
                newManagerPerformance.CompileDate = managerPerformance.CompileDate;
                newManagerPerformance.States = managerPerformance.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除现场HSSE工作顾客评价
        /// </summary>
        /// <param name="managerPerformanceId"></param>
        public static void DeleteManagerPerformanceById(string managerPerformanceId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerPerformance managerPerformance = db.Manager_ManagerPerformance.FirstOrDefault(e => e.ManagerPerformanceId == managerPerformanceId);
            if (managerPerformance != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(managerPerformanceId);
                BLL.CommonService.DeleteAttachFileById(managerPerformanceId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(managerPerformanceId);//删除审核流程
                db.Manager_ManagerPerformance.DeleteOnSubmit(managerPerformance);
                db.SubmitChanges();
            }
        }
    }
}
