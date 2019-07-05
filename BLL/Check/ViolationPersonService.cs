using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 违规人员记录
    /// </summary>
    public static class ViolationPersonService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取违规人员记录
        /// </summary>
        /// <param name="violationPersonId"></param>
        /// <returns></returns>
        public static Model.Check_ViolationPerson GetViolationPersonById(string violationPersonId)
        {
            return Funs.DB.Check_ViolationPerson.FirstOrDefault(e => e.ViolationPersonId == violationPersonId);
        }

        /// <summary>
        /// 获取一段时间内人员违规人员次数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="handleStep">违规类型</param>
        /// <returns></returns>
        public static int GetViolationPersonNum(DateTime startTime, DateTime endTime, string handleStep, string projectId)
        {
            return (from x in Funs.DB.Check_ViolationPerson where x.ViolationDate >= startTime && x.ViolationDate < endTime && x.HandleStep == handleStep && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 添加违规人员记录
        /// </summary>
        /// <param name="violationPerson"></param>
        public static void AddViolationPerson(Model.Check_ViolationPerson violationPerson)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ViolationPerson newViolationPerson = new Model.Check_ViolationPerson
            {
                ViolationPersonId = violationPerson.ViolationPersonId,
                ProjectId = violationPerson.ProjectId,
                ViolationPersonCode = violationPerson.ViolationPersonCode,
                UnitId = violationPerson.UnitId,
                PersonId = violationPerson.PersonId,
                WorkPostId = violationPerson.WorkPostId,
                ViolationDate = violationPerson.ViolationDate,
                ViolationName = violationPerson.ViolationName,
                ViolationType = violationPerson.ViolationType,
                CompileMan = violationPerson.CompileMan
            };
            newViolationPerson.ViolationDate = violationPerson.ViolationDate;
            newViolationPerson.States = violationPerson.States;
            newViolationPerson.HandleStep = violationPerson.HandleStep;
            newViolationPerson.ViolationDef = violationPerson.ViolationDef;
            db.Check_ViolationPerson.InsertOnSubmit(newViolationPerson);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectViolationPersonMenuId, violationPerson.ProjectId, null, violationPerson.ViolationPersonId, violationPerson.CompileDate);
        }

        /// <summary>
        /// 修改违规人员记录
        /// </summary>
        /// <param name="violationPerson"></param>
        public static void UpdateViolationPerson(Model.Check_ViolationPerson violationPerson)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ViolationPerson newViolationPerson = db.Check_ViolationPerson.FirstOrDefault(e => e.ViolationPersonId == violationPerson.ViolationPersonId);
            if (newViolationPerson != null)
            {
                //newViolationPerson.ProjectId = violationPerson.ProjectId;
                newViolationPerson.ViolationPersonCode = violationPerson.ViolationPersonCode;
                newViolationPerson.UnitId = violationPerson.UnitId;
                newViolationPerson.PersonId = violationPerson.PersonId;
                newViolationPerson.WorkPostId = violationPerson.WorkPostId;
                newViolationPerson.ViolationDate = violationPerson.ViolationDate;
                newViolationPerson.ViolationName = violationPerson.ViolationName;
                newViolationPerson.ViolationType = violationPerson.ViolationType;
                newViolationPerson.CompileMan = violationPerson.CompileMan;
                newViolationPerson.ViolationDate = violationPerson.ViolationDate;
                newViolationPerson.States = violationPerson.States;
                newViolationPerson.HandleStep = violationPerson.HandleStep;
                newViolationPerson.ViolationDef = violationPerson.ViolationDef;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除违规人员记录
        /// </summary>
        /// <param name="violationPersonId"></param>
        public static void DeleteViolationPersonById(string violationPersonId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ViolationPerson violationPerson = db.Check_ViolationPerson.FirstOrDefault(e => e.ViolationPersonId == violationPersonId);
            if (violationPerson != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(violationPersonId);
                CommonService.DeleteFlowOperateByID(violationPersonId);
                db.Check_ViolationPerson.DeleteOnSubmit(violationPerson);
                db.SubmitChanges();
            }
        }
    }
}
