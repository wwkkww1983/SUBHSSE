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
    /// 考试-考生记录信息
    /// </summary>
    public class TestRecordController : ApiController
    {
        #region 根据TestPlanId获取考生及试卷列表
        /// <summary>
        /// 根据TestPlanId获取考生及试卷列表
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns>考试人员</returns>
        public Model.ResponeData getTestRecordListByTestPlanId(string testPlanId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestRecordService.getTestRecordListByTestPlanId(testPlanId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据试卷ID获取试卷记录详细
        /// <summary>
        /// 根据试卷ID获取试卷记录详细
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns>试卷详细</returns>
        public Model.ResponeData getTestRecordByTestRecordId(string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestRecordService.getTestRecordByTestRecordId(testRecordId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据TestRecordId获取试卷题目列表
        /// <summary>
        /// 根据TestRecordId获取试卷题目列表
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns>试卷题目列表</returns>
        public Model.ResponeData geTestRecordItemListByTestRecordId(string testRecordId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataLists = APITestRecordService.geTestRecordItemListByTestRecordId(testRecordId);
                int pageCount = getDataLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataLists = getDataLists.OrderBy(x => x.TestType).ThenBy(x=>x.TrainingItemCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataLists };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据TestRecordItemId获取试卷题目详细
        /// <summary>
        /// 根据TestRecordItemId获取试卷题目详细
        /// </summary>
        /// <param name="testRecordItemId"></param>
        /// <returns>考试人员</returns>
        public Model.ResponeData geTestRecordItemByTestRecordItemId(string testRecordItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITestRecordService.geTestRecordItemByTestRecordItemId(testRecordItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据ProjectId、PersonId获取当前人试卷列表
        /// <summary>
        /// 根据ProjectId、PersonId获取当前人试卷列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="personId">人员ID(null查全部)</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>考试记录列表</returns>
        public Model.ResponeData getTrainingTestRecordListByProjectIdPersonId(string projectId, string personId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {                
                var getDataLists = APITestRecordService.getTrainingTestRecordListByProjectIdPersonId(projectId, personId);
                int pageCount = getDataLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataLists = getDataLists.OrderByDescending(x => x.TestStartTime).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataLists };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据ProjectId获取所有考试记录列表
        /// <summary>
        /// 根据ProjectId获取所有考试记录列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>考试记录列表</returns>
        public Model.ResponeData getTrainingTestRecordListByProjectId(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataLists= APITestRecordService.getTrainingTestRecordListByProjectId(projectId);
                int pageCount = getDataLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataLists = getDataLists.OrderByDescending(x => x.TestStartTime).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataLists };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据TestRecordItemId、selectedItem 考生答题
        /// <summary>
        /// 根据TestRecordItemId、selectedItem 考生答题
        /// </summary>
        /// <param name="testRecordItemId">题目ID</param>
        /// <param name="selectedItem">选项</param>
        public Model.ResponeData getTestRecordItemAnswerBySelectedItem(string testRecordItemId, string selectedItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getItem = BLL.TestRecordItemService.GetTestRecordItemTestRecordItemId(testRecordItemId);
                if (getItem != null)
                {
                    //更新没有结束时间且超时的考试记录
                    int closeCount = TestRecordService.UpdateTestEndTimeNull(getItem.TestRecordId);
                    if (closeCount > 0)
                    {
                        responeData.code = 2;
                        responeData.message = "本次考试已结束，系统自动交卷！";
                    }
                    else
                    {
                        APITestRecordService.getTestRecordItemAnswerBySelectedItem(getItem, selectedItem);
                    }
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

        #region 交卷 
        /// <summary>
        ///  交卷
        /// </summary>
        /// <param name="testRecordId">试卷ID</param>
        public Model.ResponeData getSubmitTestRecordByTestRecordId(string testRecordId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APITestRecordService.getSubmitTestRecordByTestRecordId(testRecordId);
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
