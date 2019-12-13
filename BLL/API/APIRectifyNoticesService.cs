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
        /// <summary>
        /// 根据RectifyNoticesId获取风险巡检信息
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
                                        RectifyNoticesCode=x.RectifyNoticesCode,
                                        UnitId=x.UnitId,
                                        UnitName=Funs.DB.Base_Unit.First(u=>u.UnitId == x.UnitId).UnitName,
                                        WorkAreaId=x.WorkAreaId,
                                        WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                        CheckPersonId=x.CheckPerson,
                                        CheckPersonName= Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                        CheckedDate=string.Format("{0:yyyy-MM-dd HH:mm}",x.CheckedDate),
                                        WrongContent=x.WrongContent,
                                        SignPersonId = x.SignPerson,
                                        SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                        SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                                        CompleteStatus=x.CompleteStatus,
                                        DutyPersonId = x.DutyPersonId,
                                        DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                        RectificationName=x.DutyPerson,
                                        CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                        IsRectify=x.IsRectify ?? false,
                                        ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                        AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId)).AttachUrl.Replace('\\', '/'),
                                        BeAttachUrl = Funs.DB.AttachFile.First(z=>z.ToKeyId== (x.RectifyNoticesId+"#0")).AttachUrl.Replace('\\', '/'),
                                        AfAttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId + "#1")).AttachUrl.Replace('\\', '/'),
                                    };
            return getRectifyNotices.FirstOrDefault();
        }

        /// <summary>
        /// 根据projectId、states获取风险信息（状态 0：待签发；1：待整改；2：已整改，待复查；3：已确认，即已闭环；）
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.RectifyNoticesItem> getRectifyNoticesByProjectIdStates(string projectId, string states)
        {
            var getRectifyNotices = from x in Funs.DB.Check_RectifyNotices
                                    where x.ProjectId == projectId
                                    select x;
            if (states == "0")  // 待签发 0
            {
                getRectifyNotices = getRectifyNotices.Where(x => !x.SignDate.HasValue);
            }
            else if (states == "1")   // 待整改 1
            {
                getRectifyNotices = getRectifyNotices.Where(x => x.SignDate.HasValue && !x.CompleteDate.HasValue);
            }
            else if (states == "2") // 待复查 2
            {
                getRectifyNotices = getRectifyNotices.Where(x => x.CompleteDate.HasValue && !x.ReCheckDate.HasValue);
            }
            else if (states == "3")  // 已闭环 3
            {
                getRectifyNotices = getRectifyNotices.Where(x => x.ReCheckDate.HasValue);
            }

            var getDataLists = (from x in getRectifyNotices
                                orderby x.CheckedDate descending
                                select new Model.RectifyNoticesItem
                                {
                                    RectifyNoticesId = x.RectifyNoticesId,
                                    ProjectId = x.ProjectId,
                                    RectifyNoticesCode = x.RectifyNoticesCode,
                                    UnitId = x.UnitId,
                                    UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                    WorkAreaId = x.WorkAreaId,
                                    WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                    CheckPersonId = x.CheckPerson,
                                    CheckPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                    CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CheckedDate),
                                    WrongContent = x.WrongContent,
                                    SignPersonId = x.SignPerson,
                                    SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                    SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                                    CompleteStatus = x.CompleteStatus,
                                    DutyPersonId = x.DutyPersonId,
                                    DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                    RectificationName = x.DutyPerson,
                                    CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                    IsRectify = x.IsRectify ?? false,
                                    ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                    AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId)).AttachUrl.Replace('\\', '/'),
                                    BeAttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId + "#0")).AttachUrl.Replace('\\', '/'),
                                    AfAttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId + "#1")).AttachUrl.Replace('\\', '/'),
                                    States = getStates(x.RectifyNoticesId),
                                }).ToList();
            return getDataLists;
        }

        public static string getStates(string RectifyNoticesId)
        {
            string states = string.Empty;
            var getRectifyNotices = Funs.DB.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == RectifyNoticesId);
            if (getRectifyNotices != null)
            {
                if (!getRectifyNotices.SignDate.HasValue)  // 待签发 0
                {
                    states = "0";
                }
                else if (getRectifyNotices.SignDate.HasValue && !getRectifyNotices.CompleteDate.HasValue)   // 待整改 1
                {
                    states = "1";
                }
                else if (getRectifyNotices.CompleteDate.HasValue && !getRectifyNotices.ReCheckDate.HasValue) // 待复查 2
                {
                    states = "2";
                }
                else if (getRectifyNotices.ReCheckDate.HasValue)  // 已闭环 3
                {
                    states = "3";
                }
            }
            return states;
        }

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
                CheckedDate = Funs.GetNewDateTime(rectifyNotices.CheckedDate),
                WrongContent = rectifyNotices.WrongContent,
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
                if (rectifyNotices.States == "1")
                {
                    isUpdate.DutyPersonId = rectifyNotices.DutyPersonId;
                    isUpdate.CompleteStatus = rectifyNotices.CompleteStatus;
                    isUpdate.SignDate = Funs.GetNewDateTime(rectifyNotices.SignDate);
                    if (!string.IsNullOrEmpty(rectifyNotices.BeAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#0", rectifyNotices.BeAttachUrl, "0");
                    }
                }
                if (rectifyNotices.States == "2")
                {
                    isUpdate.CompleteStatus = rectifyNotices.CompleteStatus;
                    isUpdate.CompleteDate = Funs.GetNewDateTime(rectifyNotices.CompleteDate);
                  //  isUpdate.DutyPersonId = rectifyNotices.DutyPersonId;
                    isUpdate.DutyPerson = rectifyNotices.RectificationName;
                    isUpdate.CheckPerson = rectifyNotices.CheckPersonId;
                    if (!string.IsNullOrEmpty(rectifyNotices.AfAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#1", rectifyNotices.AfAttachUrl, "0");
                    }
                }
                else if (rectifyNotices.States == "3")
                {
                    isUpdate.IsRectify = rectifyNotices.IsRectify;
                    if (isUpdate.IsRectify == true)
                    {
                        isUpdate.CheckPerson = rectifyNotices.CheckPersonId;
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
    }
}
