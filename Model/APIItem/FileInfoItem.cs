using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 文档类信息项
    /// </summary>
    public class FileInfoItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string FileId
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
        public string FileCode
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string FileType
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
        /// 内容
        /// </summary>
        public string FileContent
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
        /// 附件
        /// </summary>
        public string AttachUrl
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
    }
}
