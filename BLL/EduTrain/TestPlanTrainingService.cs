using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试记录
    /// </summary>
    public static class TestPlanTrainingService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 新增考生记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void AddTestPlanTraining(Model.Training_TestPlanTraining training)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_TestPlanTraining newTestPlanTraining = new Model.Training_TestPlanTraining
            {
                TestPlanTrainingId = training.TestPlanTrainingId,
                TestPlanId = training.TestPlanId,
                TrainingId = training.TrainingId,
                TestType1Count = training.TestType1Count,
                TestType2Count = training.TestType2Count,
                TestType3Count = training.TestType3Count,
            };

            db.Training_TestPlanTraining.InsertOnSubmit(newTestPlanTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据计划主键删除考试人员信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestPlanTrainingByTestPlanId(string testPlanId)
        {
            var deleteTestPlanTrainings = from x in Funs.DB.Training_TestPlanTraining where x.TestPlanId == testPlanId select x;
            if (deleteTestPlanTrainings.Count() > 0)
            {
                Funs.DB.Training_TestPlanTraining.DeleteAllOnSubmit(deleteTestPlanTrainings);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
