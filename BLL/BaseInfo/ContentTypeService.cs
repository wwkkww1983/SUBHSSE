using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ContentTypeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取交流类型信息
        /// </summary>
        /// <param name="contentTypeId">交流类型Id</param>
        /// <returns></returns>
        public static Model.Exchange_ContentType GetContentType(string contentTypeId)
        {
            return Funs.DB.Exchange_ContentType.FirstOrDefault(x => x.ContentTypeId == contentTypeId);
        }

        /// <summary>
        /// 增加交流类型
        /// </summary>
        /// <param name="contentTypeName"></param>
        /// <param name="def"></param>
        public static void AddContentType(Model.Exchange_ContentType contentType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ContentType newContentType = new Model.Exchange_ContentType
            {
                ContentTypeId = contentType.ContentTypeId,
                ContentTypeCode = contentType.ContentTypeCode,
                ContentTypeName = contentType.ContentTypeName
            };

            db.Exchange_ContentType.InsertOnSubmit(newContentType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改交流类型信息
        /// </summary>
        /// <param name="contentTypeId"></param>
        /// <param name="contentTypeName"></param>
        /// <param name="def"></param>
        public static void UpdateContentType(Model.Exchange_ContentType contentType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ContentType newContentType = db.Exchange_ContentType.FirstOrDefault(e => e.ContentTypeId == contentType.ContentTypeId);
            if (newContentType != null)
            {
                newContentType.ContentTypeCode = contentType.ContentTypeCode;
                newContentType.ContentTypeName = contentType.ContentTypeName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除交流类型
        /// </summary>
        /// <param name="contentTypeId"></param>
        public static void DeleteContentType(string contentTypeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ContentType contentType = db.Exchange_ContentType.FirstOrDefault(e => e.ContentTypeId == contentTypeId);
            if (contentType != null)
            {
                db.Exchange_ContentType.DeleteOnSubmit(contentType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断是否存在交流类型
        /// </summary>
        /// <param name="contentTypeId">交流类型</param>
        /// <returns>true:存在；false:不存在</returns>
        public static bool IsExistContentType(string contentTypeId)
        {
            Model.Exchange_ContentType m = Funs.DB.Exchange_ContentType.FirstOrDefault(e => e.ContentTypeId == contentTypeId);
            if (m != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取交流类型名称项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Exchange_ContentType> GetContentTypeLists()
        {
            var list = (from x in Funs.DB.Exchange_ContentType orderby x.ContentTypeCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取交流类型下拉选项（含最新）
        /// </summary>
        /// <returns></returns>
        public static List<Model.Exchange_ContentType> GetContentTypeListAndNew()
        {
            var q = (from x in Funs.DB.Exchange_ContentType orderby x.ContentTypeCode select x).ToList();
            List<Model.Exchange_ContentType> list = new List<Model.Exchange_ContentType>();
            Model.Exchange_ContentType c = new Exchange_ContentType
            {
                ContentTypeId = "最新",
                ContentTypeName = "最新"
            };
            list.Add(c);
            for (int i = 0; i < q.Count(); i++)
            {
                list.Add(q[i]);
            }
            return list;
        }
    }
}
