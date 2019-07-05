using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种车辆入场审批主表
    /// </summary>
    public static class CarInService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种车辆入场审批
        /// </summary>
        /// <param name="carInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_CarIn GetCarInById(string carInId)
        {
            return Funs.DB.InApproveManager_CarIn.FirstOrDefault(e => e.CarInId == carInId);
        }

        /// <summary>
        /// 添加特种车辆入场审批
        /// </summary>
        /// <param name="carIn"></param>
        public static void AddCarIn(Model.InApproveManager_CarIn carIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_CarIn newCarIn = new Model.InApproveManager_CarIn
            {
                CarInId = carIn.CarInId,
                ProjectId = carIn.ProjectId,
                UnitId = carIn.UnitId,
                DriverName = carIn.DriverName,
                CarNum = carIn.CarNum,
                CarType = carIn.CarType,
                States = carIn.States,
                CompileMan = carIn.CompileMan,
                CompileDate = carIn.CompileDate
            };
            db.InApproveManager_CarIn.InsertOnSubmit(newCarIn);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特种车辆入场审批
        /// </summary>
        /// <param name="carIn"></param>
        public static void UpdateCarIn(Model.InApproveManager_CarIn carIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_CarIn newCarIn = db.InApproveManager_CarIn.FirstOrDefault(e => e.CarInId == carIn.CarInId);
            if (newCarIn != null)
            {
                //newCarIn.ProjectId = carIn.ProjectId;
                newCarIn.UnitId = carIn.UnitId;
                newCarIn.DriverName = carIn.DriverName;
                newCarIn.CarNum = carIn.CarNum;
                newCarIn.CarType = carIn.CarType;
                newCarIn.States = carIn.States;
                newCarIn.CompileMan = carIn.CompileMan;
                newCarIn.CompileDate = carIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种车辆入场审批
        /// </summary>
        /// <param name="carInId"></param>
        public static void DeleteCarInById(string carInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_CarIn carIn = db.InApproveManager_CarIn.FirstOrDefault(e => e.CarInId == carInId);
            if (carIn != null)
            {
                CommonService.DeleteAttachFileById(carInId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(carInId);
                db.InApproveManager_CarIn.DeleteOnSubmit(carIn);
                db.SubmitChanges();
            }
        }
    }
}
