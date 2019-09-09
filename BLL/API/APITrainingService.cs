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
    /// 培训教材
    /// </summary>
    public static class APITrainingService
    {
        /// <summary>
        /// 根据父级类型ID获取培训教材类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in Funs.DB.Training_Training
                               where x.SupTrainingId == supTypeId || (supTypeId == null && x.SupTrainingId == "0")
                               orderby x.TrainingCode
                               select new Model.ResourcesItem
                               {
                                   ResourcesId = x.TrainingId,
                                   ResourcesCode = x.TrainingCode,
                                   ResourcesName = x.TrainingName,
                                   SupResourcesId = x.SupTrainingId,
                                   IsEndLever = x.IsEndLever,
                               };
            return getDataLists.ToList();
        }

        /// <summary>
        /// 根据培训教材类型id获取培训教材列表
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTrainingItemListByTrainingId(string trainingId)
        {
            var getDataLists = (from x in Funs.DB.Training_TrainingItem
                                where x.TrainingId == trainingId && x.IsPass == true
                                orderby x.TrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.TrainingItemId,
                                    BaseInfoCode = x.TrainingItemCode,
                                    BaseInfoName = x.TrainingItemName,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取培训教材详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.BaseInfoItem getTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in Funs.DB.Training_TrainingItem
                              where x.TrainingItemId == trainingItemId
                              select new Model.BaseInfoItem
                              {
                                  BaseInfoId = x.TrainingItemId,
                                  BaseInfoCode = x.TrainingItemCode,
                                  BaseInfoName = x.TrainingItemName,
                                  ImageUrl = x.AttachUrl.Replace('\\', '/'),
                              };
            return getDataInfo.FirstOrDefault();
        }
    }
}
