using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 工程现场环境与职业健康月报
    /// </summary>
    public static class HealthService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取工程现场环境与职业健康月报
        /// </summary>
        /// <param name="healthId"></param>
        /// <returns></returns>
        public static Model.Manager_Health GetHealthById(string healthId)
        {
            return Funs.DB.Manager_Health.FirstOrDefault(e => e.HealthId == healthId);
        }

        /// <summary>
        /// 添加工程现场环境与职业健康月报
        /// </summary>
        /// <param name="health"></param>
        public static void AddHealth(Model.Manager_Health health)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Health newHealth = new Model.Manager_Health
            {
                HealthId = health.HealthId,
                ProjectId = health.ProjectId,
                HealthCode = health.HealthCode,
                HealthName = health.HealthName,
                FileContent = health.FileContent,
                CompileMan = health.CompileMan,
                CompileDate = health.CompileDate,
                States = health.States
            };
            db.Manager_Health.InsertOnSubmit(newHealth);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.HealthMenuId, health.ProjectId, null, health.HealthId, health.CompileDate);
        }

        /// <summary>
        /// 修改工程现场环境与职业健康月报
        /// </summary>
        /// <param name="health"></param>
        public static void UpdateHealth(Model.Manager_Health health)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Health newHealth = db.Manager_Health.FirstOrDefault(e => e.HealthId == health.HealthId);
            if (newHealth != null)
            {
                //newHealth.ProjectId = health.ProjectId;
                newHealth.HealthCode = health.HealthCode;
                newHealth.HealthName = health.HealthName;
                newHealth.FileContent = health.FileContent;
                newHealth.CompileMan = health.CompileMan;
                newHealth.CompileDate = health.CompileDate;
                newHealth.States = health.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除工程现场环境与职业健康月报
        /// </summary>
        /// <param name="healthId"></param>
        public static void DeleteHealthById(string healthId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Health health = db.Manager_Health.FirstOrDefault(e => e.HealthId == healthId);
            if (health != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(healthId);
                BLL.CommonService.DeleteAttachFileById(healthId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(healthId);//删除审核流程
                db.Manager_Health.DeleteOnSubmit(health);
                db.SubmitChanges();
            }
        }
    }
}
