using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 气瓶入场报批明细表
    /// </summary>
    public static class GasCylinderInItemService
    {
       public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键删除气瓶入场报批明细
        /// </summary>
        /// <param name="gasCylinderInItemId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GasCylinderInItem GetGasCylinderInItemById(string gasCylinderInItemId)
        {
            return Funs.DB.InApproveManager_GasCylinderInItem.FirstOrDefault(e => e.GasCylinderInItemId == gasCylinderInItemId);
        }

        /// <summary>
        /// 根据气瓶入场报批主键获取所有相关明细信息
        /// </summary>
        /// <param name="gasCylinderInId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_GasCylinderInItem> GetGasCylinderInItemByGasCylinderInId(string gasCylinderInId)
        {
            return (from x in Funs.DB.InApproveManager_GasCylinderInItem where x.GasCylinderInId == gasCylinderInId select x).ToList();
        }

        /// <summary>
        /// 添加气瓶入场报批明细
        /// </summary>
        /// <param name="gasCylinderInItem"></param>
        public static void AddGasCylinderInItem(Model.InApproveManager_GasCylinderInItem gasCylinderInItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderInItem newGasCylinderInItem = new Model.InApproveManager_GasCylinderInItem
            {
                GasCylinderInItemId = gasCylinderInItem.GasCylinderInItemId,
                GasCylinderInId = gasCylinderInItem.GasCylinderInId,
                GasCylinderId = gasCylinderInItem.GasCylinderId,
                GasCylinderNum = gasCylinderInItem.GasCylinderNum,
                PM_IsFull = gasCylinderInItem.PM_IsFull,
                FZQ_IsFull = gasCylinderInItem.FZQ_IsFull,
                IsSameCar = gasCylinderInItem.IsSameCar
            };
            db.InApproveManager_GasCylinderInItem.InsertOnSubmit(newGasCylinderInItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改气瓶入场报批明细
        /// </summary>
        /// <param name="gasCylinderInItem"></param>
        public static void UpdateGasCylinderInItem(Model.InApproveManager_GasCylinderInItem gasCylinderInItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderInItem newGasCylinderInItem = db.InApproveManager_GasCylinderInItem.FirstOrDefault(e => e.GasCylinderInItemId == gasCylinderInItem.GasCylinderInItemId);
            if (newGasCylinderInItem != null)
            {
                newGasCylinderInItem.GasCylinderInId = gasCylinderInItem.GasCylinderInId;
                newGasCylinderInItem.GasCylinderId = gasCylinderInItem.GasCylinderId;
                newGasCylinderInItem.GasCylinderNum = gasCylinderInItem.GasCylinderNum;
                newGasCylinderInItem.PM_IsFull = gasCylinderInItem.PM_IsFull;
                newGasCylinderInItem.FZQ_IsFull = gasCylinderInItem.FZQ_IsFull;
                newGasCylinderInItem.IsSameCar = gasCylinderInItem.IsSameCar;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除气瓶入场报批明细
        /// </summary>
        /// <param name="gasCylinderInItemId"></param>
        public static void DeleteGasCylinderInItemById(string gasCylinderInItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GasCylinderInItem gasCylinderInItem = db.InApproveManager_GasCylinderInItem.FirstOrDefault(e => e.GasCylinderInItemId == gasCylinderInItemId);
            if (gasCylinderInItemId != null)
            {
                db.InApproveManager_GasCylinderInItem.DeleteOnSubmit(gasCylinderInItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据气瓶入场报批ID删除所有相关明细信息
        /// </summary>
        /// <param name="gasCylinderInId"></param>
        public static void DeleteGasCylinderInItemByGasCylinderInId(string gasCylinderInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.InApproveManager_GasCylinderInItem where x.GasCylinderInId == gasCylinderInId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_GasCylinderInItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
