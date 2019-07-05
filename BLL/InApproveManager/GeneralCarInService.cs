using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 普通车辆入场审批
    /// </summary>
   public static class GeneralCarInService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取普通车辆入场审批
        /// </summary>
        /// <param name="GeneralCarInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GeneralCarIn GetGeneralCarInById(string GeneralCarInId)
        {
            return Funs.DB.InApproveManager_GeneralCarIn.FirstOrDefault(e => e.GeneralCarInId == GeneralCarInId);
        }

        /// <summary>
        /// 添加普通车辆入场审批
        /// </summary>
        /// <param name="GeneralCarIn"></param>
        public static void AddGeneralCarIn(Model.InApproveManager_GeneralCarIn GeneralCarIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GeneralCarIn newGeneralCarIn = new Model.InApproveManager_GeneralCarIn
            {
                GeneralCarInId = GeneralCarIn.GeneralCarInId,
                ProjectId = GeneralCarIn.ProjectId,
                UnitId = GeneralCarIn.UnitId,
                DriverName = GeneralCarIn.DriverName,
                CarNum = GeneralCarIn.CarNum,
                CarType = GeneralCarIn.CarType,
                Descriptions = GeneralCarIn.Descriptions,
                States = GeneralCarIn.States,
                CompileMan = GeneralCarIn.CompileMan,
                CompileDate = GeneralCarIn.CompileDate
            };
            db.InApproveManager_GeneralCarIn.InsertOnSubmit(newGeneralCarIn);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改普通车辆入场审批
        /// </summary>
        /// <param name="GeneralCarIn"></param>
        public static void UpdateGeneralCarIn(Model.InApproveManager_GeneralCarIn GeneralCarIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GeneralCarIn newGeneralCarIn = db.InApproveManager_GeneralCarIn.FirstOrDefault(e => e.GeneralCarInId == GeneralCarIn.GeneralCarInId);
            if (newGeneralCarIn != null)
            {
               // newGeneralCarIn.ProjectId = GeneralCarIn.ProjectId;
                newGeneralCarIn.UnitId = GeneralCarIn.UnitId;
                newGeneralCarIn.DriverName = GeneralCarIn.DriverName;
                newGeneralCarIn.CarNum = GeneralCarIn.CarNum;
                newGeneralCarIn.CarType = GeneralCarIn.CarType;
                newGeneralCarIn.Descriptions = GeneralCarIn.Descriptions;
                newGeneralCarIn.States = GeneralCarIn.States;
                newGeneralCarIn.CompileMan = GeneralCarIn.CompileMan;
                newGeneralCarIn.CompileDate = GeneralCarIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除普通车辆入场审批
        /// </summary>
        /// <param name="GeneralCarInId"></param>
        public static void DeleteGeneralCarInById(string GeneralCarInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GeneralCarIn GeneralCarIn = db.InApproveManager_GeneralCarIn.FirstOrDefault(e => e.GeneralCarInId == GeneralCarInId);
            if (GeneralCarIn != null)
            {
                CommonService.DeleteAttachFileById(GeneralCarInId);
                CodeRecordsService.DeleteCodeRecordsByDataId(GeneralCarInId);//删除编号
                BLL.CommonService.DeleteFlowOperateByID(GeneralCarInId);  ////删除审核流程表
                db.InApproveManager_GeneralCarIn.DeleteOnSubmit(GeneralCarIn);
                db.SubmitChanges();
            }
        }
    }
}
