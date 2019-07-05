using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 费用管理明细
    /// </summary>
    public static class CostManageItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键删除费用管理明细
        /// </summary>
        /// <param name="costManageItemId"></param>
        /// <returns></returns>
        public static Model.CostGoods_CostManageItem GetCostManageItemById(string costManageItemId)
        {
            return Funs.DB.CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItemId);
        }

        /// <summary>
        /// 根据费用管理主键获取所有相关明细信息
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_CostManageItem> GetCostManageItemByCostManageId(string costManageId)
        {
            return (from x in Funs.DB.CostGoods_CostManageItem where x.CostManageId == costManageId select x).ToList();
        }

        /// <summary>
        /// 添加费用管理明细
        /// </summary>
        /// <param name="costManageItem"></param>
        public static void AddCostManageItem(Model.CostGoods_CostManageItem costManageItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_CostManageItem newCostManageItem = new Model.CostGoods_CostManageItem
            {
                CostManageItemId = costManageItem.CostManageItemId,
                CostManageId = costManageItem.CostManageId,
                InvestCostProject = costManageItem.InvestCostProject,
                UseReason = costManageItem.UseReason,
                Counts = costManageItem.Counts,
                PriceMoney = costManageItem.PriceMoney,
                Remark = costManageItem.Remark
            };
            db.CostGoods_CostManageItem.InsertOnSubmit(newCostManageItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改费用管理明细
        /// </summary>
        /// <param name="costManageItem"></param>
        public static void UpdateCostManageItem(Model.CostGoods_CostManageItem costManageItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_CostManageItem newCostManageItem = db.CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItem.CostManageItemId);
            if (newCostManageItem != null)
            {
                newCostManageItem.CostManageId = costManageItem.CostManageId;
                newCostManageItem.InvestCostProject = costManageItem.InvestCostProject;
                newCostManageItem.UseReason = costManageItem.UseReason;
                newCostManageItem.Counts = costManageItem.Counts;
                newCostManageItem.PriceMoney = costManageItem.PriceMoney;
                newCostManageItem.Remark = costManageItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据费用管理主键删除所有相关明细信息
        /// </summary>
        /// <param name="costManageId"></param>
        public static void DeleteCostManageItemByCostManageId(string costManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.CostGoods_CostManageItem where x.CostManageId == costManageId select x).ToList();
            if (q != null)
            {
                db.CostGoods_CostManageItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除费用管理信息
        /// </summary>
        /// <param name="costManageItemId"></param>
        public static void DeleteCostManageItemById(string costManageItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_CostManageItem costManageItem = db.CostGoods_CostManageItem.FirstOrDefault(e => e.CostManageItemId == costManageItemId);
            if (costManageItem != null)
            {
                db.CostGoods_CostManageItem.DeleteOnSubmit(costManageItem);
                db.SubmitChanges();
            }
        }
    }
}