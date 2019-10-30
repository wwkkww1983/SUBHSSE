﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 作业票控制器
    /// </summary>
    public class LicenseController : ApiController
    {
        #region 获取作业票列表
        /// <summary>
        /// 获取作业票列表
        /// </summary>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="states">状态</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseDataList(string strMenuId, string projectId, string unitId, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();            
            try
            {              
                var getDataList = APILicenseDataService.getLicenseDataList(strMenuId, projectId, unitId, states);
                int pageCount = getDataList.Count;
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = (from x in getDataList.Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize)
                                   select x).ToList();
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

        #region 根据ID获取作业票详细
        /// <summary>
        ///  根据ID获取作业票详细
        /// </summary>
        /// <param name="strMenuId">菜单ID</param>
        /// <param name="id">作业票ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseDataById(string strMenuId, string id)
        {
            var responeData = new Model.ResponeData();
            try
            {                
                Model.LicenseDataItem getInfo = new Model.LicenseDataItem();
                if (!string.IsNullOrEmpty(id))
                {
                    getInfo = APILicenseDataService.getLicenseDataById(strMenuId, id);
                }

                var getLicenseFlowOperate = APILicenseDataService.getLicenseFlowOperate(id);
                var getNextLicenseFlowOperate = APILicenseDataService.getNextLicenseFlowOperate(strMenuId, getInfo);
                responeData.data = new { getInfo, getLicenseFlowOperate, getNextLicenseFlowOperate };
            }
            catch (Exception ex)
            {                
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取作业票安全措施列表信息
        /// <summary>
        ///  获取作业票安全措施列表信息
        /// </summary>
        /// <param name="dataId">作业票ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseLicenseItemList(string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.getLicenseLicenseItemList(dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 获取作业票审核列表信息
        /// <summary>
        ///  获取作业票审核列表信息
        /// </summary>
        /// <param name="dataId">作业票ID</param>
        /// <returns></returns>
        public Model.ResponeData getLicenseFlowOperateList(string dataId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = APILicenseDataService.getLicenseFlowOperateList(dataId);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存作业票信息
        /// <summary>
        /// 保存作业票信息
        /// </summary>
        /// <param name="licenseDataItem">作业票记录</param>
        [HttpPost]
        public Model.ResponeData SaveLicenseData([FromBody] Model.LicenseDataItem licenseDataItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APILicenseDataService.SaveLicenseData(licenseDataItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }

            return responeData;
        }

        /// <summary>
        /// 保存作业票信息
        /// </summary>
        /// <param name="flowOperateItem">作业票记录</param>
        [HttpPost]
        public Model.ResponeData SaveLicenseFlowOperate([FromBody] Model.FlowOperateItem flowOperateItem)
        {
            var responeData = new Model.ResponeData();
            try
            {
                APILicenseDataService.SaveLicenseFlowOperate(flowOperateItem);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return responeData;
        }
        #endregion
    }
}
