using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    ///  公用接口
    /// </summary>
    public class CommonController : ApiController
    {
        #region  订阅消息
        /// <summary>
        ///  订阅消息
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getSubscribeMessage(string touser, string template_id,string page,object data,string miniprogram_state,string lang)
        {
            var responeData = new Model.ResponeData();
            try
            {               
                responeData.data = APICommonService.getSubscribeMessage(touser, template_id, page, data, miniprogram_state, lang);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 订阅消息
        /// <summary>
        ///  订阅消息
        /// </summary>
        /// <param name="messgeInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData postSubscribeMessage([FromBody] Model.SubscribeMessageItem messgeInfo)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (messgeInfo != null)
                {
                    responeData.data = APICommonService.getSubscribeMessage(messgeInfo.touser, messgeInfo.template_id, messgeInfo.page, messgeInfo.data, messgeInfo.miniprogram_state, messgeInfo.lang);
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

        #region  获取OpenId消息
        /// <summary>
        ///  获取OpenId消息
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getUserOpenId(string userId, string jsCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APICommonService.getUserOpenId(userId, jsCode);
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
