using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 违章处理表
    /// </summary>
    public static class ViolationRuleService
    {
        /// <summary>
        /// 根据主键获取违章处理
        /// </summary>
        /// <param name="violationRuleId"></param>
        /// <returns></returns>
        public static Model.Base_ViolationRule GetViolationRuleById(int? violationRuleId)
        {
            return Funs.DB.Base_ViolationRule.FirstOrDefault(e => e.ViolationRuleId == violationRuleId);
        }

        /// <summary>
        /// 获取违章处理下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ViolationRule> GetViolationRuleList()
        {
            return (from x in Funs.DB.Base_ViolationRule select x).ToList();
        }
    }
}
