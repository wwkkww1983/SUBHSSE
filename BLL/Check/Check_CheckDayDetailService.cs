using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 日常巡检明细表
    /// </summary>
    public class Check_CheckDayDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据日常巡检id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckDayDetail> GetCheckDayDetailByCheckDayId(string checkDayId)
        {
            return (from x in Funs.DB.Check_CheckDayDetail where x.CheckDayId == checkDayId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取日常巡检明细信息
        /// </summary>
        /// <param name="checkDayDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckDayDetail GetCheckDayDetailByCheckDayDetailId(string checkDayDetailId)
        {
            return Funs.DB.Check_CheckDayDetail.FirstOrDefault(e => e.CheckDayDetailId == checkDayDetailId);
        }

        /// <summary>
        /// 增加日常巡检明细信息
        /// </summary>
        /// <param name="CheckDayDetail"></param>
        public static void AddCheckDayDetail(Model.Check_CheckDayDetail CheckDayDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckDayDetail newCheckDayDetail = new Model.Check_CheckDayDetail
            {
                CheckDayDetailId = CheckDayDetail.CheckDayDetailId,
                CheckDayId = CheckDayDetail.CheckDayId,
                CheckItem = CheckDayDetail.CheckItem,
                CheckItemType = CheckDayDetail.CheckItemType,
                Unqualified = CheckDayDetail.Unqualified,
                CheckArea = CheckDayDetail.CheckArea,
                UnitId = CheckDayDetail.UnitId,
                HandleStep = CheckDayDetail.HandleStep,
                CompleteStatus = CheckDayDetail.CompleteStatus,
                RectifyNoticeId = CheckDayDetail.RectifyNoticeId,
                LimitedDate = CheckDayDetail.LimitedDate,
                CompletedDate = CheckDayDetail.CompletedDate,
                Suggestions = CheckDayDetail.Suggestions,
                CheckContent = CheckDayDetail.CheckContent,
                WorkArea = CheckDayDetail.WorkArea
            };
            db.Check_CheckDayDetail.InsertOnSubmit(newCheckDayDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改日常巡检明细信息
        /// </summary>
        /// <param name="CheckDayDetail"></param>
        public static void UpdateCheckDayDetail(Model.Check_CheckDayDetail CheckDayDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newCheckDayDetail = db.Check_CheckDayDetail.FirstOrDefault(x => x.CheckDayDetailId == CheckDayDetail.CheckDayDetailId);
            if (newCheckDayDetail != null)
            {
                newCheckDayDetail.Unqualified = CheckDayDetail.Unqualified;
                newCheckDayDetail.CheckArea = CheckDayDetail.CheckArea;
                newCheckDayDetail.UnitId = CheckDayDetail.UnitId;
                newCheckDayDetail.HandleStep = CheckDayDetail.HandleStep;
                newCheckDayDetail.CompleteStatus = CheckDayDetail.CompleteStatus;
                newCheckDayDetail.RectifyNoticeId = CheckDayDetail.RectifyNoticeId;
                newCheckDayDetail.LimitedDate = CheckDayDetail.LimitedDate;
                newCheckDayDetail.CompletedDate = CheckDayDetail.CompletedDate;
                newCheckDayDetail.Suggestions = CheckDayDetail.Suggestions;
                newCheckDayDetail.CheckContent = CheckDayDetail.CheckContent;
                newCheckDayDetail.WorkArea = CheckDayDetail.WorkArea;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据日常巡检ID删除所有日常巡检明细信息
        /// </summary>
        /// <param name="checkDayId"></param>
        public static void DeleteCheckDayDetails(string checkDayId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckDayDetail where x.CheckDayId == checkDayId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.CheckDayDetailId);
                }
                db.Check_CheckDayDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据日常巡检ID删除一条日常巡检明细信息
        /// </summary>
        /// <param name="checkDayDetailId"></param>
        public static void DeleteCheckDayDetailById(string checkDayDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckDayDetail where x.CheckDayDetailId == checkDayDetailId select x).FirstOrDefault();
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckDayDetailId);              
                db.Check_CheckDayDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
