using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 应急信息
    /// </summary>
    public static class APIEmergencyService
    {
        #region 根据主键ID获取应急预案信息
        /// <summary>
        ///  根据主键ID获取应急预案信息
        /// </summary>
        /// <param name="emergencyListId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencyListByEmergencyListId(string emergencyListId)
        {
            var getInfo = from x in Funs.DB.Emergency_EmergencyList
                          where x.EmergencyListId == emergencyListId
                          select new Model.FileInfoItem
                          {
                              FileId = x.EmergencyListId,
                              ProjectId = x.ProjectId,
                              FileCode = x.EmergencyCode,
                              FileName = x.EmergencyName,
                              FileType = Funs.DB.Base_EmergencyType.First(y => y.EmergencyTypeId == x.EmergencyTypeId).EmergencyTypeName,
                              UnitId = x.UnitId,
                              UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                              FileContent = x.EmergencyContents,
                              CompileManId = x.CompileMan,
                              CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                              States = x.States,
                              AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.EmergencyListId).AttachUrl.Replace('\\', '/'),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取应急预案列表信息
        /// <summary>
        /// 获取应急预案列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencyList(string projectId, string unitId, string strParam)
        {
            var getDataList = from x in Funs.DB.Emergency_EmergencyList
                                       where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                      && (strParam == null || x.EmergencyName.Contains(strParam) || x.EmergencyCode.Contains(strParam))
                                      orderby x.EmergencyCode descending 
                                      select new Model.FileInfoItem
                                      {
                                          FileId = x.EmergencyListId,
                                          ProjectId = x.ProjectId,
                                          FileCode = x.EmergencyCode,
                                          FileName = x.EmergencyName,
                                          FileType = Funs.DB.Base_EmergencyType.First(y => y.EmergencyTypeId == x.EmergencyTypeId).EmergencyTypeName,
                                          UnitId = x.UnitId,
                                          UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                          FileContent = x.EmergencyContents,
                                          CompileManId = x.CompileMan,
                                          CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                          CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                          States = x.States,
                                          AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.EmergencyListId).AttachUrl.Replace('\\', '/'),
                                      };
            return getDataList.ToList();
        }
        #endregion        

        #region 根据主键ID获取应急物资信息
        /// <summary>
        ///  根据主键ID获取应急物资信息
        /// </summary>
        /// <param name="emergencySupplyId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencySupplyByEmergencySupplyId(string emergencySupplyId)
        {
            var getInfo = from x in Funs.DB.Emergency_EmergencySupply
                          where x.FileId == emergencySupplyId
                          select new Model.FileInfoItem
                          {
                              FileId = x.FileId,
                              ProjectId = x.ProjectId,
                              FileCode = x.FileCode,
                              FileName = x.FileName,                              
                              UnitId = x.UnitId,
                              UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                              FileContent = x.FileContent,
                              CompileManId = x.CompileMan,
                              CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                              States = x.States,
                              AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.FileId).AttachUrl.Replace('\\', '/'),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取应急物资列表信息
        /// <summary>
        /// 获取应急物资列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencySupplyList(string projectId, string unitId, string strParam)
        {
            var getDataList = from x in Funs.DB.Emergency_EmergencySupply
                                       where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                      && (strParam == null || x.FileName.Contains(strParam) || x.FileCode.Contains(strParam))
                                       orderby x.FileCode descending
                                       select new Model.FileInfoItem
                                       {
                                           FileId = x.FileId,
                                           ProjectId = x.ProjectId,
                                           FileCode = x.FileCode,
                                           FileName = x.FileName,
                                           UnitId = x.UnitId,
                                           UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                           FileContent = x.FileContent,
                                           CompileManId = x.CompileMan,
                                           CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                           CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                           States = x.States,
                                           AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.FileId).AttachUrl.Replace('\\', '/'),
                                       };
            return getDataList.ToList();
        }
        #endregion        

        #region 根据主键ID获取应急队伍信息
        /// <summary>
        ///  根据主键ID获取应急队伍信息
        /// </summary>
        /// <param name="emergencyTeamAndTrainId"></param>
        /// <returns></returns>
        public static Model.FileInfoItem getEmergencyTeamAndTrainByEmergencyTeamAndTrainId(string emergencyTeamAndTrainId)
        {
            var getInfo = from x in Funs.DB.Emergency_EmergencyTeamAndTrain
                          where x.FileId == emergencyTeamAndTrainId
                          select new Model.FileInfoItem
                          {
                              FileId = x.FileId,
                              ProjectId = x.ProjectId,
                              FileCode = x.FileCode,
                              FileName = x.FileName,
                              UnitId = x.UnitId,
                              UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                              FileContent = x.FileContent,
                              CompileManId = x.CompileMan,
                              CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                              States = x.States,
                              AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.FileId).AttachUrl.Replace('\\', '/'),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取应急队伍列表信息
        /// <summary>
        /// 获取应急队伍列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.FileInfoItem> getEmergencyTeamAndTrainList(string projectId, string unitId, string strParam)
        {
            var getDataList = from x in Funs.DB.Emergency_EmergencyTeamAndTrain
                                       where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                      && (strParam == null || x.FileName.Contains(strParam) || x.FileCode.Contains(strParam))
                                       orderby x.FileCode descending
                                       select new Model.FileInfoItem
                                       {
                                           FileId = x.FileId,
                                           ProjectId = x.ProjectId,
                                           FileCode = x.FileCode,
                                           FileName = x.FileName,
                                           UnitId = x.UnitId,
                                           UnitName = Funs.DB.Base_Unit.First(y => y.UnitId == x.UnitId).UnitName,
                                           FileContent = x.FileContent,
                                           CompileManId = x.CompileMan,
                                           CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                           CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                           States = x.States,
                                           AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.ToKeyId == x.FileId).AttachUrl.Replace('\\', '/'),
                                       };
            return getDataList.ToList();
        }
        #endregion        
    }
}
