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
    /// 
    /// </summary>
    public class PersonController : ApiController
    {
        #region 根据personid获取人员信息
        /// <summary>
        /// 根据personid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByPersonId(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonByPersonId(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据identityCard获取人员信息
        /// <summary>
        /// 根据identityCard获取人员信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByIdentityCard(string identityCard)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIPersonService.getPersonByPersonId(identityCard);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取人员信息
        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByProjectIdUnitId(string projectId, string unitId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonByProjectIdUnitId(projectId, unitId);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取在岗、离岗、待审人员数量
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonStatesCount(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getViews = from x in Funs.DB.SitePerson_Person
                               where x.ProjectId == projectId 
                               select x;
                if (!CommonService.GetIsThisUnit(unitId) || string.IsNullOrEmpty(unitId))
                {
                    getViews = getViews.Where(x => x.UnitId == unitId);
                }
                int tatalCount = getViews.Count();
                //在审
                int count0 = getViews.Where(x => x.IsUsed == false).Count();
                //在岗
                int count1 = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)).Count();
                //离岗
                int count2 = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now).Count();
                responeData.data = new { tatalCount, count0, count1, count2 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取在岗、离岗、待审人员数量
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states"></param>
        /// <param name="strUnitId"></param>
        /// <param name="strWorkPostId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonStatesCount(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getViews = from x in Funs.DB.SitePerson_Person
                               where x.ProjectId == projectId && (strUnitId == null || x.UnitId == strUnitId)
                                    && (strWorkPostId == null || x.WorkPostId == strWorkPostId)
                               select x;
                if (!CommonService.GetIsThisUnit(unitId) || string.IsNullOrEmpty(unitId))
                {
                    getViews = getViews.Where(x => x.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(strParam))
                {
                    getViews = getViews.Where(x => x.PersonName.Contains(strParam) || x.IdentityCard.Contains(strParam));
                }
                int tatalCount = getViews.Count();
                //在审
                int count0 = getViews.Where(x => x.IsUsed == false).Count();
                //在岗
                int count1 = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)).Count();
                //离岗
                int count2 = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now).Count();
                responeData.data = new { tatalCount, count0, count1, count2 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取在岗、离岗、待审人员列表
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId">当前人单位ID</param>
        /// <param name="states">0待审1在岗2离岗</param>
        /// <param name="strUnitId">查询单位</param>
        /// <param name="strWorkPostId">查询岗位</param>
        ///  <param name="strParam">查询条件</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonListByProjectIdStates(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getPersonListByProjectIdStates(projectId, unitId, states, strUnitId, strWorkPostId, strParam);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 根据identityCard获取人员资质信息
        /// <summary>
        /// 根据identityCard获取人员资质信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityByIdentityCard(string identityCard)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data =from x in Funs.DB.View_QualityAudit_PersonQuality
                                  where x.IdentityCard == identityCard || x.PersonId ==identityCard
                                  select new
                                  {
                                      x.PersonQualityId,
                                      x.PersonId,
                                      x.CardNo,
                                      x.IdentityCard,
                                      x.ProjectId,
                                      x.UnitId,
                                      x.ProjectName,
                                      x.ProjectCode,
                                      x.CertificateNo,
                                      x.Grade,
                                      x.SendUnit,
                                      x.SendDate,
                                      LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                      LateCheckDate = string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                      x.ApprovalPerson,
                                      x.Remark,
                                      x.CompileMan,
                                      x.CompileManName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                      x.CertificateId,
                                      x.CertificateName
                                  };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质各状态数
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质各状态数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <returns>人员资质数量</returns>
        public Model.ResponeData getPersonQualityCount(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {                
                var getDataList = Funs.DB.View_QualityAudit_PersonQuality.Where(x => x.ProjectId == projectId && x.CertificateId != null);
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    getDataList = getDataList.Where(x => x.UnitId == unitId);
                }
                //总数
                int tatalCount = getDataList.Count();
                //过期
                int count1 = getDataList.Where(x => x.LimitDate < DateTime.Now).Count();
                //即将过期
                int count2 = getDataList.Where(x => x.LimitDate >= DateTime.Now && x.LimitDate < DateTime.Now.AddMonths(1)).Count();

                responeData.data = new { tatalCount, count1, count2 };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getPersonQualityByProjectIdUnitId(string projectId, string unitId, string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = (from x in Funs.DB.View_QualityAudit_PersonQuality
                                       where x.ProjectId == projectId && x.CertificateId != null
                                       orderby x.LimitDate
                                       select x).ToList();
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
                {
                    getQualityLists = getQualityLists.Where(x => x.UnitId == unitId).ToList();
                }
                if (type == "0")
                {
                    getQualityLists = getQualityLists.Where(x => x.LimitDate < DateTime.Now).ToList();
                }
                else if (type == "1")
                {
                    getQualityLists = getQualityLists.Where(x => x.LimitDate >= DateTime.Now && x.LimitDate < DateTime.Now.AddMonths(1)).ToList();
                }
                int pageCount = getQualityLists.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    var getdata = from x in getQualityLists.OrderBy(u => u.LimitDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.PersonQualityId,
                                          x.PersonId,
                                          x.CardNo,
                                          x.IdentityCard,
                                          x.ProjectId,
                                          x.UnitId,
                                          x.ProjectName,
                                          x.ProjectCode,
                                          x.CertificateNo,
                                          x.Grade,
                                          x.SendUnit,
                                          x.SendDate,
                                          LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                          LateCheckDate= string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                          x.ApprovalPerson,
                                          x.Remark,
                                          x.CompileMan,
                                          x.CompileManName,
                                          CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                          AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                          x.CertificateId,
                                          x.CertificateName
                                      };

                    responeData.data = new { pageCount, getdata };
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

        #region 根据项目\单位\岗位\培训类型获取项目培训\考试人员信息
        /// <summary>
        /// 根据项目\单位\岗位\培训类型获取项目培训\考试人员信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitIds">培训单位ID</param>
        /// <param name="workPostIds">培训岗位ID(可为空)</param>
        /// <param name="trainTypeId">培训类型ID(可为空)</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        public Model.ResponeData getTrainingPersonListByTrainTypeId(string projectId, string unitIds, string workPostIds, string trainTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIPersonService.getTrainingPersonListByTrainTypeId(projectId, unitIds, workPostIds, trainTypeId).OrderBy(x => x.UnitName).ThenBy(x => x.ProjectName).ToList();
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.OrderBy(u => u.UnitName).ThenBy(u => u.PersonName).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList(); ////200 ->Funs.PageSize
                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 保存项目人员信息
        /// <summary>
        /// 保存项目人员信息
        /// </summary>
        /// <param name="person">人员信息</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveSitePerson([FromBody] Model.PersonItem person)
        {
            var responeData = new Model.ResponeData();
            try
            {
                if (person != null && !string.IsNullOrEmpty(person.IdentityCard))
                {
                    APIPersonService.SaveSitePerson(person);
                }
                else
                {
                    responeData.code = 2;
                    responeData.message = "人员信息有误！";
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

        #region 人员离场
        /// <summary>
        /// 人员离场
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonOut(string personId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getPersonOut(personId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取人员出入场记录
        /// <summary>
        /// 获取人员出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="idCard"></param>
        /// <param name="isIn"></param>
        /// <param name="changeTime"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonInOut(string projectId, string idCard, int isIn,DateTime changeTime)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getPersonInOut(projectId, idCard, isIn, changeTime);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 获取发卡人员
        /// <summary>
        /// 获取发卡人员
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="type">0-门禁；1-人脸</param>
        /// <returns></returns>
        public Model.ResponeData getPersonDataExchange(string projectId, string type)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in Funs.DB.SitePerson_Person
                                   join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                                   where x.ProjectId == projectId
                                   && ((type =="0" && !x.ExchangeTime.HasValue) || (type =="1" && !x.ExchangeTime2.HasValue))
                                   && (!x.OutTime.HasValue || x.OutTime > DateTime.Now) && x.InTime < DateTime.Now
                                   && x.IsUsed == true && x.CardNo != null
                                   select new
                                   {
                                       x.PersonId,
                                       x.PersonName,
                                       x.CardNo,
                                       x.IdentityCard,
                                       x.UnitId,
                                       y.UnitCode,
                                       y.UnitName,
                                       x.Sex,
                                       Funs.DB.Base_WorkPost.First(z => z.WorkPostId == x.WorkPostId).WorkPostName,
                                       x.Telephone,
                                       x.Address,
                                       x.ExchangeTime,
                                       x.ExchangeTime2,
                                       PhotoUrl=  x.PhotoUrl==null? Funs.DB.AttachFile.First(z => z.ToKeyId == (x.PersonId+"#1")).AttachUrl:x.PhotoUrl,
                                   };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }
            return responeData;
        }
        #endregion

        #region 更新人员数据交换时间
        /// <summary>
        /// 更新人员数据交换时间
        /// </summary>
        /// <param name="personId">人员ID</param>
        /// <param name="type">交换类型</param>
        /// <returns></returns>
        public Model.ResponeData getUpdatePersonExchangeTime(string personId, string type)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APIPersonService.getUpdatePersonExchangeTime(personId, type);
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