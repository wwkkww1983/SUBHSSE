using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserItem
    {
       /// <summary>
       /// 用户ID
       /// </summary>
       public string UserId
        {
           get;
           set;
       }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
           get;
           set;
       }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 当前项目ID
        /// </summary>
        public string LoginProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }
    }
}
