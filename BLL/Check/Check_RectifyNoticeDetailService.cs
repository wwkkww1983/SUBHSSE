using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 隐患通知单明细表
    /// </summary>
    public class Check_RectifyNoticeDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据隐患通知单id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_RectifyNoticeDetail> GetRectifyNoticeDetailByRectifyNoticeId(string rectifyNoticeId)
        {
            return (from x in Funs.DB.Check_RectifyNoticeDetail where x.RectifyNoticeId == rectifyNoticeId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取隐患通知单明细信息
        /// </summary>
        /// <param name="rectifyNoticeDetailId"></param>
        /// <returns></returns>
        public static Model.Check_RectifyNoticeDetail GetRectifyNoticeDetailByRectifyNoticeDetailId(string rectifyNoticeDetailId)
        {
            return Funs.DB.Check_RectifyNoticeDetail.FirstOrDefault(e => e.RectifyNoticeDetailId == rectifyNoticeDetailId);
        }

        /// <summary>
        /// 增加隐患通知单明细信息
        /// </summary>
        /// <param name="RectifyNoticeDetail"></param>
        public static void AddRectifyNoticeDetail(Model.Check_RectifyNoticeDetail RectifyNoticeDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_RectifyNoticeDetail newRectifyNoticeDetail = new Model.Check_RectifyNoticeDetail
            {
                RectifyNoticeDetailId = RectifyNoticeDetail.RectifyNoticeDetailId,
                RectifyNoticeId = RectifyNoticeDetail.RectifyNoticeId,
                CheckItem = RectifyNoticeDetail.CheckItem,
                CheckItemType = RectifyNoticeDetail.CheckItemType,
                Unqualified = RectifyNoticeDetail.Unqualified,
                CheckArea = RectifyNoticeDetail.CheckArea,
                UnitId = RectifyNoticeDetail.UnitId,
                Suggestions = RectifyNoticeDetail.Suggestions,
                CheckContent = RectifyNoticeDetail.CheckContent
            };
            db.Check_RectifyNoticeDetail.InsertOnSubmit(newRectifyNoticeDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改隐患通知单明细信息
        /// </summary>
        /// <param name="RectifyNoticeDetail"></param>
        public static void UpdateRectifyNoticeDetail(Model.Check_RectifyNoticeDetail RectifyNoticeDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newRectifyNoticeDetail = db.Check_RectifyNoticeDetail.FirstOrDefault(x => x.RectifyNoticeDetailId == RectifyNoticeDetail.RectifyNoticeDetailId);
            if (newRectifyNoticeDetail != null)
            {
                newRectifyNoticeDetail.Unqualified = RectifyNoticeDetail.Unqualified;
                newRectifyNoticeDetail.CheckArea = RectifyNoticeDetail.CheckArea;
                newRectifyNoticeDetail.UnitId = RectifyNoticeDetail.UnitId;
                newRectifyNoticeDetail.Suggestions = RectifyNoticeDetail.Suggestions;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据隐患通知单ID删除所有隐患通知单明细信息
        /// </summary>
        /// <param name="rectifyNoticeId"></param>
        public static void DeleteRectifyNoticeDetails(string rectifyNoticeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_RectifyNoticeDetail where x.RectifyNoticeId == rectifyNoticeId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.RectifyNoticeDetailId);
                    Model.Check_CheckDayDetail dayDetail=BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(item.RectifyNoticeDetailId);
                    if (dayDetail != null)
                    {
                        dayDetail.RectifyNoticeId = null;
                        BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(dayDetail);
                    }
                    else
                    {
                        Model.Check_CheckSpecialDetail specialDetail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(item.RectifyNoticeDetailId);
                        if (specialDetail != null)
                        {
                            specialDetail.RectifyNoticeId = null;
                            BLL.Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(specialDetail);
                        }
                        else
                        {
                            Model.Check_CheckColligationDetail colligationDetail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(item.RectifyNoticeDetailId);
                            if (colligationDetail != null)
                            {
                                colligationDetail.RectifyNoticeId = null;
                                BLL.Check_CheckColligationDetailService.UpdateCheckColligationDetail(colligationDetail);
                            }
                        }
                    }
                }
                db.Check_RectifyNoticeDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据隐患通知单ID删除一条隐患通知单明细信息
        /// </summary>
        /// <param name="rectifyNoticeDetailId"></param>
        public static void DeleteRectifyNoticeDetailById(string rectifyNoticeDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_RectifyNoticeDetail where x.RectifyNoticeDetailId == rectifyNoticeDetailId select x).FirstOrDefault();
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.RectifyNoticeDetailId);
                db.Check_RectifyNoticeDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
