using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 作业票数据
    /// </summary>
    public static class APILicenseDataService
    {
        #region 根据主键ID获取作业票
        /// <summary>
        ///  根据主键ID获取作业票
        /// </summary>
        /// <param name="strMenuId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Model.LicenseDataItem getLicenseDataById(string strMenuId, string id)
        {
            Model.LicenseDataItem getInfo = new Model.LicenseDataItem();
            #region 动火作业票
            if (strMenuId == Const.ProjectFireWorkMenuId)
            {
                getInfo = (from x in Funs.DB.License_FireWork
                           where x.FireWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.FireWorkId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               FireWatchManId = x.FireWatchManId,
                               FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkMeasures = x.WorkMeasures,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.FireWorkId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 高处作业票
            else if (strMenuId == Const.ProjectHeightWorkMenuId)
            {
                getInfo = (from x in Funs.DB.License_HeightWork
                           where x.HeightWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.HeightWorkId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               WorkType = x.WorkType,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkMeasures = x.WorkMeasures,
                               EquipmentTools = x.EquipmentTools,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.HeightWorkId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 受限空间作业票
            if (strMenuId == Const.ProjectLimitedSpaceMenuId)
            {
                getInfo = (from x in Funs.DB.License_LimitedSpace
                           where x.LimitedSpaceId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.LimitedSpaceId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               FireWatchManId = x.FireWatchManId,
                               FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkMeasures = x.WorkMeasures,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.LimitedSpaceId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 射线作业票
            if (strMenuId == Const.ProjectRadialWorkMenuId)
            {
                getInfo = (from x in Funs.DB.License_RadialWork
                           where x.RadialWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.RadialWorkId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               RadialType = x.RadialType,
                               WorkLeaderId = x.WorkLeaderId,
                               WorkLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                               WorkLeaderTel=x.WorkLeaderTel,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkPalce = x.WorkPalce,
                               WorkMeasures = x.WorkMeasures,
                               FireWatchManId = x.FireWatchManId,
                               FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                               WatchManContact=x.WatchManContact,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.RadialWorkId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 断路(占道)作业票
            if (strMenuId == Const.ProjectOpenCircuitMenuId)
            {
                getInfo = (from x in Funs.DB.License_OpenCircuit
                           where x.OpenCircuitId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.OpenCircuitId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               WorkMeasures = x.WorkMeasures,                             
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               RoadName = x.RoadName,
                               SafeMeasures = x.SafeMeasures,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.OpenCircuitId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 动土作业票
            if (strMenuId == Const.ProjectBreakGroundMenuId)
            {
                getInfo = (from x in Funs.DB.License_BreakGround
                           where x.BreakGroundId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.BreakGroundId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               WorkDepth = x.WorkDepth,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkMeasures = x.WorkMeasures,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.BreakGroundId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 夜间施工作业票
            if (strMenuId == Const.ProjectNightWorkMenuId)
            {
                getInfo = (from x in Funs.DB.License_NightWork
                           where x.NightWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.NightWorkId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               WorkMeasures = x.WorkMeasures,
                               WorkLeaderId = x.WorkLeaderId,
                               WorkLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                               WorkLeaderTel = x.WorkLeaderTel,
                               SafeLeaderId = x.SafeLeaderId,
                               SafeLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.SafeLeaderId).UserName,
                               SafeLeaderTel = x.SafeLeaderTel,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),                              
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.NightWorkId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            #region 吊装作业票
            if (strMenuId == Const.ProjectLiftingWorkMenuId)
            {
                getInfo = (from x in Funs.DB.License_LiftingWork
                           where x.LiftingWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.LiftingWorkId,
                               MenuId = strMenuId,
                               ProjectId = x.ProjectId,
                               LicenseCode = x.LicenseCode,
                               ApplyUnitId = x.ApplyUnitId,
                               ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                               ApplyManId = x.ApplyManId,
                               ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                               ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                               WorkPalce = x.WorkPalce,
                               WorkLevel=x.WorkLevel,
                               ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                               ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                               WorkMeasures = x.WorkMeasures,
                               CraneCapacity=x.CraneCapacity,
                               CancelManId = x.CancelManId,
                               CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                               CancelReasons = x.CancelReasons,
                               CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                               CloseManId = x.CloseManId,
                               CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                               CloseReasons = x.CloseReasons,
                               CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                               NextManId = x.NextManId,
                               NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                               AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.LiftingWorkId).AttachUrl.Replace('\\', '/'),
                               States = x.States,
                           }).FirstOrDefault();
            }
            #endregion
            return getInfo;
        }
        #endregion        

        #region 获取作业票列表信息
        /// <summary>
        /// 获取作业票列表信息
        /// </summary>
        /// <param name="licenseType"></param>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.LicenseDataItem> getLicenseDataList(string strMenuId, string projectId, string unitId, string states)
        {
            if (!ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
            {
                unitId = null;
            }
            List<Model.LicenseDataItem> getInfoList = new List<Model.LicenseDataItem>();
            #region 动火作业票
            if (strMenuId == Const.ProjectFireWorkMenuId)
            {
                getInfoList = (from x in Funs.DB.License_FireWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.FireWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 高处作业票
            else if (strMenuId == Const.ProjectHeightWorkMenuId)
            {
                getInfoList = (from x in Funs.DB.License_HeightWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.HeightWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkType = x.WorkType,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   EquipmentTools = x.EquipmentTools,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 受限空间作业票
            if (strMenuId == Const.ProjectLimitedSpaceMenuId)
            {
                getInfoList = (from x in Funs.DB.License_LimitedSpace
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.LimitedSpaceId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 射线作业票
            if (strMenuId == Const.ProjectRadialWorkMenuId)
            {
                getInfoList = (from x in Funs.DB.License_RadialWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.RadialWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   RadialType = x.RadialType,
                                   WorkLeaderId = x.WorkLeaderId,
                                   WorkLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                   WorkLeaderTel = x.WorkLeaderTel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   FireWatchManId = x.FireWatchManId,
                                   FireWatchManName = Funs.DB.Sys_User.First(u => u.UserId == x.FireWatchManId).UserName,
                                   WatchManContact = x.WatchManContact,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 断路(占道)作业票
            if (strMenuId == Const.ProjectOpenCircuitMenuId)
            {
                getInfoList = (from x in Funs.DB.License_OpenCircuit
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.OpenCircuitId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   RoadName = x.RoadName,
                                   SafeMeasures = x.SafeMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 动土作业票
            if (strMenuId == Const.ProjectBreakGroundMenuId)
            {
                getInfoList = (from x in Funs.DB.License_BreakGround
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.BreakGroundId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkDepth = x.WorkDepth,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 夜间作业票
            if (strMenuId == Const.ProjectNightWorkMenuId)
            {
                getInfoList = (from x in Funs.DB.License_NightWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.NightWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkMeasures = x.WorkMeasures,
                                   WorkLeaderId = x.WorkLeaderId,
                                   WorkLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.WorkLeaderId).UserName,
                                   WorkLeaderTel = x.WorkLeaderTel,
                                   SafeLeaderId = x.SafeLeaderId,
                                   SafeLeaderName = Funs.DB.Sys_User.First(u => u.UserId == x.SafeLeaderId).UserName,
                                   SafeLeaderTel = x.SafeLeaderTel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   States = x.States,
                               }).ToList();
            }
            #endregion
            #region 吊装作业票
            if (strMenuId == Const.ProjectLiftingWorkMenuId)
            {
                getInfoList = (from x in Funs.DB.License_LiftingWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.LiftingWorkId,
                                   MenuId = strMenuId,
                                   ProjectId = x.ProjectId,
                                   LicenseCode = x.LicenseCode,
                                   ApplyUnitId = x.ApplyUnitId,
                                   ApplyUnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.ApplyUnitId).UnitName,
                                   ApplyManId = x.ApplyManId,
                                   ApplyManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApplyManId).UserName,
                                   ApplyDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ApplyDate),
                                   WorkPalce = x.WorkPalce,
                                   WorkLevel = x.WorkLevel,
                                   ValidityStartTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityStartTime),
                                   ValidityEndTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ValidityEndTime),
                                   WorkMeasures = x.WorkMeasures,
                                   CraneCapacity = x.CraneCapacity,
                                   CancelManId = x.CancelManId,
                                   CancelManName = Funs.DB.Sys_User.First(u => u.UserId == x.CancelManId).UserName,
                                   CancelReasons = x.CancelReasons,
                                   CancelTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CancelTime),
                                   CloseManId = x.CloseManId,
                                   CloseManName = Funs.DB.Sys_User.First(u => u.UserId == x.CloseManId).UserName,
                                   CloseReasons = x.CloseReasons,
                                   CloseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.CloseTime),
                                   NextManId = x.NextManId,
                                   NextManName = Funs.DB.Sys_User.First(u => u.UserId == x.NextManId).UserName,
                                   States = x.States,
                               }).ToList();
            }
            #endregion

            return getInfoList;
        }
        #endregion        

        #region 获取作业票审核列表信息
        /// <summary>
        /// 获取作业票审核列表信息
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.LicenseItem> getLicenseLicenseItemList(string dataId)
        {
            var getInfoList = (from x in Funs.DB.License_LicenseItem
                               where x.DataId == dataId
                               orderby x.SortIndex
                               select new Model.LicenseItem
                               {
                                   LicenseItemId = x.LicenseItemId,
                                   DataId = x.DataId,
                                   SortIndex = x.SortIndex ?? 0,
                                   SafetyMeasures = x.SafetyMeasures,
                                   IsUsed = x.IsUsed ?? false,
                                   ConfirmManId = x.ConfirmManId,
                                   ConfirmManName = Funs.DB.Sys_User.First(u => u.UserId == x.ConfirmManId).UserName,
                               }).ToList();
                
            return getInfoList;
        }
        #endregion        

        #region 获取作业票审核列表信息
        /// <summary>
        /// 获取作业票审核列表信息
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.FlowOperateItem> getLicenseFlowOperateList(string dataId)
        {
            var getInfoList = (from x in Funs.DB.License_FlowOperate
                               where x.DataId == dataId && (!x.IsFlowEnd.HasValue || x.IsFlowEnd == false)
                               orderby x.SortIndex
                               select new Model.FlowOperateItem
                               {
                                   FlowOperateId = x.FlowOperateId,
                                   AuditFlowName = x.AuditFlowName,
                                   SortIndex = x.SortIndex ?? 0,
                                   OperaterId = x.OperaterId,
                                   OperaterName = Funs.DB.Sys_User.First(u => u.UserId == x.OperaterId).UserName,
                                   OperaterTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.OperaterTime),
                                   IsAgree = x.IsAgree,
                                   Opinion = x.Opinion,
                                   IsFlowEnd = x.IsFlowEnd ?? false,
                               }).ToList();
                
            return getInfoList;
        }
        #endregion        

        #region 保存作业票信息
        /// <summary>
        /// 保存作业票信息
        /// </summary>
        /// <param name="newItem">作业票</param>
        /// <returns></returns>
        public static void SaveLicenseData(Model.LicenseDataItem newItem)
        {
            string strLicenseId = newItem.LicenseId;
            string projectId = newItem.ProjectId;
            #region 动火作业票
            if (newItem.MenuId == Const.ProjectFireWorkMenuId)
            {
                Model.License_FireWork newFireWork = new Model.License_FireWork
                {
                    FireWorkId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    FireWatchManId = newItem.FireWatchManId,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkMeasures = newItem.WorkMeasures,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newFireWork.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateFireWork = Funs.DB.License_FireWork.FirstOrDefault(x => x.FireWorkId == strLicenseId);
                if (updateFireWork == null)
                {
                    newFireWork.ApplyDate = DateTime.Now;
                    strLicenseId = newFireWork.FireWorkId = SQLHelper.GetNewID();                                      
                    newFireWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId);
                    Funs.DB.License_FireWork.InsertOnSubmit(newFireWork);                   
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId, newFireWork.FireWorkId, newFireWork.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateFireWork.CloseManId = newFireWork.CloseManId;
                        updateFireWork.CloseReasons = newFireWork.CloseReasons;
                        updateFireWork.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateFireWork.CancelManId = newFireWork.CancelManId;
                        updateFireWork.CancelReasons = newFireWork.CancelReasons;
                        updateFireWork.CancelTime = DateTime.Now;
                    }
                    else
                    {                        
                        updateFireWork.WorkPalce = newFireWork.WorkPalce;
                        updateFireWork.FireWatchManId = newFireWork.FireWatchManId;
                        updateFireWork.ValidityStartTime = newFireWork.ValidityStartTime;
                        updateFireWork.ValidityEndTime = newFireWork.ValidityEndTime;
                        updateFireWork.WorkMeasures = newFireWork.WorkMeasures;
                        updateFireWork.NextManId = newFireWork.NextManId;
                        updateFireWork.States = newFireWork.States;
                    }
                    updateFireWork.States = newFireWork.States;                    
                }
            }
            #endregion
            #region 高处作业票
            else if (newItem.MenuId == Const.ProjectHeightWorkMenuId)
            {
                Model.License_HeightWork newHeightWork = new Model.License_HeightWork
                {
                    HeightWorkId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    WorkType = newItem.WorkType,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkMeasures = newItem.WorkMeasures,
                    EquipmentTools = newItem.EquipmentTools,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newHeightWork.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateHeightWork = Funs.DB.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == strLicenseId);
                if (updateHeightWork == null)
                {
                    newHeightWork.ApplyDate = DateTime.Now;
                    strLicenseId = newHeightWork.HeightWorkId = SQLHelper.GetNewID();
                    newHeightWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectHeightWorkMenuId, newHeightWork.ProjectId, newHeightWork.ApplyUnitId);
                    Funs.DB.License_HeightWork.InsertOnSubmit(newHeightWork);
                  
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectHeightWorkMenuId, newHeightWork.ProjectId, newHeightWork.ApplyUnitId, newHeightWork.HeightWorkId, newHeightWork.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateHeightWork.CloseManId = newHeightWork.CloseManId;
                        updateHeightWork.CloseReasons = newHeightWork.CloseReasons;
                        updateHeightWork.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateHeightWork.CancelManId = newHeightWork.CancelManId;
                        updateHeightWork.CancelReasons = newHeightWork.CancelReasons;
                        updateHeightWork.CancelTime = DateTime.Now;
                    }
                    else
                    {                       
                        updateHeightWork.WorkPalce = newHeightWork.WorkPalce;
                        updateHeightWork.WorkType = newHeightWork.WorkType;
                        updateHeightWork.ValidityStartTime = newHeightWork.ValidityStartTime;
                        updateHeightWork.ValidityEndTime = newHeightWork.ValidityEndTime;
                        updateHeightWork.WorkMeasures = newHeightWork.WorkMeasures;
                        updateHeightWork.EquipmentTools = newHeightWork.EquipmentTools;
                        updateHeightWork.NextManId = newHeightWork.NextManId;
                        updateHeightWork.States = newHeightWork.States;
                    }
                    updateHeightWork.States = newHeightWork.States;                   
                }
            }
            #endregion
            #region 受限空间作业票           
            if (newItem.MenuId == Const.ProjectLimitedSpaceMenuId)
            {
                Model.License_LimitedSpace newLimitedSpace = new Model.License_LimitedSpace
                {
                    LimitedSpaceId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    FireWatchManId = newItem.FireWatchManId,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkMeasures = newItem.WorkMeasures,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newLimitedSpace.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateLimitedSpace = Funs.DB.License_LimitedSpace.FirstOrDefault(x => x.LimitedSpaceId == strLicenseId);
                if (updateLimitedSpace == null)
                {
                    newLimitedSpace.ApplyDate = DateTime.Now;
                    strLicenseId = newLimitedSpace.LimitedSpaceId = SQLHelper.GetNewID();
                    newLimitedSpace.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectLimitedSpaceMenuId, newLimitedSpace.ProjectId, newLimitedSpace.ApplyUnitId);
                    Funs.DB.License_LimitedSpace.InsertOnSubmit(newLimitedSpace);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLimitedSpaceMenuId, newLimitedSpace.ProjectId, newLimitedSpace.ApplyUnitId, newLimitedSpace.LimitedSpaceId, newLimitedSpace.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateLimitedSpace.CloseManId = newLimitedSpace.CloseManId;
                        updateLimitedSpace.CloseReasons = newLimitedSpace.CloseReasons;
                        updateLimitedSpace.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateLimitedSpace.CancelManId = newLimitedSpace.CancelManId;
                        updateLimitedSpace.CancelReasons = newLimitedSpace.CancelReasons;
                        updateLimitedSpace.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateLimitedSpace.WorkPalce = newLimitedSpace.WorkPalce;
                        updateLimitedSpace.FireWatchManId = newLimitedSpace.FireWatchManId;
                        updateLimitedSpace.ValidityStartTime = newLimitedSpace.ValidityStartTime;
                        updateLimitedSpace.ValidityEndTime = newLimitedSpace.ValidityEndTime;
                        updateLimitedSpace.WorkMeasures = newLimitedSpace.WorkMeasures;
                        updateLimitedSpace.NextManId = newLimitedSpace.NextManId;
                        updateLimitedSpace.States = newLimitedSpace.States;
                    }
                    updateLimitedSpace.States = newLimitedSpace.States;
                }
            }
            #endregion
            #region 射线作业票
            if (newItem.MenuId == Const.ProjectRadialWorkMenuId)
            {
                Model.License_RadialWork newRadialWork = new Model.License_RadialWork
                {
                    RadialWorkId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    RadialType=newItem.RadialType,
                    WorkLeaderId = newItem.WorkLeaderId,
                    WorkLeaderTel = newItem.WorkLeaderTel,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkPalce = newItem.WorkPalce,
                    WorkMeasures = newItem.WorkMeasures,
                    FireWatchManId = newItem.FireWatchManId,
                    WatchManContact=newItem.WatchManContact,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newRadialWork.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateRadialWork = Funs.DB.License_RadialWork.FirstOrDefault(x => x.RadialWorkId == strLicenseId);
                if (updateRadialWork == null)
                {
                    newRadialWork.ApplyDate = DateTime.Now;
                    strLicenseId = newRadialWork.RadialWorkId = SQLHelper.GetNewID();
                    newRadialWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectRadialWorkMenuId, newRadialWork.ProjectId, newRadialWork.ApplyUnitId);
                    Funs.DB.License_RadialWork.InsertOnSubmit(newRadialWork);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRadialWorkMenuId, newRadialWork.ProjectId, newRadialWork.ApplyUnitId, newRadialWork.RadialWorkId, newRadialWork.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateRadialWork.CloseManId = newRadialWork.CloseManId;
                        updateRadialWork.CloseReasons = newRadialWork.CloseReasons;
                        updateRadialWork.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateRadialWork.CancelManId = newRadialWork.CancelManId;
                        updateRadialWork.CancelReasons = newRadialWork.CancelReasons;
                        updateRadialWork.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateRadialWork.RadialType = newRadialWork.RadialType;
                        updateRadialWork.WorkLeaderId = newRadialWork.WorkLeaderId;
                        updateRadialWork.WorkLeaderTel = newRadialWork.WorkLeaderTel;
                        updateRadialWork.ValidityStartTime = newRadialWork.ValidityStartTime;
                        updateRadialWork.ValidityEndTime = newRadialWork.ValidityEndTime;
                        updateRadialWork.WorkPalce = newRadialWork.WorkPalce;
                        updateRadialWork.WorkMeasures = newRadialWork.WorkMeasures;
                        updateRadialWork.FireWatchManId = newRadialWork.FireWatchManId;
                        updateRadialWork.WatchManContact = newRadialWork.WatchManContact;                        
                        updateRadialWork.NextManId = newRadialWork.NextManId;
                        updateRadialWork.States = newRadialWork.States;
                    }
                    updateRadialWork.States = newRadialWork.States;
                }
            }
            #endregion
            #region 断路(占道)作业票
            if (newItem.MenuId == Const.ProjectOpenCircuitMenuId)
            {
                Model.License_OpenCircuit newOpenCircuit = new Model.License_OpenCircuit
                {
                    OpenCircuitId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    WorkMeasures = newItem.WorkMeasures,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    RoadName = newItem.RoadName,
                    SafeMeasures = newItem.SafeMeasures,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newOpenCircuit.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateOpenCircuit = Funs.DB.License_OpenCircuit.FirstOrDefault(x => x.OpenCircuitId == strLicenseId);
                if (updateOpenCircuit == null)
                {
                    newOpenCircuit.ApplyDate = DateTime.Now;
                    strLicenseId = newOpenCircuit.OpenCircuitId = SQLHelper.GetNewID();
                    newOpenCircuit.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectOpenCircuitMenuId, newOpenCircuit.ProjectId, newOpenCircuit.ApplyUnitId);
                    Funs.DB.License_OpenCircuit.InsertOnSubmit(newOpenCircuit);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectOpenCircuitMenuId, newOpenCircuit.ProjectId, newOpenCircuit.ApplyUnitId, newOpenCircuit.OpenCircuitId, newOpenCircuit.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateOpenCircuit.CloseManId = newOpenCircuit.CloseManId;
                        updateOpenCircuit.CloseReasons = newOpenCircuit.CloseReasons;
                        updateOpenCircuit.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateOpenCircuit.CancelManId = newOpenCircuit.CancelManId;
                        updateOpenCircuit.CancelReasons = newOpenCircuit.CancelReasons;
                        updateOpenCircuit.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateOpenCircuit.WorkPalce = newOpenCircuit.WorkPalce;
                        updateOpenCircuit.WorkMeasures = newOpenCircuit.WorkMeasures;
                        updateOpenCircuit.ValidityStartTime = newOpenCircuit.ValidityStartTime;
                        updateOpenCircuit.ValidityEndTime = newOpenCircuit.ValidityEndTime;                        
                        updateOpenCircuit.RoadName = newOpenCircuit.RoadName;
                        updateOpenCircuit.SafeMeasures = newOpenCircuit.SafeMeasures;
                        updateOpenCircuit.NextManId = newOpenCircuit.NextManId;
                        updateOpenCircuit.States = newOpenCircuit.States;
                    }
                    updateOpenCircuit.States = newOpenCircuit.States;
                }
            }
            #endregion
            #region 动土作业票
            if (newItem.MenuId == Const.ProjectBreakGroundMenuId)
            {
                Model.License_BreakGround newBreakGround = new Model.License_BreakGround
                {
                    BreakGroundId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    WorkDepth = newItem.WorkDepth,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkMeasures = newItem.WorkMeasures,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newBreakGround.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateBreakGround = Funs.DB.License_BreakGround.FirstOrDefault(x => x.BreakGroundId == strLicenseId);
                if (updateBreakGround == null)
                {
                    newBreakGround.ApplyDate = DateTime.Now;
                    strLicenseId = newBreakGround.BreakGroundId = SQLHelper.GetNewID();
                    newBreakGround.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectBreakGroundMenuId, newBreakGround.ProjectId, newBreakGround.ApplyUnitId);
                    Funs.DB.License_BreakGround.InsertOnSubmit(newBreakGround);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectBreakGroundMenuId, newBreakGround.ProjectId, newBreakGround.ApplyUnitId, newBreakGround.BreakGroundId, newBreakGround.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateBreakGround.CloseManId = newBreakGround.CloseManId;
                        updateBreakGround.CloseReasons = newBreakGround.CloseReasons;
                        updateBreakGround.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateBreakGround.CancelManId = newBreakGround.CancelManId;
                        updateBreakGround.CancelReasons = newBreakGround.CancelReasons;
                        updateBreakGround.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateBreakGround.WorkPalce = newBreakGround.WorkPalce;
                        updateBreakGround.WorkDepth = newBreakGround.WorkDepth;
                        updateBreakGround.ValidityStartTime = newBreakGround.ValidityStartTime;
                        updateBreakGround.ValidityEndTime = newBreakGround.ValidityEndTime;
                        updateBreakGround.WorkMeasures = newBreakGround.WorkMeasures;
                        updateBreakGround.NextManId = newBreakGround.NextManId;
                        updateBreakGround.States = newBreakGround.States;
                    }
                    updateBreakGround.States = newBreakGround.States;
                }
            }
            #endregion
            #region 夜间施工作业票
            if (newItem.MenuId == Const.ProjectNightWorkMenuId)
            {
                Model.License_NightWork newNightWork = new Model.License_NightWork
                {
                    NightWorkId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    WorkMeasures = newItem.WorkMeasures,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkLeaderId = newItem.WorkLeaderId,
                    WorkLeaderTel = newItem.WorkLeaderTel,
                    SafeLeaderId = newItem.SafeLeaderId,
                    SafeLeaderTel = newItem.SafeLeaderTel,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newNightWork.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateNightWork = Funs.DB.License_NightWork.FirstOrDefault(x => x.NightWorkId == strLicenseId);
                if (updateNightWork == null)
                {
                    newNightWork.ApplyDate = DateTime.Now;
                    strLicenseId = newNightWork.NightWorkId = SQLHelper.GetNewID();
                    newNightWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectNightWorkMenuId, newNightWork.ProjectId, newNightWork.ApplyUnitId);
                    Funs.DB.License_NightWork.InsertOnSubmit(newNightWork);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectNightWorkMenuId, newNightWork.ProjectId, newNightWork.ApplyUnitId, newNightWork.NightWorkId, newNightWork.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateNightWork.CloseManId = newNightWork.CloseManId;
                        updateNightWork.CloseReasons = newNightWork.CloseReasons;
                        updateNightWork.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateNightWork.CancelManId = newNightWork.CancelManId;
                        updateNightWork.CancelReasons = newNightWork.CancelReasons;
                        updateNightWork.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateNightWork.WorkPalce = newNightWork.WorkPalce;
                        updateNightWork.WorkMeasures = newNightWork.WorkMeasures;
                        updateNightWork.ValidityStartTime = newNightWork.ValidityStartTime;
                        updateNightWork.ValidityEndTime = newNightWork.ValidityEndTime;
                        updateNightWork.WorkLeaderId = newItem.WorkLeaderId;
                        updateNightWork.WorkLeaderTel = newItem.WorkLeaderTel;
                        updateNightWork.SafeLeaderId = newItem.SafeLeaderId;
                        updateNightWork.SafeLeaderTel = newItem.SafeLeaderTel;
                        updateNightWork.NextManId = newNightWork.NextManId;
                        updateNightWork.States = newNightWork.States;
                    }
                    updateNightWork.States = newNightWork.States;
                }
            }
            #endregion
            #region 吊装作业票
            if (newItem.MenuId == Const.ProjectLiftingWorkMenuId)
            {
                Model.License_LiftingWork newLiftingWork = new Model.License_LiftingWork
                {
                    LiftingWorkId = strLicenseId,
                    ProjectId = projectId,
                    LicenseCode = newItem.LicenseCode,
                    ApplyUnitId = newItem.ApplyUnitId,
                    ApplyManId = newItem.ApplyManId,
                    ApplyDate = Funs.GetNewDateTime(newItem.ApplyDate),
                    WorkPalce = newItem.WorkPalce,
                    WorkLevel = newItem.WorkLevel,
                    ValidityStartTime = Funs.GetNewDateTime(newItem.ValidityStartTime),
                    ValidityEndTime = Funs.GetNewDateTime(newItem.ValidityEndTime),
                    WorkMeasures = newItem.WorkMeasures,
                    CraneCapacity = newItem.CraneCapacity,
                    CancelManId = newItem.CancelManId,
                    CancelReasons = newItem.CancelReasons,
                    CancelTime = Funs.GetNewDateTime(newItem.CancelTime),
                    CloseManId = newItem.CloseManId,
                    CloseReasons = newItem.CloseReasons,
                    CloseTime = Funs.GetNewDateTime(newItem.CloseTime),
                    NextManId = newItem.NextManId,
                    States = newItem.States,
                };
                if (newItem.States == Const.State_0)
                {
                    newLiftingWork.NextManId = newItem.ApplyManId;
                }
                ////保存
                var updateLiftingWork = Funs.DB.License_LiftingWork.FirstOrDefault(x => x.LiftingWorkId == strLicenseId);
                if (updateLiftingWork == null)
                {
                    newLiftingWork.ApplyDate = DateTime.Now;
                    strLicenseId = newLiftingWork.LiftingWorkId = SQLHelper.GetNewID();
                    newLiftingWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectLiftingWorkMenuId, newLiftingWork.ProjectId, newLiftingWork.ApplyUnitId);
                    Funs.DB.License_LiftingWork.InsertOnSubmit(newLiftingWork);
                    ////增加一条编码记录
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLiftingWorkMenuId, newLiftingWork.ProjectId, newLiftingWork.ApplyUnitId, newLiftingWork.LiftingWorkId, newLiftingWork.ApplyDate);
                }
                else
                {
                    if (newItem.States == Const.State_3)
                    {
                        updateLiftingWork.CloseManId = newLiftingWork.CloseManId;
                        updateLiftingWork.CloseReasons = newLiftingWork.CloseReasons;
                        updateLiftingWork.CloseTime = DateTime.Now;
                    }
                    else if (newItem.States == Const.State_R)
                    {
                        updateLiftingWork.CancelManId = newLiftingWork.CancelManId;
                        updateLiftingWork.CancelReasons = newLiftingWork.CancelReasons;
                        updateLiftingWork.CancelTime = DateTime.Now;
                    }
                    else
                    {
                        updateLiftingWork.WorkPalce = newLiftingWork.WorkPalce;
                        updateLiftingWork.WorkLevel = newLiftingWork.WorkLevel;
                        updateLiftingWork.ValidityStartTime = newLiftingWork.ValidityStartTime;
                        updateLiftingWork.ValidityEndTime = newLiftingWork.ValidityEndTime;
                        updateLiftingWork.WorkMeasures = newLiftingWork.WorkMeasures;
                        updateLiftingWork.CraneCapacity = newLiftingWork.CraneCapacity;
                        updateLiftingWork.NextManId = newLiftingWork.NextManId;
                        updateLiftingWork.States = newLiftingWork.States;
                    }
                    updateLiftingWork.States = newLiftingWork.States;
                }
            }
            #endregion

            #region 保存安全措施明细
            if (newItem.States == Const.State_0 || newItem.States == Const.State_1)
            {
                ////删除安全措施                       
                var licenseItems = from x in Funs.DB.License_LicenseItem where x.DataId == strLicenseId select x;
                if (licenseItems.Count() > 0)
                {
                    Funs.DB.License_LicenseItem.DeleteAllOnSubmit(licenseItems);
                }
                ///// 新增安全措施
                var getLicenseItemList = newItem.LicenseItems;
                if (getLicenseItemList.Count() > 0)
                {
                    foreach (var item in getLicenseItemList)
                    {
                        Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
                        {
                            LicenseItemId = SQLHelper.GetNewID(),
                            DataId = strLicenseId,
                            SortIndex = item.SortIndex,
                            SafetyMeasures = item.SafetyMeasures,
                            IsUsed = item.IsUsed,
                            ConfirmManId = item.ConfirmManId,
                        };

                        Funs.DB.License_LicenseItem.InsertOnSubmit(newLicenseItem);                       
                    }
                }
            }
            #endregion

            #region 新增审核初始流程
            ////单据提交时 新增审核初始流程
            if (newItem.States == Const.State_1)
            {
                var gteIsNullOperates = Funs.DB.License_FlowOperate.FirstOrDefault(x=> x.DataId == strLicenseId);
                if (gteIsNullOperates != null)
                {
                    var noAgree = Funs.DB.License_FlowOperate.FirstOrDefault(x => x.DataId == strLicenseId && x.IsAgree == false && (x.IsClosed == false || x.IsClosed == null));
                    if (noAgree != null)
                    {
                        noAgree.IsAgree = null;
                        noAgree.Opinion = null;
                        noAgree.OperaterTime = null;
                    }
                    else
                    {
                        var firtFlow = (from x in Funs.DB.License_FlowOperate
                                        where x.DataId == strLicenseId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                        orderby x.SortIndex
                                        select x).FirstOrDefault();
                        if (firtFlow != null)
                        {
                            firtFlow.OperaterId = newItem.NextManId;
                        }
                    }
                }
                else
                {
                    var SysMenuFlowOperate = from x in Funs.DB.Sys_MenuFlowOperate
                                             where x.MenuId == newItem.MenuId
                                             select x;
                    if (SysMenuFlowOperate.Count() > 0)
                    {
                        foreach (var item in SysMenuFlowOperate)
                        {
                            Model.License_FlowOperate newFlowOperate = new Model.License_FlowOperate
                            {
                                FlowOperateId = SQLHelper.GetNewID(),
                                ProjectId =projectId,
                                DataId = strLicenseId,
                                MenuId = item.MenuId,
                                AuditFlowName = item.AuditFlowName,
                                SortIndex = item.FlowStep,
                                RoleIds = item.RoleId,
                                IsFlowEnd = item.IsFlowEnd,
                            };
                            if (newFlowOperate.SortIndex == 1)
                            {
                                newFlowOperate.OperaterId = newItem.NextManId;
                            }
                            Funs.DB.License_FlowOperate.InsertOnSubmit(newFlowOperate);
                        }
                    }
                }                
            }
            #endregion
            Funs.SubmitChanges();

            #region 保存附件
            ////保存附件
            if (!string.IsNullOrEmpty(newItem.AttachUrl))
            {
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.AttachUrl, 10, null), newItem.AttachUrl, newItem.MenuId, strLicenseId);
            }
            else
            {
                CommonService.DeleteAttachFileById(strLicenseId);
            }
            #endregion
        }
        #endregion

        #region 保存作业票审核信息
        /// <summary>
        /// 保存作业票审核信息
        /// </summary>
        /// <param name="newItem">保存作业票审核信息</param>
        /// <returns></returns>
        public static void SaveLicenseFlowOperate(Model.FlowOperateItem newItem)
        {
            string strMenuId = string.Empty;
            bool boolIsFlowEnd = false;
            string applyManId = string.Empty;
            var updateFlowOperate = Funs.DB.License_FlowOperate.FirstOrDefault(x => x.FlowOperateId == newItem.FlowOperateId);
            if (updateFlowOperate != null)
            {
                strMenuId = updateFlowOperate.MenuId;
                updateFlowOperate.OperaterId = newItem.OperaterId;
                updateFlowOperate.OperaterTime = DateTime.Now;
                updateFlowOperate.IsAgree = newItem.IsAgree;
                updateFlowOperate.Opinion = newItem.Opinion;
                if (newItem.IsAgree == true)
                {
                    updateFlowOperate.IsClosed = true;
                }
                
                /////增加一条审核明细记录
                Model.License_FlowOperateItem newFlowOperateItem = new Model.License_FlowOperateItem
                {
                    FlowOperateItemId = SQLHelper.GetNewID(),
                    FlowOperateId = updateFlowOperate.FlowOperateId,
                    OperaterId = updateFlowOperate.OperaterId,
                    OperaterTime = updateFlowOperate.OperaterTime,
                    IsAgree = updateFlowOperate.IsAgree,
                    Opinion = updateFlowOperate.Opinion,
                };
                Funs.DB.License_FlowOperateItem.InsertOnSubmit(newFlowOperateItem);              

                #region 新增下一步审核记录
                if (newItem.IsAgree == true)
                {
                    var getNextFlowOperate = Funs.DB.License_FlowOperate.FirstOrDefault(x => x.DataId == updateFlowOperate.DataId && x.SortIndex == (updateFlowOperate.SortIndex + 1));
                    ////判断审核步骤是否结束
                    if (getNextFlowOperate != null)
                    {
                        if (!getNextFlowOperate.IsFlowEnd.HasValue || getNextFlowOperate.IsFlowEnd == false)
                        {
                            getNextFlowOperate.OperaterId = newItem.NextOperaterId;
                        }
                        else
                        {
                            /////最后一步是关闭所有 步骤
                            var isCloseFlows = from x in Funs.DB.License_FlowOperate
                                               where x.DataId == updateFlowOperate.DataId && (!x.IsClosed.HasValue || x.IsClosed == false)
                                               select x;
                            if (isCloseFlows.Count() > 0)
                            {
                                foreach (var item in isCloseFlows)
                                {
                                    item.IsClosed = true;
                                    item.OperaterTime = DateTime.Now;
                                    item.IsAgree = true;
                                }
                            }
                            boolIsFlowEnd = true;
                        }
                    }
                }
                #endregion

                #region 动火作业票
                if (strMenuId == Const.ProjectFireWorkMenuId)
                {
                    var getFireWork = Funs.DB.License_FireWork.FirstOrDefault(x => x.FireWorkId == updateFlowOperate.DataId);
                    if (getFireWork != null)
                    {
                        applyManId = getFireWork.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getFireWork.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getFireWork.NextManId = null;
                                getFireWork.States = Const.State_2;
                                if (getFireWork.ValidityStartTime.HasValue && getFireWork.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getFireWork.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getFireWork.ValidityEndTime - getFireWork.ValidityStartTime).Value.TotalDays);
                                    }
                                    getFireWork.ValidityStartTime = DateTime.Now;
                                    getFireWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else                        
                        {
                            getFireWork.NextManId = applyManId;
                            getFireWork.States = Const.State_0;                            
                        }
                    }
                }
                #endregion
                #region 高处作业票
                else if (strMenuId == Const.ProjectHeightWorkMenuId)
                {
                    var getHeightWork = Funs.DB.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == updateFlowOperate.DataId);
                    if (getHeightWork != null)
                    {
                        applyManId = getHeightWork.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getHeightWork.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getHeightWork.NextManId = null;
                                getHeightWork.States = Const.State_2;
                                if (getHeightWork.ValidityStartTime.HasValue && getHeightWork.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getHeightWork.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getHeightWork.ValidityEndTime - getHeightWork.ValidityStartTime).Value.TotalDays);
                                    }
                                    getHeightWork.ValidityStartTime = DateTime.Now;
                                    getHeightWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getHeightWork.NextManId = applyManId;
                            getHeightWork.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 受限空间作业票
                if (strMenuId == Const.ProjectLimitedSpaceMenuId)
                {
                    var getLimitedSpace = Funs.DB.License_LimitedSpace.FirstOrDefault(x => x.LimitedSpaceId == updateFlowOperate.DataId);
                    if (getLimitedSpace != null)
                    {
                        applyManId = getLimitedSpace.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getLimitedSpace.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getLimitedSpace.NextManId = null;
                                getLimitedSpace.States = Const.State_2;
                                if (getLimitedSpace.ValidityStartTime.HasValue && getLimitedSpace.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 1;
                                    if (getLimitedSpace.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getLimitedSpace.ValidityEndTime - getLimitedSpace.ValidityStartTime).Value.TotalDays);
                                    }
                                    getLimitedSpace.ValidityStartTime = DateTime.Now;
                                    getLimitedSpace.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getLimitedSpace.NextManId = applyManId;
                            getLimitedSpace.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 射线作业票
                if (strMenuId == Const.ProjectRadialWorkMenuId)
                {
                    var getRadialWork = Funs.DB.License_RadialWork.FirstOrDefault(x => x.RadialWorkId == updateFlowOperate.DataId);
                    if (getRadialWork != null)
                    {
                        applyManId = getRadialWork.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getRadialWork.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getRadialWork.NextManId = null;
                                getRadialWork.States = Const.State_2;
                                if (getRadialWork.ValidityStartTime.HasValue && getRadialWork.ValidityStartTime < DateTime.Now)
                                {
                                    int hours = 24;
                                    if (getRadialWork.ValidityEndTime.HasValue)
                                    {
                                        hours = Convert.ToInt32((getRadialWork.ValidityEndTime - getRadialWork.ValidityStartTime).Value.TotalHours);
                                    }
                                    getRadialWork.ValidityStartTime = DateTime.Now;
                                    getRadialWork.ValidityEndTime = DateTime.Now.AddHours(hours);
                                }
                            }
                        }
                        else
                        {
                            getRadialWork.NextManId = applyManId;
                            getRadialWork.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 断路(占道)作业票
                if (strMenuId == Const.ProjectOpenCircuitMenuId)
                {
                    var getOpenCircuit = Funs.DB.License_OpenCircuit.FirstOrDefault(x => x.OpenCircuitId == updateFlowOperate.DataId);
                    if (getOpenCircuit != null)
                    {
                        applyManId = getOpenCircuit.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getOpenCircuit.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getOpenCircuit.NextManId = null;
                                getOpenCircuit.States = Const.State_2;
                                if (getOpenCircuit.ValidityStartTime.HasValue && getOpenCircuit.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getOpenCircuit.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getOpenCircuit.ValidityEndTime - getOpenCircuit.ValidityStartTime).Value.TotalDays);
                                    }
                                    getOpenCircuit.ValidityStartTime = DateTime.Now;
                                    getOpenCircuit.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getOpenCircuit.NextManId = applyManId;
                            getOpenCircuit.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 动土作业票
                if (strMenuId == Const.ProjectBreakGroundMenuId)
                {
                    var getBreakGround = Funs.DB.License_BreakGround.FirstOrDefault(x => x.BreakGroundId == updateFlowOperate.DataId);
                    if (getBreakGround != null)
                    {
                        applyManId = getBreakGround.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getBreakGround.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getBreakGround.NextManId = null;
                                getBreakGround.States = Const.State_2;
                                if (getBreakGround.ValidityStartTime.HasValue && getBreakGround.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getBreakGround.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getBreakGround.ValidityEndTime - getBreakGround.ValidityStartTime).Value.TotalDays);
                                    }
                                    getBreakGround.ValidityStartTime = DateTime.Now;
                                    getBreakGround.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getBreakGround.NextManId = applyManId;
                            getBreakGround.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 夜间施工作业票
                if (strMenuId == Const.ProjectNightWorkMenuId)
                {
                    var getNightWork = Funs.DB.License_NightWork.FirstOrDefault(x => x.NightWorkId == updateFlowOperate.DataId);
                    if (getNightWork != null)
                    {
                        applyManId = getNightWork.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getNightWork.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getNightWork.NextManId = null;
                                getNightWork.States = Const.State_2;
                                if (getNightWork.ValidityStartTime.HasValue && getNightWork.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getNightWork.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getNightWork.ValidityEndTime - getNightWork.ValidityStartTime).Value.TotalDays);
                                    }
                                    getNightWork.ValidityStartTime = DateTime.Now;
                                    getNightWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getNightWork.NextManId = applyManId;
                            getNightWork.States = Const.State_0;
                        }
                    }
                }
                #endregion
                #region 吊装作业票
                if (strMenuId == Const.ProjectLiftingWorkMenuId)
                {
                    var getLiftingWork = Funs.DB.License_LiftingWork.FirstOrDefault(x => x.LiftingWorkId == updateFlowOperate.DataId);
                    if (getLiftingWork != null)
                    {
                        applyManId = getLiftingWork.ApplyManId;
                        if (newItem.IsAgree == true)
                        {
                            getLiftingWork.NextManId = newItem.NextOperaterId;
                            if (boolIsFlowEnd == true)
                            {
                                getLiftingWork.NextManId = null;
                                getLiftingWork.States = Const.State_2;
                                if (getLiftingWork.ValidityStartTime.HasValue && getLiftingWork.ValidityStartTime < DateTime.Now)
                                {
                                    int days = 7;
                                    if (getLiftingWork.ValidityEndTime.HasValue)
                                    {
                                        days = Convert.ToInt32((getLiftingWork.ValidityEndTime - getLiftingWork.ValidityStartTime).Value.TotalDays);
                                    }
                                    getLiftingWork.ValidityStartTime = DateTime.Now;
                                    getLiftingWork.ValidityEndTime = DateTime.Now.AddDays(days);
                                }
                            }
                        }
                        else
                        {
                            getLiftingWork.NextManId = applyManId;
                            getLiftingWork.States = Const.State_0;
                        }
                    }
                }
                #endregion
            }
            Funs.SubmitChanges();
        }
        #endregion

        #region 获取当前审核记录
        /// <summary>
        /// 获取当前审核记录
        /// </summary>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public static Model.FlowOperateItem getLicenseFlowOperate(string dataId)
        {
            ////审核记录
            var getFlowOperate = from x in Funs.DB.License_FlowOperate
                                 where x.DataId == dataId && x.OperaterId != null
                                 && (!x.IsClosed.HasValue || x.IsClosed == false)
                                 orderby x.SortIndex
                                 select new Model.FlowOperateItem
                                 {
                                     FlowOperateId = x.FlowOperateId,
                                     MenuId = x.MenuId,
                                     DataId = x.DataId,
                                     AuditFlowName = x.AuditFlowName,
                                     SortIndex = x.SortIndex ?? 0,
                                     RoleIds = x.RoleIds,
                                     OperaterId = x.OperaterId,
                                     OperaterName = Funs.DB.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                     IsAgree = x.IsAgree,
                                     Opinion = x.Opinion,
                                     IsFlowEnd = x.IsFlowEnd ?? false,
                                     SignatureUrl= Funs.DB.Sys_User.First(y => y.UserId == x.OperaterId).SignatureUrl.Replace('\\', '/'),
                                 };
            return getFlowOperate.FirstOrDefault();
        }
        #endregion

        #region 获取下一步审核
        /// <summary>
        /// 获取下一步审核
        /// </summary>
        /// <param name="dataId">主键ID</param>
        /// <returns></returns>
        public static Model.FlowOperateItem getNextLicenseFlowOperate(string strMenuId, Model.LicenseDataItem licenseInfo)
        {
            Model.FlowOperateItem getFlowOperate = new Model.FlowOperateItem();
            if (licenseInfo == null)
            {
                getFlowOperate = (from x in Funs.DB.Sys_MenuFlowOperate
                                  where x.MenuId == strMenuId
                                  orderby x.FlowStep
                                  select new Model.FlowOperateItem
                                  {
                                      FlowOperateId = x.FlowOperateId,
                                      MenuId = x.MenuId,
                                      AuditFlowName = x.AuditFlowName,
                                      SortIndex = x.FlowStep ?? 0,
                                      RoleIds = x.RoleId,                                   
                                      IsFlowEnd = x.IsFlowEnd ?? false,
                                  }).FirstOrDefault();
            }
            else
            {
                if (licenseInfo.States == Const.State_0)
                {
                    getFlowOperate = (from x in Funs.DB.License_FlowOperate
                                      where x.DataId == licenseInfo.LicenseId && x.IsAgree == false && (!x.IsClosed.HasValue || x.IsClosed == false)
                                      orderby x.SortIndex
                                      select new Model.FlowOperateItem
                                      {
                                          FlowOperateId = x.FlowOperateId,
                                          MenuId = x.MenuId,
                                          DataId = x.DataId,
                                          AuditFlowName = x.AuditFlowName,
                                          SortIndex = x.SortIndex ?? 0,
                                          RoleIds = x.RoleIds,
                                          OperaterId = x.OperaterId,
                                          OperaterName = Funs.DB.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                          IsAgree = x.IsAgree,
                                          Opinion = x.Opinion,
                                          IsFlowEnd = x.IsFlowEnd ?? false,
                                      }).FirstOrDefault(); ;
                }
                else if (licenseInfo.States == Const.State_1)
                {
                    getFlowOperate = (from x in Funs.DB.License_FlowOperate
                                      where x.DataId == licenseInfo.LicenseId && x.OperaterId == null && (!x.IsClosed.HasValue || x.IsClosed == false)
                                      orderby x.SortIndex
                                      select new Model.FlowOperateItem
                                      {
                                          FlowOperateId = x.FlowOperateId,
                                          MenuId = x.MenuId,
                                          DataId = x.DataId,
                                          AuditFlowName = x.AuditFlowName,
                                          SortIndex = x.SortIndex ?? 0,
                                          RoleIds = x.RoleIds,
                                          OperaterId = x.OperaterId,
                                          OperaterName = Funs.DB.Sys_User.First(y => y.UserId == x.OperaterId).UserName,
                                          IsAgree = x.IsAgree,
                                          Opinion = x.Opinion,
                                          IsFlowEnd = x.IsFlowEnd ?? false,
                                      }).FirstOrDefault();
                }
            }
            return getFlowOperate;
        }
        #endregion
    }
}
