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
        ///  <param name="states">状态0-待提交；1-已提交</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportList(string projectId,string month, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APISeDinMonthReportService.getSeDinMonthReportList(projectId, month, states);
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
        
        #region 获取赛鼎月报详细信息
        /// <summary>
        /// 根据主键ID获取赛鼎月报
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="month">月份</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public Model.ResponeData getSeDinMonthReportInfo(string projectId, string month, string startDate, string endDate, string pageNum)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (pageNum == "0") ////封面
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport0ById(projectId, month);
                    if (responeData.data == null)
                    {
                        responeData.data= APISeDinMonthReportService.getSeDinMonthReportNullPage0(projectId);
                    }
                }
                else if (pageNum == "1") ////1、项目信息
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport1ById(projectId, month);
                    if (responeData.data == null)
                    {
                        responeData.data = APISeDinMonthReportService.getSeDinMonthReportNullPage1(projectId);
                    }
                }
                else if (pageNum == "2") ////2、项目安全工时统计
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport2ById(projectId, month);
                    if (responeData.data == null)
                    {
                        responeData.data = APISeDinMonthReportService.getSeDinMonthReportNullPage2(projectId, month, startDate, endDate);
                    }
                }
                else if (pageNum == "3") ////3、项目HSE事故、事件统计
                {                    
                    var getInfo = APISeDinMonthReportService.getSeDinMonthReport3ById(projectId, month);            
                    if (getInfo == null || getInfo.SeDinMonthReport3Item == null || getInfo.SeDinMonthReport3Item.Count() == 0)
                    {
                        getInfo = APISeDinMonthReportService.getSeDinMonthReportNullPage3(projectId, month, startDate, endDate);
                    }

                    responeData.data = getInfo;
                }
                else if (pageNum == "4") ////2、项目安全工时统计
                {
                    var getLists= APISeDinMonthReportService.getSeDinMonthReport4ById(projectId, month);
                    if (getLists.Count() == 0)
                    {
                        getLists = APISeDinMonthReportService.getSeDinMonthReportNullPage4(projectId, month, startDate, endDate);
                    }
                    responeData.data = getLists;
                }
                else ////13、14、本月HSE活动综述、下月HSE工作计划
                {
                    responeData.data = APISeDinMonthReportService.getSeDinMonthReport13ById(projectId, month); ;
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

        #region 保存 MonthReport3、项目HSE事故、事件统计
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport3([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (!string.IsNullOrEmpty(newItem.MonthReportId))
                {
                    responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport3(newItem);
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

        #region 保存 MonthReport13、14、本月HSE活动综述、下月HSE工作计划
        /// <summary>
        /// 保存赛鼎月报
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSeDinMonthReport13([FromBody] Model.SeDinMonthReportItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APISeDinMonthReportService.SaveSeDinMonthReport13(newItem);
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
