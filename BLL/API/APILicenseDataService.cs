using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

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
        public static Model.LicenseDataItem getLicenseDataById(string licenseType, string id)
        {
            Model.LicenseDataItem getInfo = new Model.LicenseDataItem();
            if (licenseType == "DH")
            {
                #region 作业票
                getInfo = (from x in Funs.DB.License_FireWork
                           where x.FireWorkId == id
                           select new Model.LicenseDataItem
                           {
                               LicenseId = x.FireWorkId,
                               LicenseType = licenseType,
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
                #endregion
            }

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
        public static List<Model.LicenseDataItem> getLicenseDataList(string licenseType, string projectId, string unitId, string states)
        {
            List<Model.LicenseDataItem> getInfoList = new List<Model.LicenseDataItem>();
            if (licenseType == "DH")
            {
                #region 作业票
                getInfoList = (from x in Funs.DB.License_FireWork
                               where x.ProjectId == projectId && (x.ApplyUnitId == unitId || unitId == null)
                                    && (states == null || x.States == states)
                               orderby x.LicenseCode descending
                               select new Model.LicenseDataItem
                               {
                                   LicenseId = x.FireWorkId,
                                   LicenseType= licenseType,
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
                #endregion
            }
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
                               where x.DataId == dataId
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
            string strMenuId = string.Empty;
            if (newItem.LicenseType == "DH")
            {
                #region 作业票
                strMenuId = Const.ProjectFireWorkMenuId;
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
                    States = newItem.States,
                };

                var updateFireWork = Funs.DB.License_FireWork.FirstOrDefault(x => x.FireWorkId == newItem.LicenseId);
                if (updateFireWork == null)
                {
                    newFireWork.ApplyDate = DateTime.Now;
                    newFireWork.FireWorkId = SQLHelper.GetNewID();
                    newFireWork.NextManId = newItem.ApplyManId;
                    newFireWork.LicenseCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectFireWorkMenuId, newFireWork.ProjectId, newFireWork.ApplyUnitId);
                    LicensePublicService.AddFireWork(newFireWork);
                }
                else
                {
                    updateFireWork.WorkPalce = newFireWork.WorkPalce;
                    updateFireWork.FireWatchManId = newFireWork.FireWatchManId;
                    updateFireWork.ValidityStartTime = newFireWork.ValidityStartTime;
                    updateFireWork.ValidityEndTime = newFireWork.ValidityEndTime;
                    updateFireWork.WorkMeasures = newFireWork.WorkMeasures;
                    updateFireWork.CancelManId = newFireWork.CancelManId;
                    updateFireWork.CancelReasons = newFireWork.CancelReasons;
                    updateFireWork.CancelTime = newFireWork.CancelTime;
                    updateFireWork.CloseManId = newFireWork.CloseManId;
                    updateFireWork.CloseReasons = newFireWork.CloseReasons;
                    updateFireWork.CloseTime = newFireWork.CloseTime;
                    updateFireWork.States = newFireWork.States;
                    LicensePublicService.UpdateFireWork(newFireWork);
                    ////删除安全措施
                    LicensePublicService.DeleteLicenseItemByDataId(newItem.LicenseId);
                }
                #endregion
            }
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
                }
                else
                {
                    var SysMenuFlowOperate = from x in db.Sys_MenuFlowOperate
                                             where x.MenuId == strMenuId
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
                            LicensePublicService.AddFlowOperate(newFlowOperate);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存作业票审核信息
        /// </summary>
        /// <param name="newItem">保存作业票审核信息</param>
        /// <returns></returns>
        public static void SaveLicenseFlowOperate(Model.FlowOperateItem newItem)
        { 
            ////审核记录
            //var getFlowOperate = newItem.FlowOperateItem;
        }
    }
}
