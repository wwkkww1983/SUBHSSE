using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HSSE_Hazard_CheckSpecialAuditService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据APP专项检查Id获取所有APP专项检查审批信息
        /// </summary>
        /// <param name="constructSolutionCode">APP专项检查Id</param>
        /// <returns>APP专项检查审批集合</returns>
        public static List<Model.HSSE_Hazard_CheckSpecialAudit> GetCheckSpecialAuditsByCheckSpecialId(string checkSpecialId)
        {
            return (from x in db.HSSE_Hazard_CheckSpecialAudit where x.CheckSpecialId == checkSpecialId select x).ToList();
        }

        /// <summary>
        /// 根据APP专项检查Id获取一个APP专项检查审批信息
        /// </summary>
        /// <param name="constructSolutionCode">APP专项检查Id</param>
        /// <returns>一个APP专项检查审批实体</returns>
        public static Model.HSSE_Hazard_CheckSpecialAudit GetCheckSpecialAuditByCheckSpecialId(string checkSpecialId)
        {
            return db.HSSE_Hazard_CheckSpecialAudit.FirstOrDefault(x => x.CheckSpecialId == checkSpecialId && x.AuditDate == null);
        }

        /// <summary>
        /// 根据APP专项检查Id和办理步骤获取一个APP专项检查审批信息
        /// </summary>
        /// <param name="constructSolutionCode">APP专项检查Id</param>
        /// <param name="handleStep">办理步骤</param>
        /// <returns>一个APP专项检查审批实体</returns>
        public static Model.HSSE_Hazard_CheckSpecialAudit GetCheckSpecialAuditByCheckSpecialIdAndHandleStep(string checkSpecialId, string handleStep)
        {
            return db.HSSE_Hazard_CheckSpecialAudit.FirstOrDefault(x => x.CheckSpecialId == checkSpecialId && x.AuditStep == handleStep);
        }

        /// <summary>
        /// 增加APP专项检查审批信息
        /// </summary>
        /// <param name="checkSpecialAudit">APP专项检查审批实体</param>
        public static void AddCheckSpecialAudit(Model.HSSE_Hazard_CheckSpecialAudit checkSpecialAudit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_CheckSpecialAudit));
            Model.HSSE_Hazard_CheckSpecialAudit newCheckSpecialAudit = new Model.HSSE_Hazard_CheckSpecialAudit();
            newCheckSpecialAudit.CheckSpecialAuditId = newKeyID;
            newCheckSpecialAudit.CheckSpecialId = checkSpecialAudit.CheckSpecialId;
            newCheckSpecialAudit.AuditMan = checkSpecialAudit.AuditMan;
            newCheckSpecialAudit.AuditDate = checkSpecialAudit.AuditDate;
            newCheckSpecialAudit.AuditStep = checkSpecialAudit.AuditStep;

            db.HSSE_Hazard_CheckSpecialAudit.InsertOnSubmit(newCheckSpecialAudit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改APP专项检查审批信息
        /// </summary>
        /// <param name="checkSpecialAudit">APP专项检查审批实体</param>
        public static void UpdateCheckSpecialAudit(Model.HSSE_Hazard_CheckSpecialAudit checkSpecialAudit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_CheckSpecialAudit newCheckSpecialAudit = db.HSSE_Hazard_CheckSpecialAudit.First(e => e.CheckSpecialId == checkSpecialAudit.CheckSpecialId && e.AuditDate == null);
            newCheckSpecialAudit.CheckSpecialId = checkSpecialAudit.CheckSpecialId;
            newCheckSpecialAudit.AuditMan = checkSpecialAudit.AuditMan;
            newCheckSpecialAudit.AuditDate = checkSpecialAudit.AuditDate;
            newCheckSpecialAudit.AuditStep = checkSpecialAudit.AuditStep;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据APP专项检查主键删除对应的所有APP专项检查审批信息
        /// </summary>
        /// <param name="constructSolutionCode">APP专项检查主键</param>
        public static void DeleteCheckSpecialAuditByCheckSpecialId(string checkSpecialId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.HSSE_Hazard_CheckSpecialAudit where x.CheckSpecialId == checkSpecialId select x).ToList();
            db.HSSE_Hazard_CheckSpecialAudit.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据用户主键获得APP专项检查审批的数量
        /// </summary>
        /// <param name="userId">角色</param>
        /// <returns></returns>
        public static int GetCheckSpecialAuditCountByUserId(string userId)
        {
            var q = (from x in Funs.DB.HSSE_Hazard_CheckSpecialAudit where x.AuditMan == userId select x).ToList();
            return q.Count();
        }
    }
}
