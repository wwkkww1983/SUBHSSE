using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class RegistrationRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据隐患巡检记录Id获取一个隐患巡检记录信息
        /// </summary>
        /// <param name="registrationRecordId">隐患巡检记录Id</param>
        /// <returns>一个隐患巡检记录实体</returns>
        public static Model.Inspection_RegistrationRecord GetRegisterRecordByRegisterRecordId(string registrationRecordId)
        {
            return Funs.DB.Inspection_RegistrationRecord.FirstOrDefault(x => x.RegistrationRecordId == registrationRecordId);
        }

        /// <summary>
        /// 根据巡检人和日期获取一个隐患巡检记录信息
        /// </summary>
        /// <param name="checkMan">巡检人</param>
        /// <param name="checkDate">日期</param>
        /// <returns>一个隐患巡检记录实体</returns>
        public static Model.Inspection_RegistrationRecord GetRegisterRecordByCheckManAndDate(string checkMan, DateTime? checkDate)
        {
            return Funs.DB.Inspection_RegistrationRecord.FirstOrDefault(x => x.CheckPerson == checkMan && x.CheckDate == checkDate);
        }

        /// <summary>
        /// 增加隐患巡检记录信息
        /// </summary>
        /// <param name="RegistrationRecord">隐患巡检记录实体</param>
        public static void AddRegisterRecord(Model.Inspection_RegistrationRecord RegistrationRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Inspection_RegistrationRecord newRegisterRecord = new Model.Inspection_RegistrationRecord
            {
                RegistrationRecordId = RegistrationRecord.RegistrationRecordId,
                ProjectId = RegistrationRecord.ProjectId,
                RegistrationIds = RegistrationRecord.RegistrationIds,
                CheckDate = RegistrationRecord.CheckDate,
                CheckPerson = RegistrationRecord.CheckPerson,
                CompileMan = RegistrationRecord.CompileMan,
                CompileDate = RegistrationRecord.CompileDate
            };

            db.Inspection_RegistrationRecord.InsertOnSubmit(newRegisterRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改隐患巡检记录信息
        /// </summary>
        /// <param name="RegistrationRecord">隐患巡检记录实体</param>
        public static void UpdateRegisterRecord(Model.Inspection_RegistrationRecord RegistrationRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Inspection_RegistrationRecord newRegisterRecord = db.Inspection_RegistrationRecord.First(e => e.RegistrationRecordId == RegistrationRecord.RegistrationRecordId);
            newRegisterRecord.RegistrationIds = RegistrationRecord.RegistrationIds;
            newRegisterRecord.CheckDate = RegistrationRecord.CheckDate;
            newRegisterRecord.CheckPerson = RegistrationRecord.CheckPerson;
            newRegisterRecord.CompileMan = RegistrationRecord.CompileMan;
            newRegisterRecord.CompileDate = RegistrationRecord.CompileDate;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据隐患巡检记录Id删除一个隐患巡检记录信息
        /// </summary>
        /// <param name="RegistrationRecordId">隐患巡检记录Id</param>
        public static void DeleteRegisterRecordByRegisterRecordId(string registrationRecordId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Inspection_RegistrationRecord RegistrationRecord = db.Inspection_RegistrationRecord.First(e => e.RegistrationRecordId == registrationRecordId);

            db.Inspection_RegistrationRecord.DeleteOnSubmit(RegistrationRecord);
            db.SubmitChanges();
        }
    }
}
