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

                var unitItems = SafeReportUnitItemService.GetSafeReportUnitItemListBySafeReportId(safeReportId);
                foreach (var item in unitItems)
                {
                    SafeReportUnitItemService.DeleteSafeReportUnitItemBySafeReportUnitItemId(item.SafeReportUnitItemId);
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
                var detailCout = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != BLL.Const.State_0);
                if (detailCout != null)
                {
                    isDelete = false;
                }
                var detailUnitCout = Funs.DB.Manager_SafeReportUnitItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != BLL.Const.State_0);
                if (detailUnitCout != null)
                {
                    isDelete = false;
                }
                var supItemSetCount = GetSafeReportBySupItem(safeReportId);
                if (supItemSetCount.Count() > 0)
                {
                    isDelete = false;
                }
            }

            return isDelete;
        }

        /// <summary>
        /// 安全报表是否未上报
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-已上报，false-未上报</returns>
        public static bool IsUpLoadSafeReport(string safeReportId)
        {
            bool isUpLoad = true;
            var safeReport = GetSafeReportBySafeReportId(safeReportId);
            if (safeReport != null && safeReport.IsEndLever == true)
            {
                var detailCout = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != Const.State_2);
                if (detailCout != null)
                {
                    isUpLoad = false;
                }

                var detailUnitCout = Funs.DB.Manager_SafeReportUnitItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != Const.State_2);
                if (detailUnitCout != null)
                {
                    isUpLoad = false;
                }
            }

            return isUpLoad;
        }

        /// <summary>
        /// 项目是否未上报
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-已上报，false-未上报</returns>
        public static bool IsUpLoadSafeReport(string safeReportId, string projectId)
        {
            bool isUpLoad = true;
            var safeReport = GetSafeReportBySafeReportId(safeReportId);
            if (safeReport != null && safeReport.IsEndLever == true)
            {
                var detailCout = Funs.DB.Manager_SafeReportItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != Const.State_2 && x.ProjectId == projectId);
                if (detailCout != null)
                {
                    isUpLoad = false;
                }               
            }

            return isUpLoad;
        }

        /// <summary>
        /// 单位是否未上报
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-已上报，false-未上报</returns>
        public static bool IsUpLoadSafeReportUnit(string safeReportId, string unitId)
        {
            bool isUpLoad = true;
            var safeReport = GetSafeReportBySafeReportId(safeReportId);
            if (safeReport != null && safeReport.IsEndLever == true)
            {
                var detailUnitCout = Funs.DB.Manager_SafeReportUnitItem.FirstOrDefault(x => x.SafeReportId == safeReportId && x.States != Const.State_2 && x.UnitId == unitId);
                if (detailUnitCout != null)
                {
                    isUpLoad = false;
                }
            }

            return isUpLoad;
        }

        /// <summary>
        /// 根据项目id 获取项目下的安全文件上报目录
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Manager_SafeReport> getProjectSafeReportList(string projectId)
        {
            List<Model.Manager_SafeReport> safeReportLists = new List<Model.Manager_SafeReport>();
            var safeReportItem = from x in Funs.DB.Manager_SafeReportItem where x.ProjectId == projectId select x.SafeReportId;
            foreach (var item in safeReportItem)
            {
                var safeReport = GetSafeReportBySafeReportId(item);
                if (safeReport != null)
                {
                    safeReportLists.Add(safeReport);
                    if (safeReport.SupSafeReportId != "0" && safeReportLists.FirstOrDefault(x => x.SafeReportId == safeReport.SupSafeReportId) == null)
                    {
                        List<Model.Manager_SafeReport> setLists = new List<Model.Manager_SafeReport>();
                        var getLists = getSafeReportBySafeReportId(safeReport.SupSafeReportId, setLists);
                        if (getLists.Count() > 0)
                        {
                            safeReportLists.AddRange(getLists);
                        }
                    }
                }
            }
            return safeReportLists.Distinct().OrderByDescending(x=>x.SafeReportCode).ToList();
        }

        /// <summary>
        /// 根据单位id 获取该单位的安全文件上报目录
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.Manager_SafeReport> getUnitSafeReportList(string unitId)
        {
            List<Model.Manager_SafeReport> safeReportLists = new List<Model.Manager_SafeReport>();
            var safeReportUnitItem = from x in Funs.DB.Manager_SafeReportUnitItem
                                 where x.UnitId == unitId
                                 select x.SafeReportId;
            foreach (var item in safeReportUnitItem)
            {
                var safeReport = GetSafeReportBySafeReportId(item);
                if (safeReport != null)
                {
                    safeReportLists.Add(safeReport);
                    if (safeReport.SupSafeReportId != "0" && safeReportLists.FirstOrDefault(x => x.SafeReportId == safeReport.SupSafeReportId) == null)
                    {
                        List<Model.Manager_SafeReport> setLists = new List<Model.Manager_SafeReport>();
                        var getLists = getSafeReportBySafeReportId(safeReport.SupSafeReportId, setLists);
                        if (getLists.Count() > 0)
                        {
                            safeReportLists.AddRange(getLists);
                        }
                    }
                }
            }
            return safeReportLists.Distinct().OrderByDescending(x => x.SafeReportCode).ToList();
        }

        /// <summary>
        ///  循环获取上级安全文件目录
        /// </summary>
        /// <param name="safeReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_SafeReport> getSafeReportBySafeReportId(string safeReportId, List<Model.Manager_SafeReport> safeReportLists)
        {
            if (safeReportLists.FirstOrDefault(x => x.SafeReportId == safeReportId) == null)
            {
                var safeReport = GetSafeReportBySafeReportId(safeReportId);
                if (safeReport != null)
                {
                    safeReportLists.Add(safeReport);
                    if (safeReport.SupSafeReportId != "0")
                    {
                        getSafeReportBySafeReportId(safeReport.SupSafeReportId, safeReportLists);
                    }
                }
            }
            return safeReportLists;
        }
    }
}
