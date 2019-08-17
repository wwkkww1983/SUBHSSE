using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class MillionsMonthlyReportItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 百万工时安全统计月报表明细表
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>百万工时安全统计月报表明细表</returns>
        public static Model.Information_MillionsMonthlyReportItem GetMillionsMonthlyReportItemByMillionsMonthlyReportItemId(string MillionsMonthlyReportItemId)
        {
            return Funs.DB.Information_MillionsMonthlyReportItem.FirstOrDefault(e => e.MillionsMonthlyReportItemId == MillionsMonthlyReportItemId);
        }

        /// <summary>
        /// 百万工时安全统计月报表明细表
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>百万工时安全统计月报表明细表</returns>
        public static Model.Information_MillionsMonthlyReportItem GetMillionsMonthlyReportItemByMillionsMonthlyReportIdAndTypeId(string MillionsMonthlyReportId)
        {
            return Funs.DB.Information_MillionsMonthlyReportItem.FirstOrDefault(e => e.MillionsMonthlyReportId == MillionsMonthlyReportId);
        }

        /// <summary>
        /// 根据主表Id判断是否存在明细记录
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表表Id</param>
        /// <returns>是否存在明细记录</returns>
        public static bool IsExitItems(string MillionsMonthlyReportId)
        {
            return (from x in Funs.DB.Information_MillionsMonthlyReportItem where x.MillionsMonthlyReportId == MillionsMonthlyReportId select x).Count() > 0;
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.Information_MillionsMonthlyReportItem> GetItems(string MillionsMonthlyReportId)
        {
            return (from x in Funs.DB.Information_MillionsMonthlyReportItem
                    where x.MillionsMonthlyReportId == MillionsMonthlyReportId
                    orderby x.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.Information_MillionsMonthlyReportItem> GetYearSumItems(string unitId, int? year,int? month)
        {
            return (from x in Funs.DB.Information_MillionsMonthlyReportItem
                    join y in Funs.DB.Information_MillionsMonthlyReport 
                    on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                    where y.UnitId == unitId && y.Year == year && y.Month <= month
                    && x.Affiliation == "本月合计"
                    select x).Distinct().ToList();
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.Information_MillionsMonthlyReportItem> GetAllUnitYearSumItems(int year, int month)
        {
            return (from x in Funs.DB.Information_MillionsMonthlyReportItem
                    join y in Funs.DB.Information_MillionsMonthlyReport
                    on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                    where y.Year == year && y.Month <= month && x.Affiliation == "本月合计"
                    select x).Distinct().ToList();
        }

        /// <summary>
        /// 根据主表Id获取明细记录集合(不包含本月合计行)
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId">百万工时安全统计月报表明细表Id</param>
        /// <returns>明细记录集合</returns>
        public static List<Model.Information_MillionsMonthlyReportItem> GetItemsNoSum(string MillionsMonthlyReportId)
        {
            return (from x in Funs.DB.Information_MillionsMonthlyReportItem
                    where x.MillionsMonthlyReportId == MillionsMonthlyReportId 
                    && (x.Affiliation != "本月合计" || x.Affiliation == null)
                    orderby x.SortIndex
                    select x).ToList();
        }

        /// <summary>
        /// 增加百万工时安全统计月报表明细表
        /// </summary>
        /// <param name="MillionsMonthlyReportItem">百万工时安全统计月报表明细表实体</param>
        public static void AddMillionsMonthlyReportItem(Model.Information_MillionsMonthlyReportItem MillionsMonthlyReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_MillionsMonthlyReportItem newMillionsMonthlyReportItem = new Model.Information_MillionsMonthlyReportItem
            {
                MillionsMonthlyReportItemId = MillionsMonthlyReportItem.MillionsMonthlyReportItemId,
                MillionsMonthlyReportId = MillionsMonthlyReportItem.MillionsMonthlyReportId,
                SortIndex = MillionsMonthlyReportItem.SortIndex,
                Affiliation = MillionsMonthlyReportItem.Affiliation,
                Name = MillionsMonthlyReportItem.Name,
                PostPersonNum = MillionsMonthlyReportItem.PostPersonNum,
                SnapPersonNum = MillionsMonthlyReportItem.SnapPersonNum,
                ContractorNum = MillionsMonthlyReportItem.ContractorNum,
                SumPersonNum = MillionsMonthlyReportItem.SumPersonNum,
                TotalWorkNum = MillionsMonthlyReportItem.TotalWorkNum,
                SeriousInjuriesNum = MillionsMonthlyReportItem.SeriousInjuriesNum,
                SeriousInjuriesPersonNum = MillionsMonthlyReportItem.SeriousInjuriesPersonNum,
                SeriousInjuriesLossHour = MillionsMonthlyReportItem.SeriousInjuriesLossHour,
                MinorAccidentNum = MillionsMonthlyReportItem.MinorAccidentNum,
                MinorAccidentPersonNum = MillionsMonthlyReportItem.MinorAccidentPersonNum,
                MinorAccidentLossHour = MillionsMonthlyReportItem.MinorAccidentLossHour,
                OtherAccidentNum = MillionsMonthlyReportItem.OtherAccidentNum,
                OtherAccidentPersonNum = MillionsMonthlyReportItem.OtherAccidentPersonNum,
                OtherAccidentLossHour = MillionsMonthlyReportItem.OtherAccidentLossHour,
                RestrictedWorkPersonNum = MillionsMonthlyReportItem.RestrictedWorkPersonNum,
                RestrictedWorkLossHour = MillionsMonthlyReportItem.RestrictedWorkLossHour,
                MedicalTreatmentPersonNum = MillionsMonthlyReportItem.MedicalTreatmentPersonNum,
                MedicalTreatmentLossHour = MillionsMonthlyReportItem.MedicalTreatmentLossHour,
                FireNum = MillionsMonthlyReportItem.FireNum,
                ExplosionNum = MillionsMonthlyReportItem.ExplosionNum,
                TrafficNum = MillionsMonthlyReportItem.TrafficNum,
                EquipmentNum = MillionsMonthlyReportItem.EquipmentNum,
                QualityNum = MillionsMonthlyReportItem.QualityNum,
                OtherNum = MillionsMonthlyReportItem.OtherNum,
                FirstAidDressingsNum = MillionsMonthlyReportItem.FirstAidDressingsNum,
                AttemptedEventNum = MillionsMonthlyReportItem.AttemptedEventNum,
                LossDayNum = MillionsMonthlyReportItem.LossDayNum
            };

            db.Information_MillionsMonthlyReportItem.InsertOnSubmit(newMillionsMonthlyReportItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改百万工时安全统计月报表明细表
        /// </summary>
        /// <param name="MillionsMonthlyReportItem">百万工时安全统计月报表明细表实体</param>
        public static void UpdateMillionsMonthlyReportItem(Model.Information_MillionsMonthlyReportItem MillionsMonthlyReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Information_MillionsMonthlyReportItem newMillionsMonthlyReportItem = db.Information_MillionsMonthlyReportItem.FirstOrDefault(e => e.MillionsMonthlyReportItemId == MillionsMonthlyReportItem.MillionsMonthlyReportItemId);
            newMillionsMonthlyReportItem.SortIndex = MillionsMonthlyReportItem.SortIndex;
            newMillionsMonthlyReportItem.Affiliation = MillionsMonthlyReportItem.Affiliation;
            newMillionsMonthlyReportItem.Name = MillionsMonthlyReportItem.Name;
            newMillionsMonthlyReportItem.PostPersonNum = MillionsMonthlyReportItem.PostPersonNum;
            newMillionsMonthlyReportItem.SnapPersonNum = MillionsMonthlyReportItem.SnapPersonNum;
            newMillionsMonthlyReportItem.ContractorNum = MillionsMonthlyReportItem.ContractorNum;
            newMillionsMonthlyReportItem.SumPersonNum = MillionsMonthlyReportItem.SumPersonNum;
            newMillionsMonthlyReportItem.TotalWorkNum = MillionsMonthlyReportItem.TotalWorkNum;
            newMillionsMonthlyReportItem.SeriousInjuriesNum = MillionsMonthlyReportItem.SeriousInjuriesNum;
            newMillionsMonthlyReportItem.SeriousInjuriesPersonNum = MillionsMonthlyReportItem.SeriousInjuriesPersonNum;
            newMillionsMonthlyReportItem.SeriousInjuriesLossHour = MillionsMonthlyReportItem.SeriousInjuriesLossHour;
            newMillionsMonthlyReportItem.MinorAccidentNum = MillionsMonthlyReportItem.MinorAccidentNum;
            newMillionsMonthlyReportItem.MinorAccidentPersonNum = MillionsMonthlyReportItem.MinorAccidentPersonNum;
            newMillionsMonthlyReportItem.MinorAccidentLossHour = MillionsMonthlyReportItem.MinorAccidentLossHour;
            newMillionsMonthlyReportItem.OtherAccidentNum = MillionsMonthlyReportItem.OtherAccidentNum;
            newMillionsMonthlyReportItem.OtherAccidentPersonNum = MillionsMonthlyReportItem.OtherAccidentPersonNum;
            newMillionsMonthlyReportItem.OtherAccidentLossHour = MillionsMonthlyReportItem.OtherAccidentLossHour;
            newMillionsMonthlyReportItem.RestrictedWorkPersonNum = MillionsMonthlyReportItem.RestrictedWorkPersonNum;
            newMillionsMonthlyReportItem.RestrictedWorkLossHour = MillionsMonthlyReportItem.RestrictedWorkLossHour;
            newMillionsMonthlyReportItem.MedicalTreatmentPersonNum = MillionsMonthlyReportItem.MedicalTreatmentPersonNum;
            newMillionsMonthlyReportItem.MedicalTreatmentLossHour = MillionsMonthlyReportItem.MedicalTreatmentLossHour;
            newMillionsMonthlyReportItem.FireNum = MillionsMonthlyReportItem.FireNum;
            newMillionsMonthlyReportItem.ExplosionNum = MillionsMonthlyReportItem.ExplosionNum;
            newMillionsMonthlyReportItem.TrafficNum = MillionsMonthlyReportItem.TrafficNum;
            newMillionsMonthlyReportItem.EquipmentNum = MillionsMonthlyReportItem.EquipmentNum;
            newMillionsMonthlyReportItem.QualityNum = MillionsMonthlyReportItem.QualityNum;
            newMillionsMonthlyReportItem.OtherNum = MillionsMonthlyReportItem.OtherNum;
            newMillionsMonthlyReportItem.FirstAidDressingsNum = MillionsMonthlyReportItem.FirstAidDressingsNum;
            newMillionsMonthlyReportItem.AttemptedEventNum = MillionsMonthlyReportItem.AttemptedEventNum;
            newMillionsMonthlyReportItem.LossDayNum = MillionsMonthlyReportItem.LossDayNum;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据Id删除所有数据
        /// </summary>
        /// <param name="MillionsMonthlyReportItemId"></param>
        public static void DeleteMillionsMonthlyReportItemByMillionsMonthlyReportId(string MillionsMonthlyReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = from x in db.Information_MillionsMonthlyReportItem where x.MillionsMonthlyReportId == MillionsMonthlyReportId select x;
            if (q != null)
            {
                db.Information_MillionsMonthlyReportItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
