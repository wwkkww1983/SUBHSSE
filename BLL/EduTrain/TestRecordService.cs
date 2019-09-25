using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试记录
    /// </summary>
    public static class TestRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取考试记录
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public static Model.Training_TestRecord GetTestRecordById(string testRecordId)
        {
            return db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
        }
        
        /// <summary>
        /// 新增考生记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void AddTestRecord(Model.Training_TestRecord testRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_TestRecord newTestRecord = new Model.Training_TestRecord
            {
                TestRecordId = testRecord.TestRecordId,
                ProjectId = testRecord.ProjectId,
                TestPlanId = testRecord.TestPlanId,
                TestManId = testRecord.TestManId,
                TestType=testRecord.TestType,
            };

            if (string.IsNullOrEmpty(newTestRecord.TestType))
            {
                var getTrainTypeName = (from x in Funs.DB.Training_TestPlan
                                      join y in Funs.DB.Training_Plan on x.PlanId equals y.PlanId
                                      join z in Funs.DB.Base_TrainType on y.TrainTypeId equals z.TrainTypeId
                                      where x.TestPlanId == testRecord.TestPlanId
                                      select z).FirstOrDefault();
                    
                if (getTrainTypeName != null)
                {
                    testRecord.TestType = getTrainTypeName.TrainTypeName;
                }
            }

            db.Training_TestRecord.InsertOnSubmit(newTestRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改考试记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTestRecord(Model.Training_TestRecord testRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_TestRecord newTestRecord = db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecord.TestRecordId);
            if (newTestRecord != null)
            {
                newTestRecord.TestScores = testRecord.TestScores;
                newTestRecord.TestEndTime = testRecord.TestEndTime;
                newTestRecord.IsFiled = testRecord.IsFiled;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据计划主键删除考试人员信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordByTestPlanId(string testPlanId)
        {
            var deleteRecords = from x in db.Training_TestRecord where x.TestPlanId == testPlanId select x;
            if (deleteRecords.Count() > 0)
            {
                foreach (var item in deleteRecords)
                {
                    DeleteTestRecordByTestRecordId(item.TestRecordId);
                }
            }
        }

        /// <summary>
        /// 根据考生主键删除考生信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordByTestRecordId(string testRecordId)
        {
            var testRecord = Funs.DB.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
            if (testRecord != null)
            {
                var testRecordItem = from x in Funs.DB.Training_TestRecordItem where x.TestRecordId == testRecordId select x;
                if (testRecordItem.Count() > 0)
                {
                    foreach (var item in testRecordItem)
                    {
                        TestRecordItemService.DeleteTestRecordItemmByTestRecordItemId(item.TestRecordItemId);
                    }
                }

                Funs.DB.Training_TestRecord.DeleteOnSubmit(testRecord);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 更新没有结束时间且超时的考试记录
        /// </summary>
        public static int UpdateTestEndTimeNull(string testRecordId)
        {
            int icount = 0;
            var testRecord = from x in Funs.DB.Training_TestRecord
                             where !x.TestEndTime.HasValue && x.TestStartTime.HasValue
                             && x.TestStartTime.Value.AddMinutes(x.Duration) < DateTime.Now
                             && x.TestRecordId == testRecordId
                             select x;
            if (testRecord.Count() > 0)
            {
                foreach (var item in testRecord)
                {
                    item.TestEndTime = item.TestStartTime.Value.AddMinutes(item.Duration);
                    item.TestScores = Funs.DB.Training_TestRecordItem.Where(x => x.TestRecordId == item.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                    Funs.DB.SubmitChanges();
                    icount++;                   
                }
            }
            return icount;
        }
    }
}
