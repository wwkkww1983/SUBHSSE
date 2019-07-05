using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
   public static class SysMenuService
    {
       public static Model.SUBHSSEDB db = Funs.DB;

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetSupMenuListBySuperMenu(string superMenu)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.SuperMenu == superMenu orderby x.SortIndex select x).ToList();          
           return list;
       }

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_Menu GetSupMenuBySuperMenu(string superMenu)
       {
           return Funs.DB.Sys_Menu.FirstOrDefault(x => x.SuperMenu == superMenu);    
       }

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_Menu GetSysMenuByMenuId(string menuId)
       {
           return Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == menuId);
       }

       /// <summary>
       /// 根据MenuType获取菜单集合
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetMenuListByMenuType(string menuType)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.MenuType == menuType orderby x.SortIndex select x).ToList();
           return list;
       }

       /// <summary>
       /// 根据MenuType获取菜单集合
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetIsUsedMenuListByMenuType(string menuType)
       {
           var list = (from x in Funs.DB.Sys_Menu
                       join y in Funs.DB.Sys_MenuUsed on x.MenuId equals y.MenuId
                       where x.MenuType == menuType && y.IsUsed == true
                       orderby x.SortIndex select x).Distinct().ToList();
           return list;
       }

       /// <summary>
       /// 删除单位使用菜单
       /// </summary>
       /// <param name="roleId"></param>
       public static void DeleteSys_Menu(string unitId)
       {
           var sysMenus = from x in Funs.DB.Sys_Menu 
                         // where x.UnitId == unitId 
                          select x;
           if (sysMenus.Count() > 0)
           {
               foreach (var item in sysMenus)
               {
                   if (item.IsEnd == false || !item.IsEnd.HasValue)
                   {
                       BLL.ButtonPowerService.DeleteButtonPowerByMenuId(item.MenuId);
                   }

                   Funs.DB.Sys_Menu.DeleteOnSubmit(item);
                   Funs.DB.SubmitChanges();
               }
           }
       }
       
       #region 生成单位菜单方法
       /// <summary>
       /// 生成单位菜单方法
       /// </summary>
       /// <param name="unitId"></param>
       public static void SetSys_MenuData(string unitId, string menuModel)
       {
           DeleteSys_Menu(unitId);          
           var menuCommon = from x in Funs.DB.Sys_MenuCommon
                            where (x.IsUnit == false || !x.IsUnit.HasValue)
                            select x;
           var menuCommonIsUnit = from x in Funs.DB.Sys_MenuCommon
                                  join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                  where x.IsUnit == true && y.UnitId == unitId
                                  select x;
           var menuNew1 = menuCommon.Union(menuCommonIsUnit);
           if (menuNew1.Count() > 0)
           {
               foreach (var item in menuNew1)
               {
                    Model.Sys_Menu newMenu = new Model.Sys_Menu
                    {
                        MenuUsedId = SQLHelper.GetNewID(typeof(Model.Sys_Menu)),
                        MenuId = item.MenuId,
                        UnitId = unitId,
                        MenuName = item.MenuName,
                        Icon = item.Icon,
                        Url = item.Url,
                        SortIndex = item.SortIndex,
                        SuperMenu = item.SuperMenu,
                        MenuType = item.MenuType,
                        IsEnd = item.IsEnd,
                        IsUsed = getMenuIsUsed(item.MenuId), ///是否启用
                        ModifyDate = System.DateTime.Now
                    };
                    Funs.DB.Sys_Menu.InsertOnSubmit(newMenu);
                   Funs.DB.SubmitChanges();
               }
           }

           if (menuModel == "A")
           {
               var menuProjectA = from x in Funs.DB.Sys_MenuProjectA
                                  where (x.IsUnit == false || !x.IsUnit.HasValue)
                                  select x;
               var menuProjectAIsUnit = from x in Funs.DB.Sys_MenuProjectA
                                        join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                        where x.IsUnit == true && y.UnitId == unitId
                                        select x;
               var menuNew2 = menuProjectA.Union(menuProjectAIsUnit);
               if (menuNew2.Count() > 0)
               {
                   foreach (var item in menuNew2)
                   {
                        Model.Sys_Menu newMenu = new Model.Sys_Menu
                        {
                            MenuUsedId = SQLHelper.GetNewID(typeof(Model.Sys_Menu)),
                            MenuId = item.MenuId,
                            UnitId = unitId,
                            MenuName = item.MenuName,
                            Icon = item.Icon,
                            Url = item.Url,
                            SortIndex = item.SortIndex,
                            SuperMenu = item.SuperMenu,
                            MenuType = item.MenuType,
                            IsEnd = item.IsEnd,
                            IsUsed = getMenuIsUsed(item.MenuId), ///是否启用
                            ModifyDate = System.DateTime.Now
                        };
                        Funs.DB.Sys_Menu.InsertOnSubmit(newMenu);
                       Funs.DB.SubmitChanges();
                   }
               }
           }
           else
           {

               var menuProjectB = from x in Funs.DB.Sys_MenuProjectB
                                  where (x.IsUnit == false || !x.IsUnit.HasValue)
                                  select x;
               var menuProjectBIsUnit = from x in Funs.DB.Sys_MenuProjectB
                                        join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                        where x.IsUnit == true && y.UnitId == unitId
                                        select x;
               var menuNew3 = menuProjectB.Union(menuProjectBIsUnit);
               if (menuNew3.Count() > 0)
               {
                   foreach (var item in menuNew3)
                   {
                        Model.Sys_Menu newMenu = new Model.Sys_Menu
                        {
                            MenuUsedId = SQLHelper.GetNewID(typeof(Model.Sys_Menu)),
                            MenuId = item.MenuId,
                            UnitId = unitId,
                            MenuName = item.MenuName,
                            Icon = item.Icon,
                            Url = item.Url,
                            SortIndex = item.SortIndex,
                            SuperMenu = item.SuperMenu,
                            MenuType = item.MenuType,
                            IsEnd = item.IsEnd,
                            IsUsed = getMenuIsUsed(item.MenuId), ///是否启用
                            ModifyDate = System.DateTime.Now
                        };
                        Funs.DB.Sys_Menu.InsertOnSubmit(newMenu);
                       Funs.DB.SubmitChanges();
                   }
               }
           }
       }
       #endregion

       #region 菜单启用 相关方法
       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_MenuUsed GetSysMenuUsedByMenuId(string menuId)
       {
           return Funs.DB.Sys_MenuUsed.FirstOrDefault(x => x.MenuId == menuId);
       }

       /// <summary>
       /// 获取菜单是否启用标识
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static bool getMenuIsUsed(string menuId)
       {
           bool isUsed = false;
           var Sys_MenuUsed = Funs.DB.Sys_MenuUsed.FirstOrDefault(x => x.MenuId == menuId && x.IsUsed == true);
           if (Sys_MenuUsed != null)
           {
               isUsed = true;
           }

           return isUsed;
       }

       /// <summary>
       /// 添加菜单是否启用记录表
       /// </summary>
       /// <param name="menuUsed"></param>
       public static void AddMenuUsed(Model.Sys_MenuUsed menuUsed)
       {
           Model.SUBHSSEDB db = Funs.DB;
            Model.Sys_MenuUsed newMenuUsed = new Model.Sys_MenuUsed
            {
                MenuUsedId = SQLHelper.GetNewID(typeof(Model.Sys_MenuUsed)),
                MenuId = menuUsed.MenuId,
                MenuType = menuUsed.MenuType,
                IsUsed = menuUsed.IsUsed,
                ModifyDate = System.DateTime.Now
            };
            db.Sys_MenuUsed.InsertOnSubmit(newMenuUsed);
           db.SubmitChanges();
       }

       /// <summary>
       /// 清空菜单启用表
       /// </summary>
       public static void DeleteMenuUsedByMenuType(string menuType)
       {
           Model.SUBHSSEDB db = Funs.DB;
           var menuUsed = from x in db.Sys_MenuUsed where x.MenuType == menuType select x;
           if (menuUsed.Count() > 0)
           {
               db.Sys_MenuUsed.DeleteAllOnSubmit(menuUsed);
               db.SubmitChanges();
           }
       }
       #endregion
    }
}
