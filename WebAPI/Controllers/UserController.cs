using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]      
        public Model.ResponeData postLoginOn([FromBody] Model.UserItem userInfo)
        {
            ///登录方法 Model.UserItem
            string msg = "账号密码不匹配！";
            int code= 1;
            object data = new object();
            try
            {
                var user = UserAPIService.UserLogOn(userInfo);
                if (user != null)
                {
                    msg = "登录成功！";
                }
                data = user;
            }
            catch (Exception ex)
            {
                code = 0;
                msg = ex.Message;
            }

            return new Model.ResponeData() { code = code, message = msg, data = data };
        }


        public Model.ResponeData getUser(string userId)
        {
            var getUser = UserAPIService.getUserByUserId(userId);
            return new Model.ResponeData() { code = 1, message = "", data = getUser };
        }
    }
}
