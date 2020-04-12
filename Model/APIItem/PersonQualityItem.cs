using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PersonQualityItem
    {
        /// <summary>
        /// 人员特岗资质ID
        /// </summary>
        public string PersonQualityId
        {
            get;
            set;
        }
        /// <summary>
        /// 人员主键ID
        /// </summary>
        public string PersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard
        {
            get;
            set;
        }
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
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
        /// 单位代号
        /// </summary>
        public string UnitCode
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
        /// 证书ID
        /// </summary>
        public string CertificateId
        {
            get;
            set;
        }
        /// <summary>
        /// 证书名称
        /// </summary>
        public string CertificateName
        {
            get;
            set;
        }
        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertificateNo
        {
            get;
            set;
        }
        /// <summary>
        /// 操作类别
        /// </summary>
        public string Grade
        {
            get;
            set;
        }
        /// <summary>
        /// 发证单位
        /// </summary>
        public string SendUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 发证时间
        /// </summary>
        public string SendDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public string LimitDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? LimitDateD
        {
            get;
            set;
        }
        /// <summary>
        /// 最近复查日期
        /// </summary>
        public string LateCheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovalPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人
        /// </summary>
        public string CompileMan
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
        /// 审核日期
        /// </summary>
        public string AuditDate
        {
            get;
            set;
        }
        /// <summary>
        /// 证书附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
    }
}
