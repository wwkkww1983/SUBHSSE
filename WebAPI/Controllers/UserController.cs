using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        #region 根据账号或手机号码登录方法
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData postLoginOn([FromBody] Model.UserItem userInfo)
        {
            ///登录方法 Model.UserItem
            var responeData = new Model.ResponeData
            {
                message = "账号密码不匹配！"
            };
            try
            {
                var user = BLL.APIUserService.UserLogOn(userInfo);
                if (user != null)
                {
                    responeData.message = "登录成功！";
                }
                responeData.data = user;
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据userid获取用户信息
        /// <summary>
        /// 根据userid获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByUserId(string userId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIUserService.getUserByUserId(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取用户信息
        /// <summary>
        /// 根据projectId、unitid获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitId(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIUserService.getUserByProjectIdUnitId(projectId, unitId);
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
