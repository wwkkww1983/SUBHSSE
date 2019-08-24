using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIUnitService
    {
        /// <summary>
        /// 根据UnitId获取单位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static Model.UnitItem getUnitByUnitId(string unitId)
        {
            var getUnit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.Base_Unit, Model.UnitItem>().Map(getUnit);
        }

        /// <summary>
        /// 根据projectId、unitType获取单位信息（总包1;施工分包2;监理3;业主4;其他5）
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.UnitItem> getUnitByProjectIdUnitType(string projectId, string unitType)
        {
            var units= (from x in Funs.DB.Base_Unit
                        join y in Funs.DB.Project_ProjectUnit
                        on x.UnitId equals y.UnitId
                        where y.ProjectId == projectId && (y.UnitType == unitType || unitType == null) && (x.IsHide == null || x.IsHide == false) 
                        orderby x.UnitCode
                        select x).ToList();
            return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.Base_Unit>, List<Model.UnitItem>>().Map(units.ToList());
        }
    }
}
