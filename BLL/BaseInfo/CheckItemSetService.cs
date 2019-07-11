using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 专项检查类型主表
    /// </summary>
    public static class CheckItemSetService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取专项检查
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static Model.HSSE_Check_CheckItemSet GetCheckItemSetById(string checkItemSetId)
        {
            return db.HSSE_Check_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
        }

        /// <summary>
        /// 根据上级菜单Id获取相关专项检查
        /// </summary>
        /// <param name="supCheckItem"></param>
        /// <returns></returns>
        public static List<Model.HSSE_Check_CheckItemSet> GetCheckItemSetBySupCheckItem(string supCheckItem)
        {
            return (from x in db.HSSE_Check_CheckItemSet where x.SupCheckItem == supCheckItem orderby x.CheckItemName select x).ToList();
        }

        /// <summary>
        /// 添加专项检查
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void AddCheckItemSet(Model.HSSE_Check_CheckItemSet checkItemSet)
        {
            Model.HSSE_Check_CheckItemSet newCheckItemSet = new Model.HSSE_Check_CheckItemSet();
            newCheckItemSet.CheckItemSetId = checkItemSet.CheckItemSetId;
            newCheckItemSet.CheckItemName = checkItemSet.CheckItemName;
            newCheckItemSet.SupCheckItem = checkItemSet.SupCheckItem;
            newCheckItemSet.MapCode = checkItemSet.MapCode;
            newCheckItemSet.IsEndLever = checkItemSet.IsEndLever;
            newCheckItemSet.SortIndex = checkItemSet.SortIndex;
            newCheckItemSet.IsBuiltIn = checkItemSet.IsBuiltIn;
            db.HSSE_Check_CheckItemSet.InsertOnSubmit(newCheckItemSet);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改专项检查
        /// </summary>
        /// <param name="checkItemSet"></param>
        public static void UpdateCheckItemSet(Model.HSSE_Check_CheckItemSet checkItemSet)
        {
            Model.HSSE_Check_CheckItemSet newCheckItemSet = db.HSSE_Check_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSet.CheckItemSetId);
            if (newCheckItemSet != null)
            {
                newCheckItemSet.CheckItemName = checkItemSet.CheckItemName;
                newCheckItemSet.SupCheckItem = checkItemSet.SupCheckItem;
                newCheckItemSet.MapCode = checkItemSet.MapCode;
                newCheckItemSet.IsEndLever = checkItemSet.IsEndLever;
                newCheckItemSet.SortIndex = checkItemSet.SortIndex;
                newCheckItemSet.IsBuiltIn = checkItemSet.IsBuiltIn;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专项检查类型
        /// </summary>
        /// <param name="checkItemSetId"></param>
        public static void DeleteCheckItemSetById(string checkItemSetId)
        {
            Model.HSSE_Check_CheckItemSet checkItemSet = db.HSSE_Check_CheckItemSet.FirstOrDefault(e => e.CheckItemSetId == checkItemSetId);
            if (checkItemSet != null)
            {
                db.HSSE_Check_CheckItemSet.DeleteOnSubmit(checkItemSet);
                db.SubmitChanges();
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
            var checkItemSet = BLL.CheckItemSetService.GetCheckItemSetById(checkItemSetId);
            if (checkItemSet != null)
            {
                if (checkItemSet.IsEndLever == true)
                {
                    var detailCout = Funs.DB.HSSE_Check_CheckItemDetail.FirstOrDefault(x => x.CheckItemSetId == checkItemSetId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.CheckItemSetService.GetCheckItemSetBySupCheckItem(checkItemSetId);
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
        public static bool IsExistCheckItemName(string checkItemSetId, string supCheckItem, string checkItemName)
        {
            var q = Funs.DB.HSSE_Check_CheckItemSet.FirstOrDefault(x => x.SupCheckItem == supCheckItem && x.CheckItemName == checkItemName
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
