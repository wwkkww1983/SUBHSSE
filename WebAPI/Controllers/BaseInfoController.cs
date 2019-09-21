using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
        ///  获项目图片
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getProjectPictureByProjectId(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIBaseInfoService.getProjectPictureByProjectId(projectId);
                int pageCount = getDataList.Count();
                if (pageCount > 0)
                {
                    getDataList = getDataList.OrderByDescending(u => u.BaseInfoCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();

                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }

        /// <summary>
        ///   获项目图片
        /// </summary>
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

        #region 获取通知通告
        /// <summary>
        /// 获取头条通知
        /// </summary>
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
        /// 根据项目ID通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getNotices(string projectId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var noticeList = APIBaseInfoService.getNotices(projectId);
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
        /// 根据noticeId获取通知通告详细
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public Model.ResponeData getNoticesByNoticeId(string noticeId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getNoticesByNoticeId(noticeId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取法律法规类型
        /// <summary>
        ///   获取法律法规类型
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getLawsRegulationsType()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getLawsRegulationsType();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取标准规范类型
        /// <summary>
        ///   获取标准规范类型
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardListType()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getHSSEStandardListType(); 
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取规章制度类型
        /// <summary>
        ///   获取规章制度类型
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getRulesRegulationsType()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getRulesRegulationsType();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取管理规定类型
        /// <summary>
        ///   获取管理规定类型
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getManageRuleType()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getManageRuleType();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region  获取培训类别
        /// <summary>
        ///   获取培训类别
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getTrainType()
        {
            var responeData = new Model.ResponeData();
            try
            {              
                responeData.data = APIBaseInfoService.getTrainType();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region  获取培训级别
        /// <summary>
        ///   获取培训级别
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getTrainLevel()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIBaseInfoService.getTrainLevel();
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
