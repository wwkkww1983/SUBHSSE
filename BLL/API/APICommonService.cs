using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 获取身份证信息
    /// </summary>
    public static class APICommonService
    {
        #region  获取appid、secret
        /// <summary>
        ///  获取appid
        /// </summary>
        /// <returns></returns>
        public static string getUnitAppId()
        {
            string appid = string.Empty;
            string secret = string.Empty;
            switch (CommonService.GetIsThisUnitId())
            {
                case Const.UnitId_TCC_:
                    appid = Const.AppID_TCC_;
                    secret = Const.AppSecret_TCC_;
                    break;

                case Const.UnitId_SEDIN:
                    appid = Const.AppID_SEDIN;
                    secret = Const.AppSecret_SEDIN;
                    break;
            }

            return appid;
        }

        /// <summary>
        /// 获取secret
        /// </summary>
        /// <returns></returns>
        public static string getUnitSecret()
        {
            string appid = string.Empty;
            string secret = string.Empty;
            switch (CommonService.GetIsThisUnitId())
            {
                case Const.UnitId_TCC_:
                    appid = Const.AppID_TCC_;
                    secret = Const.AppSecret_TCC_;
                    break;

                case Const.UnitId_SEDIN:
                    appid = Const.AppID_SEDIN;
                    secret = Const.AppSecret_SEDIN;
                    break;

                case Const.UnitId_XJYJ:
                    appid = Const.AppID_XJYJ;
                    secret = Const.AppSecret_XJYJ;
                    break;
            }

            return secret;
        }
        #endregion

        #region 获取access_token信息
        /// <summary>
        ///  获取access_token信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getaccess_token()
        {
            string access_token = string.Empty;
            string appid = getUnitAppId();
            string secret = getUnitSecret();
            //string appid = "wxb5f0e8051b7b9eee";
            //string secret = "626175f8860bf84beb4cf507b9445115";

            var getToken = Funs.DB.Sys_AccessToken.FirstOrDefault();
            if (getToken != null && getToken.Endtime > DateTime.Now)
            {
                access_token = getToken.Access_token;
            }
            else
            {
                if (getToken != null)
                {
                    Funs.DB.Sys_AccessToken.DeleteOnSubmit(getToken);
                }
                var strJosn = APIGetHttpService.Http("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret);
                if (!string.IsNullOrEmpty(strJosn))
                {
                    JObject obj = JObject.Parse(strJosn);
                    access_token = obj["access_token"].ToString();
                    int expires_in = Funs.GetNewIntOrZero(obj["expires_in"].ToString());
                    Model.Sys_AccessToken newToken = new Model.Sys_AccessToken
                    {
                        Access_token = access_token,
                        Expires_in = expires_in,
                        Endtime = DateTime.Now.AddSeconds(expires_in),
                    };

                    Funs.DB.Sys_AccessToken.InsertOnSubmit(newToken);
                    Funs.SubmitChanges();
                }
            }

            return access_token;
        }
        #endregion

        /// <summary>
        ///  订阅消息
        /// </summary>
        /// <returns></returns>
        public static string getSubscribeMessage(string touser, string template_id, string page, object data, string miniprogram_state, string lang)
        {
            string access_token = APICommonService.getaccess_token();
            string url = "https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token="+ access_token + "&template_id="+ template_id + "&page=" + page + "&data=" + data + "&miniprogram_state=" + miniprogram_state + "&lang=" + lang;
            return APIGetHttpService.Http(url, "POST");
        }

        /// <summary>
        /// 获取OpenId消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public static string getUserOpenId(string userId, string jsCode)
        {
            string openId = string.Empty;
            string appid = getUnitAppId();
            string secret = getUnitSecret();
            //string appid = "wxb5f0e8051b7b9eee";
            //string secret = "626175f8860bf84beb4cf507b9445115";      
            var getUser = Funs.DB.Sys_User.FirstOrDefault(x=>x.UserId == userId);
            if (getUser != null)
            {
                if (!string.IsNullOrEmpty(getUser.OpenId))
                {
                    openId = getUser.OpenId;
                }
                else
                {
                    string getUrl = "https://api.weixin.qq.com/sns/jscode2session?appid=" + appid + "&secret=" + secret + "&js_code=" + jsCode + "&grant_type=authorization_code";
                    var strJosn = APIGetHttpService.Http(getUrl);
                    if (!string.IsNullOrEmpty(strJosn))
                    {
                        JObject obj = JObject.Parse(strJosn);
                        if (obj["openid"] != null)
                        {
                            openId = obj["openid"].ToString();
                            getUser.OpenId = openId;
                            Funs.SubmitChanges();
                        }
                    }
                }
            }        

            return openId;
        }
    }
}
