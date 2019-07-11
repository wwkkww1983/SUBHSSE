using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 施工单位安全费用明细管理
    /// </summary>
    public static class HSSECostUnitManageItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="HSSECostUnitManageItemId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostUnitManageItem GetHSSECostUnitManageItemByHSSECostUnitManageItemId(string HSSECostUnitManageItemId)
        {
            return Funs.DB.CostGoods_HSSECostUnitManageItem.FirstOrDefault(e => e.HSSECostUnitManageItemId == HSSECostUnitManageItemId);
        }
        
        /// <summary>
        /// 添加安全费用管理
        /// </summary>
        /// <param name="item"></param>
        public static void AddHSSECostUnitManageItem(Model.CostGoods_HSSECostUnitManageItem item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostUnitManageItem newItem = new Model.CostGoods_HSSECostUnitManageItem
            {
                HSSECostUnitManageItemId = item.HSSECostUnitManageItemId,
                HSSECostUnitManageId = item.HSSECostUnitManageId,
                Type = item.Type,
                SortIndex = item.SortIndex,
                ReportTime = item.ReportTime,
                CostContent = item.CostContent,
                Quantity = item.Quantity,
                Metric = item.Metric,
                Price = item.Price,
                TotalPrice = item.TotalPrice,
                AuditQuantity = item.AuditQuantity,
                AuditPrice = item.AuditPrice,
                AuditTotalPrice = item.AuditTotalPrice,
                IsAgree = item.IsAgree,
                AuditExplain = item.AuditExplain,
                RatifiedQuantity = item.RatifiedQuantity,
                RatifiedPrice = item.RatifiedPrice,
                RatifiedTotalPrice = item.RatifiedTotalPrice,
                RatifiedExplain = item.RatifiedExplain,
            };
            db.CostGoods_HSSECostUnitManageItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

    /// <summary>
    /// 修改费用管理
    /// </summary>
    /// <param name="HSSECostUnitManageItem"></param>
    public static void UpdateHSSECostUnitManageItem(Model.CostGoods_HSSECostUnitManageItem HSSECostUnitManageItem)
    {
        Model.SUBHSSEDB db = Funs.DB;
        Model.CostGoods_HSSECostUnitManageItem newItem = db.CostGoods_HSSECostUnitManageItem.FirstOrDefault(e => e.HSSECostUnitManageItemId == HSSECostUnitManageItem.HSSECostUnitManageItemId);
        if (newItem != null)
        {
            newItem.Type = HSSECostUnitManageItem.Type;
            newItem.SortIndex = HSSECostUnitManageItem.SortIndex;
            newItem.ReportTime = HSSECostUnitManageItem.ReportTime;
            newItem.CostContent = HSSECostUnitManageItem.CostContent;
            newItem.Quantity = HSSECostUnitManageItem.Quantity;
            newItem.Metric = HSSECostUnitManageItem.Metric;
            newItem.Price = HSSECostUnitManageItem.Price;
            newItem.TotalPrice = HSSECostUnitManageItem.TotalPrice;
            newItem.AuditQuantity = HSSECostUnitManageItem.AuditQuantity;
            newItem.AuditPrice = HSSECostUnitManageItem.AuditPrice;
            newItem.AuditTotalPrice = HSSECostUnitManageItem.AuditTotalPrice;
            newItem.IsAgree = HSSECostUnitManageItem.IsAgree;
            newItem.AuditExplain = HSSECostUnitManageItem.AuditExplain;
            newItem.RatifiedQuantity = HSSECostUnitManageItem.RatifiedQuantity;
            newItem.RatifiedPrice = HSSECostUnitManageItem.RatifiedPrice;
            newItem.RatifiedTotalPrice = HSSECostUnitManageItem.RatifiedTotalPrice;
            newItem.RatifiedExplain = HSSECostUnitManageItem.RatifiedExplain;
            db.SubmitChanges();
        }
    }

        /// <summary>
        /// 根据安全费用主键删除施工单位费用管理
        /// </summary>
        /// <param name="HSSECostUnitManageItemId"></param>
        public static void DeleteHSSECostUnitManageItemByHSSECostManageId(string id)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var item = db.CostGoods_HSSECostUnitManageItem.FirstOrDefault(x => x.HSSECostUnitManageItemId == id);
            if (item != null)
            {
                CommonService.DeleteAttachFileById(id);//删除附件
                db.CostGoods_HSSECostUnitManageItem.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全费用主键删除施工单位费用管理
        /// </summary>
        /// <param name="HSSECostUnitManageItemId"></param>
        public static void DeleteHSSECostUnitManageItemByHSSECostUnitManageId(string HSSECostUnitManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var items = from x in db.CostGoods_HSSECostUnitManageItem where x.HSSECostUnitManageId == HSSECostUnitManageId select x;
            if (items.Count() > 0)
            {
                foreach (var item in items)
                {
                    DeleteHSSECostUnitManageItemByHSSECostManageId(item.HSSECostUnitManageItemId);
                }
            }
        }
    }
}
