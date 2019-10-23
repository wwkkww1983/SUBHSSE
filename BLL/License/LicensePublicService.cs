using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                ///删除安全检查单
                DeleteLicenseItemByDataId(fireWorkId);
                ///删除作业票审核信息
                DeleteFlowOperateByDataId(fireWorkId);
                db.License_FireWork.DeleteOnSubmit(fireWork);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 作业票安全检查单
        /// <summary>
        /// 根据主键获取安全检查单
        /// </summary>
        /// <param name="licenseItemId"></param>
        /// <returns></returns>
        public static Model.License_LicenseItem GetLicenseItemById(string licenseItemId)
        {
            return Funs.DB.License_LicenseItem.FirstOrDefault(e => e.LicenseItemId == licenseItemId);
        }

        /// <summary>
        /// 根据作业票主键获取安全检查单列表
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
        /// 添加安全检查单
        /// </summary>
        /// <param name="licenseItem"></param>
        public static void AddLicenseItem(Model.License_LicenseItem licenseItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_LicenseItem newLicenseItem = new Model.License_LicenseItem
            {
                LicenseItemId = licenseItem.LicenseItemId,
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
        /// 修改安全检查单
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
        /// 根据主键删除安全检查单
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
        /// 根据作业票主键删除安全检查单
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
                FlowOperateId = flowOperate.FlowOperateId,
                DataId = flowOperate.DataId,
                AuditFlowName = flowOperate.AuditFlowName,
                SortIndex = flowOperate.SortIndex,
                RoleIds = flowOperate.RoleIds,
                OperaterId = flowOperate.OperaterId,
                OperaterTime = flowOperate.OperaterTime,
                IsAgree = flowOperate.IsAgree,
                Opinion = flowOperate.Opinion,
                IsClosed = flowOperate.IsClosed,
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
