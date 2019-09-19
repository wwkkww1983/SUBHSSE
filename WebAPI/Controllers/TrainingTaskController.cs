using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class TrainingTaskController : ApiController
    {
        #region 根据ProjectId、PersonId获取培训任务教材明细列表
        /// <summary>
        /// 根据ProjectId、PersonId获取培训任务教材明细列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="personId"></param>
        /// <param name="states"></param>
        /// <param name="pageIndex">0-未发布；1-已发布；2-已完成</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingTaskListByProjectIdPersonId(string projectId, string personId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITrainingTaskService.getTrainingTaskListByProjectIdPersonId(projectId, personId, states);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TrainStartDate).Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize)
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

        #region 根据TaskId获取培训任务教材明细列表
        /// <summary>
        /// 根据TaskId获取培训任务教材明细列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingTaskItemListByTaskId(string taskId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APITrainingTaskService.getTrainingTaskItemListByTaskId(taskId);
                int pageCount = getQualityLists.Count;
                if (pageCount > 0)
                {
                    var getdata = from x in getQualityLists.OrderByDescending(u => u.TrainingItemCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
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
    }
}
