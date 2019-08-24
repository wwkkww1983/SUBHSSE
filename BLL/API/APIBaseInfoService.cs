using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BLL
{
    public static class APIBaseInfoService
    {
        /// <summary>
        /// 根据类型获取巡检隐患类型表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getHazardRegisterTypes(string type)
        {
            var getDataLists = (from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes                                        
                                          where x.HazardRegisterType == type
                                          orderby x.TypeCode
                                          select new Model.BaseInfoItem { BaseInfoId = x.RegisterTypesId, BaseInfoCode = x.TypeCode, BaseInfoName = x.RegisterTypesName }).ToList();
            return getDataLists;
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<List<Model.HSSE_Hazard_HazardRegisterTypes>, List<Model.BaseInfoItem>>(
            //   new DefaultMapConfig().MatchMembers((x, y) =>
            //   {
            //       if (x == "RegisterTypesId" && y == "BaseInfoId")
            //       {
            //           return true;
            //       }
            //       return x == y;
            //   })
            //   );
            //return mapper.Map(getHazardRegisterTypes);

        }

        /// <summary>
        /// 根据项目id获取区域表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getProjectWorkArea(string projectId)
        {
            var getDataLists = (from x in Funs.DB.ProjectData_WorkArea                                          
                                          where x.ProjectId == projectId
                                          orderby x.WorkAreaCode
                                          select new Model.BaseInfoItem { BaseInfoId = x.WorkAreaId, BaseInfoCode = x.WorkAreaCode, BaseInfoName = x.WorkAreaName }).ToList();
            return getDataLists;
        }
    }
}
