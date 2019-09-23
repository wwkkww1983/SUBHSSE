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
        #region 根据TestPlanId获取考试人员列表
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
                                    TestScores=x.TestScores ?? 0,
                                    TestType = x.TestType,
                                    TemporaryUser=x.TemporaryUser,
                                }).ToList();
            return getDataLists;
        }
        #endregion
    }
}
