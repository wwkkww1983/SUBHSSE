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
                if (pageCount > 0)
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

        #region 保存 TestPlan
        /// <summary>
        /// 保存TestPlan
        /// </summary>
        /// <param name="testPlan">考试计划项目</param>
        [HttpPost]
        public Model.ResponeData SaveTestPlan([FromBody] Model.TestPlanItem testPlan)
        {
            var responeData = new Model.ResponeData();
            try
            {
                BLL.APITestPlanService.SaveTestPlan(testPlan);
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
    }
}
