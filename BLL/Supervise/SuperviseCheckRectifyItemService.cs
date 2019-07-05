using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全监督检查整改明细表
    /// </summary>
    public class SuperviseCheckRectifyItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查整改明细信息
        /// </summary>
        /// <param name="superviseCheckRectifyItemId"></param>
        /// <returns></returns>
        public static Model.Supervise_SuperviseCheckRectifyItem GetSuperviseCheckRectifyItemById(string superviseCheckRectifyItemId)
        {
            return Funs.DB.Supervise_SuperviseCheckRectifyItem.FirstOrDefault(e => e.SuperviseCheckRectifyItemId == superviseCheckRectifyItemId);
        }

        /// <summary>
        /// 根据安全监督检查整改id获取所有相关明细信息
        /// </summary>
        /// <param name="superviseCheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Supervise_SuperviseCheckRectifyItem> GetSuperviseCheckRectifyItemBySuperviseCheckRectifyId(string superviseCheckRectifyId)
        {
            return (from x in Funs.DB.Supervise_SuperviseCheckRectifyItem where x.SuperviseCheckRectifyId == superviseCheckRectifyId select x).ToList();
        }

        /// <summary>
        /// 添加安全监督检查整改明细信息
        /// </summary>
        /// <param name="superviseCheckRectifyItem"></param>
        public static void AddSuperviseCheckRectifyItem(Model.Supervise_SuperviseCheckRectifyItem superviseCheckRectifyItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckRectifyItem newSuperviseCheckRectifyItem = new Model.Supervise_SuperviseCheckRectifyItem
            {
                SuperviseCheckRectifyItemId = superviseCheckRectifyItem.SuperviseCheckRectifyItemId,
                SuperviseCheckRectifyId = superviseCheckRectifyItem.SuperviseCheckRectifyId,
                RectifyItemId = superviseCheckRectifyItem.RectifyItemId,
                ConfirmMan = superviseCheckRectifyItem.ConfirmMan,
                ConfirmDate = superviseCheckRectifyItem.ConfirmDate,
                OrderEndDate = superviseCheckRectifyItem.OrderEndDate,
                OrderEndPerson = superviseCheckRectifyItem.OrderEndPerson,
                RealEndDate = superviseCheckRectifyItem.RealEndDate,
                AttachUrl = superviseCheckRectifyItem.AttachUrl
            };
            db.Supervise_SuperviseCheckRectifyItem.InsertOnSubmit(newSuperviseCheckRectifyItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除安全监督检查整改明细信息
        /// </summary>
        /// <param name="superviseCheckRectifyItemId"></param>
        public static void DeleteSuperviseCheckRectifyItem(string superviseCheckRectifyItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckRectifyItem superviseCheckRectifyItem = db.Supervise_SuperviseCheckRectifyItem.FirstOrDefault(e => e.SuperviseCheckRectifyItemId == superviseCheckRectifyItemId);
            if (superviseCheckRectifyItem != null)
            {
                db.Supervise_SuperviseCheckRectifyItem.DeleteOnSubmit(superviseCheckRectifyItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全监督检查整改ID删除所有相关明细信息
        /// </summary>
        /// <param name="superviseCheckRectifyId"></param>
        public static void DeleteSuperviseCheckRectifyItemBySuperviseCheckRectifyId(string superviseCheckRectifyId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Supervise_SuperviseCheckRectifyItem where x.SuperviseCheckRectifyId == superviseCheckRectifyId select x).ToList();
            if (q != null)
            {
                db.Supervise_SuperviseCheckRectifyItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
