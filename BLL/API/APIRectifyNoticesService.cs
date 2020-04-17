using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIRectifyNoticesService
    {
        #region 根据RectifyNoticesId获取风险巡检信息详细信息
        /// <summary>
        /// 根据RectifyNoticesId获取风险巡检信息详细信息
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public static Model.RectifyNoticesItem getRectifyNoticesById(string rectifyNoticesId)
        {
            var getRectifyNotices = from x in Funs.DB.Check_RectifyNotices
                                    where x.RectifyNoticesId == rectifyNoticesId                              
                                    select new Model.RectifyNoticesItem
                                    {
                                        RectifyNoticesId=x.RectifyNoticesId,
                                        ProjectId=x.ProjectId,
                                        ProjectName=Funs.DB.Base_Project.First(z=>z.ProjectId ==x.ProjectId).ProjectName,
                                        RectifyNoticesCode =x.RectifyNoticesCode,
                                        UnitId=x.UnitId,
                                        UnitName=Funs.DB.Base_Unit.First(u=>u.UnitId == x.UnitId).UnitName,
                                        WorkAreaId=x.WorkAreaId,
                                        WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                        CheckManNames=x.CheckManNames,
                                        CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CheckedDate),
                                        CheckedDateD=x.CheckedDate,
                                        HiddenHazardType=x.HiddenHazardType,
                                        HiddenHazardTypeName= x.HiddenHazardType=="2"?"较大":(x.HiddenHazardType == "3" ? "重大" :"一般"),
                                        CompleteManId=x.CompleteManId,
                                        CompleteManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompleteManId).UserName,
                                        SignPersonId = x.SignPerson,
                                        SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                        SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                                        ProfessionalEngineerId =x.ProfessionalEngineerId,
                                        ProfessionalEngineerName = Funs.DB.Sys_User.First(u => u.UserId == x.ProfessionalEngineerId).UserName,
                                        ProfessionalEngineerTime1= string.Format("{0:yyyy-MM-dd HH:mm}", x.ProfessionalEngineerTime1),
                                        ProfessionalEngineerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProfessionalEngineerTime2),
                                        ConstructionManagerId = x.ConstructionManagerId,
                                        ConstructionManagerName = Funs.DB.Sys_User.First(u => u.UserId == x.ConstructionManagerId).UserName,
                                        ConstructionManagerTime1 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ConstructionManagerTime1),
                                        ConstructionManagerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ConstructionManagerTime2),
                                        ProjectManagerId = x.ProjectManagerId,
                                        ProjectManagerName = Funs.DB.Sys_User.First(u => u.UserId == x.ProjectManagerId).UserName,
                                        ProjectManagerTime1 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProjectManagerTime1),
                                        ProjectManagerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProjectManagerTime2),
                                        DutyPersonId = x.DutyPersonId,
                                        DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                        DutyPersonTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.DutyPersonTime),                                        
                                        CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                        UnitHeadManId = x.UnitHeadManId,
                                        UnitHeadManName = Funs.DB.Sys_User.First(u => u.UserId == x.UnitHeadManId).UserName,
                                        UnitHeadManDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.UnitHeadManDate),
                                        CheckPersonId = x.CheckPerson,
                                        CheckPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                        ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                        //WrongContent =x.WrongContent,                                       
                                        //CompleteStatus=x.CompleteStatus,                                       
                                        //RectificationName=x.DutyPerson,                                       
                                        IsRectify=x.IsRectify ?? false,  
                                        States=x.States,
                                        AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId)).AttachUrl.Replace('\\', '/'),
                                        //BeAttachUrl = Funs.DB.AttachFile.First(z=>z.ToKeyId== (x.RectifyNoticesId+"#0")).AttachUrl.Replace('\\', '/'),
                                        //AfAttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId + "#1")).AttachUrl.Replace('\\', '/'),
                                        RectifyNoticesItemItem= getRectifyNoticesItemItem(x.RectifyNoticesId),

                                    };

            return getRectifyNotices.FirstOrDefault();
        }
        #endregion

        #region 根据隐患整改单ID 获取整改单明细信息
        /// <summary>
        ///  根据隐患整改单ID 获取整改单明细信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.RectifyNoticesItemItem> getRectifyNoticesItemItem(string rectifyNoticesId)
        {
            var getInfo = from x in Funs.DB.Check_RectifyNoticesItem
                          where x.RectifyNoticesId == rectifyNoticesId
                          orderby x.LimitTime descending
                          select new Model.RectifyNoticesItemItem
                          {
                              RectifyNoticesItemId = x.RectifyNoticesItemId,
                              RectifyNoticesId = x.RectifyNoticesId,
                              WrongContent = x.WrongContent,
                              Requirement = x.Requirement,
                              LimitTime = string.Format("{0:yyyy-MM-dd}", x.LimitTime),
                              RectifyResults=x.RectifyResults,
                              IsRectify = x.IsRectify ?? false,
                              PhotoBeforeUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesItemId+"#1", null),
                              PhotoAfterUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesItemId + "#2", null),
                          };
            return getInfo.ToList();
        }
        #endregion

        #region 根据隐患整改单ID 获取整改单审核信息
        /// <summary>
        ///  根据隐患整改单ID 获取整改单明细信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.RectifyNoticesFlowOperateItem> getRectifyNoticesFlowOperateItem(string rectifyNoticesId)
        {
            var getInfo = from x in Funs.DB.Check_RectifyNoticesFlowOperate
                          where x.RectifyNoticesId == rectifyNoticesId
                          orderby x.OperateTime descending
                          select new Model.RectifyNoticesFlowOperateItem
                          {
                              FlowOperateId = x.FlowOperateId,
                              RectifyNoticesId = x.RectifyNoticesId,
                              OperateName = x.OperateName,
                              OperateManId = x.OperateManId,
                              OperateManName=Funs.DB.Sys_User.First(z=>z.UserId == x.OperateManId).UserName,
                              OperateTime = string.Format("{0:yyyy-MM-dd}", x.OperateTime),
                              IsAgree = x.IsAgree,
                              Opinion=x.Opinion,
                              SignatureUrl = APIUpLoadFileService.getFileUrl(string.Empty, x.SignatureUrl),
                          };
            return getInfo.ToList();
        }
        #endregion

        #region 根据projectId、states获取风险信息（状态 0：待提交；1：待签发；2：待整改；3：待复查；4：已完成）
        /// <summary>
        /// 根据projectId、states获取风险信息（状态 0：待提交；1：待签发；2：待整改；3：待复查；4：已完成）
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.RectifyNoticesItem> getRectifyNoticesByProjectIdStates(string projectId, string states)
        {
            var getDataLists = (from x in Funs.DB.Check_RectifyNotices
                                where x.ProjectId == projectId && x.States == states
                                orderby x.CheckedDate descending
                                select new Model.RectifyNoticesItem
                                {
                                    RectifyNoticesId = x.RectifyNoticesId,
                                    ProjectId = x.ProjectId,
                                    ProjectName = Funs.DB.Base_Project.First(z => z.ProjectId == x.ProjectId).ProjectName,
                                    RectifyNoticesCode = x.RectifyNoticesCode,
                                    UnitId = x.UnitId,
                                    UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                    WorkAreaId = x.WorkAreaId,
                                    WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                    CheckManNames = x.CheckManNames,
                                    CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CheckedDate),
                                    CheckedDateD = x.CheckedDate,
                                    HiddenHazardType = x.HiddenHazardType,
                                    HiddenHazardTypeName = x.HiddenHazardType == "2" ? "较大" : (x.HiddenHazardType == "3" ? "重大" : "一般"),
                                    CompleteManId = x.CompleteManId,
                                    CompleteManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompleteManId).UserName,
                                    SignPersonId = x.SignPerson,
                                    SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                    SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),

                                    DutyPersonId = x.DutyPersonId,
                                    DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                    DutyPersonTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.DutyPersonTime),

                                    CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                    UnitHeadManId = x.UnitHeadManId,
                                    UnitHeadManName = Funs.DB.Sys_User.First(u => u.UserId == x.UnitHeadManId).UserName,
                                    UnitHeadManDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.UnitHeadManDate),

                                    CheckPersonId = x.CheckPerson,
                                    CheckPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                    ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                    ReCheckOpinion = x.ReCheckOpinion,
                                    IsRectify = x.IsRectify ?? false,
                                    States = x.States,
                                    AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId)).AttachUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 状态 -old
        /// <summary>
        /// 状态 -old
        /// </summary>
        /// <param name="SignDate"></param>
        /// <param name="CompleteDate"></param>
        /// <param name="ReCheckDate"></param>
        /// <returns></returns>
        public static string getStates(DateTime? SignDate, DateTime? CompleteDate, DateTime? ReCheckDate)
        {
            string states = string.Empty;
            if (!SignDate.HasValue)  // 待签发 0
            {
                states = "0";
            }
            else if (SignDate.HasValue && !CompleteDate.HasValue)   // 待整改 1
            {
                states = "1";
            }
            else if (CompleteDate.HasValue && !ReCheckDate.HasValue) // 待复查 2
            {
                states = "2";
            }
            else if (ReCheckDate.HasValue)  // 已闭环 3
            {
                states = "3";
            }
            return states;
        }
        #endregion

        #region 保存RectifyNotices
        /// <summary>
        /// 保存RectifyNotices
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static void SaveRectifyNotices(Model.RectifyNoticesItem rectifyNotices)
        {
            Model.Check_RectifyNotices newRectifyNotices = new Model.Check_RectifyNotices
            {
                RectifyNoticesId = rectifyNotices.RectifyNoticesId,
                ProjectId = rectifyNotices.ProjectId,
                RectifyNoticesCode = rectifyNotices.RectifyNoticesCode,
                UnitId = rectifyNotices.UnitId,
                WorkAreaId = rectifyNotices.WorkAreaId,
                CheckManNames= rectifyNotices.CheckManNames,
                CheckedDate = Funs.GetNewDateTime(rectifyNotices.CheckedDate),
                HiddenHazardType = rectifyNotices.HiddenHazardType,

                SignPerson = rectifyNotices.SignPersonId,
                DutyPersonId = rectifyNotices.DutyPersonId,
                DutyPerson = rectifyNotices.RectificationName,               
                CheckPerson = rectifyNotices.CheckPersonId,                
            };
            
            var isUpdate = Funs.DB.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == newRectifyNotices.RectifyNoticesId);
            if (isUpdate == null)
            {
                newRectifyNotices.RectifyNoticesId = SQLHelper.GetNewID();
                newRectifyNotices.RectifyNoticesCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.ProjectId, newRectifyNotices.UnitId);
                if (rectifyNotices.States == "1")
                {
                    newRectifyNotices.SignDate = Funs.GetNewDateTime(rectifyNotices.SignDate);
                    newRectifyNotices.DutyPersonId = rectifyNotices.DutyPersonId;
                }               
                if (!string.IsNullOrEmpty(rectifyNotices.BeAttachUrl))
                {
                    APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#0", rectifyNotices.BeAttachUrl, "0");
                }
                RectifyNoticesService.AddRectifyNotices(newRectifyNotices);
                //// 回写巡检记录表
                if (!string.IsNullOrEmpty(rectifyNotices.HazardRegisterId))
                {
                    List<string> listIds = Funs.GetStrListByStr(rectifyNotices.HazardRegisterId, ',');
                    foreach (var item in listIds)
                    {
                        var getHazardRegister = Funs.DB.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == item);
                        if (getHazardRegister != null)
                        {
                            getHazardRegister.States = "3";
                            getHazardRegister.HandleIdea += "已升级为隐患整改单：" + newRectifyNotices.RectifyNoticesCode;
                            getHazardRegister.ResultId = newRectifyNotices.RectifyNoticesId;
                            getHazardRegister.ResultType = "1";
                            Funs.SubmitChanges();
                        }
                    }
                }
            }
            else
            {
                //// 签发
                if (rectifyNotices.States == "1")
                {
                    isUpdate.UnitId = rectifyNotices.UnitId;
                    isUpdate.WorkAreaId = rectifyNotices.WorkAreaId;
                    isUpdate.CheckedDate = Funs.GetNewDateTime(rectifyNotices.CheckedDate);
                    isUpdate.WrongContent = rectifyNotices.WrongContent;
                    isUpdate.DutyPersonId = rectifyNotices.DutyPersonId;                
                    isUpdate.SignDate = Funs.GetNewDateTime(rectifyNotices.SignDate);
                    if (!string.IsNullOrEmpty(rectifyNotices.BeAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#0", rectifyNotices.BeAttachUrl, "0");
                    }
                }
                if (rectifyNotices.States == "2")  //// 整改
                {
                    isUpdate.CompleteStatus = rectifyNotices.CompleteStatus;
                    isUpdate.CompleteDate = Funs.GetNewDateTime(rectifyNotices.CompleteDate);               
                    isUpdate.DutyPerson = rectifyNotices.RectificationName;
                    isUpdate.CheckPerson = rectifyNotices.CheckPersonId;
                    if (!string.IsNullOrEmpty(rectifyNotices.AfAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#1", rectifyNotices.AfAttachUrl, "0");
                    }
                }
                else if (rectifyNotices.States == "3") //// 复查
                {
                    isUpdate.IsRectify = rectifyNotices.IsRectify;
                    if (isUpdate.IsRectify == true)
                    {
                        isUpdate.ReCheckDate = Funs.GetNewDateTime(rectifyNotices.ReCheckDate);                      
                    }
                    else
                    {
                        isUpdate.CompleteDate = null;
                        isUpdate.ReCheckDate = null;
                    }
                    if (!string.IsNullOrEmpty(rectifyNotices.AttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId, rectifyNotices.AttachUrl, "0");
                    }
                }
            }
            Funs.SubmitChanges();
        }
        #endregion
    }
}
