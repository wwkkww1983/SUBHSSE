using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ContentService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取交流帖子
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public static Model.Exchange_Content GetContentById(string contentId)
        {
            return Funs.DB.Exchange_Content.FirstOrDefault(e => e.ContentId == contentId);
        }

        /// <summary>
        /// 添加交流帖子
        /// </summary>
        /// <param name="content"></param>
        public static void AddContent(Model.Exchange_Content content)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_Content newContent = new Model.Exchange_Content
            {
                ContentId = content.ContentId,
                ContentTypeId = content.ContentTypeId,
                Theme = content.Theme,
                Contents = content.Contents,
                CompileMan = content.CompileMan,
                CompileDate = content.CompileDate,
                AttachUrl = content.AttachUrl
            };
            db.Exchange_Content.InsertOnSubmit(newContent);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改交流帖子
        /// </summary>
        /// <param name="content"></param>
        public static void UpdateContent(Model.Exchange_Content content)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_Content newContent = db.Exchange_Content.FirstOrDefault(e => e.ContentId == content.ContentId);
            if (newContent != null)
            {
                newContent.ContentTypeId = content.ContentTypeId;
                newContent.Theme = content.Theme;
                newContent.Contents = content.Contents;
                newContent.CompileDate = content.CompileDate;
                newContent.AttachUrl = content.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除交流帖子
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteContentById(string contentId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_Content content = db.Exchange_Content.FirstOrDefault(e => e.ContentId == contentId);
            if (content != null)
            {
                if (!string.IsNullOrEmpty(content.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, content.AttachUrl);
                }
                db.Exchange_Content.DeleteOnSubmit(content);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取某一交流类型下是否不存在帖子信息
        /// </summary>
        /// <returns></returns>
        public static bool IsExitContentType(string contentTypeId)
        {
            return (from x in Funs.DB.Exchange_Content where x.ContentTypeId == contentTypeId select x).Count() == 0;
        }
    }
}
