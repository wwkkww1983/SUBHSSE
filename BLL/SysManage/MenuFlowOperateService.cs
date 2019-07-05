using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class MenuFlowOperateService
   {        
       /// <summary>
       ///  根据工作流主键获取工作流信息
       /// </summary>
       /// <param name="flowOperateId"></param>
       /// <returns></returns>
       public static Model.Sys_MenuFlowOperate GetMenuFlowOperateByFlowOperateId(string flowOperateId)
       {
           return  Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x=> x.FlowOperateId == flowOperateId);
       }
        
       /// <summary>
       /// 添加工作流信息
       /// </summary>
       /// <param name="flow"></param>
       public static void AddAuditFlow(Model.Sys_MenuFlowOperate flow)
      {
            Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_MenuFlowOperate)),
                MenuId = flow.MenuId,
                FlowStep = flow.FlowStep,
                AuditFlowName = flow.AuditFlowName,
                RoleId = flow.RoleId,
                IsFlowEnd = flow.IsFlowEnd
            };
            Funs.DB.Sys_MenuFlowOperate.InsertOnSubmit(newMenuFlowOperate);
           Funs.DB.SubmitChanges();
       }

       /// <summary>
       /// 修改工作流信息
       /// </summary>
       /// <param name="flow"></param>
       public static void UpdateAuditFlow(Model.Sys_MenuFlowOperate flow)
       {           
           Model.Sys_MenuFlowOperate newMenuFlowOperate = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(e => e.FlowOperateId == flow.FlowOperateId);
           if (newMenuFlowOperate != null)
           {
               newMenuFlowOperate.MenuId = flow.MenuId;
               newMenuFlowOperate.FlowStep = flow.FlowStep;
               newMenuFlowOperate.AuditFlowName = flow.AuditFlowName;
               newMenuFlowOperate.RoleId = flow.RoleId;
               newMenuFlowOperate.IsFlowEnd = flow.IsFlowEnd;
               Funs.DB.SubmitChanges();
           }
       }

       /// <summary>
       /// 删除工作流信息
       /// </summary>
       /// <param name="FlowOperateId"></param>
       public static void DeleteAuditFlow(string FlowOperateId)
       {
           Model.Sys_MenuFlowOperate flow = Funs.DB.Sys_MenuFlowOperate.First(e => e.FlowOperateId == FlowOperateId);
           if (flow != null)
           {
               Funs.DB.Sys_MenuFlowOperate.DeleteOnSubmit(flow);
               Funs.DB.SubmitChanges();
           }
       }
       
       /// <summary>
       ///  步骤更新顺序
       /// </summary>
       public static void SetSortIndex(string menuId)
       {
           var menuFlowOperate = from x in Funs.DB.Sys_MenuFlowOperate where x.MenuId == menuId  select x;
           if (menuFlowOperate.Count() > 0)
           {
               var maxSortIndex = menuFlowOperate.Select(x => x.FlowStep).Max();
               if (menuFlowOperate.Count() < maxSortIndex)
               {
                   int sortIndex = 0;
                   foreach (var item in menuFlowOperate)
                   {
                       sortIndex++;
                       item.FlowStep = sortIndex;
                       Funs.DB.SubmitChanges();
                   }
               }
           }
       }

       /// <summary>
       ///  步骤顺序说明
       /// </summary>
       public static string GetFlowOperateName(string menuId)
       {
           string returnValue = string.Empty;
           var menuFlowOperate = from x in Funs.DB.Sys_MenuFlowOperate 
                                 where x.MenuId == menuId 
                                 orderby x.FlowStep
                                 select x;
           if (menuFlowOperate.Count() > 0)
           {
               foreach (var item in menuFlowOperate)
               {
                   returnValue += ("第" + item.FlowStep + "步[" + item.AuditFlowName+"]");
                   if(item.IsFlowEnd == false || !item.IsFlowEnd.HasValue)
                   {
                       returnValue += ("办理角色为{" + BLL.RoleService.getRoleNamesRoleIds(item.RoleId) + "}；");
                   }
               }
           }
           if (string.IsNullOrEmpty(returnValue))
           {
               returnValue = "未定义流程审批规则。";
           }

           return returnValue;
       }
    }
}
