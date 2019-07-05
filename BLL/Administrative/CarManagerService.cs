using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class CarManagerService
    {
        /// <summary>
        /// 现场车辆管理
        /// </summary>
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取车辆管理信息
        /// </summary>
        /// <param name="carManagerId"></param>
        /// <returns></returns>
        public static Model.Administrative_CarManager GetCarManagerById(string carManagerId)
        {
            return Funs.DB.Administrative_CarManager.FirstOrDefault(e => e.CarManagerId == carManagerId);
        }

        /// <summary>
        /// 添加现场车辆管理
        /// </summary>
        /// <param name="carManager"></param>
        public static void AddCarManager(Model.Administrative_CarManager carManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CarManager newCarManager = new Model.Administrative_CarManager
            {
                CarManagerId = carManager.CarManagerId,
                ProjectId = carManager.ProjectId,
                CarManagerCode = carManager.CarManagerCode,
                CarName = carManager.CarName,
                CarModel = carManager.CarModel,
                BuyDate = carManager.BuyDate,
                LastYearCheckDate = carManager.LastYearCheckDate,
                InsuranceDate = carManager.InsuranceDate,
                Remark = carManager.Remark,
                CompileMan = carManager.CompileMan,
                CompileDate = carManager.CompileDate,
                States = carManager.States
            };
            db.Administrative_CarManager.InsertOnSubmit(newCarManager);
            db.SubmitChanges();
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.CarManagerMenuId, carManager.ProjectId, null, carManager.CarManagerId, carManager.CompileDate);
        }

        /// <summary>
        /// 修改现场车辆管理
        /// </summary>
        /// <param name="carManager"></param>
        public static void UpdateCarManager(Model.Administrative_CarManager carManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CarManager newCarManager = db.Administrative_CarManager.FirstOrDefault(e => e.CarManagerId == carManager.CarManagerId);
            if (newCarManager != null)
            {
                //newCarManager.ProjectId = carManager.ProjectId;
                newCarManager.CarManagerCode = carManager.CarManagerCode;
                newCarManager.CarName = carManager.CarName;
                newCarManager.CarModel = carManager.CarModel;
                newCarManager.BuyDate = carManager.BuyDate;
                newCarManager.LastYearCheckDate = carManager.LastYearCheckDate;
                newCarManager.InsuranceDate = carManager.InsuranceDate;
                newCarManager.Remark = carManager.Remark;
                newCarManager.CompileMan = carManager.CompileMan;
                newCarManager.CompileDate = carManager.CompileDate;
                newCarManager.States = carManager.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除现场车辆管理
        /// </summary>
        /// <param name="carManagerId"></param>
        public static void DeleteCarManagerById(string carManagerId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CarManager carManager = db.Administrative_CarManager.FirstOrDefault(e => e.CarManagerId == carManagerId);
            if (carManager != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(carManagerId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(carManager.CarManagerId);
                db.Administrative_CarManager.DeleteOnSubmit(carManager);
                db.SubmitChanges();
            }
        }
    }
}
