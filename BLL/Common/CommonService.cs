﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CommonService
    {
        #region 获取当前人系统集合
        /// <summary>
        ///  获取当前人系统集合
        /// </summary> 
        /// <param name="userId">用户id</param>
        /// <returns>是否具有权限</returns>
        public static List<string> GetSystemPowerList(string userId)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                var getUser = db.Sys_User.FirstOrDefault(x => x.UserId == userId);
                if (getUser != null)
                {
                      return new List<string>() { "Menu_HSSE" };
                    //    if (userId == Const.sysglyId || userId == Const.hfnbdId)  ////|| getUser.DepartId == Const.Depart_constructionId
                    //    {
                    //        return new List<string>() { Const.Menu_Server, Const.Menu_HSSE, Const.Menu_CQMS, Const.Menu_HJGL };
                    //    }
                    //    else if (userId == Const.sedinId)
                    //    {
                    //        return new List<string>() { Const.Menu_CQMS };
                    //    }
                    //    else
                    //    {
                    //        List<string> returnList = new List<string>();
                    //        string rolesStr = string.Empty;
                    //        if (!string.IsNullOrEmpty(getUser.RoleId))
                    //        {
                    //            rolesStr = getUser.RoleId;
                    //            var getOffice = db.Sys_RolePower.FirstOrDefault(x => x.RoleId == getUser.RoleId && x.IsOffice == true);
                    //            if (getOffice != null)
                    //            {
                    //                returnList.Add(Const.Menu_Server);
                    //            }
                    //        }
                    //        ////获取项目角色的集合
                    //        var getPRoles = (from x in db.Project_ProjectUser
                    //                         join y in db.Base_Project on x.ProjectId equals y.ProjectId
                    //                         where (y.ProjectState == Const.ProjectState_1 || y.ProjectState == null) && x.UserId == userId && x.RoleId != null
                    //                         select x.RoleId).ToList();
                    //        foreach (var item in getPRoles)
                    //        {
                    //            if (string.IsNullOrEmpty(rolesStr))
                    //            {
                    //                rolesStr = item;
                    //            }
                    //            else
                    //            {
                    //                if (!rolesStr.Contains(item))
                    //                {
                    //                    rolesStr += "," + item;
                    //                }
                    //            }
                    //        }
                    //        ////项目角色集合list
                    //        List<string> roleIdList = Funs.GetStrListByStr(rolesStr, ',').Distinct().ToList();
                    //        var getProjectRolePowers = (from x in db.Sys_RolePower
                    //                                    where roleIdList.Contains(x.RoleId)
                    //                                    select x).ToList();
                    //        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_HSSE) != null)
                    //        {
                    //            returnList.Add(Const.Menu_HSSE);
                    //        }
                    //        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_CQMS) != null)
                    //        {
                    //            returnList.Add(Const.Menu_CQMS);
                    //        }
                    //        if (getProjectRolePowers.FirstOrDefault(x => x.MenuType == Const.Menu_HJGL) != null)
                    //        {
                    //            returnList.Add(Const.Menu_HJGL);
                    //        }

                    //        return returnList.Distinct().ToList();
                    //    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 获取当前人菜单集合
        /// <summary>
        ///  获取当前人菜单集合
        /// </summary> 
        /// <param name="projectId">项目ID</param>    
        /// <param name="userId">用户id</param>
        /// <returns>是否具有权限</returns>
        public static List<string> GetAllMenuList(string projectId, string userId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            List<Model.Sys_Menu> menus = new List<Model.Sys_Menu>();
            /// 启用且末级菜单
            var getMenus = from x in db.Sys_Menu
                           where x.IsUsed == true && x.IsEnd == true
                           select x;
            if (!string.IsNullOrEmpty(projectId))
            {
                getMenus = getMenus.Where(x => x.MenuType == Const.Menu_Project);
            }


            if (userId == Const.sysglyId || userId == Const.hfnbdId)
            {
                menus = getMenus.ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        menus = (from x in getMenus
                                 join y in db.Sys_RolePower on x.MenuId equals y.MenuId
                                 where y.RoleId == user.RoleId
                                 select x).ToList();
                    }
                }
                else
                {
                    var pUser = ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                        menus = (from x in db.Sys_RolePower
                                 join y in getMenus on x.MenuId equals y.MenuId
                                 where roleIdList.Contains(x.RoleId)
                                 select y).ToList();
                    }
                }
            }


            return menus.Select(x => x.MenuId).ToList();
        }
        #endregion

        #region 根据登陆id菜单id判断是否有权限
        /// <summary>
        /// 根据登陆id菜单id判断是否有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ReturnMenuByUserIdMenuId(string userId, string menuId, string projectId)
        {
            bool returnValue = false;
            var menu = SysMenuService.GetSysMenuByMenuId(menuId);
            if (menu != null)
            {
                ///1、当前用户是管理员 
                ///2、当前菜单是个人设置 资源库|| menu.MenuType == BLL.Const.Menu_Resource
                if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId || menu.MenuType == Const.Menu_Personal)
                {
                    returnValue = true;
                }              
                else if (string.IsNullOrEmpty(projectId)) ///本部、系统设置
                {
                    var user = BLL.UserService.GetUserByUserId(userId); ////用户
                    if (user != null && !string.IsNullOrEmpty(user.RoleId))
                    {
                        var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && x.RoleId == user.RoleId);
                        if (power != null)
                        {
                            returnValue = true;
                        }

                       // var getRoles=Funs.DB.Sys_Role.FirstOrDefault(x=> user.RoleId)
                    }
                }
                else
                {
                    ///3、管理角色、领导角色能访问项目菜单
                    bool isLeaderManage = IsLeaderOrManage(userId);
                    if (isLeaderManage  && menu.MenuType == BLL.Const.Menu_Project)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        var puser = ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ////用户
                        if (puser != null && !string.IsNullOrEmpty(puser.RoleId))
                        {
                            List<string> roleIdList = Funs.GetStrListByStr(puser.RoleId, ',');
                            var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && roleIdList.Contains(x.RoleId));
                            if (power != null)
                            {
                                returnValue = true;
                            }
                        }
                    }                    
                }
            }
            return returnValue;
        }
        #endregion

        #region 获取当前人按钮集合
        /// <summary>
        ///  获取当前人按钮集合
        /// </summary>        
        /// <param name="userId">用户id</param>
        /// <param name="menuId">按钮id</param>    
        /// <returns>是否具有权限</returns>
        public static List<string> GetAllButtonList(string projectId, string userId, string menuId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            List<string> buttonList = new List<string>();
            List<Model.Sys_ButtonToMenu> buttons = new List<Model.Sys_ButtonToMenu>();
            if (userId == Const.sedinId)
            {
                return buttonList;
            }
            if (userId == Const.sysglyId || userId == Const.hfnbdId)
            {
                buttons = (from x in db.Sys_ButtonToMenu
                           where x.MenuId == menuId
                           select x).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = BLL.UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        buttons = (from x in db.Sys_ButtonToMenu
                                   join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                   where y.RoleId == user.RoleId && y.MenuId == menuId && x.MenuId == menuId
                                   select x).ToList();
                    }
                }
                else
                {
                    var pUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                        buttons = (from x in db.Sys_ButtonToMenu
                                   join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                   where roleIdList.Contains(y.RoleId) && y.MenuId == menuId && x.MenuId == menuId
                                   select x).ToList();
                    }
                }
            }

            if (buttons.Count() > 0)
            {
                if (!CommonService.GetIsBuildUnit())
                {
                    var menu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
                    if (menu != null && menu.MenuType == BLL.Const.Menu_Resource)
                    {
                        for (int p = buttons.Count - 1; p > -1; p--)
                        {
                            if (buttons[p].ButtonName == BLL.Const.BtnSaveUp || buttons[p].ButtonName == BLL.Const.BtnUploadResources || buttons[p].ButtonName == BLL.Const.BtnAuditing)
                            {
                                buttons.Remove(buttons[p]);
                            }
                        }
                    }
                }

                buttonList = buttons.Select(x => x.ButtonName).ToList();
            }

            if (!String.IsNullOrEmpty(projectId) && menuId != BLL.Const.ProjectShutdownMenuId)
            {
                var porject = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_2 || porject.ProjectState == BLL.Const.ProjectState_3))
                {
                    buttonList.Clear();
                }
            }
            return buttonList;
        }
        #endregion

        #region 获取当前人是否具有按钮操作权限
        /// <summary>
        /// 获取当前人是否具有按钮操作权限
        /// </summary>        
        /// <param name="userId">用户id</param>
        /// <param name="menuId">按钮id</param>
        /// <param name="buttonName">按钮名称</param>
        /// <returns>是否具有权限</returns>
        public static bool GetAllButtonPowerList(string projectId, string userId, string menuId, string buttonName)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isPower = false;    ////定义是否具备按钮权限    
            if (userId == Const.sedinId)
            {
                return isPower;
            }
            if (!isPower && (userId == Const.sysglyId || userId == Const.hfnbdId))
            {
                isPower = true;
            }
            // 根据角色判断是否有按钮权限
            if (!isPower)
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = UserService.GetUserByUserId(userId); ////用户            
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            var buttonToMenu = from x in db.Sys_ButtonToMenu
                                               join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                               join z in db.Sys_Menu on x.MenuId equals z.MenuId
                                               where y.RoleId == user.RoleId && y.MenuId == menuId
                                               && x.ButtonName == buttonName && x.MenuId == menuId
                                               select x;
                            if (buttonToMenu.Count() > 0)
                            {
                                isPower = true;
                            }
                        }
                    }
                }
                else
                {
                    var pUser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ///项目用户
                    if (pUser != null)
                    {
                        if (!string.IsNullOrEmpty(pUser.RoleId))
                        {
                            List<string> roleIdList = Funs.GetStrListByStr(pUser.RoleId, ',');
                            var buttonToMenu = from x in db.Sys_ButtonToMenu
                                               join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                               join z in db.Sys_Menu on x.MenuId equals z.MenuId
                                               where roleIdList.Contains(y.RoleId) && y.MenuId == menuId
                                               && x.ButtonName == buttonName && x.MenuId == menuId
                                               select x;
                            if (buttonToMenu.Count() > 0)
                            {
                                isPower = true;
                            }
                        }
                    }
                }
            }

            if (isPower && !String.IsNullOrEmpty(projectId) && menuId != BLL.Const.ProjectShutdownMenuId)
            {
                var porject = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (porject != null && (porject.ProjectState == BLL.Const.ProjectState_2 || porject.ProjectState == BLL.Const.ProjectState_3))
                {
                    isPower = false;
                }
            }
            if (!CommonService.GetIsBuildUnit())
            {
                var menu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
                if (menu != null && menu.MenuType == BLL.Const.Menu_Resource)
                {
                    if (buttonName == BLL.Const.BtnSaveUp || buttonName == BLL.Const.BtnUploadResources || buttonName == BLL.Const.BtnAuditing)
                    {
                        isPower = false;
                    }
                }
            }
            return isPower;
        }
        #endregion
        
        #region 根据用户Id判断是否为本单位用户或管理员
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsMainUnitOrAdmin(string userId)
        {
            bool result = false;
            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                result = true;
            }
            else
            {
                var user = UserService.GetUserByUserId(userId);
                if (user != null)
                {
                    Model.Base_Unit unit = UnitService.GetUnitByUnitId(user.UnitId);
                    if (unit != null && unit.IsThisUnit == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 根据用户ID判断是否 管理角色、领导角色 且是本单位用户
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsThisUnitLeaderOrManage(string userId)
        {
            bool result = false;
            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                result = true;
            }
            else
            {
                bool isRoleType = false;
                bool isThisUnit = false;
                var user = BLL.UserService.GetUserByUserId(userId);
                if (user != null && user.IsOffice == true)
                {
                    var role = BLL.RoleService.GetRoleByRoleId(user.RoleId);
                    if (role != null && (role.RoleType == "3" || role.RoleType == "2") ) ////是管理、领导角色
                    {
                        isRoleType = true;
                    }

                    var unit = UnitService.GetUnitByUnitId(user.UnitId);
                    if (unit != null)
                    {
                        if (unit.IsThisUnit == true)
                        {
                            isThisUnit = true;
                        }
                        else
                        {
                            isThisUnit = false;
                        }
                    }
                }

                if (isRoleType && isThisUnit)
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion

        #region 根据用户ID判断是否 管理角色、领导角色 
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsLeaderOrManage(string userId)
        {
            bool result = false;
            if (userId == Const.sysglyId || userId == Const.hfnbdId || userId == Const.sedinId)
            {
                result = true;
            }
            else
            {
                var user = UserService.GetUserByUserId(userId);
                if (user != null && user.IsOffice == true)
                {
                    var role = RoleService.GetRoleByRoleId(user.RoleId);
                    if (role != null && (role.RoleType == "3" || role.RoleType == "2")) ////是管理、领导角色
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
        #endregion

        #region 根据项目Id返回对应的所属单位及上级UnitIdLists
        /// <summary>
        /// 根据项目Id返回对应的所属单位及上级UnitIdLists
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUnitIdListsByProject(Model.Base_Project project)
        {
            List<string> unitIdList = new List<string>();
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.UnitId))
                {
                    unitIdList.Add(project.UnitId);
                    List<string> supUnitId = GetSupUnitId(project.UnitId);
                    if (supUnitId.Count > 0)
                    {
                        unitIdList.AddRange(supUnitId);
                    }
                }
            }
            return unitIdList;
        }
        #endregion

        #region 根据UnitId返回对应的SupUnitId
        /// <summary>
        /// 根据用户UnitId返回对应的UnitId
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSupUnitId(string unitId)
        {
            List<string> unitIdList = new List<string>();
            if (!string.IsNullOrEmpty(unitId))
            {
               var unit = Funs.DB.Base_Unit.FirstOrDefault(e => e.UnitId == unitId);  //本单位
                if (unit != null && !string.IsNullOrEmpty(unit.SupUnitId))
                {
                    unitIdList.Add(unit.SupUnitId);
                    GetSupUnitId(unit.SupUnitId);
                }
            }
            return unitIdList;
        }
        #endregion

        #region 根据UnitId返回对应的SupUnitId
        /// <summary>
        /// 根据用户UnitId返回对应的UnitId
        /// </summary>
        /// <returns></returns>
        public static List<string> GetChildrenUnitId(string unitId)
        {
            List<string> unitIdList = new List<string>();
            if (!string.IsNullOrEmpty(unitId))
            {
                var unit = Funs.DB.Base_Unit.FirstOrDefault(e => e.SupUnitId == unitId);  //本单位
                if (unit != null)
                {
                    unitIdList.Add(unit.UnitId);
                    GetSupUnitId(unit.UnitId);
                }
            }
            return unitIdList;
        }
        #endregion

        #region 根据用户UnitId返回对应的UnitId
        /// <summary>
        /// 根据用户UnitId返回对应的UnitId
        /// </summary>
        /// <returns></returns>
        public static string GetUnitId(string unitId)
        {
            string id = unitId;
            if (string.IsNullOrEmpty(unitId))
            {
                Model.Base_Unit unit = Funs.DB.Base_Unit.FirstOrDefault(e => e.IsThisUnit == true);  //本单位
                if (unit != null)
                {
                    id = unit.UnitId;
                }
            }
            return id;
        }
        #endregion

        #region 得到本单位信息
        /// <summary>
        /// 得到本单位信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Unit GetIsThisUnit()
        {
            return Funs.DB.Base_Unit.FirstOrDefault(e => e.IsThisUnit == true);  //本单位
        }

        /// <summary>
        /// 得到本单位信息
        /// </summary>
        /// <returns></returns>
        public static string GetIsThisUnitId()
        {
            string unitId = string.Empty;
            var unit= Funs.DB.Base_Unit.FirstOrDefault(e => e.IsThisUnit == true);  //本单位
            if (unit != null)
            {
                unitId = unit.UnitId;
            }
            return unitId;
        }
        #endregion

        #region 判断UnitId是否本单位
        /// <summary>
        /// 判断UnitId是否本单位
        /// </summary>
        /// <returns></returns>
        public static bool GetIsThisUnit(string unitId)
        {
            bool isU = false;
            //本单位
            var unitThis = Funs.DB.Base_Unit.FirstOrDefault(e => e.IsThisUnit == true && e.UnitId == unitId);
            if (unitThis != null)
            {
                isU = true;
            }
            return isU;  
        }
        #endregion

        #region 根据用户UnitId返回是否 化学内置单位
        /// <summary>
        /// 根据用户UnitId返回对应的UnitId
        /// </summary>
        /// <returns></returns>
        public static bool GetIsBuildUnit()
        {
            bool isThis = false;
            var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.IsThisUnit == true && x.IsBuild == true);
            if (unit != null)
            {
                isThis = true;
            }
            return isThis;
        }
        #endregion

        /// <summary>
        ///根据主键删除附件
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAttachFileById(string id)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.AttachFile attachFile = db.AttachFile.FirstOrDefault(e => e.ToKeyId == id);
                if (attachFile != null)
                {
                    if (!string.IsNullOrEmpty(attachFile.AttachUrl))
                    {
                        UploadFileService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                    }

                    db.AttachFile.DeleteOnSubmit(attachFile);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        ///根据主键删除附件
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAttachFileById(string menuId,string id)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.AttachFile attachFile = db.AttachFile.FirstOrDefault(e =>e.MenuId == menuId && e.ToKeyId == id);
            if (attachFile != null)
            {
                if (!string.IsNullOrEmpty(attachFile.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                }

                db.AttachFile.DeleteOnSubmit(attachFile);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除流程
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteFlowOperateByID(string id)
        {
            var flowOperateList = from x in Funs.DB.Sys_FlowOperate where x.DataId == id select x;
            if (flowOperateList.Count() > 0)
            {
                BLL.SafetyDataItemService.DeleteSafetyDataItemByID(id); // 删除安全资料项
                Funs.DB.Sys_FlowOperate.DeleteAllOnSubmit(flowOperateList);
                Funs.DB.SubmitChanges();
            }
        }

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public static void btnSaveData(string projectId, string menuId, string dataId, string userId, bool isClosed, string content, string url)
        {
            Model.Sys_FlowOperate newFlowOperate = new Model.Sys_FlowOperate
            {
                MenuId = menuId,
                DataId = dataId,
                OperaterId = userId,
                State = Const.State_2,
                IsClosed = isClosed,
                Opinion = "系统自动关闭流程",
                ProjectId = projectId,
                Url = url
            };
            var user = BLL.UserService.GetUserByUserId(newFlowOperate.OperaterId);
            if (user != null)
            {
                var roles = BLL.RoleService.GetRoleByRoleId(user.RoleId);
                if (roles != null && !string.IsNullOrEmpty(roles.RoleName))
                {
                    newFlowOperate.AuditFlowName = "[" + roles.RoleName + "]";
                }
                else
                {
                    newFlowOperate.AuditFlowName = "[" + user.UserName + "]";
                }

                newFlowOperate.AuditFlowName += "系统审核完成";
            }

            var updateFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                    where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                    select x;
            if (updateFlowOperate.Count() > 0)
            {
                foreach (var item in updateFlowOperate)
                {
                    item.OperaterId = newFlowOperate.OperaterId;
                    item.OperaterTime = System.DateTime.Now;
                    item.State = newFlowOperate.State;
                    item.Opinion = newFlowOperate.Opinion;
                    item.AuditFlowName = "系统审核完成";
                    item.IsClosed = newFlowOperate.IsClosed;
                    Funs.DB.SubmitChanges();
                }
            }
            else
            {
                int maxSortIndex = 1;
                var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                if (sortIndex.HasValue)
                {
                    maxSortIndex = sortIndex.Value + 1;
                }
                newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate));
                newFlowOperate.SortIndex = maxSortIndex;
                newFlowOperate.OperaterTime = System.DateTime.Now;
                newFlowOperate.AuditFlowName = "系统审核完成";
                Funs.DB.Sys_FlowOperate.InsertOnSubmit(newFlowOperate);
                Funs.DB.SubmitChanges();
            }

            if (newFlowOperate.IsClosed == true)
            {
                var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                                where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                                select x;
                if (updateNoClosedFlowOperate.Count() > 0)
                {
                    foreach (var itemClosed in updateNoClosedFlowOperate)
                    {
                        itemClosed.IsClosed = true;
                        Funs.DB.SubmitChanges();
                    }
                }

                ////判断单据是否 加入到企业管理资料
                var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                if (safeData != null)
                {
                    BLL.SafetyDataService.AddSafetyData(menuId, dataId, content, url, projectId);
                }

                /// 在单据审核完成后 收集工程师日志 在单据中取值
                CollectHSSELogByData(newFlowOperate.ProjectId, newFlowOperate.MenuId, newFlowOperate.DataId);
                ///单据审核时 收集工程师日志 审核流程中取值
                CollectHSSELog(newFlowOperate.ProjectId, newFlowOperate.MenuId, newFlowOperate.DataId, newFlowOperate.OperaterId, newFlowOperate.OperaterTime);
            }
        }
        #endregion

        #region 收集工程师日志 审核流程中取值
        /// <summary>
        /// 收集工程师日志记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">单据id</param>
        /// <param name="operaterId">操作人id</param>
        /// <param name="date">日期</param>
        public static void CollectHSSELog(string projectId, string menuId, string dataId, string operaterId, DateTime? date)
        {
            switch (menuId)
            {
                case BLL.Const.ProjectRectifyNoticeMenuId:
                    var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(dataId);
                    if (rectifyNotice != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "22", rectifyNotice.WrongContent, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectLicenseManagerMenuId:
                    var license = BLL.LicenseManagerService.GetLicenseManagerById(dataId);
                    if (license != null)
                    {
                        var licenseType = BLL.LicenseTypeService.GetLicenseTypeById(license.LicenseTypeId);
                        if (licenseType != null)
                        {
                            BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "23", licenseType.LicenseTypeName, Const.BtnAdd, 1);
                        }
                    }
                    break;
                case BLL.Const.ProjectEquipmentSafetyListMenuId:
                    var equipment = BLL.EquipmentSafetyListService.GetEquipmentSafetyListById(dataId);
                    if (equipment != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "24", equipment.EquipmentSafetyListName, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectHazardListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "25", "职业健康安全危险源辨识与评价", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectEnvironmentalRiskListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "25", "环境危险源辨识与评价", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectDrillRecordListMenuId:
                    var drillRecord = BLL.DrillRecordListService.GetDrillRecordListById(dataId);
                    if (drillRecord != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "26", "应急演练:" + drillRecord.DrillRecordName, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectTrainRecordMenuId:
                    var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(dataId);
                    if (trainRecord != null)
                    {
                        var details = BLL.EduTrain_TrainRecordDetailService.GetTrainRecordDetailByTrainingId(dataId);
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "27", trainRecord.TrainContent, Const.BtnAdd, details.Count());
                    }
                    break;
                case BLL.Const.ProjectWeekMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "周例会", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectMonthMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "月例会", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectSpecialMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "专题会议", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectAttendMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "其他会议", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectPromotionalActivitiesMenuId:
                    var promotionalActivities = BLL.PromotionalActivitiesService.GetPromotionalActivitiesById(dataId);
                    if (promotionalActivities != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "29", promotionalActivities.Title, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectIncentiveNoticeMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "210", "奖励通知单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectPunishNoticeMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "211", "处罚通知单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectActionPlanListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "31", "施工HSE实施计划", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ActionPlan_ManagerRuleMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "31", "项目现场HSE教育管理规定", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectConstructSolutionMenuId:
                    var solution = BLL.ConstructSolutionService.GetConstructSolutionById(dataId);
                    if (solution != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "32", solution.ConstructSolutionName, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectLargerHazardListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "32", "危险性较大的工程清单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectCostSmallDetailMenuId:
                    var costSmallDetail = BLL.CostSmallDetailService.GetCostSmallDetailById(dataId);
                    if (costSmallDetail != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "33", "审核" + BLL.UnitService.GetUnitNameByUnitId(costSmallDetail.UnitId) + "的费用申请", Const.BtnAdd, 1);
                    }
                    break;
            }
        }

        #endregion

        #region 收集工程师日志 在单据中取值
        /// <summary>
        /// 收集工程师日志记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">单据id</param>
        /// <param name="operaterId">操作人id</param>
        /// <param name="date">日期</param>
        public static void CollectHSSELogByData(string projectId, string menuId, string dataId)
        {
            switch (menuId)
            {
                case BLL.Const.ProjectCheckDayMenuId:
                    var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(dataId);
                    if (checkDay != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkDay.CheckPerson, checkDay.CheckTime, "21", "日常巡检", Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectCheckSpecialMenuId:
                    var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(dataId);
                    if (checkSpecial != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkSpecial.CheckPerson, checkSpecial.CheckTime, "21", "专项检查", Const.BtnAdd, 1);
                        if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                        {
                            List<string> partInPersonIds = Funs.GetStrListByStr(checkSpecial.PartInPersonIds, ',');
                            foreach (var item in partInPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkSpecial.CheckTime, "21", "专项检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckColligationMenuId:
                    var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(dataId);
                    if (checkColligation != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkColligation.CheckPerson, checkColligation.CheckTime, "21", "综合检查", Const.BtnAdd, 1);
                        if (!string.IsNullOrEmpty(checkColligation.PartInPersonIds))
                        {
                            List<string> partInPersonIds = Funs.GetStrListByStr(checkColligation.PartInPersonIds, ',');
                            foreach (var item in partInPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkColligation.CheckTime, "21", "综合检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckWorkMenuId:
                    var checkWork = BLL.Check_CheckWorkService.GetCheckWorkByCheckWorkId(dataId);
                    if (checkWork != null)
                    {
                        if (!string.IsNullOrEmpty(checkWork.MainUnitPerson))
                        {
                            List<string> mainUnitPersonIds = Funs.GetStrListByStr(checkWork.MainUnitPerson, ',');
                            foreach (var item in mainUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkWork.CheckTime, "21", "开工前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(checkWork.SubUnitPerson))
                        {
                            List<string> subUnitPersonIds = Funs.GetStrListByStr(checkWork.SubUnitPerson, ',');
                            foreach (var item in subUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkWork.CheckTime, "21", "开工前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckHolidayMenuId:
                    var checkHoliday = BLL.Check_CheckHolidayService.GetCheckHolidayByCheckHolidayId(dataId);
                    if (checkHoliday != null)
                    {
                        if (!string.IsNullOrEmpty(checkHoliday.MainUnitPerson))
                        {
                            List<string> mainUnitPersonIds = Funs.GetStrListByStr(checkHoliday.MainUnitPerson, ',');
                            foreach (var item in mainUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkHoliday.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(checkHoliday.SubUnitPerson))
                        {
                            List<string> subUnitPersonIds = Funs.GetStrListByStr(checkHoliday.SubUnitPerson, ',');
                            foreach (var item in subUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkHoliday.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
            }
        }

        #endregion
    }
}