﻿using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIBaseInfoService
    {
        #region 获取常量
        /// <summary>
        /// 获取培训级别
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSysConst(string groupId)
        {
            var getDataLists = (from x in Funs.DB.Sys_Const
                                where x.GroupId == groupId
                                orderby x.SortIndex
                                select new Model.BaseInfoItem { BaseInfoId = x.ConstValue, BaseInfoCode = x.SortIndex.ToString(), BaseInfoName = x.ConstText }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据类型获取巡检隐患类型表
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
        #endregion

        #region 根据项目id获取区域表
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
        #endregion

        #region 根据项目id获取项目图片
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
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.PictureId,
                                    BaseInfoName = x.Title,
                                    BaseInfoCode = string.Format("{0:yyyy-MM-dd}", x.UploadDate),
                                    ImageUrl = y.AttachUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取通知通告
        /// <summary>
        /// 根据项目id获取通知通告
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.NoticeItem> getNoticesList(string projectId, string strParam)
        {
            var getDataLists = (from x in Funs.DB.InformationProject_Notice
                                where x.AccessProjectId.Contains(projectId) && x.IsRelease == true
                                && (strParam == null || x.NoticeTitle.Contains(strParam))
                                orderby x.ReleaseDate descending
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
                                    AttachUrl = Funs.DB.AttachFile.FirstOrDefault(y => y.ToKeyId == x.NoticeId).AttachUrl.Replace("\\", "/"),
                                }).FirstOrDefault();
            return getDataLists;
        }
        #endregion

        #region 获取岗位信息
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkPost()
        {
            var getDataLists = (from x in Funs.DB.Base_WorkPost
                                orderby x.WorkPostName
                                select new Model.BaseInfoItem { BaseInfoId = x.WorkPostId, BaseInfoCode = x.WorkPostCode, BaseInfoName = x.WorkPostName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取培训类别
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
        #endregion

        #region 获取培训级别
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
        #endregion

        #region 获取法律法规类型
        /// <summary>
        /// 获取法律法规类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getLawsRegulationsType()
        {
            var getDataLists = (from x in Funs.DB.Base_LawsRegulationsType
                                orderby x.Code
                                select new Model.BaseInfoItem { BaseInfoId = x.Id, BaseInfoCode = x.Code, BaseInfoName = x.Name }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取标准规范类型
        /// <summary>
        /// 获取标准规范类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHSSEStandardListType()
        {
            var getDataLists = (from x in Funs.DB.Base_HSSEStandardListType
                                orderby x.TypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TypeId, BaseInfoCode = x.TypeCode, BaseInfoName = x.TypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取规章制度类型
        /// <summary>
        /// 获取规章制度类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getRulesRegulationsType()
        {
            var getDataLists = (from x in Funs.DB.Base_RulesRegulationsType
                                orderby x.RulesRegulationsTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.RulesRegulationsTypeId, BaseInfoCode = x.RulesRegulationsTypeCode, BaseInfoName = x.RulesRegulationsTypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取管理规定类型
        /// <summary>
        /// 获取管理规定类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getManageRuleType()
        {
            var getDataLists = (from x in Funs.DB.Base_ManageRuleType
                                orderby x.ManageRuleTypeCode
                                select new Model.BaseInfoItem { BaseInfoId = x.ManageRuleTypeId, BaseInfoCode = x.ManageRuleTypeCode, BaseInfoName = x.ManageRuleTypeName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取机具设备类型
        /// <summary>
        /// 获取机具设备类型
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSpecialEquipment(bool isSpecial)
        {
            var getDataLists = (from x in Funs.DB.Base_SpecialEquipment
                                where x.IsSpecial == isSpecial
                                orderby x.SpecialEquipmentCode
                                select new Model.BaseInfoItem { BaseInfoId = x.SpecialEquipmentId, BaseInfoCode = x.SpecialEquipmentCode, BaseInfoName = x.SpecialEquipmentName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取工作阶段
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkStage()
        {
            var getDataLists = (from x in Funs.DB.Base_WorkStage
                                orderby x.WorkStageCode
                                select new Model.BaseInfoItem { BaseInfoId = x.WorkStageId, BaseInfoCode = x.WorkStageCode, BaseInfoName = x.WorkStageName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取项目班组
        /// <summary>
        /// 获取项目班组
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTeamGroup(string projectId, string unitId)
        {
            var getDataLists = (from x in Funs.DB.ProjectData_TeamGroup
                                where x.ProjectId == projectId && (unitId == null || x.UnitId == unitId)
                                orderby x.TeamGroupCode
                                select new Model.BaseInfoItem { BaseInfoId = x.TeamGroupId, BaseInfoCode = x.TeamGroupCode, BaseInfoName = x.TeamGroupName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取项目区域
        /// <summary>
        /// 获取项目区域
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWorkArea(string projectId, string unitId)
        {
            var getDataLists = (from x in Funs.DB.ProjectData_WorkArea
                                where x.ProjectId == projectId && (unitId == null || x.UnitId == unitId)
                                orderby x.WorkAreaCode
                                select new Model.BaseInfoItem { BaseInfoId = x.WorkAreaId, BaseInfoCode = x.WorkAreaCode, BaseInfoName = x.WorkAreaName }).ToList();
            return getDataLists;
        }
        #endregion

        #region 获取作业票-安全措施
        /// <summary>
        /// 获取作业票-安全措施
        /// </summary>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getSafetyMeasures(string licenseType)
        {
            var getDataLists = (from x in Funs.DB.Base_SafetyMeasures
                                where x.LicenseType == licenseType
                                orderby x.SortIndex
                                select new Model.BaseInfoItem { BaseInfoId = x.SafetyMeasuresId, BaseInfoCode = x.SortIndex.ToString(), BaseInfoName = x.SafetyMeasures }).ToList();
            return getDataLists;
        }
        #endregion
    }
}
