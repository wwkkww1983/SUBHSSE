using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class BaseInfoController : ApiController
    {
        #region 根据groupType获取检查类型
        /// <summary>
        ///  根据groupType获取检查类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getHazardRegisterTypes(string type)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIBaseInfoService.getHazardRegisterTypes(type);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId获取区域表
        /// <summary>
        ///  根据projectId获取区域表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectWorkArea(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIBaseInfoService.getProjectWorkArea(projectId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获项目图片
        /// <summary>
        ///   获项目图片
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectPicture()
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getLists = (from x in Funs.DB.InformationProject_Picture
                                join y in Funs.DB.AttachFile on x.PictureId equals y.ToKeyId
                                where x.States == Const.State_2 && y.AttachUrl != null
                                orderby x.UploadDate descending
                                select new Model.BaseInfoItem { BaseInfoId = x.PictureId, BaseInfoName = x.Title, ImageUrl = y.AttachUrl.Replace('\\', '/') }).Take(5).ToList();
                responeData.data = getLists;
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据通知通告
        /// <summary>
        /// 获取头条通知
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getTopNotices()
        {
            var responeData = new Model.ResponeData();
            try
            {
                string returnValue = string.Empty;
                var notice = (from x in Funs.DB.InformationProject_Notice
                              where x.IsRelease== true
                              orderby x.CompileDate descending
                              select x).FirstOrDefault();

                if (notice != null)
                {
                    returnValue = notice.NoticeTitle;
                }

                responeData.data = new { returnValue };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        ///  根据通知通告
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getNotices(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var noticeList = (from x in Funs.DB.InformationProject_Notice
                                  join y in Funs.DB.AttachFile on x.NoticeId equals y.ToKeyId
                                  where x.AccessProjectId.Contains(projectId) && x.IsRelease == true
                                  select new Model.NoticeItem
                                  {
                                      NoticeId = x.NoticeId,
                                      NoticeCode = x.NoticeCode,
                                      NoticeTitle = x.NoticeTitle,
                                      ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate)
                                  }).ToList();

                int pageCount = noticeList.Count();
                if (pageCount > 0)
                {
                    noticeList = noticeList.OrderByDescending(u => u.ReleaseDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, noticeList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        ///  根据通知通告
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getNoticesByNoticeId(string noticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var notice = (from x in Funs.DB.InformationProject_Notice
                              where x.NoticeId == noticeId
                              select new Model.NoticeItem
                              {
                                  NoticeId = x.NoticeId,
                                  NoticeCode = x.NoticeCode,
                                  NoticeTitle = x.NoticeTitle,
                                  ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate),
                                  MainContent = x.MainContent,
                                  AttachUrl = Funs.DB.AttachFile.FirstOrDefault(y => y.ToKeyId == x.NoticeId).AttachUrl.Replace("\\", "/")
                              });
                responeData.data = new { notice };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}
