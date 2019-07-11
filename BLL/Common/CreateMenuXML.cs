using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BLL
{
    public static class CreateMenuXML
    {
        /// <summary>
        /// 取菜单创建XMl
        /// </summary>
        /// <param name="menuType">菜单类型</param>
        /// <param name="menuModel">菜单模式</param>
        public static void getMenuXML(string menuType,string menuModel)
        {
            var unit = BLL.CommonService.GetIsThisUnit(); ///得到当前单位
            if (unit != null)
            {               
                string unitId = unit.UnitId;
                List<Model.SpSysMenuItem> menusNewList = new List<Model.SpSysMenuItem>();
                var sysMenus = from x in Funs.DB.Sys_Menu
                               where x.UnitId == unitId && x.IsUsed == true && x.MenuType == menuType
                               select x;                
                if (sysMenus.Count() > 0) ///说明当前单位已设置了菜单
                {
                    var menusList = from x in sysMenus
                                    select new Model.SpSysMenuItem
                                    {
                                        MenuId = x.MenuId,
                                        MenuName = x.MenuName,
                                        Icon = x.Icon,
                                        Url = x.Url,
                                        SortIndex = x.SortIndex,
                                        SuperMenu = x.SuperMenu,
                                        MenuType = x.MenuType,
                                        IsEnd = x.IsEnd,
                                        IsUsed = x.IsUsed,
                                    };

                    menusNewList = menusList.ToList();

                }
                #region  ////正常设置情况下 进程不会走到下面的步骤
                else   ///说明当前单位未设置菜单，则取通用菜单
                {
                    if (menuType != BLL.Const.Menu_Project)   ///不是项目菜单时
                    {
                        var menuCommon = from x in Funs.DB.Sys_MenuCommon
                                         where x.MenuType == menuType && (x.IsUnit == false || !x.IsUnit.HasValue)
                                         select new Model.SpSysMenuItem
                             {
                                 MenuId = x.MenuId,
                                 MenuName = x.MenuName,
                                 Icon = x.Icon,
                                 Url = x.Url,
                                 SortIndex = x.SortIndex,
                                 SuperMenu = x.SuperMenu,
                                 MenuType = x.MenuType,
                                 IsEnd = x.IsEnd,
                                 IsUsed = true,
                             };
                       
                        var menuCommonIsUnit = from x in Funs.DB.Sys_MenuCommon
                                               join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                               where x.MenuType == menuType && x.IsUnit == true && y.UnitId == unitId
                                               select new Model.SpSysMenuItem
                             {
                                 MenuId = x.MenuId,
                                 MenuName = x.MenuName,
                                 Icon = x.Icon,
                                 Url = x.Url,
                                 SortIndex = x.SortIndex,
                                 SuperMenu = x.SuperMenu,
                                 MenuType = x.MenuType,
                                 IsEnd = x.IsEnd,
                                 IsUsed = true,
                             };

                        menusNewList = menuCommon.Union(menuCommonIsUnit).ToList();
                       
                    }
                    else ///如果是项目菜单时
                    {
                        if (menuModel == "A") ///项目菜单A模式
                        {
                            var menuProjectA = from x in Funs.DB.Sys_MenuProjectA
                                             where x.MenuType == menuType && (x.IsUnit == false || !x.IsUnit.HasValue)
                                             select new Model.SpSysMenuItem
                                             {
                                                 MenuId = x.MenuId,
                                                 MenuName = x.MenuName,
                                                 Icon = x.Icon,
                                                 Url = x.Url,
                                                 SortIndex = x.SortIndex,
                                                 SuperMenu = x.SuperMenu,
                                                 MenuType = x.MenuType,
                                                 IsEnd = x.IsEnd,
                                                 IsUsed = true,
                                             };

                            var menuProjectAIsUnit = from x in Funs.DB.Sys_MenuProjectA
                                                   join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                                   where x.MenuType == menuType && x.IsUnit == true && y.UnitId == unitId
                                                   select new Model.SpSysMenuItem
                                                   {
                                                       MenuId = x.MenuId,
                                                       MenuName = x.MenuName,
                                                       Icon = x.Icon,
                                                       Url = x.Url,
                                                       SortIndex = x.SortIndex,
                                                       SuperMenu = x.SuperMenu,
                                                       MenuType = x.MenuType,
                                                       IsEnd = x.IsEnd,
                                                       IsUsed = true,
                                                   };

                            menusNewList = menuProjectA.Union(menuProjectAIsUnit).ToList();
                        }
                        else ///项目菜单B模式
                        {
                            var menuProjectB = from x in Funs.DB.Sys_MenuProjectB
                                               where x.MenuType == menuType && (x.IsUnit == false || !x.IsUnit.HasValue)
                                               select new Model.SpSysMenuItem
                                               {
                                                   MenuId = x.MenuId,
                                                   MenuName = x.MenuName,
                                                   Icon = x.Icon,
                                                   Url = x.Url,
                                                   SortIndex = x.SortIndex,
                                                   SuperMenu = x.SuperMenu,
                                                   MenuType = x.MenuType,
                                                   IsEnd = x.IsEnd,
                                                   IsUsed = true,
                                               };

                            var menuProjectBIsUnit = from x in Funs.DB.Sys_MenuProjectB
                                                     join y in Funs.DB.Sys_MenuUnit on x.MenuId equals y.MenuId
                                                     where x.MenuType == menuType && x.IsUnit == true && y.UnitId == unitId
                                                     select new Model.SpSysMenuItem
                                                     {
                                                         MenuId = x.MenuId,
                                                         MenuName = x.MenuName,
                                                         Icon = x.Icon,
                                                         Url = x.Url,
                                                         SortIndex = x.SortIndex,
                                                         SuperMenu = x.SuperMenu,
                                                         MenuType = x.MenuType,
                                                         IsEnd = x.IsEnd,
                                                         IsUsed = true,
                                                     };

                            menusNewList = menuProjectB.Union(menuProjectBIsUnit).ToList();
                        }
                    }
                }
                #endregion
                var EMenuList = menusNewList.Where(x => x.SuperMenu == BLL.Const.ProjectSafetyDataESuperMenuId || x.MenuId == BLL.Const.ProjectSafetyDataESuperMenuId
                || x.MenuId == Const.MenuProjectB_BuildingMenuId || x.MenuId == Const.ProjectSetMenuId || x.MenuId == Const.ProjectShutdownMenuId
                || x.MenuId == Const.ProjectUnitMenuId || x.MenuId == Const.ProjectUserMenuId).ToList();

                menusNewList = menusNewList.Where(x => x.SuperMenu != BLL.Const.ProjectSafetyDataESuperMenuId && x.MenuId != BLL.Const.ProjectSafetyDataESuperMenuId).ToList();
                CreateMenuDataXML(menuType, menusNewList, "0", null);
                if (menuType == BLL.Const.Menu_Project)
                {
                    CreateMenuDataXML(BLL.Const.Menu_EProject, EMenuList, "0", null);
                }
            }
        }

        #region 创建菜单信息XML方法
        /// <summary>
        /// 创建菜单信息XML方法
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateMenuDataXML(string menuType, List<Model.SpSysMenuItem> menusList, string superMenu, XmlTextWriter writer)
        {           
            try
            {
                if (superMenu == "0")
                {
                    ///xml文件路径名
                    string fileName = Funs.RootPath + "common\\" + menuType + ".xml";
                    FileStream fileStream = new FileStream(fileName, FileMode.Create);
                    writer = new XmlTextWriter(fileStream, Encoding.UTF8)
                    {
                        //使用自动缩进便于阅读
                        Formatting = Formatting.Indented
                    };
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Tree");    //创建父节点
                    var menuItemList = menusList.Where(x => x.SuperMenu == superMenu).OrderBy(x=>x.SortIndex);    //获取菜单列表
                    if (menuItemList.Count() > 0)
                    {
                        foreach (var item in menuItemList)
                        {
                            writer.WriteStartElement("TreeNode");    //创建子节点
                            writer.WriteAttributeString("id", item.MenuId);    //添加属性
                            string name = item.MenuName;
                            var isUnit = BLL.CommonService.GetIsThisUnit();
                            if (isUnit != null && isUnit.UnitId == BLL.Const.UnitId_CWCEC && name.Contains("HSSE")) //五环
                            {
                                name = name.Replace("HSSE","HSE");
                            }
                            else
                            {
                                if (!name.Contains("月总结"))
                                {
                                    name = name.Replace("HSSE", "安全");
                                }
                            }
                            writer.WriteAttributeString("Text", name);
                            writer.WriteAttributeString("NavigateUrl", item.Url);
                            if (!string.IsNullOrEmpty(item.Icon))
                            {
                                writer.WriteAttributeString("Icon", item.Icon);
                            }else
                            {
                                writer.WriteAttributeString("Icon", "LayoutContent");
                            }
                            if (!item.IsEnd.HasValue || item.IsEnd == false)
                            {
                                CreateMenuDataXML(menuType, menusList, item.MenuId, writer);
                            }
                            writer.WriteFullEndElement();    //子节点结束
                            //在节点间添加一些空格
                            writer.WriteWhitespace("\n");
                        }
                    }
                    writer.WriteFullEndElement();    //父节点结束
                    writer.Close();
                    fileStream.Close();
                }
                else
                {
                    var subMenuItemList = menusList.Where(x => x.SuperMenu == superMenu).OrderBy(x => x.SortIndex);    //获取菜单集合
                    if (subMenuItemList.Count() > 0)
                    {
                        foreach (var item in subMenuItemList)
                        {
                            //使用自动缩进便于阅读
                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartElement("TreeNode");    //创建子节点
                            writer.WriteAttributeString("id", item.MenuId);    //添加属性
                            string name = item.MenuName;
                            var isUnit = BLL.CommonService.GetIsThisUnit();
                            if (isUnit != null && isUnit.UnitId == BLL.Const.UnitId_CWCEC && name.Contains("HSSE"))
                            {
                                name = name.Replace("HSSE", "HSE");
                            }
                            else
                            {
                                if (!name.Contains("月总结"))
                                {
                                    name = name.Replace("HSSE", "安全");
                                }
                            }
                            //if (BLL.SafetyDataService.GetSafetyDataByMenuId(item.MenuId) != null)
                            //{
                            //    writer.WriteAttributeString("Text", name + "*");
                            //}
                            //else
                            //{
                                writer.WriteAttributeString("Text", name);
                            //}
                            writer.WriteAttributeString("NavigateUrl", item.Url);
                            if (!string.IsNullOrEmpty(item.Icon))
                            {
                                writer.WriteAttributeString("Icon", item.Icon);
                            }
                            else
                            {
                                writer.WriteAttributeString("Icon", "LayoutContent");
                            }
                            if (!item.IsEnd.HasValue || item.IsEnd == false)
                            {
                                CreateMenuDataXML(menuType, menusList, item.MenuId, writer);
                            }
                            writer.WriteFullEndElement();    //子节点结束
                            //在节点间添加一些空格
                            writer.WriteWhitespace("\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
        }
        #endregion
    }
}