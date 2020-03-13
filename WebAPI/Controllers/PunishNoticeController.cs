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
    /// 处罚通知单
    /// </summary>
    public class PunishNoticeController : ApiController
    {
        #region 根据主键ID获取处罚通知单
        /// <summary>
        ///  根据主键ID获取处罚通知单
        /// </summary>
        /// <param name="punishNoticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getPunishNoticeById(string punishNoticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPunishNoticeService.getPunishNoticeById(punishNoticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取处罚通知单信息-查询
        /// <summary>
        /// 根据projectId、unitid获取处罚通知单信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        ///  <param name="strParam">查询条件</param>
        ///  <param name="states">查询条件</param>
        /// <param name="pageIndex"></param>  
        /// <returns></returns>
        public Model.ResponeData getPunishNoticeList(string projectId, string unitId, string strParam, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPunishNoticeService.getPunishNoticeList(projectId, unitId, strParam, states);
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

        #region 保存处罚通知单 Check_PunishNotice
        /// <summary>
        /// 保存处罚通知单 Check_PunishNotice
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SavePunishNotice([FromBody] Model.PunishNoticeItem newItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPunishNoticeService.SavePunishNotice(newItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 处罚单-附件通知单 回执单
        /// <summary>
        ///  上报处罚单-回执单
        /// </summary>
        /// <param name="punishNoticeId">主键</param>
        /// <param name="attachUrl">回执单路径</param>
        /// /// <param name="type">类型：0-通知单；1-回执单</param>
        /// <returns></returns>
        public Model.ResponeData getSavePunishNoticeReceiptUrl(string punishNoticeId, string attachUrl, string type = null)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPunishNoticeService.SavePunishNoticeReceiptUrl(punishNoticeId, attachUrl, type);
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
