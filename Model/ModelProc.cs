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
        ///// <summary>
        ///// µÃµ½²Ëµ¥
        ///// </summary>
        ///// <param name="unitcode"></param>
        ///// <param name="isono"></param>
        ///// <returns></returns>
        //[Function(Name = "[dbo].[sp_Sys_GetMenuByUserId]")]
        //public IEnumerable<SpSysMenuItem> SpGetMenuByUserId([Parameter(DbType = "nvarchar(50)")] string UserName, [Parameter(DbType = "nvarchar(50)")] string projectId)
        //{                        
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), UserName, projectId);
        //    return (ISingleResult<SpSysMenuItem>)result.ReturnValue;
        //}
    }
}
