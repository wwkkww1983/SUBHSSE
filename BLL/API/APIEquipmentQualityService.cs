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
        //public static Model.EquipmentQualityItem getEquipmentQualityByEquipmentQualityId(string equipmentQualityId)
        //{
        //    var getEquipmentQuality = from x in Funs.DB.View_QualityAudit_EquipmentQuality
        //                              where x.EquipmentQualityId == equipmentQualityId
        //                              select new
        //                              {
        //                                  x.EquipmentQualityId,
        //                                  x.ProjectId,
        //                                  x.EquipmentQualityCode,
        //                                  x.UnitId,
        //                                  x.UnitName,
        //                                  x.SpecialEquipmentId,
        //                                  x.SpecialEquipmentName,
        //                                  x.EquipmentQualityName,
        //                                  x.SizeModel,
        //                                  x.FactoryCode,
        //                                  x.CertificateCode,
        //                                  CheckDate = string.Format("{0:yyyy-MM-dd}", x.CheckDate),
        //                                  LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
        //                                  InDate = string.Format("{0:yyyy-MM-dd}", x.InDate),
        //                                  OutDate = string.Format("{0:yyyy-MM-dd}", x.OutDate),
        //                                  x.ApprovalPerson,
        //                                  x.CarNum,
        //                                  x.Remark,
        //                                  x.CompileMan,
        //                                  x.CompileManName,
        //                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
        //                                  x.QRCodeAttachUrl,
        //                                  x.AttachUrl
        //                              };
        //    return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_QualityAudit_EquipmentQuality, Model.EquipmentQualityItem>().Map(getEquipmentQuality);
        //}

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
