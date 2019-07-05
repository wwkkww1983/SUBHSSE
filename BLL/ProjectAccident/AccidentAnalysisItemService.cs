using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace BLL
{
    public class AccidentAnalysisItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 事故统计明细表
        /// </summary>
        /// <param name="AccidentAnalysisItemId">事故统计明细表Id</param>
        /// <returns>事故统计明细表</returns>
        public static Model.ProjectAccident_AccidentAnalysisItem GetAccidentAnalysisItemByAccidentAnalysisItemId(string AccidentAnalysisItemId)
        {
            return Funs.DB.ProjectAccident_AccidentAnalysisItem.FirstOrDefault(e => e.AccidentAnalysisItemId == AccidentAnalysisItemId);
        }

        /// <summary>
        /// 事故统计明细表
        /// </summary>
        /// <param name="AccidentAnalysisItemId">事故统计明细表Id</param>
        /// <returns>事故统计明细表</returns>
        public static Model.ProjectAccident_AccidentAnalysisItem GetAccidentAnalysisItemByAccidentAnalysisIdAndTypeId(string AccidentAnalysisId,string AccidentType)
        {
            return Funs.DB.ProjectAccident_AccidentAnalysisItem.FirstOrDefault(e => e.AccidentAnalysisId == AccidentAnalysisId && e.AccidentType == AccidentType);
        }

        /// <summary>
        /// 根据主表Id判断是否存在明细记录
        /// </summary>
        /// <param name="AccidentAnalysisItemId">事故统计表Id</param>
        /// <returns>是否存在明细记录</returns>
        public static bool IsExitItems(string AccidentAnalysisId)
        {
            return (from x in Funs.DB.ProjectAccident_AccidentAnalysisItem where x.AccidentAnalysisId == AccidentAnalysisId select x).Count() > 0;
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合
        /// </summary>
        /// <param name="AccidentAnalysisItemId">事故统计明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.ProjectAccident_AccidentAnalysisItem> GetItems(string AccidentAnalysisId)
        {
            return (from x in Funs.DB.ProjectAccident_AccidentAnalysisItem
                    join y in Funs.DB.Sys_Const on x.AccidentType equals y.ConstText
                    where x.AccidentAnalysisId == AccidentAnalysisId && y.GroupId==ConstValue.Group_0012
                    orderby y.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合(不包含总计行)
        /// </summary>
        /// <param name="AccidentAnalysisItemId">事故统计明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.ProjectAccident_AccidentAnalysisItem> GetItemsNoSum(string AccidentAnalysisId)
        {
           return (from x in Funs.DB.ProjectAccident_AccidentAnalysisItem
                    join y in Funs.DB.Sys_Const on x.AccidentType equals y.ConstText
                   where x.AccidentAnalysisId == AccidentAnalysisId  && y.GroupId == ConstValue.Group_0012
                    orderby y.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 增加事故统计明细表
        /// </summary>
        /// <param name="AccidentAnalysisItem">事故统计明细表实体</param>
        public static void AddAccidentAnalysisItem(Model.ProjectAccident_AccidentAnalysisItem AccidentAnalysisItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentAnalysisItem newAccidentAnalysisItem = new Model.ProjectAccident_AccidentAnalysisItem
            {
                AccidentAnalysisItemId = AccidentAnalysisItem.AccidentAnalysisItemId,
                AccidentAnalysisId = AccidentAnalysisItem.AccidentAnalysisId,
                AccidentType = AccidentAnalysisItem.AccidentType,

                Death = AccidentAnalysisItem.Death,
                Injuries = AccidentAnalysisItem.Injuries,
                MinorInjuries = AccidentAnalysisItem.MinorInjuries
            };


            db.ProjectAccident_AccidentAnalysisItem.InsertOnSubmit(newAccidentAnalysisItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故统计明细表
        /// </summary>
        /// <param name="AccidentAnalysisItem">事故统计明细表实体</param>
        public static void UpdateAccidentAnalysisItem(Model.ProjectAccident_AccidentAnalysisItem AccidentAnalysisItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentAnalysisItem newAccidentAnalysisItem = db.ProjectAccident_AccidentAnalysisItem.FirstOrDefault(e => e.AccidentAnalysisItemId == AccidentAnalysisItem.AccidentAnalysisItemId);
            newAccidentAnalysisItem.AccidentAnalysisId = AccidentAnalysisItem.AccidentAnalysisId;
            newAccidentAnalysisItem.AccidentType = AccidentAnalysisItem.AccidentType;

            newAccidentAnalysisItem.Death = AccidentAnalysisItem.Death;
            newAccidentAnalysisItem.Injuries = AccidentAnalysisItem.Injuries;
            newAccidentAnalysisItem.MinorInjuries = AccidentAnalysisItem.MinorInjuries;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据Id删除所有数据
        /// </summary>
        /// <param name="AccidentAnalysisItemId"></param>
        public static void DeleteAccidentAnalysisItemByAccidentAnalysisId(string AccidentAnalysisId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = from x in db.ProjectAccident_AccidentAnalysisItem where x.AccidentAnalysisId == AccidentAnalysisId select x;
            if (q != null)
            {                
                db.ProjectAccident_AccidentAnalysisItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
