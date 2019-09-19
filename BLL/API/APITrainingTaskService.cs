using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainingTaskService
    {
        #region 根据ProjectId、PersonId获取培训任务教材明细列表
        /// <summary>
        /// 根据ProjectId、PersonId获取培训任务教材明细列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="personId"></param>
        /// <param name="states">0-未发布；1-已发布；2-已完成</param>
        /// <returns></returns>
        public static List<Model.TrainingTaskItem> getTrainingTaskListByProjectIdPersonId(string projectId, string personId, string states)
        {
            var getDataLists = (from x in Funs.DB.Training_Task
                                join y in Funs.DB.Training_Plan on x.PlanId equals y.PlanId
                                where x.ProjectId == projectId && x.UserId == personId && (x.States == states || states == null)
                                orderby x.TaskDate descending
                                select new Model.TrainingTaskItem
                                {
                                    TaskId = x.TaskId,
                                    //PlanId = x.PlanId,
                                    PlanCode = y.PlanCode,
                                    PlanName = y.PlanName,
                                    TrainStartDate = string.Format("{0:yyyy-MM-dd}", y.TrainStartDate),
                                    TeachAddress = y.TeachAddress,
                                    //PersonId = x.UserId,
                                    PersonName = Funs.DB.SitePerson_Person.FirstOrDefault(p => p.PersonId == x.UserId).PersonName,
                                    TaskDate = string.Format("{0:yyyy-MM-dd}", x.TaskDate),
                                    TrainTypeName = Funs.DB.Base_TrainType.FirstOrDefault(b => b.TrainTypeId == y.TrainTypeId).TrainTypeName,
                                    TrainLevelName = Funs.DB.Base_TrainLevel.FirstOrDefault(b => b.TrainLevelId == y.TrainLevelId).TrainLevelName,
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据TaskId获取培训任务教材明细列表
        /// <summary>
        /// 根据TaskId获取培训任务列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static List<Model.TrainingTaskItemItem> getTrainingTaskItemListByTaskId(string taskId)
        {
            ////生成培训任务下培训明细
            GetDataService.CreateTrainingTaskItemByTaskId(taskId);

            var getDataLists = (from x in Funs.DB.Training_TaskItem
                                where x.TaskId == taskId
                                orderby x.TrainingItemCode 
                                select new Model.TrainingTaskItemItem
                                {
                                    TaskItemId = x.TaskItemId,
                                    TaskId = x.TaskId,
                                    PlanId = x.PlanId,
                                    PersonId = x.PersonId,
                                    TrainingItemCode = x.TrainingItemCode,
                                    TrainingItemName = x.TrainingItemName,
                                    AttachUrl = x.AttachUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion
    }
}
