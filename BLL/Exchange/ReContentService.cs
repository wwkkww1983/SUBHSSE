using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ReContentService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取帖子回复
        /// </summary>
        /// <param name="lawRegulationId"></param>
        /// <returns></returns>
        public static Model.Exchange_ReContent GetReContentById(string reContentId)
        {
            return Funs.DB.Exchange_ReContent.FirstOrDefault(e => e.ReContentId == reContentId);
        }

        /// <summary>
        /// 添加帖子回复
        /// </summary>
        /// <param name="reContent"></param>
        public static void AddReContent(Model.Exchange_ReContent reContent)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ReContent newReContent = new Model.Exchange_ReContent
            {
                ReContentId = reContent.ReContentId,
                ContentId = reContent.ContentId,
                Contents = reContent.Contents,
                CompileMan = reContent.CompileMan,
                CompileDate = reContent.CompileDate,
                AttachUrl = reContent.AttachUrl
            };
            db.Exchange_ReContent.InsertOnSubmit(newReContent);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改帖子回复
        /// </summary>
        /// <param name="reContent"></param>
        public static void UpdateReContent(Model.Exchange_ReContent reContent)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ReContent newReContent = db.Exchange_ReContent.FirstOrDefault(e => e.ReContentId == reContent.ReContentId);
            if (newReContent != null)
            {
                newReContent.Contents = reContent.Contents;
                newReContent.CompileDate = reContent.CompileDate;
                newReContent.AttachUrl = reContent.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///根据主键删除所有帖子回复
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteAllReContentsById(string contentId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = from x in db.Exchange_ReContent where x.ContentId == contentId select x;
            if (q != null)
            {
                foreach (var item in q)
                {
                    DeleteReContentById(item.ReContentId);
                }
            }
        }

        /// <summary>
        ///根据主键删除帖子回复
        /// </summary>
        /// <param name="lawRegulationId"></param>
        public static void DeleteReContentById(string reContentId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Exchange_ReContent reContent = db.Exchange_ReContent.FirstOrDefault(e => e.ReContentId == reContentId);
            if (reContent != null)
            {
                if (!string.IsNullOrEmpty(reContent.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, reContent.AttachUrl);
                }
                db.Exchange_ReContent.DeleteOnSubmit(reContent);
                db.SubmitChanges();
            }
        }
    }
}
