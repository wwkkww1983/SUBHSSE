using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class HSSE_Hazard_PunishItemService
    {
        /// <summary>
        /// 获取某一处罚基础信息
        /// </summary>
        /// <param name="PunishItemId"></param>
        /// <returns></returns>
        public static Model.HSSE_Hazard_PunishItem GetTitleByPunishItemId(string PunishItemId)
        {
            return Funs.DB.HSSE_Hazard_PunishItem.FirstOrDefault(e => e.PunishItemId == PunishItemId);
        }

        /// <summary>
        /// 添加处罚基础信息
        /// </summary>
        /// <param name="PunishItemName"></param>
        /// <param name="TypeCode"></param>
        public static void AddPunishItem(Model.HSSE_Hazard_PunishItem types)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_PunishItem newTitle = new Model.HSSE_Hazard_PunishItem();
            newTitle.PunishItemId = types.PunishItemId;
            newTitle.PunishItemCode = types.PunishItemCode;
            newTitle.PunishItemType = types.PunishItemType;
            newTitle.PunishItemContent = types.PunishItemContent;
            newTitle.Deduction = types.Deduction;
            newTitle.PunishMoney = types.PunishMoney;

            db.HSSE_Hazard_PunishItem.InsertOnSubmit(newTitle);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改处罚基础信息
        /// </summary>
        /// <param name="PunishItemId"></param>
        /// <param name="PunishItemName"></param>
        /// <param name="TypeCode"></param>
        public static void UpdatePunishItem(Model.HSSE_Hazard_PunishItem types)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_PunishItem newTitle = db.HSSE_Hazard_PunishItem.FirstOrDefault(e => e.PunishItemId == types.PunishItemId);
            if (newTitle != null)
            {
                newTitle.PunishItemCode = types.PunishItemCode;
                newTitle.PunishItemType = types.PunishItemType;
                newTitle.PunishItemContent = types.PunishItemContent;
                newTitle.Deduction = types.Deduction;
                newTitle.PunishMoney = types.PunishMoney;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除职务信息
        /// </summary>
        /// <param name="PunishItemId"></param>
        public static void DeleteTitle(string PunishItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_PunishItem types = db.HSSE_Hazard_PunishItem.FirstOrDefault(e => e.PunishItemId == PunishItemId);
            if (types != null)
            {
                db.HSSE_Hazard_PunishItem.DeleteOnSubmit(types);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取处罚基础项
        /// </summary>
        /// <returns></returns>
        public static List<Model.HSSE_Hazard_PunishItem> GetPunishItemList(string punishItemType)
        {
            return (from x in Funs.DB.HSSE_Hazard_PunishItem where x.PunishItemType == punishItemType orderby x.PunishItemCode select x).ToList();
        }
    }
}
