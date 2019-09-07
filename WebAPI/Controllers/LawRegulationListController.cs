using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class LawRegulationListController : ApiController
    {
        #region 根据lawRegulationId获取法律法规信息
        /// <summary>
        /// 根据lawRegulationId获取法律法规信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getLawRegulationListByLawRegulationId(string lawRegulationId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in Funs.DB.Law_LawRegulationList
                                   where x.LawRegulationId == lawRegulationId                                   
                                   select new
                                   {
                                       x.LawRegulationId,
                                       x.LawRegulationCode,
                                       x.LawRegulationName,
                                       ApprovalDate=string.Format("{0:yyyy-MM-dd}", x.ApprovalDate) ,
                                       EffectiveDate = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                       x.Description,
                                       AttachUrl = x.AttachUrl.Replace('\\', '/')
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

        #region 根据lawsRegulationsTypeId获取法律法规
        /// <summary>
        /// 根据lawsRegulationsTypeId获取法律法规
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Model.ResponeData getLawRegulationListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_LawRegulationList> q = from x in Funs.DB.Law_LawRegulationList
                                                            where x.LawsRegulationsTypeId == type && x.IsPass == true                                                            
                                                            select x;                
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.LawRegulationCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.LawRegulationId,
                                          x.LawRegulationCode,
                                          x.LawRegulationName,
                                          ApprovalDate = string.Format("{0:yyyy-MM-dd}", x.ApprovalDate),
                                          EffectiveDate = string.Format("{0:yyyy-MM-dd}", x.EffectiveDate),
                                          x.Description,
                                          AttachUrl = x.AttachUrl.Replace('\\', '/')
                                      };
                    responeData.data = new { pageCount, getDataList };
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
