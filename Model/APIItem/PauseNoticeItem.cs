using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 工程暂停令数据项
    /// </summary>
    public class PauseNoticeItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string PauseNoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PauseNoticeCode
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
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 编制时间
        /// </summary>
        public string CompileDate
        {
            get;
            set;
        }
        ///// <summary>
        ///// 编制人
        ///// </summary>
        //public string SignPerson
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 工程部位
        /// </summary>
        public string ProjectPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 安全和质量隐患内容
        /// </summary>
        public string WrongContent
        {
            get;
            set;
        }
        /// <summary>
        /// 暂停时间
        /// </summary>
        public string PauseTime
        {
            get;
            set;
        }
        /// <summary>
        /// 暂停作业项
        /// </summary>
        public string PauseContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第一条内容
        /// </summary>
        public string OneContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第二条内容
        /// </summary>
        public string SecondContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第三条内容
        /// </summary>
        public string ThirdContent
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理确认人
        /// </summary>
        public string ProjectHeadConfirm
        {
            get;
            set;
        }
        /// <summary>
        /// 是否确认
        /// </summary>
        public bool? IsConfirm
        {
            get;
            set;
        }
        /// <summary>
        /// 是否确认
        /// </summary>
        public string IsConfirmName
        {
            get;
            set;
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public string ConfirmDate
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl
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
        public string HazardRegisterId
        {
            get;
            set;
        }
    }
}
