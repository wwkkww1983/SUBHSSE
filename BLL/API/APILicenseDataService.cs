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
        #region 根据FireWorkId获取作业票
        /// <summary>
        ///  根据 FireWorkId获取作业票
        /// </summary>
        /// <param name="licenseType"></param>
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
                                   SortIndex = x.SortIndex ?? 1,
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
                                   SortIndex = x.SortIndex ?? 1,
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
            Model.SUBHSSEDB db = Funs.DB;
            #region 动火作业票
            if (newItem.MenuId == Const.ProjectFireWorkMenuId)
            {
                Model.License_FireWork newFireWork = new Model.License_FireWork
                {
                    FireWorkId = newItem.LicenseId,
                    ProjectId = newItem.ProjectId,
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
                var updateFireWork = Funs.DB.License_FireWork.FirstOrDefault(x => x.FireWorkId == newItem.LicenseId);
                if (updateFireWork == null)
                {
                    newFireWork.ApplyDate = DateTime.Now;
                    newItem.LicenseId = newFireWork.FireWorkId = SQLHelper.GetNewID();                                      
                    newFireWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId);
                    LicensePublicService.AddFireWork(newFireWork);
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
                        ////删除安全措施
                        LicensePublicService.DeleteLicenseItemByDataId(newItem.LicenseId);
                        updateFireWork.WorkPalce = newFireWork.WorkPalce;
                        updateFireWork.FireWatchManId = newFireWork.FireWatchManId;
                        updateFireWork.ValidityStartTime = newFireWork.ValidityStartTime;
                        updateFireWork.ValidityEndTime = newFireWork.ValidityEndTime;
                        updateFireWork.WorkMeasures = newFireWork.WorkMeasures;
                        updateFireWork.NextManId = newFireWork.NextManId;
                        updateFireWork.States = newFireWork.States;
                    }
                    updateFireWork.States = newFireWork.States;
                    LicensePublicService.UpdateFireWork(updateFireWork);
                }
            }
            #endregion
            #region 高处作业票
            else if (newItem.MenuId == Const.ProjectHeightWorkMenuId)
            {
                Model.License_HeightWork newHeightWork = new Model.License_HeightWork
                {
                    HeightWorkId = newItem.LicenseId,
                    ProjectId = newItem.ProjectId,
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
                var updateHeightWork = Funs.DB.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == newItem.LicenseId);
                if (updateHeightWork == null)
                {
                    newHeightWork.ApplyDate = DateTime.Now;
                    newItem.LicenseId = newHeightWork.HeightWorkId = SQLHelper.GetNewID();
                    newHeightWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectHeightWorkMenuId, newHeightWork.ProjectId, newHeightWork.ApplyUnitId);
                    LicensePublicService.AddHeightWork(newHeightWork);
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
                        ////删除安全措施
                        LicensePublicService.DeleteLicenseItemByDataId(newItem.LicenseId);
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
                    LicensePublicService.UpdateHeightWork(updateHeightWork);
                }
            }
            #endregion

            #region 保存安全措施明细
            if (newItem.States == Const.State_0 || newItem.States == Const.State_1)
            {
                ///// 新增安全措施
                var getLicenseItemList = newItem.LicenseItems;
                if (getLicenseItemList.Count() > 0)
                {
                    foreach (var item in getLicenseItemList)
                    {
                        Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
                        {
                            DataId = newItem.LicenseId,
                            SortIndex = item.SortIndex,
                            SafetyMeasures = item.SafetyMeasures,
                            IsUsed = item.IsUsed,
                            ConfirmManId = item.ConfirmManId,
                        };

                        LicensePublicService.AddLicenseItem(newLicenseItem);
                    }
                }
            }
            #endregion

            #region 新增审核初始流程
            ////单据提交时 新增审核初始流程
            if (newItem.States == Const.State_1)
            {
                var gteFlowOperates = from x in db.License_FlowOperate where x.DataId == newItem.LicenseId select x;
                if (gteFlowOperates.Count() > 0)
                {
                    var noAgree = gteFlowOperates.FirstOrDefault(x => x.IsAgree == false && (x.IsClosed == false || x.IsClosed == null));
                    if (noAgree != null)
                    {
                        noAgree.IsAgree = null;
                        noAgree.Opinion = null;
                        noAgree.OperaterTime = null;
                        db.SubmitChanges();
                    }
                    else
                    {
                        var firtFlow = gteFlowOperates.Where(x => x.IsClosed == false || !x.IsClosed.HasValue).OrderBy(x => x.SortIndex).FirstOrDefault();
                        if (firtFlow != null)
                        {
                            firtFlow.OperaterId = newItem.NextManId;
                            LicensePublicService.UpdateFlowOperate(firtFlow);
                        }
                    }
                }
                else
                {
                    var SysMenuFlowOperate = from x in db.Sys_MenuFlowOperate
                                             where x.MenuId == newItem.MenuId
                                             select x;
                    if (SysMenuFlowOperate.Count() > 0)
                    {
                        foreach (var item in SysMenuFlowOperate)
                        {
                            Model.License_FlowOperate newFlowOperate = new Model.License_FlowOperate
                            {
                                DataId = newItem.LicenseId,
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

                            LicensePublicService.AddFlowOperate(newFlowOperate);
                        }
                    }
                }                
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
                LicensePublicService.UpdateFlowOperate(updateFlowOperate);
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
                LicensePublicService.AddFlowOperateItem(newFlowOperateItem);

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
                            LicensePublicService.UpdateFlowOperate(getNextFlowOperate);
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
                                    LicensePublicService.UpdateFlowOperate(item);
                                }
                            }
                            newItem.IsFlowEnd = true;
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
                        if (newItem.IsAgree == true)
                        {
                            getFireWork.NextManId = newItem.NextOperaterId;
                            if (newItem.IsFlowEnd == true)
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
                            getFireWork.NextManId = getFireWork.ApplyManId;
                            getFireWork.States = Const.State_0;
                        }
                        LicensePublicService.UpdateFireWork(getFireWork);
                    }
                }
                #endregion
                #region 高处作业票
                else if (strMenuId == Const.ProjectHeightWorkMenuId)
                {
                    var getHeightWork = Funs.DB.License_HeightWork.FirstOrDefault(x => x.HeightWorkId == updateFlowOperate.DataId);
                    if (getHeightWork != null)
                    {
                        if (newItem.IsAgree == true)
                        {
                            getHeightWork.NextManId = newItem.NextOperaterId;
                            if (newItem.IsFlowEnd == true)
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
                            getHeightWork.NextManId = getHeightWork.ApplyManId;
                            getHeightWork.States = Const.State_0;
                        }
                        LicensePublicService.UpdateHeightWork(getHeightWork);
                    }
                }
                #endregion
            }
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
                                 where x.DataId == dataId && (!x.IsAgree.HasValue || x.IsAgree == true) && x.OperaterId != null && (!x.IsClosed.HasValue || x.IsClosed == false)
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
                                  where x.MenuId == strMenuId && x.FlowStep == 1
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
