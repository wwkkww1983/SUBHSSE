using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 考试计划记录
    /// </summary>
    public class TestPlanController : ApiController
    {
        #region 根据projectId、states获取考试计划列表
        /// <summary>
        /// 根据projectId、states获取考试计划列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="states">状态（0-待提交；1-已发布；2-考试中；3考试结束）</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTestPlanListByProjectIdStates(string projectId, string states,int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITestPlanService.getTestPlanListByProjectIdStates(projectId, states);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TestStartTime).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                  select x;
                    responeData.data = new { pageCount, getdata };
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据考试计划ID获取考试详细
        /// <summary>
        ///  根据考试计划ID获取考试详细
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns></returns>
        public Model.ResponeData getTestPlanByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPlanService.getTestPlanByTestPlanId(testPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据培训计划ID生成 考试计划信息
        /// <summary>
        ///  根据培训计划ID生成 考试计划信息
        /// </summary>
        /// <param name="trainingPlanId">培训计划ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public Model.ResponeData getSaveTestPlanByTrainingPlanId(string trainingPlanId,string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPlanService.SaveTestPlanByTrainingPlanId(trainingPlanId, userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存 TestPlan [增加、修改、开始考试、结束考试]
        /// <summary>
        /// 保存TestPlan [增加、修改、开始考试、结束考试]
        /// </summary>
        /// <param name="testPlan">考试计划项目</param>
        [HttpPost]
        public Model.ResponeData SaveTestPlan([FromBody] Model.TestPlanItem testPlan)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.message = APITestPlanService.SaveTestPlan(testPlan);
                if (!string.IsNullOrEmpty(responeData.message))
                {
                    responeData.code = 2;
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据TestPlanId获取考试试题类型列表
        /// <summary>
        /// 根据TestPlanId获取考试试题类型列表
        /// </summary>
        /// <param name="testPlanId">考试计划ID</param>
        /// <returns>试题类型</returns>
        public Model.ResponeData getTestPlanTrainingListByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestPlanService.getTestPlanTrainingListByTestPlanId(testPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据PersonId、TestPlanId扫描考试计划二维码
        /// <summary>
        /// 根据PersonId、TestPlanId扫描考试计划二维码
        /// </summary>
        /// <param name="testPlanId">培训考试计划ID</param>
        /// <param name="personId">人员ID</param>
        /// <returns></returns>
        public Model.ResponeData getTestPlanRecordItemByTestPlanIdPersonId(string testPlanId, string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getTestPlan = Funs.DB.Training_TestPlan.FirstOrDefault(e => e.TestPlanId == testPlanId); 
                if (getTestPlan != null)
                {
                    var person = PersonService.GetPersonByUserId(personId);
                    if (person != null && person.ProjectId == getTestPlan.ProjectId)
                    {
                        //2-考试中；生成考试试卷     
                        if (getTestPlan.States == "2" && getTestPlan.TestStartTime <= DateTime.Now && getTestPlan.TestEndTime >= DateTime.Now)
                        {
                            var testRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == getTestPlan.TestPlanId && x.TestManId == person.PersonId);
                            if (testRecord != null && !testRecord.TestStartTime.HasValue)
                            {////考试时长
                                testRecord.Duration = getTestPlan.Duration;
                                testRecord.TestStartTime = DateTime.Now;
                                Funs.DB.SubmitChanges();                               
                            }
                            string testRecordId = APITestRecordService.getTestRecordItemByTestPlanIdPersonId(getTestPlan, person);
                            responeData.code = 2;
                            responeData.data = new { testRecordId };
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(getTestPlan.PlanId) && getTestPlan.UnitIds.Contains(person.UnitId) && (getTestPlan.WorkPostIds == null || getTestPlan.WorkPostIds.Contains(person.WorkPostId)))
                            {
                                //0-待提交；1-已发布未考试 将人员添加进考试记录                        
                                var testTRecord = Funs.DB.Training_TestRecord.FirstOrDefault(x => x.TestPlanId == testPlanId && x.TestManId == personId);
                                if ((getTestPlan.States == "0" || getTestPlan.States == "1") && testTRecord == null && !string.IsNullOrEmpty(personId))
                                {
                                    Model.Training_TestRecord newTestRecord = new Model.Training_TestRecord
                                    {
                                        TestRecordId = SQLHelper.GetNewID(),
                                        ProjectId = getTestPlan.ProjectId,
                                        TestPlanId = getTestPlan.TestPlanId,
                                        TestManId = personId,
                                    };
                                    TestRecordService.AddTestRecord(newTestRecord);
                                    responeData.code = 3;
                                    responeData.message = "您已加入考试计划！";
                                }
                            }
                        }
                    }
                    if (responeData.code == 1)
                    {
                        //其他状态时 查看考试计划详细页
                        responeData.data = APITestPlanService.getTestPlanByTestPlanId(testPlanId);
                    }
                }
                else
                {
                    responeData.message = "扫描有误！";
                    responeData.code = 0;
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion
    }
}
