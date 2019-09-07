using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
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
                responeData.data = Funs.DB.View_SitePerson_Person.FirstOrDefault(x => x.PersonId == personId);
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
                responeData.data = Funs.DB.View_SitePerson_Person.FirstOrDefault(x => x.IdentityCard == identityCard);
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
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getPersonByProjectIdUnitId(string projectId, string unitId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = (from x in Funs.DB.View_SitePerson_Person
                                   where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                   orderby x.UnitCode, x.PersonName
                                   select x).ToList();
                int pageCount = getDataList.Count;
                if (pageCount > 0)
                {
                    getDataList = getDataList.OrderBy(u => u.UnitCode).ThenBy(u=>u.PersonName).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
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
                                  where x.IdentityCard == identityCard
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
        /// <param name="projectId"></param>
        /// <returns></returns>
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
                ///总数
                int tatalCount = getDataList.Count();
                ///过期
                int count1 = getDataList.Where(x => x.LimitDate < DateTime.Now).Count();
                ///即将过期
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
                if (pageCount > 0)
                {
                    var getdata = from x in getQualityLists.OrderBy(u => u.LimitDate).Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize)
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
    }
}