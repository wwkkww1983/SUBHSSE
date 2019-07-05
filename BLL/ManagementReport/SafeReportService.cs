using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class SafeReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据上级Id查询所有安全文件
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>安全文件的集合</returns>
        public static List<Model.Manager_SafeReport> GetSafeReportBySupItem(string supItem)
        {
            return (from x in Funs.DB.Manager_SafeReport where x.SupSafeReportId == supItem orderby x.SafeReportCode descending select x).ToList();
        }

        /// <summary>
        /// 根据安全文件Id查询安全文件实体
        /// </summary>
        /// <param name="SafeReportId">安全文件主键</param>
        /// <returns>安全文件实体</returns>
        public static Model.Manager_SafeReport GetSafeReportBySafeReportId(string SafeReportId)
        {
            Model.Manager_SafeReport SafeReport = Funs.DB.Manager_SafeReport.FirstOrDefault(e => e.SafeReportId == SafeReportId);
            return SafeReport;
        }

        /// <summary>
        /// 添加安全文件
        /// </summary>
        /// <param name="SafeReport">安全文件实体</param>
        public static void AddSafeReport(Model.Manager_SafeReport SafeReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReport newSafeReport = new Model.Manager_SafeReport
            {
                SafeReportId = SafeReport.SafeReportId,
                SafeReportCode = SafeReport.SafeReportCode,
                SafeReportName = SafeReport.SafeReportName,
                SupSafeReportId = SafeReport.SupSafeReportId,
                Requirement = SafeReport.Requirement,
                RequestTime = SafeReport.RequestTime,
                CompileManId = SafeReport.CompileManId,
                CompileTime = SafeReport.CompileTime,
                IsEndLever = SafeReport.IsEndLever,
                States = SafeReport.States
            };
            db.Manager_SafeReport.InsertOnSubmit(newSafeReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全文件
        /// </summary>
        /// <param name="SafeReport">安全文件实体</param>
        public static void UpdateSafeReport(Model.Manager_SafeReport SafeReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReport newSafeReport = db.Manager_SafeReport.First(e => e.SafeReportId == SafeReport.SafeReportId);
            newSafeReport.SafeReportCode = SafeReport.SafeReportCode;
            newSafeReport.SafeReportName = SafeReport.SafeReportName;
            newSafeReport.SupSafeReportId = SafeReport.SupSafeReportId;
            newSafeReport.Requirement = SafeReport.Requirement;
            newSafeReport.RequestTime = SafeReport.RequestTime;
            newSafeReport.CompileManId = SafeReport.CompileManId;
            newSafeReport.CompileTime = SafeReport.CompileTime;
            newSafeReport.IsEndLever = SafeReport.IsEndLever;
            newSafeReport.States = SafeReport.States;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据安全文件Id删除一个安全文件
        /// </summary>
        /// <param name="safeReportId">安全文件ID</param>
        public static void DeleteSafeReportBySafeReportId(string safeReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SafeReport delSafeReport = db.Manager_SafeReport.FirstOrDefault(e => e.SafeReportId == safeReportId);
            if (delSafeReport != null)
            {
                var items = SafeReportItemService.GetSafeReportItemListBySafeReportId(safeReportId);
                foreach (var item in items)
                {
                    SafeReportItemService.DeleteSafeReportItemBySafeReportItemId(item.SafeReportItemId);
                }
                db.Manager_SafeReport.DeleteOnSubmit(delSafeReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据上级Id删除所有对应的安全文件
        /// </summary>
        /// <param name="supItem">上级Id</param>
        public static void DeleteSafeReportBySupItem(string supItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_SafeReport where x.SupSafeReportId == supItem select x).ToList();
            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DeleteSafeReportBySafeReportId(item.SafeReportId);
                }
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteSafeReport(string safeReportId)
        {
            bool isDelete = true;
            var SafeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(safeReportId);
            if (SafeReport != null)
            {
                if (SafeReport.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == safeReportId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.SafeReportService.GetSafeReportBySupItem(safeReportId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }

            return isDelete;
        }

        /// <summary>
        /// 是否未上报
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-已上报，false-未上报</returns>
        public static bool IsUpLoadSafeReport(string safeReportId)
        {
            bool isUpLoad = true;
            var safeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(safeReportId);
            if (safeReport != null && safeReport.IsEndLever == true)
            {
                var detailCout = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != Const.State_2);
                if (detailCout != null)
                {
                    isUpLoad = false;
                }
            }

            return isUpLoad;
        }
    }
}
