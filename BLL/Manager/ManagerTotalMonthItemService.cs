using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    public class ManagerTotalMonthItemService
    {
        /// <summary>
        /// 月总结-本月HSE工作存在问题与处理（或拟采取对策）
        /// </summary>
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 添加本月HSE工作存在问题与处理（或拟采取对策）
        /// </summary>
        /// <param name="managerTotalMonthItem"></param>
        public static void AddManagerTotalMonthItem(Model.Manager_ManagerTotalMonthItem managerTotalMonthItem)
        {
            Model.Manager_ManagerTotalMonthItem newManagerTotalMonthItem = new Model.Manager_ManagerTotalMonthItem
            {
                ManagerTotalMonthItemId = managerTotalMonthItem.ManagerTotalMonthItemId,
                ManagerTotalMonthId = managerTotalMonthItem.ManagerTotalMonthId,
                ExistenceHiddenDanger = managerTotalMonthItem.ExistenceHiddenDanger,
                CorrectiveActions = managerTotalMonthItem.CorrectiveActions,
                PlanCompletedDate = managerTotalMonthItem.PlanCompletedDate,
                ResponsiMan = managerTotalMonthItem.ResponsiMan,
                ActualCompledDate = managerTotalMonthItem.ActualCompledDate,
                Remark = managerTotalMonthItem.Remark
            };
            db.Manager_ManagerTotalMonthItem.InsertOnSubmit(newManagerTotalMonthItem);
            db.SubmitChanges();
        }


        /// <summary>
        /// 添加本月HSE工作存在问题与处理（或拟采取对策）
        /// </summary>
        /// <param name="managerTotalMonthItem"></param>
        public static void UpdateManagerTotalMonthItem(Model.Manager_ManagerTotalMonthItem managerTotalMonthItem)
        {
            Model.Manager_ManagerTotalMonthItem newManagerTotalMonthItem = db.Manager_ManagerTotalMonthItem.FirstOrDefault(x => x.ManagerTotalMonthItemId == managerTotalMonthItem.ManagerTotalMonthItemId);
            if (newManagerTotalMonthItem != null)
            {                
                newManagerTotalMonthItem.ExistenceHiddenDanger = managerTotalMonthItem.ExistenceHiddenDanger;
                newManagerTotalMonthItem.CorrectiveActions = managerTotalMonthItem.CorrectiveActions;
                newManagerTotalMonthItem.PlanCompletedDate = managerTotalMonthItem.PlanCompletedDate;
                newManagerTotalMonthItem.ResponsiMan = managerTotalMonthItem.ResponsiMan;
                newManagerTotalMonthItem.ActualCompledDate = managerTotalMonthItem.ActualCompledDate;
                newManagerTotalMonthItem.Remark = managerTotalMonthItem.Remark;               
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月总结明细ID删除所有本月HSE工作存在问题与处理（或拟采取对策）
        /// </summary>
        /// <param name="managerTotalMonthId"></param>
        public static void DeleteManagerTotalMonthItemByManagerTotalMonthItemId(string managerTotalMonthItemId)
        {
            var q = db.Manager_ManagerTotalMonthItem.FirstOrDefault(x => x.ManagerTotalMonthItemId == managerTotalMonthItemId);
            if (q != null)
            {
                db.Manager_ManagerTotalMonthItem.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月总结ID删除所有本月HSE工作存在问题与处理（或拟采取对策）
        /// </summary>
        /// <param name="managerTotalMonthId"></param>
        public static void DeleteManagerTotalMonthItemByManagerTotalMonthId(string managerTotalMonthId)
        {
            var q = (from x in db.Manager_ManagerTotalMonthItem where x.ManagerTotalMonthId == managerTotalMonthId select x).ToList();
            if (q.Count()>0)
            {
                db.Manager_ManagerTotalMonthItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全工作总结Id获取安全工作总结信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.SpManagerTotalMonthItem> GetManager_ManagerTotalMonthItem(string date)
        {
            DateTime? monthTime = Funs.GetNewDateTime(date);
            var managerTotalList = BLL.ManagerTotalMonthService.GetManagerTotalByDate(monthTime.Value);           
            var projectList = BLL.ProjectService.GetTotalMonthProjectWorkList(monthTime);
            List<Model.SpManagerTotalMonthItem> reportItemPartList = new List<Model.SpManagerTotalMonthItem>();
            int projectCount = projectList.Count();
            int rows = 1;
            int sortIndex = 1;           
            foreach (var projectItem in projectList)
            {
                var managerTotalProject = managerTotalList.FirstOrDefault(x => x.ProjectId == projectItem.ProjectId);
                if (managerTotalProject != null)
                {
                    var managerTotalItems = from x in Funs.DB.Manager_ManagerTotalMonthItem where x.ManagerTotalMonthId == managerTotalProject.ManagerTotalMonthId select x;
                    if (managerTotalItems.Count() > 0)
                    {
                        foreach (var item in managerTotalItems)
                        {
                            Model.SpManagerTotalMonthItem part = new Model.SpManagerTotalMonthItem
                            {
                                ID = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonthItem)),
                                ProjectName = projectItem.ProjectName,
                                ExistenceHiddenDanger = item.ExistenceHiddenDanger,
                                CorrectiveActions = item.CorrectiveActions,
                                PlanCompletedDate = string.Format("{0:yyyy-MM-dd}", item.PlanCompletedDate),
                                ResponsiMan = item.ResponsiMan,
                                ActualCompledDate = string.Format("{0:yyyy-MM-dd}", item.ActualCompledDate),
                                Remark = item.Remark,
                                SortIndex = sortIndex
                            };
                            reportItemPartList.Add(part);
                            sortIndex++;
                        }
                    }
                    else
                    {
                        Model.SpManagerTotalMonthItem part = new Model.SpManagerTotalMonthItem
                        {
                            ID = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonthItem)),
                            ProjectName = projectItem.ProjectName,
                            ExistenceHiddenDanger = "/",
                            CorrectiveActions = "/",
                            PlanCompletedDate = null,
                            ResponsiMan = "/",
                            ActualCompledDate = null,
                            Remark = "/",
                            SortIndex = sortIndex
                        };
                        reportItemPartList.Add(part);
                        sortIndex++;
                    }
                }
                else
                {
                    Model.SpManagerTotalMonthItem part = new Model.SpManagerTotalMonthItem
                    {
                        ID = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonthItem)),
                        ProjectName = projectItem.ProjectName,
                        ExistenceHiddenDanger = "/",
                        CorrectiveActions = "/",
                        PlanCompletedDate = "/",
                        ResponsiMan = "/",
                        ActualCompledDate = "/",
                        Remark = "/",
                        SortIndex = sortIndex
                    };
                    reportItemPartList.Add(part);
                    sortIndex++;
                }

                if (projectCount > rows)
                {
                    Model.SpManagerTotalMonthItem partNull = new Model.SpManagerTotalMonthItem
                    {
                        ID = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonthItem)),
                        ProjectName = "项目名称",
                        ExistenceHiddenDanger = "存在隐患",
                        CorrectiveActions = "整改措施",
                        PlanCompletedDate = "计划完成时间",
                        ResponsiMan = "责任人",
                        ActualCompledDate = "实际完成时间",
                        Remark = "备注",
                        SortIndex = sortIndex
                    };
                    reportItemPartList.Add(partNull);
                    sortIndex++;
                }

                rows++;
            }

            return reportItemPartList;
        }


        /// <summary>
        /// 根据安全工作总结Id获取安全工作总结信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.SpManagerTotalMonthSafetyDataDItem> GetManager_SafetyDataD(string date)
        {
            List<Model.SpManagerTotalMonthSafetyDataDItem> reportSafetyDataD = new List<Model.SpManagerTotalMonthSafetyDataDItem>();
            DateTime? monthTime = Funs.GetNewDateTime(date);
            var projectList = BLL.ProjectService.GetTotalMonthProjectWorkList(monthTime);             
            int sortIndex = 1;
            Model.SpManagerTotalMonthSafetyDataDItem sunmPart = new Model.SpManagerTotalMonthSafetyDataDItem
            {
                ID = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonth)),
                ProjectName = "合计",
                ThisUnitPersonNum = "0",
                ThisUnitHSEPersonNum = "0",
                SubUnitPersonNum = "0",
                SubUnitHSEPersonNum = "0",
                ManHours = "0",
                HSEManHours = "0"
            };

            foreach (var projectItem in projectList)
            {
                Model.SpManagerTotalMonthSafetyDataDItem part = new Model.SpManagerTotalMonthSafetyDataDItem
                {
                    ID = projectItem.ProjectId,
                    ProjectName = projectItem.ProjectName,
                    SortIndex = sortIndex,
                    ThisUnitPersonNum = "/",
                    ThisUnitHSEPersonNum = "/",
                    SubUnitPersonNum = "/",
                    SubUnitHSEPersonNum = "/",
                    ManHours = "/",
                    HSEManHours = "/"
                };
                var projectMontReportD = Funs.DB.Manager_MonthReportD.FirstOrDefault(x => x.ProjectId == projectItem.ProjectId && (x.Months.Value.Year == monthTime.Value.Year && x.Months.Value.Month == monthTime.Value.Month));
                if (projectMontReportD != null)
                {
                    var safetyDataD = Funs.DB.Manager_SafetyDataD.FirstOrDefault(x => x.MonthReportId == projectMontReportD.MonthReportId);
                    if (safetyDataD != null)
                    {
                        part.ThisUnitPersonNum = safetyDataD.ThisUnitPersonNum.ToString();
                        part.ThisUnitHSEPersonNum = safetyDataD.ThisUnitHSEPersonNum.ToString();
                        part.SubUnitPersonNum = safetyDataD.SubUnitPersonNum.ToString();
                        part.SubUnitHSEPersonNum = safetyDataD.SubUnitHSEPersonNum.ToString();
                        part.ManHours = safetyDataD.ManHours.ToString();
                        part.HSEManHours = safetyDataD.HSEManHours.ToString();

                        sunmPart.ThisUnitPersonNum = (Funs.GetNewIntOrZero(sunmPart.ThisUnitPersonNum) + Funs.GetNewIntOrZero(part.ThisUnitPersonNum)).ToString();
                        sunmPart.ThisUnitHSEPersonNum = (Funs.GetNewIntOrZero(sunmPart.ThisUnitHSEPersonNum) + Funs.GetNewIntOrZero(part.ThisUnitHSEPersonNum)).ToString();
                        sunmPart.SubUnitPersonNum = (Funs.GetNewIntOrZero(sunmPart.SubUnitPersonNum) + Funs.GetNewIntOrZero(part.SubUnitPersonNum)).ToString();
                        sunmPart.SubUnitHSEPersonNum = (Funs.GetNewIntOrZero(sunmPart.SubUnitHSEPersonNum) + Funs.GetNewIntOrZero(part.SubUnitHSEPersonNum)).ToString();
                        sunmPart.ManHours = (Funs.GetNewIntOrZero(sunmPart.ManHours) + Funs.GetNewIntOrZero(part.ManHours)).ToString();
                        sunmPart.HSEManHours = (Funs.GetNewIntOrZero(sunmPart.HSEManHours) + Funs.GetNewIntOrZero(part.HSEManHours)).ToString();

                    }
                }
                reportSafetyDataD.Add(part);
                sortIndex++;
            }
            sunmPart.SortIndex = projectList.Count() + 1;

            reportSafetyDataD.Add(sunmPart);
            return reportSafetyDataD;
        }
    }
}
