using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BuildTestService
    {
        /// <summary>
        /// 添加试卷条件
        /// </summary>
        /// <param name="?"></param>
        public static void AddTestCondition(Model.Edu_Online_TestCondition test)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Edu_Online_TestCondition));
            Model.Edu_Online_TestCondition newTest = new Model.Edu_Online_TestCondition
            {
                TestConditionId = newKeyID,
                WorkPostId = test.WorkPostId,
                ABVolume = test.ABVolume,
                TestType = test.TestType,
                Chapter = test.Chapter,
                ItemType = test.ItemType,
                SelectNumber = test.SelectNumber,
                TestScore = test.TestScore
            };

            db.Edu_Online_TestCondition.InsertOnSubmit(newTest);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试卷条件
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTestCondition(Model.Edu_Online_TestCondition test)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_TestCondition newTest = db.Edu_Online_TestCondition.First(e => e.TestConditionId == test.TestConditionId);
            newTest.WorkPostId = test.WorkPostId;
            newTest.ABVolume = test.ABVolume;
            newTest.TestType = test.TestType;
            newTest.Chapter = test.Chapter;
            newTest.ItemType = test.ItemType;
            newTest.SelectNumber = test.SelectNumber;
            newTest.TestScore = test.TestScore;

            db.SubmitChanges();
        }


        /// <summary>
        /// 删除生成试卷的条件
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteTestCondition(string workPostId, string aBVolume)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var test = db.Edu_Online_TestCondition.Where(e => e.WorkPostId == workPostId &&e.ABVolume==aBVolume);
            db.Edu_Online_TestCondition.DeleteAllOnSubmit(test);
            db.SubmitChanges();
        }

        /// <summary>
        /// 添加试卷条件
        /// </summary>
        /// <param name="?"></param>
        public static void AddTest(Model.Edu_Online_Test test)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Edu_Online_Test));
            Model.Edu_Online_Test newTest = new Model.Edu_Online_Test
            {
                TestId = newKeyID,
                TestConditionId = test.TestConditionId,
                TestDBId = test.TestDBId
            };
            //newTest.TestCode = test.TestCode;

            db.Edu_Online_Test.InsertOnSubmit(newTest);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试卷生成号
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTest(string testId, int? testCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_Test newTest = db.Edu_Online_Test.First(e => e.TestId == testId);
            newTest.TestCode = testCode;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="workPostId"></param>
        /// <param name="aBVolume"></param>
        public static void DeleteTest(string workPostId, string aBVolume)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var test = from x in db.Edu_Online_Test
                       join y in db.Edu_Online_TestCondition on x.TestConditionId equals y.TestConditionId
                       where y.WorkPostId==workPostId && y.ABVolume==aBVolume
                       select x;
            db.Edu_Online_Test.DeleteAllOnSubmit(test);
            db.SubmitChanges();
        }

      
        /// <summary>
        /// 根据条件ID获取试题条件信息
        /// </summary>
        /// <param name="testConditionId"></param>
        /// <returns></returns>
        public static Model.Edu_Online_TestCondition GetTestCondition(string testConditionId)
        {
            return Funs.DB.Edu_Online_TestCondition.FirstOrDefault(e => e.TestConditionId == testConditionId);
        }

        /// <summary>
        /// 根据条件获取试题条件信息
        /// </summary>
        /// <param name="testConditionId"></param>
        /// <returns></returns>
        public static List<Model.Edu_Online_TestCondition> GetTestCondition(string workPostId,string aBVolume)
        {
            var q= Funs.DB.Edu_Online_TestCondition.Where(e => e.WorkPostId == workPostId && e.ABVolume == aBVolume);
            if (q.Count() > 0)
            {
                return q.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_WorkPost> GetWorkPostList()
        {
            var q = (from x in Funs.DB.Base_WorkPost orderby x.WorkPostCode select x).ToList();
            List<Model.Base_WorkPost> list = new List<Model.Base_WorkPost>();
            for (int i = 0; i < q.Count(); i++)
            {
                list.Add(q[i]);
            }
            return list;
        }
    }
}
