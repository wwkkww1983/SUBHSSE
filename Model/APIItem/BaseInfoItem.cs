using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BaseInfoItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string BaseInfoId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string BaseInfoCode
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string BaseInfoName
        {
            get;
            set;
        }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }
    }
}
