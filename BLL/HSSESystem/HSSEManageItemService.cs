using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全管理机构明细
    /// </summary>
    public static class HSSEManageItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据安全管理机构主键获取所有相关明细信息
        /// </summary>
        /// <param name="hsseManageId"></param>
        /// <returns></returns>
        public static List<Model.HSSESystem_HSSEManageItem> GetHSSEManageItemList(string hsseManageId)
        {
            return (from x in Funs.DB.HSSESystem_HSSEManageItem where x.HSSEManageId == hsseManageId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 根据主键获取安全管理机构明细信息
        /// </summary>
        /// <param name="hsseManageItemId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_HSSEManageItem GetHSSEManageItemById(string hsseManageItemId)
        {
            return Funs.DB.HSSESystem_HSSEManageItem.FirstOrDefault(e => e.HSSEManageItemId == hsseManageItemId);
        }

        /// <summary>
        /// 添加安全管理机构明细
        /// </summary>
        /// <param name="item"></param>
        public static void AddHSSEManageItem(Model.HSSESystem_HSSEManageItem item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManageItem newItem = new Model.HSSESystem_HSSEManageItem
            {
                HSSEManageItemId = item.HSSEManageItemId,
                HSSEManageId = item.HSSEManageId,
                Post = item.Post,
                Names = item.Names,
                Telephone = item.Telephone,
                MobilePhone = item.MobilePhone,
                EMail = item.EMail,
                Duty = item.Duty,
                SortIndex = item.SortIndex
            };
            db.HSSESystem_HSSEManageItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateHSSEManageItem(Model.HSSESystem_HSSEManageItem item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManageItem newItem = db.HSSESystem_HSSEManageItem.FirstOrDefault(e => e.HSSEManageItemId == item.HSSEManageItemId);
            if (newItem != null)
            {
                newItem.HSSEManageId = item.HSSEManageId;
                newItem.Post = item.Post;
                newItem.Names = item.Names;
                newItem.Telephone = item.Telephone;
                newItem.MobilePhone = item.MobilePhone;
                newItem.EMail = item.EMail;
                newItem.Duty = item.Duty;
                newItem.SortIndex = item.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="hsseManageItemId"></param>
        public static void DeleteHSSEManageItem(string hsseManageItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManageItem item = db.HSSESystem_HSSEManageItem.FirstOrDefault(e => e.HSSEManageItemId == hsseManageItemId);
            if (item != null)
            {
                db.HSSESystem_HSSEManageItem.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除所有相关明细信息
        /// </summary>
        /// <param name="hsseManageId"></param>
        public static void DeleteHSSEManageItemList(string hsseManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.HSSESystem_HSSEManageItem where x.HSSEManageId == hsseManageId select x).ToList();
            if (q != null)
            {
                db.HSSESystem_HSSEManageItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}