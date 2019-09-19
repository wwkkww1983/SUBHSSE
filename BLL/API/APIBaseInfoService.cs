using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APIBaseInfoService
    {
        /// <summary>
        /// 根据类型获取巡检隐患类型表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHazardRegisterTypes(string type)
        {
            var getDataLists = (from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes                                        
                                          where x.HazardRegisterType == type
                                          orderby x.TypeCode
                                          select new Model.BaseInfoItem { BaseInfoId = x.RegisterTypesId, BaseInfoCode = x.TypeCode, BaseInfoName = x.RegisterTypesName }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据项目id获取区域表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectWorkArea(string projectId)
        {
            var getDataLists = (from x in Funs.DB.ProjectData_WorkArea                                          
                                          where x.ProjectId == projectId
                                          orderby x.WorkAreaCode
                                          select new Model.BaseInfoItem { BaseInfoId = x.WorkAreaId, BaseInfoCode = x.WorkAreaCode, BaseInfoName = x.WorkAreaName }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据项目id获取项目图片
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectPictureByProjectId(string projectId)
        {
            var getDataLists = (from x in Funs.DB.InformationProject_Picture
                                join y in Funs.DB.AttachFile on x.PictureId equals y.ToKeyId
                                where x.States == Const.State_2 && y.AttachUrl != null && x.ProjectId == projectId
                                orderby x.UploadDate descending
                                select new Model.BaseInfoItem { BaseInfoId = x.PictureId, BaseInfoName = x.Title, ImageUrl = y.AttachUrl.Replace('\\', '/') }).Take(5).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.NoticeItem> getNotices(string projectId)
        {
            var getDataLists = (from x in Funs.DB.InformationProject_Notice
                                join y in Funs.DB.AttachFile on x.NoticeId equals y.ToKeyId
                                where x.AccessProjectId.Contains(projectId) && x.IsRelease == true
                                select new Model.NoticeItem
                                {
                                    NoticeId = x.NoticeId,
                                    NoticeCode = x.NoticeCode,
                                    NoticeTitle = x.NoticeTitle,
                                    ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate)
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.NoticeItem getNoticesByNoticeId(string noticeId)
        {
            var getDataLists = (from x in Funs.DB.InformationProject_Notice
                                where x.NoticeId == noticeId
                                select new Model.NoticeItem
                                {
                                    NoticeId = x.NoticeId,
                                    NoticeCode = x.NoticeCode,
                                    NoticeTitle = x.NoticeTitle,
                                    ReleaseDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReleaseDate),
                                    MainContent = x.MainContent,
                                    AttachUrl = Funs.DB.AttachFile.FirstOrDefault(y => y.ToKeyId == x.NoticeId).AttachUrl.Replace("\\", "/")
                                }).FirstOrDefault(); 
            return getDataLists;
        }

        /// <summary>
        /// 获取培训类别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainType()
        {
            var getDataLists = (from x in Funs.DB.Base_TrainType
                                orderby x.TrainTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TrainTypeId, BaseInfoCode = x.TrainTypeCode, BaseInfoName = x.TrainTypeName }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 获取培训级别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainLevel()
        {
            var getDataLists = (from x in Funs.DB.Base_TrainLevel
                                orderby x.TrainLevelCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TrainLevelId, BaseInfoCode = x.TrainLevelCode, BaseInfoName = x.TrainLevelName }).ToList();
            return getDataLists;
        }
    }
}
