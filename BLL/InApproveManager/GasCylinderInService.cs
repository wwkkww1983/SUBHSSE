using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 气瓶入场报批主表
    /// </summary>
    public static class GasCylinderInService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取气瓶入场报批
        /// </summary>
        /// <param name="gasCylinderInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GasCylinderIn GetGasCylinderInById(string gasCylinderInId)
        {
            return Funs.DB.InApproveManager_GasCylinderIn.FirstOrDefault(e => e.GasCylinderInId == gasCylinderInId);
        }

        /// <summary>
        /// 添加气瓶入场报批
        /// </summary>
        /// <param name="gasCylinderIn"></param>
        public static void AddGasCylinderIn(Model.InApproveManager_GasCylinderIn gasCylinderIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderIn newGasCylinderIn = new Model.InApproveManager_GasCylinderIn
            {
                GasCylinderInId = gasCylinderIn.GasCylinderInId,
                ProjectId = gasCylinderIn.ProjectId,
                GasCylinderInCode = gasCylinderIn.GasCylinderInCode,
                UnitId = gasCylinderIn.UnitId,
                InDate = gasCylinderIn.InDate,
                InTime = gasCylinderIn.InTime,
                DriverMan = gasCylinderIn.DriverMan,
                DriverNum = gasCylinderIn.DriverNum,
                CarNum = gasCylinderIn.CarNum,
                LeadCarMan = gasCylinderIn.LeadCarMan,
                States = gasCylinderIn.States,
                CompileMan = gasCylinderIn.CompileMan,
                CompileDate = gasCylinderIn.CompileDate
            };
            db.InApproveManager_GasCylinderIn.InsertOnSubmit(newGasCylinderIn);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GasCylinderInMenuId, gasCylinderIn.ProjectId, null, gasCylinderIn.GasCylinderInId, gasCylinderIn.CompileDate);
        }

        /// <summary>
        /// 修改气瓶入场报批
        /// </summary>
        /// <param name="gasCylinderIn"></param>
        public static void UpdateGasCylinderIn(Model.InApproveManager_GasCylinderIn gasCylinderIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderIn newGasCylinderIn = db.InApproveManager_GasCylinderIn.FirstOrDefault(e => e.GasCylinderInId == gasCylinderIn.GasCylinderInId);
            if (newGasCylinderIn != null)
            {
                //newGasCylinderIn.ProjectId = gasCylinderIn.ProjectId;
                newGasCylinderIn.GasCylinderInCode = gasCylinderIn.GasCylinderInCode;
                newGasCylinderIn.UnitId = gasCylinderIn.UnitId;
                newGasCylinderIn.InDate = gasCylinderIn.InDate;
                newGasCylinderIn.InTime = gasCylinderIn.InTime;
                newGasCylinderIn.DriverMan = gasCylinderIn.DriverMan;
                newGasCylinderIn.DriverNum = gasCylinderIn.DriverNum;
                newGasCylinderIn.CarNum = gasCylinderIn.CarNum;
                newGasCylinderIn.LeadCarMan = gasCylinderIn.LeadCarMan;
                newGasCylinderIn.States = gasCylinderIn.States;
                newGasCylinderIn.CompileMan = gasCylinderIn.CompileMan;
                newGasCylinderIn.CompileDate = gasCylinderIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除气瓶入场报批
        /// </summary>
        /// <param name="gasCylinderInId"></param>
        public static void DeleteGasCylinderInById(string gasCylinderInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderIn gasCylinderIn = db.InApproveManager_GasCylinderIn.FirstOrDefault(e => e.GasCylinderInId == gasCylinderInId);
            if (gasCylinderIn != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(gasCylinderInId);
                CommonService.DeleteAttachFileById(gasCylinderInId);
                BLL.CommonService.DeleteFlowOperateByID(gasCylinderInId);  ////删除审核流程表
                db.InApproveManager_GasCylinderIn.DeleteOnSubmit(gasCylinderIn);
                db.SubmitChanges();
            }
        }
    }
}
