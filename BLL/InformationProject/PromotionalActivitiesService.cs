using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// HSSE宣传活动
    /// </summary>
    public static class PromotionalActivitiesService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取HSSE宣传活动
        /// </summary>
        /// <param name="promotionalActivitiesId"></param>
        /// <returns></returns>
        public static Model.InformationProject_PromotionalActivities GetPromotionalActivitiesById(string promotionalActivitiesId)
        {
            return Funs.DB.InformationProject_PromotionalActivities.FirstOrDefault(e => e.PromotionalActivitiesId == promotionalActivitiesId);
        }

        /// <summary>
        /// 根据时间段获取HSE宣传活动
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目Id</param>
        /// <returns>时间段内的HSE宣传活动数量</returns>
        public static int GetCountByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.InformationProject_PromotionalActivities where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId select x).Count();
        }

        /// <summary>
        /// 根据日期获取HSE宣传活动集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>HSE宣传活动集合</returns>
        public static List<Model.InformationProject_PromotionalActivities> GetPromotionalActivitiesListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.InformationProject_PromotionalActivities where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId orderby x.CompileDate select x).ToList();
        }

        /// <summary>
        /// 添加HSSE宣传活动
        /// </summary>
        /// <param name="promotionalActivities"></param>
        public static void AddPromotionalActivities(Model.InformationProject_PromotionalActivities promotionalActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_PromotionalActivities newPromotionalActivities = new Model.InformationProject_PromotionalActivities
            {
                PromotionalActivitiesId = promotionalActivities.PromotionalActivitiesId,
                ProjectId = promotionalActivities.ProjectId,
                Code = promotionalActivities.Code,
                Title = promotionalActivities.Title,
                ActivitiesDate = promotionalActivities.ActivitiesDate,
                UnitIds = promotionalActivities.UnitIds,
                UnitNames = promotionalActivities.UnitNames,
                UserIds = promotionalActivities.UserIds,
                UserNames = promotionalActivities.UserNames,
                MainContent = promotionalActivities.MainContent,
                AttachUrl = promotionalActivities.AttachUrl,
                CompileMan = promotionalActivities.CompileMan,
                CompileDate = System.DateTime.Now,
                States = promotionalActivities.States
            };
            db.InformationProject_PromotionalActivities.InsertOnSubmit(newPromotionalActivities);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectPromotionalActivitiesMenuId, promotionalActivities.ProjectId, null, promotionalActivities.PromotionalActivitiesId, promotionalActivities.ActivitiesDate);
        }

        /// <summary>
        /// 修改HSSE宣传活动
        /// </summary>
        /// <param name="PromotionalActivities"></param>
        public static void UpdatePromotionalActivities(Model.InformationProject_PromotionalActivities promotionalActivities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_PromotionalActivities newPromotionalActivities = db.InformationProject_PromotionalActivities.FirstOrDefault(e => e.PromotionalActivitiesId == promotionalActivities.PromotionalActivitiesId);
            if (newPromotionalActivities != null)
            {
                newPromotionalActivities.Code = promotionalActivities.Code;
                newPromotionalActivities.Title = promotionalActivities.Title;
                newPromotionalActivities.ActivitiesDate = promotionalActivities.ActivitiesDate;
                newPromotionalActivities.UnitIds = promotionalActivities.UnitIds;
                newPromotionalActivities.UnitNames = promotionalActivities.UnitNames;
                newPromotionalActivities.UserIds = promotionalActivities.UserIds;
                newPromotionalActivities.UserNames = promotionalActivities.UserNames;
                newPromotionalActivities.MainContent = promotionalActivities.MainContent;
                newPromotionalActivities.AttachUrl = promotionalActivities.AttachUrl;
                newPromotionalActivities.CompileMan = promotionalActivities.CompileMan;
                newPromotionalActivities.CompileDate = promotionalActivities.CompileDate;
                newPromotionalActivities.States = promotionalActivities.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HSSE宣传活动
        /// </summary>
        /// <param name="promotionalActivitiesId"></param>
        public static void DeletePromotionalActivitiesById(string promotionalActivitiesId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InformationProject_PromotionalActivities promotionalActivities = db.InformationProject_PromotionalActivities.FirstOrDefault(e => e.PromotionalActivitiesId == promotionalActivitiesId);
            if (promotionalActivities != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(promotionalActivities.PromotionalActivitiesId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(promotionalActivities.PromotionalActivitiesId);
                ///删除工程师日志收集记录
                var flowOperate = from x in db.Sys_FlowOperate where x.DataId == promotionalActivities.PromotionalActivitiesId select x;
                if (flowOperate.Count() > 0)
                {
                    foreach (var item in flowOperate)
                    {
                        BLL.HSSELogService.CollectHSSELog(promotionalActivities.ProjectId, item.OperaterId, item.OperaterTime, "29", promotionalActivities.Title, Const.BtnDelete, 1);
                    }
                    ////删除流程表
                    BLL.CommonService.DeleteFlowOperateByID(promotionalActivities.PromotionalActivitiesId);
                } 
                db.InformationProject_PromotionalActivities.DeleteOnSubmit(promotionalActivities);
                db.SubmitChanges();
            }
        }
    }
}
