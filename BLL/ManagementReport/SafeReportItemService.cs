using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class SafeReportItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据项目安全文件Id查询项目安全文件实体
        /// </summary>
        /// <param name="SafeReportId">项目安全文件主键</param>
        /// <returns>项目安全文件实体</returns>
        public static List<Model.Manager_SafeReportItem> GetSafeReportItemListBySafeReportId(string safeReportId)
        {
            var safeReportItemList = from x in Funs.DB.Manager_SafeReportItem where x.SafeReportId == safeReportId select x;
            return safeReportItemList.ToList();
        }

        /// <summary>
        /// 根据项目安全文件Id查询项目安全文件实体
        /// </summary>
        /// <param name="safeReportItemId">项目安全文件主键</param>
        /// <returns>项目安全文件实体</returns>
        public static Model.Manager_SafeReportItem GetSafeReportItemBySafeReportItemId(string safeReportItemId)
        {
            Model.Manager_SafeReportItem SafeReportItem = Funs.DB.Manager_SafeReportItem.FirstOrDefault(e => e.SafeReportItemId == safeReportItemId);
            return SafeReportItem;
        }

        /// <summary>
        /// 根据项目ID安全文件Id查询项目安全文件实体
        /// </summary>
        /// <param name="safeReportItemId">项目安全文件主键</param>
        /// <returns>项目安全文件实体</returns>
        public static Model.Manager_SafeReportItem GetSafeReportItemBySafeReportProjectId(string safeReportId, String projectId)
        {
            Model.Manager_SafeReportItem SafeReportItem = Funs.DB.Manager_SafeReportItem.FirstOrDefault(e => e.SafeReportId == safeReportId && e.ProjectId == projectId);
            return SafeReportItem;
        }

        /// <summary>
        /// 添加项目安全文件
        /// </summary>
        /// <param name="SafeReportItem">项目安全文件实体</param>
        public static void AddSafeReportItem(Model.Manager_SafeReportItem SafeReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportItem newSafeReportItem = new Model.Manager_SafeReportItem
            {
                SafeReportItemId = SafeReportItem.SafeReportItemId,
                SafeReportId = SafeReportItem.SafeReportId,
                ProjectId = SafeReportItem.ProjectId,
                ReportContent = SafeReportItem.ReportContent,
                ReportManId = SafeReportItem.ReportManId,
                ReportTime = SafeReportItem.ReportTime,
                UpReportTime = SafeReportItem.UpReportTime,
                States = SafeReportItem.States
            };
            db.Manager_SafeReportItem.InsertOnSubmit(newSafeReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目安全文件
        /// </summary>
        /// <param name="SafeReportItem">项目安全文件实体</param>
        public static void UpdateSafeReportItem(Model.Manager_SafeReportItem SafeReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportItem newSafeReportItem = db.Manager_SafeReportItem.First(e => e.SafeReportItemId == SafeReportItem.SafeReportItemId);
            newSafeReportItem.ReportContent = SafeReportItem.ReportContent;
            newSafeReportItem.ReportManId = SafeReportItem.ReportManId;
            newSafeReportItem.ReportTime = SafeReportItem.ReportTime;
            newSafeReportItem.UpReportTime = SafeReportItem.UpReportTime;
            newSafeReportItem.States = SafeReportItem.States;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据项目安全文件Id删除一个项目安全文件
        /// </summary>
        /// <param name="safeReportItemId">项目安全文件ID</param>
        public static void DeleteSafeReportItemBySafeReportItemId(string safeReportItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReportItem safeReportItem = db.Manager_SafeReportItem.FirstOrDefault(e => e.SafeReportItemId == safeReportItemId);
            if (safeReportItem != null)
            {
                db.Manager_SafeReportItem.DeleteOnSubmit(safeReportItem);
                db.SubmitChanges();
            }
        }        
    }
}
