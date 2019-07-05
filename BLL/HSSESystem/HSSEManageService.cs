using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全管理机构
    /// </summary>
    public static class HSSEManageService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全管理机构
        /// </summary>
        /// <param name="manageId"></param>
        /// <returns></returns>
        public static Model.HSSESystem_HSSEManage GetHSSEManageById(string manageId)
        {
            return Funs.DB.HSSESystem_HSSEManage.FirstOrDefault(e => e.HSSEManageId == manageId);
        }

        /// <summary>
        /// 根据上一节点id获取安全管理机构
        /// </summary>
        /// <param name="supHSSEManageId"></param>
        /// <returns></returns>
        public static List<Model.HSSESystem_HSSEManage> GetHSSEManageBySupHSSEManageId(string supHSSEManageId)
        {
            return (from x in Funs.DB.HSSESystem_HSSEManage where x.SupHSSEManageId == supHSSEManageId orderby x.HSSEManageCode select x).ToList();
        }

        /// <summary>
        /// 添加安全管理机构
        /// </summary>
        /// <param name="manage"></param>
        public static void AddHSSEManage(Model.HSSESystem_HSSEManage manage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManage newHSSEManage = new Model.HSSESystem_HSSEManage
            {
                HSSEManageId = manage.HSSEManageId,
                HSSEManageCode = manage.HSSEManageCode,
                HSSEManageName = manage.HSSEManageName,
                SupHSSEManageId = manage.SupHSSEManageId
            };
            db.HSSESystem_HSSEManage.InsertOnSubmit(newHSSEManage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全管理机构
        /// </summary>
        /// <param name="manage"></param>
        public static void UpdateHSSEManage(Model.HSSESystem_HSSEManage manage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManage newHSSEManage = db.HSSESystem_HSSEManage.FirstOrDefault(e => e.HSSEManageId == manage.HSSEManageId);
            if (newHSSEManage != null)
            {
                newHSSEManage.HSSEManageCode = manage.HSSEManageCode;
                newHSSEManage.HSSEManageName = manage.HSSEManageName;
                newHSSEManage.SupHSSEManageId = manage.SupHSSEManageId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全管理机构
        /// </summary>
        /// <param name="manageId"></param>
        public static void DeleteHSSEManage(string manageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSESystem_HSSEManage hsseMange = db.HSSESystem_HSSEManage.FirstOrDefault(e => e.HSSEManageId == manageId);
            if (hsseMange != null)
            {               
                db.HSSESystem_HSSEManage.DeleteOnSubmit(hsseMange);
                db.SubmitChanges();
            }
        }
    }
}