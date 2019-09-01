using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    public class EquipmentQualityController : ApiController
    {
        #region 根据equipmentQualityId获取机具设备资质信息
        /// <summary>
        /// 根据equipmentQualityId获取机具设备资质信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByEquipmentQualityId(string equipmentQualityId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEquipmentQualityService.getEquipmentQualityByEquipmentQualityId(equipmentQualityId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据factoryCode获取机具设备资质信息
        /// <summary>
        /// 根据factoryCode获取机具设备资质信息
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByFactoryCode(string factoryCode)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APIEquipmentQualityService.getEquipmentQualityByFactoryCode(factoryCode);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId、unitid获取机具设备资质信息
        /// <summary>
        /// 根据projectId、unitid获取机具设备资质信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityByProjectIdUnitId(string projectId, string unitId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APIEquipmentQualityService.getEquipmentQualityByProjectIdUnitId(projectId, unitId);
                int pageCount = getDataList.Count;
                if (pageCount > 0)
                {
                    getDataList = getDataList.OrderBy(u => u.EquipmentQualityCode).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
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

        #region 根据projectId、unitid获取特岗机具设备资质资质各状态数
        /// <summary>
        /// 根据projectId、unitid获取特岗机具设备资质资质各状态数
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityQualityCount(string projectId, string unitId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = Funs.DB.QualityAudit_EquipmentQuality.Where(x => x.ProjectId == projectId);
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

        #region 根据projectId、unitid获取特岗机具设备资质资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗机具设备资质资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="type">数据类型0-已过期；1-即将过期</param>
        /// <returns></returns>
        public Model.ResponeData getEquipmentQualityQualityByProjectIdUnitId(string projectId, string unitId, string type, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getQualityLists = APIEquipmentQualityService.getEquipmentQualityByProjectIdUnitId(projectId, unitId); 
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
                    getQualityLists = getQualityLists.OrderBy(u => u.LimitDate).Skip(BLL.Funs.PageSize * (pageIndex - 1)).Take(BLL.Funs.PageSize).ToList();
                }
                responeData.data = new { pageCount, getQualityLists };
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
