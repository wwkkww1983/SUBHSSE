using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class DriverManagerService
    {
        /// <summary>
        /// 现场驾驶员管理
        /// </summary>
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取驾驶员管理信息
        /// </summary>
        /// <param name="DriverManagerId"></param>
        /// <returns></returns>
        public static Model.Administrative_DriverManager GetDriverManagerById(string DriverManagerId)
        {
            return Funs.DB.Administrative_DriverManager.FirstOrDefault(e => e.DriverManagerId == DriverManagerId);
        }

        /// <summary>
        /// 添加现场驾驶员管理
        /// </summary>
        /// <param name="DriverManager"></param>
        public static void AddDriverManager(Model.Administrative_DriverManager DriverManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_DriverManager newDriverManager = new Model.Administrative_DriverManager
            {
                DriverManagerId = DriverManager.DriverManagerId,
                ProjectId = DriverManager.ProjectId,
                DriverManagerCode = DriverManager.DriverManagerCode,
                DriverName = DriverManager.DriverName,
                DriverCarModel = DriverManager.DriverCarModel,
                DriverCode = DriverManager.DriverCode,
                DrivingDate = DriverManager.DrivingDate,
                CheckDate = DriverManager.CheckDate,
                Remark = DriverManager.Remark,
                CompileMan = DriverManager.CompileMan,
                CompileDate = DriverManager.CompileDate,
                States = DriverManager.States
            };
            db.Administrative_DriverManager.InsertOnSubmit(newDriverManager);
            db.SubmitChanges();
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.DriverManagerMenuId, DriverManager.ProjectId, null, DriverManager.DriverManagerId, DriverManager.CompileDate);
        }

        /// <summary>
        /// 修改现场驾驶员管理
        /// </summary>
        /// <param name="DriverManager"></param>
        public static void UpdateDriverManager(Model.Administrative_DriverManager DriverManager)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_DriverManager newDriverManager = db.Administrative_DriverManager.FirstOrDefault(e => e.DriverManagerId == DriverManager.DriverManagerId);
            if (newDriverManager != null)
            {
                //newDriverManager.ProjectId = DriverManager.ProjectId;
                newDriverManager.DriverManagerCode = DriverManager.DriverManagerCode;
                newDriverManager.DriverName = DriverManager.DriverName;
                newDriverManager.DriverCarModel = DriverManager.DriverCarModel;
                newDriverManager.DriverCode = DriverManager.DriverCode;
                newDriverManager.DrivingDate = DriverManager.DrivingDate;
                newDriverManager.CheckDate = DriverManager.CheckDate;
                newDriverManager.Remark = DriverManager.Remark;
                newDriverManager.CompileMan = DriverManager.CompileMan;
                newDriverManager.CompileDate = DriverManager.CompileDate;
                newDriverManager.States = DriverManager.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除现场驾驶员管理
        /// </summary>
        /// <param name="DriverManagerId"></param>
        public static void DeleteDriverManagerById(string DriverManagerId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_DriverManager DriverManager = db.Administrative_DriverManager.FirstOrDefault(e => e.DriverManagerId == DriverManagerId);
            if (DriverManager != null)
            {
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(DriverManagerId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(DriverManager.DriverManagerId);
                db.Administrative_DriverManager.DeleteOnSubmit(DriverManager);
                db.SubmitChanges();
            }
        }
    }
}
