using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainRecordService
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.TrainRecordItem> getTrainRecordListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string trainStates)
        {
            var getDataLists = (from x in Funs.DB.EduTrain_TrainRecord
                                where x.ProjectId == projectId  && x.TrainTypeId == trainTypeId
                                orderby x.TrainStartDate descending
                                select new Model.TrainRecordItem
                                {
                                    TrainRecordId = x.TrainingId,
                                    TrainingCode = x.TrainingCode,
                                    TrainTitle = x.TrainTitle,
                                    ProjectId = x.ProjectId,
                                    TrainTypeId = x.TrainTypeId,
                                    TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                    TrainLevelId = x.TrainLevelId,
                                    TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                    TeachHour = x.TeachHour ?? 0,
                                    TeachAddress = x.TeachAddress,
                                    TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据培训ID获取培训记录详细
        /// <summary>
        /// 根据培训ID获取培训记录详细
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public static Model.TrainRecordItem getTrainRecordByTrainingId(string trainRecordId)
        {
            var getDataLists = from x in Funs.DB.EduTrain_TrainRecord
                               where x.TrainingId == trainRecordId
                               select new Model.TrainRecordItem
                               {
                                   TrainRecordId = x.TrainingId,
                                   TrainingCode = x.TrainingCode,
                                   TrainTitle = x.TrainTitle,
                                   ProjectId = x.ProjectId,
                                   TrainTypeId = x.TrainTypeId,
                                   TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                   TrainLevelId = x.TrainLevelId,
                                   TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                   TeachHour = x.TeachHour ?? 0,
                                   TeachAddress = x.TeachAddress,
                                   TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                   UnitIds = x.UnitIds,
                                   WorkPostIds = x.WorkPostIds,
                                   TrainContent = x.TrainContent,
                                   AttachUrl = Funs.DB.AttachFile.First(y => y.ToKeyId == x.TrainingId).AttachUrl.Replace('\\', '/'),
                                   UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                   WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 根据考生计划结束时 将相关培训考生内容 写培训记录归档
        /// <summary>
        /// 根据考生计划结束时 将相关培训考生内容 写培训记录归档
        /// </summary>
        /// <param name="getTestPlan"></param>

        public static void InsertTrainRecord(Model.Training_TestPlan getTestPlan)
        {
            ////获取培训计划
            var getTrainingPlan = TrainingPlanService.GetPlanById(getTestPlan.PlanId);
            var getTrainRecord = Funs.DB.EduTrain_TrainRecord.FirstOrDefault(x => x.PlanId == getTestPlan.PlanId);
            if (getTrainingPlan != null && getTrainRecord == null)
            {
                Model.EduTrain_TrainRecord newTrainRecord = new Model.EduTrain_TrainRecord
                {
                    TrainingId = SQLHelper.GetNewID(),
                    TrainingCode = getTrainingPlan.PlanCode,
                    ProjectId = getTrainingPlan.ProjectId,
                    TrainTitle = getTrainingPlan.PlanName,
                    TrainContent = getTrainingPlan.TrainContent,
                    TrainStartDate = getTrainingPlan.TrainStartDate,
                    TeachHour = getTrainingPlan.TeachHour,
                    TeachMan = getTrainingPlan.TeachMan,
                    TeachAddress = getTrainingPlan.TeachAddress,
                    Remark = "来源：培训计划",
                    TrainTypeId = getTrainingPlan.TrainTypeId,
                    TrainLevelId = getTrainingPlan.TrainLevelId,
                    UnitIds = getTrainingPlan.UnitIds,
                    States = Const.State_2,
                    WorkPostIds = getTrainingPlan.WorkPostId,
                    PlanId = getTrainingPlan.PlanId,
                };
                newTrainRecord.CompileMan = UserService.GetUserNameByUserId(getTrainingPlan.DesignerId);
                ///获取培训人员
                var getTrainingTasks = from x in Funs.DB.Training_Task
                                       where x.PlanId == getTrainingPlan.PlanId
                                       select x;

                newTrainRecord.TrainPersonNum = getTrainingTasks.Count();
                ///新增培训记录
                EduTrain_TrainRecordService.AddTraining(newTrainRecord);
                foreach (var item in getTrainingTasks)
                {
                    Model.EduTrain_TrainRecordDetail newDetail = new Model.EduTrain_TrainRecordDetail
                    {
                        TrainingId = newTrainRecord.TrainingId,
                        PersonId = item.UserId,
                       
                    };
                    ////及格分数
                    int getPassScores = SysConstSetService.getPassScore();
                    ////获取 考生试卷
                    var getTestRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestManId == item.UserId);
                    if (getTestRecord != null)
                    {
                        newDetail.CheckScore = getTestRecord.TestScores;
                        if (newDetail.CheckScore >= getPassScores)
                        {
                            newDetail.CheckResult = true;
                        }
                        else
                        {
                            newDetail.CheckResult = false;
                        }
                    }
                    ////新增培训记录明细
                    EduTrain_TrainRecordDetailService.AddTrainDetail(newDetail);
                }

                CommonService.btnSaveData(newTrainRecord.ProjectId, Const.ProjectTrainRecordMenuId, newTrainRecord.TrainingId, getTrainingPlan.DesignerId, true, newTrainRecord.TrainTitle, "../EduTrain/TrainRecordView.aspx?TrainingId={0}");
            }
        }
        #endregion
    }
}
