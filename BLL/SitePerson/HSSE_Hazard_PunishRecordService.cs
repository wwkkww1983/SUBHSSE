using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HSSE_Hazard_PunishRecordService
    {
        /// <summary>
        /// 根据主键获取处罚记录明细
        /// </summary>
        /// <param name="EquipmentQualityAuditDetailId"></param>
        /// <returns></returns>
        public static Model.HSSE_Hazard_PunishRecord GetPunishRecordById(string punishRecordId)
        {
            return Funs.DB.HSSE_Hazard_PunishRecord.FirstOrDefault(e => e.PunishRecordId == punishRecordId);
        }

        /// <summary>
        /// 根据人员ID返回焊工处罚记录
        /// </summary>
        /// <param name="welderId"></param>
        /// <returns></returns>
        public static List<Model.HSSE_Hazard_PunishRecord> GetPunishRecordByPersonId(string personId)
        {
            return (from x in Funs.DB.HSSE_Hazard_PunishRecord where x.PersonId == personId select x).ToList();
        }

        /// <summary>
        /// 人员处罚记录明细表增加
        /// </summary>
        /// <param name="unitShortList"></param>
        public static void AddPunishRecord(Model.HSSE_Hazard_PunishRecord item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_PunishRecord newPunishRecord = new Model.HSSE_Hazard_PunishRecord();
            newPunishRecord.PunishRecordId = item.PunishRecordId;
            newPunishRecord.PersonId = item.PersonId;
            newPunishRecord.PunishItemId = item.PunishItemId;
            newPunishRecord.PunishDate = item.PunishDate;
            newPunishRecord.CompileMan = item.CompileMan;
            newPunishRecord.HazardRegisterId = item.HazardRegisterId;
            Funs.DB.HSSE_Hazard_PunishRecord.InsertOnSubmit(newPunishRecord);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 人员处罚记录明细表修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdatePunishRecord(Model.HSSE_Hazard_PunishRecord punishRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_PunishRecord newPunishRecord = db.HSSE_Hazard_PunishRecord.FirstOrDefault(e => e.PunishRecordId == punishRecord.PunishRecordId);
            if (newPunishRecord != null)
            {
                newPunishRecord.PunishItemId = punishRecord.PunishItemId;
                newPunishRecord.PunishDate = punishRecord.PunishDate;
                newPunishRecord.CompileMan = punishRecord.CompileMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据处罚记录明细Id删除焊工对应处罚记录明细
        /// </summary>
        /// <param name="shortListId"></param>
        public static void DeletePunishRecordByPunishRecordId(string punishRecordId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.HSSE_Hazard_PunishRecord where x.PunishRecordId == punishRecordId select x).FirstOrDefault();
            if (q != null)
            {
                db.HSSE_Hazard_PunishRecord.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊工Id删除所有对应焊工处罚记录明细
        /// </summary>
        /// <param name="shortListId"></param>
        public static void DeletePunishRecordByPersonId(string personId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.HSSE_Hazard_PunishRecord where x.PersonId == personId select x).ToList();
            if (q.Count() > 0)
            {
                db.HSSE_Hazard_PunishRecord.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
