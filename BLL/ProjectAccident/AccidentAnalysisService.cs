using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 事故统计表
    /// </summary>
    public static class AccidentAnalysisService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取事故统计
        /// </summary>
        /// <param name="accidentAnalysisId"></param>
        /// <returns></returns>
        public static Model.ProjectAccident_AccidentAnalysis GetAccidentAnalysisById(string accidentAnalysisId)
        {
            return Funs.DB.ProjectAccident_AccidentAnalysis.FirstOrDefault(e => e.AccidentAnalysisId == accidentAnalysisId);
        }

        /// <summary>
        /// 添加事故统计
        /// </summary>
        /// <param name="accidentAnalysis"></param>
        public static void AddAccidentAnalysis(Model.ProjectAccident_AccidentAnalysis accidentAnalysis)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentAnalysis newAccidentAnalysis = new Model.ProjectAccident_AccidentAnalysis
            {
                AccidentAnalysisId = accidentAnalysis.AccidentAnalysisId,
                ProjectId = accidentAnalysis.ProjectId,
                CompileMan = accidentAnalysis.CompileMan,
                Remarks = accidentAnalysis.Remarks,
                CompileDate = accidentAnalysis.CompileDate
            };


            db.ProjectAccident_AccidentAnalysis.InsertOnSubmit(newAccidentAnalysis);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故统计
        /// </summary>
        /// <param name="accidentAnalysis"></param>
        public static void UpdateAccidentAnalysis(Model.ProjectAccident_AccidentAnalysis accidentAnalysis)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentAnalysis newAccidentAnalysis = db.ProjectAccident_AccidentAnalysis.FirstOrDefault(e => e.AccidentAnalysisId == accidentAnalysis.AccidentAnalysisId);
            if (newAccidentAnalysis != null)
            {
                newAccidentAnalysis.ProjectId = accidentAnalysis.ProjectId;
                newAccidentAnalysis.CompileMan = accidentAnalysis.CompileMan;
                newAccidentAnalysis.Remarks = accidentAnalysis.Remarks;
                newAccidentAnalysis.CompileDate = accidentAnalysis.CompileDate;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故统计
        /// </summary>
        /// <param name="accidentAnalysisId"></param>
        public static void DeleteAccidentAnalysisById(string accidentAnalysisId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentAnalysis accidentAnalysis = db.ProjectAccident_AccidentAnalysis.FirstOrDefault(e => e.AccidentAnalysisId == accidentAnalysisId);
            if (accidentAnalysis != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(accidentAnalysisId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(accidentAnalysisId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(accidentAnalysisId);
                db.ProjectAccident_AccidentAnalysis.DeleteOnSubmit(accidentAnalysis);
                db.SubmitChanges();
            }
        }
    }
}