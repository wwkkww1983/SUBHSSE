using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 季节性/节假日检查明细表
    /// </summary>
    public class Check_CheckHolidayDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据季节性/节假日检查id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckHolidayDetail> GetCheckHolidayDetailByCheckHolidayId(string checkHolidayId)
        {
            return (from x in Funs.DB.Check_CheckHolidayDetail where x.CheckHolidayId == checkHolidayId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取季节性/节假日检查明细信息
        /// </summary>
        /// <param name="checkHolidayDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckHolidayDetail GetCheckHolidayDetailByCheckHolidayDetailId(string checkHolidayDetailId)
        {
            return Funs.DB.Check_CheckHolidayDetail.FirstOrDefault(e => e.CheckHolidayDetailId == checkHolidayDetailId);
        }

        /// <summary>
        /// 增加季节性/节假日检查明细信息
        /// </summary>
        /// <param name="CheckHolidayDetail"></param>
        public static void AddCheckHolidayDetail(Model.Check_CheckHolidayDetail CheckHolidayDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckHolidayDetail newCheckHolidayDetail = new Model.Check_CheckHolidayDetail
            {
                CheckHolidayDetailId = CheckHolidayDetail.CheckHolidayDetailId,
                CheckHolidayId = CheckHolidayDetail.CheckHolidayId,
                CheckItem = CheckHolidayDetail.CheckItem,
                CheckResult = CheckHolidayDetail.CheckResult,
                CheckOpinion = CheckHolidayDetail.CheckOpinion,
                CheckStation = CheckHolidayDetail.CheckStation,
                HandleResult = CheckHolidayDetail.HandleResult,
                CheckContent = CheckHolidayDetail.CheckContent,
                WorkArea = CheckHolidayDetail.WorkArea
            };
            db.Check_CheckHolidayDetail.InsertOnSubmit(newCheckHolidayDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改季节性/节假日检查明细信息
        /// </summary>
        /// <param name="CheckHolidayDetail"></param>
        public static void UpdateCheckHolidayDetail(Model.Check_CheckHolidayDetail CheckHolidayDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newCheckHolidayDetail = db.Check_CheckHolidayDetail.FirstOrDefault(x => x.CheckHolidayDetailId == CheckHolidayDetail.CheckHolidayDetailId);
            if (newCheckHolidayDetail != null)
            {
                newCheckHolidayDetail.CheckResult = CheckHolidayDetail.CheckResult;
                newCheckHolidayDetail.CheckOpinion = CheckHolidayDetail.CheckOpinion;
                newCheckHolidayDetail.CheckStation = CheckHolidayDetail.CheckStation;
                newCheckHolidayDetail.HandleResult = CheckHolidayDetail.HandleResult;
                newCheckHolidayDetail.CheckContent = CheckHolidayDetail.CheckContent;
                newCheckHolidayDetail.WorkArea = CheckHolidayDetail.WorkArea;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据季节性/节假日检查ID删除所有季节性/节假日检查明细信息
        /// </summary>
        /// <param name="checkHolidayId"></param>
        public static void DeleteCheckHolidayDetails(string checkHolidayId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckHolidayDetail where x.CheckHolidayId == checkHolidayId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.CheckHolidayDetailId);
                }
                db.Check_CheckHolidayDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据季节性/节假日检查ID删除一条季节性/节假日检查明细信息
        /// </summary>
        /// <param name="checkHolidayDetailId"></param>
        public static void DeleteCheckHolidayDetailById(string checkHolidayDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckHolidayDetail where x.CheckHolidayDetailId == checkHolidayDetailId select x).FirstOrDefault();
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckHolidayDetailId);
                db.Check_CheckHolidayDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
