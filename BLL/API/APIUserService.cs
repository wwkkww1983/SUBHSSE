using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIUserService
    {
       /// <summary>
       /// 获取用户登录信息
       /// </summary>
       /// <param name="userInfo"></param>
       /// <returns></returns>
        public static Model.UserItem UserLogOn(Model.UserItem userInfo)
        {
            var getUser = Funs.DB.View_Sys_User.FirstOrDefault(x => (x.Account == userInfo.Account || x.Telephone == userInfo.Telephone) && x.IsPost == true && x.Password == Funs.EncryptionPassword(userInfo.Password));
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_Sys_User, Model.UserItem>().Map(getUser);
        }
        
        /// <summary>
        /// 根据userId获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Model.UserItem getUserByUserId(string userId)
        {
            var getUser = Funs.DB.View_Sys_User.FirstOrDefault(x => x.UserId == userId);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_Sys_User, Model.UserItem>().Map(getUser);
        }

        /// <summary>
        /// 根据projectId、unitid获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Model.UserItem> getUserByProjectIdUnitId(string projectId, string unitId)
        {
            IQueryable <Model.View_Sys_User> users =null;
            if (!string.IsNullOrEmpty(projectId))
            {
                users = (from x in Funs.DB.View_Sys_User
                         join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                         where y.ProjectId == projectId && x.IsAuditFlow == true && (x.UnitId == unitId || unitId == null)
                         orderby x.RoleCode, x.UserCode
                         select x);
            }
            else
            {
                users = (from x in Funs.DB.View_Sys_User
                         where x.IsPost == true && x.IsAuditFlow == true && (x.UnitId == unitId || unitId == null)
                         orderby x.UserCode
                         select x);
            }
            return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.View_Sys_User>, List<Model.UserItem>>().Map(users.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static List<Model.UserItem> UserLogOn2()
        {
            var user = from x in Funs.DB.Sys_User
                       where x.IsPost == true
                       select x;
            return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Sys_User>, List<Model.UserItem>>().Map(user.ToList());
        }
    }
}
