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
    /// 用户
    /// </summary>
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
            //登录方法 Model.UserItem
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
                    responeData.data = user;
                }
                else
                {
                    var user1= BLL.APIPersonService.PersonLogOn(userInfo);
                    if (user1 != null)
                    {
                        responeData.message = "登录成功！";
                        responeData.code = 2;
                        responeData.data = user1;
                    }
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
                responeData.data = APIUserService.getUserByUserId(userId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据unitId获取用户信息
        /// <summary>
        /// 根据unitId获取用户信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByUnitid(string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByUnitId(unitId, null);
                responeData.data = new { getDataList.Count, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据unitId获取用户信息
        /// <summary>
        /// 根据unitId获取用户信息
        /// </summary>
        /// <param name="unitId">单位ID</param>
        /// <param name="strParam">查询</param>
        /// <returns></returns>
        public Model.ResponeData getUserByUnitidQuery(string unitId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByUnitId(unitId, strParam);
                responeData.data = new { getDataList.Count, getDataList };
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
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitId(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUserService.getUserByProjectIdUnitIdQuery(projectId, unitId, null, null);
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
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="roleIds"></param>
        /// <param name="strParam"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getUserByProjectIdUnitIdQuery(string projectId, string unitId, string roleIds, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIUserService.getUserByProjectIdUnitIdQuery(projectId, unitId, roleIds, strParam);
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
    }
}
