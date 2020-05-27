using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
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

        #region 发送订阅消息
        /// <summary>
        /// 发送订阅消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="thing2"></param>
        /// <param name="name1"></param>
        /// <param name="date3"></param>
        public static string SendSubscribeMessage(string userId, string thing2, string name1, string date3)
        {
            string access_token = APICommonService.getaccess_token();
            var getUser = Funs.DB.Sys_User.FirstOrDefault(x => x.UserId == userId);
            if (getUser != null && !string.IsNullOrEmpty(getUser.OpenId))
            {
                string miniprogram_state = ConfigurationManager.AppSettings["miniprogram_state"];
                if (string.IsNullOrEmpty(miniprogram_state))
                {
                    miniprogram_state = "formal";
                }
                string contenttype = "application/json;charset=utf-8";
                string url = $"https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token={access_token}";
                var tempData = new
                {
                    access_token,
                    touser = getUser.OpenId,
                    template_id = Const.WX_TemplateID,
                    page = "pages/home/main",
                    data = new
                    {
                        thing2 = new { value = thing2 },
                        name1 = new { value = name1 },
                        date3 = new { value = date3 }
                    },
                    miniprogram_state,
                    lang = "zh_CN",
                };
                string messages= APIGetHttpService.Http(url, "POST", contenttype, null, JsonConvert.SerializeObject(tempData));
                //// 记录
                SaveSysHttpLog(getUser.UserName, url, messages);
                return messages;
            }
            else
            {
                return "openId is null";
            }
        }
        #endregion

        #region 获取OpenId消息
        /// <summary>
        /// 获取OpenId消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public static string getUserOpenId(string userId, string jsCode,bool isRefresh = false)
        {
            string openId = string.Empty;
            string appid = getUnitAppId();
            string secret = getUnitSecret();
            //string appid = "wxb5f0e8051b7b9eee";
            //string secret = "626175f8860bf84beb4cf507b9445115";      
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                var getUser = db.Sys_User.FirstOrDefault(x => x.UserId == userId);
                if (getUser != null)
                {
                    if (!string.IsNullOrEmpty(getUser.OpenId) && !isRefresh)
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
                                db.SubmitChanges();

                                var getUsers = from x in db.Sys_User
                                               where x.UserId != getUser.UserId && x.OpenId == openId
                                               select x;
                                if (getUsers.Count() > 0)
                                {
                                    foreach (var item in getUsers)
                                    {
                                        item.OpenId = null;
                                        db.SubmitChanges();
                                    }
                                }
                            }
                        }
                        //// 记录
                        SaveSysHttpLog(getUser.UserName, getUrl, strJosn);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="HttpUrl"></param>
        /// <param name="LogTxt"></param>
        public static void SaveSysHttpLog(string userName, string httpUrl, string logTxt)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.Sys_HttpLog newLog = new Model.Sys_HttpLog()
                {
                    HttpLogId = SQLHelper.GetNewID(),
                    LogTime = DateTime.Now,
                    UserName = userName,
                    HttpUrl = httpUrl,
                    LogTxt = logTxt,
                };
                db.Sys_HttpLog.InsertOnSubmit(newLog);
                db.SubmitChanges();
            }
        }
    }
}
