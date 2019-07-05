using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 事故处理表
    /// </summary>
    public static class AccidentStatisticsService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取事故处理
        /// </summary>
        /// <param name="accidentStatisticsId"></param>
        /// <returns></returns>
        public static Model.ProjectAccident_AccidentStatistics GetAccidentStatisticsById(string accidentStatisticsId)
        {
            return Funs.DB.ProjectAccident_AccidentStatistics.FirstOrDefault(e => e.AccidentStatisticsId == accidentStatisticsId);
        }

        /// <summary>
        /// 添加事故处理
        /// </summary>
        /// <param name="accidentStatistics"></param>
        public static void AddAccidentStatistics(Model.ProjectAccident_AccidentStatistics accidentStatistics)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentStatistics newAccidentStatistics = new Model.ProjectAccident_AccidentStatistics
            {
                AccidentStatisticsId = accidentStatistics.AccidentStatisticsId,
                ProjectId = accidentStatistics.ProjectId,
                UnitId = accidentStatistics.UnitId,
                Person = accidentStatistics.Person,
                Treatment = accidentStatistics.Treatment,
                CompileDate = accidentStatistics.CompileDate,
                AttachUrl = accidentStatistics.AttachUrl,
                States = accidentStatistics.States
            };
            db.ProjectAccident_AccidentStatistics.InsertOnSubmit(newAccidentStatistics);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故处理
        /// </summary>
        /// <param name="accidentStatistics"></param>
        public static void UpdateAccidentStatistics(Model.ProjectAccident_AccidentStatistics accidentStatistics)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentStatistics newAccidentStatistics = db.ProjectAccident_AccidentStatistics.FirstOrDefault(e => e.AccidentStatisticsId == accidentStatistics.AccidentStatisticsId);
            if (newAccidentStatistics != null)
            {
                newAccidentStatistics.ProjectId = accidentStatistics.ProjectId;
                newAccidentStatistics.UnitId = accidentStatistics.UnitId;
                newAccidentStatistics.Person = accidentStatistics.Person;
                newAccidentStatistics.Treatment = accidentStatistics.Treatment;
                newAccidentStatistics.CompileDate = accidentStatistics.CompileDate;
                newAccidentStatistics.AttachUrl = accidentStatistics.AttachUrl;
                newAccidentStatistics.States = accidentStatistics.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故处理
        /// </summary>
        /// <param name="accidentStatisticsId"></param>
        public static void DeleteAccidentStatisticsById(string accidentStatisticsId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentStatistics accidentStatistics = db.ProjectAccident_AccidentStatistics.FirstOrDefault(e => e.AccidentStatisticsId == accidentStatisticsId);
            if (accidentStatistics != null)
            {
                if (!string.IsNullOrEmpty(accidentStatistics.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, accidentStatistics.AttachUrl);//删除附件
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(accidentStatistics.AccidentStatisticsId);
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(accidentStatistics.AccidentStatisticsId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(accidentStatistics.AccidentStatisticsId);
                db.ProjectAccident_AccidentStatistics.DeleteOnSubmit(accidentStatistics);
                db.SubmitChanges();
            }
        }
    }
}