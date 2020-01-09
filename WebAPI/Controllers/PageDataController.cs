using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 首页数据
    /// </summary>
    public class PageDataController : ApiController
    {
        #region 根据projectId获取首页数据
        /// <summary>
        /// 根据projectId获取首页数据
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getPageDataByProject(string projectId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getProject = ProjectService.GetProjectByProjectId(projectId);
                if (getProject != null)
                {
                    ////项目开始时间
                    string ProjectData = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    int SafeDayCount = 0, SafeHours = 0, SitePersonNum = 0, SpecialEquipmentNum = 0, EntryTrainingNum = 0, HiddenDangerNum = 0,
                     RectificationNum = 0, RiskI = 0, RiskII = 0, RiskIII = 0, RiskIV = 0, RiskV = 0;
                    if(getProject.StartDate.HasValue)
                    {
                        ProjectData = string.Format("{0:yyyy-MM-dd}", getProject.StartDate);
                        ////安全运行天数
                        SafeDayCount = Convert.ToInt32((DateTime.Now - getProject.StartDate).Value.TotalDays);
                    }
                    
                    //获取输入数据记录
                    var getDataList = Funs.DB.Wx_PageData.FirstOrDefault(x => x.ProjectId == projectId && x.CreatDate.Value.Year == DateTime.Now.Year
                                    && x.CreatDate.Value.Month == DateTime.Now.Month && x.CreatDate.Value.Day == DateTime.Now.Day);
                    if (getDataList != null)
                    {
                        SafeHours = getDataList.SafeHours ?? 0;
                        SitePersonNum = getDataList.SitePersonNum ?? 0;
                        SpecialEquipmentNum = getDataList.SpecialEquipmentNum ?? 0;
                        EntryTrainingNum = getDataList.EntryTrainingNum ?? 0;
                        HiddenDangerNum = getDataList.HiddenDangerNum ?? 0;
                        RectificationNum = getDataList.RectificationNum ?? 0;
                        RiskI = getDataList.RiskI ?? 0;
                        RiskII = getDataList.RiskII ?? 0;
                        RiskIII = getDataList.RiskIII ?? 0;
                        RiskIV = getDataList.RiskIV ?? 0;
                        RiskV = getDataList.RiskV ?? 0;
                    }
                    else
                    {
                        var getHazardItems = from x in Funs.DB.Hazard_HazardSelectedItem
                                             join y in Funs.DB.Hazard_HazardList on x.HazardListId equals y.HazardListId
                                             where y.ProjectId == projectId && y.CompileDate.Value.Year == DateTime.Now.Year 
                                             && y.CompileDate.Value.Month == DateTime.Now.Month 
                                             && y.CompileDate.Value.Day == DateTime.Now.Day
                                             select x;
                        if (getHazardItems.Count() > 0)
                        {
                            RiskI = getHazardItems.Where(x=>x.HazardLevel =="1").Count();
                            RiskII = getHazardItems.Where(x => x.HazardLevel == "2").Count();
                            RiskIII = getHazardItems.Where(x => x.HazardLevel == "3").Count();
                            RiskIV = getHazardItems.Where(x => x.HazardLevel == "4").Count();
                            RiskV = getHazardItems.Where(x => x.HazardLevel == "5").Count();
                        }
                    }

                    SitePersonNum = 0;

                    var getPersons = from x in Funs.DB.SitePerson_Person
                                     where x.ProjectId == projectId && x.IsUsed == true && (!x.OutTime.HasValue || x.OutTime > DateTime.Now)
                                     select x;
                    foreach (var item in getPersons)
                    {
                        var getMax = from x in Funs.DB.SitePerson_PersonInOut
                                     where x.ProjectId == projectId && x.PersonId == item.PersonId
                                     orderby x.ChangeTime descending
                                     select x;
                        if (getMax.Count() > 0)
                        {
                            var maxF = getMax.FirstOrDefault();
                            if (maxF != null && maxF.IsIn == true)
                            {
                                SitePersonNum = SitePersonNum + 1;
                            }
                        }
                    }

                    string hiddenStr = RectificationNum.ToString() + "/" + HiddenDangerNum.ToString();
                    responeData.data = new { ProjectData, SafeDayCount, SafeHours, SitePersonNum, SpecialEquipmentNum, EntryTrainingNum, hiddenStr, RiskI, RiskII, RiskIII, RiskIV, RiskV };
                }
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}
