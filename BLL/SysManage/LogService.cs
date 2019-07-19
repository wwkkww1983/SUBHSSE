namespace BLL
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net;

    public static class LogService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="userId">操作人ID</param>
        /// <param name="opLog">操作内容</param>
        /// <param name="code">编号</param>
        /// <param name="dataId">主键ID</param>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="strOperationName">操作名称</param>
        public static void AddSys_Log(Model.Sys_User CurrUser, string code, string dataId, string strMenuId, string strOperationName)
        {
            if (CurrUser != null)
            {
                Model.SUBHSSEDB db = Funs.DB;
                Model.Sys_Log syslog = new Model.Sys_Log
                {
                    LogId = SQLHelper.GetNewID(typeof(Model.Sys_Log)),
                    HostName = Dns.GetHostName(),
                    OperationTime = DateTime.Now,
                    UserId = CurrUser.UserId,
                    MenuId = strMenuId,
                    OperationName = strOperationName,
                    UpState = Const.UpState_2,
                    LogSource = 1,
                };

                IPAddress[] ips = Dns.GetHostAddresses(syslog.HostName);
                if (ips.Length > 0)
                {
                    foreach (IPAddress ip in ips)
                    {
                        if (ip.ToString().IndexOf('.') != -1)
                        {
                            syslog.Ip = ip.ToString();
                        }
                    }
                }
                string opLog = string.Empty;
                var menu = BLL.SysMenuService.GetSysMenuByMenuId(strMenuId);
                if (menu != null)
                {
                    opLog = menu.MenuName + ":";
                }

                if (!string.IsNullOrEmpty(strOperationName))
                {
                    opLog += strOperationName;
                }

                if (!string.IsNullOrEmpty(code))
                {
                    syslog.OperationLog = opLog + "；" + code + "。";
                }
                else
                {
                    var returnCode = BLL.CodeRecordsService.ReturnCodeByDataId(dataId);
                    if (!string.IsNullOrEmpty(returnCode))
                    {
                        syslog.OperationLog = opLog + "；" + returnCode + "。";
                    }
                    else
                    {
                        syslog.OperationLog = opLog;
                    }
                }

                var project = BLL.ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId);
                if (project != null)
                {
                    syslog.ProjectId = project.ProjectId;
                }

                db.Sys_Log.InsertOnSubmit(syslog);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目Id删除所有相关日志信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteLog(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Sys_Log where x.ProjectId == projectId select x).ToList();
            if (q != null)
            {
                db.Sys_Log.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }        
    }
}