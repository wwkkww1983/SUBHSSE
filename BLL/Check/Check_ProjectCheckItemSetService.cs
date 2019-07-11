using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目检查项
    /// </summary>
    public static class Check_ProjectCheckItemSetService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目检查项
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static Model.Check_ProjectCheckItemSet GetCheckItemSetById(string checkItemSetId)
        {
            return Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
        }


        /// <summary>
        /// 根据检测名称获取项目检查项
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static Model.Check_ProjectCheckItemSet GetCheckItemSetByCheckItemName(string checkItemName)
        {
            return Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemName == checkItemName);
        }

        /// <summary>
        /// 根据主键获取顶级检查项名称
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static string GetCheckItemNameBySupCheckItem(string supCheckItem)
        {
            string name = string.Empty;
            Model.Check_ProjectCheckItemSet checkItemSet = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemSetId == supCheckItem);
            if (checkItemSet != null)
            {
                if (checkItemSet.SupCheckItem == "0")
                {
                    name = checkItemSet.CheckItemName;
                }
                else
                {
                    name = GetCheckItemNameBySupCheckItem(checkItemSet.SupCheckItem);
                }
            }
            return name;
        }

        /// <summary>
        /// 获取一级节点检查类型
        /// </summary>
        /// <param name="CheckItem"></param>
        /// <returns></returns>
        public static string ConvertCheckItemType(object CheckItem)
        {
            string type = string.Empty;
            if (CheckItem != null)
            {
                Model.Check_ProjectCheckItemDetail detail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(CheckItem.ToString());
                if (detail != null)
                {
                    Model.Check_ProjectCheckItemSet item = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(detail.CheckItemSetId);
                    if (item != null)
                    {
                        if (item.SupCheckItem == "0")
                        {
                            type = item.CheckItemName;
                        }
                        else
                        {
                            type = BLL.Check_ProjectCheckItemSetService.GetCheckItemNameBySupCheckItem(item.SupCheckItem);
                        }
                    }
                }
                else
                {
                    Model.Check_ProjectCheckItemSet item = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(CheckItem.ToString());
                    if (item != null)
                    {
                        if (item.SupCheckItem == "0")
                        {
                            type = item.CheckItemName;
                        }
                        else
                        {
                            type = BLL.Check_ProjectCheckItemSetService.GetCheckItemNameBySupCheckItem(item.SupCheckItem);
                        }
                    }
                }
            }
            return type;
        }

        /// <summary>
        /// 根据上一节点id获取项目检查项
        /// </summary>
        /// <param name="supCheckItemSetId"></param>
        /// <returns></returns>
        public static List<Model.Check_ProjectCheckItemSet> GetCheckItemSetBySupCheckItemSetId(string supCheckItemSetId)
        {
            return (from x in Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == supCheckItemSetId select x).ToList();
        }

        /// <summary>
        /// 添加项目检查项
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void AddCheckItemSet(Model.Check_ProjectCheckItemSet checkItemSet)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemSet newCheckItemSet = new Model.Check_ProjectCheckItemSet
            {
                CheckItemSetId = checkItemSet.CheckItemSetId,
                ProjectId = checkItemSet.ProjectId,
                CheckItemName = checkItemSet.CheckItemName,
                SupCheckItem = checkItemSet.SupCheckItem,
                CheckType = checkItemSet.CheckType,
                MapCode = checkItemSet.MapCode,
                IsEndLever = checkItemSet.IsEndLever,
                SortIndex = checkItemSet.SortIndex,
                IsBuiltIn = checkItemSet.IsBuiltIn
            };
            db.Check_ProjectCheckItemSet.InsertOnSubmit(newCheckItemSet);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目检查项
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void UpdateCheckItemSet(Model.Check_ProjectCheckItemSet checkItemSet)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemSet newCheckItemSet = db.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSet.CheckItemSetId);
            if (newCheckItemSet != null)
            {
                newCheckItemSet.CheckItemName = checkItemSet.CheckItemName;
                newCheckItemSet.SupCheckItem = checkItemSet.SupCheckItem;
                newCheckItemSet.MapCode = checkItemSet.MapCode;
                newCheckItemSet.IsEndLever = checkItemSet.IsEndLever;
                newCheckItemSet.SortIndex = checkItemSet.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目检查项
        /// </summary>
        /// <param name="checkItemSetId"></param>
        public static void DeleteCheckItemSet(string checkItemSetId,string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemSet checkItemSet = db.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId && e.ProjectId == projectId);
            if (checkItemSet != null)
            {
                db.Check_ProjectCheckItemSet.DeleteOnSubmit(checkItemSet);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否末级
        /// </summary>
        /// <param name="checkItemSetId">项目检查项目主键</param>
        /// <returns>布尔值</returns>
        public static bool IsEndLevel(string checkItemSetId)
        {
            if (checkItemSetId == "0")
            {
                return false;
            }
            else
            {
                Model.Check_ProjectCheckItemSet checkItemSet = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
                return Convert.ToBoolean(checkItemSet.IsEndLever);
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteCheckItemSet(string checkItemSetId)
        {
            bool isDelete = true;
            var checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(checkItemSetId);
            if (checkItemSet != null)
            {
                //if (checkItemSet.IsBuiltIn == true)
                //{
                //    isDelete = false;
                //}
                if (checkItemSet.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Check_ProjectCheckItemDetail.FirstOrDefault(x => x.CheckItemSetId == checkItemSetId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetBySupCheckItemSetId(checkItemSetId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        /// <summary>
        /// 是否存在项目检查项名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistCheckItemName(string projectId, string type, string checkItemSetId, string supCheckItem, string checkItemName)
        {
            var q = Funs.DB.Check_ProjectCheckItemSet.FirstOrDefault(x =>x.ProjectId == projectId && x.CheckType == type && x.SupCheckItem == supCheckItem && x.CheckItemName == checkItemName
                    && x.CheckItemSetId != checkItemSetId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
