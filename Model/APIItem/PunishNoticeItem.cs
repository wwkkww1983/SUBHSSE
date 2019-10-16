using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 处罚通知单数据项
    /// </summary>
    public class PunishNoticeItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string PunishNoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PunishNoticeCode
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
        /// 处罚单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNum
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚时间
        /// </summary>
        public string PunishNoticeDate
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚根据
        /// </summary>
        public string BasicItem
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚原因
        /// </summary>
        public string IncentiveReason
        {
            get;
            set;
        }
        /// <summary>
        /// 处罚金额
        /// </summary>
        public decimal? PunishMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 有关证据（被奖罚人姓名、岗位、证件编号、有关文字描述或照片等）：
        /// </summary>
        public string FileContents
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CompileManId
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人姓名
        /// </summary>
        public string CompileManName
        {
            get;
            set;
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public string CompileDate
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人ID
        /// </summary>
        public string SignManId
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人姓名
        /// </summary>
        public string SignManName
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人ID
        /// </summary>
        public string ApproveManId
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string ApproveManName
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
        /// 通知单附件
        /// </summary>
        public string PunishUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 回执单附件
        /// </summary>
        public string ReceiptUrl
        {
            get;
            set;
        }
    }
}
