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
    /// HSE管理月报
    /// </summary>
    public class HSEMonthController : ApiController
    {
        #region 获取HSE管理月报列表
        /// <summary>
        /// 获取HSE管理月报列表
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="reportMonths">月份</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getHSEMonthList(string projectId, string reportMonths, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIHSEMonthService.getHSEMonthList(projectId, reportMonths);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = (from x in getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                   select x).ToList();
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

        #region 获取HSE管理月报详细信息
        /// <summary>
        /// 获取HSE管理月报详细信息
        /// </summary>
        /// <param name="monthReportId">主键ID</param>
        /// <returns></returns>
        public Model.ResponeData getHSEMonth(string monthReportId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIHSEMonthService.getHSEMonth(monthReportId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        //#region 保存HSE管理月报信息
        ///// <summary>
        ///// 保存HSE管理月报信息
        ///// </summary>
        ///// <param name="hseDiaryItem">HSE管理月报</param>
        //[HttpPost]
        //public Model.ResponeData SaveHSEDiary([FromBody] Model.HSEDiaryItem hseDiaryItem)
        //{
        //    var responeData = new Model.ResponeData();
        //    try
        //    {
        //        APIHSEDiaryService.SaveHSEDiary(hseDiaryItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        responeData.code = 0;
        //        responeData.message = ex.Message;
        //    }

        //    return responeData;
        //}
        //#endregion
    }
}
