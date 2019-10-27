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
        /// 根据unitid获取用户信息
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getUserByUnitId(string unitId)
        {
            var getUser = (from x in Funs.DB.Sys_User
                           join y in Funs.DB.Sys_Role on x.RoleId equals y.RoleId
                           where x.UnitId == unitId && x.IsPost == true
                           orderby x.UserName 
                           select new Model.BaseInfoItem { BaseInfoId = x.UserId, BaseInfoName = x.UserName, BaseInfoCode = x.Telephone }).ToList();

            return getUser;
        }
        
        /// <summary>
        ///  根据projectId、unitid获取用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.UserItem> getUserByProjectIdUnitIdQuery(string projectId, string unitId, string roleIds, string strParam)
        {
            List<Model.UserItem> getDataList = new List<Model.UserItem>();
            List<string> roleList = Funs.GetStrListByStr(roleIds, ',');
            if (!string.IsNullOrEmpty(projectId))
            {
                getDataList = (from x in Funs.DB.Sys_User
                               join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                               where y.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                  && (roleIds == null || roleList.Contains(y.RoleId)) && (strParam == null || x.UserName.Contains(strParam))
                               orderby x.UserName
                               select new Model.UserItem
                               {
                                   UserId = x.UserId,
                                   Account = x.Account,
                                   UserCode = x.UserCode,
                                   Password = x.Password,
                                   UserName = x.UserName,
                                   RoleId = y.RoleId,
                                   RoleName = Funs.DB.Sys_Role.First(z => z.RoleId == y.RoleId).RoleName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                   LoginProjectId = y.ProjectId,
                                   LoginProjectName = Funs.DB.Base_Project.First(z => z.ProjectId == y.ProjectId).ProjectName,
                                   IdentityCard = x.IdentityCard,
                                   Email = x.Email,
                                   Telephone = x.Telephone,
                                   IsOffice = x.IsOffice,
                                   SignatureUrl = x.SignatureUrl.Replace('\\', '/'),

                               }).ToList();
            }
            else
            {
                getDataList = (from x in Funs.DB.Sys_User
                               where x.IsPost == true && (x.UnitId == unitId || unitId == null)
                              && (roleIds == null || roleList.Contains(x.RoleId)) && (strParam == null || x.UserName.Contains(strParam))
                               orderby x.UserName
                               select new Model.UserItem
                               {
                                   UserId = x.UserId,
                                   Account = x.Account,
                                   UserCode = x.UserCode,
                                   Password = x.Password,
                                   UserName = x.UserName,
                                   RoleId = x.RoleId,
                                   RoleName = Funs.DB.Sys_Role.First(z => z.RoleId == x.RoleId).RoleName,
                                   UnitId = x.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                   //LoginProjectId = y.ProjectId,
                                   //LoginProjectName = Funs.DB.Base_Project.First(z => z.ProjectId == y.ProjectId).ProjectName,
                                   IdentityCard = x.IdentityCard,
                                   Email = x.Email,
                                   Telephone = x.Telephone,
                                   IsOffice = x.IsOffice,
                                   SignatureUrl = x.SignatureUrl.Replace('\\', '/'),

                               }).ToList();
            }
            return getDataList;
        }

        /// <summary>
        ///  获取所有在岗用户
        /// </summary>
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
