using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BLL
{
    /// <summary>
    /// 管理人员资质明细
    /// </summary>
    public class ManagePersonQualityItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据人员资质ID获取相关明细信息
        /// </summary>
        /// <param name="managePersonQualityId"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_ManagePersonQualityItem> GetManagePersonQualityItemById(string managePersonQualityId)
        {
            return (from x in db.QualityAudit_ManagePersonQualityItem where x.ManagePersonQualityId == managePersonQualityId select x).ToList();
        }

        /// <summary>
        /// 添加管理人员资质明细
        /// </summary>
        /// <param name="item"></param>
        public static void AddManagePersonQualityItem(Model.QualityAudit_ManagePersonQualityItem item)
        {
            Model.QualityAudit_ManagePersonQualityItem newItem = new Model.QualityAudit_ManagePersonQualityItem();
            newItem.ManagePersonQualityItemId = item.ManagePersonQualityItemId;
            newItem.ManagePersonQualityId = item.ManagePersonQualityId;
            newItem.CertificateNo = item.CertificateNo;
            newItem.CertificateName = item.CertificateName;
            newItem.SendUnit = item.SendUnit;
            newItem.SendDate = item.SendDate;
            newItem.LimitDate = item.LimitDate;
            db.QualityAudit_ManagePersonQualityItem.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据人员资质主键删除所有相关明细信息
        /// </summary>
        /// <param name="managePersonQualityId"></param>
        public static void DeleteManagePersonQualityItemByManagePersonQualityId(string managePersonQualityId)
        {
            var q = (from x in db.QualityAudit_ManagePersonQualityItem where x.ManagePersonQualityId == managePersonQualityId select x).ToList();
            if (q != null)
            {
                db.QualityAudit_ManagePersonQualityItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
