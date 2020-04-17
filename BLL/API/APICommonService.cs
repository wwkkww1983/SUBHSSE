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

        #region 订阅消息
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
        #endregion

        #region 获取OpenId消息
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
        #endregion

        #region 待办信息接收(不审核)保存方法
        /// <summary>
        /// 待办信息接收(不审核)保存方法
        /// </summary>
        /// <param name="flowReceiveItem"></param>
        /// <returns></returns>
        public static void SaveFlowReceiveItem(Model.FlowReceiveItem flowReceiveItem)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                //// 隐患整改单
                if (flowReceiveItem.MenuId == Const.ProjectRectifyNoticeMenuId)
                {
                    var getRectifyNotices = db.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == flowReceiveItem.DataId);
                    if (getRectifyNotices != null)
                    {
                        if (getRectifyNotices.ProfessionalEngineerId == flowReceiveItem.OperaterId) ////专业工程师
                        {
                            if (!getRectifyNotices.ProfessionalEngineerTime1.HasValue)
                            {
                                getRectifyNotices.ProfessionalEngineerTime1 = DateTime.Now;
                                getRectifyNotices.ProfessionalEngineerTime2 = null;
                            }
                            else
                            {
                                getRectifyNotices.ProfessionalEngineerTime2 = DateTime.Now;
                            }
                        }
                        else if (getRectifyNotices.ConstructionManagerId == flowReceiveItem.OperaterId) ////施工经理
                        {
                            if (!getRectifyNotices.ConstructionManagerTime1.HasValue)
                            {
                                getRectifyNotices.ConstructionManagerTime1 = DateTime.Now;
                                getRectifyNotices.ConstructionManagerTime2 = null;
                            }
                            else
                            {
                                getRectifyNotices.ConstructionManagerTime2 = DateTime.Now;
                            }
                        }
                        else if (getRectifyNotices.ProjectManagerId == flowReceiveItem.OperaterId) ////项目经理
                        {
                            if (!getRectifyNotices.ProjectManagerTime1.HasValue)
                            {
                                getRectifyNotices.ProjectManagerTime1 = DateTime.Now;
                                getRectifyNotices.ProjectManagerTime2 = null;
                            }
                            else
                            {
                                getRectifyNotices.ProjectManagerTime2 = DateTime.Now;
                            }
                        }
                        else if (getRectifyNotices.DutyPersonId == flowReceiveItem.OperaterId) //// 接收人
                        {
                            if (!getRectifyNotices.DutyPersonTime.HasValue)
                            {
                                getRectifyNotices.DutyPersonTime = DateTime.Now;                               
                            }
                        }

                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion
    }
}
