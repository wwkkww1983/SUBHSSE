using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class FileCabinetAService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据用户id获取文件柜主表列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.InformationProject_FileCabinetA> GetFileCabinetAListByProjectId(string projectId)
        {
            var FileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA 
                                   orderby x.Code
                                   where x.ProjectId == projectId select x;
            return FileCabinetAList.ToList();
        }

        /// <summary>
        /// 根据主键id获取文件柜
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.InformationProject_FileCabinetA GetFileCabinetAByID(string FileCabinetAId)
        {
            return Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.FileCabinetAId == FileCabinetAId);
        }

        /// <summary>
        /// 根据菜单id项目id获取文件柜
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.InformationProject_FileCabinetA GetFileCabinetAByMenuIdProjectId(string MenuId, string ProjectId)
        {
            return Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.MenuId == MenuId && x.ProjectId == ProjectId);
        }

        /// <summary>
        /// 添加文件柜
        /// </summary>
        /// <param name="FileCabinetA"></param>
        public static void AddFileCabinetA(Model.InformationProject_FileCabinetA FileCabinetA)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetA newFileCabinetA = new Model.InformationProject_FileCabinetA
            {
                FileCabinetAId = FileCabinetA.FileCabinetAId,
                ProjectId = FileCabinetA.ProjectId,
                MenuId = FileCabinetA.MenuId,
                Code = FileCabinetA.Code,
                Title = FileCabinetA.Title,
                SupFileCabinetAId = FileCabinetA.SupFileCabinetAId,
                IsEndLever = FileCabinetA.IsEndLever
            };
            db.InformationProject_FileCabinetA.InsertOnSubmit(newFileCabinetA);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改文件柜
        /// </summary>
        /// <param name="FileCabinetA"></param>
        public static void UpdateFileCabinetA(Model.InformationProject_FileCabinetA FileCabinetA)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetA newFileCabinetA = db.InformationProject_FileCabinetA.FirstOrDefault(e => e.FileCabinetAId == FileCabinetA.FileCabinetAId);
            if (newFileCabinetA != null)
            {
                newFileCabinetA.Code = FileCabinetA.Code;
                newFileCabinetA.Title = FileCabinetA.Title;
                newFileCabinetA.SupFileCabinetAId = FileCabinetA.SupFileCabinetAId;
                newFileCabinetA.IsEndLever = FileCabinetA.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="FileCabinetAId"></param>
        public static void DeleteFileCabinetAByID(string FileCabinetAId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_FileCabinetA FileCabinetA = db.InformationProject_FileCabinetA.FirstOrDefault(e => e.FileCabinetAId == FileCabinetAId);
            {
                db.InformationProject_FileCabinetA.DeleteOnSubmit(FileCabinetA);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string FileCabinetAId, string supFileCabinetAId, string title)
        {
            var q = Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.SupFileCabinetAId == supFileCabinetAId && x.Title == title
                    && x.FileCabinetAId != FileCabinetAId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否可删除节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteFileCabinetA(string FileCabinetAId)
        {
            bool isDelete = true;
            var FileCabinetA = GetFileCabinetAByID(FileCabinetAId);
            if (FileCabinetA != null)
            {
                if (FileCabinetA.IsEndLever == true)
                {
                    var detailCout = Funs.DB.InformationProject_FileCabinetAItem.FirstOrDefault(x => x.FileCabinetAId == FileCabinetAId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.SupFileCabinetAId == FileCabinetAId);
                    if (supItemSetCount != null)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        #region 页面信息添加到文件柜A
        #region 添加到文件柜A中方法
        /// <summary>
        /// 添加到文件柜A中方法
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        public static void AddFileCabinetA(string menuId, string dataId, string content, string url, string projectId)
        {
            var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByMenuIdProjectId(menuId, projectId);
            if (fileCabinetA != null)   ////判断这个菜单id 是否在文件柜A中 在则插入明细 不在则查找上一级菜单id
            {
                AddDataToFileCabinetAItem(dataId, content, fileCabinetA.FileCabinetAId, url);
            }
            else   ///增加改菜单到文件柜A主表 然后增加明细
            {
                AddDataToFileCabinetA(menuId, dataId, content, url, projectId);
            }
        }
        #endregion

        #region 添加到文件柜A中方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <param name="projectId"></param>
        public static void AddDataToFileCabinetA(string menuId, string dataId, string content, string url, string projectId)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu != null)
            {
                Model.InformationProject_FileCabinetA newFileCabinetA = new Model.InformationProject_FileCabinetA
                {
                    FileCabinetAId = SQLHelper.GetNewID(typeof(Model.InformationProject_FileCabinetA)),
                    ProjectId = projectId,
                    Code = sysMenu.SortIndex.ToString(),
                    Title = sysMenu.MenuName,
                    MenuId = menuId
                };
                if (menuId == "0")
                {
                    newFileCabinetA.SupFileCabinetAId = "0";
                }
                else
                {
                    newFileCabinetA.SupFileCabinetAId = null;
                }
                newFileCabinetA.IsEndLever = sysMenu.IsEnd;
                BLL.FileCabinetAService.AddFileCabinetA(newFileCabinetA);

                ///查询是否存在下级菜单
                var fileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA
                                       join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                                       where x.SupFileCabinetAId == null && x.ProjectId == projectId && y.SuperMenu == menuId
                                       select x;
                if (fileCabinetAList.Count() > 0)
                {
                    foreach (var item in fileCabinetAList)
                    {
                        item.SupFileCabinetAId = newFileCabinetA.FileCabinetAId;
                        BLL.FileCabinetAService.UpdateFileCabinetA(item);
                    }
                }
                if (sysMenu.IsEnd == true)  ///增加明细
                {
                    AddDataToFileCabinetAItem(dataId, content, newFileCabinetA.FileCabinetAId, url);
                }
                var fileCabinetASuper = BLL.FileCabinetAService.GetFileCabinetAByMenuIdProjectId(sysMenu.SuperMenu, projectId);
                if (fileCabinetASuper == null)  ///继续增加上级菜单
                {
                    AddDataToFileCabinetA(sysMenu.SuperMenu, dataId, content, url, projectId);
                }
                else
                {

                    var fileCabinetASuperList = from x in Funs.DB.InformationProject_FileCabinetA
                                                join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                                                where x.ProjectId == projectId && y.SuperMenu == fileCabinetASuper.MenuId && x.SupFileCabinetAId == null
                                                select x;
                    if (fileCabinetASuperList.Count() > 0)
                    {
                        foreach (var item in fileCabinetASuperList)
                        {
                            item.SupFileCabinetAId = fileCabinetASuper.FileCabinetAId;
                            BLL.FileCabinetAService.UpdateFileCabinetA(item);
                        }
                    }
                }
            }
            else
            {
                ///查询是否存在下级菜单
                var fileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA
                                       where x.SupFileCabinetAId == null && x.ProjectId == projectId
                                       select x;
                if (fileCabinetAList.Count() > 0)
                {
                    foreach (var item in fileCabinetAList)
                    {
                        item.SupFileCabinetAId = "0";
                        BLL.FileCabinetAService.UpdateFileCabinetA(item);
                    }
                }
            }
        }
        #endregion

        #region 增加文件柜明细
        /// <summary>
        ///  增加文件柜明细
        /// </summary>
        public static void AddDataToFileCabinetAItem(string dataId, string content, string fileCabinetAId, string url)
        {
            var fileCabinetAItem = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(dataId); ///明细是否存在
            if (fileCabinetAItem != null)
            {
                fileCabinetAItem.Title = content;
                BLL.FileCabinetAItemService.UpdateFileCabinetAItem(fileCabinetAItem);
            }
            else
            {
                Model.InformationProject_FileCabinetAItem newFileCabinetAItem = new Model.InformationProject_FileCabinetAItem
                {
                    FileCabinetAItemId = dataId,
                    FileCabinetAId = fileCabinetAId,
                    Code = BLL.CodeRecordsService.ReturnCodeByDataId(dataId),
                    Title = content,
                    CompileDate = System.DateTime.Now,
                    IsMenu = true,
                    Url = url
                };
                BLL.FileCabinetAItemService.AddFileCabinetAItem(newFileCabinetAItem);
            }
        }
        #endregion

        #endregion
    }
}
