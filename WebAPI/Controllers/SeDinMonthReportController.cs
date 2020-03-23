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
        #region 获取赛鼎月报列表信息
        /// <summary>
        /// 获取赛鼎月报列表信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="month">月份</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportList(string projectId,string month, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APISeDinMonthReportService.getSeDinMonthReportList(projectId, month);
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

        #region 获取赛鼎月报初始化页面详细信息
        /// <summary>
        /// 获取赛鼎月报初始化页面详细信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="month">月份</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportNullPage(string projectId, string month, string startDate, string endDate, string pageNum)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (pageNum == "0")   ////封面
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReportNullPage0(projectId);
                }
                else if (pageNum == "1")   ////1、项目信息
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReportNullPage1(projectId);
                }
                else if (pageNum == "2")   ////2、项目安全工时统计
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReportNullPage2(projectId, month);
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

        #region 获取赛鼎月报详细信息
        /// <summary>
        ///  根据主键ID获取赛鼎月报
        /// </summary>
        /// <param name="monthReportId">月报主键ID</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportById(string monthReportId, string pageNum)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (pageNum == "0")
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport0ById(monthReportId);
                }
                else if (pageNum == "1")
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport1ById(monthReportId);
                }
                else if (pageNum == "2")
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport2ById(monthReportId);
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

        #region 保存赛鼎月报信息
        #region 保存 MonthReport0 封面
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport0([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport0(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存 MonthReport1、项目信息
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport1([FromBody] Model.SeDinMonthReport1Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport1(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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

        #region 保存 MonthReport2、项目安全工时统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport2([FromBody] Model.SeDinMonthReport2Item newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport2(newItem);
                }
                else
                {
                    responeData.code = 1;
                    responeData.message = "请先保存月报主表信息！";
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
        #endregion
    }
}
