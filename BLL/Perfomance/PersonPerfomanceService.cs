using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 个人绩效评价
    /// </summary>
    public static class PersonPerfomanceService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取个人绩效评价
        /// </summary>
        /// <param name="personPerfomanceId"></param>
        /// <returns></returns>
        public static Model.Perfomance_PersonPerfomance GetPersonPerfomanceById(string personPerfomanceId)
        {
            return Funs.DB.Perfomance_PersonPerfomance.FirstOrDefault(e => e.PersonPerfomanceId == personPerfomanceId);
        }

        /// <summary>
        /// 添加个人绩效评价
        /// </summary>
        /// <param name="personPerfomance"></param>
        public static void AddPersonPerfomance(Model.Perfomance_PersonPerfomance personPerfomance)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Perfomance_PersonPerfomance newPersonPerfomance = new Model.Perfomance_PersonPerfomance
            {
                PersonPerfomanceId = personPerfomance.PersonPerfomanceId,
                ProjectId = personPerfomance.ProjectId,
                PersonPerfomanceCode = personPerfomance.PersonPerfomanceCode,
                UnitId = personPerfomance.UnitId,
                TeamGroupId = personPerfomance.TeamGroupId,
                PersonId = personPerfomance.PersonId,
                SubContractNum = personPerfomance.SubContractNum,
                EvaluationDate = personPerfomance.EvaluationDate,
                EvaluationDef = personPerfomance.EvaluationDef,
                RewardOrPunish = personPerfomance.RewardOrPunish,
                RPMoney = personPerfomance.RPMoney,
                AssessmentGroup = personPerfomance.AssessmentGroup,
                Behavior_1 = personPerfomance.Behavior_1,
                Behavior_2 = personPerfomance.Behavior_2,
                Behavior_3 = personPerfomance.Behavior_3,
                Behavior_4 = personPerfomance.Behavior_4,
                Behavior_5 = personPerfomance.Behavior_5,
                Behavior_6 = personPerfomance.Behavior_6,
                Behavior_7 = personPerfomance.Behavior_7,
                Behavior_8 = personPerfomance.Behavior_8,
                Behavior_9 = personPerfomance.Behavior_9,
                Behavior_10 = personPerfomance.Behavior_10,
                Score_1 = personPerfomance.Score_1,
                Score_2 = personPerfomance.Score_2,
                Score_3 = personPerfomance.Score_3,
                Score_4 = personPerfomance.Score_4,
                Score_5 = personPerfomance.Score_5,
                Score_6 = personPerfomance.Score_6,
                Score_7 = personPerfomance.Score_7,
                Score_8 = personPerfomance.Score_8,
                Score_9 = personPerfomance.Score_9,
                Score_10 = personPerfomance.Score_10,
                TotalJudging = personPerfomance.TotalJudging,
                TotalScore = personPerfomance.TotalScore,
                States = personPerfomance.States,
                CompileMan = personPerfomance.CompileMan,
                CompileDate = personPerfomance.CompileDate,
                AttachUrl = personPerfomance.AttachUrl
            };
            db.Perfomance_PersonPerfomance.InsertOnSubmit(newPersonPerfomance);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.PersonPerfomanceMenuId, personPerfomance.ProjectId, null, personPerfomance.PersonPerfomanceId, personPerfomance.CompileDate);
        }

        /// <summary>
        /// 修改个人绩效评价
        /// </summary>
        /// <param name="personPerfomance"></param>
        public static void UpdatePersonPerfomance(Model.Perfomance_PersonPerfomance personPerfomance)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Perfomance_PersonPerfomance newPersonPerfomance = db.Perfomance_PersonPerfomance.FirstOrDefault(e => e.PersonPerfomanceId == personPerfomance.PersonPerfomanceId);
            if (newPersonPerfomance != null)
            {
                //newPersonPerfomance.ProjectId = personPerfomance.ProjectId;
                newPersonPerfomance.PersonPerfomanceCode = personPerfomance.PersonPerfomanceCode;
                newPersonPerfomance.UnitId = personPerfomance.UnitId;
                newPersonPerfomance.TeamGroupId = personPerfomance.TeamGroupId;
                newPersonPerfomance.PersonId = personPerfomance.PersonId;
                newPersonPerfomance.SubContractNum = personPerfomance.SubContractNum;
                newPersonPerfomance.EvaluationDate = personPerfomance.EvaluationDate;
                newPersonPerfomance.EvaluationDef = personPerfomance.EvaluationDef;
                newPersonPerfomance.RewardOrPunish = personPerfomance.RewardOrPunish;
                newPersonPerfomance.RPMoney = personPerfomance.RPMoney;
                newPersonPerfomance.AssessmentGroup = personPerfomance.AssessmentGroup;
                newPersonPerfomance.Behavior_1 = personPerfomance.Behavior_1;
                newPersonPerfomance.Behavior_2 = personPerfomance.Behavior_2;
                newPersonPerfomance.Behavior_3 = personPerfomance.Behavior_3;
                newPersonPerfomance.Behavior_4 = personPerfomance.Behavior_4;
                newPersonPerfomance.Behavior_5 = personPerfomance.Behavior_5;
                newPersonPerfomance.Behavior_6 = personPerfomance.Behavior_6;
                newPersonPerfomance.Behavior_7 = personPerfomance.Behavior_7;
                newPersonPerfomance.Behavior_8 = personPerfomance.Behavior_8;
                newPersonPerfomance.Behavior_9 = personPerfomance.Behavior_9;
                newPersonPerfomance.Behavior_10 = personPerfomance.Behavior_10;
                newPersonPerfomance.Score_1 = personPerfomance.Score_1;
                newPersonPerfomance.Score_2 = personPerfomance.Score_2;
                newPersonPerfomance.Score_3 = personPerfomance.Score_3;
                newPersonPerfomance.Score_4 = personPerfomance.Score_4;
                newPersonPerfomance.Score_5 = personPerfomance.Score_5;
                newPersonPerfomance.Score_6 = personPerfomance.Score_6;
                newPersonPerfomance.Score_7 = personPerfomance.Score_7;
                newPersonPerfomance.Score_8 = personPerfomance.Score_8;
                newPersonPerfomance.Score_9 = personPerfomance.Score_9;
                newPersonPerfomance.Score_10 = personPerfomance.Score_10;
                newPersonPerfomance.TotalJudging = personPerfomance.TotalJudging;
                newPersonPerfomance.TotalScore = personPerfomance.TotalScore;
                newPersonPerfomance.States = personPerfomance.States;
                newPersonPerfomance.CompileMan = personPerfomance.CompileMan;
                newPersonPerfomance.CompileDate = personPerfomance.CompileDate;
                newPersonPerfomance.AttachUrl = personPerfomance.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除个人绩效评价
        /// </summary>
        /// <param name="personPerfomanceId"></param>
        public static void DeletePersonPerfomanceById(string personPerfomanceId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Perfomance_PersonPerfomance personPerfomance = db.Perfomance_PersonPerfomance.FirstOrDefault(e => e.PersonPerfomanceId == personPerfomanceId);
            if (personPerfomance != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(personPerfomanceId);//删除编号
                CommonService.DeleteFlowOperateByID(personPerfomanceId);//删除流程
                UploadFileService.DeleteFile(Funs.RootPath, personPerfomance.AttachUrl);//删除附件
                db.Perfomance_PersonPerfomance.DeleteOnSubmit(personPerfomance);
                db.SubmitChanges();
            }
        }
    }
}
