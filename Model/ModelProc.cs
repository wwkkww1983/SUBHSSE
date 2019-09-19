namespace Model
{
    using System;
    using System.Data.Linq;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Linq.Mapping;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;

    public partial class SUBHSSEDB : System.Data.Linq.DataContext
    {
        /// <summary>
        /// 获取当前用户在移动端待办事项
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_APP_GetToDoItems]")]
        public IEnumerable<ToDoItem> Sp_APP_GetToDoItems([Parameter(DbType = "nvarchar(50)")] string projectId, [Parameter(DbType = "nvarchar(50)")] string userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), projectId, userId);
            return (ISingleResult<ToDoItem>)result.ReturnValue;
        }

        /// <summary>
        /// 获取人员培训教材
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_GetTraining_TaskItemTraining]")]
        public IEnumerable<TrainingTaskItemItem> Sp_GetTraining_TaskItemTraining([Parameter(DbType = "nvarchar(50)")] string planId, [Parameter(DbType = "nvarchar(200)")] string workPostId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), planId, workPostId);
            return (ISingleResult<TrainingTaskItemItem>)result.ReturnValue;
        }
    }
}
