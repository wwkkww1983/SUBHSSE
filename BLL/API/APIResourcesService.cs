using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public static class APIResourcesService
    {
        #region 集团培训教材
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
        #endregion

        #region 公司培训教材
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
        #endregion

        #region 考试试题
        /// <summary>
        /// 根据父级类型ID获取考试试题类型
        /// </summary>
        /// <param name="supTypeId"></param>
        /// <returns></returns>
        public static List<Model.ResourcesItem> getTestTrainingListBySupTrainingId(string supTypeId)
        {
            var getDataLists = from x in Funs.DB.Training_TestTraining
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
        /// 根据培训教材类型id获取考试试题列表
        /// </summary>
        /// <param name="testTrainingId">试题类型ID</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTestTrainingItemListByTrainingId(string testTrainingId)
        {
            var getDataLists = (from x in Funs.DB.Training_TestTrainingItem
                                where x.TrainingId == testTrainingId
                                orderby x.TrainingItemCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.TrainingItemId,
                                    BaseInfoCode = x.TrainingItemCode,
                                    BaseInfoName = x.Abstracts,
                                    ImageUrl = x.AttachUrl
                                }).ToList();
            return getDataLists;
        }

        /// <summary>
        /// 根据培训教材主键获取考试试题详细信息
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public static Model.TestTrainingResourcesItem getTestTrainingItemByTrainingItemId(string trainingItemId)
        {
            var getDataInfo = from x in Funs.DB.Training_TestTrainingItem
                              where x.TrainingItemId == trainingItemId
                              select new Model.TestTrainingResourcesItem
                              {
                                  TrainingItemId = x.TrainingItemId,
                                  TrainingId = x.TrainingId,
                                  TrainingItemCode = x.TrainingItemCode,
                                  Abstracts = x.Abstracts,
                                  AttachUrl = x.AttachUrl.Replace('\\', '/'),
                                  TestType = x.TestType,
                                  TestTypeName = x.TestType == "1" ? "单选题" : (x.TestType == "2" ? "多选题" : "判断题"),
                                  WorkPostIds = x.WorkPostIds,
                                  WorkPostNames = WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                                  AItem = x.AItem,
                                  BItem = x.BItem,
                                  CItem = x.CItem,
                                  DItem = x.DItem,
                                  EItem = x.EItem,
                                  Score = x.Score ?? 0,
                                  AnswerItems = x.AnswerItems,

                              };
            return getDataInfo.FirstOrDefault();
        }
        #endregion
    }
}
