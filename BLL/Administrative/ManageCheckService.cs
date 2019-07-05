using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 行政管理检查记录
    /// </summary>
    public static class ManageCheckService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取行政管理检查记录
        /// </summary>
        /// <param name="manageCheckId"></param>
        /// <returns></returns>
        public static Model.Administrative_ManageCheck GetManageCheckById(string manageCheckId)
        {
            return Funs.DB.Administrative_ManageCheck.FirstOrDefault(e => e.ManageCheckId == manageCheckId);
        }

        /// <summary>
        /// 添加行政管理检查记录
        /// </summary>
        /// <param name="manageCheck"></param>
        public static void AddManageCheck(Model.Administrative_ManageCheck manageCheck)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_ManageCheck newManageCheck = new Model.Administrative_ManageCheck
            {
                ManageCheckId = manageCheck.ManageCheckId,
                ProjectId = manageCheck.ProjectId,
                ManageCheckCode = manageCheck.ManageCheckCode,
                CheckTypeCode = manageCheck.CheckTypeCode,
                SupplyCheck = manageCheck.SupplyCheck,
                IsSupplyCheck = manageCheck.IsSupplyCheck,
                ViolationRule = manageCheck.ViolationRule,
                ToViolationHandleCode = manageCheck.ToViolationHandleCode,
                CheckPerson = manageCheck.CheckPerson,
                CheckTime = manageCheck.CheckTime,
                VerifyPerson = manageCheck.VerifyPerson,
                VerifyTime = manageCheck.VerifyTime,
                States = manageCheck.States,
                CompileMan = manageCheck.CompileMan,
                CompileDate = manageCheck.CompileDate
            };
            db.Administrative_ManageCheck.InsertOnSubmit(newManageCheck);
            db.SubmitChanges();
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ManageCheckMenuId, manageCheck.ProjectId, null, manageCheck.ManageCheckId, manageCheck.CheckTime);
        }

        /// <summary>
        /// 修改行政管理检查记录
        /// </summary>
        /// <param name="manageCheck"></param>
        public static void UpdateManageCheck(Model.Administrative_ManageCheck manageCheck)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_ManageCheck newManageCheck = db.Administrative_ManageCheck.FirstOrDefault(e => e.ManageCheckId == manageCheck.ManageCheckId);
            if (newManageCheck != null)
            {
                newManageCheck.ProjectId = manageCheck.ProjectId;
                newManageCheck.ManageCheckCode = manageCheck.ManageCheckCode;
                newManageCheck.CheckTypeCode = manageCheck.CheckTypeCode;
                newManageCheck.SupplyCheck = manageCheck.SupplyCheck;
                newManageCheck.IsSupplyCheck = manageCheck.IsSupplyCheck;
                newManageCheck.ViolationRule = manageCheck.ViolationRule;
                newManageCheck.ToViolationHandleCode = manageCheck.ToViolationHandleCode;
                newManageCheck.CheckPerson = manageCheck.CheckPerson;
                newManageCheck.CheckTime = manageCheck.CheckTime;
                newManageCheck.VerifyPerson = manageCheck.VerifyPerson;
                newManageCheck.VerifyTime = manageCheck.VerifyTime;
                newManageCheck.States = manageCheck.States;
                newManageCheck.CompileMan = manageCheck.CompileMan;
                newManageCheck.CompileDate = manageCheck.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除行政管理检查记录
        /// </summary>
        /// <param name="manageCheckId"></param>
        public static void DeleteManageCheckById(string manageCheckId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_ManageCheck manageCheck = db.Administrative_ManageCheck.FirstOrDefault(e => e.ManageCheckId == manageCheckId);
            if (manageCheck != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(manageCheckId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(manageCheckId);
                db.Administrative_ManageCheck.DeleteOnSubmit(manageCheck);
                db.SubmitChanges();
            }
        }
    }
}
