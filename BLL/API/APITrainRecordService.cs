using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APITrainRecordService
    {
        #region 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// <summary>
        /// 根据projectId、trainTypeId、TrainStates获取培训记录列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.TrainRecordItem> getTrainRecordListByProjectIdTrainTypeIdTrainStates(string projectId, string trainTypeId, string trainStates)
        {
            var getDataLists = (from x in Funs.DB.EduTrain_TrainRecord
                                where x.ProjectId == projectId && (x.TrainStates == trainStates || trainStates == null) && x.TrainTypeId == trainTypeId
                                orderby x.TrainStartDate descending
                                select new Model.TrainRecordItem
                                {
                                    TrainRecordId = x.TrainingId,
                                    TrainingCode = x.TrainingCode,
                                    TrainTitle = x.TrainTitle,
                                    ProjectId = x.ProjectId,
                                    TrainTypeId = x.TrainTypeId,
                                    TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                    TrainLevelId = x.TrainLevelId,
                                    TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                    TeachHour = x.TeachHour ?? 0,
                                    TeachAddress = x.TeachAddress,
                                    TrainStartDate = string.Format("{0:yyyy-MM-dd}", x.TrainStartDate),
                                    TrainStates = x.TrainStates,
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 根据培训ID获取培训记录详细
        /// <summary>
        /// 根据培训ID获取培训记录详细
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public static Model.TrainRecordItem getTrainRecordByTrainingId(string trainRecordId)
        {
            var getDataLists = from x in Funs.DB.EduTrain_TrainRecord
                               where x.TrainingId == trainRecordId
                               select new Model.TrainRecordItem
                               {
                                   TrainRecordId = x.TrainingId,
                                   TrainingCode = x.TrainingCode,
                                   TrainTitle = x.TrainTitle,
                                   ProjectId = x.ProjectId,
                                   TrainTypeId = x.TrainTypeId,
                                   TrainTypeName = Funs.DB.Base_TrainType.First(y => y.TrainTypeId == x.TrainTypeId).TrainTypeName,
                                   TrainLevelId = x.TrainLevelId,
                                   TrainLevelName = Funs.DB.Base_TrainLevel.First(y => y.TrainLevelId == x.TrainLevelId).TrainLevelName,
                                   TeachHour = x.TeachHour ?? 0,
                                   TeachAddress = x.TeachAddress,
                                   TrainStartDate = string.Format("{0:yyyy-MM-dd}", x.TrainStartDate),
                                   TrainStates = x.TrainStates,
                                   UnitIds = x.UnitIds,
                                   WorkPostIds = x.WorkPostIds,
                                   TrainContent = x.TrainContent,
                                   AttachUrl = Funs.DB.AttachFile.First(y => y.ToKeyId == x.TrainingId).AttachUrl.Replace('\\', '/'),
                                   UnitNames = UnitService.getUnitNamesUnitIds(x.UnitIds),
                                   WorkPostNames= WorkPostService.getWorkPostNamesWorkPostIds(x.WorkPostIds),
                               };
            return getDataLists.FirstOrDefault();
        }
        #endregion
    }
}
