using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 施工方案
    /// </summary>
    public class ConstructSolutionController : ApiController
    {
        #region 根据 constructSolutionId获取施工方案
        /// <summary>
        ///  根据 constructSolutionId获取施工方案
        /// </summary>
        /// <param name="constructSolutionId"></param>
        /// <returns></returns>
        public Model.ResponeData getConstructSolutionByConstructSolutionId(string constructSolutionId)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getData = from x in Funs.DB.Solution_ConstructSolution
                              where x.ConstructSolutionId == constructSolutionId
                              select new Model.ConstructSolutionItem
                              {
                                  ConstructSolutionId = x.ConstructSolutionId,
                                  ProjectId = x.ProjectId,
                                  ConstructSolutionCode = x.ConstructSolutionCode,
                                  ConstructSolutionName = x.ConstructSolutionName,
                                  SolutinTypeName = Funs.DB.Sys_Const.FirstOrDefault(c=>c.GroupId== ConstValue.Group_CNProfessional && c.ConstValue == x.SolutinType).ConstText,
                                  FileContents = x.FileContents,
                                  AttachUrl = Funs.DB.AttachFile.FirstOrDefault(z=>z.ToKeyId==x.ConstructSolutionId).AttachUrl.Replace('\\', '/'),
                              };
 
                 responeData.data = getData;
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}
