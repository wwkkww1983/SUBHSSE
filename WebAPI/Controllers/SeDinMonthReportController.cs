using BLL;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 赛鼎月报
    /// </summary>
    public class SeDinMonthReportController : ApiController
    {
        #region 获取赛鼎月报详细信息
        /// <summary>
        ///  根据主键ID获取赛鼎月报
        /// </summary>
        /// <param name="monthReportId">月报主键ID</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportById(string monthReportId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APISeDinMonthReportService.getSeDinMonthReportById(monthReportId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取赛鼎月报列表信息
        /// <summary>
        /// 获取赛鼎月报列表信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="pageIndex">页数</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportList(string projectId,  int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APISeDinMonthReportService.getSeDinMonthReportList(projectId);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
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

        #region 保存赛鼎月报
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APISeDinMonthReportService.SaveSeDinMonthReport(newItem);
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
