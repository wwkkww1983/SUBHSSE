using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 气瓶出场报批明细表
    /// </summary>
    public static class GasCylinderOutItemService
    {
      public static  Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键删除气瓶出场报批明细
        /// </summary>
        /// <param name="gasCylinderOutItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GasCylinderOutItem GetGasCylinderOutItemById(string gasCylinderOutItemId)
        {
            return Funs.DB.InApproveManager_GasCylinderOutItem.FirstOrDefault(e => e.GasCylinderOutItemId == gasCylinderOutItemId);
        }

        /// <summary>
        /// 根据气瓶出场报批ID获取所有相关明细信息
        /// </summary>
        /// <param name="gasCylinderOutId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_GasCylinderOutItem> GetGasCylinderOutItemByGasCylinderOutId(string gasCylinderOutId)
        {
            return (from x in Funs.DB.InApproveManager_GasCylinderOutItem where x.GasCylinderOutId == gasCylinderOutId select x).ToList();
        }

        /// <summary>
        /// 添加气瓶出场报批明细
        /// </summary>
        /// <param name="gasCylinderOutItem"></param>
        public static void AddGasCylinderOutItem(Model.InApproveManager_GasCylinderOutItem gasCylinderOutItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOutItem newGasCylinderOutItem = new Model.InApproveManager_GasCylinderOutItem
            {
                GasCylinderOutItemId = gasCylinderOutItem.GasCylinderOutItemId,
                GasCylinderOutId = gasCylinderOutItem.GasCylinderOutId,
                GasCylinderId = gasCylinderOutItem.GasCylinderId,
                GasCylinderNum = gasCylinderOutItem.GasCylinderNum
            };
            db.InApproveManager_GasCylinderOutItem.InsertOnSubmit(newGasCylinderOutItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改气瓶出场报批明细
        /// </summary>
        /// <param name="gasCylinderOutItem"></param>
        public static void UpdateGasCylinderOutItem(Model.InApproveManager_GasCylinderOutItem gasCylinderOutItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOutItem newGasCylinderOutItem = db.InApproveManager_GasCylinderOutItem.FirstOrDefault(e => e.GasCylinderOutItemId == gasCylinderOutItem.GasCylinderOutItemId);
            if (newGasCylinderOutItem != null)
            {
                newGasCylinderOutItem.GasCylinderOutId = gasCylinderOutItem.GasCylinderOutId;
                newGasCylinderOutItem.GasCylinderId = gasCylinderOutItem.GasCylinderId;
                newGasCylinderOutItem.GasCylinderNum = gasCylinderOutItem.GasCylinderNum;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除气瓶出场报批明细
        /// </summary>
        /// <param name="gasCylinderOutItemId"></param>
        public static void DeleteGasCylinderOutItemById(string gasCylinderOutItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderOutItem gasCylinderOutItem = db.InApproveManager_GasCylinderOutItem.FirstOrDefault(e => e.GasCylinderOutItemId == gasCylinderOutItemId);
            if (gasCylinderOutItem != null)
            {
                db.InApproveManager_GasCylinderOutItem.DeleteOnSubmit(gasCylinderOutItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据气瓶出场报批ID删除所有相关明细信息
        /// </summary>
        /// <param name="gasCylinderOutId"></param>
        public static void DeleteGasCylinderOutItemByGasCylinderOutId(string gasCylinderOutId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.InApproveManager_GasCylinderOutItem where x.GasCylinderOutId == gasCylinderOutId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_GasCylinderOutItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}