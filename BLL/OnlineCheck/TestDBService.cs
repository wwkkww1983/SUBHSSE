using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TestDBService
    {

        /// <summary>
        /// 添加试题
        /// </summary>
        /// <param name="?"></param>
        public static void AddTestDB(Model.Edu_Online_TestDB test)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Edu_Online_TestDB));
            Model.Edu_Online_TestDB newTest = new Model.Edu_Online_TestDB
            {
                TestId = newKeyID,
                TestType = test.TestType,
                Chapter = test.Chapter,
                ItemType = test.ItemType,
                TestNo = test.TestNo,
                TestKey = test.TestKey,
                KeyNumber = test.KeyNumber,
                TestContent = test.TestContent,
                TestPath = test.TestPath
            };

            db.Edu_Online_TestDB.InsertOnSubmit(newTest);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTestDB(Model.Edu_Online_TestDB test)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_TestDB newTest = db.Edu_Online_TestDB.First(e => e.TestId == test.TestId);
            newTest.TestType = test.TestType;
            newTest.Chapter = test.Chapter;
            newTest.ItemType = test.ItemType;
            newTest.TestNo = test.TestNo;
            newTest.TestKey = test.TestKey;
            newTest.KeyNumber = test.KeyNumber;
            newTest.TestContent = test.TestContent;
            newTest.TestPath = test.TestPath;

            db.SubmitChanges();
        }


        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteTestDB(string testId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_TestDB test = db.Edu_Online_TestDB.First(e => e.TestId == testId);
            db.Edu_Online_TestDB.DeleteOnSubmit(test);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据Id获取试题库信息
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public static Model.Edu_Online_TestDB GetTestDBByTestId(string testId)
        {
            return Funs.DB.Edu_Online_TestDB.FirstOrDefault(x => x.TestId == testId);
        }

        /// <summary>
        ///  试题库信息
        /// </summary>
        /// <param name="testType"></param>
        /// <param name="chapter"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static List<Model.Edu_Online_TestDB> GetTestDB(string testType,string chapter,string itemType)
        {
            return Funs.DB.Edu_Online_TestDB.Where(x => x.TestType == testType && x.Chapter == chapter && x.ItemType == itemType).ToList();
        }

        /// <summary>
        /// 获取试题类型列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTestTypeList()
        {
            var q = (from x in Funs.DB.Edu_Online_TestDB  select x.TestType).Distinct().ToList();
            List<string> list = new List<string>();
            for (int i = 0; i < q.Count(); i++)
            {
                list.Add(q[i]);
            }
            return list;
        }

        /// <summary>
        /// 获取试题章节列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetChapterList()
        {
            var q = (from x in Funs.DB.Edu_Online_TestDB select x.Chapter).Distinct().ToList();
            List<string> list = new List<string>();
            for (int i = 0; i < q.Count(); i++)
            {
                list.Add(q[i]);
            }
            return list;
        }

        /// <summary>
        /// 获取题型（如：单选题，判断题等）列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetItemTypeList()
        {
            var q = (from x in Funs.DB.Edu_Online_TestDB select x.ItemType).Distinct().ToList();
            List<string> list = new List<string>();
            for (int i = 0; i < q.Count(); i++)
            {
                list.Add(q[i]);
            }
            return list;
        }
    }
}
