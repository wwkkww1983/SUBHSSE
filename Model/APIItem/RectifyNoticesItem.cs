using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 隐患整改通知单项
    /// </summary>
    public class RectifyNoticesItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string RectifyNoticesId
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
        public string RectifyNoticesCode
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
        /// 区域ID
        /// </summary>
        public string WorkAreaId
        {
            get;
            set;
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string WorkAreaName
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人ID
        /// </summary>
        public string CheckPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 检查人姓名
        /// </summary>
        public string CheckPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 受检时间
        /// </summary>
        public string CheckedDate
        {
            get;
            set;
        }
        /// <summary>
        /// 安全隐患内容及整改意见
        /// </summary>
        public string WrongContent
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人ID
        /// </summary>
        public string SignPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 签发人姓名
        /// </summary>
        public string SignPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 签发日期
        /// </summary>
        public string SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 整改措施和完成情况
        /// </summary>
        public string CompleteStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 责任人ID
        /// </summary>
        public string DutyPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 责任人姓名
        /// </summary>
        public string DutyPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 整改日期
        /// </summary>
        public string CompleteDate
        {
            get;
            set;
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsRectify
        {
            get;
            set;
        }
       
        /// <summary>
        /// 复查日期
        /// </summary>
        public string ReCheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 图片/附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
    }
}
