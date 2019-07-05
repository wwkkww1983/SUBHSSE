using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 职业健康
    /// </summary>
    public static class HealthManageService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取职业健康
        /// </summary>
        /// <param name="healthManageId"></param>
        /// <returns></returns>
        public static Model.Administrative_HealthManage GetHealthManageById(string healthManageId)
        {
            return Funs.DB.Administrative_HealthManage.FirstOrDefault(e => e.HealthManageId == healthManageId);
        }

        /// <summary>
        /// 添加职业健康
        /// </summary>
        /// <param name="healthManage"></param>
        public static void AddHealthManage(Model.Administrative_HealthManage healthManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_HealthManage newHealthManage = new Model.Administrative_HealthManage
            {
                HealthManageId = healthManage.HealthManageId,
                ProjectId = healthManage.ProjectId,
                PersonId = healthManage.PersonId,
                Age = healthManage.Age,
                Bloodtype = healthManage.Bloodtype,
                HealthState = healthManage.HealthState,
                Taboo = healthManage.Taboo,
                CheckTime = healthManage.CheckTime,
                Remark = healthManage.Remark,
                States = healthManage.States,
                CompileMan = healthManage.CompileMan,
                CompileDate = healthManage.CompileDate
            };
            db.Administrative_HealthManage.InsertOnSubmit(newHealthManage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改职业健康
        /// </summary>
        /// <param name="healthManage"></param>
        public static void UpdateHealthManage(Model.Administrative_HealthManage healthManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_HealthManage newHealthManage = db.Administrative_HealthManage.FirstOrDefault(e => e.HealthManageId == healthManage.HealthManageId);
            if (newHealthManage != null)
            {
                //newHealthManage.ProjectId = healthManage.ProjectId;
                newHealthManage.PersonId = healthManage.PersonId;
                newHealthManage.Age = healthManage.Age;
                newHealthManage.Bloodtype = healthManage.Bloodtype;
                newHealthManage.HealthState = healthManage.HealthState;
                newHealthManage.Taboo = healthManage.Taboo;
                newHealthManage.CheckTime = healthManage.CheckTime;
                newHealthManage.Remark = healthManage.Remark;
                newHealthManage.States = healthManage.States;
                newHealthManage.CompileMan = healthManage.CompileMan;
                newHealthManage.CompileDate = healthManage.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除职业健康
        /// </summary>
        /// <param name="healthManageId"></param>
        public static void DeleteHealthManageById(string healthManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_HealthManage healthManage = db.Administrative_HealthManage.FirstOrDefault(e => e.HealthManageId == healthManageId);
            if (healthManage != null)
            {
                BLL.CommonService.DeleteAttachFileById(healthManageId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(healthManage.HealthManageId);
                db.Administrative_HealthManage.DeleteOnSubmit(healthManage);
                db.SubmitChanges();
            }
        }
    }
}

