using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训任务
    /// </summary>
    public static class TaskService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取培训任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Model.Training_Task GetTaskById(string taskId)
        {
            return db.Training_Task.FirstOrDefault(e => e.TaskId == taskId);
        }

        /// <summary>
        /// 添加培训任务
        /// </summary>
        /// <param name="task"></param>
        public static void AddTask(Model.Training_Task task)
        {
            Model.Training_Task newTask = new Model.Training_Task
            {
                TaskId = task.TaskId,
                PlanId = task.PlanId,
                UserId = task.UserId,
                TaskDate = task.TaskDate,
                States = task.States
            };
            db.Training_Task.InsertOnSubmit(newTask);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训任务
        /// </summary>
        /// <param name="task"></param>
        public static void UpdateTask(Model.Training_Task task)
        {
            Model.Training_Task newTask = db.Training_Task.FirstOrDefault(e => e.TaskId == task.TaskId);
            if (newTask != null)
            {
                newTask.PlanId = task.PlanId;
                newTask.UserId = task.UserId;
                newTask.TaskDate = task.TaskDate;
                //newTask.States = task.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训任务
        /// </summary>
        /// <param name="taskId"></param>
        public static void DeleteTaskById(string taskId)
        {
            Model.Training_Task task = db.Training_Task.FirstOrDefault(e => e.TaskId == taskId);
            if (task != null)
            {
                db.Training_Task.DeleteOnSubmit(task);
                db.SubmitChanges();
            }
        }
    }
}
