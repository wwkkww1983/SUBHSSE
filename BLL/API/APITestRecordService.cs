using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITestRecordService
    {
        #region 根据TestPlanId获取考试试卷人员列表
        /// <summary>
        /// 根据TestPlanId获取考试人员列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public static List<Model.TestRecordItem> getTestRecordListByTestPlanId(string testPlanId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestRecord
                                where x.TestPlanId == testPlanId
                                orderby x.TestStartTime descending
                                select new Model.TestRecordItem
                                {
                                    TestRecordId = x.TestRecordId,
                                    ProjectId = x.ProjectId,
                                    TestPlanId = x.TestPlanId,
                                    TestManId = x.TestManId,
                                    TestManName = Funs.DB.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.TestManId).PersonName,
                                    TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                    TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                    TestScores = x.TestScores ?? 0,
                                    TestType = x.TestType,
                                    TemporaryUser = x.TemporaryUser,
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据试卷ID获取试卷记录详细
        /// <summary>
        /// 根据试卷ID获取试卷记录详细
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public static Model.TestRecordItem getTestRecordByTestRecordId(string testRecordId)
        {
            var getDataLists = from x in Funs.DB.Training_TestRecord
                               join y in Funs.DB.Training_TestPlan on x.TestPlanId equals y.TestPlanId
                               where x.TestRecordId == testRecordId
                               select new Model.TestRecordItem
                               {
                                   TestRecordId = x.TestRecordId,
                                   ProjectId = x.ProjectId,
                                   TestPlanId = x.TestPlanId,
                                   TestPlanName = y.PlanName,
                                   TestManId = x.TestManId,
                                   TestManName = Funs.DB.SitePerson_Person.First(u => u.PersonId == x.TestManId).PersonName,
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                   TestPlanEndTime = x.TestStartTime.HasValue ? string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime.Value.AddMinutes(x.Duration)) : "",
                                   Duration = y.Duration,
                                   TestScores = x.TestScores ?? 0,
                                   TestType = x.TestType,
                                   TemporaryUser = x.TemporaryUser,
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 根据PersonId、TestPlanId生成试卷 扫码生成试卷
        /// <summary>
        /// 根据PersonId、TestPlanId生成试卷 扫码生成试卷
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        public static string getTestRecordItemByTestPlanIdPersonId(Model.Training_TestPlan getTestPlan, Model.SitePerson_Person person)
        {
            string testRecordId = string.Empty;
            ////当前人考试记录  未加入考试计划的 当考试开始扫码时 不允许再参与考试
            var testRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestManId == person.PersonId);
            if (testRecord != null)
            {
                testRecordId = testRecord.TestRecordId;
                if (!testRecord.TestStartTime.HasValue)
                {
                    ////考试时长
                    testRecord.Duration = getTestPlan.Duration;
                    Funs.DB.SubmitChanges();

                    List<Model.Training_TestTrainingItem> getTestTrainingItemList = new List<Model.Training_TestTrainingItem>();
                    var testPlanTrainings = from x in Funs.DB.Training_TestPlanTraining
                                            where x.TestPlanId == getTestPlan.TestPlanId
                                            select x;
                    foreach (var itemT in testPlanTrainings)
                    {
                        ////获取类型下适合岗位试题集合
                        var getTestTrainingItems = Funs.DB.Training_TestTrainingItem.Where(x => x.TrainingId == itemT.TrainingId && (x.WorkPostIds == null || (x.WorkPostIds.Contains(person.WorkAreaId) && person.WorkAreaId != null)));
                        if (getTestTrainingItems.Count() > 0)
                        {
                            getTestTrainingItems = getTestTrainingItems.OrderBy(q => Guid.NewGuid());
                            ////单选题
                            var getSItem = getTestTrainingItems.Where(x => x.TestType == "1").Take(itemT.TestType1Count ?? 1);
                            if (getSItem.Count() > 0)
                            {
                                getTestTrainingItemList.AddRange(getSItem);
                            }
                            ///多选题
                            var getMItem = getTestTrainingItems.Where(x => x.TestType == "2").Take(itemT.TestType1Count ?? 1);
                            if (getMItem.Count() > 0)
                            {
                                getTestTrainingItemList.AddRange(getMItem);
                            }
                            ///判断题
                            var getJItem = getTestTrainingItems.Where(x => x.TestType == "3").Take(itemT.TestType1Count ?? 1);
                            if (getJItem.Count() > 0)
                            {
                                getTestTrainingItemList.AddRange(getJItem);
                            }
                        }
                    }

                    if (getTestTrainingItemList.Count() > 0)
                    {                       
                        var getItems = from x in getTestTrainingItemList
                                       select new Model.Training_TestRecordItem
                                       {
                                           TestRecordItemId = SQLHelper.GetNewID(),
                                           TestRecordId = testRecordId,
                                           TrainingItemName = x.TrainingItemName,
                                           TrainingItemCode = x.TrainingItemCode,
                                           Abstracts = x.Abstracts,
                                           AttachUrl = x.AttachUrl,
                                           TestType = x.TestType,
                                           AItem = x.AItem,
                                           BItem = x.BItem,
                                           CItem = x.CItem,
                                           DItem = x.DItem,
                                           EItem = x.EItem,
                                           AnswerItems = x.AnswerItems,
                                           Score = x.TestType == "1" ? getTestPlan.SValue : (x.TestType == "2" ? getTestPlan.MValue : getTestPlan.JValue),
                                       };

                        Funs.DB.Training_TestRecordItem.InsertAllOnSubmit(getItems);
                        Funs.DB.SubmitChanges();
                    }
                }
            }

            return testRecordId;
        }
        #endregion

        #region 根据ProjectId、PersonId获取试卷列表
        /// <summary>
        /// 根据ProjectId、PersonId获取试卷列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="personId">人员ID(null查全部)</param>
        /// <returns>考试记录列表</returns>
        public static List<Model.TestRecordItem> getTrainingTestRecordListByProjectIdPersonId(string projectId, string personId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestRecord
                               join y in Funs.DB.Training_TestPlan on x.TestPlanId equals y.TestPlanId
                               where x.ProjectId == projectId && x.TestManId == personId && x.TestStartTime.HasValue
                               orderby x.TestStartTime descending
                               select new Model.TestRecordItem
                               {
                                   TestRecordId = x.TestRecordId,
                                   ProjectId = x.ProjectId,
                                   TestPlanId = x.TestPlanId,
                                   TestPlanName = y.PlanName,
                                   TestManId = x.TestManId,
                                   TestManName = Funs.DB.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.TestManId).PersonName,
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                   Duration = x.Duration,
                                   TestPlanEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime.Value.AddMinutes(x.Duration)),
                                   TotalScore = y.TotalScore ?? 0,
                                   TestScores = x.TestScores ?? 0,
                                   TestType = x.TestType,
                                   TemporaryUser = x.TemporaryUser,
                               }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据ProjectId、PersonId获取试卷列表
        /// <summary>
        /// 根据ProjectId、PersonId获取试卷列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>考试记录列表</returns>
        public static List<Model.TestRecordItem> getTrainingTestRecordListByProjectId(string projectId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestRecord
                                join y in Funs.DB.Training_TestPlan on x.TestPlanId equals y.TestPlanId
                               where  x.ProjectId == projectId && x.TestStartTime.HasValue && x.TestEndTime.HasValue 
                               orderby x.TestStartTime descending
                               select new Model.TestRecordItem
                               {
                                   TestRecordId = x.TestRecordId,
                                   ProjectId = x.ProjectId,
                                   TestPlanId = x.TestPlanId,
                                   TestPlanName = y.PlanName,
                                   TestManId = x.TestManId,
                                   TestManName = Funs.DB.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.TestManId).PersonName,
                                   TestStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime),
                                   TestEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestEndTime),
                                   Duration = x.Duration,
                                   TestPlanEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.TestStartTime.Value.AddMinutes(x.Duration)),
                                   TotalScore = y.TotalScore ?? 0,
                                   TestScores = x.TestScores ?? 0,
                                   TestType = x.TestType,
                                   TemporaryUser = x.TemporaryUser,
                               }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据TestRecordId获取试卷题目列表
        /// <summary>
        /// 根据TestRecordId获取试卷题目列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public static List<Model.TestRecordItemItem> geTestRecordItemListByTestRecordId(string testRecordId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestRecordItem
                                where x.TestRecordId == testRecordId
                                select new Model.TestRecordItemItem
                                {
                                    TestRecordItemId = x.TestRecordItemId,
                                    TestRecordId = x.TestRecordId,
                                    TrainingItemCode = x.TrainingItemCode,
                                    TrainingItemName = x.TrainingItemName,
                                    Abstracts = x.Abstracts,
                                    AttachUrl = x.AttachUrl.Replace("\\", "/") ?? "",
                                    TestType = x.TestType,
                                    TestTypeName = x.TestType == "1" ? "单选题" : (x.TestType == "2" ? "多选题" : "判断题"),
                                    AItem = x.AItem ?? "",
                                    BItem = x.BItem ?? "",
                                    CItem = x.CItem ?? "",
                                    DItem = x.DItem ?? "",
                                    EItem = x.EItem ?? "",
                                    AnswerItems = x.AnswerItems ?? "",
                                    Score = x.Score ?? 0,
                                    SubjectScore = x.SubjectScore ?? 0,
                                    SelectedItem = x.SelectedItem ?? "",
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据TestRecordItemId获取试卷题目详细
        /// <summary>
        /// 根据TestRecordItemId获取试卷题目详细
        /// </summary>
        /// <param name="testRecordItemId"></param>
        /// <returns>考试人员</returns>
        public static Model.TestRecordItemItem geTestRecordItemByTestRecordItemId(string testRecordItemId)
        {
            var getDataLists = from x in Funs.DB.Training_TestRecordItem
                               where x.TestRecordItemId == testRecordItemId
                               select new Model.TestRecordItemItem
                               {
                                   TestRecordItemId = x.TestRecordItemId,
                                   TestRecordId = x.TestRecordId,
                                   TrainingItemCode = x.TrainingItemCode,
                                   TrainingItemName = x.TrainingItemName,
                                   Abstracts = x.Abstracts,
                                   AttachUrl = x.AttachUrl.Replace("\\", "/") ?? "",
                                   TestType = x.TestType,
                                   TestTypeName = x.TestType == "1" ? "单选题" : (x.TestType == "2" ? "多选题" : "判断题"),
                                   AItem = x.AItem ?? "",
                                   BItem = x.BItem ?? "",
                                   CItem = x.CItem ?? "",
                                   DItem = x.DItem ?? "",
                                   EItem = x.EItem ?? "",
                                   AnswerItems = x.AnswerItems ?? "",
                                   Score = x.Score ?? 0,
                                   SubjectScore = x.SubjectScore ?? 0,
                                   SelectedItem = x.SelectedItem ?? "",

                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 根据TestRecordItemId、AnswerItems 考生答题
        /// <summary>
        /// 根据TestRecordItemId、AnswerItems 考生答题
        /// </summary>
        /// <param name="testRecordItemId"></param>
        /// <param name="answerItems"></param>
        public static void getTestRecordItemAnswerBySelectedItem(Model.Training_TestRecordItem getTItem, string selectedItem)
        {
            getTItem.SubjectScore = 0;
            getTItem.SelectedItem = selectedItem;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                if (getTItem.AnswerItems == selectedItem)
                {
                    getTItem.SubjectScore = getTItem.Score ?? 0;
                }
                else
                {
                    var listA = Funs.GetStrListByStr(getTItem.AnswerItems.ToUpper(), ',');
                    var listS = Funs.GetStrListByStr(selectedItem.ToUpper(), ',');
                    if (getTItem.TestType == "2" && listA.Count >= listS.Count)
                    {
                        int i = 0;
                        foreach (var item in listS)
                        {
                            if (!listA.Contains(item))
                            {
                                i++;
                                break;
                            }
                        }
                        if (i == 0)
                        {
                            if (listA.Count == listS.Count)
                            {
                                getTItem.SubjectScore = getTItem.Score ?? 0;
                            }
                            else
                            {
                                getTItem.SubjectScore = Convert.ToDecimal((getTItem.Score ?? 0) * 1.0 / 4);
                            }
                        }
                    }
                }
            }
            Funs.DB.SubmitChanges();
        }
        #endregion

        #region 根据TestRecordId 提交试卷
        /// <summary>
        /// 根据TestRecordId 提交试卷
        /// </summary>
        /// <param name="testRecordId"></param>
        public static void getSubmitTestRecordByTestRecordId(string testRecordId)
        {
            //试卷
            var getTestRecord = TestRecordService.GetTestRecordById(testRecordId);
            if (getTestRecord != null && getTestRecord.TestStartTime.HasValue)
            {
                getTestRecord.TestEndTime = DateTime.Now;
                getTestRecord.TestScores= Funs.DB.Training_TestRecordItem.Where(x => x.TestRecordId == testRecordId).Sum(x => x.SubjectScore) ?? 0;
                getTestRecord.TestEndTime = getTestRecord.TestStartTime.Value.AddMinutes(getTestRecord.Duration);
                Funs.DB.SubmitChanges();
                //考试计划
                var getTestPlan = TestPlanService.GetTestPlanById(getTestRecord.TestPlanId);
                if (getTestPlan != null)
                {
                    ////所有人员 都交卷时 考试计划结束 状态置为3
                    var getAllTestRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestEndTime.HasValue && x.TestRecordId != testRecordId);
                    if (getAllTestRecord == null)
                    {
                        getTestPlan.States = "3";
                        Funs.DB.SubmitChanges();
                        APITestPlanService.SubmitTest(getTestPlan);
                    }
                }
            }
        }
        #endregion
    }
}
