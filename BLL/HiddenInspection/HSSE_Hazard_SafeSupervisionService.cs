using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// APP领导督察表
    /// </summary>
    public static class HSSE_Hazard_SafeSupervisionService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary> 
        /// 根据APP领导督察Id获取一个APP领导督察信息
        /// </summary>
        /// <param name="safeSupervisionId">APP领导督察Id</param>
        /// <returns>一个APP领导督察实体</returns>
        public static Model.HSSE_Hazard_SafeSupervision GetSafeSupervisionBySafeSupervisionId(string safeSupervisionId)
        {
            return Funs.DB.HSSE_Hazard_SafeSupervision.FirstOrDefault(x => x.SafeSupervisionId == safeSupervisionId);
        }

        /// <summary> 
        /// 根据APP领导督察Id获取一个APP领导督察视图信息
        /// </summary>
        /// <param name="safeSupervisionId">APP领导督察Id</param>
        /// <returns>一个APP领导督察实体</returns>
        public static Model.View_Hazard_SafeSupervision GetSafeSupervisionViewBySafeSupervisionId(string safeSupervisionId)
        {
            return Funs.DB.View_Hazard_SafeSupervision.FirstOrDefault(x => x.SafeSupervisionId == safeSupervisionId);
        }

        /// <summary>
        /// 增加APP领导督察信息
        /// </summary>
        /// <param name="safeSupervision">APP领导督察实体</param>
        public static void AddSafeSupervision(Model.HSSE_Hazard_SafeSupervision safeSupervision)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_SafeSupervision newSafeSupervision = new Model.HSSE_Hazard_SafeSupervision();
            newSafeSupervision.SafeSupervisionId = safeSupervision.SafeSupervisionId;
            newSafeSupervision.ProjectId = safeSupervision.ProjectId;
            newSafeSupervision.CheckType = safeSupervision.CheckType;
            newSafeSupervision.CheckManId = safeSupervision.CheckManId;
            newSafeSupervision.CheckDate = safeSupervision.CheckDate;

            db.HSSE_Hazard_SafeSupervision.InsertOnSubmit(newSafeSupervision);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改APP领导督察信息
        /// </summary>
        /// <param name="safeSupervision">APP领导督察实体</param>
        public static void UpdateSafeSupervision(Model.HSSE_Hazard_SafeSupervision safeSupervision)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_SafeSupervision newSafeSupervision = db.HSSE_Hazard_SafeSupervision.First(e => e.SafeSupervisionId == safeSupervision.SafeSupervisionId);
            newSafeSupervision.CheckType = safeSupervision.CheckType;
            newSafeSupervision.CheckManId = safeSupervision.CheckManId;
            newSafeSupervision.CheckDate = safeSupervision.CheckDate;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据APP领导督察Id删除一个APP领导督察信息
        /// </summary>
        /// <param name="safeSupervisionId">APP领导督察Id</param>
        public static void DeleteSafeSupervision(string safeSupervisionId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.HSSE_Hazard_SafeSupervision safeSupervision = db.HSSE_Hazard_SafeSupervision.First(e => e.SafeSupervisionId == safeSupervisionId);
            if (safeSupervision != null)
            {
                db.HSSE_Hazard_SafeSupervision.DeleteOnSubmit(safeSupervision);
                db.SubmitChanges();
            }
        }
    }
}
