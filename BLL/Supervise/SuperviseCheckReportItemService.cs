using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全监督检查报告明细表
    /// </summary>
    public static class SuperviseCheckReportItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查报告明细信息
        /// </summary>
        /// <param name="superviseCheckReportItemId"></param>
        /// <returns></returns>
        public static Model.Supervise_SuperviseCheckReportItem GetSuperviseCheckReportItemById(string superviseCheckReportItemId)
        {
            return Funs.DB.Supervise_SuperviseCheckReportItem.FirstOrDefault(e => e.SuperviseCheckReportItemId == superviseCheckReportItemId);
        }

        /// <summary>
        /// 根据安全监督检查报告id获取所有相关明细信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        /// <returns></returns>
        public static List<Model.Supervise_SuperviseCheckReportItem> GetSuperviseCheckReportItemBySuperviseCheckReportId(string superviseCheckReportId)
        {
            return (from x in Funs.DB.Supervise_SuperviseCheckReportItem where x.SuperviseCheckReportId == superviseCheckReportId select x).ToList();
        }

        /// <summary>
        /// 根据安全监督检查报告id获取所有选中的相关明细信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        /// <returns></returns>
        public static List<Model.Supervise_SuperviseCheckReportItem> GetSelectedSuperviseCheckReportItemBySuperviseCheckReportId(string superviseCheckReportId)
        {
            return (from x in Funs.DB.Supervise_SuperviseCheckReportItem where x.SuperviseCheckReportId == superviseCheckReportId && x.IsSelected == true select x).ToList();
        }

        /// <summary>
        /// 添加安全监督检查报告明细信息
        /// </summary>
        /// <param name="superviseCheckReportItem"></param>
        public static void AddSuperviseCheckReportItem(Model.Supervise_SuperviseCheckReportItem superviseCheckReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckReportItem newSuperviseCheckReportItem = new Model.Supervise_SuperviseCheckReportItem
            {
                SuperviseCheckReportItemId = superviseCheckReportItem.SuperviseCheckReportItemId,
                SuperviseCheckReportId = superviseCheckReportItem.SuperviseCheckReportId,
                RectifyItemId = superviseCheckReportItem.RectifyItemId,
                IsSelected = superviseCheckReportItem.IsSelected,
                AttachUrl = superviseCheckReportItem.AttachUrl
            };
            db.Supervise_SuperviseCheckReportItem.InsertOnSubmit(newSuperviseCheckReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除安全监督检查报告明细信息
        /// </summary>
        /// <param name="superviseCheckReportItemId"></param>
        public static void DeleteSuperviseCheckReportItem(string superviseCheckReportItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckReportItem superviseCheckReportItem = db.Supervise_SuperviseCheckReportItem.FirstOrDefault(e => e.SuperviseCheckReportItemId == superviseCheckReportItemId);
            if (superviseCheckReportItem != null)
            {
                db.Supervise_SuperviseCheckReportItem.DeleteOnSubmit(superviseCheckReportItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全监督检查报告ID删除所有相关明细信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteSuperviseCheckReportItemBySuperviseCheckReportId(string superviseCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Supervise_SuperviseCheckReportItem where x.SuperviseCheckReportId == superviseCheckReportId select x).ToList();
            if (q != null)
            {
                db.Supervise_SuperviseCheckReportItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
