using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class SafeReportUnitItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据单位安全文件Id查询单位安全文件实体
        /// </summary>
        /// <param name="SafeReportId">单位安全文件主键</param>
        /// <returns>单位安全文件实体</returns>
        public static List<Model.Manager_SafeReportUnitItem> GetSafeReportUnitItemListBySafeReportId(string safeReportId)
        {
            var SafeReportUnitItemList = from x in Funs.DB.Manager_SafeReportUnitItem where x.SafeReportId == safeReportId select x;
            return SafeReportUnitItemList.ToList();
        }

        /// <summary>
        /// 根据单位安全文件Id查询单位安全文件实体
        /// </summary>
        /// <param name="safeReportUnitItemId">单位安全文件主键</param>
        /// <returns>单位安全文件实体</returns>
        public static Model.Manager_SafeReportUnitItem GetSafeReportUnitItemBySafeReportUnitItemId(string safeReportUnitItemId)
        {
            Model.Manager_SafeReportUnitItem SafeReportUnitItem = Funs.DB.Manager_SafeReportUnitItem.FirstOrDefault(e => e.SafeReportUnitItemId == safeReportUnitItemId);
            return SafeReportUnitItem;
        }

        /// <summary>
        /// 根据单位ID安全文件Id查询单位安全文件实体
        /// </summary>
        /// <param name="SafeReportUnitItemId">单位安全文件主键</param>
        /// <returns>单位安全文件实体</returns>
        public static Model.Manager_SafeReportUnitItem GetSafeReportUnitItemBySafeReportUnitId(string safeReportId, String unitId)
        {
            Model.Manager_SafeReportUnitItem SafeReportUnitItem = Funs.DB.Manager_SafeReportUnitItem.FirstOrDefault(e => e.SafeReportId == safeReportId && e.UnitId == unitId);
            return SafeReportUnitItem;
        }

        /// <summary>
        /// 添加单位安全文件
        /// </summary>
        /// <param name="safeReportUnitItem">单位安全文件实体</param>
        public static void AddSafeReportUnitItem(Model.Manager_SafeReportUnitItem safeReportUnitItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportUnitItem newSafeReportUnitItem = new Model.Manager_SafeReportUnitItem
            {
                SafeReportUnitItemId = safeReportUnitItem.SafeReportUnitItemId,
                SafeReportId = safeReportUnitItem.SafeReportId,
                UnitId = safeReportUnitItem.UnitId,
                ReportContent = safeReportUnitItem.ReportContent,
                ReportManId = safeReportUnitItem.ReportManId,
                ReportTime = safeReportUnitItem.ReportTime,
                UpReportTime = safeReportUnitItem.UpReportTime,
                States = safeReportUnitItem.States
            };
            db.Manager_SafeReportUnitItem.InsertOnSubmit(newSafeReportUnitItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改单位安全文件
        /// </summary>
        /// <param name="safeReportUnitItem">单位安全文件实体</param>
        public static void UpdateSafeReportUnitItem(Model.Manager_SafeReportUnitItem safeReportUnitItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportUnitItem newSafeReportUnitItem = db.Manager_SafeReportUnitItem.First(e => e.SafeReportUnitItemId == safeReportUnitItem.SafeReportUnitItemId);
            newSafeReportUnitItem.ReportContent = safeReportUnitItem.ReportContent;
            newSafeReportUnitItem.ReportManId = safeReportUnitItem.ReportManId;
            newSafeReportUnitItem.ReportTime = safeReportUnitItem.ReportTime;
            newSafeReportUnitItem.UpReportTime = safeReportUnitItem.UpReportTime;
            newSafeReportUnitItem.States = safeReportUnitItem.States;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位安全文件Id删除一个单位安全文件
        /// </summary>
        /// <param name="safeReportUnitItemId">单位安全文件ID</param>
        public static void DeleteSafeReportUnitItemBySafeReportUnitItemId(string safeReportUnitItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportUnitItem SafeReportUnitItem = db.Manager_SafeReportUnitItem.FirstOrDefault(e => e.SafeReportUnitItemId == safeReportUnitItemId);
            if (SafeReportUnitItem != null)
            {
                db.Manager_SafeReportUnitItem.DeleteOnSubmit(SafeReportUnitItem);
                db.SubmitChanges();
            }
        }        
    }
}
