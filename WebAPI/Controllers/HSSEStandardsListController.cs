using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class HSSEStandardsListController : ApiController
    {
        #region 根据standardId获取标准规范信息
        /// <summary>
        /// 根据standardId获取标准规范信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardsListByStandardId(string standardId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = from x in Funs.DB.Law_HSSEStandardsList
                                   where x.StandardId == standardId
                                   select new
                                   {
                                       x.StandardId,
                                       x.StandardGrade,
                                       x.StandardNo,
                                       x.StandardName,
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

        #region 根据lawsRegulationsTypeId获取标准规范
        /// <summary>
        /// 根据lawsRegulationsTypeId获取标准规范
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Model.ResponeData getHSSEStandardsListByTypeId(string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                IQueryable<Model.Law_HSSEStandardsList> q = from x in Funs.DB.Law_HSSEStandardsList
                                                            where x.TypeId == type && x.IsPass == true
                                                            select x;
                int pageCount = q.Count();
                if (pageCount == 0)
                {
                    responeData.data = new { pageCount, q };
                }
                else
                {
                    var getDataList = from x in q.OrderBy(u => u.StandardNo).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                      select new
                                      {
                                          x.StandardId,
                                          x.StandardGrade,
                                          x.StandardNo,
                                          x.StandardName,
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
