using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目检查项明细
    /// </summary>
    public static class Check_ProjectCheckItemDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目检查项明细
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        /// <returns></returns>
        public static Model.Check_ProjectCheckItemDetail GetCheckItemDetailById(string checkItemDetailId)
        {
            return Funs.DB.Check_ProjectCheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
        }

        /// <summary>
        /// 添加项目检查项明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void AddCheckItemDetail(Model.Check_ProjectCheckItemDetail checkItemDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemDetail newCheckItemDetail = new Model.Check_ProjectCheckItemDetail
            {
                CheckItemDetailId = checkItemDetail.CheckItemDetailId,
                CheckItemSetId = checkItemDetail.CheckItemSetId,
                CheckContent = checkItemDetail.CheckContent,
                SortIndex = checkItemDetail.SortIndex,
                IsBuiltIn = checkItemDetail.IsBuiltIn
            };
            db.Check_ProjectCheckItemDetail.InsertOnSubmit(newCheckItemDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目检查项明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void UpdateCheckItemDetail(Model.Check_ProjectCheckItemDetail checkItemDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemDetail newCheckItemDetail = db.Check_ProjectCheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetail.CheckItemDetailId);
            if (newCheckItemDetail != null)
            {
                newCheckItemDetail.CheckContent = checkItemDetail.CheckContent;
                newCheckItemDetail.SortIndex = checkItemDetail.SortIndex;
                newCheckItemDetail.IsBuiltIn = checkItemDetail.IsBuiltIn;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目检查项明细
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        public static void DeleteCheckItemDetail(string checkItemDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_ProjectCheckItemDetail checkItemDetail = db.Check_ProjectCheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
            if (checkItemDetail != null)
            {
                db.Check_ProjectCheckItemDetail.DeleteOnSubmit(checkItemDetail);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目检查项主键删除所有相关明细信息
        /// </summary>
        /// <param name="rectifyId"></param>
        public static void DeleteCheckItemDetailByCheckItemSetId(string checkItemSetId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_ProjectCheckItemDetail where x.CheckItemSetId == checkItemSetId select x).ToList();
            if (q != null)
            {
                db.Check_ProjectCheckItemDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
