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
    /// 公共资源
    /// </summary>
    public class ResourcesController : ApiController
    {
        #region 集团培训教材
        #region 根据父级类型ID获取培训教材类型
        /// <summary>
        /// 根据父级类型ID获取培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingListBySupTrainingId(string supTypeId,int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APITrainingService.getTrainingListBySupTrainingId(supTypeId);
                int pageCount = getDataList.Count;
                if (pageCount > 0)
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

        #region 根据培训教材类型id获取培训教材列表
        /// <summary>
        /// 根据培训教材类型id获取培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingItemListByTrainingId(string trainingId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APITrainingService.getTrainingItemListByTrainingId(trainingId);
                int pageCount = getDataList.Count;
                if (pageCount > 0)
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

        #region 根据培训教材主键获取培训教材详细信息
        /// <summary>
        /// 根据培训教材主键获取培训教材详细信息
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getTrainingItemByTrainingItemId(string trainingItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APITrainingService.getTrainingItemByTrainingItemId(trainingItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion

        #region 公司培训教材
        #region 根据父级类型ID获取公司培训教材类型
        /// <summary>
        /// 根据父级类型ID获取公司培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingListBySupTrainingId(string supTypeId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APICompanyTrainingService.getCompanyTrainingListBySupTrainingId(supTypeId);
                int pageCount = getDataList.Count;
                if (pageCount > 0)
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

        #region 根据培训教材类型id获取公司培训教材列表
        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingItemListByTrainingId(string trainingId, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = APICompanyTrainingService.getCompanyTrainingItemListByTrainingId(trainingId);
                int pageCount = getDataList.Count;
                if (pageCount > 0)
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

        #region 根据培训教材主键获取公司培训教材详细信息
        /// <summary>
        /// 根据培训教材主键获取公司培训教材详细信息
        /// </summary>
        /// <param name="trainingItemId"></param>
        /// <returns></returns>
        public Model.ResponeData getCompanyTrainingItemByTrainingItemId(string trainingItemId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APICompanyTrainingService.getCompanyTrainingItemByTrainingItemId(trainingItemId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
        #endregion
    }
}
