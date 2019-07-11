using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class HSSE_Hazard_HazardRegisterRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据隐患巡检记录Id获取一个隐患巡检记录信息
        /// </summary>
        /// <param name="hazardRegisterRecordId">隐患巡检记录Id</param>
        /// <returns>一个隐患巡检记录实体</returns>
        public static Model.HSSE_Hazard_HazardRegisterRecord GetHazardRegisterRecordByHazardRegisterRecordId(string hazardRegisterRecordId)
        {
            return Funs.DB.HSSE_Hazard_HazardRegisterRecord.FirstOrDefault(x => x.HazardRegisterRecordId == hazardRegisterRecordId);
        }

        /// <summary>
        /// 根据巡检人和日期获取一个隐患巡检记录信息
        /// </summary>
        /// <param name="checkMan">巡检人</param>
        /// <param name="checkDate">日期</param>
        /// <returns>一个隐患巡检记录实体</returns>
        public static Model.HSSE_Hazard_HazardRegisterRecord GetHazardRegisterRecordByCheckManAndDate(string checkMan, DateTime? checkDate, string type)
        {
            return Funs.DB.HSSE_Hazard_HazardRegisterRecord.FirstOrDefault(x => x.CheckPerson == checkMan && x.CheckDate == checkDate && x.CheckType == type);
        }

        /// <summary>
        /// 增加隐患巡检记录信息
        /// </summary>
        /// <param name="HazardRegisterRecord">隐患巡检记录实体</param>
        public static void AddHazardRegisterRecord(Model.HSSE_Hazard_HazardRegisterRecord HazardRegisterRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterRecord newHazardRegisterRecord = new Model.HSSE_Hazard_HazardRegisterRecord();
            newHazardRegisterRecord.HazardRegisterRecordId = HazardRegisterRecord.HazardRegisterRecordId;
            newHazardRegisterRecord.ProjectId = HazardRegisterRecord.ProjectId;
            newHazardRegisterRecord.HazardRegisterIds = HazardRegisterRecord.HazardRegisterIds;
            newHazardRegisterRecord.CheckDate = HazardRegisterRecord.CheckDate;
            newHazardRegisterRecord.CheckPerson = HazardRegisterRecord.CheckPerson;
            newHazardRegisterRecord.CheckType = HazardRegisterRecord.CheckType;
            newHazardRegisterRecord.CompileMan = HazardRegisterRecord.CompileMan;
            newHazardRegisterRecord.CompileDate = HazardRegisterRecord.CompileDate;

            db.HSSE_Hazard_HazardRegisterRecord.InsertOnSubmit(newHazardRegisterRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改隐患巡检记录信息
        /// </summary>
        /// <param name="HazardRegisterRecord">隐患巡检记录实体</param>
        public static void UpdateHazardRegisterRecord(Model.HSSE_Hazard_HazardRegisterRecord HazardRegisterRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterRecord newHazardRegisterRecord = db.HSSE_Hazard_HazardRegisterRecord.First(e => e.HazardRegisterRecordId == HazardRegisterRecord.HazardRegisterRecordId);
            newHazardRegisterRecord.HazardRegisterIds = HazardRegisterRecord.HazardRegisterIds;
            newHazardRegisterRecord.CheckDate = HazardRegisterRecord.CheckDate;
            newHazardRegisterRecord.CheckPerson = HazardRegisterRecord.CheckPerson;
            newHazardRegisterRecord.CheckType = HazardRegisterRecord.CheckType;
            newHazardRegisterRecord.CompileMan = HazardRegisterRecord.CompileMan;
            newHazardRegisterRecord.CompileDate = HazardRegisterRecord.CompileDate;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据隐患巡检记录Id删除一个隐患巡检记录信息f
        /// </summary>
        /// <param name="HazardRegisterRecordId">隐患巡检记录Id</param>
        public static void DeleteHazardRegisterRecordByHazardRegisterRecordId(string hazardRegisterRecordId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegisterRecord HazardRegisterRecord = db.HSSE_Hazard_HazardRegisterRecord.First(e => e.HazardRegisterRecordId == hazardRegisterRecordId);

            db.HSSE_Hazard_HazardRegisterRecord.DeleteOnSubmit(HazardRegisterRecord);
            db.SubmitChanges();
        }
    }
}
