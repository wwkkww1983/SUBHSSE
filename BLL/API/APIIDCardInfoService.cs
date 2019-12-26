using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 获取身份证信息
    /// </summary>
    public static class APIIDCardInfoService
    {
        /// <summary>
        ///  获取身份证信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReadIDCardInfo(string url)
        {
            string access_token = string.Empty;
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

            return APIGetHttpService.Http("https://api.weixin.qq.com/cv/ocr/idcard?type=photo&img_url=" + url + "&access_token=" + access_token, "POST");
        }
    }
}
