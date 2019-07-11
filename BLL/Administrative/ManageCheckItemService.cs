using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 行政管理检查记录明细表
    /// </summary>
    public static class ManageCheckItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据行政管理检查项目编号查询对应的所有检查内容信息的数量
        /// </summary>
        /// <param name="manageCheckCode">行政管理检查项目编号</param>
        /// <returns></returns>
        public static int GetManageCheckItemCountByManageCheckId(string manageCheckId)
        {
            return (from x in Funs.DB.Administrative_ManageCheckItem where x.ManageCheckId == manageCheckId select x.ManageCheckItemId).Count();
        }

        /// <summary>
        /// 根据行政管理检查记录Id获取所有相关明细信息
        /// </summary>
        /// <param name="manageCheckId"></param>
        /// <returns></returns>
        public static List<Model.Administrative_ManageCheckItem> ManageCheckItemByManageCheckId(string manageCheckId)
        {
            return (from x in Funs.DB.Administrative_ManageCheckItem where x.ManageCheckId == manageCheckId select x).ToList();
        }

        /// <summary>
        /// 添加行政管理检查记录明细
        /// </summary>
        /// <param name="manageCheckItem"></param>
        public static void AddManageCheckItem(Model.Administrative_ManageCheckItem manageCheckItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_ManageCheckItem newManageCheckItem = new Model.Administrative_ManageCheckItem
            {
                ManageCheckItemId = manageCheckItem.ManageCheckItemId,
                ManageCheckId = manageCheckItem.ManageCheckId,
                CheckTypeCode = manageCheckItem.CheckTypeCode,
                IsCheck = manageCheckItem.IsCheck
            };
            db.Administrative_ManageCheckItem.InsertOnSubmit(newManageCheckItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据行政管理ID删除所有相关明细信息
        /// </summary>
        /// <param name="manageCheckId"></param>
        public static void DeleteMangeCheckItemByManageCheckId(string manageCheckId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var item = (from x in db.Administrative_ManageCheckItem where x.ManageCheckId == manageCheckId select x).ToList();
            if (item.Count() > 0)
            {
                db.Administrative_ManageCheckItem.DeleteAllOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}
