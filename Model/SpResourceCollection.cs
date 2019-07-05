using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SpResourceCollection
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 总上传数量
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 总采用数量
        /// </summary>
        public int TotalUsedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 总采用率
        /// </summary>
        public string TotalUsedRate
        {
            get;
            set;
        }

        /// <summary>
        /// 法律法规上传数量
        /// </summary>
        public int LawRegulationCount
        {
            get;
            set;
        }

        /// <summary>
        /// 标准规范上传数量
        /// </summary>
        public int HSSEStandardListCount
        {
            get;
            set;
        }

        /// <summary>
        /// 规章制度上传数量
        /// </summary>
        public int RulesRegulationsCount
        {
            get;
            set;
        }

        /// <summary>
        /// 管理规定上传数量
        /// </summary>
        public int ManageRuleCount
        {
            get;
            set;
        }

        /// <summary>
        /// 培训教材上传数量
        /// </summary>
        public int TrainDBCount
        {
            get;
            set;
        }

        /// <summary>
        /// 安全试题库
        /// </summary>
        public int TrainTestDBCount
        {
            get;
            set;
        }

        /// <summary>
        /// 事故案例库
        /// </summary>
        public int AccidentCaseCount
        {
            get;
            set;
        }

        /// <summary>
        /// 应知应会库
        /// </summary>
        public int KnowledgeDBCount
        {
            get;
            set;
        }

        /// <summary>
        /// 危险源上传数量
        /// </summary>
        public int HazardListCount
        {
            get;
            set;
        }

        /// <summary>
        /// 安全隐患
        /// </summary>
        public int RectifyCount
        {
            get;
            set;
        }

        /// <summary>
        /// HAZOP管理
        /// </summary>
        public int HAZOPCount
        {
            get;
            set;
        }
        /// <summary>
        /// 安全评价
        /// </summary>
        public int AppraiseCount
        {
            get;
            set;
        }
        /// <summary>
        /// 安全专家
        /// </summary>
        public int ExpertCount
        {
            get;
            set;
        }

        /// <summary>
        /// 应急预案上传数量
        /// </summary>
        public int EmergencyCount
        {
            get;
            set;
        }

        /// <summary>
        /// 专项方案上传数量
        /// </summary>
        public int SpecialSchemeCount
        {
            get;
            set;
        }
    }
}
