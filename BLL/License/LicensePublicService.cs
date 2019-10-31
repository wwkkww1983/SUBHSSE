using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 作业票 -公共类集合
    /// </summary>
    public static class LicensePublicService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
        /// <summary>
        /// 状态下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] drpStatesItem()
        {
            ListItem[] list = new ListItem[6];
            list[0] = new ListItem("全部", Const._Null);
            list[1] = new ListItem("待提交", Const.State_0);
            list[2] = new ListItem("审核中", Const.State_1);
            list[3] = new ListItem("作业中", Const.State_2);
            list[4] = new ListItem("已关闭", Const.State_3);
            list[5] = new ListItem("已取消", Const.State_R);
            return list;
        }

        #region 作业票
        #region 动火作业票
        /// <summary>
        /// 根据主键获取动火作业票
        /// </summary>
        /// <param name="fireWorkId"></param>
        /// <returns></returns>
        public static Model.License_FireWork GetFireWorkById(string fireWorkId)
        {
            return Funs.DB.License_FireWork.FirstOrDefault(e => e.FireWorkId == fireWorkId);
        }
        
        /// <summary>
        /// 添加动火作业票
        /// </summary>
        /// <param name="fireWork"></param>
        public static void AddFireWork(Model.License_FireWork fireWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FireWork newFireWork = new Model.License_FireWork
            {
                FireWorkId = fireWork.FireWorkId,
                ProjectId = fireWork.ProjectId,
                LicenseCode = fireWork.LicenseCode,
                ApplyUnitId = fireWork.ApplyUnitId,
                ApplyManId = fireWork.ApplyManId,
                ApplyDate = fireWork.ApplyDate,
                WorkPalce = fireWork.WorkPalce,
                FireWatchManId = fireWork.FireWatchManId,
                ValidityStartTime = fireWork.ValidityStartTime,
                ValidityEndTime = fireWork.ValidityEndTime,
                WorkMeasures = fireWork.WorkMeasures,
                CancelManId = fireWork.CancelManId,
                CancelReasons = fireWork.CancelReasons,
                CancelTime = fireWork.CancelTime,
                CloseManId = fireWork.CloseManId,
                CloseReasons = fireWork.CloseReasons,
                CloseTime = fireWork.CloseTime,
                NextManId=fireWork.NextManId,
                States = fireWork.States,
            };
            db.License_FireWork.InsertOnSubmit(newFireWork);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectFireWorkMenuId, fireWork.ProjectId, fireWork.ApplyUnitId, fireWork.FireWorkId, fireWork.ApplyDate);
        }

        /// <summary>
        /// 修改动火作业票
        /// </summary>
        /// <param name="fireWork"></param>
        public static void UpdateFireWork(Model.License_FireWork fireWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FireWork newFireWork = db.License_FireWork.FirstOrDefault(e => e.FireWorkId == fireWork.FireWorkId);
            if (newFireWork != null)
            {
                newFireWork.WorkPalce = fireWork.WorkPalce;
                newFireWork.FireWatchManId = fireWork.FireWatchManId;
                newFireWork.ValidityStartTime = fireWork.ValidityStartTime;
                newFireWork.ValidityEndTime = fireWork.ValidityEndTime;
                newFireWork.WorkMeasures = fireWork.WorkMeasures;
                newFireWork.CancelManId = fireWork.CancelManId;
                newFireWork.CancelReasons = fireWork.CancelReasons;
                newFireWork.CancelTime = fireWork.CancelTime;
                newFireWork.CloseManId = fireWork.CloseManId;
                newFireWork.CloseReasons = fireWork.CloseReasons;
                newFireWork.CloseTime = fireWork.CloseTime;
                newFireWork.NextManId = fireWork.NextManId;
                newFireWork.States = fireWork.States;                
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除动火作业票
        /// </summary>
        /// <param name="fireWorkId"></param>
        public static void DeleteFireWorkById(string fireWorkId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FireWork fireWork = db.License_FireWork.FirstOrDefault(e => e.FireWorkId == fireWorkId);
            if (fireWork!=null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(fireWorkId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(fireWorkId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(fireWorkId);
                db.License_FireWork.DeleteOnSubmit(fireWork);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 高处作业票
        /// <summary>
        /// 根据主键获取高处作业票
        /// </summary>
        /// <param name="heightWorkId"></param>
        /// <returns></returns>
        public static Model.License_HeightWork GetHeightWorkById(string heightWorkId)
        {
            return Funs.DB.License_HeightWork.FirstOrDefault(e => e.HeightWorkId == heightWorkId);
        }

        /// <summary>
        /// 添加高处作业票
        /// </summary>
        /// <param name="heightWork"></param>
        public static void AddHeightWork(Model.License_HeightWork heightWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_HeightWork newHeightWork = new Model.License_HeightWork
            {
                HeightWorkId = heightWork.HeightWorkId,
                ProjectId = heightWork.ProjectId,
                LicenseCode = heightWork.LicenseCode,
                ApplyUnitId = heightWork.ApplyUnitId,
                ApplyManId = heightWork.ApplyManId,
                ApplyDate = heightWork.ApplyDate,
                WorkPalce = heightWork.WorkPalce,
                WorkType = heightWork.WorkType,
                ValidityStartTime = heightWork.ValidityStartTime,
                ValidityEndTime = heightWork.ValidityEndTime,
                WorkMeasures = heightWork.WorkMeasures,
                EquipmentTools= heightWork.EquipmentTools,
                CancelManId = heightWork.CancelManId,
                CancelReasons = heightWork.CancelReasons,
                CancelTime = heightWork.CancelTime,
                CloseManId = heightWork.CloseManId,
                CloseReasons = heightWork.CloseReasons,
                CloseTime = heightWork.CloseTime,
                NextManId = heightWork.NextManId,
                States = heightWork.States,
            };
            db.License_HeightWork.InsertOnSubmit(newHeightWork);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectHeightWorkMenuId, heightWork.ProjectId, heightWork.ApplyUnitId, heightWork.HeightWorkId, heightWork.ApplyDate);
        }

        /// <summary>
        /// 修改高处作业票
        /// </summary>
        /// <param name="heightWork"></param>
        public static void UpdateHeightWork(Model.License_HeightWork heightWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_HeightWork newHeightWork = db.License_HeightWork.FirstOrDefault(e => e.HeightWorkId == heightWork.HeightWorkId);
            if (newHeightWork != null)
            {
                newHeightWork.WorkPalce = heightWork.WorkPalce;
                newHeightWork.WorkType = heightWork.WorkType;
                newHeightWork.ValidityStartTime = heightWork.ValidityStartTime;
                newHeightWork.ValidityEndTime = heightWork.ValidityEndTime;
                newHeightWork.WorkMeasures = heightWork.WorkMeasures;
                newHeightWork.EquipmentTools = heightWork.EquipmentTools;
                newHeightWork.CancelManId = heightWork.CancelManId;
                newHeightWork.CancelReasons = heightWork.CancelReasons;
                newHeightWork.CancelTime = heightWork.CancelTime;
                newHeightWork.CloseManId = heightWork.CloseManId;
                newHeightWork.CloseReasons = heightWork.CloseReasons;
                newHeightWork.CloseTime = heightWork.CloseTime;
                newHeightWork.NextManId = heightWork.NextManId;
                newHeightWork.States = heightWork.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除高处作业票
        /// </summary>
        /// <param name="heightWorkId"></param>
        public static void DeleteHeightWorkById(string heightWorkId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_HeightWork heightWork = db.License_HeightWork.FirstOrDefault(e => e.HeightWorkId == heightWorkId);
            if (heightWork != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(heightWorkId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(heightWorkId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(heightWorkId);
                db.License_HeightWork.DeleteOnSubmit(heightWork);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 受限空间作业票
        /// <summary>
        /// 根据主键获取受限空间作业票
        /// </summary>
        /// <param name="limitedSpaceId"></param>
        /// <returns></returns>
        public static Model.License_LimitedSpace GetLimitedSpaceById(string limitedSpaceId)
        {
            return Funs.DB.License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == limitedSpaceId);
        }

        /// <summary>
        /// 添加受限空间作业票
        /// </summary>
        /// <param name="limitedSpace"></param>
        public static void AddLimitedSpace(Model.License_LimitedSpace limitedSpace)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LimitedSpace newLimitedSpace = new Model.License_LimitedSpace
            {
                LimitedSpaceId = limitedSpace.LimitedSpaceId,
                ProjectId = limitedSpace.ProjectId,
                LicenseCode = limitedSpace.LicenseCode,
                ApplyUnitId = limitedSpace.ApplyUnitId,
                ApplyManId = limitedSpace.ApplyManId,
                ApplyDate = limitedSpace.ApplyDate,
                WorkPalce = limitedSpace.WorkPalce,
                FireWatchManId = limitedSpace.FireWatchManId,
                ValidityStartTime = limitedSpace.ValidityStartTime,
                ValidityEndTime = limitedSpace.ValidityEndTime,
                WorkMeasures = limitedSpace.WorkMeasures,
                CancelManId = limitedSpace.CancelManId,
                CancelReasons = limitedSpace.CancelReasons,
                CancelTime = limitedSpace.CancelTime,
                CloseManId = limitedSpace.CloseManId,
                CloseReasons = limitedSpace.CloseReasons,
                CloseTime = limitedSpace.CloseTime,
                NextManId = limitedSpace.NextManId,
                States = limitedSpace.States,
            };
            db.License_LimitedSpace.InsertOnSubmit(newLimitedSpace);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectLimitedSpaceMenuId, limitedSpace.ProjectId, limitedSpace.ApplyUnitId, limitedSpace.LimitedSpaceId, limitedSpace.ApplyDate);
        }

        /// <summary>
        /// 修改受限空间作业票
        /// </summary>
        /// <param name="limitedSpace"></param>
        public static void UpdateLimitedSpace(Model.License_LimitedSpace limitedSpace)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LimitedSpace newLimitedSpace = db.License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == limitedSpace.LimitedSpaceId);
            if (newLimitedSpace != null)
            {
                newLimitedSpace.WorkPalce = limitedSpace.WorkPalce;
                newLimitedSpace.FireWatchManId = limitedSpace.FireWatchManId;
                newLimitedSpace.ValidityStartTime = limitedSpace.ValidityStartTime;
                newLimitedSpace.ValidityEndTime = limitedSpace.ValidityEndTime;
                newLimitedSpace.WorkMeasures = limitedSpace.WorkMeasures;
                newLimitedSpace.CancelManId = limitedSpace.CancelManId;
                newLimitedSpace.CancelReasons = limitedSpace.CancelReasons;
                newLimitedSpace.CancelTime = limitedSpace.CancelTime;
                newLimitedSpace.CloseManId = limitedSpace.CloseManId;
                newLimitedSpace.CloseReasons = limitedSpace.CloseReasons;
                newLimitedSpace.CloseTime = limitedSpace.CloseTime;
                newLimitedSpace.NextManId = limitedSpace.NextManId;
                newLimitedSpace.States = limitedSpace.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除受限空间作业票
        /// </summary>
        /// <param name="limitedSpaceId"></param>
        public static void DeleteLimitedSpaceById(string limitedSpaceId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LimitedSpace limitedSpace = db.License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == limitedSpaceId);
            if (limitedSpace != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(limitedSpaceId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(limitedSpaceId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(limitedSpaceId);
                db.License_LimitedSpace.DeleteOnSubmit(limitedSpace);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 射线作业票
        /// <summary>
        /// 根据主键获取射线作业票
        /// </summary>
        /// <param name="radialWorkId"></param>
        /// <returns></returns>
        public static Model.License_RadialWork GetRadialWorkById(string radialWorkId)
        {
            return Funs.DB.License_RadialWork.FirstOrDefault(e => e.RadialWorkId == radialWorkId);
        }

        /// <summary>
        /// 添加射线作业票
        /// </summary>
        /// <param name="radialWork"></param>
        public static void AddRadialWork(Model.License_RadialWork radialWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_RadialWork newRadialWork = new Model.License_RadialWork
            {
                RadialWorkId = radialWork.RadialWorkId,
                ProjectId = radialWork.ProjectId,
                LicenseCode = radialWork.LicenseCode,
                ApplyUnitId = radialWork.ApplyUnitId,
                ApplyManId = radialWork.ApplyManId,
                ApplyDate = radialWork.ApplyDate,
                RadialType = radialWork.RadialType,
                WorkLeaderId = radialWork.WorkLeaderId,
                WorkLeaderTel = radialWork.WorkLeaderTel,
                ValidityStartTime = radialWork.ValidityStartTime,
                ValidityEndTime = radialWork.ValidityEndTime,
                WorkPalce = radialWork.WorkPalce,
                WorkMeasures = radialWork.WorkMeasures,
                FireWatchManId = radialWork.FireWatchManId,
                WatchManContact = radialWork.WatchManContact,
                CancelManId = radialWork.CancelManId,
                CancelReasons = radialWork.CancelReasons,
                CancelTime = radialWork.CancelTime,
                CloseManId = radialWork.CloseManId,
                CloseReasons = radialWork.CloseReasons,
                CloseTime = radialWork.CloseTime,
                NextManId = radialWork.NextManId,
                States = radialWork.States,
            };
            db.License_RadialWork.InsertOnSubmit(newRadialWork);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRadialWorkMenuId, radialWork.ProjectId, radialWork.ApplyUnitId, radialWork.RadialWorkId, radialWork.ApplyDate);
        }

        /// <summary>
        /// 修改射线作业票
        /// </summary>
        /// <param name="radialWork"></param>
        public static void UpdateRadialWork(Model.License_RadialWork radialWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_RadialWork newRadialWork = db.License_RadialWork.FirstOrDefault(e => e.RadialWorkId == radialWork.RadialWorkId);
            if (newRadialWork != null)
            {
                newRadialWork.RadialWorkId = radialWork.RadialWorkId;
                newRadialWork.ProjectId = radialWork.ProjectId;
                newRadialWork.LicenseCode = radialWork.LicenseCode;
                newRadialWork.ApplyUnitId = radialWork.ApplyUnitId;
                newRadialWork.ApplyManId = radialWork.ApplyManId;
                newRadialWork.ApplyDate = radialWork.ApplyDate;
                newRadialWork.RadialType = radialWork.RadialType;
                newRadialWork.WorkLeaderId = radialWork.WorkLeaderId;
                newRadialWork.WorkLeaderTel = radialWork.WorkLeaderTel;
                newRadialWork.ValidityStartTime = radialWork.ValidityStartTime;
                newRadialWork.ValidityEndTime = radialWork.ValidityEndTime;
                newRadialWork.WorkPalce = radialWork.WorkPalce;
                newRadialWork.WorkMeasures = radialWork.WorkMeasures;
                newRadialWork.FireWatchManId = radialWork.FireWatchManId;
                newRadialWork.WatchManContact = radialWork.WatchManContact;
                newRadialWork.CancelManId = radialWork.CancelManId;
                newRadialWork.CancelReasons = radialWork.CancelReasons;
                newRadialWork.CancelTime = radialWork.CancelTime;
                newRadialWork.CloseManId = radialWork.CloseManId;
                newRadialWork.CloseReasons = radialWork.CloseReasons;
                newRadialWork.CloseTime = radialWork.CloseTime;
                newRadialWork.NextManId = radialWork.NextManId;
                newRadialWork.States = radialWork.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除射线作业票
        /// </summary>
        /// <param name="radialWorkId"></param>
        public static void DeleteRadialWorkById(string radialWorkId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_RadialWork radialWork = db.License_RadialWork.FirstOrDefault(e => e.RadialWorkId == radialWorkId);
            if (radialWork != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(radialWorkId);
                ///删除附件
                CommonService.DeleteAttachFileById(radialWorkId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(radialWorkId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(radialWorkId);
                db.License_RadialWork.DeleteOnSubmit(radialWork);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 断路(占道)作业票
        /// <summary>
        /// 根据主键获取断路(占道)作业票
        /// </summary>
        /// <param name="openCircuitId"></param>
        /// <returns></returns>
        public static Model.License_OpenCircuit GetOpenCircuitById(string openCircuitId)
        {
            return Funs.DB.License_OpenCircuit.FirstOrDefault(e => e.OpenCircuitId == openCircuitId);
        }

        /// <summary>
        /// 添加断路(占道)作业票
        /// </summary>
        /// <param name="openCircuit"></param>
        public static void AddOpenCircuit(Model.License_OpenCircuit openCircuit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_OpenCircuit newOpenCircuit = new Model.License_OpenCircuit
            {
                OpenCircuitId = openCircuit.OpenCircuitId,
                ProjectId = openCircuit.ProjectId,
                LicenseCode = openCircuit.LicenseCode,
                ApplyUnitId = openCircuit.ApplyUnitId,
                ApplyManId = openCircuit.ApplyManId,
                ApplyDate = openCircuit.ApplyDate,
                WorkPalce = openCircuit.WorkPalce,
                WorkMeasures = openCircuit.WorkMeasures,
                ValidityStartTime = openCircuit.ValidityStartTime,
                ValidityEndTime = openCircuit.ValidityEndTime,
                RoadName=openCircuit.RoadName,
                SafeMeasures = openCircuit.SafeMeasures,
                CancelManId = openCircuit.CancelManId,
                CancelReasons = openCircuit.CancelReasons,
                CancelTime = openCircuit.CancelTime,
                CloseManId = openCircuit.CloseManId,
                CloseReasons = openCircuit.CloseReasons,
                CloseTime = openCircuit.CloseTime,
                NextManId = openCircuit.NextManId,
                States = openCircuit.States,
            };
            db.License_OpenCircuit.InsertOnSubmit(newOpenCircuit);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectOpenCircuitMenuId, openCircuit.ProjectId, openCircuit.ApplyUnitId, openCircuit.OpenCircuitId, openCircuit.ApplyDate);
        }

        /// <summary>
        /// 修改断路(占道)作业票
        /// </summary>
        /// <param name="openCircuit"></param>
        public static void UpdateOpenCircuit(Model.License_OpenCircuit openCircuit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_OpenCircuit newOpenCircuit = db.License_OpenCircuit.FirstOrDefault(e => e.OpenCircuitId == openCircuit.OpenCircuitId);
            if (newOpenCircuit != null)
            {
                newOpenCircuit.WorkPalce = openCircuit.WorkPalce;
                newOpenCircuit.WorkMeasures = openCircuit.WorkMeasures;
                newOpenCircuit.ValidityStartTime = openCircuit.ValidityStartTime;
                newOpenCircuit.ValidityEndTime = openCircuit.ValidityEndTime;                
                newOpenCircuit.RoadName = openCircuit.RoadName;
                newOpenCircuit.SafeMeasures = openCircuit.SafeMeasures;
                newOpenCircuit.CancelManId = openCircuit.CancelManId;
                newOpenCircuit.CancelReasons = openCircuit.CancelReasons;
                newOpenCircuit.CancelTime = openCircuit.CancelTime;
                newOpenCircuit.CloseManId = openCircuit.CloseManId;
                newOpenCircuit.CloseReasons = openCircuit.CloseReasons;
                newOpenCircuit.CloseTime = openCircuit.CloseTime;
                newOpenCircuit.NextManId = openCircuit.NextManId;
                newOpenCircuit.States = openCircuit.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除断路(占道)作业票
        /// </summary>
        /// <param name="openCircuitId"></param>
        public static void DeleteOpenCircuitById(string openCircuitId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_OpenCircuit openCircuit = db.License_OpenCircuit.FirstOrDefault(e => e.OpenCircuitId == openCircuitId);
            if (openCircuit != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(openCircuitId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(openCircuitId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(openCircuitId);
                db.License_OpenCircuit.DeleteOnSubmit(openCircuit);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 动土作业票
        /// <summary>
        /// 根据主键获取动土作业票
        /// </summary>
        /// <param name="breakGroundId"></param>
        /// <returns></returns>
        public static Model.License_BreakGround GetBreakGroundById(string breakGroundId)
        {
            return Funs.DB.License_BreakGround.FirstOrDefault(e => e.BreakGroundId == breakGroundId);
        }

        /// <summary>
        /// 添加动土作业票
        /// </summary>
        /// <param name="breakGround"></param>
        public static void AddBreakGround(Model.License_BreakGround breakGround)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_BreakGround newBreakGround = new Model.License_BreakGround
            {
                BreakGroundId = breakGround.BreakGroundId,
                ProjectId = breakGround.ProjectId,
                LicenseCode = breakGround.LicenseCode,
                ApplyUnitId = breakGround.ApplyUnitId,
                ApplyManId = breakGround.ApplyManId,
                ApplyDate = breakGround.ApplyDate,
                WorkPalce = breakGround.WorkPalce,
                WorkDepth = breakGround.WorkDepth,
                ValidityStartTime = breakGround.ValidityStartTime,
                ValidityEndTime = breakGround.ValidityEndTime,
                WorkMeasures = breakGround.WorkMeasures,
                CancelManId = breakGround.CancelManId,
                CancelReasons = breakGround.CancelReasons,
                CancelTime = breakGround.CancelTime,
                CloseManId = breakGround.CloseManId,
                CloseReasons = breakGround.CloseReasons,
                CloseTime = breakGround.CloseTime,
                NextManId = breakGround.NextManId,
                States = breakGround.States,
            };
            db.License_BreakGround.InsertOnSubmit(newBreakGround);
            db.SubmitChanges();
            ////增加一条编码记录
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectBreakGroundMenuId, breakGround.ProjectId, breakGround.ApplyUnitId, breakGround.BreakGroundId, breakGround.ApplyDate);
        }

        /// <summary>
        /// 修改动土作业票
        /// </summary>
        /// <param name="breakGround"></param>
        public static void UpdateBreakGround(Model.License_BreakGround breakGround)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_BreakGround newBreakGround = db.License_BreakGround.FirstOrDefault(e => e.BreakGroundId == breakGround.BreakGroundId);
            if (newBreakGround != null)
            {
                newBreakGround.WorkPalce = breakGround.WorkPalce;
                newBreakGround.WorkDepth = breakGround.WorkDepth;
                newBreakGround.ValidityStartTime = breakGround.ValidityStartTime;
                newBreakGround.ValidityEndTime = breakGround.ValidityEndTime;
                newBreakGround.WorkMeasures = breakGround.WorkMeasures;
                newBreakGround.CancelManId = breakGround.CancelManId;
                newBreakGround.CancelReasons = breakGround.CancelReasons;
                newBreakGround.CancelTime = breakGround.CancelTime;
                newBreakGround.CloseManId = breakGround.CloseManId;
                newBreakGround.CloseReasons = breakGround.CloseReasons;
                newBreakGround.CloseTime = breakGround.CloseTime;
                newBreakGround.NextManId = breakGround.NextManId;
                newBreakGround.States = breakGround.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除动土作业票
        /// </summary>
        /// <param name="breakGroundId"></param>
        public static void DeleteBreakGroundById(string breakGroundId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_BreakGround breakGround = db.License_BreakGround.FirstOrDefault(e => e.BreakGroundId == breakGroundId);
            if (breakGround != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(breakGroundId);
                ////删除附件
                CommonService.DeleteAttachFileById(breakGroundId);
                ///删除-安全措施
                DeleteLicenseItemByDataId(breakGroundId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(breakGroundId);
                db.License_BreakGround.DeleteOnSubmit(breakGround);
                db.SubmitChanges();
            }
        }
        #endregion
        #endregion

        #region 作业票-安全措施
        /// <summary>
        /// 根据主键获取-安全措施
        /// </summary>
        /// <param name="licenseItemId"></param>
        /// <returns></returns>
        public static Model.License_LicenseItem GetLicenseItemById(string licenseItemId)
        {
            return Funs.DB.License_LicenseItem.FirstOrDefault(e => e.LicenseItemId == licenseItemId);
        }

        /// <summary>
        /// 根据作业票主键获取-安全措施列表
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.License_LicenseItem> GetLicenseItemListByDataId(string dataId)
        {
            return (from x in Funs.DB.License_LicenseItem
                    where x.DataId == dataId
                    orderby x.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 添加-安全措施
        /// </summary>
        /// <param name="licenseItem"></param>
        public static void AddLicenseItem(Model.License_LicenseItem licenseItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
            {
                LicenseItemId = SQLHelper.GetNewID(),
                DataId = licenseItem.DataId,
                SortIndex = licenseItem.SortIndex,
                SafetyMeasures = licenseItem.SafetyMeasures,
                IsUsed = licenseItem.IsUsed,
                ConfirmManId = licenseItem.ConfirmManId,
            };
            db.License_LicenseItem.InsertOnSubmit(newLicenseItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改-安全措施
        /// </summary>
        /// <param name="licenseItem"></param>
        public static void UpdateLicenseItem(Model.License_LicenseItem licenseItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LicenseItem newLicenseItem = db.License_LicenseItem.FirstOrDefault(e => e.LicenseItemId == licenseItem.LicenseItemId);
            if (newLicenseItem != null)
            {
                newLicenseItem.IsUsed = licenseItem.IsUsed;
                newLicenseItem.ConfirmManId = licenseItem.ConfirmManId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除-安全措施
        /// </summary>
        /// <param name="licenseItemId"></param>
        public static void DeleteLicenseItemById(string licenseItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LicenseItem licenseItem = db.License_LicenseItem.FirstOrDefault(e => e.LicenseItemId == licenseItemId);
            if (licenseItem != null)
            {                      
                db.License_LicenseItem.DeleteOnSubmit(licenseItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据作业票主键删除-安全措施
        /// </summary>
        /// <param name="dataId"></param>
        public static void DeleteLicenseItemByDataId(string dataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var licenseItems = from x in db.License_LicenseItem where x.DataId == dataId select x;
            if (licenseItems.Count()>0)
            {
                db.License_LicenseItem.DeleteAllOnSubmit(licenseItems);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 作业票审核
        #region 作业票审核信息
        /// <summary>
        /// 根据主键获取审核信息
        /// </summary>
        /// <param name="flowOperateId"></param>
        /// <returns></returns>
        public static Model.License_FlowOperate GetFlowOperateById(string flowOperateId)
        {
            return Funs.DB.License_FlowOperate.FirstOrDefault(e => e.FlowOperateId == flowOperateId);
        }

        /// <summary>
        /// 根据作业票主键获取审核信息列表
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static List<Model.License_FlowOperate> GetFlowOperateListByDataId(string dataId)
        {
            return (from x in Funs.DB.License_FlowOperate
                    where x.DataId == dataId
                    orderby x.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 添加审核信息
        /// </summary>
        /// <param name="flowOperate"></param>
        public static void AddFlowOperate(Model.License_FlowOperate flowOperate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FlowOperate newFlowOperate = new Model.License_FlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(),
                DataId = flowOperate.DataId,
                MenuId = flowOperate.MenuId,
                OperaterId=flowOperate.OperaterId,
                AuditFlowName = flowOperate.AuditFlowName,
                SortIndex = flowOperate.SortIndex,
                RoleIds = flowOperate.RoleIds,               
                IsFlowEnd = flowOperate.IsFlowEnd,
            };
            db.License_FlowOperate.InsertOnSubmit(newFlowOperate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改审核信息
        /// </summary>
        /// <param name="flowOperate"></param>
        public static void UpdateFlowOperate(Model.License_FlowOperate flowOperate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FlowOperate newFlowOperate = db.License_FlowOperate.FirstOrDefault(e => e.FlowOperateId == flowOperate.FlowOperateId);
            if (newFlowOperate != null)
            {
                newFlowOperate.OperaterId = flowOperate.OperaterId;
                newFlowOperate.OperaterTime = flowOperate.OperaterTime;
                newFlowOperate.IsAgree = flowOperate.IsAgree;
                newFlowOperate.Opinion = flowOperate.Opinion;
                newFlowOperate.IsClosed = flowOperate.IsClosed;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除审核信息
        /// </summary>
        /// <param name="flowOperateId"></param>
        public static void DeleteFlowOperateById(string flowOperateId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FlowOperate flowOperate = db.License_FlowOperate.FirstOrDefault(e => e.FlowOperateId == flowOperateId);
            if (flowOperate != null)
            {
                DeleteFlowOperateItemByDataId(flowOperateId);
                db.License_FlowOperate.DeleteOnSubmit(flowOperate);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据作业票主键删除审核信息
        /// </summary>
        /// <param name="dataId"></param>
        public static void DeleteFlowOperateByDataId(string dataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var flowOperates = from x in db.License_FlowOperate where x.DataId == dataId select x;
            if (flowOperates.Count() > 0)
            {
                foreach (var item in flowOperates)
                {
                    DeleteFlowOperateById(item.FlowOperateId);
                }
            }
        }
        #endregion

        #region 作业票审核明细信息
        /// <summary>
        /// 根据主键获取审核明细信息
        /// </summary>
        /// <param name="flowOperateItemId"></param>
        /// <returns></returns>
        public static Model.License_FlowOperateItem GetFlowOperateItemById(string flowOperateItemId)
        {
            return Funs.DB.License_FlowOperateItem.FirstOrDefault(e => e.FlowOperateItemId == flowOperateItemId);
        }

        /// <summary>
        /// 根据作业票主键获取审核明细信息列表
        /// </summary>
        /// <param name="flowOperateId"></param>
        /// <returns></returns>
        public static List<Model.License_FlowOperateItem> GetFlowOperateItemListByDataId(string flowOperateId)
        {
            return (from x in Funs.DB.License_FlowOperateItem
                    where x.FlowOperateId == flowOperateId
                    orderby x.OperaterTime
                    select x).ToList();
        }

        /// <summary>
        /// 添加审核明细信息
        /// </summary>
        /// <param name="flowOperateItem"></param>
        public static void AddFlowOperateItem(Model.License_FlowOperateItem flowOperateItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_FlowOperateItem newFlowOperateItem = new Model.License_FlowOperateItem
            {
                FlowOperateItemId = flowOperateItem.FlowOperateItemId,
                FlowOperateId = flowOperateItem.FlowOperateId,
                OperaterId = flowOperateItem.OperaterId,
                OperaterTime = flowOperateItem.OperaterTime,
                IsAgree = flowOperateItem.IsAgree,
                Opinion = flowOperateItem.Opinion,
            };
            db.License_FlowOperateItem.InsertOnSubmit(newFlowOperateItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据作业票主键删除审核明细信息
        /// </summary>
        /// <param name="flowOperateId"></param>
        public static void DeleteFlowOperateItemByDataId(string flowOperateId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var flowOperateItems = from x in db.License_FlowOperateItem where x.FlowOperateId == flowOperateId select x;
            if (flowOperateItems.Count() > 0)
            {
                db.License_FlowOperateItem.DeleteAllOnSubmit(flowOperateItems);
                db.SubmitChanges();
            }
        }
        #endregion
        #endregion
    }
}
