using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 隐患整改单
    /// </summary>
    public class RectifyNoticesController : ApiController
    {
        #region 根据Id获取隐患整改单
        /// <summary>
        /// 根据Id获取隐患整改单
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesById(string rectifyNoticesId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = BLL.APIRectifyNoticesService.getRectifyNoticesById(rectifyNoticesId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、states获取隐患整改单
        /// <summary>
        /// 根据projectId、states获取隐患整改单
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states">状态 0：待签发；1：待整改；2：已整改，待复查；3：已确认，即已闭环；</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesByProjectIdStates(string projectId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = BLL.APIRectifyNoticesService.getRectifyNoticesByProjectIdStates(projectId, states);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize).ToList();

                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取各状态风险数
        /// <summary>
        /// 根据projectId获取各状态风险数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getRectifyNoticesCount(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                //总数
                var getDataList = BLL.Funs.DB.Check_RectifyNotices.Where(x => x.ProjectId == projectId);
                int tatalCount = getDataList.Count();
                //待签发 0
                int count0 = getDataList.Where(x => !x.SignDate.HasValue).Count();
                //待整改 1
                int count1 = getDataList.Where(x => x.SignDate.HasValue && !x.CompleteDate.HasValue).Count();
                //待复查 2
                int count2 = getDataList.Where(x => x.CompleteDate.HasValue && !x.ReCheckDate.HasValue).Count();
                //已闭环 3
                int count3 = getDataList.Where(x => x.ReCheckDate.HasValue).Count();
                responeData.data = new { tatalCount, count0, count1, count2, count3 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存RectifyNotices
        /// <summary>
        /// 保存RectifyNotices
        /// </summary>
        /// <param name="rectifyNotices"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveRectifyNotices([FromBody] Model.RectifyNoticesItem rectifyNotices)
        {
            var responeData = new Model.ResponeData();
            try
            {
                BLL.APIRectifyNoticesService.SaveRectifyNotices(rectifyNotices);
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
