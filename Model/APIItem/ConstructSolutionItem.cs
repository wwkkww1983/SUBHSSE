using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ConstructSolutionItem
    {
       /// <summary>
       /// 方案ID
       /// </summary>
       public string ConstructSolutionId
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
        public string ConstructSolutionCode
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string ConstructSolutionName
        {
            get;
            set;
        }
        /// <summary>
        /// 方案类型
        /// </summary>
        public string SolutinTypeName
        {
           get;
           set;
       }
        /// <summary>
        /// 内容
        /// </summary>
        public string FileContents
        {
            get;
            set;
        }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
    }
}
