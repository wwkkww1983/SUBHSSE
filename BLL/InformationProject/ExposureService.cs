using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 违章曝光台
    /// </summary>
    public static class ExposureService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取违章曝光台
        /// </summary>
        /// <param name="exposureId"></param>
        /// <returns></returns>
        public static Model.InformationProject_Exposure GetExposureById(string exposureId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            return db.InformationProject_Exposure.FirstOrDefault(e => e.ExposureId == exposureId);
        }

        /// <summary>
        /// 添加违章曝光台
        /// </summary>
        /// <param name="exposure"></param>
        public static void AddExposure(Model.InformationProject_Exposure exposure)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_Exposure newExposure = new Model.InformationProject_Exposure
            {
                ExposureId = exposure.ExposureId,
                ProjectId = exposure.ProjectId,
                ExposureCode = exposure.ExposureCode,
                ExposureDate = exposure.ExposureDate,
                UnitId = exposure.UnitId,
                UnitName = exposure.UnitName,
                FileName = exposure.FileName,
                Remark = exposure.Remark,
                CompileMan = exposure.CompileMan,
                CompileDate = exposure.CompileDate
            };
            db.InformationProject_Exposure.InsertOnSubmit(newExposure);
            db.SubmitChanges();
            if (newExposure.UnitId.Length > 50)
            {               
                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectExposureMenuId, exposure.ProjectId, null, exposure.ExposureId, exposure.CompileDate);
            }
            else
            {
                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectExposureMenuId, exposure.ProjectId, exposure.UnitId, exposure.ExposureId, exposure.CompileDate);
            }
        }

        /// <summary>
        /// 修改违章曝光台
        /// </summary>
        /// <param name="exposure"></param>
        public static void UpdateExposure(Model.InformationProject_Exposure exposure)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_Exposure newExposure = db.InformationProject_Exposure.FirstOrDefault(e => e.ExposureId == exposure.ExposureId);
            if (newExposure != null)
            {
                newExposure.ProjectId = exposure.ProjectId;
                newExposure.ExposureCode = exposure.ExposureCode;
                newExposure.ExposureDate = exposure.ExposureDate;
                newExposure.UnitId = exposure.UnitId;
                newExposure.UnitName = exposure.UnitName;
                newExposure.FileName = exposure.FileName;
                newExposure.Remark = exposure.Remark;
                newExposure.CompileMan = exposure.CompileMan;
                newExposure.CompileDate = exposure.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除违章曝光台
        /// </summary>
        /// <param name="exposureId"></param>
        public static void DeleteExposureById(string exposureId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_Exposure exposure = db.InformationProject_Exposure.FirstOrDefault(e => e.ExposureId == exposureId);
            if (exposure != null)
            {
                CommonService.DeleteAttachFileById(exposureId);//删除附件
                CodeRecordsService.DeleteCodeRecordsByDataId(exposureId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(exposureId);//删除流程
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(exposureId);
                db.InformationProject_Exposure.DeleteOnSubmit(exposure);
                db.SubmitChanges();
            }
        }
    }
}
