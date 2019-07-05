using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 气瓶出场报批主表
    /// </summary>
    public static class GasCylinderOutService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取气瓶出场报批
        /// </summary>
        /// <param name="gasCylinderOutId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GasCylinderOut GetGasCylinderOutById(string gasCylinderOutId)
        {
            return Funs.DB.InApproveManager_GasCylinderOut.FirstOrDefault(e => e.GasCylinderOutId == gasCylinderOutId);
        }

        /// <summary>
        /// 添加气瓶出场报批
        /// </summary>
        /// <param name="gasCylinderOut"></param>
        public static void AddGasCylinderOut(Model.InApproveManager_GasCylinderOut gasCylinderOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOut newGasCylinderOut = new Model.InApproveManager_GasCylinderOut
            {
                GasCylinderOutId = gasCylinderOut.GasCylinderOutId,
                ProjectId = gasCylinderOut.ProjectId,
                GasCylinderOutCode = gasCylinderOut.GasCylinderOutCode,
                UnitId = gasCylinderOut.UnitId,
                OutDate = gasCylinderOut.OutDate,
                OutTime = gasCylinderOut.OutTime,
                DriverName = gasCylinderOut.DriverName,
                DriverNum = gasCylinderOut.DriverNum,
                CarNum = gasCylinderOut.CarNum,
                LeaderName = gasCylinderOut.LeaderName,
                States = gasCylinderOut.States,
                CompileMan = gasCylinderOut.CompileMan,
                CompileDate = gasCylinderOut.CompileDate
            };
            db.InApproveManager_GasCylinderOut.InsertOnSubmit(newGasCylinderOut);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GasCylinderOutMenuId, gasCylinderOut.ProjectId, null, gasCylinderOut.GasCylinderOutId, gasCylinderOut.CompileDate);
        }

        /// <summary>
        /// 修改气瓶出场报批
        /// </summary>
        /// <param name="gasCylinderOut"></param>
        public static void UpdateGasCylinderOut(Model.InApproveManager_GasCylinderOut gasCylinderOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOut newGasCylinderOut = db.InApproveManager_GasCylinderOut.FirstOrDefault(e => e.GasCylinderOutId == gasCylinderOut.GasCylinderOutId);
            if (newGasCylinderOut != null)
            {
                //newGasCylinderOut.ProjectId = gasCylinderOut.ProjectId;
                newGasCylinderOut.GasCylinderOutCode = gasCylinderOut.GasCylinderOutCode;
                newGasCylinderOut.UnitId = gasCylinderOut.UnitId;
                newGasCylinderOut.OutDate = gasCylinderOut.OutDate;
                newGasCylinderOut.OutTime = gasCylinderOut.OutTime;
                newGasCylinderOut.DriverName = gasCylinderOut.DriverName;
                newGasCylinderOut.DriverNum = gasCylinderOut.DriverNum;
                newGasCylinderOut.CarNum = gasCylinderOut.CarNum;
                newGasCylinderOut.LeaderName = gasCylinderOut.LeaderName;
                newGasCylinderOut.States = gasCylinderOut.States;
                newGasCylinderOut.CompileMan = gasCylinderOut.CompileMan;
                newGasCylinderOut.CompileDate = gasCylinderOut.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除气瓶出场报批
        /// </summary>
        /// <param name="gasCulinderOutId"></param>
        public static void DeleteGasCylinderOutById(string gasCulinderOutId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOut gasCylinderOut = db.InApproveManager_GasCylinderOut.FirstOrDefault(e => e.GasCylinderOutId == gasCulinderOutId);
            if (gasCylinderOut != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(gasCulinderOutId);
                CommonService.DeleteAttachFileById(gasCulinderOutId);
                BLL.CommonService.DeleteFlowOperateByID(gasCulinderOutId);  ////删除审核流程表
                db.InApproveManager_GasCylinderOut.DeleteOnSubmit(gasCylinderOut);
                db.SubmitChanges();
            }
        }
    }
}