using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全组织体系
    /// </summary>
    public static class HSSEOrganizeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全组织体系
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_HSSEOrganize GetHSSEOrganizeById(string organizeId)
        {
            return Funs.DB.HSSESystem_HSSEOrganize.FirstOrDefault(e => e.HSSEOrganizeId == organizeId);
        }
        
        /// <summary>
        /// 根据上级Id获取安全组织体系集合
        /// </summary>
        /// <param name="supHSSEOrganizeId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_HSSEOrganize GetHSSEOrganizeByUnitId(string unitId)
        {
            return Funs.DB.HSSESystem_HSSEOrganize.FirstOrDefault(e => e.UnitId == unitId);
        }

        /// <summary>
        /// 添加安全组织体系
        /// </summary>
        /// <param name="organize"></param>
        public static void AddHSSEOrganize(Model.HSSESystem_HSSEOrganize organize)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEOrganize newHSSEOrganize = new Model.HSSESystem_HSSEOrganize
            {
                HSSEOrganizeId = organize.HSSEOrganizeId,
                UnitId = organize.UnitId,
                Remark = organize.Remark,
                AttachUrl = organize.AttachUrl,
                SeeFile = organize.SeeFile
            };
            db.HSSESystem_HSSEOrganize.InsertOnSubmit(newHSSEOrganize);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全组织体系
        /// </summary>
        /// <param name="organize"></param>
        public static void UpdateHSSEOrganize(Model.HSSESystem_HSSEOrganize organize)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEOrganize newHSSEOrganize = db.HSSESystem_HSSEOrganize.FirstOrDefault(e => e.HSSEOrganizeId == organize.HSSEOrganizeId);
            if (newHSSEOrganize != null)
            {
                newHSSEOrganize.Remark = organize.Remark;
                newHSSEOrganize.AttachUrl = organize.AttachUrl;
                newHSSEOrganize.SeeFile = organize.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全组织体系
        /// </summary>
        /// <param name="organizeId"></param>
        public static void DeleteHSSEOrganize(string organizeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEOrganize organize = db.HSSESystem_HSSEOrganize.FirstOrDefault(e => e.HSSEOrganizeId == organizeId);
            if (organize != null)
            {
                if (!string.IsNullOrEmpty(organize.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, organize.AttachUrl);//删除附件
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(organize.HSSEOrganizeId);

                db.HSSESystem_HSSEOrganize.DeleteOnSubmit(organize);
                db.SubmitChanges();
            }
        }

        ///// <summary>
        ///// 是否可删除资源节点
        ///// </summary>
        ///// <param name="postName"></param>
        ///// <returns>true-可以，false-不可以</returns>
        //public static bool IsDeleteOrganize(string organizeById)
        //{
        //    bool isDelete = true;
        //    var organize = BLL.HSSEOrganizeService.GetHSSEOrganizeById(organizeById);
        //    if (organize != null)
        //    {
        //        var supItemSetCount = BLL.HSSEOrganizeService.GetHSSEOrganizeBySupHSSEOrganizeId(organizeById);
        //        if (supItemSetCount.Count() > 0)
        //        {
        //            isDelete = false;
        //        }
        //    }
        //    return isDelete;
        //}
    }
}