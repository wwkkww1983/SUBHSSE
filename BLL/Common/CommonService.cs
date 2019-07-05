using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CommonService
    {
        #region 根据登陆id菜单id判断是否有权限
        /// <summary>
        /// 根据登陆id菜单id判断是否有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ReturnMenuByUserIdMenuId(string userId, string menuId, string projectId)
        {
            bool returnValue = false;
            var menu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (menu != null)
            {   
                ///1、当前用户是管理员 
                ///2、当前菜单是个人设置 资源库
                if (userId == Const.sysglyId || userId == BLL.Const.hfnbdId || menu.MenuType == BLL.Const.Menu_Personal || menu.MenuType == BLL.Const.Menu_Resource)
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
                    }
                }
                else
                {
                    ///3、管理角色、领导角色能访问项目菜单
                    bool isLeaderManage = IsThisUnitLeaderOrManage(userId);
                    if (isLeaderManage && menu.MenuType == BLL.Const.Menu_Project)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        var puser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, userId); ////用户
                        if (puser != null && !string.IsNullOrEmpty(puser.RoleId))
                        {
                            var power = Funs.DB.Sys_RolePower.FirstOrDefault(x => x.MenuId == menuId && x.RoleId == puser.RoleId);
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
            if (userId == Const.sysglyId || userId == BLL.Const.hfnbdId)
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
                        buttons = (from x in db.Sys_ButtonToMenu
                                   join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                   where y.RoleId == pUser.RoleId && y.MenuId == menuId && x.MenuId == menuId
                                   select x).ToList();
                    }
                }
            }

            if (buttons.Count() > 0)
            {        
                if (!BLL.CommonService.GetIsBuildUnit())
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
            if (!isPower && (userId == Const.sysglyId || userId == BLL.Const.hfnbdId))
            {
                isPower = true;
            }
            // 根据角色判断是否有按钮权限
            if (!isPower)
            {
                if (string.IsNullOrEmpty(projectId))
                {
                    var user = BLL.UserService.GetUserByUserId(userId); ////用户            
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
                            var buttonToMenu = from x in db.Sys_ButtonToMenu
                                               join y in db.Sys_ButtonPower on x.ButtonToMenuId equals y.ButtonToMenuId
                                               join z in db.Sys_Menu on x.MenuId equals z.MenuId
                                               where y.RoleId == pUser.RoleId && y.MenuId == menuId
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
            if (!BLL.CommonService.GetIsBuildUnit())
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
        
        #region 根据用户UnitId判断是否为本单位用户或管理员
        /// <summary>
        /// 根据用户UnitId判断是否为本单位用户或管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsMainUnitOrAdmin(string userId)
        {
            bool result = false;
            if (userId == BLL.Const.sysglyId || userId == BLL.Const.hfnbdId)
            {
                result = true;
            }
            else
            {
                var user = BLL.UserService.GetUserByUserId(userId);
                if (user != null)
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(user.UnitId);
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
            if (userId == BLL.Const.sysglyId || userId == BLL.Const.hfnbdId)
            {
                result = true;
            }
            else
            {
                bool isRoleType = false;
                bool isThisUnit = false;
                var user = BLL.UserService.GetUserByUserId(userId);
                if (user != null)
                {
                    var role = BLL.RoleService.GetRoleByRoleId(user.RoleId);
                    if (role != null && (role.RoleType == "3" || role.RoleType == "2")) ////是管理、领导角色
                    {
                        isRoleType = true;
                    }

                    if (user.IsOffice == true)
                    {
                        isRoleType = true;
                    }

                    var unit = BLL.UnitService.GetUnitByUnitId(user.UnitId);
                    if (unit != null)
                    {
                        if (unit.IsThisUnit == true)
                        {
                            isThisUnit = true;
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
            return (Funs.DB.Base_Unit.FirstOrDefault(e => e.IsThisUnit == true));  //本单位
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
            Model.SUBHSSEDB db = Funs.DB;
            Model.AttachFile attachFile = db.AttachFile.FirstOrDefault(e => e.ToKeyId == id);
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
                Funs.DB.Sys_FlowOperate.DeleteAllOnSubmit(flowOperateList);
                Funs.DB.SubmitChanges();
            }
        }
    }
}