using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 普通车辆入场审批明细
    /// </summary>
   public static class GeneralCarInItemService
   {
       public static Model.SUBHSSEDB db = Funs.DB;

       /// <summary>
       /// 根据主表ID获取明细信息
       /// </summary>
       /// <param name="GeneralCarInId"></param>
       /// <returns></returns>
       public static List<Model.InApproveManager_GeneralCarInItem> GetGeneralCarInItemByGeneralCarInId(string GeneralCarInId)
       {
           return (from x in Funs.DB.InApproveManager_GeneralCarInItem where x.GeneralCarInId == GeneralCarInId select x).ToList();
       }

       /// <summary>
       /// 添加普通车辆入场明细
       /// </summary>
       /// <param name="GeneralCarInItem"></param>
       public static void AddGeneralCarInItem(Model.InApproveManager_GeneralCarInItem GeneralCarInItem)
       {
           Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GeneralCarInItem newGeneralCarInItem = new Model.InApproveManager_GeneralCarInItem
            {
                GeneralCarInItemId = GeneralCarInItem.GeneralCarInItemId,
                GeneralCarInId = GeneralCarInItem.GeneralCarInId,
                CheckItem1 = GeneralCarInItem.CheckItem1,
                CheckItem2 = GeneralCarInItem.CheckItem2,
                CheckItem3 = GeneralCarInItem.CheckItem3,
                CheckItem4 = GeneralCarInItem.CheckItem4,
                CheckItem5 = GeneralCarInItem.CheckItem5,
                CheckItem6 = GeneralCarInItem.CheckItem6,
                CheckItem7 = GeneralCarInItem.CheckItem7,
                CheckItem8 = GeneralCarInItem.CheckItem8,
                CheckItem9 = GeneralCarInItem.CheckItem9,
                CheckItem10 = GeneralCarInItem.CheckItem10,
                CheckItem11 = GeneralCarInItem.CheckItem11,
                CheckItem12 = GeneralCarInItem.CheckItem12,
                CheckItem13 = GeneralCarInItem.CheckItem13
            };
            db.InApproveManager_GeneralCarInItem.InsertOnSubmit(newGeneralCarInItem);
           db.SubmitChanges();
       }

       /// <summary>
       /// 根据普通车辆入场审批ID删除相关明细信息
       /// </summary>
       /// <param name="GeneralCarInId"></param>
       public static void DeleteGeneralCarInItemByGeneralCarInId(string GeneralCarInId)
       {
           Model.SUBHSSEDB db = Funs.DB;
           var q = (from x in db.InApproveManager_GeneralCarInItem where x.GeneralCarInId == GeneralCarInId select x).ToList();
           if (q != null)
           {
               db.InApproveManager_GeneralCarInItem.DeleteAllOnSubmit(q);
               db.SubmitChanges();
           }
       }
   }
}
