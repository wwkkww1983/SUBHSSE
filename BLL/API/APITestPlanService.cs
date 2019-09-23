using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    /// <summary>
    /// 考试计划
    /// </summary>
    public static class APITestPlanService
    {
        #region 根据projectId、states获取考试计划列表
        /// <summary>
        /// 根据projectId、states获取考试计划列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="states">状态（0-待提交；1-已发布；2-考试中；3考试结束）</param>
        /// <returns></returns>
        public static List<Model.TestPlanItem> getTestPlanListByProjectIdStates(string projectId, string states)
        {
            var getDataLists = (from x in Funs.DB.Training_TestPlan
                                where x.ProjectId == projectId && (x.States == states || states == null)
                                orderby x.TestStartTime descending
                                select new Model.TestPlanItem
                                {
                                    TestPlanId = x.TestPlanId,
                                    TestPlanCode = x.PlanCode,
                                    TestPlanName = x.PlanName,
                                    ProjectId = x.ProjectId,
                                    TestPlanManId = x.PlanManId,
                                    TestPlanManName = Funs.DB.Sys_User.First(y => y.UserId == x.PlanManId).UserName,
                                    TestPalce = x.TestPalce,
                                    TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                    TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),                                    
                                    States = x.States,
                                    QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据考试ID获取考试计划详细
        /// <summary>
        /// 根据考试ID获取考试计划详细
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static Model.TestPlanItem getTestPlanByTestPlanId(string testPlanId)
        {
            var getDataLists = from x in Funs.DB.Training_TestPlan
                                   //join y in Funs.DB.Training_Plan on x.PlanId equals y.PlanId
                               where x.TestPlanId == testPlanId
                               select new Model.TestPlanItem
                               {
                                   TestPlanId = x.TestPlanId,
                                   ProjectId = x.ProjectId,
                                   TestPlanCode = x.PlanCode,
                                   TestPlanName = x.PlanName,
                                   TestPlanManId = x.PlanManId,
                                   TestPlanManName = Funs.DB.Sys_User.First(y => y.UserId == x.TestPlanId).UserName,
                                   TestPlanDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.PlanDate),
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                   Duration = x.Duration ?? 0,
                                   TotalScore = x.TotalScore ?? 0,
                                   QuestionCount = x.QuestionCount ?? 0,
                                   TestPalce = x.TestPalce,
                                   UnitIds = x.UnitIds,
                                   UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                   WorkPostIds = x.WorkPostIds,
                                   WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                                   States = x.States,
                                   QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                   TrainingPlanId = x.PlanId,
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 保存考试计划
        /// <summary>
        /// 保存TestPlan
        /// </summary>
        /// <param name="getTestPlan">考试计划记录</param>
        public static void SaveTestPlan(Model.TestPlanItem getTestPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
            {
                TestPlanId = getTestPlan.TestPlanId,
                ProjectId = getTestPlan.ProjectId,
                PlanCode = getTestPlan.TestPlanCode,
                PlanName = getTestPlan.TestPlanName,
                PlanManId = getTestPlan.TestPlanManId,
                //PlanDate= getTestPlan.TestPlanDate,
                TestStartTime = Funs.GetNewDateTime(getTestPlan.TestStartTime),
                TestEndTime = Funs.GetNewDateTime(getTestPlan.TestEndTime),
                Duration = getTestPlan.Duration,
                TotalScore = getTestPlan.TotalScore,
                QuestionCount = getTestPlan.QuestionCount,
                TestPalce = getTestPlan.TestPalce,
                UnitIds = getTestPlan.UnitIds,
                WorkPostIds = getTestPlan.WorkPostIds,
                States = getTestPlan.States,
                PlanId = getTestPlan.TrainingPlanId,
                PlanDate = DateTime.Now,
            };

            var isUpdate = Funs.DB.Training_TestPlan.FirstOrDefault(x => x.PlanId == newTestPlan.PlanId);
            if (isUpdate == null)
            {
                string unitId = string.Empty;
                var user = UserService.GetUserByUserId(newTestPlan.PlanManId);
                if (user != null)
                {
                    unitId = user.UnitId;
                }
                newTestPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, unitId);
                if (string.IsNullOrEmpty(newTestPlan.PlanId))
                {
                    newTestPlan.PlanId = SQLHelper.GetNewID();
                }
                db.Training_TestPlan.InsertOnSubmit(newTestPlan);
                db.SubmitChanges();
            }
            else
            {
                if (newTestPlan.States == "0" || newTestPlan.States == "1")
                {
                    isUpdate.PlanName = newTestPlan.PlanName;
                    isUpdate.PlanManId = newTestPlan.PlanManId;
                    isUpdate.PlanDate = newTestPlan.PlanDate;
                    isUpdate.TestStartTime = newTestPlan.TestStartTime;
                    isUpdate.TestEndTime = newTestPlan.TestEndTime;
                    isUpdate.Duration = newTestPlan.Duration;
                    isUpdate.TotalScore = newTestPlan.TotalScore;
                    isUpdate.QuestionCount = newTestPlan.QuestionCount;
                    isUpdate.TestPalce = newTestPlan.TestPalce;
                    isUpdate.UnitIds = newTestPlan.UnitIds;
                    isUpdate.WorkPostIds = newTestPlan.WorkPostIds;
                    isUpdate.States = newTestPlan.States;
                    db.SubmitChanges();

                    ////删除 考生记录
                    TestRecordService.DeleteTestRecordByTestPlanId(newTestPlan.PlanId);
                    ////删除 考试题目类型
                    TestPlanTrainingService.DeleteTestPlanTrainingByTestPlanId(newTestPlan.PlanId);                    
                }
            }

            if (getTestPlan.TestRecordItems.Count() > 0)
            {
                ////新增考试人员明细
                AddTrainingTestRecord(getTestPlan.TestRecordItems, newTestPlan);
            }
            if (getTestPlan.TestPlanTrainingItems.Count() > 0)
            {
                ////新增考试教材类型明细
                AddTrainingTestPlanTraining(getTestPlan.TestPlanTrainingItems, newTestPlan.PlanId);
            }
        }

        /// <summary>
        ///  新增 考试人员明细
        /// </summary>
        public static void AddTrainingTestRecord(List<Model.TestRecordItem> testRecords, Model.Training_TestPlan newTestPlan)
        {
            foreach (var item in testRecords)
            {
                Model.Training_TestRecord newTrainDetail = new Model.Training_TestRecord
                {
                    TestRecordId = SQLHelper.GetNewID(),
                    ProjectId = newTestPlan.ProjectId,
                    TestPlanId = newTestPlan.TestPlanId,
                    TestManId = item.TestManId,
                    TestType = item.TestType,
                };

                Funs.DB.Training_TestRecord.InsertOnSubmit(newTrainDetail);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        ///  新增 考试教材类型 明细
        /// </summary>
        public static void AddTrainingTestPlanTraining(List<Model.TestPlanTrainingItem> TestPlanItems, string testPlanId)
        {
            foreach (var item in TestPlanItems)
            {
                Model.Training_TestPlanTraining newPlanItem = new Model.Training_TestPlanTraining
                {
                    TestPlanTrainingId = SQLHelper.GetNewID(),
                    TestPlanId = testPlanId,
                    TrainingId = item.TrainingTypeId,
                    TestType1Count = item.TestType1Count,
                    TestType2Count = item.TestType2Count,
                    TestType3Count = item.TestType3Count,
                };

                Funs.DB.Training_TestPlanTraining.InsertOnSubmit(newPlanItem);
                Funs.DB.SubmitChanges();
            }
        }
        #endregion

        #region 根据培训计划ID生成 考试计划信息
        /// <summary>
        /// 根据培训计划ID生成 考试计划信息
        /// </summary>
        /// <param name="getTestPlan"></param>
        public static string SaveTestPlanByTrainingPlanId(string trainingPlanId,string userId)
        {
            string testPlanId = string.Empty;
            ////培训计划
            var getTrainingPlan = TrainingPlanService.GetPlanById(trainingPlanId);
            if (getTrainingPlan != null && getTrainingPlan.States=="1")
            {
                testPlanId = SQLHelper.GetNewID();
                Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
                {
                    TestPlanId = testPlanId,
                    ProjectId = getTrainingPlan.ProjectId,
                    //PlanCode = getTrainingPlan.PlanCode,
                    PlanName = getTrainingPlan.PlanName,
                    PlanManId = userId,
                    PlanDate = DateTime.Now,
                    TestStartTime = DateTime.Now,                
                    TestPalce = getTrainingPlan.TeachAddress,
                    UnitIds = getTrainingPlan.UnitIds,
                    UnitNames = UnitService.getUnitNamesUnitIds(getTrainingPlan.UnitIds),
                    WorkPostIds = getTrainingPlan.WorkPostId,
                    WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(getTrainingPlan.WorkPostId),
                    PlanId = getTrainingPlan.PlanId,
                    States = "0",
                };

                string unitId = string.Empty;
                var user = UserService.GetUserByUserId(userId);
                if (user != null)
                {
                    unitId = user.UnitId;
                }
                newTestPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTestPlanMenuId, newTestPlan.ProjectId, unitId);

                int testType1Count = 0;
                int testType2Count = 0;
                int testType3Count = 0;
                var getTestRule = Funs.DB.Sys_TestRule.FirstOrDefault(); ////考试数据设置
                if (getTestRule != null)
                {
                    testType1Count = getTestRule.SCount;
                    testType2Count = getTestRule.MCount;
                    testType3Count = getTestRule.JCount;
                    newTestPlan.Duration = getTestRule.Duration;
                    newTestPlan.TotalScore = testType1Count * getTestRule.SValue + testType2Count * getTestRule.MValue + testType3Count * getTestRule.JValue;
                    newTestPlan.QuestionCount = testType1Count + testType2Count + testType3Count;
                }
                ////新增考试计划记录
                TestPlanService.AddTestPlan(newTestPlan);

                ///培训人员
                var getTrainingTask = TrainingTaskService.GetTaskListByPlanId(trainingPlanId);
                foreach (var itemTask in getTrainingTask)
                {
                    Model.Training_TestRecord newTestRecord = new Model.Training_TestRecord
                    {
                        TestRecordId = SQLHelper.GetNewID(),
                        ProjectId = getTrainingPlan.ProjectId,
                        TestPlanId = testPlanId,
                        TestManId = itemTask.UserId,
                    };
                    var getTrainType = TrainTypeService.GetTrainTypeById(getTrainingPlan.TrainTypeId);
                    if (getTrainType != null)
                    {
                        newTestRecord.TestType = getTrainType.TrainTypeName;
                    }
                    TestRecordService.AddTestRecord(newTestRecord);
                }
                /////考试题型类别及数量
                Model.Training_TestPlanTraining newTestPlanTraining = new Model.Training_TestPlanTraining
                {
                    TestPlanTrainingId = SQLHelper.GetNewID(),
                    TestPlanId = testPlanId,
                    TestType1Count = testType1Count,
                    TestType2Count = testType2Count,
                    TestType3Count = testType3Count,
                };
                TestPlanTrainingService.AddTestPlanTraining(newTestPlanTraining);
                ////回写培训计划状态
                getTrainingPlan.States = "2";
                TrainingPlanService.UpdatePlan(getTrainingPlan);
            }

            return testPlanId;
        }
        #endregion

        #region 根据TestPlanId获取考试试题类型列表
        /// <summary>
        /// 根据TestPlanId获取考试试题类型列表
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns></returns>
        public static List<Model.TestPlanTrainingItem> getTestPlanTrainingListByTestPlanId(string testPlanId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestPlanTraining
                                join y in Funs.DB.Training_TestTraining on x.TrainingId equals y.TrainingId
                                where x.TestPlanId == testPlanId
                                orderby y.TrainingCode
                                select new Model.TestPlanTrainingItem
                                {
                                    TestPlanTrainingId = x.TestPlanTrainingId,
                                    TestPlanId = x.TestPlanId,
                                    TrainingTypeId = x.TrainingId,
                                    TrainingTypeName = y.TrainingName,
                                    TestType1Count = x.TestType1Count ?? 0,
                                    TestType2Count = x.TestType2Count ?? 0,
                                    TestType3Count = x.TestType3Count ?? 0,
                                }).ToList();
            return getDataLists;
        }
        #endregion
    }
}
