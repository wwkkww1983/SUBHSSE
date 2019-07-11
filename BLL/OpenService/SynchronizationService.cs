namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SynchronizationService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        #region 自动
        /// <summary>
        /// 同步方法
        /// </summary>
        public static void SynchDataTime()
        {           
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null)
            {
                ///创建客户端服务
                var poxy = ServiceProxyBll.CreateServiceClient();               
                ///日志提取
                poxy.DataInsertSys_SubUnitLogListTableCompleted += Poxy_DataInsertSys_SubUnitLogListTableCompleted;
                var sysLog = from x in Funs.DB.View_Sys_Log
                             where x.UpState == Const.UpState_2
                             select new HSSEService.Sys_SubUnitLog
                             {
                                 LogId = x.LogId,
                                 UnitId = unit.UnitId,
                                 UserName = x.UserName,
                                 OperationTime = x.OperationTime,
                                 Ip = x.Ip,
                                 HostName = x.HostName,
                                 OperationLog = x.OperationLog,
                                 ProjectName = x.ProjectName,
                                 LogSource = x.LogSource,
                                 MenuId = x.MenuId,
                                 OperationName = x.OperationName,
                             };
                poxy.DataInsertSys_SubUnitLogListTableAsync(sysLog.ToList());

                // 版本信息
                poxy.GetSys_VersionToSUBCompleted += new EventHandler<HSSEService.GetSys_VersionToSUBCompletedEventArgs>(poxy_GetSys_VersionToSUBCompleted);
                poxy.GetSys_VersionToSUBAsync();
                ///催报信息
                poxy.GetInformation_UrgeReportToSUBCompleted += new EventHandler<HSSEService.GetInformation_UrgeReportToSUBCompletedEventArgs>(poxy_GetInformation_UrgeReportToSUBCompleted);
                poxy.GetInformation_UrgeReportToSUBAsync(unit.UnitId);
                ///安全监督检查整改
                poxy.GetCheck_CheckRectifyListToSUBCompleted += new EventHandler<HSSEService.GetCheck_CheckRectifyListToSUBCompletedEventArgs>(poxy_GetCheck_CheckRectifyListToSUBCompleted);
                poxy.GetCheck_CheckRectifyListToSUBAsync(unit.UnitId);
                ///安全监督检查报告
                poxy.GetCheck_CheckInfo_Table8ItemListToSUBCompleted += new EventHandler<HSSEService.GetCheck_CheckInfo_Table8ItemListToSUBCompletedEventArgs>(poxy_GetCheck_CheckInfo_Table8ItemListToSUBCompleted);
                poxy.GetCheck_CheckInfo_Table8ItemListToSUBAsync(unit.UnitId);
                ////企业安全文件上报
                poxy.GetSupervise_SubUnitReportListToSUBCompleted += new EventHandler<HSSEService.GetSupervise_SubUnitReportListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportListToSUBCompleted);
                poxy.GetSupervise_SubUnitReportListToSUBAsync();
                ///企业安全文件上报明细
                poxy.GetSupervise_SubUnitReportItemListToSUBCompleted += new EventHandler<HSSEService.GetSupervise_SubUnitReportItemListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportItemListToSUBCompleted);
                poxy.GetSupervise_SubUnitReportItemListToSUBAsync(unit.UnitId);
            }
        }
        #endregion

        #region 日志提取到集团
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Poxy_DataInsertSys_SubUnitLogListTableCompleted(object sender, HSSEService.DataInsertSys_SubUnitLogListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var log = Funs.DB.Sys_Log.FirstOrDefault(x => x.LogId == item);
                    if (log != null)
                    {
                        log.UpState = BLL.Const.UpState_3;
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 版本信息从集团公司提取
        /// <summary>
        /// 版本信息从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetSys_VersionToSUBCompleted(object sender, HSSEService.GetSys_VersionToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var versionItems = e.Result;
                if (versionItems.Count() > 0)
                {
                    count = versionItems.Count();
                    foreach (var item in versionItems)
                    {
                        var version = Funs.DB.Sys_Version.FirstOrDefault(x => x.VersionId == item.VersionId);
                        if (version == null)
                        {
                            Model.Sys_Version newVersion = new Model.Sys_Version
                            {
                                VersionId = item.VersionId,
                                VersionName = item.VersionName,
                                VersionDate = item.VersionDate,
                                CompileMan = item.CompileMan,
                                AttachUrl = item.AttachUrl,
                                IsSub = item.IsSub
                            };
                            Funs.DB.Sys_Version.InsertOnSubmit(newVersion);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 催报信息从集团公司提取到企业
        /// <summary>
        /// 催报信息从集团公司提取到企业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetInformation_UrgeReportToSUBCompleted(object sender, HSSEService.GetInformation_UrgeReportToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var UrgeReportItems = e.Result;
                if (UrgeReportItems.Count() > 0)
                {
                    count = UrgeReportItems.Count();
                    foreach (var item in UrgeReportItems)
                    {
                        var urg = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UrgeReportId == item.UrgeReportId);
                        if (urg == null)
                        {
                            Model.Information_UrgeReport newUrgeReport = new Model.Information_UrgeReport
                            {
                                UrgeReportId = item.UrgeReportId,
                                UnitId = item.UnitId,
                                ReprotType = item.ReprotType,
                                YearId = item.YearId,
                                MonthId = item.MonthId,
                                QuarterId = item.QuarterId,
                                HalfYearId = item.HalfYearId,
                                UrgeDate = item.UrgeDate,
                                IsComplete = null,
                                IsCancel = item.IsCancel
                            };
                            Funs.DB.Information_UrgeReport.InsertOnSubmit(newUrgeReport);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            urg.IsCancel = item.IsCancel;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 安全监督检查整改信息从集团公司提取
        /// <summary>
        /// 安全监督检查整改从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetCheck_CheckRectifyListToSUBCompleted(object sender, HSSEService.GetCheck_CheckRectifyListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                List<string> ids = new List<string>();
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        if (!ids.Contains(item.CheckRectifyId))
                        {
                            var newRectify = BLL.CheckRectifyService.GetCheckRectifyByCheckRectifyId(item.CheckRectifyId);
                            if (newRectify == null)
                            {
                                ids.Add(item.CheckRectifyId);
                                Model.Check_CheckRectify newCheckRectify = new Model.Check_CheckRectify
                                {
                                    CheckRectifyId = item.CheckRectifyId,
                                    CheckRectifyCode = item.CheckRectifyCode,
                                    ProjectId = item.ProjectId,
                                    UnitId = item.UnitId,
                                    CheckDate = item.CheckDate,
                                    IssueMan = item.IssueMan,
                                    IssueDate = item.IssueDate,
                                    HandleState = item.HandleState
                                };
                                Funs.DB.Check_CheckRectify.InsertOnSubmit(newCheckRectify);
                                Funs.DB.SubmitChanges();

                                //获取对应主表主键的明细集合
                                var table5Items = items.Where(x => x.CheckRectifyId == item.CheckRectifyId);
                                foreach (var newItem in table5Items)
                                {
                                    var oldItem5 = Funs.DB.Check_CheckInfo_Table5Item.FirstOrDefault(x => x.ID == newItem.Table5ItemId);
                                    if (oldItem5 == null)
                                    {
                                        Model.Check_CheckInfo_Table5Item newCheckRectifyItem = new Model.Check_CheckInfo_Table5Item
                                        {
                                            ID = newItem.Table5ItemId,
                                            SortIndex = newItem.SortIndex,
                                            WorkType = newItem.WorkType,
                                            DangerPoint = newItem.DangerPoint,
                                            RiskExists = newItem.RiskExists,
                                            IsProject = newItem.IsProject,
                                            CheckMan = newItem.CheckMan,
                                            SubjectUnitMan = newItem.SubjectUnitMan
                                        };
                                        Funs.DB.Check_CheckInfo_Table5Item.InsertOnSubmit(newCheckRectifyItem);
                                        Funs.DB.SubmitChanges();

                                        ////上传附件
                                        if (!string.IsNullOrEmpty(newItem.AttachFileId))
                                        {
                                            BLL.FileInsertService.InsertAttachFile(newItem.AttachFileId, newItem.Table5ItemId, newItem.AttachSource, newItem.AttachUrl, newItem.FileContext);
                                        }
                                    }

                                    var oldItem = BLL.CheckRectifyItemService.GetCheckRectifyItemByCheckRectifyItemId(newItem.CheckRectifyItemId);
                                    if (oldItem == null)
                                    {
                                        Model.Check_CheckRectifyItem newCheckRectifyItem = new Model.Check_CheckRectifyItem
                                        {
                                            CheckRectifyItemId = newItem.CheckRectifyItemId,
                                            CheckRectifyId = newItem.CheckRectifyId,
                                            Table5ItemId = newItem.Table5ItemId,
                                            ConfirmMan = newItem.ConfirmMan,
                                            ConfirmDate = newItem.ConfirmDate,
                                            OrderEndDate = newItem.OrderEndDate,
                                            OrderEndPerson = newItem.OrderEndPerson,
                                            RealEndDate = newItem.RealEndDate
                                        };
                                        Funs.DB.Check_CheckRectifyItem.InsertOnSubmit(newCheckRectifyItem);
                                        Funs.DB.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 安全监督检查报告信息从集团公司提取
        /// <summary>
        /// 安全监督检查报告从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetCheck_CheckInfo_Table8ItemListToSUBCompleted(object sender, HSSEService.GetCheck_CheckInfo_Table8ItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                List<string> ids = new List<string>();
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        if (!ids.Contains(item.CheckInfoId))
                        {
                            var updateCheckInfo = Funs.DB.Check_CheckInfo.FirstOrDefault(x => x.CheckInfoId == item.CheckInfoId);
                            if (updateCheckInfo == null)
                            {
                                ids.Add(item.CheckInfoId);
                                Model.Check_CheckInfo newCheckInfo = new Model.Check_CheckInfo
                                {
                                    CheckInfoId = item.CheckInfoId,
                                    CheckTypeName = item.CheckTypeName,
                                    SubjectUnitId = item.SubjectUnitId,
                                    SubjectUnitAdd = item.SubjectUnitAdd,
                                    SubjectUnitMan = item.SubjectUnitMan,
                                    SubjectUnitTel = item.SubjectUnitTel,
                                    CheckStartTime = item.CheckStartTime,
                                    CheckEndTime = item.CheckEndTime,
                                    SubjectObject = item.SubjectObject
                                };
                                Funs.DB.Check_CheckInfo.InsertOnSubmit(newCheckInfo);
                                Funs.DB.SubmitChanges();
                            }
                            else
                            {
                                updateCheckInfo.CheckInfoId = item.CheckInfoId;
                                updateCheckInfo.CheckTypeName = item.CheckTypeName;
                                updateCheckInfo.SubjectUnitId = item.SubjectUnitId;
                                updateCheckInfo.SubjectUnitAdd = item.SubjectUnitAdd;
                                updateCheckInfo.SubjectUnitMan = item.SubjectUnitMan;
                                updateCheckInfo.SubjectUnitTel = item.SubjectUnitTel;
                                updateCheckInfo.CheckStartTime = item.CheckStartTime;
                                updateCheckInfo.CheckEndTime = item.CheckEndTime;
                                updateCheckInfo.SubjectObject = item.SubjectObject;
                                Funs.DB.SubmitChanges();
                            }
                        }

                        var updateTable8 = Funs.DB.Check_CheckInfo_Table8.FirstOrDefault(x => x.CheckItemId == item.CheckItemId);
                        if (updateTable8 == null)
                        {
                            Model.Check_CheckInfo_Table8 newTable8 = new Model.Check_CheckInfo_Table8
                            {
                                CheckItemId = item.CheckItemId,
                                CheckInfoId = item.CheckInfoId,
                                Values1 = item.Values1,
                                Values2 = item.Values2,
                                Values3 = item.Values3,
                                Values4 = item.Values4,
                                Values5 = item.Values5,
                                Values6 = item.Values6,
                                Values7 = item.Values7,
                                Values8 = item.Values8
                            };
                            Funs.DB.Check_CheckInfo_Table8.InsertOnSubmit(newTable8);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            updateTable8.Values1 = item.Values1;
                            updateTable8.Values2 = item.Values2;
                            updateTable8.Values3 = item.Values3;
                            updateTable8.Values4 = item.Values4;
                            updateTable8.Values5 = item.Values5;
                            updateTable8.Values6 = item.Values6;
                            updateTable8.Values7 = item.Values7;
                            updateTable8.Values8 = item.Values8;
                            Funs.DB.SubmitChanges();
                        }

                        var updateTable8Item = Funs.DB.Check_CheckInfo_Table8Item.FirstOrDefault(x => x.ID == item.ID);
                        if (updateTable8Item == null)
                        {
                            Model.Check_CheckInfo_Table8Item newTable8Item = new Model.Check_CheckInfo_Table8Item
                            {
                                ID = item.ID,
                                CheckInfoId = item.CheckInfoId,
                                Name = item.Name,
                                Sex = item.Sex,
                                UnitName = item.UnitName,
                                PostName = item.PostName,
                                WorkTitle = item.WorkTitle,
                                CheckPostName = item.CheckPostName,
                                CheckDate = item.CheckDate,
                                SortIndex = item.SortIndex
                            };
                            Funs.DB.Check_CheckInfo_Table8Item.InsertOnSubmit(newTable8Item);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            updateTable8Item.ID = item.ID;
                            updateTable8Item.CheckInfoId = item.CheckInfoId;
                            updateTable8Item.Name = item.Name;
                            updateTable8Item.Sex = item.Sex;
                            updateTable8Item.UnitName = item.UnitName;
                            updateTable8Item.PostName = item.PostName;
                            updateTable8Item.WorkTitle = item.WorkTitle;
                            updateTable8Item.CheckPostName = item.CheckPostName;
                            updateTable8Item.CheckDate = item.CheckDate;
                            updateTable8Item.SortIndex = item.SortIndex;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 企业安全从集团公司提取
        /// <summary>
        /// 企业安全从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetSupervise_SubUnitReportListToSUBCompleted(object sender, HSSEService.GetSupervise_SubUnitReportListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var subUnitReportList = e.Result;
                if (subUnitReportList.Count() > 0)
                {
                    count = subUnitReportList.Count();
                    foreach (var item in subUnitReportList)
                    {
                        var newSubUnitReport = BLL.SubUnitReportService.GetSubUnitReportById(item.SubUnitReportId);
                        if (newSubUnitReport == null)
                        {
                            Model.Supervise_SubUnitReport newSubUnitReport1 = new Model.Supervise_SubUnitReport
                            {
                                SubUnitReportId = item.SubUnitReportId,
                                SubUnitReportCode = item.SubUnitReportCode,
                                SubUnitReportName = item.SubUnitReportName,
                                SupSubUnitReportId = item.SupSubUnitReportId,
                                IsEndLever = item.IsEndLever
                            };
                            Funs.DB.Supervise_SubUnitReport.InsertOnSubmit(newSubUnitReport1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newSubUnitReport.SubUnitReportCode = item.SubUnitReportCode;
                            newSubUnitReport.SubUnitReportName = item.SubUnitReportName;
                            newSubUnitReport.SupSubUnitReportId = item.SupSubUnitReportId;
                            newSubUnitReport.IsEndLever = item.IsEndLever;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }
        }
        #endregion

        #region 企业安全文件上报明细从集团公司提取
        /// <summary>
        /// 企业安全文件上报明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void poxy_GetSupervise_SubUnitReportItemListToSUBCompleted(object sender, HSSEService.GetSupervise_SubUnitReportItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.SubUnitReportItemService.GetSubUnitReportItemById(item.SubUnitReportItemId);
                        if (newItem == null)
                        {
                            var newSubUnitReport = BLL.SubUnitReportService.GetSubUnitReportById(item.SubUnitReportId);
                            if (newSubUnitReport != null)
                            {
                                Model.Supervise_SubUnitReportItem newItem1 = new Model.Supervise_SubUnitReportItem
                                {
                                    SubUnitReportItemId = item.SubUnitReportItemId,
                                    SubUnitReportId = item.SubUnitReportId,
                                    UnitId = item.UnitId,
                                    PlanReortDate = item.PlanReortDate,
                                    State = item.State
                                };
                                Funs.DB.Supervise_SubUnitReportItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.SubUnitReportId = item.SubUnitReportId;
                            newItem.UnitId = item.UnitId;
                            newItem.PlanReortDate = item.PlanReortDate;
                            newItem.State = item.State;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion
    }
}