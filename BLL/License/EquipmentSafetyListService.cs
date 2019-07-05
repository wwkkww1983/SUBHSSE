using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 施工机具、安全设施检查验收
    /// </summary>
    public static class EquipmentSafetyListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取施工机具、安全设施检查验收
        /// </summary>
        /// <param name="equipmentSafetyListId"></param>
        /// <returns></returns>
        public static Model.License_EquipmentSafetyList GetEquipmentSafetyListById(string equipmentSafetyListId)
        {
            return Funs.DB.License_EquipmentSafetyList.FirstOrDefault(e => e.EquipmentSafetyListId == equipmentSafetyListId);
        }

        /// <summary>
        /// 根据时间段获取集合数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的集合数量</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            var Safety = (from x in Funs.DB.License_EquipmentSafetyList
                          where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId
                          select x.EquipmentSafetyListCount ?? 0).ToList();
            if (Safety.Count > 0)
            {
                return Convert.ToInt32(Safety.Sum());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加施工机具、安全设施检查验收
        /// </summary>
        /// <param name="equipmentSafetyList"></param>
        public static void AddEquipmentSafetyList(Model.License_EquipmentSafetyList equipmentSafetyList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_EquipmentSafetyList newEquipmentSafetyList = new Model.License_EquipmentSafetyList
            {
                EquipmentSafetyListId = equipmentSafetyList.EquipmentSafetyListId,
                ProjectId = equipmentSafetyList.ProjectId,
                EquipmentSafetyListCode = equipmentSafetyList.EquipmentSafetyListCode,
                EquipmentSafetyListName = equipmentSafetyList.EquipmentSafetyListName,
                UnitId = equipmentSafetyList.UnitId,
                EquipmentSafetyListCount = equipmentSafetyList.EquipmentSafetyListCount,
                WorkAreaId = equipmentSafetyList.WorkAreaId,
                CompileMan = equipmentSafetyList.CompileMan,
                CompileDate = equipmentSafetyList.CompileDate,
                States = equipmentSafetyList.States,
                SendMan = equipmentSafetyList.SendMan
            };
            db.License_EquipmentSafetyList.InsertOnSubmit(newEquipmentSafetyList);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEquipmentSafetyListMenuId, equipmentSafetyList.ProjectId, null, equipmentSafetyList.EquipmentSafetyListId, equipmentSafetyList.CompileDate);
        }

        /// <summary>
        /// 修改施工机具、安全设施检查验收
        /// </summary>
        /// <param name="equipmentSafetyList"></param>
        public static void UpdateEquipmentSafetyList(Model.License_EquipmentSafetyList equipmentSafetyList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_EquipmentSafetyList newEquipmentSafetyList = db.License_EquipmentSafetyList.FirstOrDefault(e => e.EquipmentSafetyListId == equipmentSafetyList.EquipmentSafetyListId);
            if (newEquipmentSafetyList != null)
            {
                //newEquipmentSafetyList.ProjectId = equipmentSafetyList.ProjectId;
                newEquipmentSafetyList.EquipmentSafetyListCode = equipmentSafetyList.EquipmentSafetyListCode;
                newEquipmentSafetyList.EquipmentSafetyListName = equipmentSafetyList.EquipmentSafetyListName;
                newEquipmentSafetyList.UnitId = equipmentSafetyList.UnitId;
                newEquipmentSafetyList.EquipmentSafetyListCount = equipmentSafetyList.EquipmentSafetyListCount;
                newEquipmentSafetyList.WorkAreaId = equipmentSafetyList.WorkAreaId;
                newEquipmentSafetyList.CompileMan = equipmentSafetyList.CompileMan;
                newEquipmentSafetyList.CompileDate = equipmentSafetyList.CompileDate;
                newEquipmentSafetyList.States = equipmentSafetyList.States;
                newEquipmentSafetyList.SendMan = equipmentSafetyList.SendMan;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除施工机具、安全设施检查验收
        /// </summary>
        /// <param name="equipmentSafetyListId"></param>
        public static void DeleteEquipmentSafetyListById(string equipmentSafetyListId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_EquipmentSafetyList equipmentSafetyList = db.License_EquipmentSafetyList.FirstOrDefault(e => e.EquipmentSafetyListId == equipmentSafetyListId);
            if (equipmentSafetyList != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(equipmentSafetyListId);//删除编号
                BLL.CommonService.DeleteAttachFileById(equipmentSafetyListId);//删除附件
                ///删除工程师日志收集记录
                var flowOperate = from x in db.Sys_FlowOperate where x.DataId == equipmentSafetyList.EquipmentSafetyListId select x;
                if (flowOperate.Count() > 0)
                {
                    foreach (var item in flowOperate)
                    {
                        BLL.HSSELogService.CollectHSSELog(equipmentSafetyList.ProjectId, item.OperaterId, item.OperaterTime, "24", equipmentSafetyList.EquipmentSafetyListName, Const.BtnDelete, 1);
                    }
                    ////删除流程表
                    BLL.CommonService.DeleteFlowOperateByID(equipmentSafetyList.EquipmentSafetyListId);
                }
                db.License_EquipmentSafetyList.DeleteOnSubmit(equipmentSafetyList);
                db.SubmitChanges();
            }
        }
    }
}
