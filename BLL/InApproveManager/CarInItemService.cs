using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种车辆入场审批明细
    /// </summary>
    public static class CarInItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主表ID获取明细信息
        /// </summary>
        /// <param name="carInId"></param>
        /// <returns></returns>
        public static List<Model.InApproveManager_CarInItem> GetCarInItemByCarInId(string carInId)
        {
            return (from x in Funs.DB.InApproveManager_CarInItem where x.CarInId == carInId select x).ToList();
        }

        /// <summary>
        /// 添加特种车辆入场明细
        /// </summary>
        /// <param name="carInItem"></param>
        public static void AddCarInItem(Model.InApproveManager_CarInItem carInItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_CarInItem newCarInItem = new Model.InApproveManager_CarInItem
            {
                CarInItemId = carInItem.CarInItemId,
                CarInId = carInItem.CarInId,
                CheckItem1 = carInItem.CheckItem1,
                CheckItem2 = carInItem.CheckItem2,
                CheckItem3 = carInItem.CheckItem3,
                CheckItem4 = carInItem.CheckItem4,
                CheckItem5 = carInItem.CheckItem5,
                CheckItem6 = carInItem.CheckItem6,
                CheckItem7 = carInItem.CheckItem7,
                CheckItem8 = carInItem.CheckItem8,
                CheckItem9 = carInItem.CheckItem9,
                CheckItem10 = carInItem.CheckItem10,
                CheckItem11 = carInItem.CheckItem11,
                CheckItem12 = carInItem.CheckItem12,
                CheckItem13 = carInItem.CheckItem13
            };
            db.InApproveManager_CarInItem.InsertOnSubmit(newCarInItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据特种车辆入场审批ID删除相关明细信息
        /// </summary>
        /// <param name="carInId"></param>
        public static void DeleteCarInItemByCarInId(string carInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.InApproveManager_CarInItem where x.CarInId == carInId select x).ToList();
            if (q != null)
            {
                db.InApproveManager_CarInItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
