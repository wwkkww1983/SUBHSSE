using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 监督检查报告明细
    /// </summary>
    public static class SubUnitCheckRectifyItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据评价报告主键获取所有明细信息
        /// </summary>
        /// <param name="subUnitCheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Supervise_SubUnitCheckRectifyItem> GetSubUnitCheckRectifyItemList(string subUnitCheckRectifyId)
        {
            return (from x in Funs.DB.Supervise_SubUnitCheckRectifyItem where x.SubUnitCheckRectifyId == subUnitCheckRectifyId orderby x.CheckDate descending select x).ToList();
        }

        /// <summary>
        /// 添加监督评价报告明细
        /// </summary>
        /// <param name="item"></param>
        public static void AddSubUnitCheckRectifyItem(Model.Supervise_SubUnitCheckRectifyItem item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SubUnitCheckRectifyItem newItem = new Model.Supervise_SubUnitCheckRectifyItem
            {
                SubUnitCheckRectifyItemId = item.SubUnitCheckRectifyItemId,
                SubUnitCheckRectifyId = item.SubUnitCheckRectifyId,
                Name = item.Name,
                Sex = item.Sex,
                UnitName = item.UnitName,
                PostName = item.PostName,
                WorkTitle = item.WorkTitle,
                CheckPostName = item.CheckPostName,
                CheckDate = item.CheckDate
            };
            db.Supervise_SubUnitCheckRectifyItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据评价报告主键删除所有相关明细信息
        /// </summary>
        /// <param name="subUnitCheckRectifyId"></param>
        public static void DeleteSubUnitCheckRectifyItemsList(string subUnitCheckRectifyId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Supervise_SubUnitCheckRectifyItem where x.SubUnitCheckRectifyId == subUnitCheckRectifyId select x).ToList();
            if (q != null)
            {
                db.Supervise_SubUnitCheckRectifyItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteSubUnitCheckRectifyBySuperviseCheckReportId(string superviseCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Supervise_SubUnitCheckRectify where x.SuperviseCheckReportId == superviseCheckReportId select x);
            if (q.Count()>0)
            {
                foreach (var item in q)
                {
                    var subUnitCheckRectifyItem = from x in db.Supervise_SubUnitCheckRectifyItem where x.SubUnitCheckRectifyId == item.SubUnitCheckRectifyId select x;
                    if (subUnitCheckRectifyItem.Count() > 0)
                    {
                        db.Supervise_SubUnitCheckRectifyItem.DeleteAllOnSubmit(subUnitCheckRectifyItem);
                        db.SubmitChanges();
                    }

                    db.Supervise_SubUnitCheckRectify.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
                
            }
        }
    }
}
