using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 企业安全管理资料考核
    /// </summary>
    public static class SafetyDataCheckService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取企业安全管理资料考核
        /// </summary>
        /// <param name="SafetyDataCheckId"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataCheck GetSafetyDataCheckById(string SafetyDataCheckId)
        {
            return Funs.DB.SafetyData_SafetyDataCheck.FirstOrDefault(e => e.SafetyDataCheckId == SafetyDataCheckId);
        }

        /// <summary>
        /// 添加企业安全管理资料考核
        /// </summary>
        /// <param name="SafetyDataCheck"></param>
        public static void AddSafetyDataCheck(Model.SafetyData_SafetyDataCheck SafetyDataCheck)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheck newSafetyDataCheck = new Model.SafetyData_SafetyDataCheck
            {
                SafetyDataCheckId = SafetyDataCheck.SafetyDataCheckId,
                Code = SafetyDataCheck.Code,
                Title = SafetyDataCheck.Title,
                StartDate = SafetyDataCheck.StartDate,
                EndDate = SafetyDataCheck.EndDate,
                CompileDate = SafetyDataCheck.CompileDate,
                CompileMan = SafetyDataCheck.CompileMan
            };
            db.SafetyData_SafetyDataCheck.InsertOnSubmit(newSafetyDataCheck);
            db.SubmitChanges();           
        }

        /// <summary>
        /// 修改企业安全管理资料考核
        /// </summary>
        /// <param name="SafetyDataCheck"></param>
        public static void UpdateSafetyDataCheck(Model.SafetyData_SafetyDataCheck SafetyDataCheck)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheck newSafetyDataCheck = db.SafetyData_SafetyDataCheck.FirstOrDefault(e => e.SafetyDataCheckId == SafetyDataCheck.SafetyDataCheckId);
            if (newSafetyDataCheck != null)
            {
                //newSafetyDataCheck.ProjectId = SafetyDataCheck.ProjectId;
                newSafetyDataCheck.Code = SafetyDataCheck.Code;
                newSafetyDataCheck.Title = SafetyDataCheck.Title;
                newSafetyDataCheck.StartDate = SafetyDataCheck.StartDate;
                newSafetyDataCheck.EndDate = SafetyDataCheck.EndDate;
                newSafetyDataCheck.CompileDate = SafetyDataCheck.CompileDate;
                newSafetyDataCheck.CompileMan = SafetyDataCheck.CompileMan;  
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除企业安全管理资料考核
        /// </summary>
        /// <param name="SafetyDataCheckId"></param>
        public static void DeleteSafetyDataCheckById(string SafetyDataCheckId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataCheck SafetyDataCheck = db.SafetyData_SafetyDataCheck.FirstOrDefault(e => e.SafetyDataCheckId == SafetyDataCheckId);
            if (SafetyDataCheck != null)
            {
                ///页面判断了是否存在明细 存在则不删除 此方法注销
                BLL.SafetyDataCheckItemService.DeleteSafetyDataCheckItemBySafetyDataCheckId(SafetyDataCheck.SafetyDataCheckId); ///删除明细信息             
                db.SafetyData_SafetyDataCheck.DeleteOnSubmit(SafetyDataCheck);
                db.SubmitChanges();
            }
        }
    }
}
