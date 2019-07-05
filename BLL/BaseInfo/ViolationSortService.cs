using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 违章种类
    /// </summary>
    public static class ViolationSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取违章种类
        /// </summary>
        /// <param name="violationSortId"></param>
        /// <returns></returns>
        public static Model.Base_ViolationSort GetViolationSortById(string violationSortId)
        {
            return Funs.DB.Base_ViolationSort.FirstOrDefault(e => e.ViolationSortId == violationSortId);
        }

        /// <summary>
        /// 添加违章种类
        /// </summary>
        /// <param name="violationSort"></param>
        public static void AddViolationSort(Model.Base_ViolationSort violationSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ViolationSort newViolationSort = new Model.Base_ViolationSort
            {
                ViolationSortId = violationSort.ViolationSortId,
                ViolationSortCode = violationSort.ViolationSortCode,
                ViolationSortName = violationSort.ViolationSortName,
                Remark = violationSort.Remark
            };
            db.Base_ViolationSort.InsertOnSubmit(newViolationSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改违章种类
        /// </summary>
        /// <param name="violationSort"></param>
        public static void UpdateViolationSort(Model.Base_ViolationSort violationSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ViolationSort newViolationSort = db.Base_ViolationSort.FirstOrDefault(e => e.ViolationSortId == violationSort.ViolationSortId);
            if (newViolationSort != null)
            {
                newViolationSort.ViolationSortCode = violationSort.ViolationSortCode;
                newViolationSort.ViolationSortName = violationSort.ViolationSortName;
                newViolationSort.Remark = violationSort.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除违章种类
        /// </summary>
        /// <param name="violationSortId"></param>
        public static void DeleteViolationSortById(string violationSortId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ViolationSort violationSort = db.Base_ViolationSort.FirstOrDefault(e => e.ViolationSortId == violationSortId);
            if (violationSort != null)
            {
                db.Base_ViolationSort.DeleteOnSubmit(violationSort);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取违章种类下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ViolationSort> GetViolationSortList()
        {
            return (from x in Funs.DB.Base_ViolationSort orderby x.ViolationSortCode select x).ToList();
        }
    }
}
