using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIEquipmentQualityService
    {        
        /// <summary>
        /// 根据equipmentQualityId获取机具设备信息
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        /// <returns></returns>
        public static Model.EquipmentQualityItem getEquipmentQualityByEquipmentQualityId(string equipmentQualityId)
        {
            var getEquipmentQuality = Funs.DB.View_QualityAudit_EquipmentQuality.FirstOrDefault(x => x.EquipmentQualityId == equipmentQualityId);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_QualityAudit_EquipmentQuality, Model.EquipmentQualityItem>().Map(getEquipmentQuality);
        }

        /// <summary>
        /// 根据factoryCode获取机具设备资质信息
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public static Model.EquipmentQualityItem getEquipmentQualityByFactoryCode(string factoryCode)
        {
            var getEquipmentQuality = Funs.DB.View_QualityAudit_EquipmentQuality.FirstOrDefault(x => x.FactoryCode == factoryCode);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_QualityAudit_EquipmentQuality, Model.EquipmentQualityItem>().Map(getEquipmentQuality);
        }

        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.EquipmentQualityItem> getEquipmentQualityByProjectIdUnitId(string projectId, string unitId)
        {
            var getList = (from x in Funs.DB.View_QualityAudit_EquipmentQuality
                           where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                           orderby x.UnitName, x.EquipmentQualityCode
                           select x);
            return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.View_QualityAudit_EquipmentQuality>, List<Model.EquipmentQualityItem>>().Map(getList.ToList());
        }
    }
}
