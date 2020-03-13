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
    /// 工程暂停令
    /// </summary>
    public class PauseNoticeController : ApiController
    {
        #region 根据主键ID获取工程暂停令
        /// <summary>
        ///  根据主键ID获取工程暂停令
        /// </summary>
        /// <param name="PauseNoticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getPauseNoticeById(string PauseNoticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                 responeData.data = APIPauseNoticeService.getPauseNoticeById(PauseNoticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取工程暂停令信息-查询
        /// <summary>
        /// 根据projectId、unitid获取工程暂停令信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId">单位ID</param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">状态（0：全部；1：待确认；2：已确认）</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getPauseNoticeList(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPauseNoticeService.getPauseNoticeList(projectId, unitId, strParam, states);
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

        #region 保存工程暂停令 Check_PauseNotice
        /// <summary>
        /// 保存工程暂停令 Check_PauseNotice
        /// </summary>
        /// <param name="newItem">工程暂停令</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePauseNotice([FromBody] Model.PauseNoticeItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPauseNoticeService.SavePauseNotice(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion        

        #region 暂停令通知单附件
        /// <summary>
        ///  上报处罚单-回执单
        /// </summary>
        /// <param name="pauseNoticeId">主键</param>
        /// <param name="attachUrl">通知单路径</param>
        /// <returns></returns>
        public Model.ResponeData getSavePauseNoticeReceiptUrl(string pauseNoticeId, string attachUrl)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPauseNoticeService.SavePauseNoticeUrl(pauseNoticeId, attachUrl);
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
