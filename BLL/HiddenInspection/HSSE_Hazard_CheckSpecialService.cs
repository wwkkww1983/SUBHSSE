using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// APP专项检查表
    /// </summary>
    public static class HSSE_Hazard_CheckSpecialService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据APP专项检查Id获取一个APP专项检查信息
        /// </summary>
        /// <param name="checkSpecialId">APP专项检查Id</param>
        /// <returns>一个APP专项检查实体</returns>
        public static Model.HSSE_Hazard_CheckSpecial GetCheckSpecialByCheckSpecialId(string checkSpecialId)
        {
            return Funs.DB.HSSE_Hazard_CheckSpecial.FirstOrDefault(x => x.CheckSpecialId == checkSpecialId);
        }

        /// <summary>
        /// 根据APP专项检查编号获取一个APP专项检查信息
        /// </summary>
        /// <param name="checkSpecialCode">APP专项检查编号</param>
        /// <returns>一个APP专项检查实体</returns>
        public static Model.HSSE_Hazard_CheckSpecial GetCheckSpecialByCheckSpecialCode(string checkSpecialCode)
        {
            return Funs.DB.HSSE_Hazard_CheckSpecial.FirstOrDefault(x => x.CheckSpecialCode == checkSpecialCode);
        }

        /// <summary>
        /// 增加APP专项检查信息
        /// </summary>
        /// <param name="checkSpecial">APP专项检查实体</param>
        public static void AddCheckSpecial(Model.HSSE_Hazard_CheckSpecial checkSpecial)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_CheckSpecial newCheckSpecial = new Model.HSSE_Hazard_CheckSpecial();
            newCheckSpecial.CheckSpecialId = checkSpecial.CheckSpecialId;
            newCheckSpecial.CheckSpecialCode = checkSpecial.CheckSpecialCode;
            newCheckSpecial.ProjectId = checkSpecial.ProjectId;
            newCheckSpecial.UnitId = checkSpecial.UnitId;
            newCheckSpecial.Date = checkSpecial.Date;
            newCheckSpecial.CheckMan = checkSpecial.CheckMan;
            newCheckSpecial.JointCheckMan = checkSpecial.JointCheckMan;
            newCheckSpecial.States = checkSpecial.States;
            newCheckSpecial.IsReceived = checkSpecial.IsReceived;

            db.HSSE_Hazard_CheckSpecial.InsertOnSubmit(newCheckSpecial);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改APP专项检查信息
        /// </summary>
        /// <param name="checkSpecial">APP专项检查实体</param>
        public static void UpdateCheckSpecial(Model.HSSE_Hazard_CheckSpecial checkSpecial)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_CheckSpecial newCheckSpecial = db.HSSE_Hazard_CheckSpecial.First(e => e.CheckSpecialId == checkSpecial.CheckSpecialId);
            newCheckSpecial.CheckSpecialCode = checkSpecial.CheckSpecialCode;
            newCheckSpecial.UnitId = checkSpecial.UnitId;
            newCheckSpecial.Date = checkSpecial.Date;
            newCheckSpecial.CheckMan = checkSpecial.CheckMan;
            newCheckSpecial.JointCheckMan = checkSpecial.JointCheckMan;
            newCheckSpecial.States = checkSpecial.States;
            newCheckSpecial.IsReceived = checkSpecial.IsReceived;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据APP专项检查Id删除一个APP专项检查信息
        /// </summary>
        /// <param name="checkSpecialId">APP专项检查Id</param>
        public static void DeleteCheckSpecial(string checkSpecialId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_CheckSpecial checkSpecial = db.HSSE_Hazard_CheckSpecial.First(e => e.CheckSpecialId == checkSpecialId);
            if (checkSpecial != null)
            {
                db.HSSE_Hazard_CheckSpecial.DeleteOnSubmit(checkSpecial);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<Model.HandleStep> GetNetAuditStepByState(string state)
        {
            List<Model.HandleStep> handleSteps = new List<Model.HandleStep>();
            if (state == BLL.Const.APPCheckSpecial_Compile || state == BLL.Const.APPCheckSpecial_ReCompile)
            {
                Model.HandleStep step1 = new Model.HandleStep();
                step1.Id = Const.APPCheckSpecial_Check;
                step1.Name = "下一步";
                handleSteps.Add(step1);
                Model.HandleStep step8 = new Model.HandleStep();
                step8.Id = Const.APPCheckSpecial_ApproveCompleted;
                step8.Name = "审批完成";
                handleSteps.Add(step8);
            }
            else if (state == BLL.Const.APPCheckSpecial_Check)
            {
                Model.HandleStep step1 = new Model.HandleStep();
                step1.Id = Const.APPCheckSpecial_Check;
                step1.Name = "下一步";
                handleSteps.Add(step1);
                Model.HandleStep step8 = new Model.HandleStep();
                step8.Id = Const.APPCheckSpecial_ApproveCompleted;
                step8.Name = "审批完成";
                handleSteps.Add(step8);
            }
            return handleSteps;
        }
    }
}
