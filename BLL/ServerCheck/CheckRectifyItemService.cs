using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全监督检查整改明细表
    /// </summary>
    public class CheckRectifyItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查整改明细信息
        /// </summary>
        /// <param name="CheckRectifyItemId"></param>
        /// <returns></returns>
        public static Model.Check_CheckRectifyItem GetCheckRectifyItemByTable5ItemId(string table5ItemId)
        {
            return Funs.DB.Check_CheckRectifyItem.FirstOrDefault(e => e.Table5ItemId == table5ItemId);
        }

        /// <summary>
        /// 根据安全监督检查整改id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckRectifyItem> GetCheckRectifyItemByCheckRectifyId(string checkRectifyId)
        {
            return (from x in Funs.DB.Check_CheckRectifyItem where x.CheckRectifyId == checkRectifyId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取安全监督检查整改明细信息
        /// </summary>
        /// <param name="checkRectifyItemId"></param>
        /// <returns></returns>
        public static Model.Check_CheckRectifyItem GetCheckRectifyItemByCheckRectifyItemId(string checkRectifyItemId)
        {
            return Funs.DB.Check_CheckRectifyItem.FirstOrDefault(e => e.CheckRectifyItemId == checkRectifyItemId);
        }

        /// <summary>
        /// 添加安全监督检查整改明细信息
        /// </summary>
        /// <param name="CheckRectifyItem"></param>
        public static void UpdateCheckRectifyItem(Model.Check_CheckRectifyItem CheckRectifyItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newCheckRectifyItem = db.Check_CheckRectifyItem.FirstOrDefault(x => x.CheckRectifyItemId == CheckRectifyItem.CheckRectifyItemId);
            if (newCheckRectifyItem != null)
            {
                newCheckRectifyItem.OrderEndPerson = CheckRectifyItem.OrderEndPerson;
                newCheckRectifyItem.RealEndDate = CheckRectifyItem.RealEndDate;
                newCheckRectifyItem.Verification = CheckRectifyItem.Verification;
                db.SubmitChanges();
            }
        }
    }
}
