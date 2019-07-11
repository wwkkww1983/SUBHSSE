using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 专项检查类型明细表
    /// </summary>
    public static class CheckItemDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取检查内容
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        /// <returns></returns>
        public static Model.HSSE_Check_CheckItemDetail GetCheckItemDetailById(string checkItemDetailId)
        {
            return db.HSSE_Check_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
        }

        /// <summary>
        /// 根据专项检查Id获取所有相关明细
        /// </summary>
        /// <param name="checkItemSetId"></param>
        /// <returns></returns>
        public static List<Model.HSSE_Check_CheckItemDetail> GetCheckItemDetailByCheckItemSetId(string checkItemSetId)
        {
            return (from x in db.HSSE_Check_CheckItemDetail where x.CheckItemSetId == checkItemSetId select x).ToList();
        }

        /// <summary>
        /// 添加专项检查明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void AddCheckItemDetail(Model.HSSE_Check_CheckItemDetail checkItemDetail)
        {
            Model.HSSE_Check_CheckItemDetail newCheckItemDetail = new Model.HSSE_Check_CheckItemDetail();
            newCheckItemDetail.CheckItemDetailId = checkItemDetail.CheckItemDetailId;
            newCheckItemDetail.CheckItemSetId = checkItemDetail.CheckItemSetId;
            newCheckItemDetail.CheckContent = checkItemDetail.CheckContent;
            newCheckItemDetail.SortIndex = checkItemDetail.SortIndex;
            newCheckItemDetail.IsBuiltIn = checkItemDetail.IsBuiltIn;
            db.HSSE_Check_CheckItemDetail.InsertOnSubmit(newCheckItemDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改专项检查明细
        /// </summary>
        /// <param name="checkItemDetail"></param>
        public static void UpdateCheckItemDetail(Model.HSSE_Check_CheckItemDetail checkItemDetail)
        {
            Model.HSSE_Check_CheckItemDetail newCheckItemDetail = db.HSSE_Check_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetail.CheckItemDetailId);
            if (newCheckItemDetail != null)
            {
                newCheckItemDetail.CheckContent = checkItemDetail.CheckContent;
                newCheckItemDetail.SortIndex = checkItemDetail.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专项检查明细
        /// </summary>
        /// <param name="checkItemDetailId"></param>
        public static void DeleteCheckItemDetailById(string checkItemDetailId)
        {
            Model.HSSE_Check_CheckItemDetail checkItemDetail = db.HSSE_Check_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
            if (checkItemDetail != null)
            {
                db.HSSE_Check_CheckItemDetail.DeleteOnSubmit(checkItemDetail);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据专项检查Id删除相关检查内容
        /// </summary>
        /// <param name="checkItemSetId"></param>
        public static void DeleteCheckItemDetailByCheckItemSetId(string checkItemSetId)
        {
            var checkItemDetails = (from x in db.HSSE_Check_CheckItemDetail where x.CheckItemSetId == checkItemSetId select x).ToList();
            if (checkItemDetails != null)
            {
                db.HSSE_Check_CheckItemDetail.DeleteAllOnSubmit(checkItemDetails);
                db.SubmitChanges();
            }
        }
    }
}