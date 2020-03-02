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
        public Model.ResponeData getSubscribeMessage()
        {
            var responeData = new Model.ResponeData();
            try
            {               
                responeData.data = APICommonService.getSubscribeMessage();
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
        public Model.ResponeData getUserOpenId(string userId,string jsCode)
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
