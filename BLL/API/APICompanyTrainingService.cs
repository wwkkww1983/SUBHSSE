using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    /// <summary>
    /// 公司培训教材
    /// </summary>
    public static class APICompanyTrainingService
    {
        /// <summary>
        /// 根据父级类型ID获取公司培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getCompanyTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in Funs.DB.Training_CompanyTraining
                               where x.SupCompanyTrainingId == supTypeId || (supTypeId == null && x.SupCompanyTrainingId == "0")
                               orderby x.CompanyTrainingCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.CompanyTrainingId,
                                   ResourcesCode = x.CompanyTrainingCode,
                                   ResourcesName = x.CompanyTrainingName,
                                   SupResourcesId = x.SupCompanyTrainingId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据培训教材类型id获取公司培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getCompanyTrainingItemListByTrainingId(string trainingId)
        {
            var getDataLists = (from x in Funs.DB.Training_CompanyTrainingItem
                                where x.CompanyTrainingId == trainingId 
                                orderby x.CompanyTrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.CompanyTrainingItemId,
                                    BaseInfoCode = x.CompanyTrainingItemCode,
                                    BaseInfoName = x.CompanyTrainingItemName,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取公司培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getCompanyTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in Funs.DB.Training_CompanyTrainingItem
                              where x.CompanyTrainingItemId == trainingItemId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.CompanyTrainingItemId,
                                  BaseInfoCode = x.CompanyTrainingItemCode,
                                  BaseInfoName = x.CompanyTrainingItemName,
                                  ImageUrl = x.AttachUrl.Replace('\\', '/'),
                              };
            return getDataInfo.FirstOrDefault();
        }
    }
}
