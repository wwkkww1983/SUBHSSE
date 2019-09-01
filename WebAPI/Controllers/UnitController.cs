using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class UnitController : ApiController
    {
        #region 根据UnitId获取单位信息
        /// <summary>
        /// 根据UnitId获取单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Model.ResponeData getUnitByUnitId(string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitByUnitId(unitId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取所有单位
        /// <summary>
        /// 获取所有单位
        /// </summary>
        /// <returns></returns>
        public Model.ResponeData getUnitLists()
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitLists();
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ResponeData getUnitByProjectIdUnitType(string projectId, string unitType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIUnitService.getUnitByProjectIdUnitType(projectId, unitType);
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
