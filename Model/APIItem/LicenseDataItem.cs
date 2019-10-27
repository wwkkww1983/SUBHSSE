using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 作业票项
    /// </summary>
    public class LicenseDataItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string LicenseId
        {
            get;
            set;
        }
        /// <summary>
        /// 作业票类型
        /// </summary>
        public string LicenseType
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string LicenseCode
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单位ID
        /// </summary>
        public string ApplyUnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单位名称
        /// </summary>
        public string ApplyUnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public string ApplyManId
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplyManName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplyDate
        {
            get;
            set;
        }
        /// <summary>
        /// 作业地点
        /// </summary>
        public string WorkPalce
        {
            get;
            set;
        }
        /// <summary>
        /// 监火人ID
        /// </summary>
        public string FireWatchManId
        {
            get;
            set;
        }
        /// <summary>
        /// 监火人名称
        /// </summary>
        public string FireWatchManName
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public string ValidityStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public string ValidityEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 作业内容、措施
        /// </summary>
        public string WorkMeasures
        {
            get;
            set;
        }
        /// <summary>
        /// 取消人ID
        /// </summary>
        public string CancelManId
        {
            get;
            set;
        }
        /// <summary>
        /// 取消人姓名
        /// </summary>
        public string CancelManName
        {
            get;
            set;
        }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReasons
        {
            get;
            set;
        }
        /// <summary>
        /// 取消时间
        /// </summary>
        public string CancelTime
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭人ID
        /// </summary>
        public string CloseManId
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭人姓名
        /// </summary>
        public string CloseManName
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭原因
        /// </summary>
        public string CloseReasons
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭时间
        /// </summary>
        public string CloseTime
        {
            get;
            set;
        }
        /// <summary>
        /// 下步办理人ID
        /// </summary>
        public string NextManId
        {
            get;
            set;
        }
        /// <summary>
        /// 下步办理人姓名
        /// </summary>
        public string NextManName
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
        /// <summary>
        /// 安全措施明细
        /// </summary>
        public List<LicenseItem> LicenseItems
        {
            get;
            set;
        }
        /// <summary>
        /// 审核流程
        /// </summary>
        public FlowOperateItem FlowOperateItem
        {
            get;
            set;
        }
    }
}
