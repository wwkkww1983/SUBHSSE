﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainingPlanService
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训计划列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训计划列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.TrainingPlanItem> getTrainingPlanListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string states)
        {
            var getDataLists = (from x in Funs.DB.Training_Plan
                                where x.ProjectId == projectId && (x.States == states || states == null) && x.TrainTypeId == trainTypeId
                                orderby x.TrainStartDate descending
                                select new Model.TrainingPlanItem
                                {
                                    PlanId = x.PlanId,
                                    PlanCode = x.PlanCode,
                                    PlanName = x.PlanName,
                                    ProjectId = x.ProjectId,
                                    TrainTypeId = x.TrainTypeId,
                                    TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                    TrainLevelId = x.TrainLevelId,
                                    TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                    DesignerName = Funs.DB.Sys_User.First(y => y.UserId == x.DesignerId).UserName,
                                    DesignerId = x.DesignerId,
                                    DesignerDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.DesignerDate),
                                    TeachHour = x.TeachHour ?? 0,
                                    TeachAddress = x.TeachAddress,
                                    TeachMan = x.TeachMan,
                                    TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                    States = x.States,
                                    QRCodeUrl = x.QRCodeUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据培训ID获取培训计划详细
        /// <summary>
        /// 根据培训ID获取培训计划详细
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static Model.TrainingPlanItem getTrainingPlanByTrainingId(string planId)
        {
            var getDataLists = from x in Funs.DB.Training_Plan
                               where x.PlanId == planId
                               select new Model.TrainingPlanItem
                               {
                                   PlanId = x.PlanId,
                                   PlanCode = x.PlanCode,
                                   PlanName = x.PlanName,
                                   ProjectId = x.ProjectId,
                                   TrainTypeId = x.TrainTypeId,
                                   TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                   TrainLevelId = x.TrainLevelId,
                                   TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                   TeachHour = x.TeachHour ?? 0,
                                   TeachAddress = x.TeachAddress,
                                   TrainStartDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                   TeachMan=x.TeachMan,
                                   UnitIds = x.UnitIds,
                                   WorkPostId = x.WorkPostId,
                                   TrainContent = x.TrainContent,
                                   UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                   WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostId),
                                   DesignerId = x.DesignerId,
                                   DesignerName = Funs.DB.Sys_User.First(y => y.UserId == x.DesignerId).UserName,
                                   DesignerDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.TrainStartDate),
                                   States = x.States,
                                   QRCodeUrl =x.QRCodeUrl.Replace('\\', '/'),
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion

        #region 根据TrainingPlanId获取培训教材类型列表
        /// <summary>
        /// 根据TrainingPlanId获取培训教材类型列表
        /// </summary>
        /// <param name="trainingPlanId"></param>
        /// <returns>培训计划人员</returns>
        public static List<Model.TrainingPlanItemItem> getTrainingPlanItemListByTrainingPlanId(string trainingPlanId)
        {
            var getDataLists = (from x in Funs.DB.Training_PlanItem
                                join y in Funs.DB.Training_CompanyTraining on x.CompanyTrainingId equals y.CompanyTrainingId
                                where x.PlanId == trainingPlanId
                                orderby y.CompanyTrainingCode
                                select new Model.TrainingPlanItemItem
                                {
                                    PlanItemId = x.PlanItemId,
                                    PlanId = x.PlanId,
                                    CompanyTrainingId = x.CompanyTrainingId,
                                    CompanyTrainingName = y.CompanyTrainingName,
                                    CompanyTrainingCode = y.CompanyTrainingCode,
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 保存培训计划
        /// <summary>
        /// 保存TrainingPlan
        /// </summary>
        /// <param name="trainingPlan">培训计划记录</param>
        /// <param name="trainingTasks">培训人员list</param>
        /// <param name="trainingPlanItems">培训教材类型list</param>
        public static void SaveTrainingPlan(Model.TrainingPlanItem trainingPlan)
        {            
            Model.Training_Plan newTrainingPlan = new Model.Training_Plan
            {
                PlanId = trainingPlan.PlanId,
                PlanCode = trainingPlan.PlanCode,
                ProjectId = trainingPlan.ProjectId,
                DesignerId=trainingPlan.DesignerId,
                PlanName = trainingPlan.PlanName,
                TrainContent = trainingPlan.TrainContent,
                TrainStartDate = Funs.GetNewDateTime(trainingPlan.TrainStartDate),
                TeachHour = trainingPlan.TeachHour,
                TeachMan = trainingPlan.TeachMan,
                TeachAddress = trainingPlan.TeachAddress,
                TrainTypeId = trainingPlan.TrainTypeId,
                TrainLevelId = trainingPlan.TrainLevelId,
                UnitIds = trainingPlan.UnitIds,
                WorkPostId = trainingPlan.WorkPostId,
                States = trainingPlan.States,
            };
            if (newTrainingPlan.TrainStartDate.HasValue && newTrainingPlan.TeachHour.HasValue)
            {
                double dd = (double)((decimal)newTrainingPlan.TeachHour.Value);
                newTrainingPlan.TrainEndDate = newTrainingPlan.TrainStartDate.Value.AddHours(dd);
            }

            List<Model.TrainingTaskItem> trainingTasks = trainingPlan.TrainingTasks;
            List<Model.TrainingPlanItemItem> trainingPlanItems = trainingPlan.TrainingPlanItems;

            var isUpdate = Funs.DB.Training_Plan.FirstOrDefault(x => x.PlanId == newTrainingPlan.PlanId);
            if (isUpdate == null)
            {
                newTrainingPlan.DesignerDate = DateTime.Now;
                string unitId = string.Empty;
                var user = UserService.GetUserByUserId(newTrainingPlan.DesignerId);
                if (user != null)
                {
                    unitId = user.UnitId;
                }
                newTrainingPlan.PlanCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectTrainingPlanMenuId, newTrainingPlan.ProjectId, unitId);
                if (string.IsNullOrEmpty(newTrainingPlan.PlanId))
                {
                    newTrainingPlan.PlanId = SQLHelper.GetNewID();
                }
                Funs.DB.Training_Plan.InsertOnSubmit(newTrainingPlan);
                Funs.SubmitChanges();

                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectTrainingPlanMenuId, newTrainingPlan.ProjectId, null, newTrainingPlan.PlanId, newTrainingPlan.DesignerDate);
            }
            else
            {
                if (newTrainingPlan.States == "0" || newTrainingPlan.States == "1")
                {
                    isUpdate.PlanName = newTrainingPlan.PlanName;
                    isUpdate.TrainContent = newTrainingPlan.TrainContent;
                    isUpdate.TrainStartDate = newTrainingPlan.TrainStartDate;
                    isUpdate.TeachHour = newTrainingPlan.TeachHour;
                    isUpdate.TrainEndDate = newTrainingPlan.TrainEndDate;
                    isUpdate.TeachMan = newTrainingPlan.TeachMan;
                    isUpdate.TeachAddress = newTrainingPlan.TeachAddress;
                    isUpdate.TrainTypeId = newTrainingPlan.TrainTypeId;
                    isUpdate.TrainLevelId = newTrainingPlan.TrainLevelId;
                    isUpdate.UnitIds = newTrainingPlan.UnitIds;
                    isUpdate.WorkPostId = newTrainingPlan.WorkPostId;
                    isUpdate.States = newTrainingPlan.States;
                    Funs.SubmitChanges();
                }
                ////删除培训任务
                TrainingTaskService.DeleteTaskByPlanId(newTrainingPlan.PlanId);
                ////删除培训教材类型
                TrainingPlanItemService.DeletePlanItemByPlanId(newTrainingPlan.PlanId);              
            }

            if (trainingTasks.Count() > 0)
            {
                ////新增培训人员明细
                AddTraining_Task(trainingTasks, newTrainingPlan.PlanId, newTrainingPlan.ProjectId);
            }
            if (trainingPlanItems.Count() > 0)
            {
                ////新增培训教材类型明细
                AddTraining_PlanItem(trainingPlanItems, newTrainingPlan.PlanId);
            }
        }

        /// <summary>
        ///  新增 培训人员明细
        /// </summary>
        public static void AddTraining_Task(List<Model.TrainingTaskItem> trainingTasks, string planId, string projectId)
        {
            foreach (var item in trainingTasks)
            {
                if (!string.IsNullOrEmpty(item.PersonId))
                {
                    Model.Training_Task newTrainDetail = new Model.Training_Task
                    {
                        TaskId = SQLHelper.GetNewID(),
                        ProjectId = projectId,
                        PlanId = planId,
                        UserId = item.PersonId,
                        TaskDate = DateTime.Now,
                        States = Const.State_0, ////未生成培训教材明细
                    };

                    Funs.DB.Training_Task.InsertOnSubmit(newTrainDetail);
                    Funs.SubmitChanges();
                }
            }
        }

        /// <summary>
        ///  新增 培训教材类型 明细
        /// </summary>
        public static void AddTraining_PlanItem(List<Model.TrainingPlanItemItem> trainingPlanItems, string planId)
        {
            foreach (var item in trainingPlanItems)
            {
                if (!string.IsNullOrEmpty(item.CompanyTrainingId))
                {
                    Model.Training_PlanItem newPlanItem = new Model.Training_PlanItem
                    {
                        PlanItemId = SQLHelper.GetNewID(),
                        PlanId = planId,
                        CompanyTrainingId = item.CompanyTrainingId,
                    };

                    Funs.DB.Training_PlanItem.InsertOnSubmit(newPlanItem);
                    Funs.SubmitChanges();
                }
            }
        }
        #endregion
    }
}
